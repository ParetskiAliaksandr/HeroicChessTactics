using HCT.Scripts.Services;
using Zenject;

namespace HCT.Scripts.Core
{
    public class Bootstrapper : IInitializable
    {
        private readonly ILoggerService _loggerService;

        public Bootstrapper(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public void Initialize()
        {
            _loggerService.LogInfo("Игра загружается!");
        }
    }
}
