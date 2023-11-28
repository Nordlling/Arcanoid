using System.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Effects;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public class FinalService : IWinable, ILoseable, IRestartable, IPrePlayable, IPlayable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;
        private readonly Vector2 _effectSpawnPosition;
        private int _currentDuration;

        private readonly Effect _effect;
        private readonly FinalConfig _finalConfig;
        private readonly Vector2 _winSpawnPosition;
        private readonly Vector2 _loseSpawnPosition;

        public FinalService(
            ITimeProvider timeProvider, 
            IEffectFactory effectFactory, 
            ComprehensiveRaycastBlocker comprehensiveRaycastBlocker,
            FinalConfig finalConfig, 
            Vector2 winSpawnPosition,
            Vector2 loseSpawnPosition)
        {
            _timeProvider = timeProvider;
            _comprehensiveRaycastBlocker = comprehensiveRaycastBlocker;
            _finalConfig = finalConfig;
            _winSpawnPosition = winSpawnPosition;
            _loseSpawnPosition = loseSpawnPosition;

            SpawnContext spawnContext = new();
            _effect = effectFactory.Spawn(spawnContext);
        }
        
        public async Task Win()
        {
            _currentDuration = _finalConfig.WinDuration;
            _effect.transform.position = _winSpawnPosition;
            _effect.EnableEffect(_finalConfig.WinEffectKey, false, false);
            _comprehensiveRaycastBlocker.Enable();
            while (_currentDuration > 0)
            {
                await Task.Delay(_finalConfig.Delay);
                _currentDuration -= _finalConfig.Delay;
                _timeProvider.SlowTime(_currentDuration / (float)_finalConfig.WinDuration);
            }

            _comprehensiveRaycastBlocker.Disable();
        }

        public Task Restart()
        {
            _effect.DisableEffect();
            return Task.CompletedTask;
        }

        public async Task Lose()
        {
            _effect.transform.position = _loseSpawnPosition;
            _effect.EnableEffect(_finalConfig.LoseEffectKey, false, false);
            _comprehensiveRaycastBlocker.Enable();
            await Task.Delay(_finalConfig.LoseDuration);
            _currentDuration = _finalConfig.WinDuration;
            _comprehensiveRaycastBlocker.Disable();
        }

        public Task PrePlay()
        {
            _effect.DisableEffect();
            return Task.CompletedTask;
        }

        public Task Play()
        {
            _effect.DisableEffect();
            return Task.CompletedTask;
        }
    }
}