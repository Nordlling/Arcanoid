using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class WinUIView : UIView
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private Image _mapImage;
        
        private void OnEnable()
        {
            _nextButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(RestartGame);
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