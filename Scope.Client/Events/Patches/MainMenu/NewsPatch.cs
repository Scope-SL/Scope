// -----------------------------------------------------------------------
// <copyright file="NewsPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.QueryProcessor
{
#pragma warning disable SA1600 // Elements should be documented

    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="NewsLoader.Start"/>.
    /// </summary>
    [HarmonyPatch(typeof(NewsLoader), nameof(NewsLoader.Start))]
    internal class NewsPatch
    {
        public static bool Prefix() => false;
    }
}
