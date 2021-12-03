using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Scope.Client.Loader;

namespace Scope.Client
{
    [BepInPlugin("scopeclient.client.github", "ScopeClient", "0.0.1")]
    [BepInProcess("SCPSL.exe")]
    public class BepInExLoad : BasePlugin
    {
        public static Harmony Harmony;
        public static BepInExLoad Instance;
        
        public Client Client => _client ?? (_client = new Client());
        private Client _client;
        public override void Load()
        {
            Instance = this;
            Harmony = new Harmony("scopeclient.client.github");

            Loader.Paths.LoadPaths();
            ModLoader.LoadAll();
            Client.OnApplicationStart();
            ClientComponent.CreateInstance(Client, Instance);
        }
    }
}