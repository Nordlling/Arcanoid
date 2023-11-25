using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public class TimeProvider : ITimeProvider, IPauseable, IGameOverable, IWinable, IPrePlayable, IPlayable
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

        public void Pause()
        {
            StopTime();
        }

        public void UnPause()
        {
            TurnBackTime();
        }

        public void GameOver()
        {
            StopTime();
        }

        public void Win()
        {
            StopTime();
        }

        public void PrePlay()
        {
            SetRealTime();
        }

        public void Play()
        {
            TurnBackTime();
        }
    }
}