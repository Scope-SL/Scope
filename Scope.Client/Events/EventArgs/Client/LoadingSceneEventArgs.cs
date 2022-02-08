// -----------------------------------------------------------------------
// <copyright file="LoadingSceneEventArgs.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.EventArgs.Client
{
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Contains all informations before loading a new scene.
    /// </summary>
    public class LoadingSceneEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingSceneEventArgs"/> class.
        /// </summary>
        /// <param name="oldScene">The current <see cref="Scene"/>.</param>
        /// <param name="newScene">The <see cref="Scene"/> which is being loaded.</param>
        /// <param name="isAllowed">Indicates wheter the event can be executed or not.</param>
        public LoadingSceneEventArgs(Scene oldScene, Scene newScene, bool isAllowed = true)
        {
            OldScene = oldScene;
            NewScene = newScene;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets the current <see cref="Scene"/>.
        /// </summary>
        public Scene OldScene { get; }

        /// <summary>
        /// Gets or sets the <see cref="Scene"/> which is being changed.
        /// </summary>
        public Scene NewScene { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the <see cref="Scene"/> can be loaded.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}
