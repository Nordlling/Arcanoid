using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public class LoadSceneState : IParametrizedState<ServiceContainer, SceneContext>
    {

        public GameStateMachine StateMachine { get; set; }

        public void Enter(ServiceContainer serviceContainer, SceneContext sceneContext)
        {
            sceneContext.Setup(serviceContainer);
            StateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}