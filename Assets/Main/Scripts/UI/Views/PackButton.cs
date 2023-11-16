using System;
using Main.Scripts.Infrastructure.Services.LevelMap;
using Main.Scripts.Infrastructure.Services.LevelMap.Parser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PackButton : MonoBehaviour
    {
        [SerializeField] private Button _packSelectButton;
        
        [Header("Pack")]
        [SerializeField] private Image[] _coloredImages;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _packProgressText;
        [SerializeField] private Image _packImage;
        [SerializeField] private GameObject _visual;
        
        [SerializeField] private TextMeshProUGUI _packProgressTextBlocked;
        [SerializeField] private GameObject _visualBlocked;
        
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
            if (!_packService.PackProgresses[_packIndex].IsOpen)
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
            ColorUtility.TryParseHtmlString(packInfo.ButtonColor, out Color currentColor);
            
            for (int i = 0; i < _coloredImages.Length; i++)
            {
                _coloredImages[i].color = currentColor;
            }

            _packNameText.text = packInfo.PackName;
            _packImage.sprite = packInfo.MapImage;
            
            _packProgressTextBlocked.text = $"0/{packInfo.LevelsCount}";
        }
        
        private void RefreshDynamicInfo(PackInfo packInfo, PackProgress packProgress)
        {
            _visual.SetActive(packProgress.IsOpen);
            _visualBlocked.SetActive(!packProgress.IsOpen);

            _packProgressText.text = $"{packProgress.CurrentLevelIndex}/{packInfo.LevelsCount}";
        }
    }
}