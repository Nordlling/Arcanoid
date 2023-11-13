using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameOverUIView : UIView
    {
        [SerializeField] private Button _restartButton;
        
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
        }

        private async void RestartGame()
        {
            _gameplayStateMachine.Enter<RestartState>();
            Close();
            await Task.Yield();
            _gameplayStateMachine.Enter<PrePlayState>();
        }
    }
}