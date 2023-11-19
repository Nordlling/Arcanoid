using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.GameGrid;
using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class BallInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterBallCollisionService(serviceContainer);
            RegisterBallKeeper(serviceContainer);
            RegisterBallManager(serviceContainer);
            RegisterBallBoundsChecker(serviceContainer);
        }
        
        private void RegisterBallCollisionService(ServiceContainer serviceContainer)
        {
            BallCollisionService ballCollisionService = new BallCollisionService();
            serviceContainer.SetService<IBallCollisionService, BallCollisionService>(ballCollisionService);
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
            BallContainer ballContainer = new BallContainer(
                serviceContainer.Get<PlatformMovement>(),
                serviceContainer.Get<IBallFactory>(),
                serviceContainer.Get<BallKeeper>(),
                serviceContainer.Get<IHealthService>());
            
            serviceContainer.SetService<IBallContainer, BallContainer>(ballContainer);
            
            SetGameplayStates(serviceContainer, ballContainer);
        }
        
        private void RegisterBallBoundsChecker(ServiceContainer serviceContainer)
        {
            BallBoundsChecker ballBoundsChecker = new BallBoundsChecker(
                serviceContainer.Get<ZonesManager>(),
                serviceContainer.Get<IBallContainer>());
            
            serviceContainer.SetService<IBallBoundsChecker, BallBoundsChecker>(ballBoundsChecker);
            serviceContainer.SetService<ITickable, BallBoundsChecker>(ballBoundsChecker);
        }
        
        private void SetGameplayStates(ServiceContainer serviceContainer, IGameplayStatable gameplayStatable)
        {
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameplayStatable);
        }
    }
}