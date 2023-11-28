using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameplayUIInstaller : MonoInstaller
    {
        
        [Header("Scene Objects")]
        [SerializeField] private GameplayUIView _gameplayUIView;
        [SerializeField] private ProgressUIView _progressUIView;
        [SerializeField] private HealthUIView _healthUIView;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameplayUI(serviceContainer);
            RegisterProgressUI(serviceContainer);
            RegisterHealthUI(serviceContainer);
        }

        private void RegisterGameplayUI(ServiceContainer serviceContainer)
        {
            _gameplayUIView.Construct(serviceContainer.Get<IGameplayStateMachine>());
            serviceContainer.SetServiceSelf(_gameplayUIView);
        }
        
        private void RegisterProgressUI(ServiceContainer serviceContainer)
        {
            _progressUIView.Construct
                (
                    serviceContainer.Get<IPackService>(), 
                    serviceContainer.Get<IGameGridService>(),
                    serviceContainer.Get<ITimeProvider>());

            serviceContainer.SetService<IInitializable, ProgressUIView>(_progressUIView);
            serviceContainer.SetService<ITickable, ProgressUIView>(_progressUIView);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(_progressUIView);
        }
        
        private void RegisterHealthUI(ServiceContainer serviceContainer)
        {
            _healthUIView.Construct(serviceContainer.Get<IHealthService>());
            serviceContainer.SetService<IInitializable, HealthUIView>(_healthUIView);
        }
    }
}