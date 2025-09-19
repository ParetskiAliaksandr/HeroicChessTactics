using HCT.Scripts.Services;
using HCT.Scripts.Services.SceneManagement;
using Zenject;

namespace HCT.Scripts.Core
{
    public class Bootstrapper : IInitializable
    {
        private readonly ILoggerService _loggerService;
        private readonly ISceneFlowController _sceneFlowController;

        public Bootstrapper(ILoggerService loggerService, ISceneFlowController sceneFlowController)
        {
            _loggerService = loggerService;
            _sceneFlowController = sceneFlowController;
        }

        public void Initialize()
        {
            _loggerService.LogInfo("Здесь будет сцена загрузки с UI отображением процента загрузки игры...");
            _sceneFlowController.LoadScene("MainMenu");
        }
    }
}
