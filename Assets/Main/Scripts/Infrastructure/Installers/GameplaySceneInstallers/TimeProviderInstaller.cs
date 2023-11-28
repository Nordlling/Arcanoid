using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class TimeProviderInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
        }

        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            TimeProvider timeProvider = new TimeProvider();
            serviceContainer.SetService<ITimeProvider, TimeProvider>(timeProvider);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(timeProvider);
        }
    }
}