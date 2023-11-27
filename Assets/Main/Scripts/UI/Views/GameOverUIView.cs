using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.States;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameOverUIView : UIView
    {
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _menuSceneName;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _lastTryButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        [SerializeField] private TextMeshProUGUI _lastTryCostValue;

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
            _restartButton.onClick.AddListener(RestartGame);
            _lastTryButton.onClick.AddListener(LastTry);
            _menuButton.onClick.AddListener(ExitGame);
            _energyBarUIView.OnOpen();
            _lastTryCostValue.text = _energyService.EnergyForLastTry.ToString();
            _energyBarUIView.RefreshEnergy();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _restartButton.onClick.RemoveListener(RestartGame);
            _lastTryButton.onClick.RemoveListener(LastTry);
            _menuButton.onClick.RemoveListener(ExitGame);
            _energyBarUIView.OnClose();
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
            await  gamePlayStateMachine.Enter<PrePlayState>();
        }

        private async void LastTry()
        {
            if (!_energyService.TryWasteEnergy(_energyService.EnergyForLastTry))
            {
                _energyBarUIView.Focus();
                return;
            }
            
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            Close();
            await Task.Yield();
            await gamePlayStateMachine.Enter<PrePlayState>();
        }

        private async void ExitGame()
        {
            await _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
            Close();
        }
    }
}