using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class BallBoostSpeed : MonoBehaviour, ITriggerInteractable
    {
        private Boost _boost;
        private SpeedBallConfig _speedBallConfig;
        private IBallSpeedSystem _ballSpeedSystem;

        public void Construct(Boost boost, SpeedBallConfig speedBallConfig, IBallSpeedSystem ballSpeedSystem)
        {
            _boost = boost;
            _speedBallConfig = speedBallConfig;
            _ballSpeedSystem = ballSpeedSystem;
        }

        public void Interact()
        {
            _ballSpeedSystem.ActivateSpeedBoost(_speedBallConfig);
            _boost.Destroy();
        }
    }
}