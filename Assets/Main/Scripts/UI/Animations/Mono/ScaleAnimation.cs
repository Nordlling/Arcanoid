using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.UI.Animations.Mono
{
    public class ScaleAnimation : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _pauseDuration;
        
        private readonly Dictionary<Transform, Sequence> _sequences = new();
        private readonly Dictionary<RectTransform, Sequence> _rectSequences = new();
        
        public void Play(Transform currentObject, Vector3 targetScale)
        {
            if (_sequences.TryGetValue(currentObject, out Sequence sequence))
            {
                sequence.Kill();
            }
            
            _sequences[currentObject] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(currentObject.DOScale(targetScale, _animationDuration));
        }
        
        public void Play(RectTransform currentObject, Vector2 targetScale, Action onFinish = null)
        {
            if (_rectSequences.TryGetValue(currentObject, out Sequence sequence))
            {
                sequence.Kill();
            }
            
            _rectSequences[currentObject] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(DOTween.To(() => currentObject.sizeDelta, x => currentObject.sizeDelta = x, targetScale, 1f))
                .OnKill(() =>
                {
                    currentObject.sizeDelta = targetScale;
                    onFinish?.Invoke();
                });
        }

    }
}