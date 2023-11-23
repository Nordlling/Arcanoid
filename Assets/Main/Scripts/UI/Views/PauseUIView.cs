using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
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
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        
        private IGameStateMachine _gameStateMachine;
        private IEnergyService _energyService;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _energyBarUIView.Construct(_energyService);
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            _continueButton.onClick.AddListener(ContinueGame);
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ExitGame);
            _energyBarUIView.OnOpen();
            _energyBarUIView.RefreshEnergy();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _continueButton.onClick.RemoveListener(ContinueGame);
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ExitGame);
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
            gamePlayStateMachine.Enter<RestartState>();
            Close();
            await Task.Yield();
            gamePlayStateMachine.Enter<PrePlayState>();
        }

        private async void ContinueGame()
        {
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            Close();
            await Task.Yield();
            gamePlayStateMachine.EnterPreviousState();
        }
    }
}