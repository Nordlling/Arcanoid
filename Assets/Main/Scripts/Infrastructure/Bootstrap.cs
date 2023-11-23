using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;
        
        private void Awake()
        {
            ServiceContainer serviceContainer = new ServiceContainer(ProjectContext.Instance.GetServices());
            serviceContainer.Get<IWindowsManager>().SetServiceContainer(serviceContainer);
            serviceContainer.Get<IGameStateMachine>().Enter<LoadSceneState, ServiceContainer, SceneContext>(serviceContainer, _sceneContext);
        }

    }
}