using UnityEngine.InputSystem;

namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneFlowController : ISceneFlowController
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly SceneConfigSO _sceneConfigSO;

        public SceneFlowController(ISceneLoaderService sceneLoaderService, SceneConfigSO config)
        {
            _sceneLoaderService = sceneLoaderService;
            _sceneConfigSO = config;
        }

        public void LoadScene(string key)
        {
            _sceneLoaderService.LoadScene(_sceneConfigSO.GetSceneName(key));
        }
    }
}
