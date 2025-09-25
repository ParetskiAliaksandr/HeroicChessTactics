using HCT.Scripts.Services;
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
        }
    }
}