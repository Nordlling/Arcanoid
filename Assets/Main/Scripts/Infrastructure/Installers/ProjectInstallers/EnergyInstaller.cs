using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class EnergyInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private EnergyConfig _energyConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterEnergyService(serviceContainer);
        }
        
        private void RegisterEnergyService(ServiceContainer serviceContainer)
        {
            EnergyService energyService = new EnergyService(serviceContainer.Get<ISaveLoadService>(), _energyConfig);

            serviceContainer.SetService<IEnergyService, EnergyService>(energyService);
        }
    }
}