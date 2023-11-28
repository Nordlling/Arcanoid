using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class FinalServiceInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private Transform _winEffectPoint;
        [SerializeField] private Transform _loseEffectPoint;
        
        [Header("Configs")]
        [SerializeField] private FinalConfig _finalConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterFinalService(serviceContainer);
        }

        private void RegisterFinalService(ServiceContainer serviceContainer)
        {
            FinalService finalService = new FinalService(
                serviceContainer.Get<ITimeProvider>(),
                serviceContainer.Get<IEffectFactory>(),
                serviceContainer.Get<ComprehensiveRaycastBlocker>(),
                serviceContainer.Get<BallBoundsChecker>(),
                _finalConfig,
                _winEffectPoint.position,
                _loseEffectPoint.position
            );
            
            serviceContainer.SetServiceSelf(finalService);
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(finalService);
        }
    }
}