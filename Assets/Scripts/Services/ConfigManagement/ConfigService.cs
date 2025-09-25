using HCT.Scripts.Config.ConfigManagement;
using HCT.Scripts.Services.ConfigManagement;
using System.Threading.Tasks;

namespace Assets.Scripts.Services.ConfigManagement
{
    public class ConfigService : IConfigService
    {
        private readonly ConfigLoaderService _configLoaderService;
        public GameConfig GameConfig { get; private set; }

        public ConfigService(ConfigLoaderService configLoaderService)
        {
            _configLoaderService = configLoaderService;
        }

        public async Task InitializeAsync()
        {
            GameConfig = await _configLoaderService.LoadConfigsAsync();
        }
    }
}
