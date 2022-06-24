// -----------------------------------------------------------------------
// <copyright file="Installer.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Installer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.Win32;

    /// <summary>
    /// The installer tool.
    /// </summary>
    public class Installer
    {
        /// <summary>
        /// The URL to download the Scope files from.
        /// </summary>
        private const string DownloadUrl = "https://scopesl.com/ScopeStuff.zip";

        /// <summary>
        /// The download hash.
        /// </summary>
        private const string ZipHash = "DD567472AFF7FCF14916EF3AF4E80EA58BB02AF8200EEF9BED639307AF5A2D67";

        /// <summary>
        /// Files that are renamed instead of overwritten.
        /// </summary>
        private static readonly List<string> DoNotOverwrite = new()
        {
            "SCPSL.exe",
        };

        /// <summary>
        /// This method is the entrypoint of the program.
        /// </summary>
        /// <param name="args"><see cref="string"/>[] args.</param>
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Scope.Installer {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine("Finding SL installation...");
            string gameFolder = await GetSLFolder();
            if (!Directory.Exists(gameFolder) || Directory.GetFiles(gameFolder).Contains("SCPSL.exe"))
            {
                Console.WriteLine("Could not find the game folder, aborting.");
                Console.Read();
                Environment.Exit(0);
            }

            try
            {
                Console.WriteLine("Downloading files...");
                var download = await Download(DownloadUrl);
                var sha256 = SHA256.Create();
                Console.WriteLine("Checking hash...");
                var hash = BitConverter.ToString(sha256.ComputeHash(download)).Replace("-", string.Empty);

                if (hash != ZipHash)
                {
                    Console.WriteLine("The archive hash does not match!");
                    Console.Read();
                    Environment.Exit(0);
                }

                Console.WriteLine("Extracting files...");
                var archive = new ZipArchive(download);
                if (archive.Entries.All(x => !x.FullName.StartsWith("ScopeStuff")))
                {
                    Console.WriteLine("Unable to find archive contents, is the installer outdated?");
                    Console.Read();
                    Environment.Exit(0);
                }

                Console.WriteLine("Moving files...");
                archive.ExtractToDirectory(gameFolder, true);
                if (!Directory.Exists(Path.Combine(gameFolder, "ScopeStuff")))
                {
                    return;
                }

                var AC = Path.Combine(gameFolder, "SL-AC.dll");
                if (File.Exists(AC))
                {
                    File.Move(AC, $"{AC}.disabled");
                }

                foreach (var file in Directory.GetFiles(Path.Combine(gameFolder, "ScopeStuff")))
                {
                    var fileName = file
                        .Substring(file.Length - file
                            .ToCharArray()
                            .Reverse()
                            .ToList()
                            .IndexOf(Path.DirectorySeparatorChar));
                    var originalPath = Path.Combine(gameFolder, fileName);

                    if (File.Exists(originalPath) && DoNotOverwrite.Contains(fileName))
                    {
                        if (!File.Exists($"{originalPath}.old"))
                        {
                            File.Move(originalPath, $"{originalPath}.old", true);
                        }
                    }

                    File.Move(file, originalPath, true);
                }

                foreach (var dir in Directory.GetDirectories(Path.Combine(gameFolder, "ScopeStuff")))
                {
                    var newDir = dir.Replace("ScopeStuff\\", string.Empty);
                    if (Directory.Exists(newDir))
                    {
                        Directory.Delete(newDir, true);
                    }

                    Directory.Move(dir, newDir);
                }

                Console.WriteLine("Cleaning up...");
                if (Directory.Exists(Path.Combine(gameFolder, "ScopeStuff")))
                {
                    Directory.Delete(Path.Combine(gameFolder, "ScopeStuff"));
                }

                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.Read();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Downloads a zip from the giving URL.
        /// </summary>
        /// <param name="url">The URL to download from.</param>
        /// <returns>The downloaded file.</returns>
        private static async Task<Stream> Download(string url)
        {
            Stream stream = Stream.Null;

            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(480),
            };
            client.DefaultRequestHeaders.Add("User-Agent", "Scope Installer");

            try
            {
                var download = await client.GetAsync(DownloadUrl).ConfigureAwait(false);
                stream = await download.Content.ReadAsStreamAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (!(ex is TaskCanceledException || ex is HttpRequestException))
                {
                    throw;
                }

                Console.WriteLine("Check your internet connection and Scope server status and try again");
                Console.Read();
                Environment.Exit(0);
            }

            return stream;
        }

        private const string DefaultSteamPath = @"C:\Program Files (x86)\Steam";
        private const string DefaultSLPath = @"C:\Program Files (x86)\Steam\steamapps\common\SCP Secret Laboratory";
        private const string SLFolderName = @"SCP Secret Laboratory";

        private static async Task<string> GetSLFolder()
        {
            bool usingDefault = true;
            string steamPath;
            RegistryKey softwareKey = null;
            try
            {
                string softwarePath = Environment.Is64BitProcess ? @"SOFTWARE\WOW6432Node" : "SOFTWARE";
                softwareKey = Registry.LocalMachine.OpenSubKey(softwarePath, false);

                using (var key = softwareKey.OpenSubKey(@"Valve\Steam"))
                {
                    var obj = key.GetValue("InstallPath");
                    var installPath = obj as string;
                    bool foundSteam = (!string.IsNullOrWhiteSpace(installPath) && Directory.Exists(installPath));

                    Console.WriteLine(foundSteam ? $"Found Steam at: {installPath}" : $"Could not find Steam, using default path: {DefaultSteamPath}");
                    steamPath = foundSteam ? installPath : DefaultSteamPath;
                }
            }
            catch
            {
                steamPath = DefaultSteamPath;
            }
            finally
            {
                if (softwareKey != null)
                    softwareKey.Close();
            }

            string SLPath = DefaultSLPath;
            var libraryPaths = new List<string>();

            var vdfFile = new FileInfo(Path.Combine(steamPath, @"steamapps\libraryfolders.vdf"));
            if (vdfFile.Exists)
            {
                string content;
                using (var stream = vdfFile.OpenRead())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                string[] lines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"path";
                foreach (string line in lines)
                {
                    if (!Regex.IsMatch(line, pattern))
                    {
                        continue;
                    }

                    var path = line
                        .Replace(@"\\", @"\")
                        .Replace("path", string.Empty)
                        .Replace("\"", string.Empty);

                    path = path.Substring(path.ToCharArray().ToList()
                        .FindIndex(x => !string.IsNullOrWhiteSpace(x.ToString())));
                    libraryPaths.Add(path);
                }

                foreach (var path in libraryPaths)
                {
                    var gamePath = Path.Combine(path, "steamapps", "common");
                    if (Directory.Exists(gamePath) && Directory.GetDirectories(gamePath).Any(x => x.EndsWith(SLFolderName)))
                    {
                        SLPath = Path.Combine(gamePath, SLFolderName);
                        usingDefault = false;
                        Console.WriteLine($"Found SL install path: {SLPath}");
                        break;
                    }

                    if (Directory.Exists(path) && Directory.GetDirectories(path).Any(x => x.EndsWith(SLFolderName)))
                    {
                        SLPath = Path.Combine(path, SLFolderName);
                        usingDefault = false;
                        Console.WriteLine($"Found SL install path: {SLPath}");
                        break;
                    }
                }
            }

            if (usingDefault)
            {
                Console.WriteLine($"Unable to auto-detect SL path, using default path: {DefaultSLPath}");
            }

            return SLPath;
        }
    }
}