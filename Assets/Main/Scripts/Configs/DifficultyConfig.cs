using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Configs/Difficulty")]
    public class DifficultyConfig : ScriptableObject
    {
        [Range(1f, 30f)]
        public float StartBallSpeed = 4f;
        [Range(1f, 30f)]
        public float FinishBallSpeed = 10f;

        [Range(1f, 30f)]
        public float MinBallSpeed = 1f;
        [Range(1f, 30f)]
        public float MaxBallSpeed = 11f;
    }
}