using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.Starters
{
    public class GameplaySceneStarter : ISceneStarter
    {
        public void StartScene(ServiceContainer serviceContainer)
        {
            serviceContainer.Get<IGameplayStateMachine>()?.Enter<PrePlayState>();
        }

    }
}