using HCT.Scripts.Config.ConfigManagement;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace HCT.Scripts.Services.ConfigManagement
{
    public class ConfigLoaderService
    {
        public async Task<GameConfig> LoadConfigsAsync()
        {
            var sceneConfig = Addressables.LoadAssetAsync<SceneConfigSO>("SceneConfig");

            await Task.WhenAll(sceneConfig.Task);

            return new GameConfig(sceneConfig.Result);
        }
    }
}
