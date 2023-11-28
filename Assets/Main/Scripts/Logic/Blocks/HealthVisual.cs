using DG.Tweening;
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
        private GameObject _visual;

        private int _breakSpriteIndex;
        private string _damageEffectKey;
        private string _destroyEffectKey;

        private Sequence _sequence;

        public void Construct(
            IEffectFactory effectFactory,
            SpriteRenderer breakSpriteRenderer, 
            BlockHealthVisualConfig blockHealthVisualConfig,
            GameObject visual)
        {
            _effectFactory = effectFactory;
            _breakSpriteRenderer = breakSpriteRenderer;
            _blockHealthVisualConfig = blockHealthVisualConfig;
            _visual = visual;

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

            _effectFactory.SpawnAndEnable(transform.position, _blockHealthVisualConfig.DamageEffectKey);
            
            PlayAnimation();
        }
        
        public void RefreshDieView()
        {
            _effectFactory.SpawnAndEnable(transform.position, _blockHealthVisualConfig.DestroyEffectKey);
        }

        private void PlayAnimation()
        {
            _sequence?.Kill();
            
            Vector3 originalScale = _visual.transform.localScale;

            _sequence = DOTween.Sequence()
                .Append(_visual.transform.DOPunchScale(originalScale * 0.2f, 0.2f, 1, 1).SetEase(Ease.InOutQuad))
                .OnKill(() => _visual.transform.localScale = originalScale);
        }
    }
}