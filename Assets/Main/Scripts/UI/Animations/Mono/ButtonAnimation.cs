using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main.Scripts.UI.Animations.Mono
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Color _pressedColor;
        [SerializeField] private Vector2 _buttonPressedScale;

        private Color _originalColor;
        private Vector2 _originalScale;

        private Sequence _sequence;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            AnimateButton(_buttonPressedScale, _pressedColor);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            AnimateButton(_originalScale, _originalColor);
        }

        private void Start()
        {
            _originalColor = _image.color;
            _originalScale = _image.transform.localScale;
        }
        
        private void AnimateButton(Vector2 scaleTarget, Color endColor)
        {
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence()
                .Join(_image.transform.DOScale(scaleTarget, _animationDuration))
                .Join(_image.DOColor(endColor, _animationDuration));
        }
        
    }
}