using HCT.Scripts.Enums;
using System.Threading.Tasks;

namespace HCT.Scripts.Services.SceneManagement
{
    public interface ISceneFlowController
    {
        Task LoadSceneAsync(SceneKey key);
    }
}
