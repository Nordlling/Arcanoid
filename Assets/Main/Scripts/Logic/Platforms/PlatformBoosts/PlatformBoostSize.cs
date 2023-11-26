using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Platforms.PlatformSystems;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms.PlatformBoosts
{
    public class PlatformBoostSize : MonoBehaviour, ITriggerInteractable
    {
        private Boost _boost;
        private SizePlatformConfig _sizePlatformConfig;
        private ISizePlatformSystem _sizePlatformSystem;

        public void Construct(Boost boost, SizePlatformConfig sizePlatformConfig, ISizePlatformSystem sizePlatformSystem)
        {
            _boost = boost;
            _sizePlatformConfig = sizePlatformConfig;
            _sizePlatformSystem = sizePlatformSystem;
        }

        public void Interact()
        {
            _sizePlatformSystem.ActivateSizeBoost(_sizePlatformConfig);
            _boost.Destroy();
        }
    }
}