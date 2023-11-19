using System.Collections.Generic;
using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Animations
{
    public class RunningCounterAnimation : MonoBehaviour
    {
        [SerializeField] float _animationDuration;
        [SerializeField] float _pauseDuration;
        
        private ITimeProvider _timeProvider;
        
        private int _currentScore;
        
        private readonly Dictionary<TextMeshProUGUI, Sequence> _sequences = new();

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        private void Update()
        {
            foreach (Sequence sequences in _sequences.Values)
            {
                sequences.timeScale = _timeProvider.Stopped ? 0f : 1f;
            }
        }
        
        public void Play(TextMeshProUGUI counterValue, int newCounter)
        {
            Play(counterValue, "", newCounter);
        }

        public void Play(TextMeshProUGUI counterValue, string tail, int newCounter)
        {
            string counter = counterValue.text;
            if (!string.IsNullOrEmpty(tail))
            {
                counter = counter.Replace(tail, "");
            }

            if (!int.TryParse(counter, out int oldCounter))
            {
                Debug.LogWarning("'counterValue' is not integer");
            }
            
            
            if (_sequences.TryGetValue(counterValue, out Sequence sequence))
            {
                sequence.Kill();
            }

            _sequences[counterValue] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(DOTween
                    .To(() => oldCounter, x => oldCounter = x, newCounter, _animationDuration)
                    .OnUpdate(() => counterValue.text = oldCounter + tail));
        }
    }
}