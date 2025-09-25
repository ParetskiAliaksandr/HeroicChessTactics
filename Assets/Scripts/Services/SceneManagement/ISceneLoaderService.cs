using System.Threading.Tasks;

namespace HCT.Scripts.Services
{
    public interface ISceneLoaderService
    {
        Task LoadSceneAsync(string sceneName);
    }
}
