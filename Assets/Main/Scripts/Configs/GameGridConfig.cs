using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameGridConfig", menuName = "Configs/GameGrid")]
    public class GameGridConfig : ScriptableObject
    {
        public float Spacing;
        public Sprite BlockSprite;
    }
}