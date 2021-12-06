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
    public class Paths
    {
        /// <summary>
        /// Gets or sets the directory where all mods get stored.
        /// </summary>
        public static string ModsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the directory where all required modules get stored.
        /// </summary>
        public static string ModsDependenciesDirectory { get; set; }

        /// <summary>
        /// Loads all paths.
        /// </summary>
        internal static void LoadPaths()
        {
            ModsDirectory = Path.Combine("ScopeClient", "Mods");
            if (!Directory.Exists(ModsDirectory))
                Directory.CreateDirectory(ModsDirectory);
            ModsDependenciesDirectory = Path.Combine(ModsDirectory, "dependencies");
            if (!Directory.Exists(ModsDependenciesDirectory))
                Directory.CreateDirectory(ModsDependenciesDirectory);
        }
    }
}