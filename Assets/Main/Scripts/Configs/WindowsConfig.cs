using System.Collections.Generic;
using Main.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/Windows")]
    public class WindowsConfig : SerializedScriptableObject
    {
        public Dictionary<string, UIView> Windows;
    }
}