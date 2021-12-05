// -----------------------------------------------------------------------
// <copyright file="CentralAuthManagerSignPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.Authentication
{
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable IDE0060 // Remove unused parameter

    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="CentralAuthManager.Sign"/>.
    /// </summary>
    [HarmonyPatch(typeof(CentralAuthManager), nameof(CentralAuthManager.Sign))]
    internal class CentralAuthManagerSignPatch
    {
        public static bool Prefix(ref string __result, string ticket)
        {
            __result = "Client Authenticated";
            CentralAuthManager.Authenticated = true;
            return false;
        }
    }
}
