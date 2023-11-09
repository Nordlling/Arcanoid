using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ZonesConfig", menuName = "Configs/Zones")]
    public class ZonesConfig : ScriptableObject
    {
        [Header("Grid Zone")]
        [Range(0f, 1f)]
        public float UpperOffset;
        [Range(0f, 0.5f)]
        public float SideOffset;
        public Color GridZoneColor = Color.yellow;
        
        [Header("Living Zone")]
        [Range(0f, 10f)]
        public float LiveOffset;
        public Color LivingZoneColor = Color.red;
    }
}