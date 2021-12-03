using Scope.Client.API.Interfaces;

namespace Scope.Client.API.Features
{
    public abstract class Mod<TConfig> : IScopeMod<TConfig> where TConfig : IConfig, new()
    {
        public virtual void OnEnabled()
        {
            
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnGUI()
        {
            
        }

        public virtual void OnDisabled()
        {
            
        }
    }
}