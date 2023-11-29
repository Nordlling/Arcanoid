using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Animations;
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
        [SerializeField] private float _lastTryPause;

        [Header("Animation")]
        [SerializeField] private Vector3 _spawnOffset;
        [SerializeField] private float _animationDuration;

        private IGameStateMachine _gameStateMachine;
        private IEnergyService _energyService;
        private ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;

        private Vector3 _originalPosition;
        private float _originalPositionY;
        private Vector3 _spawnPosition;

        private TransformAnimations _transformAnimations = new();

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _comprehensiveRaycastBlocker = _serviceContainer.Get<ComprehensiveRaycastBlocker>();
            _energyBarUIView.Construct(_energyService);
            _originalPosition = transform.position;
        }
        
        protected override async void OnOpen()
        {
            base.OnOpen();
            _restartButton.onClick.AddListener(RestartGame);
            _lastTryButton.onClick.AddListener(LastTry);
            _menuButton.onClick.AddListener(ExitGame);
            _energyBarUIView.OnOpen();
            _energyBarUIView.RefreshEnergy();
            
            _lastTryCostValue.text = _energyService.EnergyForLastTry.ToString();
            
            await PlayShowAnimation();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _restartButton.onClick.RemoveListener(RestartGame);
            _lastTryButton.onClick.RemoveListener(LastTry);
            _menuButton.onClick.RemoveListener(ExitGame);
            _energyBarUIView.OnClose();
        }

        private async UniTask PlayShowAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            
            _spawnPosition = _originalPosition + _spawnOffset;
            transform.position = _spawnPosition;
            await _transformAnimations.MoveTo(transform, _originalPosition, _animationDuration);
            
            _comprehensiveRaycastBlocker.Disable();
        }
        
        private async UniTask PlayHideAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            await _transformAnimations.MoveTo(transform, _spawnPosition, _animationDuration);
            _comprehensiveRaycastBlocker.Disable();
        }

        private async void RestartGame()
        {
            if (!_energyService.TryWasteEnergy(_energyService.EnergyForPlay))
            {
                _energyBarUIView.Focus();
                return;
            }

            await PlayHideAnimation();
            Close();
            await _serviceContainer.Get<IGameplayStateMachine>().Enter<RestartState>();
        }

        private async void LastTry()
        {
            if (!_energyService.TryWasteEnergy(_energyService.EnergyForLastTry))
            {
                _energyBarUIView.Focus();
                return;
            }
            
            _comprehensiveRaycastBlocker.Enable();
            await Task.Delay((int)(_lastTryPause * 1000));
            await PlayHideAnimation();
            Close();
            
            await _serviceContainer.Get<IGameplayStateMachine>().Enter<PrePlayState>();
        }

        private async void ExitGame()
        {
            await _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
            Close();
        }
    }
}