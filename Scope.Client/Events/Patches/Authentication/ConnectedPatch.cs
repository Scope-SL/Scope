// -----------------------------------------------------------------------
// <copyright file="ConnectedPatch.cs" company="Scope SL">
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
    /// Patches <see cref="NetworkClient.OnConnected"/>.
    /// </summary>
    [HarmonyPatch(typeof(NetworkClient), nameof(NetworkClient.OnConnected))]
    internal class ConnectedPatch
    {
        public static void Prefix() => Log.Info($"Client Connected");
    }
}
