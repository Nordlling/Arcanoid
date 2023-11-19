using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Difficulty;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class DifficultyInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private DifficultyConfig _difficultyConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterDifficultyService(serviceContainer);
        }
        
        private void RegisterDifficultyService(ServiceContainer serviceContainer)
        {
            DifficultyService difficultyService = new DifficultyService(_difficultyConfig);

            serviceContainer.SetService<IDifficultyService, DifficultyService>(difficultyService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(difficultyService);
        }
    }
}