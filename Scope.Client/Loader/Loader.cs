// -----------------------------------------------------------------------
// <copyright file="Loader.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using System;
    using System.Reflection;
    using Il2CppSystem.Collections.Generic;
    using Il2CppSystem.IO;
    using Scope.Client.API.Features;
    using Scope.Client.API.Interfaces;

    /// <summary>
    /// Used to handle mods.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// Gets the mods list.
        /// </summary>
        public static List<IMod<IConfig>> Mods => new();

        /// <summary>
        /// Loads all mods.
        /// </summary>
        public static void LoadAll()
        {
            foreach (string file in Directory.GetFiles(Paths.ModsDependenciesDirectory, "*.dll"))
                Assembly.UnsafeLoadFrom(file);

            foreach (string file in Directory.GetFiles(Paths.ModsDirectory, "*.dll"))
            {
                Assembly assembly = Assembly.UnsafeLoadFrom(file);

                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || type.IsInterface || type.BaseType?.GetGenericTypeDefinition() != typeof(Mod<>))
                        continue;

                    IMod<IConfig> mod = null;

                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor != null)
                        mod = constructor.Invoke(null) as IMod<IConfig>;
                    else
                    {
                        object value = Array.Find(type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public), property => property.PropertyType == type)?.GetValue(null);
                        if (value != null)
                            mod = value as IMod<IConfig>;
                    }

                    mod?.OnEnabled();

                    Mods.Add(mod);
                }
            }
        }

        /// <summary>
        /// Enables all mods.
        /// </summary>
        public static void EnableMods()
        {
            List<IMod<IConfig>> toLoad = Mods;

            foreach (IMod<IConfig> mod in toLoad)
            {
                try
                {
                    if (mod.Name.StartsWith("Scope") && mod.Config.IsEnabled)
                    {
                        mod.OnEnabled();
                        toLoad.Remove(mod);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Mod \"{mod.Name}\" threw an exeption while enabling: {e}");
                }
            }

            foreach (IMod<IConfig> mod in toLoad)
            {
                try
                {
                    if (mod.Config.IsEnabled)
                        mod.OnEnabled();
                }
                catch (Exception exception)
                {
                    Log.Error($"Mod \"{mod.Name}\" threw an exception while enabling: {exception}");
                }
            }
        }
    }
}
