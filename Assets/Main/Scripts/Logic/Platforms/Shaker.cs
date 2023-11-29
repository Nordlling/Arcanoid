using System.Threading.Tasks;
using DG.Tweening;
using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class Shaker
    {
        private readonly Camera _camera;
        private readonly ShakerConfig _shakerConfig;

        private bool _used;

        public Shaker(Camera camera, ShakerConfig shakerConfig)
        {
            _camera = camera;
            _shakerConfig = shakerConfig;
        }

        public async void Shake()
        {
            await Task.Delay((int)(_shakerConfig.Pause * 1000));
            
            if (_shakerConfig.EnableVibration)
            {
                Handheld.Vibrate();
            }
            
            if (_used)
            {
                return;
            }

            _used = true;
            _camera.transform
                .DOShakePosition(_shakerConfig.ShakeDuration, _shakerConfig.ShakeStrength)
                .OnComplete(() => _used = false);
        }
    }
}