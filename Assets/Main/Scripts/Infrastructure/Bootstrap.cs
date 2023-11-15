using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI;
using Main.Scripts.UI.Loading;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private UICurtainView _curtainView;
        private void Awake()
        {
            ServiceContainer serviceContainer = new ServiceContainer(ProjectContext.Instance.GetServices());
            IGameStateMachine gameStateMachine = serviceContainer.Get<IGameStateMachine>();
            serviceContainer.Get<IWindowsManager>().SetServiceContainer(serviceContainer);
            gameStateMachine.Enter<LoadSceneState, ServiceContainer, SceneContext>(serviceContainer, _sceneContext);
        }

    }
}