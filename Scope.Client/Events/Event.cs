using Scope.Client.API.Features;
using System;
using static Scope.Client.Events.Events;

namespace Scope.Client.Events.Extensions
{
    public static class Event
    {
        public static void InvokeSafely<T>(this CustomEventHandler<T> ev, T arg) where T : System.EventArgs
        {
            if (ev == null) return;

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

        public static void InvokeSafely(this CustomEventHandler ev)
        {
            if (ev == null) return;

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
