using HCT.Scripts.Services.ConfigManagement;
using System.Threading.Tasks;

namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneFlowController : ISceneFlowController
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IConfigService _configService;
        private readonly ILoggerService _logger;

        public SceneFlowController(ISceneLoaderService sceneLoaderService, IConfigService configService, ILoggerService logger)
        {
            _sceneLoaderService = sceneLoaderService;
            _configService = configService;
            _logger = logger;
        }

        public async Task LoadSceneAsync(string key)
        {
            string sceneName = _configService.GameConfig.SceneConfig.GetSceneName(key);

            if (string.IsNullOrEmpty(sceneName))
            {
                _logger.LogError($"[SceneFlow] Не удалось загрузить сцену по ключу '{key}'");
                return;
            }

            _logger.LogInfo($"[SceneFlow] Загружаем сцену '{sceneName}' по ключу '{key}'");
            await _sceneLoaderService.LoadSceneAsync(sceneName);
        }
    }
}
