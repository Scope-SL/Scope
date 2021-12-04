// -----------------------------------------------------------------------
// <copyright file="TransmissionNetwork.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches
{
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1401 // Fields should be private

    using HarmonyLib;
    using Scope.Client.API.Extensions;
    using Scope.Client.API.Features;
    using Scope.Client.Events.EventArgs;
    using UnhollowerBaseLib;

    /// <summary>
    /// Patches <see cref="GameConsoleTransmission.Start"/> and
    /// <see cref="GameConsoleTransmission.UserCode_TargetPrintOnConsole(Mirror.NetworkConnection, Il2CppStructArray{byte}, bool)"/>.
    /// </summary>
    internal class TransmissionNetwork
    {
        internal static GameConsoleTransmission Transmission = null;

        [HarmonyPatch(typeof(GameConsoleTransmission), nameof(GameConsoleTransmission.Start))]
        [HarmonyPrefix]
        internal static void TransmissionStart(GameConsoleTransmission __instance) => Transmission = __instance;

        [HarmonyPatch(typeof(GameConsoleTransmission), nameof(GameConsoleTransmission.UserCode_TargetPrintOnConsole))]
        [HarmonyPrefix]
        internal static bool TargetPrintOnConsole(Il2CppStructArray<byte> data, bool encrypted)
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
