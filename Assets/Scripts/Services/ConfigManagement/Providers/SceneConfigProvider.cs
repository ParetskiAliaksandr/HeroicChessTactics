using HCT.Scripts.Enums;

namespace HCT.Scripts.Services.ConfigManagement.Providers
{
    public class SceneConfigProvider : ISceneConfigProvider
    {
        private readonly IConfigService _configService;
        private readonly ILoggerService _loggerService;

        public SceneConfigProvider(IConfigService configService, ILoggerService loggerService)
        {
            _configService = configService;
            _loggerService = loggerService;
        }

        public string GetSceneName(SceneKey sceneKey)
        {
            if (_configService?.GameConfig == null || _configService.GameConfig.SceneConfigSO == null)
            {
                _loggerService.LogError($"[SceneConfigProvider] GameConfig is not initialized. Call IConfigService.InitializeAsync() first.");
                return null;
            }

            var name = _configService.GameConfig.SceneConfigSO.GetSceneName(sceneKey);

            if (string.IsNullOrEmpty(name))
            {
                _loggerService.LogError($"[SceneConfigProvider] Scene name for key '{sceneKey}' is missing.");
                return null;
            }

            return name;
        }
    }
}
