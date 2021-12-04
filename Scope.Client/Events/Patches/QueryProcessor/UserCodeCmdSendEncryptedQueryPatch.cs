// -----------------------------------------------------------------------
// <copyright file="UserCodeCmdSendEncryptedQueryPatch.cs" company="Scope SL">
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
    /// Patches <see cref="QueryProcessor.UserCode_CmdSendEncryptedQuery"/>.
    /// </summary>
    [HarmonyPatch(typeof(QueryProcessor), nameof(QueryProcessor.UserCode_CmdSendEncryptedQuery))]
    internal static class UserCodeCmdSendEncryptedQueryPatch
    {
        public static void Prefix() => Log.Info("Encrypted Query Call");
    }
}
