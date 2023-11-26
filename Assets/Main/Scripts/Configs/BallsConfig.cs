using System;
using System.Collections.Generic;
using Main.Scripts.Factory.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Configs/Balls")]
    public class BallsConfig : SerializedScriptableObject
    {
        public Dictionary<string, BallInfo> BallInfos;
    }
    
    [Serializable]
    public class BallInfo
    {
        public BasicInfo BasicInfo;
        [SerializeReference] public IBallComponentFactory[] ComponentFactories;
    }
}