using System;
using System.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public class WinService : IWinable, IRestartable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly Vector2 _effectSpawnPosition;
        private int _currentDuration;

        private readonly Effect _effect;
        private readonly WinConfig _winConfig;

        public event Action<bool> OnRaycastSwitched;

        public WinService(ITimeProvider timeProvider, IEffectFactory effectFactory, SpawnContext spawnContext, WinConfig winConfig)
        {
            _winConfig = winConfig;
            _timeProvider = timeProvider;

            _currentDuration = _winConfig.Duration;

            _effect = effectFactory.Spawn(spawnContext);
            _effect.EnableEffect(_winConfig.EffectKey, false);
            _effect.gameObject.SetActive(false);
        }
        
        public async Task Win()
        {
            _effect.gameObject.SetActive(true);
            OnRaycastSwitched?.Invoke(false);
            while (_currentDuration > 0)
            {
                await Task.Delay(_winConfig.Delay);
                _currentDuration -= _winConfig.Delay;
                _timeProvider.SlowTime(_currentDuration / (float)_winConfig.Duration);
            }

            _currentDuration = _winConfig.Duration;
            OnRaycastSwitched?.Invoke(true);
        }

        public Task Restart()
        {
            _effect.gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}