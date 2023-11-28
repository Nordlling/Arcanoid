using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "LifeConfig", menuName = "Configs/Boosts/Life")]
    public class LifeConfig : ScriptableObject
    {
        public int Count;
        public string UsageEffectKey;
    }
}