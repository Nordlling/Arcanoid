using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI;

namespace Main.Scripts.Infrastructure.Installers
{
    public class StateMachinesInstaller : MonoInstaller
    {
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameplayStateMachine(serviceContainer);
        }

        private void RegisterGameplayStateMachine(ServiceContainer serviceContainer)
        {
            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();

            gameplayStateMachine.AddState(new PrepareState());
            gameplayStateMachine.AddState(new PrePlayState());
            gameplayStateMachine.AddState(new PlayState());
            gameplayStateMachine.AddState(new PauseState());
            gameplayStateMachine.AddState(new LoseState());
            gameplayStateMachine.AddState(new GameOverState());
            gameplayStateMachine.AddState(new RestartState());
            gameplayStateMachine.AddState(new WinState());
            
            serviceContainer.SetService<IGameplayStateMachine, GameplayStateMachine>(gameplayStateMachine);
        }
        
    }
}