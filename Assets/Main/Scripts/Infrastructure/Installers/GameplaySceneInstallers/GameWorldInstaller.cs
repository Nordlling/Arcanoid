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
        [SerializeField] private Transform _winEffectPoint;
        [SerializeField] private Transform _loseEffectPoint;
        
        [Header("Configs")]
        [SerializeField] private FinalConfig _finalConfig;

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
            FinalService finalService = new FinalService(
                serviceContainer.Get<ITimeProvider>(),
                serviceContainer.Get<IEffectFactory>(),
                serviceContainer.Get<ComprehensiveRaycastBlocker>(),
                _finalConfig,
                _winEffectPoint.position,
                _loseEffectPoint.position
            );
            
            serviceContainer.SetServiceSelf(finalService);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(finalService);
        }

        private void RegisterBounder(ServiceContainer serviceContainer)
        {
            _bounder.Init();
            _boundsVisualizer.Init();
            serviceContainer.SetService<ITickable, BoundsVisualizer>(_boundsVisualizer);
        }
    }
}