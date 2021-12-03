using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scope.Client.API.Enums
{
    public enum TransmissionNetworkObjectType
    {
        Welcome = 0x00,
        Message = 0x01,
        ObjectSpawn = 0x0A,
        ObjectDestroy = 0x0B,
        ObjectLocation = 0x0C,
        ClientRedirect = 0x14,
        ClientPlaySound = 0x15,
        RoundStart = 0x1E,
    }
}
