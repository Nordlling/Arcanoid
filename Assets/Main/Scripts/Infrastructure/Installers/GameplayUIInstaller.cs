using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameplayUIInstaller : MonoInstaller
    {
        
        [Header("Scene Objects")]
        [SerializeField] private GameplayUIView _gameplayUIView;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameplayUI(serviceContainer);
        }

        private void RegisterGameplayUI(ServiceContainer serviceContainer)
        {
            _gameplayUIView.Construct(serviceContainer.Get<IGameplayStateMachine>());
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(_gameplayUIView);
        }
    }
}