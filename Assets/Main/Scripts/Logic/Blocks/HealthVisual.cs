using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class HealthVisual : MonoBehaviour
    {
        private IEffectFactory _effectFactory;
        private SpriteRenderer _breakSpriteRenderer;
        private BlockHealthVisualConfig _blockHealthVisualConfig;
        
        private int _breakSpriteIndex;
        private string _damageEffectKey;
        private string _destroyEffectKey;
        private readonly SpawnContext _spawnContext = new();

        public void Construct(
            IEffectFactory effectFactory,
            SpriteRenderer breakSpriteRenderer, 
            BlockHealthVisualConfig blockHealthVisualConfig)
        {
            _effectFactory = effectFactory;
            _breakSpriteRenderer = breakSpriteRenderer;
            _blockHealthVisualConfig = blockHealthVisualConfig;
            
            _breakSpriteIndex = -1;
            _breakSpriteRenderer.sprite = null;
        }

        public void RefreshDamageView(int count)
        {
            _breakSpriteIndex += count;
            if (_breakSpriteIndex >= _blockHealthVisualConfig.HealthSprites.Length)
            {
                _breakSpriteIndex = _blockHealthVisualConfig.HealthSprites.Length - 1;
            }
            _breakSpriteRenderer.sprite = _blockHealthVisualConfig.HealthSprites[_breakSpriteIndex];
            
            CreateEffect(_blockHealthVisualConfig.DamageEffectKey);
        }
        
        public void RefreshDieView()
        {
            CreateEffect(_blockHealthVisualConfig.DestroyEffectKey);
        }

        private void CreateEffect(string effectKey)
        {
            if (string.IsNullOrEmpty(effectKey))
            {
                return;
            }
            _spawnContext.Position = transform.position;
            Effect effect = _effectFactory.Spawn(_spawnContext);
            effect.EnableEffect(effectKey, true);
        }
    }
}