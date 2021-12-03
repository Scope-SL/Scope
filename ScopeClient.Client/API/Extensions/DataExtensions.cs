using Scope.Client.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scope.Client.API.Features.TransmissionNetworkObject;

namespace Scope.Client.API.Extensions
{
    public static class DataExtensions
    {
        public static bool IsData(this byte[] source)
        {
            if (source.Length < 2) return false;
            return GetDataEminenceFromEncodedObject(source);
        }
    }
}
