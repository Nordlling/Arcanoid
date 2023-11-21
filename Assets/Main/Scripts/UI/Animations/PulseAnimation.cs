using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.UI.Animations
{
    public class PulseAnimation : MonoBehaviour
    {
        [SerializeField] private float _pauseDuration;
        
        [SerializeField] private float _animationDuration;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _elasticity;
        [SerializeField] private Vector3 _scaleMultiplier;
        [SerializeField] private int _loops;
        
        private readonly Dictionary<Transform, Sequence> _sequences = new();

        public void Play(Transform currentObject)
        {
            Vector3 punch = Vector3.Scale(currentObject.localScale, _scaleMultiplier);
            Play(currentObject, punch);
        }
        
        public void Play(Transform currentObject, float scaleMultiplier)
        {
            Vector3 punch = currentObject.localScale * scaleMultiplier;
            Play(currentObject, punch);
        }

        public void Play(Transform currentObject, Vector3 punch)
        {
            if (_sequences.TryGetValue(currentObject, out Sequence sequence))
            {
                sequence.Kill();
            }

            Vector3 originalScale = currentObject.localScale;

            _sequences[currentObject] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(currentObject.DOPunchScale(punch, _animationDuration, _vibrato, _elasticity).SetLoops(_loops).SetEase(Ease.InOutQuad))
                .OnKill(() => currentObject.localScale = originalScale);
        }
    }
}