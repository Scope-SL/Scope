// -----------------------------------------------------------------------
// <copyright file="Paths.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using System.IO;
    using Il2CppSystem.Reflection;

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
            ModsDirectory = Assembly.GetCallingAssembly().Location + "\\..\\..\\..\\Mods";
            ModsDependenciesDirectory = Path.Combine(ModsDirectory, "dependencies");
        }
    }
}