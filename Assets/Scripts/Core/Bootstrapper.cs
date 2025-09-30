using HCT.Scripts.Enums;
using HCT.Scripts.Services;
using HCT.Scripts.Services.ConfigManagement;
using HCT.Scripts.Services.SceneManagement;
using System;
using UnityEngine;
using Zenject;

namespace HCT.Scripts.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private IConfigService _configService;
        private ISceneFlowController _sceneFlow;
        private ILoggerService _logger;

        [Inject]
        public void Construct(IConfigService configService, ISceneFlowController sceneFlow,ILoggerService logger)
        {
            _configService = configService;
            _sceneFlow = sceneFlow;
            _logger = logger;
        }

        private async void Start()
        {
            try
            {
                _logger.LogInfo("🚀 [Bootstrapper] Starting initialization...");

                _logger.LogInfo("📂 [Bootstrapper] Loading configs...");
                await _configService.InitializeAsync();
                _logger.LogInfo("✅ [Bootstrapper] Configs loaded");

                _logger.LogInfo("🎮 [Bootstrapper] Loading the MainMenu scene...");
                await _sceneFlow.LoadSceneAsync(SceneKey.MainMenu);
                _logger.LogInfo("✅ [Bootstrapper] MainMenu scene is loaded");

                _logger.LogInfo("🏁 [Bootstrapper] Bootstrapper has completed its work.");
            }
            catch (Exception ex)
            {
                _logger.LogError($" [Bootstrapper] Bootstrapper failed: {ex}");
            }
        }
    }
}