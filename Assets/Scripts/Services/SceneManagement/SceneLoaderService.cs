using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HCT.Scripts.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public async Task LoadSceneAdditive(string sceneName, IProgress<float> progress = null, CancellationToken token = default)
        {
            var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            if (op == null)
            {
                throw new InvalidOperationException($"Failed to start loading scene '{sceneName}'. Check Build Settings or Addressables.");
            }

            float lastReported = -1f;
            var lastReportTime = DateTime.UtcNow;
            var minInterval = TimeSpan.FromMilliseconds(80);
            const float minDelta = 0.01f;

            try
            {
                while (!op.isDone)
                {
                    token.ThrowIfCancellationRequested();

                    float raw = op.progress;
                    float normalized = (raw >= 0.9f) ? 1f : Mathf.Clamp01(raw / 0.9f);

                    var now = DateTime.UtcNow;
                    bool changedEnough = Mathf.Abs(normalized - lastReported) >= minDelta;
                    bool timeElapsed = (now - lastReportTime) >= minInterval;

                    if (changedEnough || timeElapsed)
                    {
                        progress?.Report(normalized);
                        lastReported = normalized;
                        lastReportTime = now;
                    }

                    await Task.Yield();
                }

                if (lastReported < 1f)
                {
                    progress?.Report(1f);
                }
            }

            catch (OperationCanceledException)
            {
                if (SceneManager.GetSceneByName(sceneName).IsValid())
                {
                    _ = SceneManager.UnloadSceneAsync(sceneName);
                }

                throw;
            }
        }

        public async Task UnloadSceneAsync(string sceneName, CancellationToken token = default)
        {
            var operation = SceneManager.UnloadSceneAsync(sceneName);

            if (operation == null)
            {
                return;
            }

            while (!operation.isDone)
            {
                token.ThrowIfCancellationRequested();

                await Task.Yield();
            }
        }
    }
}

