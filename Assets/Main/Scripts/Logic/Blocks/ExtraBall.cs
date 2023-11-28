using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class ExtraBall : MonoBehaviour, ICollisionInteractable, ITriggerInteractable
    {
        private Block _block;
        private FireballConfig _fireballConfig;
        private IExtraBallSystem _extraBallSystem;
        private IEffectFactory _effectFactory;
        private string _effectKey;
        
        public void Construct(
            Block block,
            IExtraBallSystem extraBallSystem, 
            IEffectFactory effectFactory,
            string effectKey)
        {
            _block = block;
            _extraBallSystem = extraBallSystem;
            _effectFactory = effectFactory;
            _effectKey = effectKey;
        }

        public void Interact()
        {
            Vector2 spawnPosition = transform.position;
            _extraBallSystem.ActivateExtraBallBoost(spawnPosition);
            _effectFactory.SpawnAndEnable(spawnPosition, _effectKey);
            _block.Destroy();
        }
    }
}