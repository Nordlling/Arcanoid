using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.GameGrid;
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
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameplayUI(serviceContainer);
            RegisterProgressUI(serviceContainer);
        }

        private void RegisterGameplayUI(ServiceContainer serviceContainer)
        {
            _gameplayUIView.Construct(serviceContainer.Get<IGameplayStateMachine>());
        }
        
        private void RegisterProgressUI(ServiceContainer serviceContainer)
        {
            _progressUIView.Construct(serviceContainer.Get<IPackService>(), serviceContainer.Get<IGameGridService>());
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(_progressUIView);
        }
    }
}