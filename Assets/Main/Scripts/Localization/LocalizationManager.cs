using System;
using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Main.Scripts.Localization
{
	public class LocalizationManager : ILocalizationManager
	{
		private readonly string _defaultLanguage;
		private readonly ILocalizationParser _localizationParser;
		private readonly ISaveLoadService _saveLoadService;
		private readonly LocalizationConfig _localizationConfig;
		private Dictionary<string, Dictionary<string, string>> _wordDictionary = new();
		private UserSettings _userSettings;

		public event Action LocalizationChanged;
		public string CurrentLanguage => _userSettings.SelectedLanguage;
		public string[] AllLanguages { get; private set; }

		public LocalizationManager(ILocalizationParser localizationParser, ISaveLoadService saveLoadService, LocalizationConfig localizationConfig)
		{
			_localizationParser = localizationParser;
			_saveLoadService = saveLoadService;
			_localizationConfig = localizationConfig;
			
			InitCurrentLanguage();
			_defaultLanguage = CurrentLanguage;
			
			ReadLocalization();
			AllLanguages = _wordDictionary.Keys.ToArray();
		}

		public void ChangeLanguage(string language)
		{
			_userSettings.SelectedLanguage = language;
			_saveLoadService.SaveUserSettings(_userSettings);
			LocalizationChanged?.Invoke();
		}

		public string Localize(string localizationKey)
		{
			if (_wordDictionary.Count > 0)
			{
				ReadLocalization();
			}

			if (!_wordDictionary.ContainsKey(CurrentLanguage))
			{
				throw new KeyNotFoundException("Language not found: " + CurrentLanguage);
			}

			if (!HasKey(localizationKey))
			{
				Debug.LogWarning($"Translation not found: {localizationKey} ({CurrentLanguage}).");

				return _wordDictionary[_defaultLanguage].ContainsKey(localizationKey) ? _wordDictionary[_defaultLanguage][localizationKey] : localizationKey;
			}

			return _wordDictionary[CurrentLanguage][localizationKey];
		}

		private void InitCurrentLanguage()
		{
			_userSettings = _saveLoadService.LoadUserSettings();

			if (_userSettings is null || string.IsNullOrEmpty(_userSettings.SelectedLanguage))
			{
				_userSettings = new UserSettings(_localizationConfig.defaultLanguage.ToString());
				_saveLoadService.SaveUserSettings(_userSettings);
			}
		}

		private void ReadLocalization()
		{
			if (_wordDictionary.Count > 0)
			{
				return;
			}

			_wordDictionary = _localizationParser.ParseLocalization(_localizationConfig.Sheets);
		}

		private bool HasKey(string localizationKey)
		{
			return _wordDictionary.Count > 0 && _wordDictionary[CurrentLanguage].ContainsKey(localizationKey) && _wordDictionary[CurrentLanguage][localizationKey] != "";
		}
	}
}