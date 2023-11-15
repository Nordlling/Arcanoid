using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.States;
using UnityEngine;

namespace Main.Scripts.UI
{
    public class UIView : MonoBehaviour
    {
        protected IWindowsManager _windowsManager;
        protected IGameStateMachine _gameStateMachine;
        protected IGameplayStateMachine _gameplayStateMachine;
        public PanelMessage PanelMessage { get; set; }

        public void Construct(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }

        public void SetStateMachines(IGameStateMachine gameStateMachine, IGameplayStateMachine gameplayStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameplayStateMachine = gameplayStateMachine; 
        }
        
        public void Open()
        {
            gameObject.SetActive(true);
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}