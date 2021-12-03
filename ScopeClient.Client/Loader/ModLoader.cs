using System;
using System.Reflection;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Scope.Client.API.Features;
using Scope.Client.API.Interfaces;

namespace Scope.Client.Loader
{
    public static class ModLoader
    {
        public static List<IScopeMod<IConfig>> Mods = new List<IScopeMod<IConfig>>();

        public static void LoadAll()
        {
            foreach (var file in Directory.GetFiles(Paths.ModsDependenciesDirectory, "*.dll"))
            {
                try
                {
                    Assembly.UnsafeLoadFrom(file);
                }
                catch
                {
                    // ignored 
                }
            }
            foreach (var file in Directory.GetFiles(Paths.ModsDirectory, "*.dll"))
            {
                var assembly = Assembly.UnsafeLoadFrom(file);

                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if(type.IsAbstract || type.IsInterface || type.BaseType?.GetGenericTypeDefinition() != typeof(Mod<>))
                            continue;

                        IScopeMod<IConfig> Mod = null;
                        
                        var constructor = type.GetConstructor(Type.EmptyTypes);
                        if (constructor != null)
                        {
                            Mod = constructor.Invoke(null) as IScopeMod<IConfig>;
                        }
                        else
                        {
                            var value = Array.Find(type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public), property => property.PropertyType == type)?.GetValue(null);
                            if (value != null)
                                Mod = value as IScopeMod<IConfig>;
                        }
                        
                        Mod?.OnEnabled();

                        Mods.Add(Mod);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}