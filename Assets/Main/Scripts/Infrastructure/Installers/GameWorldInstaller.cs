using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.ButtonContainer;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Configs")]

        [Header("Prefabs")]
        
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterGameplayStateMachine(serviceContainer);
        }


        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            SlowedTimeProvider slowedTimeProvider = new SlowedTimeProvider();
            
            serviceContainer.SetService<ITimeProvider, SlowedTimeProvider>(slowedTimeProvider);
        }

        private void RegisterGameplayStateMachine(ServiceContainer serviceContainer)
        {
            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();
            
            gameplayStateMachine.AddState(new PlayState());
            gameplayStateMachine.AddState(new PauseState(serviceContainer.Get<ITimeProvider>()));
            gameplayStateMachine.AddState(new LoseState(serviceContainer.Get<IButtonContainerService>()));
            gameplayStateMachine.AddState(new GameOverState());
            gameplayStateMachine.AddState(new RestartState());
            gameplayStateMachine.AddState(new PrepareState(serviceContainer.Get<IButtonContainerService>()));
            
            serviceContainer.SetService<IGameplayStateMachine, GameplayStateMachine>(gameplayStateMachine);
            gameplayStateMachine.Enter<PlayState>();
        }
    }
}