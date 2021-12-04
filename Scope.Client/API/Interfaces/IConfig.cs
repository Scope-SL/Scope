// -----------------------------------------------------------------------
// <copyright file="IConfig.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Interfaces
{
    /// <summary>
    /// Defines the contract for basic config features.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the mod is enabled.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}