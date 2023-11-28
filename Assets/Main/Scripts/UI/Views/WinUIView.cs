using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
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
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        
        private IGameStateMachine _gameStateMachine;
        private IPackService _packService;
        private IEnergyService _energyService;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _packService = _serviceContainer.Get<IPackService>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _energyBarUIView.Construct(_energyService);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _nextButton.onClick.AddListener(NextGame);
            SetMapInfo();
            _energyBarUIView.OnOpen();
            _energyBarUIView.RefreshEnergyWithAnimations();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _nextButton.onClick.RemoveListener(NextGame);
            _energyBarUIView.OnClose();
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

            if (IsReplayablePack(packProgress) || IsLastPack(packProgress) || !_energyService.TryWasteEnergy(_energyService.EnergyForPlay))
            {
                await _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
                Close();
            }
            else
            {
                gamePlayStateMachine?.Enter<RestartState>();
                Close();
                await Task.Yield();
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