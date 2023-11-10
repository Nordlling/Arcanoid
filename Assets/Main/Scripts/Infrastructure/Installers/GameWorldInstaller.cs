using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.ButtonContainer;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.GameGrid;
using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [Header("Configs")] 
        
        [Header("Prefabs")] 
        [SerializeField] private BallMovement _ballPrefab;
        
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;
        [SerializeField] private PlatformMovement _platform;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterGameplayStateMachine(serviceContainer);

            RegisterBallCollisionService(serviceContainer);

            InitPlatform(serviceContainer);
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            SlowedTimeProvider slowedTimeProvider = new SlowedTimeProvider();
            
            serviceContainer.SetService<ITimeProvider, SlowedTimeProvider>(slowedTimeProvider);
        }

        private static void RegisterBallCollisionService(ServiceContainer serviceContainer)
        {
            BallCollisionService ballCollisionService = new BallCollisionService();
            serviceContainer.SetService<IBallCollisionService, BallCollisionService>(ballCollisionService);
        }

        private void RegisterGameplayStateMachine(ServiceContainer serviceContainer)
        {
            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();
            
            gameplayStateMachine.AddState(new PlayState());
            gameplayStateMachine.AddState(new PauseState(serviceContainer.Get<ITimeProvider>()));
            gameplayStateMachine.AddState(new LoseState(serviceContainer.Get<IButtonContainerService>()));
            gameplayStateMachine.AddState(new GameOverState());
            gameplayStateMachine.AddState(new RestartState());
            gameplayStateMachine.AddState(new PrepareState(serviceContainer.Get<IButtonContainerService>()));
            
            serviceContainer.SetService<IGameplayStateMachine, GameplayStateMachine>(gameplayStateMachine);
        }

        private void InitPlatform(ServiceContainer serviceContainer)
        {
            BallMovement ball = Instantiate(_ballPrefab, _platform.transform);
            ball.GetComponent<CollisionDetector>().Construct(serviceContainer.Get<IBallCollisionService>());
            ball.Construct(serviceContainer.Get<ZonesManager>());
            _platform.Construct(serviceContainer.Get<ZonesManager>(), ball, _camera);
        }
    }
}