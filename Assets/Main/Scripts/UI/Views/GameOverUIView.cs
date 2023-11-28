using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] private float _lastTryPause;
        [SerializeField] private Vector3 _spawnOffset;
        [SerializeField] private float _animationDuration;
        [SerializeField] private GameObject _localRaycastBlocker;

        private IGameStateMachine _gameStateMachine;
        private IEnergyService _energyService;

        private Vector3 _originalPosition;
        private float _originalPositionY;
        private Vector3 _spawnPosition;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _energyBarUIView.Construct(_energyService);
            _originalPosition = transform.position;
        }
        
        protected override async void OnOpen()
        {
            base.OnOpen();
            await PlayShowAnimation();
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

        private async UniTask PlayShowAnimation()
        {
            _localRaycastBlocker.SetActive(true);
            _spawnPosition = transform.position;
            _spawnPosition += _spawnOffset;
            transform.position = _spawnPosition;
            
            await transform.DOMoveY(_originalPosition.y, _animationDuration);
            _localRaycastBlocker.SetActive(false);
        }
        
        private async UniTask PlayHideAnimation()
        {
            _localRaycastBlocker.SetActive(true);
            await transform.DOMoveY(_spawnPosition.y, _animationDuration);
            _localRaycastBlocker.SetActive(false);
        }

        private async void RestartGame()
        {
            if (!_energyService.TryWasteEnergy(_energyService.EnergyForPlay))
            {
                _energyBarUIView.Focus();
                return;
            }

            await PlayHideAnimation();
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            await gamePlayStateMachine.Enter<RestartState>();
            Close();
            await UniTask.Yield();
        }

        private async void LastTry()
        {
            if (!_energyService.TryWasteEnergy(_energyService.EnergyForLastTry))
            {
                _energyBarUIView.Focus();
                return;
            }
            
            _localRaycastBlocker.SetActive(true);
            await Task.Delay((int)(_lastTryPause * 1000));
            await PlayHideAnimation();
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            Close();
            await Task.Yield();
            await gamePlayStateMachine.Enter<PrePlayState>();
        }

        private async void ExitGame()
        {
            _localRaycastBlocker.SetActive(true);
            await _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
            Close();
        }
    }
}