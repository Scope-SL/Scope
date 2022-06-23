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
    using System.Net;
    using System.Net.Http;
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
            string GameFolder = GetSLFolder();

            try
            {
                var download = await Download(DownloadUrl);
                var sha256 = SHA256.Create();
                var hash = BitConverter.ToString(sha256.ComputeHash(download)).Replace("-", string.Empty);

                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile("https://github.com/moddedmcplayer/scope-files/raw/main/ScopeStuff.zip", "ScopeStuff.zip");
                }

                if (hash != ZipHash)
                {
                    Console.WriteLine("The archive hash does not match!");
                    Console.Read();
                    Environment.Exit(0);
                }

                var archive = new ZipArchive(download);
                if (archive.Entries.All(x => !x.FullName.StartsWith("ScopeStuff")))
                {
                    Console.WriteLine("Unable to find archive contents, is the installer outdated?");
                    Console.Read();
                    Environment.Exit(0);
                }

                archive.ExtractToDirectory(GameFolder, true);
                if (!Directory.Exists(Path.Combine(GameFolder, "ScopeStuff")))
                {
                    return;
                }

                var AC = Path.Combine(GameFolder, "SL-AC.dll");
                if (File.Exists(AC))
                {
                    File.Move(AC, $"{AC}.disabled");
                }

                foreach (var file in Directory.GetFiles(Path.Combine(GameFolder, "ScopeStuff")))
                {
                    var fileName = file
                        .Substring(file.Length - file
                            .ToCharArray()
                            .Reverse()
                            .ToList()
                            .IndexOf(Path.DirectorySeparatorChar));
                    var originalPath = Path.Combine(GameFolder, fileName);

                    if (File.Exists(originalPath) && DoNotOverwrite.Contains(fileName))
                    {
                        if (!File.Exists($"{originalPath}.old"))
                        {
                            File.Move(originalPath, $"{originalPath}.old", true);
                        }
                    }

                    File.Move(file, originalPath, true);
                }

                foreach (var dir in Directory.GetDirectories(Path.Combine(GameFolder, "ScopeStuff")))
                {
                    Directory.Move(dir, dir.Replace("ScopeStuff\\", string.Empty));
                }
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

        private static string GetSLFolder()
        {
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
            if (!vdfFile.Exists)
            {
                string content;
                using (var stream = vdfFile.OpenRead())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                var matches = Regex.Matches(content, "\"\\d\"\\s+\"(?<path>.+)\"");
                foreach (Match match in matches)
                {
                    string path = match.Groups["path"].Value;
                    path = path.Replace(@"\\", @"\");
                    libraryPaths.Add(path);
                }

                foreach (var path in libraryPaths)
                {
                    if (Directory.Exists(path) && Directory.GetDirectories(path).Contains(SLFolderName))
                    {
                        SLPath = path;
                    }
                }
            }

            return SLPath;
        }
    }
}