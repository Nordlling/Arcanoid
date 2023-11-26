using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "SpeedPlatformConfig", menuName = "Configs/Boosts/SpeedPlatform")]
    public class SpeedPlatformConfig : ScriptableObject
    {
        public float SpeedMultiplier;
        public float Duration;
    }
}