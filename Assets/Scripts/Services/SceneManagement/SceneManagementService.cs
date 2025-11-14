using HCT.Scripts.Enums;
using HCT.Scripts.Services.ConfigManagement.Providers;
using HCT.Scripts.Services.LoadingScreenManagement;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneManagementService : ISceneManagementService
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly ISceneConfigProvider _sceneConfigProvider;
        private readonly ILoggerService _logger;
        private readonly ILoadingScreenService _loadingScreenService;

        private readonly SemaphoreSlim _sceneOpLock = new SemaphoreSlim(1, 1);

        public SceneManagementService(ISceneLoaderService sceneLoaderService, ISceneConfigProvider sceneConfigProvider, ILoggerService logger,
            ILoadingScreenService loadingScreenService)
        {
            _sceneLoaderService = sceneLoaderService;
            _sceneConfigProvider = sceneConfigProvider;
            _logger = logger;
            _loadingScreenService = loadingScreenService;
        }

        public async Task<bool> LoadSceneAdditive(SceneKey targetKey, IProgress<float> progress = null, CancellationToken token = default)
        {
            string sceneName = _sceneConfigProvider.GetSceneName(targetKey);

            if (sceneName == null)
            {
                return false;
            }

            await _sceneOpLock.WaitAsync(token);

            try
            {
                Scene existingScene = SceneManager.GetSceneByName(sceneName);

                if (existingScene.IsValid() && existingScene.isLoaded)
                {
                    _logger.LogInfo($"[SceneManagementService] Scene '{sceneName}' already loaded.");
                    progress?.Report(1f);
                    return true;
                }

                if (targetKey == SceneKey.LoadScreen)
                {
                    await _sceneLoaderService.LoadSceneAdditive(sceneName, null, token);
                    _logger.LogInfo($"[SceneManagementService] Scene '{sceneName}' loaded.");
                    return true;
                }

                _loadingScreenService.Show();
                _loadingScreenService.ResetProgress();
                IProgress<float> progressReporter = new Progress<float>(p =>
                {
                    _loadingScreenService.SetProgress(p);
                });
                await _sceneLoaderService.LoadSceneAdditive(sceneName, progressReporter, token);
                _loadingScreenService.Hide();

                var loaded = SceneManager.GetSceneByName(sceneName);
                if (loaded.IsValid() && loaded.isLoaded)
                {
                    if (targetKey != SceneKey.LoadScreen)
                    {
                        SceneManager.SetActiveScene(loaded);
                        _logger.LogInfo($"[SceneManagementService] Scene '{sceneName}' loaded");
                        _logger.LogInfo($"[SceneManagementService] SetActiveScene -> {sceneName}");
                    }

                    return true;
                }

                _logger.LogWarning($"[SceneManagementService] Scene '{sceneName}' finished loading but not marked loaded.");
                return false;
            }
            finally
            {
                _sceneOpLock.Release();
            }
        }


        public async Task<bool> UnloadScene(SceneKey key, CancellationToken token = default)
        {
            string sceneName = _sceneConfigProvider.GetSceneName(key);

            if (sceneName == null)
            {
                return false;
            }

            await _sceneOpLock.WaitAsync(token);

            try
            {
                var existingScene = SceneManager.GetSceneByName(sceneName);

                if (!existingScene.IsValid() || !existingScene.isLoaded)
                {
                    _logger.LogInfo($"[SceneManagementService] Scene '{sceneName}' is not loaded or not valid. Nothing to unload.");
                    return true;
                }

                var activeScene = SceneManager.GetActiveScene();

                try
                {
                    await _sceneLoaderService.UnloadSceneAsync(sceneName, token);
                    _logger.LogInfo($"[SceneManagementService] Scene '{sceneName}' unloaded successfully.");
                    return true;
                }
                catch (OperationCanceledException)
                {
                    _logger.LogWarning($"[SceneManagementService] Unload of scene '{sceneName}' was canceled.");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[SceneManagementService] Failed to unload scene '{sceneName}': {ex}");
                    return false;
                }
            }
            finally
            {
                _sceneOpLock.Release();
            }
        }
    }
}

