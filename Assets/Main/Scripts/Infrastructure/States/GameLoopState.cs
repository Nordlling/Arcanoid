using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Starters;

namespace Main.Scripts.Infrastructure.States
{
    public class GameLoopState : IParametrizedState<ServiceContainer>
    {

        public GameStateMachine StateMachine { get; set; }

        public Task Enter(ServiceContainer serviceContainer)
        {
            serviceContainer.Get<ISceneStarter>()?.StartScene(serviceContainer);
            return Task.CompletedTask;
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}