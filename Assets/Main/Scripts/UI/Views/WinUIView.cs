using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Packs;
using TMPro;
using Main.Scripts.Infrastructure.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class WinUIView : UIView
    {
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _menuSceneName;
        
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private Image _mapImage;
        
        private IGameStateMachine _gameStateMachine;
        private IPackService _packService;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _packService = _serviceContainer.Get<IPackService>();
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _nextButton.onClick.AddListener(NextGame);
            SetMapInfo();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _nextButton.onClick.RemoveListener(NextGame);
        }

        private void SetMapInfo()
        {
            string currentLevelIndex = (_packService.WonLevelIndex + 1).ToString();
            string allLevels = _packService.PackInfos[_packService.WonPackIndex].LevelsCount.ToString();
            
            _packProgressValue.text =$"{currentLevelIndex}/{allLevels}";
            _mapImage.sprite = _packService.PackInfos[_packService.WonPackIndex].MapImage;
        }

        private async void NextGame()
        {
            PackProgress packProgress = _packService.PackProgresses[_packService.WonPackIndex];
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();

            if (IsReplayablePack(packProgress) || IsLastPack(packProgress))
            {
                Close();
                _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
            }
            else
            {
                gamePlayStateMachine?.Enter<RestartState>();
                Close();
                await Task.Yield();
                gamePlayStateMachine?.Enter<PrePlayState>();
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