using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class ShakerInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;
        
        [Header("Configs")]
        [SerializeField] private ShakerConfig _shakerConfig;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterShaker(serviceContainer);
        }

        private void RegisterShaker(ServiceContainer serviceContainer)
        {
            Shaker shaker = new Shaker(_camera, _shakerConfig);
            serviceContainer.SetServiceSelf(shaker);
        }
    }
}