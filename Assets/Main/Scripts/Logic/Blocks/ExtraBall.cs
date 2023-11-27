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

        private readonly SpawnContext _spawnContext = new();

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
            _extraBallSystem.ActivateExtraBallBoost(transform.position);
            CreateEffect();
            _block.Destroy();
        }
        
        private void CreateEffect()
        {
            _spawnContext.Position = transform.position;
            Effect effect = _effectFactory.Spawn(_spawnContext);
            effect.EnableEffect(_effectKey, true);
        }
    }
}