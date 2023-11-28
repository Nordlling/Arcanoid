using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Applications;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        [SerializeField] private ApplicationService _applicationService;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterApplicationService(serviceContainer);
        }
        
        private void RegisterApplicationService(ServiceContainer serviceContainer)
        {
            _applicationService.Construct(_applicationConfig);
            serviceContainer.SetService<IApplicationService, ApplicationService>(_applicationService);
        }
    }
}