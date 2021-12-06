// -----------------------------------------------------------------------
// <copyright file="ExecutionPriorityComparer.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader.Features
{
    using System.Collections.Generic;
    using Scope.Client.API.Interfaces;

    /// <summary>
    /// Comparator implementation for execution priorities.
    /// </summary>
    public sealed class ExecutionPriorityComparer : IComparer<IMod<IConfig>>
    {
        /// <summary>
        /// Gets the static instance.
        /// </summary>
        public static ExecutionPriorityComparer Instance => new();

        /// <inheritdoc/>
        public int Compare(IMod<IConfig> x, IMod<IConfig> y)
        {
            int result = y.Priority.CompareTo(x.Priority);

            if (result == 0)
                result = x.GetHashCode().CompareTo(y.GetHashCode());

            return result == 0 ? 1 : result;
        }
    }
}