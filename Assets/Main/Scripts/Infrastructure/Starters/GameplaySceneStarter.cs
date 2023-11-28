using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.Starters
{
    public class GameplaySceneStarter : ISceneStarter
    {
        public async void StartScene(ServiceContainer serviceContainer)
        {
            await serviceContainer.Get<IGameplayStateMachine>().Enter<PrepareState>();
        }

    }
}