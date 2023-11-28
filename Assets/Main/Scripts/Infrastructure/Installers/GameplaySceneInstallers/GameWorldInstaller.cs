using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Bounds;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Bounder _bounder;
        [SerializeField] private BoundsVisualizer _boundsVisualizer;
        [SerializeField] private Transform _finalEffectTransform;
        
        [Header("Configs")]
        [SerializeField] private WinConfig _winConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterFinalService(serviceContainer);
            RegisterBounder(serviceContainer);
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            TimeProvider timeProvider = new TimeProvider();
            serviceContainer.SetService<ITimeProvider, TimeProvider>(timeProvider);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(timeProvider);
        }

        private void RegisterFinalService(ServiceContainer serviceContainer)
        {
            SpawnContext spawnContext = new SpawnContext { Position = _finalEffectTransform.position };
            
            WinService winService = new WinService(
                serviceContainer.Get<ITimeProvider>(),
                serviceContainer.Get<IEffectFactory>(),
                serviceContainer.Get<ComprehensiveRaycastBlocker>(),
                _winConfig,
                spawnContext
            );
            
            serviceContainer.SetServiceSelf(winService);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(winService);
        }

        private void RegisterBounder(ServiceContainer serviceContainer)
        {
            _bounder.Init();
            _boundsVisualizer.Init();
            serviceContainer.SetService<ITickable, BoundsVisualizer>(_boundsVisualizer);
        }
    }
}