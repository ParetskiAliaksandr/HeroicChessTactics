using HCT.Scripts.Services;
using HCT.Scripts.Services.ConfigManagement;
using HCT.Scripts.Services.ConfigManagement.Providers;
using HCT.Scripts.Services.LoadingScreenManagement;
using HCT.Scripts.Services.SceneManagement;
using Zenject;

namespace HCT.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILoggerService>().To<LoggerService>().AsSingle();

            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<ISceneFlowController>().To<SceneFlowController>().AsSingle();
            Container.Bind<ILoadingScreenService>().To<LoadingScreenService>().AsSingle();

            Container.Bind<ConfigLoaderService>().AsSingle();
            Container.Bind<IConfigService>().To<ConfigService>().AsSingle();
            Container.Bind<ISceneConfigProvider>().To<SceneConfigProvider>().AsSingle();
        }
    }
}