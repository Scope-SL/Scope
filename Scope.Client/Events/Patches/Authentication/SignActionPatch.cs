// -----------------------------------------------------------------------
// <copyright file="SignActionPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.Authentication
{
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter

    using HarmonyLib;
    using Scope.Client.API.Features;

    /// <summary>
    /// Patches <see cref="CentralAuthManager.Sign"/>.
    /// </summary>
    [HarmonyPatch(typeof(CentralAuthManager), nameof(CentralAuthManager.Sign))]
    internal class SignActionPatch
    {
        public static bool Prefix(ref string __result, string ticket)
        {
            Log.Info($"CentralAuthManager::Sign action: {ticket}");
            __result = string.Empty;
            return false;
        }
    }
}
