using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlatformConfig", menuName = "Configs/Platform")]
    public class PlatformConfig : ScriptableObject
    {
        public float MovingSpeed = 6f;
        public float DecelerationSpeed = 0.25f;
        public float MinDistanceToMove = 0.05f;
    }
}