using System.Collections.Generic;
using Main.Scripts.Infrastructure;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class PackSelectUIView : UIView
    {
        [SerializeField] private Button _backButton;
        
        [Header("Scene Names")]
        [SerializeField] private string _gameplaySceneName;
        [SerializeField] private string _initialSceneName;
        
        [SerializeField] private PackButton _packButtonPrefab;
        [SerializeField] private GameObject _contentGroup;
        
        private IPackService _packService;
        private readonly List<PackButton> _buttons = new();

        public void Construct(IPackService packService)
        {
            _packService = packService;
        }

        private void Start()
        {
            _packService = ProjectContext.Instance.ServiceContainer.Get<IPackService>();
            CreateButtons();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
        }

        private void CreateButtons()
        {
            ClearAllChildren();
            for (int i = 0; i < _packService.PackInfos.Count; i++)
            {
                PackButton packButton = Instantiate(_packButtonPrefab, _contentGroup.transform);
                packButton.Construct(_packService, i);
                packButton.OnPressed += OpenPackSelect;
                _buttons.Add(packButton);
            }
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