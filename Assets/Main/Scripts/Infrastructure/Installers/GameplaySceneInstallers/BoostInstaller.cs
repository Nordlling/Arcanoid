using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Explosions;
using Main.Scripts.Logic.Zones;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class BoostInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterBoostContainer(serviceContainer);
            RegisterBoostBoundsChecker(serviceContainer);
            RegisterBoostCollisionService(serviceContainer);
            RegisterExplosionSystem(serviceContainer);
        }
        
        private void RegisterBoostContainer(ServiceContainer serviceContainer)
        {
            BoostContainer boostContainer = new BoostContainer(serviceContainer.Get<IBoostFactory>());
            
            serviceContainer.SetService<IBoostContainer, BoostContainer>(boostContainer);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(boostContainer);
        }
        
        private void RegisterBoostBoundsChecker(ServiceContainer serviceContainer)
        {
            BoostBoundsChecker boostBoundsChecker = new BoostBoundsChecker( 
                serviceContainer.Get<ZonesManager>(),
                serviceContainer.Get<IBoostContainer>());
            
            serviceContainer.SetServiceSelf(boostBoundsChecker);
            serviceContainer.SetService<ITickable, BoostBoundsChecker>(boostBoundsChecker);
        }

        private void RegisterBoostCollisionService(ServiceContainer serviceContainer)
        {
            BoostCollisionService boostCollisionService = new BoostCollisionService();
            serviceContainer.SetService<IBoostCollisionService, BoostCollisionService>(boostCollisionService);
        }
        
        private void RegisterExplosionSystem(ServiceContainer serviceContainer)
        {
            ExplosionSystem explosionSystem = new ExplosionSystem(
                serviceContainer.Get<IGameGridService>(),
                serviceContainer.Get<IEffectFactory>(),
                serviceContainer.Get<ITimeProvider>());
            
            serviceContainer.SetService<ITickable, ExplosionSystem>(explosionSystem);
            serviceContainer.SetService<IExplosionSystem, ExplosionSystem>(explosionSystem);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(explosionSystem);
        }
    }
}