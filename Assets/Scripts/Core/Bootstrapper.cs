using HCT.Scripts.Config.ConfigManagement;
using HCT.Scripts.Services.ConfigManagement;
using HCT.Scripts.Services.SceneManagement;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace HCT.Scripts.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private async void Start()
        {
            await RunAsync();
        }

        private async Task RunAsync()
        {
            // 1. Загружаем все конфиги
            GameConfig gameConfig = await InitializeConfigs();

            // 2. Регистрируем их в контейнер
            RegisterGameConfig(gameConfig);

            // 3. Запускаем игру
            StartGame();
        }

        private void StartGame()
        {
            var sceneFlow = ProjectContext.Instance.Container.Resolve<ISceneFlowController>();
            sceneFlow.LoadScene("MainMenu");
        }

        private void RegisterGameConfig(GameConfig gameConfig)
        {
            ProjectContext.Instance.Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();
        }

        private async Task<GameConfig> InitializeConfigs()
        {
            Debug.Log("🚀 Bootstrapper: загружаем все конфиги...");

            ConfigLoaderService configLoaderService = new ConfigLoaderService();
            GameConfig newGameConfig = await configLoaderService.LoadConfigsAsync();

            Debug.Log("✅ Bootstrapper: все конфиги загружены!");

            return newGameConfig;
        }
    }
}