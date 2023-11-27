using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameplayUIView : MonoBehaviour
    {
        public GraphicRaycaster GraphicRaycaster => _graphicRaycaster;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        private IGameplayStateMachine _gameplayStateMachine;
        private WinService _winService;

        public void Construct(IGameplayStateMachine gameplayStateMachine, WinService winService)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _winService = winService;
            _winService.OnRaycastSwitched += SwitchRaycast;
            
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseGame);
            _winService.OnRaycastSwitched -= SwitchRaycast;
        }

        private void SwitchRaycast(bool active)
        {
            _graphicRaycaster.enabled = active;
        }

        private void PauseGame()
        {
            _gameplayStateMachine.Enter<PauseState>();
        }
    }
}