using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Configs/Difficulty")]
    public class DifficultyConfig : ScriptableObject
    {
        public float BallSpeedInit = 2f;
        public float BallSpeedIncrement = 0.1f;
         public float BallMaxSpeed = 8f;
    }
}