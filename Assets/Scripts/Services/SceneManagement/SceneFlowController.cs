using HCT.Scripts.Enums;
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

        public async Task LoadSceneAsync(SceneKey key)
        {
            string sceneName = _configService.GameConfig.SceneConfig.GetSceneName(key);

            if (string.IsNullOrEmpty(sceneName))
            {
                _logger.LogError($"[SceneFlow] Failed to load scene by key '{key}'");
                return;
            }

            _logger.LogInfo($"[SceneFlow] Loading scene '{sceneName}' by key '{key}'");
            await _sceneLoaderService.LoadSceneAsync(sceneName);
        }
    }
}
