using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.Logic.Bounds;
using Main.Scripts.Logic.Platforms;
using Main.Scripts.Logic.Zones;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private HealthConfig _healthConfig;
        
        [Header("Prefabs")] 
        
        [Header("Scene Objects")]
        [SerializeField] private Bounder _bounder;
        [SerializeField] private BoundsVisualizer _boundsVisualizer;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterHealthService(serviceContainer);
            RegisterBounder(serviceContainer);
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            TimeProvider timeProvider = new TimeProvider();
            
            serviceContainer.SetService<ITimeProvider, TimeProvider>(timeProvider);
            
            SetGameplayStates(serviceContainer, timeProvider);
        }

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            HealthService healthService = new HealthService(serviceContainer.Get<IGameplayStateMachine>(), _healthConfig);

            serviceContainer.SetService<IHealthService, HealthService>(healthService);
            
            SetGameplayStates(serviceContainer, healthService);
        }

        private void RegisterBounder(ServiceContainer serviceContainer)
        {
            _bounder.Init();
            _boundsVisualizer.Init();
            serviceContainer.SetService<ITickable, BoundsVisualizer>(_boundsVisualizer);
        }
        
        private void SetGameplayStates(ServiceContainer serviceContainer, IGameplayStatable gameplayStatable)
        {
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameplayStatable);
        }
    }
}