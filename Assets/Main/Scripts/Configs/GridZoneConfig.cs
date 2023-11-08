using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GridZoneConfig", menuName = "Configs/GridZone")]
    public class GridZoneConfig : ScriptableObject
    {
        [Range(0f, 1f)]
        public float UpperOffset;
        
        [Range(0f, 0.5f)]
        public float SideOffset;

        public Color RectangleColor = Color.red;
    }
}