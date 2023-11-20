using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Configs/Difficulty")]
    public class DifficultyConfig : ScriptableObject
    {
        [Range(1f, 30f)]
        public float BallSpeedInit = 4f;
        [Range(1f, 30f)]
        public float BallMaxSpeed = 10f;
    }
}