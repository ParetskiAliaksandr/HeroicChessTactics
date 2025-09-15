using UnityEngine;

namespace HCT.Scripts.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogError(string message)
        {
            Debug.LogError($"<color=#FF0000>[ERROR]</color> {message}");
        }

        public void LogInfo(string message)
        {
            Debug.Log($"<color=#00FF00>[INFO]</color> {message}");
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning($"<color=#FFFF00>[WARNING]</color> {message}");
        }
    }
}
