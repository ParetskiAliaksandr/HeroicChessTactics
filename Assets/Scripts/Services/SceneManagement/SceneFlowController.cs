using HCT.Scripts.Enums;
using HCT.Scripts.Services.ConfigManagement.Providers;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HCT.Scripts.Services.SceneManagement
{
    public class SceneFlowController : ISceneFlowController
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly ISceneConfigProvider _configProvider;
        private readonly ILoggerService _logger;

        private readonly SemaphoreSlim _sceneOpLock = new SemaphoreSlim(1, 1);

        public SceneFlowController(ISceneLoaderService sceneLoaderService, ISceneConfigProvider configProvider, ILoggerService logger)
        {
            _sceneLoaderService = sceneLoaderService;
            _configProvider = configProvider;
            _logger = logger;
        }

        public async Task<bool> LoadSceneAdditive(SceneKey targetKey, IProgress<float> progress = null, CancellationToken token = default)
        {
            string sceneName = _configProvider.GetSceneName(targetKey);
            if (string.IsNullOrEmpty(sceneName))
            {
                _logger.LogError($"[SceneFlow] LoadSceneAdditive: scene name for key '{targetKey}' is null or empty.");
                return false;
            }

            await _sceneOpLock.WaitAsync(token);
            try
            {
                var existing = SceneManager.GetSceneByName(sceneName);
                if (existing.IsValid() && existing.isLoaded)
                {
                    _logger.LogInfo($"[SceneFlow] Scene '{sceneName}' already loaded.");
                    progress?.Report(1f);
                    return true;
                }

                if (targetKey == SceneKey.LoadScreen)
                {
                    await _sceneLoaderService.LoadSceneAdditive(sceneName, null, token);
                    _logger.LogInfo($"[SceneFlow] Scene '{sceneName}' loaded (no UI access during load).");
                    return true;
                }

                IProgress<float> progressReporter = new Progress<float>(p =>
                {
                    // _loadingScreenService?.SetProgress(p)
                });

                await _sceneLoaderService.LoadSceneAdditive(sceneName, progressReporter, token);

                var loaded = SceneManager.GetSceneByName(sceneName);
                if (loaded.IsValid() && loaded.isLoaded)
                {
                    if (targetKey != SceneKey.LoadScreen)
                    {
                        SceneManager.SetActiveScene(loaded);
                        _logger.LogInfo($"[SceneFlow] SetActiveScene -> {sceneName}");
                    }

                    return true;
                }

                _logger.LogWarning($"[SceneFlow] Scene '{sceneName}' finished loading but not marked loaded.");
                return false;
            }
            finally
            {
                _sceneOpLock.Release();
            }
        }


        public async Task<bool> UnloadScene(SceneKey key, CancellationToken token = default)
        {
            string sceneName = _configProvider.GetSceneName(key);

            if (string.IsNullOrEmpty(sceneName))
            {
                _logger.LogWarning($"[SceneFlow] UnloadScene called with invalid key '{key}' (no scene name).");
                return false;
            }

            await _sceneOpLock.WaitAsync(token);
            try
            {
                var scene = SceneManager.GetSceneByName(sceneName);

                if (!scene.IsValid() || !scene.isLoaded)
                {
                    _logger.LogInfo($"[SceneFlow] Scene '{sceneName}' is not loaded or not valid. Nothing to unload.");
                    return true;
                }

                var activeScene = SceneManager.GetActiveScene();
                if (activeScene.IsValid() && activeScene.name == sceneName)
                {
                    Scene fallback = default;
                    for (int i = 0; i < SceneManager.sceneCount; i++)
                    {
                        var s = SceneManager.GetSceneAt(i);
                        if (s.IsValid() && s.isLoaded && s.name != sceneName)
                        {
                            fallback = s;
                            break;
                        }
                    }

                    if (fallback.IsValid())
                    {
                        SceneManager.SetActiveScene(fallback);
                        _logger.LogInfo($"[SceneFlow] Active scene was '{sceneName}'. Switched active scene to '{fallback.name}' before unload.");
                    }
                    else
                    {
                        _logger.LogWarning($"[SceneFlow] Active scene '{sceneName}' will be unloaded but no other loaded scene found to switch to.");
                    }
                }

                try
                {
                    await _sceneLoaderService.UnloadSceneAsync(sceneName, token);
                    _logger.LogInfo($"[SceneFlow] Scene '{sceneName}' unloaded successfully.");
                    return true;
                }
                catch (OperationCanceledException)
                {
                    _logger.LogWarning($"[SceneFlow] Unload of scene '{sceneName}' was canceled.");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[SceneFlow] Failed to unload scene '{sceneName}': {ex}");
                    return false;
                }
            }
            finally
            {
                _sceneOpLock.Release();
            }
        }

        public void Dispose()
        {
            _sceneOpLock?.Dispose();
        }
    }
}

