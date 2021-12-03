using BepInEx.Logging;

namespace Scope.Client.API.Features
{
    public class Log
    {
        public static void Info(object message) => BepInExLoad.Instance.Log.LogInfo(message);

        public static void Warn(object message) => BepInExLoad.Instance.Log.LogWarning(message);

        public static void Error(object message) => BepInExLoad.Instance.Log.LogError(message);

        public static void Fatal(object message) => BepInExLoad.Instance.Log.LogFatal(message);

        public static void Message(object message) => BepInExLoad.Instance.Log.LogMessage(message);

        public static void Debug(object message, bool canBeSent = true)
        {
            if (!canBeSent) 
                return;

            BepInExLoad.Instance.Log.LogDebug(message);
        }

        public static void SendRaw(object message, LogLevel logLevel) => BepInExLoad.Instance.Log.Log(logLevel, message);
    }
}
