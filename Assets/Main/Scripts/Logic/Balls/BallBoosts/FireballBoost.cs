using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Boosts;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallBoosts
{
    public class FireballBoost : MonoBehaviour, ITriggerInteractable
    {
        private IFireballSystem _fireballSystem;
        private FireballConfig _fireballConfig;
        private Boost _boost;

        public void Construct(Boost boost, FireballConfig fireballConfig, IFireballSystem fireballSystem)
        {
            _boost = boost;
            _fireballSystem = fireballSystem;
            _fireballConfig = fireballConfig;
        }
        
        public void Interact()
        {
            _fireballSystem.ActivateFireballBoost(_fireballConfig);
            _boost.Destroy();
        }
    }
}