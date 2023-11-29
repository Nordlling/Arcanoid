using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Main.Scripts.Infrastructure.Services.Packs;
using TMPro;
using Main.Scripts.UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class WinAnimation : MonoBehaviour
    {
        [Header("Show Move")]
        [SerializeField] private Transform _popUp;
        [SerializeField] private Transform _winText;
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        [SerializeField] private Vector3 _spawnOffset = new(0f, 7f, 0f);
        [SerializeField] private float _showElementDuration = 1f;
        [SerializeField] private Vector3 _bounceOffset = new(0f, -0.2f, 0f);
        [SerializeField] private float _bounceDuration = 0.1f;
        
        [Header("Energy")]
        [SerializeField] private float _energyDuration = 2f;
        
        [Header("Hide Move")]
        [SerializeField] private float _hideDuration = 0.6f;
        [SerializeField] private float _moveIntervals = 0.1f;

        [Header("Rays")]
        [SerializeField] private Transform _rays;
        [SerializeField] private float _commonSpeed = 0.3f;
        [SerializeField] private float _packChangeSpeed = 1f;
        
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private Transform _packProgress;
        [SerializeField] private float _changeLevelDuration;

        [Header("Pack")]
        [SerializeField] private float _flashDuration = 0.5f;
        [SerializeField] private float _pauseBeforeFlash = 0.5f;
        [SerializeField] private Image _mapImage;
        [SerializeField] private Image _mapStrokeImage;
        
        [Header("Button")]
        [SerializeField] private CanvasGroup _buttonGroup;
        [SerializeField] private float _buttonShowDuration = 0.8f;
        
        
        private IPackService _packService;
        private ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;
        
        private Vector3 _popUpOriginalPosition;
        private Vector3 _popUpSpawnPosition;
        
        private Vector3 _winTextOriginalPosition;
        private Vector3 _winTextSpawnPosition;
        
        private Vector3 _energyOriginalPosition;
        private Vector3 _energySpawnPosition;

        private readonly TransformAnimations _transformAnimations = new();
        private readonly ImageAnimations _imageAnimations = new();

        public void Construct(ComprehensiveRaycastBlocker comprehensiveRaycastBlocker, IPackService packService)
        {
            _comprehensiveRaycastBlocker = comprehensiveRaycastBlocker;
            _packService = packService;

            Init();
        }

        public async UniTask PlayShowAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            
            InitAnimationElements();
            AnimateRays();
            await AnimateShowMove();
            _energyBarUIView.RefreshEnergyWithAnimations();
            await Task.Delay((int)(_energyDuration * 1000));
            await AnimateLevelUp();
            await TryAnimatePackUp();
            await AnimateButton();

            _comprehensiveRaycastBlocker.Disable();
        }

        public async UniTask PlayHideAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            
            await AnimateHideMove();

            _comprehensiveRaycastBlocker.Disable();
        }

        private void Init()
        {
            _popUpOriginalPosition = _popUp.position;
            _winTextOriginalPosition = _winText.position;
            _energyOriginalPosition = _energyBarUIView.transform.position;
        }

        private void InitAnimationElements()
        {
            PackInfo wonPack = _packService.PackInfos[_packService.WonPackIndex];
            
            _buttonGroup.alpha = 0f;
            
            _popUpSpawnPosition = _popUpOriginalPosition + _spawnOffset;
            _winTextSpawnPosition = _winTextOriginalPosition + _spawnOffset;
            _energySpawnPosition = _energyOriginalPosition + _spawnOffset;

            _popUp.position = _popUpSpawnPosition;
            _winText.position = _winTextSpawnPosition;
            _energyBarUIView.transform.position = _energySpawnPosition;
            
            string currentLevelIndex = _packService.WonLevelIndex.ToString();
            string allLevels = wonPack.LevelsCount.ToString();
            
            _packProgressValue.text =$"{currentLevelIndex}/{allLevels}";
            _mapImage.sprite = wonPack.MapImage;
            _mapStrokeImage.color = wonPack.ButtonColor;
        }

        private void AnimateRays()
        {
            _transformAnimations.EndlessRotateTo(_rays, _commonSpeed);
        }

        private async Task AnimateShowMove()
        {
            await _transformAnimations.MoveTo(_popUp, _popUpOriginalPosition, _showElementDuration);
            await _transformAnimations.MoveTo(_winText, _winTextOriginalPosition + _bounceOffset, _showElementDuration);
            await _transformAnimations.MoveTo(_winText, _winTextOriginalPosition, _bounceDuration);
            await _transformAnimations.MoveTo(_energyBarUIView.transform, _energyOriginalPosition + _bounceOffset, _showElementDuration);
            await _transformAnimations.MoveTo(_energyBarUIView.transform, _energyOriginalPosition, _bounceDuration);
        }

        private async Task AnimateButton()
        {
            await _imageAnimations.FadeDo(_buttonGroup, 1f, _buttonShowDuration);
        }

        private async Task AnimateLevelUp()
        {
            await _transformAnimations.ScaleTo(_packProgress, new Vector3(0f, 1f, 1f), _changeLevelDuration);
            string currentLevelIndex = (_packService.WonLevelIndex + 1).ToString();
            string allLevels = _packService.PackInfos[_packService.WonPackIndex].LevelsCount.ToString();
            _packProgressValue.text =$"{currentLevelIndex}/{allLevels}";
            await _transformAnimations.ScaleTo(_packProgress, Vector3.one, _changeLevelDuration);
        }

        private async UniTask TryAnimatePackUp()
        {
            PackProgress packProgress = _packService.PackProgresses[_packService.SelectedPackIndex];
            PackInfo packInfo = _packService.PackInfos[_packService.SelectedPackIndex];
            
            if (_packService.SelectedPackIndex == _packService.WonPackIndex || packProgress.IsPassed)
            {
                return;
            }

            _transformAnimations.EndlessRotateTo(_rays, _packChangeSpeed);
            await Task.Delay((int)(_pauseBeforeFlash * 1000));
            await _imageAnimations.FadeDo(_comprehensiveRaycastBlocker.Image, 1f, _flashDuration);

            string nextPackPassLevelsCount = packProgress.IsPassed
                ? packInfo.LevelsCount.ToString()
                : packProgress.CurrentLevelIndex.ToString();
            string allLevels = packInfo.LevelsCount.ToString();

            _mapImage.sprite = packInfo.MapImage;
            _mapStrokeImage.color = packInfo.ButtonColor;
            _packProgressValue.text = $"{nextPackPassLevelsCount}/{allLevels}";

            await _imageAnimations.FadeDo(_comprehensiveRaycastBlocker.Image, 0f, _flashDuration);
            _transformAnimations.EndlessRotateTo(_rays, _commonSpeed);

        }

        private async Task AnimateHideMove()
        {
            await _transformAnimations.MoveTo(_energyBarUIView.transform, _energySpawnPosition, _hideDuration, false);
            await Task.Delay((int)(_moveIntervals * 1000));
            await _transformAnimations.MoveTo(_winText, _energySpawnPosition, _hideDuration, false);
            await Task.Delay((int)(_moveIntervals * 1000));
            await _transformAnimations.MoveTo(_popUp, _energySpawnPosition, _hideDuration);
        }
    }
}