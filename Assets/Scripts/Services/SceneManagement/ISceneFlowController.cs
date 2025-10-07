using HCT.Scripts.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HCT.Scripts.Services.SceneManagement
{
    public interface ISceneFlowController
    {
        Task<bool> LoadSceneAdditive(SceneKey targetKey, IProgress<float> progress = null, CancellationToken token = default);
        Task<bool> UnloadScene(SceneKey targetKey, CancellationToken token = default);
    }
}
