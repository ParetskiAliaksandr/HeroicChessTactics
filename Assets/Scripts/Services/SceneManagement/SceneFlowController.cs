using HCT.Scripts.Config.ConfigManagement;

namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneFlowController : ISceneFlowController
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly GameConfig _gameConfig;
        private readonly ILoggerService _logger;

        public SceneFlowController(ISceneLoaderService sceneLoaderService, GameConfig gameConfig, ILoggerService logger)
        {
            _sceneLoaderService = sceneLoaderService;
            _gameConfig = gameConfig;
            _logger = logger;
        }

        public void LoadScene(string key)
        {
            string sceneName = _gameConfig.SceneConfig.GetSceneName(key);

            if (string.IsNullOrEmpty(sceneName))
            {
                _logger.LogError($"[SceneFlow] Не удалось загрузить сцену по ключу '{key}'");
                return;
            }

            _logger.LogInfo($"[SceneFlow] Загружаем сцену '{sceneName}' по ключу '{key}'");
            _sceneLoaderService.LoadScene(sceneName);
        }
    }
}
