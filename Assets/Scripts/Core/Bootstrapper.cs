using HCT.Scripts.Services;
using HCT.Scripts.Services.ConfigManagement;
using HCT.Scripts.Services.SceneManagement;
using System;
using System.Threading.Tasks;
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
                _logger.LogInfo("🚀 Bootstrapper: старт инициализации...");

                _logger.LogInfo("📂 Загружаем конфиги...");
                await _configService.InitializeAsync();
                _logger.LogInfo("✅ Конфиги загружены");

                _logger.LogInfo("🎮 Загружаем сцену MainMenu...");
                await _sceneFlow.LoadSceneAsync("MainMenu");
                _logger.LogInfo("✅ Сцена MainMenu загружена");

                _logger.LogInfo("🏁 Bootstrapper завершил работу");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bootstrapper failed: {ex}");
            }
        }
    }
}