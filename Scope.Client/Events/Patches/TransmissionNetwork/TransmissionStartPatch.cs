// -----------------------------------------------------------------------
// <copyright file="TransmissionStartPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.TransmissionNetwork
{
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1401 // Fields should be private

    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="GameConsoleTransmission.Start"/>.
    /// </summary>
    [HarmonyPatch(typeof(GameConsoleTransmission), nameof(GameConsoleTransmission.Start))]
    internal class TransmissionStartPatch
    {
        internal static GameConsoleTransmission Transmission = null;

        internal static void Prefix(GameConsoleTransmission __instance) => Transmission = __instance;
    }
}
