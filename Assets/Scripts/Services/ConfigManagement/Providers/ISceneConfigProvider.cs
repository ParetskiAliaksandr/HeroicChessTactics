using HCT.Scripts.Enums;

namespace HCT.Scripts.Services.ConfigManagement.Providers
{
    public interface ISceneConfigProvider
    {
        public string GetSceneName(SceneKey sceneKey);
    }
}
