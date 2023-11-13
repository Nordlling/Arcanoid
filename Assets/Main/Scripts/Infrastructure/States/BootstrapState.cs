using Main.Scripts.UI;
using UnityEngine.SceneManagement;
using ServiceContainer = Main.Scripts.Infrastructure.Services.ServiceContainer;

namespace Main.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _serviceContainer;

        private string _sceneName;

        public GameStateMachine StateMachine { get; set; }
        
        public BootstrapState(SceneLoader sceneLoader, ServiceContainer serviceContainer)
        {
            _sceneLoader = sceneLoader;
            _serviceContainer = serviceContainer;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneName = _sceneName = SceneManager.GetActiveScene().name;
            _sceneLoader.Load(_sceneName, onLoaded: EnterLoadScene);
        }

        private void RegisterServices()
        {
            _serviceContainer.SetService<IGameStateMachine, GameStateMachine>(StateMachine);
            _serviceContainer.Get<IWindowsManager>().SetServiceContainer(_serviceContainer);
        }

        public void Exit()
        {
        }

        private void EnterLoadScene()
        {
            StateMachine.Enter<LoadSceneState, string>(_sceneName);
        }
    }
}