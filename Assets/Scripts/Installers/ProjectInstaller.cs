
using Zenject;
using HCT.Scripts.Core;
using HCT.Scripts.Services;

namespace HCT.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILoggerService>().To<LoggerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<Bootstrapper>().AsSingle();
        }
    }
}
