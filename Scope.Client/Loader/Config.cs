// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System.ComponentModel;
    using Scope.Client.API.Interfaces;

    /// <summary>
    /// The configs of the loader.
    /// </summary>
    public sealed class Config : IConfig
    {
        /// <inheritdoc/>
        [Description("Indicates whether or not the mod is enabled")]
        public bool IsEnabled { get; set; } = true;
    }
}
