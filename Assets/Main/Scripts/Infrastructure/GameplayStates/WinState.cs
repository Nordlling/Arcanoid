using System.Collections.Generic;
using Main.Scripts.UI;
using Main.Scripts.UI.Views;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class WinState : IGameplayState
    {
        private readonly List<IWinable> _winables = new();
        private readonly IWindowsManager _windowsManager;

        public WinState(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IWinable winable)
            {
                _winables.Add(winable); 
            }
        }

        public void Enter()
        {
            foreach (IWinable winable in _winables)
            {
                winable.Win();
            }

            _windowsManager.GetWindow<WinUIView>().Open();
        }

        public void Exit()
        {
            _windowsManager.GetWindow<WinUIView>().Close();
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IWinable : IGameplayStatable
    {
        void Win();
    }
}