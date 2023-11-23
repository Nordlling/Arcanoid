using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Starters;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class StarterInstaller : MonoInstaller
    {
        [SerializeReference] private ISceneStarter _sceneStarter;
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterSceneStarter(serviceContainer);
        }
        
        private void RegisterSceneStarter(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(_sceneStarter);
        }
        
    }
}