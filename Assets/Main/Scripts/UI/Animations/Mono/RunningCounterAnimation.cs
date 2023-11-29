using System;
using System.Collections.Generic;
using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Animations.Mono
{
    public class RunningCounterAnimation : MonoBehaviour
    {
        [SerializeField] float _animationDuration;
        [SerializeField] float _pauseDuration;
        
        private ITimeProvider _timeProvider;
        
        private int _currentScore;

        private readonly Dictionary<TextMeshProUGUI, Sequence> _sequences = new();

        public void UpdateTime(float timescale)
        {
            foreach (Sequence sequences in _sequences.Values)
            {
                sequences.timeScale = timescale;
            }
        }

        public void Play(TextMeshProUGUI counterValue, int newCounter, string tail = "", Action onFinish = null)
        {
            string counter = counterValue.text;
            if (!string.IsNullOrEmpty(tail))
            {
                counter = counter.Replace(tail, "");
            }

            if (!int.TryParse(counter, out int oldCounter))
            {
                Debug.LogWarning("'counterValue' is not integer");
                return;
            }
            
            if (_sequences.TryGetValue(counterValue, out Sequence sequence))
            {
                sequence.Kill();
                counterValue.text = oldCounter + tail;
            }

            _sequences[counterValue] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(DOTween
                    .To(() => oldCounter, x => oldCounter = x, newCounter, _animationDuration)
                    .OnUpdate(() => counterValue.text = oldCounter + tail))
                .OnKill(() =>
                {
                    counterValue.text = newCounter + tail;
                    onFinish?.Invoke();
                });
        }
    }
}