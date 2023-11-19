using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.UI.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class ProgressUIView : MonoBehaviour, IRestartable
    {
        [SerializeField] private RunningCounterAnimation _runningCounterAnimation;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private string _packProgressText;
        [SerializeField] private Image _packImage;
        [SerializeField] private TextMeshProUGUI _levelProgressValue;

        private IPackService _packService;
        private IGameGridService _gameGridService;

        public void Construct(IPackService packService, IGameGridService gameGridService, ITimeProvider timeProvider)
        {
            _packService = packService;
            _gameGridService = gameGridService;
            
            _runningCounterAnimation.Construct(timeProvider);
            
            RefreshPackProgress();
            RefreshLevelProgress();
        }

        public void Restart()
        {
            RefreshPackProgress();
            RefreshLevelProgress();
        }

        private void OnEnable()
        {
            _gameGridService.OnDestroyed += RefreshLevelProgress;
        }

        private void OnDisable()
        {
            _gameGridService.OnDestroyed -= RefreshLevelProgress;
        }

        private void RefreshPackProgress()
        {
            int currentLevelNumber = _packService.PackProgresses[_packService.SelectedPackIndex].CurrentLevelIndex + 1;
            int allLevelsCount = _packService.PackInfos[_packService.SelectedPackIndex].Levels.Count;
            _packProgressValue.text = $"{currentLevelNumber}/{allLevelsCount}";

            _packImage.sprite = _packService.PackInfos[_packService.SelectedPackIndex].MapImage;
        }

        private void RefreshLevelProgress()
        {
            float levelProgressValue = (float)_gameGridService.DestroyedBlocksToWin / _gameGridService.AllBlocksToWin;
            int levelProgressPercents = (int)(100 * levelProgressValue);
            _runningCounterAnimation.Play(_levelProgressValue, _packProgressText, levelProgressPercents);
        }
    }
}