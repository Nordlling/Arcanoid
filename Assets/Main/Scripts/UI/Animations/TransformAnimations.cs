using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.UI.Animations
{
    public class TransformAnimations
    {
        private readonly Dictionary<Transform, Tweener> _tweeners = new();
        
        public async UniTask MoveTo(Transform currentObject, Vector3 targetPosition, float animationDuration)
        {
            if (_tweeners.TryGetValue(currentObject, out Tweener tweener))
            {
                tweener.Kill();
            }
            _tweeners[currentObject] = currentObject.DOMove(targetPosition, animationDuration);
            await _tweeners[currentObject].Play();
        }
        
        public async UniTask ScaleTo(Transform currentObject, Vector3 targetScale, float animationDuration)
        {
            if (_tweeners.TryGetValue(currentObject, out Tweener tweener))
            {
                tweener.Kill();
            }
            _tweeners[currentObject] = currentObject.DOScale(targetScale, animationDuration);
            await _tweeners[currentObject].Play();
        }

    }
}