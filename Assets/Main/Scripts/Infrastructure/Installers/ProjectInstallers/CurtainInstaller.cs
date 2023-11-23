using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class CurtainInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private CurtainUIView _curtainUIViewPrefab;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterCurtain(serviceContainer);
        }
        
        private void RegisterCurtain(ServiceContainer serviceContainer)
        {
            CurtainUIView curtainUIView = Instantiate(_curtainUIViewPrefab);
            curtainUIView.Construct(serviceContainer.Get<Camera>());
            
            serviceContainer.SetServiceSelf(curtainUIView);
            
            DontDestroyOnLoad(curtainUIView);
        }
    }
}