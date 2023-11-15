using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameplayStateInitializer : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            InitGameplayState(serviceContainer);
        }

        private void InitGameplayState(ServiceContainer serviceContainer)
        {
            serviceContainer.Get<IGameplayStateMachine>()?.Enter<PrePlayState>();
        }
        
    }
}