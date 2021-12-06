// -----------------------------------------------------------------------
// <copyright file="Loader.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Scope.Client.API.Features;
    using Scope.Client.API.Interfaces;
    using Scope.Client.Loader.Features;
    using Scope.Client.Loader.Features.Configs;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;
    using YamlDotNet.Serialization.NodeDeserializers;

    /// <summary>
    /// Used to handle mods.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the mods.
        /// </summary>
        public static SortedSet<IMod<IConfig>> Mods => new(ExecutionPriorityComparer.Instance);

        /// <summary>
        /// Gets the <see cref="System.Version"/> of the assembly.
        /// </summary>
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Gets the mod's configs.
        /// </summary>
        public static Config Config => new();

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing the file paths of assemblies.
        /// </summary>
        public static Dictionary<Assembly, string> Locations => new();

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing all the required modules to load the mod.
        /// </summary>
        public static List<Assembly> Dependencies => new();

        /// <summary>
        /// Gets the serializer for configs.
        /// </summary>
        public static ISerializer Serializer { get; } = new SerializerBuilder()
            .WithTypeInspector(inner => new CommentGatheringTypeInspector(inner))
            .WithEmissionPhaseObjectGraphVisitor(args => new CommentsObjectGraphVisitor(args.InnerVisitor))
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .IgnoreFields()
            .Build();

        /// <summary>
        /// Gets the deserializer for configs.
        /// </summary>
        public static IDeserializer Deserializer { get; } = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .WithNodeDeserializer(inner => new ConfigsValidator(inner), deserializer => deserializer.InsteadOf<ObjectNodeDeserializer>())
            .IgnoreFields()
            .IgnoreUnmatchedProperties()
            .Build();

        /// <summary>
        /// Loads an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="path">The path to load the assembly from.</param>
        /// <returns>The loaded <see cref="Assembly"/>; otherwise, <see langword="null"/>.</returns>
        public static Assembly LoadAssemblyFromPath(string path)
        {
            try
            {
                return Assembly.Load(File.ReadAllBytes(path));
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading an assembly at {path}! {exception}");
            }

            return null;
        }

        /// <summary>
        /// Loads all dependencies, mods, configs and then enables all mods.
        /// </summary>
        /// <param name="deps">The dependencies loaded by Scope.</param>
        public static void LoadAll(Assembly[] deps = null)
        {
            Log.Message($"{Assembly.GetExecutingAssembly().GetName().Name} - " +
                $"Version {Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}");
            CustomNetworkManager.Modded = true;
            Paths.Init();

            if (deps?.Length > 0)
                Dependencies.AddRange(deps);

            GlobalStartupProcess();
        }

        /// <summary>
        /// Loads and enables all mods and their dependencies.
        /// </summary>
        public static void GlobalStartupProcess()
        {
            LoadDependencies();
            LoadMods();
            EnableMods();
        }

        /// <summary>
        /// Loads all mods.
        /// </summary>
        public static void LoadMods()
        {
            foreach (string file in Directory.GetFiles(Paths.Dependencies, "*.dll"))
                Assembly.UnsafeLoadFrom(file);

            foreach (Type type in from string file in Directory.GetFiles(Paths.Mods, "*.dll")
                                  let assembly = Assembly.UnsafeLoadFrom(file)
                                  from Type type in assembly.GetTypes()
                                  select type)
            {
                if (type.IsAbstract || type.IsInterface || type.BaseType?.GetGenericTypeDefinition() != typeof(Mod<>))
                    continue;

                IMod<IConfig> mod = null;
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                if (constructor is null)
                {
                    object value = Array.Find(type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public), property => property.PropertyType == type)?.GetValue(null);
                    if (value != null)
                        mod = value as IMod<IConfig>;
                }
                else
                    mod = constructor.Invoke(null) as IMod<IConfig>;

                if (CheckModVersion(mod))
                    continue;

                mod?.OnEnabled();
                Mods.Add(mod);
            }
        }

        /// <summary>
        /// Enables all mods.
        /// </summary>
        public static void EnableMods()
        {
            List<IMod<IConfig>> toLoad = Mods.ToList();

            foreach (IMod<IConfig> mod in toLoad)
            {
                try
                {
                    if (!mod.Name.StartsWith("Scope") || !mod.Config.IsEnabled)
                        continue;

                    mod.OnEnabled();
                    toLoad.Remove(mod);
                }
                catch (Exception e)
                {
                    Log.Error($"Mod \"{mod.Name}\" threw an exception while enabling: {e}");
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

        /// <summary>
        /// Loads all dependencies.
        /// </summary>
        public static void LoadDependencies()
        {
            try
            {
                Log.Info($"Loading dependencies at {Paths.Dependencies}");

                foreach (string dependency in Directory.GetFiles(Paths.Dependencies, "*.dll"))
                {
                    Assembly assembly = LoadAssemblyFromPath(dependency);

                    if (assembly == null)
                        continue;

                    Locations[assembly] = dependency;

                    Dependencies.Add(assembly);

                    Log.Info($"Loaded dependency {assembly.GetName().Name}@{assembly.GetName().Version.ToString(3)}");
                }

                Log.Info("Dependencies loaded successfully!");
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while loading dependencies! {exception}");
            }
        }

        private static bool CheckModVersion(IMod<IConfig> mod)
        {
            Version requiredVersion = mod.RequiredScopeVersion;
            Version actualVersion = Version;

            if (requiredVersion.Major == actualVersion.Major)
                return false;

            if (requiredVersion.Major > actualVersion.Major)
            {
                Log.Error($"You're running an older version of Scope ({Version.ToString(3)})! {mod.Name} won't be loaded! " +
                          $"Required version to load it: {mod.RequiredScopeVersion.ToString(3)}");

                return true;
            }
            else if (requiredVersion.Major < actualVersion.Major)
            {
                Log.Error($"You're running an older version of {mod.Name} ({mod.Version.ToString(3)})! Scope won't load it! " +
                    $"Required version to load it: {requiredVersion.Major}");

                return true;
            }

            return false;
        }
    }
}