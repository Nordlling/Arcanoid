using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.UI;
using Main.Scripts.UI.Views;

namespace Main.Scripts.Infrastructure.Installers.InitialSceneInstallers
{
    public class InitialSceneWindowInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            OpenInitialWindow(serviceContainer);
        }

        private void OpenInitialWindow(ServiceContainer serviceContainer)
        {
            IWindowsManager windowsManager = serviceContainer.Get<IWindowsManager>();
            InitialUIView initialUIView = windowsManager.GetWindow<InitialUIView>();
            initialUIView.Construct(serviceContainer.Get<ISaveLoadService>(), serviceContainer.Get<IPackService>());
            initialUIView.Open();
        }
    }
}