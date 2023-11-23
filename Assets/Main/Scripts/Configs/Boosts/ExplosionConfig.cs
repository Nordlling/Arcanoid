using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "ExplosionConfig", menuName = "Configs/Boosts/Explosion")]
    public class ExplosionConfig : ScriptableObject
    {
        [Range(0f, 10f)]
        public int Damage;
        
        public Vector2Int[] Directions;
        
        [PropertyTooltip("Set to -1 if you want the length to be to the edges of the grid")]
        [Range(-1, 20)]
        public int Length;

        [Range(0f, 5f)]
        public float SecondsPerWave;
        
        public string ExplosionEffectKey;

    }
}