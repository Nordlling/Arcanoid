using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Platforms.PlatformSystems;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms.PlatformBoosts
{
    public class PlatformBoostSpeed : MonoBehaviour, ITriggerInteractable
    {
        private Boost _boost;
        private SpeedPlatformConfig _speedPlatformConfig;
        private ISpeedPlatformSystem _speedPlatformSystem;

        public void Construct(Boost boost, SpeedPlatformConfig speedPlatformConfig, ISpeedPlatformSystem speedPlatformSystem)
        {
            _boost = boost;
            _speedPlatformConfig = speedPlatformConfig;
            _speedPlatformSystem = speedPlatformSystem;
        }

        public void Interact()
        {
            _speedPlatformSystem.ActivateSpeedBoost(_speedPlatformConfig, _boost.ID);
            _boost.Destroy();
        }
    }
}