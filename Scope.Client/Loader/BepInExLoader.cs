// -----------------------------------------------------------------------
// <copyright file="BepInExLoader.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using System.Collections.Generic;
    using BepInEx;
    using BepInEx.IL2CPP;
    using HarmonyLib;
    using Scope.Client.API.Features;
    using UnhollowerRuntimeLib;
    using UnityEngine;

    /// <summary>
    /// Used to load the BepInEx.
    /// </summary>
    [BepInPlugin("scopeclient.client.github", "ScopeClient", "0.0.1")]
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
        /// Gets the <see cref="Client"></see>.
        /// </summary>
        public Client Client { get; private set; }

        /// <inheritdoc/>
        public override void Load()
        {
            Instance = this;

            Client = new Client();

            ClassInjector.RegisterTypeInIl2Cpp<ClientComponent>();

            Harmony = new Harmony("scope.client.github");
            Harmony.PatchAll();

            Loader.LoadAll();

            Client.OnApplicationStart();
        }
    }
}