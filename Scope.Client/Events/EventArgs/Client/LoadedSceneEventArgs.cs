// -----------------------------------------------------------------------
// <copyright file="LoadedSceneEventArgs.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.EventArgs
{
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Contains all informations after a new scene is loaded.
    /// </summary>
    public class LoadedSceneEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedSceneEventArgs"/> class.
        /// </summary>
        /// <param name="newScene">The new <see cref="Scene"/>.</param>
        /// <param name="mode">The <see cref="LoadSceneMode"/>.</param>
        public LoadedSceneEventArgs(Scene newScene, LoadSceneMode mode)
        {
            NewScene = newScene;
            Mode = mode;
        }

        /// <summary>
        /// Gets the new <see cref="Scene"/>.
        /// </summary>
        public Scene NewScene { get; }

        /// <summary>
        /// Gets the <see cref="LoadSceneMode"/>.
        /// </summary>
        public LoadSceneMode Mode { get; }
    }
}
