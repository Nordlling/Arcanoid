using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PauseUIView : UIView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        [SerializeField] private string _menuSceneName;
        
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueGame);
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ExitGame);
        }

        private void ExitGame()
        {
            Close();
            _gameStateMachine.Enter<LoadSceneState, string>(_menuSceneName);
        }

        private async void RestartGame()
        {
            _gameplayStateMachine.Enter<RestartState>();
            Close();
            await Task.Yield();
            _gameplayStateMachine.Enter<PrePlayState>();
        }

        private async void ContinueGame()
        {
            Close();
            await Task.Yield();
            _gameplayStateMachine.EnterPreviousState();
        }
    }
}