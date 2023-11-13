using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Loading
{
    public class UICurtainView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private float fadeOutDuration = 1f;

        
        public void FadeInBackground(Action onFinished)
        {
            // _buttonContainerService.DisableAllButtons();
            float targetAlpha = 1f;

            _loadingText.DOFade(targetAlpha, fadeInDuration);
            _backgroundImage.DOFade(targetAlpha, fadeInDuration).OnKill(() =>
            {
                // _buttonContainerService.EnableAllButtons();
                onFinished?.Invoke();
            });
        }
        
        public void FadeOutBackground(Action onFinished)
        {
            // _buttonContainerService.DisableAllButtons();
            float targetAlpha = 0f;

            _loadingText.DOFade(targetAlpha, fadeOutDuration);
            _backgroundImage.DOFade(targetAlpha, fadeOutDuration).OnKill(() =>
            {
                // _buttonContainerService.EnableAllButtons();
                onFinished?.Invoke();
            });
        }
    }
}