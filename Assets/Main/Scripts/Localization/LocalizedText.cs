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

        public void Localize(string localizationKey)
        {
            if (string.IsNullOrEmpty(localizationKey))
            {
                return;
            }
            
            Init();

            if (_localizationManager is null || _textField is null)
            {
                return;
            }

            _localizationKey = localizationKey;
            _textField.text = _localizationManager.Localize(localizationKey);
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            TryGetComponent(out _textField);
            _localizationManager = ProjectContext.Instance.ServiceContainer.Get<ILocalizationManager>();
        }

        private void OnEnable()
        {
            _localizationManager.LocalizationChanged += Localize;
            Localize();
        }

        private void Localize()
        {
            Localize(_localizationKey);
        }

        private void OnDisable()
        {
            _localizationManager.LocalizationChanged -= Localize;
        }
    }
}