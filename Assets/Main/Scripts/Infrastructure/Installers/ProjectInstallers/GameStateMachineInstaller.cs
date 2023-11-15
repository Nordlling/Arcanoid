using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameStateMachine(serviceContainer);
        }

        private void RegisterGameStateMachine(ServiceContainer serviceContainer)
        {
            GameStateMachine gameStateMachine = CreateGameStateMachine(serviceContainer);
            serviceContainer.SetService<IGameStateMachine, GameStateMachine>(gameStateMachine);
        }

        private GameStateMachine CreateGameStateMachine(ServiceContainer serviceContainer)
        {
            SceneLoader sceneLoader = new SceneLoader();
            GameStateMachine gameStateMachine = new GameStateMachine();
            
            gameStateMachine.AddState(new LoadSceneState());
            gameStateMachine.AddState(new GameLoopState());
            gameStateMachine.AddState(new TransitSceneState(sceneLoader, serviceContainer));
           
            return gameStateMachine;
        }
    }
}