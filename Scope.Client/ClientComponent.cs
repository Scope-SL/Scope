// -----------------------------------------------------------------------
// <copyright file="ClientComponent.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client
{
    using System;
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
        /// <param name="gameObject">The <see cref="GameObject"/> value.</param>
        public static void CreateInstance(GameObject gameObject)
        {
            ClientComponent obj = gameObject.AddComponent<ClientComponent>();
            DontDestroyOnLoad(obj.gameObject);
            obj.hideFlags |= HideFlags.HideAndDontSave;
            obj._client = BepInExLoader.Client;
        }

        private void Update()
        {
            _client?.OnUpdate();
        }

        private void OnGUI()
        {
            _client?.OnGUI();
        }

        private void OnApplicationQuit()
        {
            _client?.OnApplicationQuit();
        }
    }
}