using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Logic.Explosions;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class ExplosionInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterExplosionSystem(serviceContainer);
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