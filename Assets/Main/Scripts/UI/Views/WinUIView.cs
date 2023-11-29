using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.Packs;
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
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        
        [SerializeField] private WinAnimation _winAnimation;
        
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
            _winAnimation.Construct(_serviceContainer.Get<ComprehensiveRaycastBlocker>(), _serviceContainer.Get<IPackService>());
        }

        protected override async void OnOpen()
        {
            base.OnOpen();
            _energyBarUIView.OnOpen();
            _nextButton.onClick.AddListener(NextGame);
            await _winAnimation.PlayShowAnimation();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _nextButton.onClick.RemoveListener(NextGame);
            _energyBarUIView.OnClose();
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
                await _winAnimation.PlayHideAnimation();
                Close();
                gamePlayStateMachine?.Enter<RestartState>();
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