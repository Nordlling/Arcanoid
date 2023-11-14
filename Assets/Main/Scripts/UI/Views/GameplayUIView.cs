using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameplayUIView : MonoBehaviour, ILoseable, IPrePlayable
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        private IGameplayStateMachine _gameplayStateMachine;

        public void Construct(IGameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Lose()
        {
            _graphicRaycaster.enabled = false;
        }

        public void PrePlay()
        {
            _graphicRaycaster.enabled = true;
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