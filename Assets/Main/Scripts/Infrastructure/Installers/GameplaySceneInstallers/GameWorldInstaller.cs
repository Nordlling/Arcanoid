using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.BoostTimers;
using Main.Scripts.Logic.Bounds;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Bounder _bounder;
        [SerializeField] private BoundsVisualizer _boundsVisualizer;
        
        [Header("Configs")]
        [SerializeField] private TiledBlockConfig _tiledBlockConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterBoostTimersService(serviceContainer);
            RegisterBounder(serviceContainer);
        }

        private void RegisterBoostTimersService(ServiceContainer serviceContainer)
        {
            BoostTimersService boostTimersService = new BoostTimersService(_tiledBlockConfig);
            serviceContainer.SetService<IBoostTimersService, BoostTimersService>(boostTimersService);
        }

        private void RegisterBounder(ServiceContainer serviceContainer)
        {
            _bounder.Init();
            _boundsVisualizer.Init();
            serviceContainer.SetService<ITickable, BoundsVisualizer>(_boundsVisualizer);
        }
    }
}