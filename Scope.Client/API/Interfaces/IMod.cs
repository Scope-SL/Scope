// -----------------------------------------------------------------------
// <copyright file="IMod.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Interfaces
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines the contract for basic mod features.
    /// </summary>
    /// <typeparam name="TConfig">The config type.</typeparam>
    public interface IMod<out TConfig>
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
        /// Called after enabling the mod.
        /// </summary>
        public void OnEnabled();

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void OnUpdate();

        /// <summary>
        /// Called for rendering and handling GUI events.
        /// </summary>
        public void OnGUI();

        /// <summary>
        /// Called after disabling the mod.
        /// </summary>
        public void OnDisabled();
    }
}