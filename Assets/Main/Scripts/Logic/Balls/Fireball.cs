using Main.Scripts.Factory;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class Fireball : MonoBehaviour
    {
        private IEffectFactory _effectFactory;
        private IBallContainer _ballContainer;
        private SpriteRenderer _spriteRenderer;
        private Sprite _fireballSprite;
        private string _fireballEffectKey;

        private Sprite _originalSprite;
        private Effect _currentEffect;
        private readonly SpawnContext _spawnContext = new();

        public void Construct(
            IEffectFactory effectFactory, 
            IBallContainer ballContainer, 
            SpriteRenderer spriteRenderer,
            Sprite fireballSprite,
            string fireballEffectKey)
        {
            _spriteRenderer = spriteRenderer;
            _ballContainer = ballContainer;
            _effectFactory = effectFactory;
            _fireballSprite = fireballSprite;
            _fireballEffectKey = fireballEffectKey;
            
            Init();
        }

        private void Init()
        {
            _originalSprite = _spriteRenderer.sprite;
            if (string.IsNullOrEmpty(_fireballEffectKey))
            {
                return;
            }

            _spawnContext.Position = transform.position;
            _currentEffect = _effectFactory.Spawn(_spawnContext);
            _currentEffect.EnableEffect(_fireballEffectKey);
            _currentEffect.gameObject.SetActive(false);

            _ballContainer.OnSwitchedFireball += SwitchFireball;
        }

        private void SwitchFireball(bool isFireball)
        {
            if (isFireball)
            {
                EnableVisual();
            }
            else
            {
                DisableVisual();
            }
        }

        public void Despawn()
        {
            _ballContainer.OnSwitchedFireball -= SwitchFireball;
            if (_currentEffect is null)
            {
                return;
            }
            _currentEffect.DespawnEffect();
            _currentEffect = null;
        }

        private void Update()
        {
            _currentEffect.transform.position = transform.position;
        }

        private void EnableVisual()
        {
            _spriteRenderer.sprite = _fireballSprite;
            if (_currentEffect is not null)
            {
                _currentEffect.gameObject.SetActive(true);
            }
        }
        
        private void DisableVisual()
        {
            _spriteRenderer.sprite = _originalSprite;
            if (_currentEffect is not null)
            {
                _currentEffect.gameObject.SetActive(false);
            }
        }
    }
}