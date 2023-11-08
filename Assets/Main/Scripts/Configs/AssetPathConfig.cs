using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "AssetPathConfig", menuName = "Configs/AssetPath")]
    public class AssetPathConfig : ScriptableObject
    {
        public string LevelPacksPath;
    }
}