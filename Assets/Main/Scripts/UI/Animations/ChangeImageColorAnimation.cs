using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Animations
{
    public class ChangeImageColorAnimation : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _pauseDuration;
        
        private readonly Dictionary<Image, Sequence> _sequences = new();
        
        public void Play(Image image, Color endColor, Action onFinish = null)
        {
            Color oldColor = image.color;
            if (_sequences.TryGetValue(image, out Sequence sequence))
            {
                sequence.Kill();
                image.color = oldColor;
            }
            
            _sequences[image] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(image.DOColor(endColor, _animationDuration))
                .OnKill(() =>
                {
                    image.color = endColor;
                    onFinish?.Invoke();
                });
        }

    }
}