using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.UI.Animations
{
    public class TransformAnimations
    {
        private readonly Dictionary<Transform, Tweener> _tweeners = new();
        
        public async UniTask MoveTo(Transform currentObject, Vector3 targetPosition, float animationDuration, bool wait = true)
        {
            if (_tweeners.TryGetValue(currentObject, out Tweener tweener))
            {
                tweener.Kill();
            }
            _tweeners[currentObject] = currentObject.DOMove(targetPosition, animationDuration);
            if (!wait)
            {
                return;
            }
            await _tweeners[currentObject].Play();
        }
        
        public async UniTask LocalMoveTo(Transform currentObject, Vector3 targetPosition, float animationDuration, bool wait = true)
        {
            if (_tweeners.TryGetValue(currentObject, out Tweener tweener))
            {
                tweener.Kill();
            }
            _tweeners[currentObject] = currentObject.DOLocalMove(targetPosition, animationDuration);
            if (!wait)
            {
                return;
            }
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
        
        public void EndlessRotateTo(Transform currentObject, float speed)
        {
            if (_tweeners.TryGetValue(currentObject, out Tweener tweener))
            {
                tweener.timeScale = speed;
                return;
            }
            
            _tweeners[currentObject] = currentObject
                .DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);

            _tweeners[currentObject].timeScale = speed;
        }

    }
}