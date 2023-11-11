using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/Health")]
    public class HealthConfig : ScriptableObject
    {
        public int HealthCount = 3;
    }
}