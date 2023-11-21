using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
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
        
        private IGameStateMachine _gameStateMachine;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            _continueButton.onClick.AddListener(ContinueGame);
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ExitGame);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _continueButton.onClick.RemoveListener(ContinueGame);
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ExitGame);
        }

        private void ExitGame()
        {
            Close();
            _gameStateMachine.Enter<TransitSceneState, string>(_menuSceneName);
        }

        private async void RestartGame()
        {
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