using System.Threading.Tasks;

namespace HCT.Scripts.Services.SceneManagement
{
    public interface ISceneFlowController
    {
        Task LoadSceneAsync(string key);
    }
}
