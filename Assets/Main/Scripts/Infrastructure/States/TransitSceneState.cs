using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI.CommonViews;

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

        public async Task Enter(string sceneName)
        {
            _serviceContainer.Get<CurtainUIView>().gameObject.SetActive(true);
            await _serviceContainer.Get<CurtainUIView>().Enable();
            await _sceneLoader.Load(sceneName);
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }
    }
}