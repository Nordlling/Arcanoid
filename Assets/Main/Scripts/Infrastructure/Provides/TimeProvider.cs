using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public class TimeProvider : ITimeProvider, IPauseable, IGameOverable, IWinable, IPrePlayable, IPlayable, IRestartable
    {
        private float _timeScale = 1f;
        private float _cachedTimeScale = 1f;
        
        
        public bool Stopped => _timeScale == 0f;
        
        public float DeltaTime => Time.deltaTime * _timeScale;
        public float TimeScale => Time.timeScale * _timeScale;

        public void StopTime()
        {
            _cachedTimeScale = _timeScale;
            _timeScale = 0f;
        }

        public void SlowTime(float timeScale)
        {
            _cachedTimeScale = _timeScale;
            _timeScale = timeScale;
        }

        public void TurnBackTime()
        {
            _timeScale = _cachedTimeScale;
        }

        public void SetRealTime()
        {
            _timeScale = 1f;
            _cachedTimeScale = _timeScale;
        }

        public Task Pause()
        {
            StopTime();
            return Task.CompletedTask;
        }

        public Task UnPause()
        {
            TurnBackTime();
            return Task.CompletedTask;
        }

        public Task GameOver()
        {
            StopTime();
            return Task.CompletedTask;
        }

        public Task Win()
        {
            StopTime();
            return Task.CompletedTask;
        }

        public Task PrePlay()
        {
            SetRealTime();
            return Task.CompletedTask;
        }

        public Task Play()
        {
            TurnBackTime();
            return Task.CompletedTask;
        }

        public Task Restart()
        {
            StopTime();
            return Task.CompletedTask;
        }
    }
}