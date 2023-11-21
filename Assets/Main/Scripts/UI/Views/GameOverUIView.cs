using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameOverUIView : UIView
    {
        [SerializeField] private Button _restartButton;
        
        protected override void OnOpen()
        {
            base.OnOpen();
            _restartButton.onClick.AddListener(RestartGame);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _restartButton.onClick.RemoveListener(RestartGame);
        }

        private async void RestartGame()
        {
            IGameplayStateMachine gamePlayStateMachine = _serviceContainer.Get<IGameplayStateMachine>();
            gamePlayStateMachine.Enter<RestartState>();
            Close();
            await Task.Yield();
            gamePlayStateMachine.Enter<PrePlayState>();
        }
    }
}