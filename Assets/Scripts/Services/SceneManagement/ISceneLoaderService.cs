using System;
using System.Threading;
using System.Threading.Tasks;

namespace HCT.Scripts.Services
{
    public interface ISceneLoaderService
    {
        Task LoadSceneAdditive(string sceneName, IProgress<float> progress = null, CancellationToken token = default);
        Task UnloadSceneAsync(string sceneName, CancellationToken token = default);
    }
}
