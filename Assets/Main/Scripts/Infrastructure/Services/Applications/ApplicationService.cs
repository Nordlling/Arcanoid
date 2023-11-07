using System;
using DG.Tweening;
using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Applications
{
    public class ApplicationService : MonoBehaviour, IApplicationService
    {
        private ApplicationConfig _applicationConfig;

        public event Action OnPaused;
        public event Action OnSaved;

        public void Construct(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        private void OnEnable()
        {
            Application.focusChanged += FocusChanged;
        }

        private void OnDisable()
        {
            Application.focusChanged -= FocusChanged;
        }

        private void Start()
        {
            Application.targetFrameRate = _applicationConfig.TargetFPS;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            DOTween.SetTweensCapacity(500, 300);
        }


        private void FocusChanged(bool isFocused)
        {
            if (!isFocused)
            {
#if !UNITY_EDITOR
                OnSaved?.Invoke();
#endif
            }
        }
    }
}