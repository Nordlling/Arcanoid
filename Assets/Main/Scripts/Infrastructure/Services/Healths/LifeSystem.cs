using Main.Scripts.Configs.Boosts;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public class LifeSystem : ILifeSystem
    {
        private readonly IHealthService _healthService;

        public LifeSystem(IHealthService healthService)
        {
            _healthService = healthService;
        }

        public void ActivateLifeBoost(LifeConfig lifeConfig)
        {
            _healthService.ChangeHealth(lifeConfig.Count, false);
        }
        
    }
}