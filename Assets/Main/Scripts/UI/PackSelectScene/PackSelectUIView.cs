using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Buttons;
using Main.Scripts.UI.CommonViews;
using Main.Scripts.UI.PopUps;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.PackSelectScene
{
    public class PackSelectUIView : UIView
    {
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _gameplaySceneName;
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _initialSceneName;
        
        [Header("Buttons")]
        [SerializeField] private Button _backButton;

        [Header("Packs")]
        [SerializeField] private PackButton _packButtonPrefab;
        [SerializeField] private int _minButtonsCount;
        [SerializeField] private GameObject _contentGroup;
        
        [Header("Energy")]
        [SerializeField] private EnergyBarUIView _energyBarUIView;
        
        private PackButton _lastOpenedButton;

        private readonly List<PackButton> _buttons = new();

        private IGameStateMachine _gameStateMachine;
        private IPackService _packService;
        private IEnergyService _energyService;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _packService = _serviceContainer.Get<IPackService>();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _energyBarUIView.Construct(_energyService);
            InitButtons();
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            FindLastOpenedButton();
            _lastOpenedButton.Focus();
            _backButton.onClick.AddListener(Back);
            _energyBarUIView.OnOpen();
            _energyBarUIView.RefreshEnergyWithAnimations();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _backButton.onClick.RemoveListener(Back);
            _energyBarUIView.OnClose();
        }

        private void InitButtons()
        {
            ClearAllChildren();
            CreateButtons();
            CreateStubButtons();
        }

        private void CreateButtons()
        {
            for (int i = 0; i < _packService.PackInfos.Count; i++)
            {
                PackButton packButton = Instantiate(_packButtonPrefab, _contentGroup.transform);
                packButton.Construct(_packService, i);
                packButton.OnPressed += OpenPackSelect;
                _buttons.Add(packButton);
            }
        }

        private void CreateStubButtons()
        {
            if (_minButtonsCount <= _packService.PackInfos.Count)
            {
                return;
            }
            
            for (int i = _packService.PackInfos.Count; i < _minButtonsCount; i++)
            {
                PackButton packButton = Instantiate(_packButtonPrefab, _contentGroup.transform);
                _buttons.Add(packButton);
            }
        }

        private void FindLastOpenedButton()
        {
            for (int i = 0; i < _packService.PackProgresses.Count; i++)
            {
                if (_packService.PackProgresses[i].IsOpen)
                {
                    continue;
                }
                _lastOpenedButton = _buttons[i - 1];
                return;
            }
            _lastOpenedButton = _buttons[_packService.PackProgresses.Count - 1];
        }

        private async void OpenPackSelect()
        {
            if (!_energyService.TryWasteEnergy(_energyService.WasteForPlay))
            {
                _energyBarUIView.Focus();
                _serviceContainer.Get<IWindowsManager>().GetWindow<NoEnergyUIView>()?.Open();
                return;
            }
            
            await _gameStateMachine.Enter<TransitSceneState, string>(_gameplaySceneName);
            Close();
        }

        private async void Back()
        {
            await _gameStateMachine.Enter<TransitSceneState, string>(_initialSceneName);
            Close();
        }
        
        private void ClearAllChildren()
        {
            int childCount = _contentGroup.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = _contentGroup.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
        
    }
}