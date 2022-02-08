// -----------------------------------------------------------------------
// <copyright file="Mod.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Scope.Client.API.Enums;
    using Scope.Client.API.Extensions;
    using Scope.Client.API.Interfaces;
    using Scope.Client.Loader;
    using UnityEngine;

    /// <summary>
    /// Expose the structure of the mod.
    /// </summary>
    /// <typeparam name="TConfig">The config type.</typeparam>
    public abstract class Mod<TConfig> : IMod<TConfig>
        where TConfig : IConfig, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mod{TConfig}"/> class.
        /// </summary>
        public Mod()
        {
            Assembly = Assembly.GetCallingAssembly();
            Name = Assembly.GetName().Name;
            Prefix = Name.ToSnakeCase();
            Author = Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            Version = Assembly.GetName().Version;
        }

        /// <inheritdoc/>
        public TConfig Config => new();

        /// <inheritdoc/>
        public virtual Assembly Assembly { get; }

        /// <inheritdoc/>
        public virtual string Name { get; }

        /// <inheritdoc/>
        public virtual string Prefix { get; }

        /// <inheritdoc/>
        public virtual string Author { get; }

        /// <inheritdoc/>
        public virtual Version Version { get; }

        /// <inheritdoc/>
        public ExecutionPriority Priority { get; }

        /// <inheritdoc/>
        public Version RequiredScopeVersion { get; } = typeof(IMod<>).Assembly.GetName().Version;

        /// <inheritdoc/>
        public bool RequireServerValidation { get; }

        /// <inheritdoc/>
        public bool RequireExternalAssets { get; }

        /// <inheritdoc/>
        public List<Il2CppAssetBundle> Assets { get; }

        /// <summary>
        /// Gets the assets path.
        /// </summary>
        public string AssetsPath => Path.Combine(Paths.Config, Path.Combine(Name, "Assets"));

        /// <inheritdoc/>
        public virtual void OnEnabled()
        {
            AssemblyInformationalVersionAttribute attribute = Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            Log.Info($"{Name} v{(attribute is null ? $"{Version.Major}.{Version.Minor}.{Version.Build}" : attribute.InformationalVersion)} by {Author} has been enabled!");

            if (RequireExternalAssets)
            {
                foreach (string file in Directory.GetFiles(AssetsPath))
                {
                    Il2CppAssetBundle asset = Il2CppAssetBundleManager.LoadFromFile(file);
                    Assets.Add(asset);
                    BepInExLoader.AssetBundles.Add(file, asset);
                }
            }
        }

        /// <inheritdoc/>
        public virtual void OnUpdate()
        {
        }

        /// <inheritdoc/>
        public virtual void OnGUI()
        {
        }

        /// <inheritdoc/>
        public virtual void OnDisabled()
        {
            if (RequireExternalAssets)
            {
                foreach (Il2CppAssetBundle assetBundle in Assets)
                    assetBundle.Unload(true);
            }

            Log.Info($"{Name} has been disabled!");
        }

        /// <inheritdoc/>
        public virtual void OnServerEnabled()
        {
            AssemblyInformationalVersionAttribute attribute = Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            Log.Info($"{Name} v{(attribute is null ? $"{Version.Major}.{Version.Minor}.{Version.Build}" : attribute.InformationalVersion)} by {Author} has been enabled!");
        }

        /// <inheritdoc/>
        public virtual void OnServerDisabled()
        {
            Log.Info($"{Name} has been disabled!");
        }

        /// <inheritdoc/>
        public int CompareTo(IMod<IConfig> other) => -Priority.CompareTo(other.Priority);
    }
}