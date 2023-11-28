using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Healths;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class HealthInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private HealthConfig _healthConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterHealthService(serviceContainer);
            RegisterLifeSystem(serviceContainer);
        }

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            HealthService healthService = new HealthService(serviceContainer.Get<IGameplayStateMachine>(), _healthConfig);

            serviceContainer.SetService<IHealthService, HealthService>(healthService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(healthService);
        }
        
        private void RegisterLifeSystem(ServiceContainer serviceContainer)
        {
            LifeSystem lifeSystem = new LifeSystem(serviceContainer.Get<IHealthService>(), serviceContainer.Get<IEffectFactory>());

            serviceContainer.SetService<ILifeSystem, LifeSystem>(lifeSystem);
        }
    }
}