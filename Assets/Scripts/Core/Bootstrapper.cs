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
        private async void Start()
        {
            try
            {
                await RunAsync();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Bootstrapper failed: {ex}");
            }
        }

        private async Task RunAsync()
        {
            var logger = ProjectContext.Instance.Container.Resolve<ILoggerService>();

            logger.LogInfo("🚀 Bootstrapper: старт инициализации...");

            logger.LogInfo("📂 Загружаем конфиги...");
            var configService = ProjectContext.Instance.Container.Resolve<IConfigService>();
            await configService.InitializeAsync();
            logger.LogInfo("✅ Конфиги загружены");

            logger.LogInfo("🎮 Загружаем сцену MainMenu...");
            var sceneFlow = ProjectContext.Instance.Container.Resolve<ISceneFlowController>();
            await sceneFlow.LoadSceneAsync("MainMenu");
            logger.LogInfo("✅ Сцена MainMenu загружена");

            logger.LogInfo("🏁 Bootstrapper завершил работу");
        }
    }
}