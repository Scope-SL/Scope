// -----------------------------------------------------------------------
// <copyright file="ClientComponent.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client
{
    using System;
    using Scope.Client.API;
    using Scope.Client.API.Interfaces;
    using Scope.Client.Loader;
    using UnityEngine;

    /// <summary>
    /// A tool to handle the basic implementations related to the client.
    /// </summary>
    public class ClientComponent : MonoBehaviour
    {
        private Client _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientComponent"/> class.
        /// </summary>
        /// <param name="ptr">The provided <see cref="IntPtr"/>.</param>
        public ClientComponent(IntPtr ptr)
            : base(ptr)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ClientComponent">instance</see>.
        /// </summary>
        /// <param name="client">The <see cref="Client"/> value.</param>
        /// <param name="loader">The <see cref="BepInExLoader"/> value.</param>
        public static void CreateInstance(Client client, BepInExLoader loader)
        {
            ClientComponent obj = new GameObject().AddComponent<ClientComponent>();
            DontDestroyOnLoad(obj.gameObject);
            obj.hideFlags |= HideFlags.HideAndDontSave;
            obj._client = client;
        }

        private void Update()
        {
            _client?.OnUpdate();
            foreach (IMod<IConfig> mod in Loader.Loader.Mods)
            {
                mod.OnUpdate();
            }
        }

        private void OnGUI()
        {
            _client.OnGUI();
            foreach (IMod<IConfig> mod in Loader.Loader.Mods)
            {
                mod.OnGUI();
            }
        }

        private void OnApplicationQuit()
        {
            _client.OnApplicationQuit();
            foreach (IMod<IConfig> mod in Loader.Loader.Mods)
            {
                mod.OnDisabled();
            }
        }
    }
}