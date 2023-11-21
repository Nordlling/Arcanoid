using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Configs/Scenes")]
    public class ScenesConfig : SerializedScriptableObject
    {
        public List<string> SceneNames;
    }
}