using System;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Localization;
using Main.Scripts.UI.Animations.Mono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Buttons
{
    public class PackButton : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _packSelectButton;
        
        [Header("Pack")]
        [SerializeField] private Image _packImage;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private TextMeshProUGUI _packProgressValueBlocked;
        [SerializeField] private LocalizedText _packNameValue;
        
        [Header("Visual")]
        [SerializeField] private GameObject _visual;
        [SerializeField] private GameObject _visualBlocked;
        
        [Header("Color")]
        [SerializeField] private Image[] _coloredImages;
        
        [Header("Animation")]
        [SerializeField] private PulseAnimation _pulseAnimation;
        [SerializeField] private Transform _glowTransformToPulse;

        private IPackService _packService;
        private int _packIndex;

        public Action OnPressed;

        public void Construct(IPackService packService, int packIndex)
        {
            _packService = packService;
            _packIndex = packIndex;
            RefreshStaticInfo(_packService.PackInfos[_packIndex]);
            RefreshDynamicInfo(_packService.PackInfos[_packIndex], _packService.PackProgresses[_packIndex]);
        }

        public void Focus()
        {
            _pulseAnimation.Play(_glowTransformToPulse);
        }
        
        private void OnEnable()
        {
            _packSelectButton.onClick.AddListener(OpenPackSelect);
            if (_packService is not null)
            {
                RefreshDynamicInfo(_packService.PackInfos[_packIndex], _packService.PackProgresses[_packIndex]);
            }
        }

        private void OpenPackSelect()
        {
            if (_packService is null || !_packService.PackProgresses[_packIndex].IsOpen)
            {
                return;
            }
            
            _packService.SelectedPackIndex = _packIndex;
            OnPressed?.Invoke();
        }

        private void OnDisable()
        {
            _packSelectButton.onClick.RemoveListener(OpenPackSelect);
        }

        private void RefreshStaticInfo(PackInfo packInfo)
        {
            for (int i = 0; i < _coloredImages.Length; i++)
            {
                _coloredImages[i].color = packInfo.ButtonColor;
            }

            _packNameValue.Localize(_packService.PackInfos[_packIndex].PackName);
            _packImage.sprite = packInfo.MapImage;
            
            _packProgressValueBlocked.text = $"0/{packInfo.LevelsCount}";
        }
        
        private void RefreshDynamicInfo(PackInfo packInfo, PackProgress packProgress)
        {
            _visual.SetActive(packProgress.IsOpen);
            _visualBlocked.SetActive(!packProgress.IsOpen);
            int passedLevelsCount = packProgress.IsPassed ? packInfo.LevelsCount : packProgress.CurrentLevelIndex;
            _packProgressValue.text = $"{passedLevelsCount}/{packInfo.LevelsCount}";
        }
    }
}