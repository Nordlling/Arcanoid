using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI.Views;

namespace Main.Scripts.Infrastructure.States
{
    public class LoadSceneState : IParametrizedState<ServiceContainer, SceneContext>
    {

        public GameStateMachine StateMachine { get; set; }

        public async Task Enter(ServiceContainer serviceContainer, SceneContext sceneContext)
        {
            TaskCompletionSource<bool> tcs = new();
            sceneContext.Setup(serviceContainer, tcs);
            await tcs.Task;
            await StateMachine.Enter<GameLoopState, ServiceContainer>(serviceContainer);
            await serviceContainer.Get<CurtainUIView>().Disable();
            serviceContainer.Get<CurtainUIView>().gameObject.SetActive(false);
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}