using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "SpeedBallConfig", menuName = "Configs/Boosts/SpeedBall")]
    public class SpeedBallConfig : ScriptableObject
    {
        public float SpeedMultiplier;
        public float Duration;
    }
}