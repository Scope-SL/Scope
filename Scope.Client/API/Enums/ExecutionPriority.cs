// -----------------------------------------------------------------------
// <copyright file="ExecutionPriority.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Enums
{
    /// <summary>
    /// All execution priority values.
    /// </summary>
    public enum ExecutionPriority
    {
        /// <summary>
        /// Execute the mod last, after other ones.
        /// </summary>
        Lowest = 0,

        /// <summary>
        /// Default mod execution priority.
        /// </summary>
        Lower = 1,

        /// <summary>
        /// Low mod execution priority.
        /// </summary>
        Low = 2,

        /// <summary>
        /// Medium mod execution priority.
        /// </summary>
        Medium = 3,

        /// <summary>
        /// Higher mod execution priority.
        /// </summary>
        High = 4,

        /// <summary>
        /// Higher mod execution priority.
        /// </summary>
        Higher = 5,

        /// <summary>
        /// Execute the mod first, before other ones.
        /// </summary>
        Highest = 6,
    }
}
