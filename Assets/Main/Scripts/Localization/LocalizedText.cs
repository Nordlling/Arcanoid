using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizationKey;
        private ILocalizationManager _localizationManager;

        public void Construct(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public void OnEnable()
        {
            _localizationManager = ServiceContainer.Instance.Get<ILocalizationManager>();
            Localize();
            _localizationManager.LocalizationChanged += Localize;
        }

        public void OnDisable()
        {
            _localizationManager.LocalizationChanged -= Localize;
        }

        void Localize()
        {
            if (_localizationKey.Equals(""))
            {
                return;
            }

            Text textComponent = GetComponent<Text>();
            TextMeshProUGUI textMeshProComponent = GetComponent<TextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.text = _localizationManager.Localize(_localizationKey);
            }

            if (textMeshProComponent != null)
            {
                textMeshProComponent.text = _localizationManager.Localize(_localizationKey);
            }
        }
    }
}