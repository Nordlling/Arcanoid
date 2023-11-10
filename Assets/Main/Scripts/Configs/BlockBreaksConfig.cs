using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BlockBreaksConfig", menuName = "Configs/BlockBreaks")]
    public class BlockBreaksConfig : ScriptableObject
    {
        public Sprite[] BreakSprites;
    }
}