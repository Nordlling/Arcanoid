using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public class LifeBoost : MonoBehaviour, ITriggerInteractable
    {
        private Boost _boost;
        private LifeConfig _lifeConfig;
        private ILifeSystem _lifeSystem;

        public void Construct(Boost boost, LifeConfig lifeConfig, ILifeSystem lifeSystem)
        {
            _boost = boost;
            _lifeConfig = lifeConfig;
            _lifeSystem = lifeSystem;
        }

        public void Interact()
        {
            _lifeSystem.ActivateLifeBoost(_lifeConfig);
            _boost.Destroy();
        }
        
    }
}