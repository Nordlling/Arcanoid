using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PackSelectUIView : UIView
    {
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _gameplaySceneName;
        [ValueDropdown("GetSceneNames")]
        [SerializeField] private string _initialSceneName;
        
        [SerializeField] private Button _backButton;

        [SerializeField] private PackButton _packButtonPrefab;
        [SerializeField] private int _minButtonsCount;
        [SerializeField] private GameObject _contentGroup;
        private PackButton _lastOpenedButton;

        private readonly List<PackButton> _buttons = new();

        private IGameStateMachine _gameStateMachine;
        private IPackService _packService;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _gameStateMachine = _serviceContainer.Get<IGameStateMachine>();
            _packService = _serviceContainer.Get<IPackService>();
            InitButtons();
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            FindLastOpenedButton();
            _lastOpenedButton.Focus();
            _backButton.onClick.AddListener(Back);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            _backButton.onClick.RemoveListener(Back);
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

        private void OpenPackSelect()
        {
            Close();
            _gameStateMachine.Enter<TransitSceneState, string>(_gameplaySceneName);
        }

        private void Back()
        {
            Close();
            _gameStateMachine.Enter<TransitSceneState, string>(_initialSceneName);
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