using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.UI.Animations
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _pauseDuration;
        
        private readonly Dictionary<Transform, Sequence> _sequences = new();
        
        public void Play(Transform currentObject, Action onFinish = null)
        {
            Vector3 oldEulerAngles = currentObject.eulerAngles;
            
            if (_sequences.TryGetValue(currentObject, out Sequence sequence))
            {
                sequence.Kill();
                currentObject.eulerAngles = oldEulerAngles;
            }
            
            _sequences[currentObject] = DOTween.Sequence()
                .AppendInterval(_pauseDuration)
                .Append(DOTween.To(
                        () => currentObject.eulerAngles.z,
                        x => currentObject.eulerAngles = new Vector3(currentObject.eulerAngles.x, currentObject.eulerAngles.y, x), 
                        360,
                        _animationDuration)
                    .SetRelative(true))
                .OnKill(() =>
                {
                    currentObject.eulerAngles =new Vector3(currentObject.eulerAngles.x, currentObject.eulerAngles.y, 0f);
                    onFinish?.Invoke();
                });
        }

    }
}