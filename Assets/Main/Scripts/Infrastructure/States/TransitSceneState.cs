using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public class TransitSceneState : IParametrizedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _serviceContainer;

        public GameStateMachine StateMachine { get; set; }

        public TransitSceneState(SceneLoader sceneLoader, ServiceContainer serviceContainer)
        {
            _sceneLoader = sceneLoader;
            _serviceContainer = serviceContainer;
        }

        public async void Enter(string sceneName)
        {
            await _sceneLoader.Load(sceneName);
        }

        public void Exit()
        {
        }
    }
}