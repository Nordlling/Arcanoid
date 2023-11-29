using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI.CommonViews;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class ComprehensiveRaycastBlockerInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterComprehensiveRaycastBlocker(serviceContainer);
        }
        
        private void RegisterComprehensiveRaycastBlocker(ServiceContainer serviceContainer)
        {
            ComprehensiveRaycastBlocker comprehensiveRaycastBlocker = Instantiate(_comprehensiveRaycastBlocker);
            serviceContainer.SetServiceSelf(comprehensiveRaycastBlocker);
            DontDestroyOnLoad(comprehensiveRaycastBlocker);
        }
    }
}