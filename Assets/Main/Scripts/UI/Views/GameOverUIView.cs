using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
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

        private void RestartGame()
        {
            _gameplayStateMachine.Enter<RestartState>();
            Close();
        }
    }
}