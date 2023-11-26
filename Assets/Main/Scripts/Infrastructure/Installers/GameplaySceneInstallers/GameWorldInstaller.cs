using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Bounds;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Bounder _bounder;
        [SerializeField] private BoundsVisualizer _boundsVisualizer;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterBounder(serviceContainer);
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            TimeProvider timeProvider = new TimeProvider();
            serviceContainer.SetService<ITimeProvider, TimeProvider>(timeProvider);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(timeProvider);
        }

        private void RegisterBounder(ServiceContainer serviceContainer)
        {
            _bounder.Init();
            _boundsVisualizer.Init();
            serviceContainer.SetService<ITickable, BoundsVisualizer>(_boundsVisualizer);
        }
    }
}