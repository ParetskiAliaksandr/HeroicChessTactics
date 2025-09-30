using HCT.Scripts.Enums;
using HCT.Scripts.Services.ConfigManagement.Providers;
using System.Threading.Tasks;

namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneFlowController : ISceneFlowController
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly ISceneConfigProvider _configProvider;
        private readonly ILoggerService _logger;

        public SceneFlowController(ISceneLoaderService sceneLoaderService, ISceneConfigProvider configProvider, ILoggerService logger)
        {
            _sceneLoaderService = sceneLoaderService;
            _configProvider = configProvider;
            _logger = logger;
        }

        public async Task LoadSceneAsync(SceneKey key)
        {
            string sceneName = _configProvider.GetSceneName(key);

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
