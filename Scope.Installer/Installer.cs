// -----------------------------------------------------------------------
// <copyright file="Installer.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Scope.Installer
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The installer tool.
    /// </summary>
    public class Installer
    {
        /// <summary>
        /// The URL to download the Scope files from.
        /// </summary>
        private const string DownloadUrl = "https://scopesl.com/ScopeStuff.zip";

        // TODO: Autodetect game path.

        /// <summary>
        /// The game folder path.
        /// </summary>
        private const string GameFolder = @"C:\Program Files (x86)\Steam\steamapps\common\SCP Secret Laboratory";

        /// <summary>
        /// This method is the entrypoint of the program.
        /// </summary>
        /// <param name="args"><see cref="string"/>[] args.</param>
        public static async Task Main(string[] args)
        {
            try
            {
                var download = await Download(DownloadUrl);
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

                foreach (var file in Directory.GetFiles(Path.Combine(GameFolder, "ScopeStuff")))
                {
                    var originalPath = Path.Combine(GameFolder, file
                        .Substring(file.Length - file
                            .ToCharArray()
                            .Reverse()
                            .ToList()
                            .IndexOf(Path.DirectorySeparatorChar)));

                    if (File.Exists(originalPath))
                    {
                        File.Move(originalPath, $"{originalPath}.old", true);
                    }

                    File.Move(file, originalPath, true);
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
    }
}