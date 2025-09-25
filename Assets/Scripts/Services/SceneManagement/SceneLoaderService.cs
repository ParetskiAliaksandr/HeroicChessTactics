using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace HCT.Scripts.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public async Task LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
                await Task.Yield();
        }
    }
}
