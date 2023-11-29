using Main.Scripts.UI.Animations;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class PlatformShaker : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
        [SerializeField] private float _shakeLength;
        [SerializeField] private float _duration;

        private bool _used;
        private readonly TransformAnimations _transformAnimations = new();
        
        public async void Shake(Vector2 direction)
        {
            if (_used)
            {
                return;
            }

            _used = true;
            
            Vector3 moveDirection = direction * _shakeLength;
            await _transformAnimations.LocalMoveTo(_visual, _visual.localPosition + moveDirection, _duration);
            await _transformAnimations.LocalMoveTo(_visual, Vector3.zero, _duration);
            
            _used = false;
        }
    }
}