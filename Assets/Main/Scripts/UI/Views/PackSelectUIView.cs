using System.Collections.Generic;
using Main.Scripts.Infrastructure;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.States;
using UnityEngine;
using Main.Scripts.UI.Buttons;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PackSelectUIView : UIView
    {
        [SerializeField] private Button _backButton;

        [SerializeField] private PackButton _packButtonPrefab;
        [SerializeField] private int _minButtonsCount;
        [SerializeField] private GameObject _contentGroup;
        private PackButton _lastOpenedButton;

        [Header("Scene Names")]
        [SerializeField] private string _gameplaySceneName;
        [SerializeField] private string _initialSceneName;

        private IPackService _packService;
        private readonly List<PackButton> _buttons = new();

        public void Construct(IPackService packService)
        {
            _packService = packService;
        }

        private void Start()
        {
            _packService = ProjectContext.Instance.ServiceContainer.Get<IPackService>();
            InitButtons();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
        }

        private void OnEnable()
        {
            FindLastOpenedButton();
            _lastOpenedButton.Focus();
            _backButton.onClick.AddListener(Back);
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