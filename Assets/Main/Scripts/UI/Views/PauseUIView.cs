using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.Logic.Balls;
using Main.Scripts.UI.Animations;
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
        [SerializeField] private float _skipDuration;
        
        [Header("Animation")]
        [SerializeField] private Vector3 _showFirstStepScale;
        [SerializeField] private Vector3 _showSecondTargetScale;
        [SerializeField] private float _showFirstStepDuration;
        [SerializeField] private float _showSecondsStepDuration;
        [SerializeField] private float _hideDuration;
        

        private IGameStateMachine _gameStateMachine;
        private IEnergyService _energyService;
        private ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;

        private readonly TransformAnimations _transformAnimations = new();

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
            PlayShowAnimation();
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

        private async void PlayShowAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            transform.localScale = Vector3.zero;
            await _transformAnimations.ScaleTo(transform, _showFirstStepScale, _showFirstStepDuration);
            await _transformAnimations.ScaleTo(transform, _showSecondTargetScale, _showSecondsStepDuration);
            _comprehensiveRaycastBlocker.Disable();
        }
        
        private async Task PlayHideAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            await _transformAnimations.ScaleTo(transform, Vector3.zero, _hideDuration);
            _comprehensiveRaycastBlocker.Disable();
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
            
            await PlayHideAnimation();
            Close();
            await gamePlayStateMachine.Enter<RestartState>();
        }

        private async void ContinueGame()
        {
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            await PlayHideAnimation();
            Close();
            await gamePlayStateMachine.EnterPreviousState();
        }

        private async void SkipLevel()
        {
            await PlayHideAnimation();
            Close();
            
            _comprehensiveRaycastBlocker.Enable();
            _serviceContainer.Get<BallBoundsChecker>().Check = false;

            await _serviceContainer.Get<IGameplayStateMachine>().EnterPreviousState();
            await _serviceContainer.Get<IGameGridController>().KillAllWinnableBlocks(_skipDuration);
        }
    }
}