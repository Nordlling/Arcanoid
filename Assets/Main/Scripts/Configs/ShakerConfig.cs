using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ShakerConfig", menuName = "Configs/Shaker")]
    public class ShakerConfig : ScriptableObject
    {
        [Header("Camera")]
        public float ShakeDuration = 0.3f;
        public float ShakeStrength = 0.1f;
        public float Pause = 0.5f;

        [Header("Device")] 
        public bool EnableVibration = true;
    }
}