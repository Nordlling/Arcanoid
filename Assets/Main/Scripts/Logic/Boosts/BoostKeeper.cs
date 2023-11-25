using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Boosts
{
    public class BoostKeeper : MonoBehaviour, ICollisionInteractable
    {
        private Block _block;
        private IBoostContainer _boostContainer;
        private string _boostId;
        private IEffectFactory _effectFactory;
        private string _effectKey;
       
        private readonly SpawnContext _spawnContext = new();

        public void Construct(Block block, IBoostContainer boostContainer, IEffectFactory effectFactory, string boostId, string effectKey)
        {
            _block = block;
            _boostContainer = boostContainer;
            _effectFactory = effectFactory;
            _boostId = boostId;
            _effectKey = effectKey;
        }

        public void Interact()
        {
            _boostContainer.CreateBoost(_boostId, transform.position);
            CreateEffect();
            _block.Destroy();
        }

        private void CreateEffect()
        {
            _spawnContext.Position = transform.position;
            Effect effect = _effectFactory.Spawn(_spawnContext);
            effect.EnableEffect(_effectKey);
        }
    }
}