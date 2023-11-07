using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Loading;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private UICurtainView _curtainView;
        private IGameStateMachine _gameStateMachine;
        private void Awake()
        {
            ServiceContainer serviceContainer = new ServiceContainer(ProjectContext.Instance.GetServices());
            SceneLoader sceneLoader = new SceneLoader(this);
            
            InitGameStateMachine(serviceContainer, sceneLoader);
            _gameStateMachine.Enter<BootstrapState>();
        }

        private void InitGameStateMachine(ServiceContainer serviceContainer, SceneLoader sceneLoader)
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.AddState(new BootstrapState(sceneLoader, serviceContainer));
            _gameStateMachine.AddState(new LoadSceneState(sceneLoader, _sceneContext, serviceContainer, _curtainView));
            _gameStateMachine.AddState(new GameLoopState());
        }
    }
}