// -----------------------------------------------------------------------
// <copyright file="Mod.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System;
    using System.Reflection;
    using Scope.Client.API.Enums;
    using Scope.Client.API.Extensions;
    using Scope.Client.API.Interfaces;

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
        public Version RequiredScopeVersion { get; }

        /// <inheritdoc/>
        public virtual void OnEnabled()
        {
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
        }

        /// <inheritdoc/>
        public int CompareTo(IMod<IConfig> other)
        {
            throw new NotImplementedException();
        }
    }
}