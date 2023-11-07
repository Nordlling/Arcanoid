using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public class SlowedTimeProvider : ITimeProvider
    {
        private float _timeScale = 1f;
        private float _cachedTimeScale = 1f;
        
        
        public bool Stopped => _timeScale == 0f;
        
        public float GetDeltaTime()
        {
            return Time.deltaTime * _timeScale;
        }

        public float GetTimeScale()
        {
            return Time.timeScale * _timeScale;
        }

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
    }
}