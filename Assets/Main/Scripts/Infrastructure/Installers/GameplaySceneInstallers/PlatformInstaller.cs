using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Platforms;
using Main.Scripts.Logic.Zones;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class PlatformInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Platform _platform;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterPlatform(serviceContainer);
            RegisterSizePlatformSystem(serviceContainer);
        }

        private void RegisterPlatform(ServiceContainer serviceContainer)
        {
            _platform.PlatformMovement.Construct
                (
                    serviceContainer.Get<ZonesManager>(), 
                    _camera, 
                    serviceContainer.Get<ITimeProvider>()
                );
            
            serviceContainer.SetServiceSelf(_platform);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(_platform.PlatformMovement);
        }
        
        private void RegisterSizePlatformSystem(ServiceContainer serviceContainer)
        {
            SizePlatformSystem sizePlatformSystem = new SizePlatformSystem(
                serviceContainer.Get<Platform>().StretchedTransform,
                serviceContainer.Get<ITimeProvider>());
            
            serviceContainer.SetService<ITickable, SizePlatformSystem>(sizePlatformSystem);
            serviceContainer.SetService<ISizePlatformSystem, SizePlatformSystem>(sizePlatformSystem);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(sizePlatformSystem);
        }
    }
}