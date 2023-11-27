using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "WinConfig", menuName = "Configs/Win")]
    public class WinConfig : ScriptableObject
    {
        public int Duration;
        public int Delay;
        public string EffectKey;
    }
}