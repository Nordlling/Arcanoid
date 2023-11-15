using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class InitialUIView : UIView
    {
        [SerializeField] private Button _packSelectButton;
        [SerializeField] private string _packSelectSceneName;
        
        private void OnEnable()
        {
            _packSelectButton.onClick.AddListener(OpenPackSelect);
        }

        private void OnDisable()
        {
            _packSelectButton.onClick.RemoveListener(OpenPackSelect);
        }

        private void OpenPackSelect()
        {
            Close();
            _gameStateMachine.Enter<LoadSceneState, string>(_packSelectSceneName);
        }
    }
}