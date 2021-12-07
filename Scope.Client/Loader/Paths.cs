// -----------------------------------------------------------------------
// <copyright file="Paths.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using System.IO;

    /// <summary>
    /// A tool to easily manage and interact with paths.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets the SCP: Secret Laboratory path.
        /// </summary>
        public static string SCPSL { get; }

        /// <summary>
        /// Gets or sets the Scope path.
        /// </summary>
        public static string Scope { get; set; }

        /// <summary>
        /// Gets or sets the mods path.
        /// </summary>
        public static string Mods { get; set; }

        /// <summary>
        /// Gets or sets the required modules and dependecies path.
        /// </summary>
        public static string Dependencies { get; set; }

        /// <summary>
        /// Gets or sets configs path.
        /// </summary>
        public static string Configs { get; set; }

        /// <summary>
        /// Gets or sets configs path.
        /// </summary>
        public static string Config { get; set; }

        /// <summary>
        /// Reloads all paths.
        /// </summary>
        /// <param name="rootDirectory">The new root directory name.</param>
        internal static void Reload(string rootDirectory = "Scope")
        {
            Scope = Path.GetFullPath(rootDirectory);
            Mods = Path.Combine(Scope, "Mods");
            Dependencies = Path.Combine(Mods, "Dependencies");
            Configs = Path.Combine(Scope, "Configs");
            Config = Path.Combine(Configs, "scope-config.yml");
        }

        /// <summary>
        /// Creates any missing paths if any, and reloads them all.
        /// </summary>
        internal static void Init()
        {
            Reload();

            if (!Directory.Exists(Configs))
                Directory.CreateDirectory(Configs);

            if (!Directory.Exists(Mods))
                Directory.CreateDirectory(Mods);

            if (!Directory.Exists(Dependencies))
                Directory.CreateDirectory(Dependencies);
        }
    }
}