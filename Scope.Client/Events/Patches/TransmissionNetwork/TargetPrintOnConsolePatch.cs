// -----------------------------------------------------------------------
// <copyright file="TargetPrintOnConsolePatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.TransmissionNetwork
{
#pragma warning disable SA1600 // Elements should be documented

    using HarmonyLib;
    using Scope.Client.API.Extensions;
    using Scope.Client.API.Features;
    using Scope.Client.Events.EventArgs;
    using Scope.Client.Events.EventArgs.Data;
    using UnhollowerBaseLib;

    /// <summary>
    /// Patches <see cref="GameConsoleTransmission.UserCode_TargetPrintOnConsole(Mirror.NetworkConnection, Il2CppStructArray{byte}, bool)"/>.
    /// </summary>
    [HarmonyPatch(typeof(GameConsoleTransmission), nameof(GameConsoleTransmission.UserCode_TargetPrintOnConsole))]
    internal class TargetPrintOnConsolePatch
    {
        internal static bool Prefix(Il2CppStructArray<byte> data, bool encrypted)
        {
            if (data == null)
                return false;

            if (encrypted || !data.IsData())
                return true;

            TransmissionNetworkObject decodedTransmissionNetworkObject = TransmissionNetworkObject.DecodeTransmissionNetworkObject(data);

            ReceivingDataEventArgs ev = new(decodedTransmissionNetworkObject);

            Handlers.Data.OnReceivingData(ev);

            if (!ev.IsAllowed)
                return false;

            TransmissionNetworkObject.ReceiveData(ev.Data);
            return false;
        }
    }
}
