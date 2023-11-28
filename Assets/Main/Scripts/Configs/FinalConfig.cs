using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "FinalConfig", menuName = "Configs/Final")]
    public class FinalConfig : ScriptableObject
    {
        public int Delay;
        public int WinDuration;
        public int LoseDuration;
        public string WinEffectKey;
        public string LoseEffectKey;
    }
}