using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "SizePlatformConfig", menuName = "Configs/Boosts/SizePlatform")]
    public class SizePlatformConfig : ScriptableObject
    {
        public float SizeMultiplier;
        public float Duration;
        public float ResizeSpeed;
    }
}