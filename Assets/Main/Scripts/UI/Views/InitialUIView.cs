using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.Infrastructure.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class InitialUIView : UIView
    {
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _packSelectSceneName;
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _gameplaySceneName;

        [SerializeField] private Button _packSelectButton;
        [SerializeField] private LanguageSelectUIView _languageSelectUIView;
        
        private IGameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;
        private IPackService _packService;
        private IEnergyService _energyService;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _saveLoadService = _serviceContainer.Get<ISaveLoadService>();
            _packService = _serviceContainer.Get<IPackService>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _languageSelectUIView.Init();
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _packSelectButton.onClick.AddListener(OpenPackSelect);
            _languageSelectUIView.OnOpen();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _packSelectButton.onClick.RemoveListener(OpenPackSelect);
            _languageSelectUIView.OnClose();
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
                _energyService.TryWasteEnergy(_energyService.EnergyForPlay);
                _saveLoadService.SaveIsPlayed(1);
                _packService.SelectedPackIndex = 0;
                _gameStateMachine.Enter<TransitSceneState, string>(_gameplaySceneName);
            }
        }
    }
}