using Main.Scripts.Infrastructure;
using Main.Scripts.Localization;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Views
{
	public class LanguageSelectUIView : UIView
	{
		[SerializeField] private TMP_Dropdown _languageDropdown;
		
		private ILocalizationManager _localizationManager;

		private void Start()
		{
			_localizationManager = ProjectContext.Instance.ServiceContainer.Get<ILocalizationManager>();
			InitDropdown();
		}

		private void OnEnable()
		{
			_languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
		}

		private void OnDisable()
		{
			_languageDropdown.onValueChanged.RemoveListener(OnLanguageChanged);
		}

		private void InitDropdown()
		{
			_languageDropdown.options.Clear();
			
			for (int i = 0; i < _localizationManager.AllLanguages.Length; i++)
			{
				TMP_Dropdown.OptionData newDropDownOption = new(_localizationManager.AllLanguages[i]);
				_languageDropdown.options.Add(newDropDownOption);
				CheckForCurrentLanguage(i);
			}
		}

		private void CheckForCurrentLanguage(int dropDownIndex)
		{
			if (_localizationManager.AllLanguages[dropDownIndex] == _localizationManager.CurrentLanguage)
			{
				_languageDropdown.SetValueWithoutNotify(dropDownIndex);
			}
		}

		private void OnLanguageChanged(int value)
		{
			_localizationManager.ChangeLanguage(_localizationManager.AllLanguages[value]);
		}
	}
}