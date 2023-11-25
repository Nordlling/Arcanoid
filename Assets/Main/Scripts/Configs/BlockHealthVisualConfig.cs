using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BlockHealthVisualConfig", menuName = "Configs/BlockHealthVisual")]
    public class BlockHealthVisualConfig : ScriptableObject
    {
        public string DamageEffectKey;
        public string DestroyEffectKey;
        public Sprite[] HealthSprites;
    }
}