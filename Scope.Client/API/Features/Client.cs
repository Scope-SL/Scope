// -----------------------------------------------------------------------
// <copyright file="Client.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System;
    using Mirror.LiteNetLib4Mirror;
    using RemoteAdmin;
    using Scope.Client.API.Features.Packets;
    using Scope.Client.Events.EventArgs;
    using Steamworks;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Represents the client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        internal Client()
        {
            MainMenu = UnityEngine.Object.FindObjectOfType<NewMainMenu>();
        }

        /// <summary>
        /// Gets the <see cref="Client"/> instance.
        /// </summary>
        public static Client Instance => Loader.BepInExLoader.Instance.Client;

        /// <summary>
        /// Gets the client <see cref="API.Features.Hardware"/>.
        /// </summary>
        public Hardware Hardware => new();

        /// <summary>
        /// Gets the current scene.
        /// </summary>
        public Scene CurrentScene => SceneManager.GetActiveScene();

        /// <summary>
        /// Gets a value indicating whether or not the client is connected to a server.
        /// </summary>
        public bool IsConnected => CurrentScene.name == "Facility";

        /// <summary>
        /// Gets the SteamClient username.
        /// </summary>
        public string SteamUsername { get; internal set; }

        /// <summary>
        /// Gets the server IP address.
        /// </summary>
        public string IPAddress => LiteNetLib4MirrorTransport.Singleton.clientAddress;

        /// <summary>
        /// Gets the server port.
        /// </summary>
        public ushort Port => LiteNetLib4MirrorTransport.Singleton.port;

        /// <summary>
        /// Gets or sets a value indicating whether or not the application can run in background.
        /// </summary>
        public bool RunInBackground
        {
            get => Application.runInBackground;
            set => Application.runInBackground = value;
        }

        /// <summary>
        /// Gets a value indicating whether or not the client is playing.
        /// </summary>
        public bool IsPlaying => Application.isPlaying;

        /// <summary>
        /// Gets the currently loaded level index.
        /// </summary>
        public int LoadedLevel => Application.loadedLevel;

        /// <summary>
        /// Gets the currently loaded level name.
        /// </summary>
        public string LoadedLevelName => Application.loadedLevelName;

        /// <summary>
        /// Gets the stream progress for the current level.
        /// </summary>
        public float StreamProgressForCurrentLevel => Application.GetStreamProgressForLevel(LoadedLevelName);

        private NewMainMenu MainMenu { get; set; }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame() => Application.Quit();

        /// <summary>
        /// Quits the game with a specified exit code.
        /// </summary>
        /// <param name="exitCode">The exit code.</param>
        public void QuitGame(int exitCode) => Application.Quit(exitCode);

        /// <summary>
        /// Gets a value indicating whether or not the specified streamed level can be loaded.
        /// </summary>
        /// <param name="levelName">The name of the level to check.</param>
        /// <returns><see langword="true"/> if the level streamed level can be loaded successfully; otherwise, <see langword="false"/>.</returns>
        public bool CanStreamedLevelBeLoaded(string levelName) => Application.CanStreamedLevelBeLoaded(levelName);

        /// <summary>
        /// Gets a value indicating whether or not the specified streamed level can be loaded.
        /// </summary>
        /// <param name="levelIndex">The index of the level to check.</param>
        /// <returns><see langword="true"/> if the level streamed level can be loaded successfully; otherwise, <see langword="false"/>.</returns>
        public bool CanStreamedLevelBeLoaded(int levelIndex) => Application.CanStreamedLevelBeLoaded(levelIndex);

        /// <summary>
        /// Captures and saves a screenshot.
        /// </summary>
        /// <param name="fileName">The name of the capture.</param>
        public void CaptureScreenshot(string fileName) => Application.CaptureScreenshot(fileName);

        /// <summary>
        /// Captures and saves a screenshot.
        /// </summary>
        /// <param name="fileName">The name of the capture.</param>
        /// <param name="superSize">The super size.</param>
        public void CaptureScreenshot(string fileName, int superSize) => Application.CaptureScreenshot(fileName, superSize);

        /// <summary>
        /// Forces the application crash.
        /// </summary>
        /// <param name="mode">The crash mode.</param>
        public void ForceCrash(int mode) => Application.ForceCrash(mode);

        /// <summary>
        /// Gets the stream progress for the level with the name same as the specified one.
        /// </summary>
        /// <param name="levelName">The specified name.</param>
        /// <returns>The stream progress for the level.</returns>
        public float GetStreamProgressForLevel(string levelName) => Application.GetStreamProgressForLevel(levelName);

        /// <summary>
        /// Gets the stream progress for the level with the index same as the specified one.
        /// </summary>
        /// <param name="levelIndex">The specified index.</param>
        /// <returns>The stream progress for the level.</returns>
        public float GetStreamProgressForLevel(int levelIndex) => Application.GetStreamProgressForLevel(levelIndex);

        /// <summary>
        /// Opens the specified url.
        /// </summary>
        /// <param name="url">The url to open.</param>
        public void OpenURL(string url) => Application.OpenURL(url);

        /// <summary>
        /// Unloads the current level.
        /// </summary>
        /// <returns><see langword="true"/> if the level has been unloaded successfully; otherwise, <see langword="false"/>.</returns>
        public bool UnloadCurrentLevel() => Application.UnloadLevel(LoadedLevel);

        /// <summary>
        /// Unloads the level given the specified index.
        /// </summary>
        /// <param name="levelIndex">The index of the level to unload.</param>
        /// <returns><see langword="true"/> if the level has been unloaded successfully; otherwise, <see langword="false"/>.</returns>
        public bool UnloadLevel(int levelIndex) => Application.UnloadLevel(levelIndex);

        /// <summary>
        /// Unloads the level at the specified path.
        /// </summary>
        /// <param name="scenePath">The path of the level to unload.</param>
        /// <returns><see langword="true"/> if the level has been unloaded successfully; otherwise, <see langword="false"/>.</returns>
        public bool UnloadLevel(string scenePath) => Application.UnloadLevel(scenePath);

        /// <summary>
        /// Called when client is being loaded.
        /// </summary>
        public void OnApplicationStart()
        {
            RegisterEvents();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void OnUpdate()
        {
        }

        /// <summary>
        /// Called for rendering and handling GUI events.
        /// </summary>
        public void OnGUI()
        {
        }

        /// <summary>
        /// Called when client is being unloaded.
        /// </summary>
        public void OnApplicationQuit()
        {
            UnregisterEvents();
        }

        private void RegisterEvents()
        {
            Events.Handlers.Data.ReceivingData += OnReceivingData;
            SceneManager.add_sceneLoaded(new Action<Scene, LoadSceneMode>(OnSceneLoaded));
        }

        private void UnregisterEvents()
        {
            Events.Handlers.Data.ReceivingData -= OnReceivingData;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Log.Info($"Scene changed to {scene.name}");

            switch (scene.name)
            {
                case "Facility":
                    {
                        break;
                    }

                case "Loader":
                    {
                        if (!SteamClient.IsLoggedOn)
                            break;

                        SteamUsername = SteamClient.Name;
                        break;
                    }

                case "NewMainMenu":
                    {
                        LoadedMainMenuEventArgs mainMenuEv = new();
                        Events.Handlers.Client.OnLoadedMainMenu(mainMenuEv);
                        break;
                    }

                default:
                    break;
            }

            LoadedSceneEventArgs newSceneEv = new(scene, mode);
            Events.Handlers.Client.OnLoadedScene(newSceneEv);
        }

        private void OnReceivingData(ReceivingDataEventArgs dataEv)
        {
            if (dataEv.Data.Type == RoundStart.Type)
            {
                RoundStartedEventArgs ev = new();
                Events.Handlers.Client.OnRoundStarted(ev);
                return;
            }

            if (dataEv.Data.Type == ClientRedirect.Type)
            {
                RedirectedEventArgs ev = new();
                Events.Handlers.Client.OnRedirected(ev);
                return;
            }

            if (dataEv.Data.Type == ConnectionSuccessful.Type)
            {
                ConnectionSuccessful.Decode(dataEv.Data, out string[] clientMods);

                Log.Info("Welcome Packet: " + dataEv.Data.Source);

                byte[] salt = new byte[32];

                // QueryProcessor.Localplayer.Key = new UnhollowerBaseLib.Il2CppStructArray<byte>(default(IntPtr));
                QueryProcessor.Localplayer.CryptoManager.ExchangeRequested = true;

                // QueryProcessor.Localplayer.CryptoManager.EncryptionKey = new UnhollowerBaseLib.Il2CppStructArray<byte>(default(IntPtr));
                QueryProcessor.Localplayer.Salt = salt;
                QueryProcessor.Localplayer.ClientSalt = salt;

                TransmissionNetworkObject.SendData(TransmissionNetworkObject.GetSource(1, "Client connected successfully"));
                return;
            }
        }
    }
}