// -----------------------------------------------------------------------
// <copyright file="Client.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Handlers
{
    using Scope.Client.Events.EventArgs;
    using Scope.Client.Events.Extensions;
    using static Scope.Client.Events.Events;

    /// <summary>
    /// Data related events.
    /// </summary>
    public static class Client
    {
        /// <summary>
        /// Invoked after a new round is started.
        /// </summary>
        public static event CustomEventHandler<RoundStartedEventArgs> RoundStarted;

        /// <summary>
        /// Invoked after the client is redirected.
        /// </summary>
        public static event CustomEventHandler<RedirectedEventArgs> Redirected;

        /// <summary>
        /// Invoked after the client is connected successfully.
        /// </summary>
        public static event CustomEventHandler<ConnectedSuccessfullyEventArgs> ConnectionSuccessfully;

        /// <summary>
        /// Invoked after the main menu is loaded.
        /// </summary>
        public static event CustomEventHandler<LoadedMainMenuEventArgs> LoadedMainMenu;

        /// <summary>
        /// Invoked after a new scene is loaded.
        /// </summary>
        public static event CustomEventHandler<LoadedSceneEventArgs> LoadedScene;

        /// <summary>
        /// Called after a new round is started.
        /// </summary>
        /// <param name="ev">The <see cref="RoundStartedEventArgs"/> event.</param>
        public static void OnRoundStarted(RoundStartedEventArgs ev) => RoundStarted.InvokeSafely(ev);

        /// <summary>
        /// Called after the client is redirected.
        /// </summary>
        /// <param name="ev">The <see cref="RedirectedEventArgs"/> event.</param>
        public static void OnRedirected(RedirectedEventArgs ev) => Redirected.InvokeSafely(ev);

        /// <summary>
        /// Called after the client is connected successfully.
        /// </summary>
        /// <param name="ev">The <see cref="ConnectedSuccessfullyEventArgs"/> event.</param>
        public static void OnConnectedSuccessfully(ConnectedSuccessfullyEventArgs ev) => ConnectionSuccessfully.InvokeSafely(ev);

        /// <summary>
        /// Called after the main menu is loaded.
        /// </summary>
        /// <param name="ev">The <see cref="LoadedMainMenuEventArgs"/> event.</param>
        public static void OnLoadedMainMenu(LoadedMainMenuEventArgs ev) => LoadedMainMenu.InvokeSafely(ev);

        /// <summary>
        /// Called after a new scene is loaded.
        /// </summary>
        /// <param name="ev">The <see cref="LoadedSceneEventArgs"/> event.</param>
        public static void OnLoadedScene(LoadedSceneEventArgs ev) => LoadedScene.InvokeSafely(ev);
    }
}
