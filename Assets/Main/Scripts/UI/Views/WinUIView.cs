using System.Threading.Tasks;
using Main.Scripts.Infrastructure;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Packs;
using TMPro;
using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class WinUIView : UIView
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private Image _mapImage;
        [SerializeField] private string _menuSceneName;

        private IPackService _packService;

        private void Awake()
        {
            _packService = ProjectContext.Instance.ServiceContainer.Get<IPackService>();
        }

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(NextGame);
            SetMapInfo();
        }

        private void SetMapInfo()
        {
            string currentLevelIndex = (_packService.WonLevelIndex + 1).ToString();
            string allLevels = _packService.PackInfos[_packService.WonPackIndex].LevelsCount.ToString();
            
            _packProgressValue.text =$"{currentLevelIndex}/{allLevels}";
            _mapImage.sprite = _packService.PackInfos[_packService.WonPackIndex].MapImage;
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(NextGame);
        }

        private async void NextGame()
        {
            PackProgress packProgress = _packService.PackProgresses[_packService.WonPackIndex];
            if (IsReplayablePack(packProgress) || IsLastPack(packProgress))
            {
                Close();
                _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
            }
            else
            {
                _gameplayStateMachine.Enter<RestartState>();
                Close();
                await Task.Yield();
                _gameplayStateMachine.Enter<PrePlayState>();
            }
        }

        private bool IsReplayablePack(PackProgress packProgress)
        {
            return packProgress.CurrentLevelIndex == 0 && packProgress.Cycle > 1;
        }
        
        private bool IsLastPack(PackProgress packProgress)
        {
            return packProgress.CurrentLevelIndex == 0 && _packService.WonPackIndex == _packService.SelectedPackIndex;
        }
    }
}