using System;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ZonesConfig", menuName = "Configs/Zones")]
    public class ZonesConfig : ScriptableObject
    {
        public ZoneSettings GridZone;
        
        public ZoneSettings LivingZone;
        
        public ZoneSettings InputZone;
    }

    [Serializable]
    public class ZoneSettings
    {
        [Range(-1f, 1f)]
        public float UpperOffset;
        [Range(-1f, 1f)]
        public float BottomOffset;
        [Range(-1f, 1f)]
        public float LeftOffset;
        [Range(-1f, 1f)]
        public float RightOffset;
        public Color Color;
    }
}