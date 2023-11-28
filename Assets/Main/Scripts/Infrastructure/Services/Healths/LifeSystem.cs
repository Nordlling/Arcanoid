using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public class LifeSystem : ILifeSystem
    {
        private readonly IHealthService _healthService;
        private readonly IEffectFactory _effectFactory;

        public LifeSystem(IHealthService healthService, IEffectFactory effectFactory)
        {
            _healthService = healthService;
            _effectFactory = effectFactory;
        }

        public void ActivateLifeBoost(LifeConfig lifeConfig, Vector2 position)
        {
            if (_healthService.TryChangeHealth(lifeConfig.Count, false))
            {
                _effectFactory.SpawnAndEnable(position, lifeConfig.UsageEffectKey);
            }
        }
        
    }
}