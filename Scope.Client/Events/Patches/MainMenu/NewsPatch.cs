// -----------------------------------------------------------------------
// <copyright file="NewsPatch.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Patches.QueryProcessor
{
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1118 // Parameter should not span multiple lines
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter

    using HarmonyLib;
    using Scope.Client.Loader;

    /// <summary>
    /// Patches <see cref="NewsLoader.Start"/>.
    /// </summary>
    [HarmonyPatch(typeof(NewsLoader), nameof(NewsLoader.Start))]
    internal static class NewsPatch
    {
        public static bool Prefix(NewsLoader __instance)
        {
            __instance._announcements = new Il2CppSystem.Collections.Generic.List<NewsLoader.Announcement>();
            __instance._announcements.Add(new NewsLoader.Announcement(
                $"<color=#dad467>Scope Client {BepInExLoader.DisplayVersion}</color>",
                "<b><size=20>Welcome to Scope-SL, a SCP:SL modded version.</size></b>\n" +
                "\n<color=#ec0c02>Content is subject to constant changes.</color>",
                "Feb 7, 2022",
                "https://scopesl.com", null));
            __instance.ShowAnnouncement(0);

            return false;
        }
    }
}
