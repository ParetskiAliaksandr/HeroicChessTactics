using HCT.Scripts.Config.ConfigManagement;
using System.Threading;
using System.Threading.Tasks;


namespace HCT.Scripts.Services.ConfigManagement
{
    public interface IConfigService
    {
        GameConfig GameConfig { get; }
        Task InitializeAsync(CancellationToken token);
    }
}
