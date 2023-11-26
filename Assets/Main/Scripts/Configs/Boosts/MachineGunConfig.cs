using UnityEngine;

namespace Main.Scripts.Configs.Boosts
{
    [CreateAssetMenu(fileName = "MachineGunConfig", menuName = "Configs/Boosts/MachineGun")]
    public class MachineGunConfig : ScriptableObject
    {
        public float Duration;
        public float BulletSpeed;
        public float Interval;
        public int Damage;
    }
    
}