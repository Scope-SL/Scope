// -----------------------------------------------------------------------
// <copyright file="Event.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Events.Extensions
{
    using System;
    using Scope.Client.API.Features;
    using static Scope.Client.Events.Events;

    /// <summary>
    /// A set of tools to execute events safely and without breaking other mods.
    /// </summary>
    public static class Event
    {
        /// <summary>
        /// Executes all <see cref="Events.CustomEventHandler{TEventArgs}"/> listeners safely.
        /// </summary>
        /// <typeparam name="T">Event arg type.</typeparam>
        /// <param name="ev">Source event.</param>
        /// <param name="arg">Event arg.</param>
        /// <exception cref="ArgumentNullException">Event or its arg is null.</exception>
        public static void InvokeSafely<T>(this CustomEventHandler<T> ev, T arg)
            where T : System.EventArgs
        {
            if (ev == null)
                return;

            string eventName = ev.GetType().FullName;
            foreach (CustomEventHandler<T> handler in ev.GetInvocationList())
            {
                try
                {
                    handler(arg);
                }
                catch (Exception ex)
                {
                    LogException(ex, handler.Method.Name, handler.Method.ReflectedType.FullName, eventName);
                }
            }
        }

        /// <summary>
        /// Executes all <see cref="Events.CustomEventHandler"/> listeners safely.
        /// </summary>
        /// <param name="ev">Source event.</param>
        /// <exception cref="ArgumentNullException">Event is null.</exception>
        public static void InvokeSafely(this CustomEventHandler ev)
        {
            if (ev == null)
                return;

            string eventName = ev.GetType().FullName;
            foreach (CustomEventHandler handler in ev.GetInvocationList())
            {
                try
                {
                    handler();
                }
                catch (Exception ex)
                {
                    LogException(ex, handler.Method.Name, handler.Method.ReflectedType?.FullName, eventName);
                }
            }
        }

        private static void LogException(Exception ex, string methodName, string className, string eventName)
        {
            Log.Error($"Method \"{methodName}\" of the class \"{className}\" caused an exception when handling the event \"{eventName}\"");
            Log.Error(ex);
        }
    }
}
