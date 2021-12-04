// -----------------------------------------------------------------------
// <copyright file="ConsoleAwakePatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.QueryProcessor
{
#pragma warning disable SA1600 // Elements should be documented

    using HarmonyLib;
    using Scope.Client.API.Features;

    /// <summary>
    /// Patches <see cref="GameCore.Console.Awake"/>.
    /// </summary>
    [HarmonyPatch(typeof(GameCore.Console), nameof(GameCore.Console.Awake))]
    internal class ConsoleAwakePatch
    {
        public static void Prefix() => Log.Info("Loading GameConsole");
    }
}
