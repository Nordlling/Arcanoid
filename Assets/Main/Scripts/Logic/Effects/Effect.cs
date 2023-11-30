using System.Linq;
using System.Threading.Tasks;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Effects
{
    public class Effect : SpawnableItemMono
    {
        private IEffectFactory _effectFactory;
        private ITimeProvider _timeProvider;
        
        [SerializeField] private EffectInfo[] _effects;

        private EffectInfo _enabledEffect;
        private float _initialSimulationSpeed;
        private bool _timeDependent = true;
        private bool _destroyOnFinish = true;

        public void Construct(IEffectFactory effectFactory, ITimeProvider timeProvider)
        {
            _effectFactory = effectFactory;
            _timeProvider = timeProvider;
        }

        public void EnableEffect(string effectKey, bool timeDependent = true, bool destroyOnFinish = true)
        {
            _timeDependent = timeDependent;
            _destroyOnFinish = destroyOnFinish;
            TryEnableEffect(effectKey);
        }

        public void DisableEffect()
        {
            if (_enabledEffect is null)
            {
                return;
            }
            
            _enabledEffect.Effect.gameObject.SetActive(false);
        }

        public void DespawnEffect()
        {
            if (_enabledEffect is null)
            {
                return;
            }
            
            _enabledEffect.Effect.gameObject.SetActive(false);
            _effectFactory.Despawn(this);
            _enabledEffect = null;
        }

        public async void EnableEffectForTime(string effectKey, float seconds)
        {
            if (!TryEnableEffect(effectKey))
            {
                return;
            }
            
            await Task.Delay((int)(seconds * 1000));

            DisableEffect();
        }

        private void Update()
        {
            if (_enabledEffect is null || !_enabledEffect.Effect.gameObject.activeSelf)
            {
                return;
            }

            if (!_enabledEffect.Effect.IsAlive())
            {
                FinishEffect();
                return;
            }
            
            if (_timeDependent)
            {
                UpdateTimeForEffects();
            }
        }

        private void FinishEffect()
        {
            if (_destroyOnFinish)
            {
                DespawnEffect();
            }
            else
            {
                DisableEffect();
            }
        }

        private void UpdateTimeForEffects()
        {
            for (int i = 0; i < _enabledEffect.Particles.Length; i++)
            {
                var effect = _enabledEffect.Particles[i].main;
                effect.simulationSpeed = _timeProvider.Stopped ? 0f : _initialSimulationSpeed;
            }
        }

        private bool TryEnableEffect(string effectKey)
        {
            EffectInfo effectInfo = _effects.FirstOrDefault(effect => effect.Key == effectKey);
            if (effectInfo == null || effectInfo.Effect == null)
            {
                Debug.Log("No found effect");
                return false;
            }

            DisableEffect();

            _enabledEffect = effectInfo;
            _initialSimulationSpeed = _enabledEffect.Effect.main.simulationSpeed;
            if (_initialSimulationSpeed == 0f)
            {
                _initialSimulationSpeed = 1f;
            }
            _enabledEffect.Effect.gameObject.SetActive(true);
            return true;
        }
        
    }
}