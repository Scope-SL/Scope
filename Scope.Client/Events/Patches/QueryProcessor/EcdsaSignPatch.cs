// -----------------------------------------------------------------------
// <copyright file="EcdsaSignPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.QueryProcessor
{
#pragma warning disable SA1600 // Elements should be documented

    using HarmonyLib;
    using RemoteAdmin;
    using Scope.Client.API.Features;

    /// <summary>
    /// Patches <see cref="QueryProcessor.EcdsaSign"/>.
    /// </summary>
    [HarmonyPatch(typeof(QueryProcessor), nameof(QueryProcessor.EcdsaSign))]
    internal class EcdsaSignPatch
    {
        public static void Prefix() => Log.Info("Sending Encrypted Query");
    }
}
