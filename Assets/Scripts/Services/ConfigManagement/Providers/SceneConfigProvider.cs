
using HCT.Scripts.Enums;

namespace HCT.Scripts.Services.ConfigManagement.Providers
{
    public class SceneConfigProvider : ISceneConfigProvider
    {
        private readonly SceneConfigSO _sceneConfigSO;

        public SceneConfigProvider(ISceneConfigeProvader configService)
        {
            _sceneConfigSO = configService.GameConfig.SceneConfigSO;
        }

        public string GetSceneName(SceneKey sceneKey)
        {
            return _sceneConfigSO.GetSceneName(sceneKey);
        }
    }
}
