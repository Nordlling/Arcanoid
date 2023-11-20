using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "ExplosionConfig", menuName = "Configs/Boosts/Explosion")]
    public class ExplosionConfig : ScriptableObject
    {
        public int Damage;
    }
}