using System;
using Scope.Client.API;
using Scope.Client.Loader;
using UnityEngine;

namespace Scope.Client
{
    public class ClientComponent : MonoBehaviour
    {
        private Client _client;

        public ClientComponent(IntPtr ptr) : base(ptr) { }

        public static void CreateInstance(Client client, BepInExLoad loader)
        {
            var obj = new GameObject().AddComponent<ClientComponent>();
            DontDestroyOnLoad(obj.gameObject);
            obj.hideFlags |= HideFlags.HideAndDontSave;
            obj._client = client;
        }

        private void Update()
        {
            _client?.OnUpdate();
            foreach (var mod in ModLoader.Mods)
            {
                mod.OnUpdate();
            }
        }

        private void OnGUI()
        {
            _client.OnGUI();
            foreach (var mod in ModLoader.Mods)
            {
                mod.OnGUI();
            }
            
        }

        private void OnApplicationQuit()
        {
            _client.OnApplicationQuit();
            foreach (var mod in ModLoader.Mods)
            {
                mod.OnDisabled();
            }
        }
    }
}