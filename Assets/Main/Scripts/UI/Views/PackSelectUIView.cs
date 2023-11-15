using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PackSelectUIView : UIView
    {
        [SerializeField] private Button _packSelectButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private string _gameplaySceneName;
        [SerializeField] private string _initialSceneName;
        
        private void OnEnable()
        {
            _packSelectButton.onClick.AddListener(OpenPackSelect);
            _backButton.onClick.AddListener(Back);
        }

        private void OnDisable()
        {
            _packSelectButton.onClick.RemoveListener(OpenPackSelect);
            _backButton.onClick.RemoveListener(Back);
        }

        private void OpenPackSelect()
        {
            Close();
            _gameStateMachine.Enter<LoadSceneState, string>(_gameplaySceneName);
        }

        private void Back()
        {
            Close();
            _gameStateMachine.Enter<LoadSceneState, string>(_initialSceneName);
        }
    }
}