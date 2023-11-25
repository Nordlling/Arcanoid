using System;
using System.Collections.Generic;
using Main.Scripts.Factory.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "TiledBlockConfig", menuName = "Configs/TiledBlock")]
    public class TiledBlockConfig : SerializedScriptableObject
    {
        public Dictionary<string, BlockInfo> BlockInfos;
        public Dictionary<string, BoostInfo> BoostInfos;
    }

    [Serializable]
    public class BlockInfo
    {
        public BasicInfo BasicInfo;
        public bool CheckToWin = true;
        [SerializeReference] public IBlockComponentFactory[] ComponentFactories;
    }
    
    [Serializable]
    public class BoostInfo
    {
        public BasicInfo BasicInfo;
        [SerializeReference] public IBoostComponentFactory[] ComponentFactories;
    }
    
    [Serializable]
    public class BasicInfo
    {
        public Sprite Visual;
    }
}