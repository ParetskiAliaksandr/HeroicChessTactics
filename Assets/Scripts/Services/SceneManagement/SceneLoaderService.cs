using UnityEngine.SceneManagement;

namespace HCT.Scripts.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
