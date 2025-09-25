namespace HCT.Scripts.Services
{
    public interface ILoggerService
    {
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}
