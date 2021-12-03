namespace Scope.Client.API.Interfaces
{
    public interface IScopeMod<out TConfig> where TConfig : IConfig
    {
        void OnEnabled();

        void OnUpdate();

        void OnGUI();

        void OnDisabled();
    }
}