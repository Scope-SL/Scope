using Scope.Client.Events.EventArgs;
using Scope.Client.Events.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Scope.Client.Events.Events;

namespace Scope.Client.Events.Handlers
{
    public static class Data
    {
        public static event CustomEventHandler<ReceivingDataEventArgs> ReceivingData;

        public static event CustomEventHandler<SendingDataEventArgs> SendingData;

        public static void OnReceivingData(ReceivingDataEventArgs ev) => ReceivingData.InvokeSafely(ev);

        public static void OnReceivingData(SendingDataEventArgs ev) => SendingData.InvokeSafely(ev);
    }
}
