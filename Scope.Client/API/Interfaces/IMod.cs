// -----------------------------------------------------------------------
// <copyright file="IMod.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Scope.Client.API.Enums;
    using UnityEngine;

    /// <summary>
    /// Defines the contract for basic mod features.
    /// </summary>
    /// <typeparam name="TConfig">The config type.</typeparam>
    public interface IMod<out TConfig> : IComparable<IMod<IConfig>>
        where TConfig : IConfig
    {
        /// <summary>
        /// Gets the mod config.
        /// </summary>
        public TConfig Config { get; }

        /// <summary>
        /// Gets the mod assembly.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the mod name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the mod prefix.
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// Gets the mod author.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets the mod version.
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Gets the mod execution priority.
        /// </summary>
        public ExecutionPriority Priority { get; }

        /// <summary>
        /// Gets the required version of Scope to execute the mod in stable environment.
        /// </summary>
        public Version RequiredScopeVersion { get; }

        /// <summary>
        /// Gets a value indicating whether the mod should be validated by the server.
        /// </summary>
        public bool RequireServerValidation { get; }

        /// <summary>
        /// Gets a value indicating whether the mod should use external assets.
        /// </summary>
        public bool RequireExternalAssets { get; }

        /// <summary>
        /// Gets a <see cref="List{T}"/> of <see cref="Il2CppAssetBundle"/> which contains all the assets used by the mod.
        /// </summary>
        public List<Il2CppAssetBundle> Assets { get; }

        /// <summary>
        /// Called after enabling the mod.
        /// </summary>
        public void OnEnabled();

        /// <summary>
        /// Called after disabling the mod.
        /// </summary>
        public void OnDisabled();

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void OnUpdate();

        /// <summary>
        /// Called for rendering and handling GUI events.
        /// </summary>
        public void OnGUI();

        /// <summary>
        /// Called after the server enables the mod, if it's needed.
        /// </summary>
        public void OnServerEnabled();

        /// <summary>
        /// Called after the server disables the mod, if it's needed.
        /// </summary>
        public void OnServerDisabled();
    }
}