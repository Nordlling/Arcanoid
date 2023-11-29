using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/Energy")]
    public class EnergyConfig : ScriptableObject
    {
        public int InitialEnergyCapacity = 30;
        public int MaxEnergyCapacity = 200;
        
        [Range(1, 100)]
        public int WasteForPlay = 3;
        [Range(1, 100)]
        public int WasteForLastTry = 6;
        [Range(1, 100)]
        public int RewardForPass = 5;
        [Range(1, 100)]
        public int RewardForBuy = 9;
        
        [Min(0)]
        public float SecondsForRecharge = 480;
    }
}