using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/Energy")]
    public class EnergyConfig : ScriptableObject
    {
        public int InitialEnergyCapacity = 30;
        [Range(1, 100)]
        public int EnergyWasteForPlay = 3;
        [Range(1, 100)]
        public int EnergyRewardForPass = 5;
        [Range(1, 100)]
        public int EnergyForLastTry = 6;
        [Min(0)]
        public float SecondsForRecharge = 480;
    }
}