using HCT.Scripts.Config.ConfigManagement;
using System.Threading;
using System.Threading.Tasks;

namespace HCT.Scripts.Services.ConfigManagement
{
    public class ConfigService : IConfigService
    {
        private readonly ConfigLoaderService _configLoaderService;
        public GameConfig GameConfig { get; private set; }

        public ConfigService(ConfigLoaderService configLoaderService)
        {
            _configLoaderService = configLoaderService;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            GameConfig = await _configLoaderService.LoadConfigsAsync(token);
        }
    }
}
