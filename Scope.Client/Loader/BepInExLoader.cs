// -----------------------------------------------------------------------
// <copyright file="BepInExLoader.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using BepInEx;
    using BepInEx.IL2CPP;
    using HarmonyLib;
    using UnhollowerRuntimeLib;

    /// <summary>
    /// Used to load the BepInEx.
    /// </summary>
    [BepInPlugin("scopeclient.client.github", "ScopeClient", "0.0.1")]
    [BepInProcess("SCPSL.exe")]
    public class BepInExLoader : BasePlugin
    {
        /// <summary>
        /// Gets the <see cref="Harmony"/> instance.
        /// </summary>
        public static Harmony Harmony { get; private set; }

        /// <summary>
        /// Gets the <see cref="BepInExLoader"/> instance.
        /// </summary>
        public static BepInExLoader Instance { get; private set; }

        /// <summary>
        /// Gets the <see cref="Client"></see>.
        /// </summary>
        public static Client Client;

        /// <inheritdoc/>
        public override void Load()
        {
            Client = new Client();
            Instance = this;
            ClassInjector.RegisterTypeInIl2Cpp<ClientComponent>();
            Harmony = new Harmony("scopeclient.client.github");
            Harmony.PatchAll();
            Paths.LoadPaths();
            Loader.LoadAll();
            Client.OnApplicationStart();
        }
    }
}