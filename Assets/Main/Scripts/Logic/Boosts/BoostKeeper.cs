using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Boosts
{
    public class BoostKeeper : MonoBehaviour, ICollisionInteractable, ITriggerInteractable
    {
        private Block _block;
        private IBoostContainer _boostContainer;
        private string _boostId;
        private IEffectFactory _effectFactory;
        private string _effectKey;
        
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
            if (_block.TryGetComponent(out Health _))
            {
                return;
            }
            
            Vector2 spawnPosition = transform.position;
            
            Boost boost = _boostContainer.CreateBoost(_boostId, spawnPosition);
            boost.transform.localScale = Vector3.Scale(boost.transform.localScale, _block.SizeRatio);
           
            _effectFactory.SpawnAndEnable(spawnPosition, _effectKey);
            
            _block.Destroy();
        }
    }
}