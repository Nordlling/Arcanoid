using Main.Scripts.Factory;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _fireballSpriteRenderer;
        [SerializeField] private Sprite _fireballSprite;
        [SerializeField] private string _fireballEffectKey;
        private IEffectFactory _effectFactory;
        private readonly SpawnContext _spawnContext = new();

        private Effect _currentEffect;

        public void Construct(IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _fireballSpriteRenderer.sprite = null;
            
            if (string.IsNullOrEmpty(_fireballEffectKey))
            {
                return;
            }
            _spawnContext.Position = transform.position;
            _currentEffect = _effectFactory.Spawn(_spawnContext);
            _currentEffect.EnableEffect(_fireballEffectKey);
            _currentEffect.gameObject.SetActive(false);
        }

        public void EnableVisual()
        {
            _fireballSpriteRenderer.sprite = _fireballSprite;
            if (_currentEffect is not null)
            {
                _currentEffect.gameObject.SetActive(true);
            }
        }
        
        public void DisableVisual()
        {
            _fireballSpriteRenderer.sprite = null;
            
            if (_currentEffect is not null)
            {
                _currentEffect.gameObject.SetActive(false);
            }
        }

        public void Despawn()
        {
            _fireballSpriteRenderer.sprite = null;
            if (_currentEffect is null)
            {
                return;
            }
            _currentEffect.DisableEffect();
            _currentEffect = null;
        }

        private void Update()
        {
            _currentEffect.transform.position = transform.position;
        }
        
    }
}