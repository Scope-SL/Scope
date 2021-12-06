// -----------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader.Features
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Scope.Client.API.Extensions;
    using Scope.Client.API.Features;
    using Scope.Client.API.Interfaces;
    using YamlDotNet.Core;

    /// <summary>
    /// Mod configs handler.
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// Loads all mod configs.
        /// </summary>
        /// <param name="rawConfigs">The raw configs to be loaded.</param>
        /// <returns>A <see cref="SortedDictionary{TKey, TValue}"/> of loaded configs.</returns>
        public static SortedDictionary<string, IConfig> LoadSorted(string rawConfigs)
        {
            try
            {
                Log.Info("Loading mod configs...");

                Dictionary<string, object> rawDeserializedConfigs = Loader.Deserializer.Deserialize<Dictionary<string, object>>(rawConfigs) ?? new();
                SortedDictionary<string, IConfig> deserializedConfigs = new(StringComparer.Ordinal);

                if (!rawDeserializedConfigs.TryGetValue("scope_loader", out object rawDeserializedConfig))
                {
                    Log.Warn($"Scope.Loader doesn't have default configs, generating...");

                    deserializedConfigs.Add("scope_loader", Loader.Config);
                }
                else
                {
                    deserializedConfigs.Add("scope_loader", Loader.Deserializer.Deserialize<Config>(Loader.Serializer.Serialize(rawDeserializedConfig)));
                    Loader.Config.CopyProperties(deserializedConfigs["scope_loader"]);
                }

                foreach (IMod<IConfig> mod in Loader.Mods)
                {
                    if (!rawDeserializedConfigs.TryGetValue(mod.Prefix, out rawDeserializedConfig))
                    {
                        Log.Warn($"{mod.Name} doesn't have default configs, generating...");
                        deserializedConfigs.Add(mod.Prefix, mod.Config);
                    }
                    else
                    {
                        try
                        {
                            deserializedConfigs.Add(mod.Prefix, (IConfig)Loader.Deserializer.Deserialize(Loader.Serializer.Serialize(rawDeserializedConfig), mod.Config.GetType()));
                            mod.Config.CopyProperties(deserializedConfigs[mod.Prefix]);
                        }
                        catch (YamlException yamlException)
                        {
                            Log.Error($"{mod.Name} configs could not be loaded, some of them are in a wrong format, default configs will be loaded instead! {yamlException}");
                            deserializedConfigs.Add(mod.Prefix, mod.Config);
                        }
                    }
                }

                Log.Info("Mod configs loaded successfully!");

                return deserializedConfigs;
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while loading configs! {exception}");

                return null;
            }
        }

        /// <summary>
        /// Reads, Loads and Saves mod configs.
        /// </summary>
        /// <returns><see langword="true"/> if the reloading process has been completed successfully; otherwise, <see langword="false"/>.</returns>
        public static bool Reload() => Save(LoadSorted(Read()));

        /// <summary>
        /// Saves mod configs.
        /// </summary>
        /// <param name="configs">The configs to be saved, already serialized in yaml format.</param>
        /// <returns><see langword="true"/> if configs have been saved successfully; otherwise, <see langword="false"/>.</returns>
        public static bool Save(string configs)
        {
            try
            {
                File.WriteAllText(Paths.Config, configs ?? string.Empty);
                return true;
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while saving configs to {Paths.Config} path: {exception}");
                return false;
            }
        }

        /// <summary>
        /// Saves mod configs.
        /// </summary>
        /// <param name="configs">The configs to be saved.</param>
        /// <returns><see langword="true"/> if configs have been saved successfully; otherwise, <see langword="false"/>.</returns>
        public static bool Save(SortedDictionary<string, IConfig> configs)
        {
            try
            {
                if (configs == null || configs.Count == 0)
                    return false;

                return Save(Loader.Serializer.Serialize(configs));
            }
            catch (YamlException yamlException)
            {
                Log.Error($"An error has occurred while serializing configs: {yamlException}");

                return false;
            }
        }

        /// <summary>
        /// Reads all mod configs.
        /// </summary>
        /// <returns>The read configs.</returns>
        public static string Read()
        {
            try
            {
                if (File.Exists(Paths.Config))
                    return File.ReadAllText(Paths.Config);
            }
            catch (Exception exception)
            {
                Log.Error($"An error has occurred while reading configs from {Paths.Config} path: {exception}");
            }

            return string.Empty;
        }

        /// <summary>
        /// Clears the configs.
        /// </summary>
        /// <returns><see langword="true"/> if configs have been cleared successfully; otherwise, <see langword="false"/>.</returns>
        public static bool Clear() => Save(string.Empty);
    }
}