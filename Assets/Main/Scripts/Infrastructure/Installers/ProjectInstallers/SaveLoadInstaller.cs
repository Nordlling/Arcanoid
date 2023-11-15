using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.SaveLoad;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterSaveLoadService(serviceContainer);
        }

        private void RegisterSaveLoadService(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<ISaveLoadService, SaveLoadService>(new SaveLoadService());
        }
    }
}