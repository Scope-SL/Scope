using Scope.Client.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scope.Client.Events.EventArgs
{
    public class SendingDataEventArgs : System.EventArgs
    {
        public SendingDataEventArgs(TransmissionNetworkObject data, bool isAllowed = true)
        {
            Data = data;
            IsAllowed = isAllowed;
        }

        public TransmissionNetworkObject Data { get; }

        public bool IsAllowed { get; set; }
    }
}
