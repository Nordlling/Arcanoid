using System;
using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Localization
{
	public class LocalizationManager : ILocalizationManager
	{
		private readonly string _defaultLanguage;
		private readonly ILocalizationParser _localizationParser;
		private readonly LocalizationConfig _localizationConfig;
		private Dictionary<string, Dictionary<string, string>> _wordDictionary = new();

		public event Action LocalizationChanged;
		public string CurrentLanguage { get; private set; }
		public string[] AllLanguages { get; private set; }

		public LocalizationManager(ILocalizationParser localizationParser, LocalizationConfig localizationConfig)
		{
			_localizationParser = localizationParser;
			_localizationConfig = localizationConfig;
			CurrentLanguage = localizationConfig.defaultLanguage.ToString();
			ReadLocalization();
			_defaultLanguage = CurrentLanguage;
			AllLanguages = _wordDictionary.Keys.ToArray();
		}

		public void ChangeLanguage(SystemLanguage language)
		{
			CurrentLanguage = language.ToString();
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