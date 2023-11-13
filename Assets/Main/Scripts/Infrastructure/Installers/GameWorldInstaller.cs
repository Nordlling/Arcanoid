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
using Main.Scripts.UI;
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
            RegisterGameplayStateMachine(serviceContainer);
            RegisterTimeProvider(serviceContainer);
            RegisterHealthService(serviceContainer);

            RegisterBallCollisionService(serviceContainer);

            InitPlatform(serviceContainer);
            InitBounder();
        }

        private void RegisterGameplayStateMachine(ServiceContainer serviceContainer)
        {
            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();

            gameplayStateMachine.AddState(new PrepareState(serviceContainer.Get<IWindowsManager>()));
            gameplayStateMachine.AddState(new PlayState());
            gameplayStateMachine.AddState(new PauseState(serviceContainer.Get<IWindowsManager>()));
            gameplayStateMachine.AddState(new LoseState());
            gameplayStateMachine.AddState(new GameOverState(serviceContainer.Get<IWindowsManager>()));
            gameplayStateMachine.AddState(new RestartState());
            
            serviceContainer.SetService<IGameplayStateMachine, GameplayStateMachine>(gameplayStateMachine);
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            TimeProvider timeProvider = new TimeProvider();
            
            serviceContainer.SetService<ITimeProvider, TimeProvider>(timeProvider);
            
            SetGameplayStates(serviceContainer, timeProvider);
        }

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            HealthService healthService = new HealthService(
                serviceContainer.Get<IBallFactory>(),
                serviceContainer.Get<IGameplayStateMachine>(),
                _platform,
                _healthConfig);

            serviceContainer.SetService<IHealthService, HealthService>(healthService);
        }
        

        private void RegisterBallCollisionService(ServiceContainer serviceContainer)
        {
            BallCollisionService ballCollisionService = new BallCollisionService();
            serviceContainer.SetService<IBallCollisionService, BallCollisionService>(ballCollisionService);
        }
        

        private void InitPlatform(ServiceContainer serviceContainer)
        {
            SpawnContext spawnContext = new SpawnContext { Parent = _platform.transform };
            Ball ball = serviceContainer.Get<IBallFactory>().Spawn(spawnContext);
            _platform.Construct(serviceContainer.Get<ZonesManager>(), ball.BallMovement, _camera, serviceContainer.Get<ITimeProvider>());
            serviceContainer.SetServiceSelf(_platform);
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