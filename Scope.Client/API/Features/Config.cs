using Scope.Client.API.Interfaces;

namespace Scope.Client.API.Features
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}
