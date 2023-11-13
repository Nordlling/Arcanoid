using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ServiceContainer _serviceContainer;

        public GameLoopState(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public GameStateMachine StateMachine { get; set; }

        public void Enter()
        {
            _serviceContainer.Get<IGameplayStateMachine>()?.Enter<PlayState>();
        }

        public void Exit()
        {
        }
    }
}