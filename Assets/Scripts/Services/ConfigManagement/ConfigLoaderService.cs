using HCT.Scripts.Config.ConfigManagement;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HCT.Scripts.Services.ConfigManagement
{
    public class ConfigLoaderService
    {
        private ILoggerService _loggerService;
        public ConfigLoaderService(ILoggerService logger)
        {
            _loggerService = logger;
        }

        public async Task<GameConfig> LoadConfigsAsync(CancellationToken cancel)
        {
            cancel.ThrowIfCancellationRequested();

            var sceneConfigHandle = Addressables.LoadAssetAsync<SceneConfigSO>("SceneConfig");

            await Task.WhenAll(sceneConfigHandle.Task);

            cancel.ThrowIfCancellationRequested();

            if (sceneConfigHandle.Status != AsyncOperationStatus.Succeeded)
            {
                _loggerService.LogError("❌ One or more configs failed to load.");

                throw new Exception($"Configs loading failed.");
            }

            return new GameConfig(sceneConfigHandle.Result);
        }
    }
}
