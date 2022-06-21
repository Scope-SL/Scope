// -----------------------------------------------------------------------
// <copyright file="Installer.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------


namespace Scope.Installer
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ICSharpCode.SharpZipLib.Zip;

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
                if (!download.CanDecompressEntry)
                {
                    Console.WriteLine("Something went wrong downloading!");
                    Console.Read();
                    Environment.Exit(0);
                }

                ZipEntry zipEntry;
                while ((zipEntry = download.GetNextEntry()) is not null)
                {
                    string name = Path.Combine(GameFolder, ZipEntry.CleanName(zipEntry.Name) ?? zipEntry.Name);
                    if (zipEntry.IsDirectory)
                    {
                        Directory.CreateDirectory(name);
                    }
                    else if (!string.IsNullOrEmpty(name))
                    {
                        if (File.Exists(name) && ConfirmOverwrite(name))
                            continue;
                        using (FileStream streamWriter = File.Create(name))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = download.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
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
        /// Confirms overwriting of existing files.
        /// </summary>
        /// <param name="filename">The path of the file.</param>
        /// <returns>Whether or not to overwrite.</returns>
        private static bool ConfirmOverwrite(string filename)
        {
            File.Move(filename, filename + ".vanilla");
            return true;
        }

        /// <summary>
        /// Downloads a zip from the giving URL.
        /// </summary>
        /// <param name="url">The URL to download from.</param>
        /// <returns>The downloaded file.</returns>
        private static async Task<ZipInputStream> Download(string url)
        {
            HttpResponseMessage message = new HttpResponseMessage();

            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(480),
            };

            try
            {
                using var download = await client.GetAsync(DownloadUrl).ConfigureAwait(false);
                message = download;
            }
            catch (Exception ex)
            {
                if (!(ex is TaskCanceledException || ex is HttpRequestException))
                {
                    throw;
                }

                Console.WriteLine("Check your internet connection and Scope server status and try again");
            }

            await using var downloadArchive = await message.Content.ReadAsStreamAsync().ConfigureAwait(false);
            await using var zipInputStream = new ZipInputStream(downloadArchive);
            return zipInputStream;
        }
    }
}