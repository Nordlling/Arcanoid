using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Platforms;
using Main.Scripts.Logic.Zones;
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
            RegisterBallContainer(serviceContainer);
            RegisterBallBoundsChecker(serviceContainer);
            RegisterBallSpeedSystem(serviceContainer);
            RegisterFireballSystem(serviceContainer);
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

        private void RegisterBallContainer(ServiceContainer serviceContainer)
        {
            BallContainer ballContainer = new BallContainer(
                serviceContainer.Get<Platform>().PlatformMovement,
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
            
            serviceContainer.SetServiceSelf(ballBoundsChecker);
            serviceContainer.SetService<ITickable, BallBoundsChecker>(ballBoundsChecker);
        }
        
        private void RegisterBallSpeedSystem(ServiceContainer serviceContainer)
        {
            BallSpeedSystem ballSpeedSystem = new BallSpeedSystem(
                serviceContainer.Get<IDifficultyService>(),
                serviceContainer.Get<ITimeProvider>());
            
            serviceContainer.SetService<IBallSpeedSystem, BallSpeedSystem>(ballSpeedSystem);
            serviceContainer.SetService<ITickable, BallSpeedSystem>(ballSpeedSystem);
            
            SetGameplayStates(serviceContainer, ballSpeedSystem);
        }
        
        private void RegisterFireballSystem(ServiceContainer serviceContainer)
        {
            FireballSystem fireballSystem = new FireballSystem(
                serviceContainer.Get<ITimeProvider>(),
                serviceContainer.Get<IGameGridService>(),
                serviceContainer.Get<IBallContainer>());
            
            serviceContainer.SetService<IFireballSystem, FireballSystem>(fireballSystem);
            serviceContainer.SetService<ITickable, FireballSystem>(fireballSystem);
            
            SetGameplayStates(serviceContainer, fireballSystem);
        }
        
        private void SetGameplayStates(ServiceContainer serviceContainer, IGameplayStatable gameplayStatable)
        {
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameplayStatable);
        }
    }
}