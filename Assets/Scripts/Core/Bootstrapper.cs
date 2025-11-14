using HCT.Scripts.Enums;
using HCT.Scripts.Services;
using HCT.Scripts.Services.ConfigManagement;
using HCT.Scripts.Services.SceneManagement;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace HCT.Scripts.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private IConfigService _configService;
        private ISceneManagementService _sceneFlow;
        private ILoggerService _logger;

        private CancellationTokenSource _cts;

        private bool isLoaded = false;
        private bool isUnloaded = false;

        [Inject]
        public void Construct(IConfigService configService, ISceneManagementService sceneFlow,ILoggerService logger)
        {
            _configService = configService;
            _sceneFlow = sceneFlow;
            _logger = logger;
        }

        private void Start()
        {
            _cts = new CancellationTokenSource();

            _ = InitializeAsync(_cts.Token).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    _logger.LogError($"❌ Initialization failed: {task.Exception}");
                }
            });
        }

        private async Task InitializeAsync(CancellationToken token)
        {
            try
            {
                _logger.LogInfo("🚀 [Bootstrapper] Starting initialization...");

                _logger.LogInfo("📂 [Bootstrapper] Loading configs...");
                await _configService.InitializeAsync(token);
                _logger.LogInfo("✅ [Bootstrapper] Configs loaded");

                _logger.LogInfo("🎮 [Bootstrapper] Loading LoadScreenScene and MainMenuScene...");
                isLoaded = await _sceneFlow.LoadSceneAdditive(SceneKey.LoadScreen, null, token);
                if (!isLoaded)
                {
                    _logger.LogError("[Bootstrapper] Failed to load LoadScreen. Aborting startup.");
                    return;
                }

                isLoaded = await _sceneFlow.LoadSceneAdditive(SceneKey.MainMenu, null, token);
                if (!isLoaded)
                {
                    _logger.LogError("[Bootstrapper] Failed to load MainMenu. Aborting startup.");
                    return;
                }
                _logger.LogInfo("✅ [Bootstrapper] LoadScreenScene and MainMenuScene loaded");

                _logger.LogInfo("🏁 [Bootstrapper] Bootstrapper has completed its work and will unload BootScene..");
                isUnloaded = await _sceneFlow.UnloadScene(SceneKey.BootScene, token);
                if (!isUnloaded)
                {
                    _logger.LogWarning("[Bootstrapper] BootScene unload returned false.");
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("⚠️ [Bootstrapper] Loading was cancelled.");
            }
        }


        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}