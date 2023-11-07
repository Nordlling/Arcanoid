using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI.Loading;

namespace Main.Scripts.Infrastructure.States
{
    public class LoadSceneState : IParametrizedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly SceneContext _sceneContext;
        private readonly ServiceContainer _serviceContainer;
        private readonly UICurtainView _curtainView;

        public GameStateMachine StateMachine { get; set; }

        public LoadSceneState(SceneLoader sceneLoader, SceneContext sceneContext, ServiceContainer serviceContainer, UICurtainView curtainView)
        {
            _sceneLoader = sceneLoader;
            _sceneContext = sceneContext;
            _serviceContainer = serviceContainer;
            _curtainView = curtainView;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();
            // _curtainView.Construct(_serviceContainer.Get<IButtonContainerService>());
            // _curtainView.gameObject.SetActive(true);
            // _curtainView.FadeOutBackground(() => _curtainView.gameObject.SetActive(false));
            StateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _sceneContext.Setup(_serviceContainer);
        }
    }
}