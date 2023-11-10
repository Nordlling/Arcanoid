using System;
using System.Collections.Generic;
using Main.Scripts.Factory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "TiledBlockConfig", menuName = "Configs/TiledBlock")]
    public class TiledBlockConfig : SerializedScriptableObject
    {
        public Dictionary<string, BlockInfo> BlockInfos;
    }

    [Serializable]
    public class BlockInfo
    {
        public Sprite Visual;
        [SerializeReference] public IComponentFactory[] ComponentFactories;
    }
}