// -----------------------------------------------------------------------
// <copyright file="ConnectPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.Authentication
{
#pragma warning disable SA1600 // Elements should be documented

    using HarmonyLib;
    using Mirror;
    using Scope.Client.API.Features;

    /// <summary>
    /// Patches <see cref="NetworkClient.Connect"/>.
    /// </summary>
    [HarmonyPatch(typeof(NetworkClient), nameof(NetworkClient.Connect), typeof(string))]
    internal class ConnectPatch
    {
        private static string _newTargetAddress = string.Empty;

        public static void Prefix(string address)
        {
            Log.Info($"Connecting with address {address}");
            _newTargetAddress = address;
        }
    }
}
