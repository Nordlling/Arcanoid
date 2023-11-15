using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameplayUIView : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        private IGameplayStateMachine _gameplayStateMachine;

        public void Construct(IGameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseGame);
        }

        private void PauseGame()
        {
            _gameplayStateMachine.Enter<PauseState>();
        }
    }
}