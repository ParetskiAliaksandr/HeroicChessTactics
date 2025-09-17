namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneFlowController : ISceneFlowController
    {
        private readonly ISceneLoaderService _sceneLoaderService;

        public SceneFlowController(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public void LoadMainMenuScene()
        {
            _sceneLoaderService.LoadScene("MainMenu");
        }
    }
}
