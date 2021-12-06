// -----------------------------------------------------------------------
// <copyright file="IL2CppObjectAdapter.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using Il2CppSystem.Collections.Generic;

    /// <summary>
    /// A tool to integrate <see cref="Il2CppSystem.Object"/> with Scope Environment.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    public class IL2CppObjectAdapter<T> : Il2CppSystem.Object
    {
        private HashSet<Il2CppSystem.Object> _objects = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="IL2CppObjectAdapter{T}"/> class.
        /// </summary>
        public IL2CppObjectAdapter() => _objects.Add(this);

        /// <summary>
        /// Gets the object.
        /// </summary>
        public T Object { get; private set; }

        /// <summary>
        /// Releases and resets all unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Object = default;
            _objects = null;
        }
    }
}
