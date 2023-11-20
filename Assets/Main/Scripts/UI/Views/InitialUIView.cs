using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class InitialUIView : UIView
    {
        [SerializeField] private Button _packSelectButton;
        [SerializeField] private string _packSelectSceneName;
        [SerializeField] private string _gameplaySceneName;
        
        private ISaveLoadService _saveLoadService;
        private IPackService _packService;

        public void Construct(ISaveLoadService saveLoadService, IPackService packService)
        {
            _saveLoadService = saveLoadService;
            _packService = packService;
        }
        
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
            
            if (_saveLoadService.LoadIsPlayed() == 1)
            {
                _gameStateMachine.Enter<TransitSceneState, string>(_packSelectSceneName);
            }
            else
            {
                _saveLoadService.SaveIsPlayed(1);
                _packService.SelectedPackIndex = 0;
                _gameStateMachine.Enter<TransitSceneState, string>(_gameplaySceneName);
            }
        }
    }
}