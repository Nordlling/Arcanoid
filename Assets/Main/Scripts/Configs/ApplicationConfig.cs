using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ApplicationConfig", menuName = "Configs/Application")]
    public class ApplicationConfig : ScriptableObject
    {
        public int TargetFPS = 60;
    }
}