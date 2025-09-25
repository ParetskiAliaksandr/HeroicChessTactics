
using Zenject;
using HCT.Scripts.Core;
using HCT.Scripts.Services;
using HCT.Scripts.Services.SceneManagement;

namespace HCT.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public SceneConfigSO sceneConfigSO;

        public override void InstallBindings()
        {
            Container.Bind<SceneConfigSO>().FromInstance(sceneConfigSO).AsSingle();

            Container.Bind<ILoggerService>().To<LoggerService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<ISceneFlowController>().To<SceneFlowController>().AsSingle();

            Container.BindInterfacesAndSelfTo<Bootstrapper>().AsSingle();

            Container.QueueForInject(sceneConfigSO);
        }
    }
}
