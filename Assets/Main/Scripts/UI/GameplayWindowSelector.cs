using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.UI.Views;

namespace Main.Scripts.UI
{
    public class GameplayWindowSelector : IGameplayWindowSelector, IPauseable, IGameOverable, IWinable, IPrePlayable
    {
       
        private readonly IWindowsManager _windowsManager;
        private readonly GameplayWindowsKeysInfo _gameplayWindowsKeysInfo;

        public GameplayWindowSelector(IWindowsManager windowsManager, GameplayWindowsKeysInfo gameplayWindowsKeysInfo)
        {
            _windowsManager = windowsManager;
            _gameplayWindowsKeysInfo = gameplayWindowsKeysInfo;
        }
        
        public void Pause()
        {
            TryOpenWindow<PauseUIView>(_gameplayWindowsKeysInfo.PauseKey, _gameplayWindowsKeysInfo.DefaultKey);
        }

        public void UnPause()
        {
            TryCloseWindow<PauseUIView>(_gameplayWindowsKeysInfo.PauseKey, _gameplayWindowsKeysInfo.DefaultKey);
        }

        public void GameOver()
        {
            TryOpenWindow<GameOverUIView>(_gameplayWindowsKeysInfo.GameOverKey, _gameplayWindowsKeysInfo.DefaultKey);
        }

        public void Win()
        {
            TryOpenWindow<WinUIView>(_gameplayWindowsKeysInfo.WinKey, _gameplayWindowsKeysInfo.DefaultKey);
        }

        public void PrePlay()
        {
            TryCloseWindow<WinUIView>(_gameplayWindowsKeysInfo.WinKey, _gameplayWindowsKeysInfo.DefaultKey);
            TryCloseWindow<PauseUIView>(_gameplayWindowsKeysInfo.PauseKey, _gameplayWindowsKeysInfo.DefaultKey);
            TryCloseWindow<GameOverUIView>(_gameplayWindowsKeysInfo.GameOverKey, _gameplayWindowsKeysInfo.DefaultKey);
        }

        private void TryCloseWindow<T>(string key, string defaultKey) where T : UIView
        {
            if (key == defaultKey)
            {
                _windowsManager.GetWindow<T>().Close();
            }
            else
            {
                _windowsManager.GetWindow(key).Close();
            }
        }

        private void TryOpenWindow<T>(string key, string defaultKey) where T : UIView
        {
            if (key == defaultKey)
            {
                _windowsManager.GetWindow<T>().Open();
            }
            else
            {
                _windowsManager.GetWindow(key).Open();
            }
        }
    }

    public interface IGameplayWindowSelector
    {
    }
}