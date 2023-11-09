using System;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "TiledBlockConfig", menuName = "Configs/TiledBlock")]
    public class TiledBlockConfig : ScriptableObject
    {
        public BlockInfo[] BlockInfos;
    }

    [Serializable]
    public class BlockInfo
    {
        public int BlockID;
        public int ClassID;
        public SpawnableItemMono BlockPrefab;
        public Sprite Visual;
        public int HealthCount;
    }
}