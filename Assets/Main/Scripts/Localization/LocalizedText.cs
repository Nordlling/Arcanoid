using Main.Scripts.Infrastructure;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizationKey;
        
        private TextMeshProUGUI _textField;
        private ILocalizationManager _localizationManager;

        public void Construct(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }
        
        private void Localize()
        {
            Localize(_localizationKey);
        }

        public void Localize(string localizationKey)
        {
            if (_localizationManager is null)
            {
                InitLocalizationManager();
            }
            
            if (string.IsNullOrEmpty(localizationKey))
            {
                return;
            }

            if (_textField is null)
            {
                return;
            }

            _localizationKey = localizationKey;
            _textField.text = _localizationManager?.Localize(localizationKey);
        }

        private void Awake()
        {
            TryGetComponent(out _textField);
            InitLocalizationManager();
        }

        private void OnEnable()
        {
            _localizationManager.LocalizationChanged += Localize;
            Localize();
        }

        private void OnDisable()
        {
            _localizationManager.LocalizationChanged -= Localize;
        }

        private void InitLocalizationManager()
        {
            _localizationManager = ProjectContext.Instance.ServiceContainer.Get<ILocalizationManager>();
        }
    }
}