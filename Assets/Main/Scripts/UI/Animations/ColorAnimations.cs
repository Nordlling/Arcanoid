using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Animations
{
    public class ImageAnimations
    {
        private readonly Dictionary<Component, Tweener> _tweeners = new();
        
        public async UniTask ColorDo(Image currentImage, Color endColor, float duration)
        {
            if (_tweeners.TryGetValue(currentImage, out Tweener tweener))
            {
                tweener.Kill();
            }
            
            _tweeners[currentImage] = currentImage.DOColor(endColor, duration);
            
            await _tweeners[currentImage].Play();
        }
        
        public async UniTask FadeDo(CanvasGroup currentCanvasGroup, float endAlpha, float duration)
        {
            if (_tweeners.TryGetValue(currentCanvasGroup, out Tweener tweener))
            {
                tweener.Kill();
            }
            
            _tweeners[currentCanvasGroup] = currentCanvasGroup.DOFade(endAlpha, duration);
            
            await _tweeners[currentCanvasGroup].Play();
        }
        
        public async UniTask FadeDo(Image currentImage, float endAlpha, float duration)
        {
            if (_tweeners.TryGetValue(currentImage, out Tweener tweener))
            {
                tweener.Kill();
            }
            
            _tweeners[currentImage] = currentImage.DOFade(endAlpha, duration);
            
            await _tweeners[currentImage].Play();
        }

    }
}