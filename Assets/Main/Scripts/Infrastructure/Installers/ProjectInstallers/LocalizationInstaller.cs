using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Localization;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class LocalizationInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationConfig _localizationConfig;
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterLocalizationService(serviceContainer);
        }
        
        private void RegisterLocalizationService(ServiceContainer serviceContainer)
        {
            LocalizationManager localizationManager = new LocalizationManager(new LocalizationParser(), _localizationConfig);
            serviceContainer.SetService<ILocalizationManager, LocalizationManager>(localizationManager);
        }
    }
}