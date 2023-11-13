using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class GameplayUIView : MonoBehaviour, ILoseable, IGameOverable, IPlayable
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        [ValueDropdown("GetKeys")]
        [SerializeField] private string _pauseKey;
        
        [ValueDropdown("GetKeys")]
        [SerializeField] private string _loseKey;
        
        [SerializeField] private WindowsConfig _windowsConfig;

        private IWindowsManager _windowsManager;
        private IGameplayStateMachine _gameplayStateMachine;

        private List<string> windowKeys;

        public void Construct(IWindowsManager windowsManager, IGameplayStateMachine gameplayStateMachine)
        {
            _windowsManager = windowsManager;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Lose()
        {
            _graphicRaycaster.enabled = false;
        }

        public void GameOver()
        {
            // _windowsManager.GetWindow(_loseKey).Open();
        }

        public void Play()
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
            // _windowsManager.GetWindow(_pauseKey);
            
        }

        private string[] GetKeys()
        {
            return _windowsConfig.Windows.Keys.ToArray();
        }
    }
}