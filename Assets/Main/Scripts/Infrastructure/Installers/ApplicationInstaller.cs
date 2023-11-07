using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Applications;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        [SerializeField] private ApplicationService _applicationServicePrefab;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterApplicationService(serviceContainer);
        }
        
        private void RegisterApplicationService(ServiceContainer serviceContainer)
        {
            ApplicationService applicationService = Instantiate(_applicationServicePrefab);
            applicationService.Construct(_applicationConfig);
            serviceContainer.SetService<IApplicationService, ApplicationService>(applicationService);
        }
    }
}