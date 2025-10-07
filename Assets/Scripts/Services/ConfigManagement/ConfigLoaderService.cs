using HCT.Scripts.Config.ConfigManagement;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace HCT.Scripts.Services.ConfigManagement
{
    public class ConfigLoaderService
    {
        public async Task<GameConfig> LoadConfigsAsync()
        {
            var sceneConfig = await Addressables.LoadAssetAsync<SceneConfigSO>("SceneConfig").Task;

            return new GameConfig(sceneConfig);
        }
    }
}
