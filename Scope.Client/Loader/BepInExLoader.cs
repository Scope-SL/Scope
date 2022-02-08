// -----------------------------------------------------------------------
// <copyright file="BepInExLoader.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using BepInEx;
    using BepInEx.IL2CPP;
    using HarmonyLib;
    using MelonLoader;
    using Scope.Client.API.Features;
    using UnhollowerRuntimeLib;
    using UnityEngine;

    /// <summary>
    /// Used to load the BepInEx.
    /// </summary>
    [BepInPlugin("scope.client.github", "ScopeClient", "0.0.1")]
    [BepInProcess("SCPSL.exe")]
    public class BepInExLoader : BasePlugin
    {
        /// <summary>
        /// Gets the <see cref="Dictionary{TKey, TValue}"/> that contains all bundles.
        /// </summary>
        public static Dictionary<string, Il2CppAssetBundle> AssetBundles => new();

        /// <summary>
        /// Gets the <see cref="Harmony"/> instance.
        /// </summary>
        public static Harmony Harmony { get; private set; }

        /// <summary>
        /// Gets the <see cref="BepInExLoader"/> instance.
        /// </summary>
        public static BepInExLoader Instance { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public static Version Version => new(0, 0, 1);

        /// <summary>
        /// Gets the version to display.
        /// </summary>
        public static string DisplayVersion => $"{Version}a";

        /// <summary>
        /// Gets the <see cref="Client"></see>.
        /// </summary>
        public Client Client { get; private set; }

        /// <inheritdoc/>
        public override void Load()
        {
            UnhollowerSupport.Initialize();

            Instance = this;
            Client = new Client();

            CustomNetworkManager.Modded = true;
            ClassInjector.RegisterTypeInIl2Cpp<ClientComponent>();

            Harmony = new Harmony("scope.client.github");
            Harmony.PatchAll();

            Loader.LoadAll();
            Client.OnApplicationStart();
        }
    }
}