using System.Collections.Generic;
using Main.Scripts.UI;
using Main.Scripts.UI.Views;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PauseState : IGameplayState
    {
        private readonly IWindowsManager _windowsManager;
        private List<IPauseable> _pauseables = new();

        public PauseState(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPauseable pauseable)
            {
                _pauseables.Add(pauseable); 
            }
        }

        public void Enter()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                pauseable.Pause();
            }
            _windowsManager.GetWindow<PauseUIView>().Open();
            // _windowsManager.EnableRaycast();
        }

        public void Exit()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                pauseable.UnPause();
            }
            _windowsManager.GetWindow<PauseUIView>().Close();
            // _windowsManager.DisableRaycast();
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPauseable : IGameplayStatable
    {
        void Pause();
        void UnPause();
    }
}