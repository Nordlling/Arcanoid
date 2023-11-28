using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PauseUIView : UIView
    {
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _menuSceneName;
        
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _skipButton;
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        
        private IGameStateMachine _gameStateMachine;
        private IEnergyService _energyService;
        private ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _comprehensiveRaycastBlocker = _serviceContainer.Get<ComprehensiveRaycastBlocker>();
            _energyBarUIView.Construct(_energyService);
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            _continueButton.onClick.AddListener(ContinueGame);
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ExitGame);
            _skipButton.onClick.AddListener(SkipLevel);
            _energyBarUIView.OnOpen();
            _energyBarUIView.RefreshEnergy();
        }

        protected override void OnClose()
        {
            base.OnClose();
            _continueButton.onClick.RemoveListener(ContinueGame);
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ExitGame);
            _skipButton.onClick.RemoveListener(SkipLevel);
            _energyBarUIView.OnClose();
        }

        private async void ExitGame()
        {
            await _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
            Close();
        }

        private async void RestartGame()
        {
            if (!_energyService.TryWasteEnergy(_energyService.EnergyForPlay))
            {
                _energyBarUIView.Focus();
                return;
            }
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            await gamePlayStateMachine.Enter<RestartState>();
            Close();
            await Task.Yield();
            await gamePlayStateMachine.Enter<PrePlayState>();
        }

        private async void ContinueGame()
        {
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            Close();
            await Task.Yield();
            await gamePlayStateMachine.EnterPreviousState();
        }

        private async void SkipLevel()
        {
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            IGameGridService gameGridService = _serviceContainer.Get<IGameGridService>();
            Close();
            _comprehensiveRaycastBlocker.Enable();
            await Task.Yield();
            await gamePlayStateMachine.EnterPreviousState();
            await gameGridService.KillAllWinnableBlocks(2);
        }
    }
}