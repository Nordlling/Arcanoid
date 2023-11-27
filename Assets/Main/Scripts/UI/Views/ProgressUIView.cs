using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.UI.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class ProgressUIView : MonoBehaviour, IInitializable, ITickable, IRestartable
    {
        [SerializeField] private RunningCounterAnimation _runningCounterAnimation;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private string _packProgressText;
        [SerializeField] private Image _packImage;
        [SerializeField] private TextMeshProUGUI _levelProgressValue;

        private IPackService _packService;
        private IGameGridService _gameGridService;
        private ITimeProvider _timeProvider;

        public void Construct(IPackService packService, IGameGridService gameGridService, ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _packService = packService;
            _gameGridService = gameGridService;
            
            RefreshPackProgress();
            
            Subscribe();
        }

        public void Init()
        {
            RefreshLevelProgress();
        }

        public void Tick()
        {
            _runningCounterAnimation.UpdateTime(_timeProvider.Stopped ? 0f : 1f);
        }

        public Task Restart()
        {
            RefreshPackProgress();
            RefreshLevelProgress();
            return Task.CompletedTask;
        }

        private void Subscribe()
        {
            _gameGridService.OnDestroyed += RefreshLevelProgress;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _gameGridService.OnDestroyed -= RefreshLevelProgress;
        }

        private void RefreshPackProgress()
        {
            int currentLevelNumber = _packService.PackProgresses[_packService.SelectedPackIndex].CurrentLevelIndex + 1;
            int allLevelsCount = _packService.PackInfos[_packService.SelectedPackIndex].LevelsCount;
            _packProgressValue.text = $"{currentLevelNumber}/{allLevelsCount}";

            _packImage.sprite = _packService.PackInfos[_packService.SelectedPackIndex].MapImage;
        }

        private void RefreshLevelProgress()
        {
            if (_gameGridService.AllBlocksToWin == 0)
            {
                _levelProgressValue.text = $"0{_packProgressText}";
            }
            float levelProgressValue = (float)_gameGridService.DestroyedBlocksToWin / _gameGridService.AllBlocksToWin;
            int levelProgressPercents = (int)(100 * levelProgressValue);
            _runningCounterAnimation.Play(_levelProgressValue, levelProgressPercents, _packProgressText);
        }
    }
}