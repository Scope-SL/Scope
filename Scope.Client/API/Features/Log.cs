// -----------------------------------------------------------------------
// <copyright file="Log.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using BepInEx.Logging;

    /// <summary>
    /// A set of tools to easily handle logging actions.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Sends a <see cref="LogLevel.Info"/> level messages.
        /// <para>This should be used to log less relevant messages.</para>
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public static void Info(object message) => Loader.BepInExLoader.Instance.Log.LogInfo(message);

        /// <summary>
        /// Sends a <see cref="LogLevel.Warning"/> level messages.
        /// <para>The <see cref="LogLevel.Warning"/> is made to be used to log warnings only.</para>
        /// <para>This is not a replacement for less relevant messages.</para>
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public static void Warn(object message) => Loader.BepInExLoader.Instance.Log.LogWarning(message);

        /// <summary>
        /// Sends a <see cref="LogLevel.Error"/> level messages.
        /// <para>The <see cref="LogLevel.Error"/> is made to be used to log errors only.</para>
        /// <para>This is not a replacement for less relevant messages.</para>
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public static void Error(object message) => Loader.BepInExLoader.Instance.Log.LogError(message);

        /// <summary>
        /// Sends a <see cref="LogLevel.Fatal"/> level messages.
        /// <para>The <see cref="LogLevel.Fatal"/> is made to be used to log fatals only.</para>
        /// <para>This is not a replacement for less relevant messages.</para>
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public static void Fatal(object message) => Loader.BepInExLoader.Instance.Log.LogFatal(message);

        /// <summary>
        /// Sends a <see cref="LogLevel.Message"/> level messages.
        /// <para>The <see cref="LogLevel.Message"/> is made to be used to log fatals only.</para>
        /// <para>This is not a replacement for less relevant messages.</para>
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public static void Message(object message) => Loader.BepInExLoader.Instance.Log.LogMessage(message);

        /// <summary>
        /// Sends a message with <see cref="LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="canBeSent">Indicates whether the log can be sent or not.</param>
        public static void Debug(object message, bool canBeSent = true)
        {
            if (!canBeSent)
                return;

            Loader.BepInExLoader.Instance.Log.LogDebug(message);
        }

        /// <summary>
        /// Sends a raw message.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="logLevel">The <see cref="LogLevel"/> to be used.</param>
        public static void SendRaw(object message, LogLevel logLevel) => Loader.BepInExLoader.Instance.Log.Log(logLevel, message);
    }
}
