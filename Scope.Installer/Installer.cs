// -----------------------------------------------------------------------
// <copyright file="Installer.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Installer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ICSharpCode.SharpZipLib.GZip;
    using ICSharpCode.SharpZipLib.Tar;

    /// <summary>
    /// The installer tool.
    /// </summary>
    public class Installer
    {
        /// <summary>
        /// The URL to download the Scope files from.
        /// </summary>
        private const string DownloadUrl = "FILL ME";

        /// <summary>
        /// This method is the entrypoint of the program.
        /// </summary>
        /// <param name="args"><see cref="string"/>[] args.</param>
        public static async Task Main(string[] args)
        {
            try
            {
                var download = await Download(DownloadUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.Read();
                Environment.Exit(0);
            }
        }

        private static async Task<TarInputStream> Download(string url)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(480),
            };

            try
            {
                using var download = await client.GetAsync(DownloadUrl).ConfigureAwait(false);
                responseMessage = download;
            }
            catch (Exception ex)
            {
                if (!(ex is TaskCanceledException || ex is HttpRequestException))
                {
                    throw;
                }

                Console.WriteLine("Check your internet connect and Scope server status and try again");
            }

            using var downloadArchive = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var gzInputStream = new GZipInputStream(downloadArchive);
            using var tarInputStream = new TarInputStream(gzInputStream, null);
            return tarInputStream;
        }
    }
}