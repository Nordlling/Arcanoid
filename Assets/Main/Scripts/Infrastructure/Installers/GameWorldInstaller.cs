using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Bounds;
using Main.Scripts.Logic.GameGrid;
using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private HealthConfig _healthConfig;
        
        [Header("Prefabs")] 
        
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;
        [SerializeField] private PlatformMovement _platform;
        [SerializeField] private Bounder _bounder;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterHealthService(serviceContainer);
            RegisterBallCollisionService(serviceContainer);
            RegisterPlatform(serviceContainer);
            
            RegisterBallKeeper(serviceContainer);
            RegisterBallManager(serviceContainer);

            InitBounder();
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            TimeProvider timeProvider = new TimeProvider();
            
            serviceContainer.SetService<ITimeProvider, TimeProvider>(timeProvider);
            
            SetGameplayStates(serviceContainer, timeProvider);
        }

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            HealthService healthService = new HealthService(serviceContainer.Get<IGameplayStateMachine>(), _healthConfig);

            serviceContainer.SetService<IHealthService, HealthService>(healthService);
            
            SetGameplayStates(serviceContainer, healthService);
        }
        
        private void RegisterBallCollisionService(ServiceContainer serviceContainer)
        {
            BallCollisionService ballCollisionService = new BallCollisionService();
            serviceContainer.SetService<IBallCollisionService, BallCollisionService>(ballCollisionService);
        }

        private void RegisterPlatform(ServiceContainer serviceContainer)
        {
            _platform.Construct
                (
                    serviceContainer.Get<ZonesManager>(), 
                    _camera, 
                    serviceContainer.Get<ITimeProvider>()
                );
            
            serviceContainer.SetServiceSelf(_platform);
            
            SetGameplayStates(serviceContainer, _platform);
            
        }

        private void RegisterBallKeeper(ServiceContainer serviceContainer)
        {
            BallKeeper ballKeeper = new BallKeeper(
                serviceContainer.Get<ZonesManager>(),
                _camera,
                serviceContainer.Get<ITimeProvider>(),
                serviceContainer.Get<IGameplayStateMachine>());
            
            serviceContainer.SetServiceSelf(ballKeeper);
            serviceContainer.SetService<ITickable, BallKeeper>(ballKeeper);
            
            SetGameplayStates(serviceContainer, ballKeeper);
        }
        private void RegisterBallManager(ServiceContainer serviceContainer)
        {
            BallManager ballManager = new BallManager(
                serviceContainer.Get<PlatformMovement>(),
                serviceContainer.Get<IBallFactory>(),
                serviceContainer.Get<BallKeeper>());
            
            serviceContainer.SetService<IBallManager, BallManager>(ballManager);
            
            SetGameplayStates(serviceContainer, ballManager);
        }

        private void InitBounder()
        {
            _bounder.Init();
        }
        
        private void SetGameplayStates(ServiceContainer serviceContainer, IGameplayStatable gameplayStatable)
        {
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameplayStatable);
        }
    }
}