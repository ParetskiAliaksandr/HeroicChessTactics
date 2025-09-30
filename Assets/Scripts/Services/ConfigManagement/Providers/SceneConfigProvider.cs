
using HCT.Scripts.Enums;

namespace HCT.Scripts.Services.ConfigManagement.Providers
{
    public class SceneConfigProvider : ISceneConfigProvider
    {
        private readonly IConfigService _configService;

        public SceneConfigProvider(IConfigService configService)
        {
            _configService = configService;
        }

        public string GetSceneName(SceneKey sceneKey)
        {
            return _configService.GameConfig.SceneConfigSO.GetSceneName(sceneKey);
        }
    }
}
