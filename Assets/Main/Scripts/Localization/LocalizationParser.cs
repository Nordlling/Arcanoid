using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Main.Scripts.Localization
{
	public class LocalizationParser : ILocalizationParser
	{
        public Dictionary<string, Dictionary<string, string>> ParseLocalization(List<Sheet> sheets)
		{
			Dictionary<string, Dictionary<string, string>> wordDictionary = new();
			List<string> keys = new List<string>();

			foreach (Sheet sheet in sheets)
			{
				TextAsset textAsset = sheet.TextAsset;
				List<string> lines = GetLines(textAsset.text);
				List<string> languages = lines[0].Split(',').Select(i => i.Trim()).ToList();

				if (languages.Count != languages.Distinct().Count())
				{
					Debug.LogWarning($"Duplicated languages found in `{sheet.Name}`. This sheet is not loaded.");
					continue;
				}

				FillLanguages(languages, wordDictionary);
				FillWords(lines, keys, sheet, languages, wordDictionary);
			}

			return wordDictionary;
		}

        private static void FillLanguages(List<string> languages, Dictionary<string, Dictionary<string, string>> wordDictionary)
        {
	        for (int i = 1; i < languages.Count; i++)
	        {
		        if (!wordDictionary.ContainsKey(languages[i]))
		        {
			        wordDictionary.Add(languages[i], new Dictionary<string, string>());
		        }
	        }
        }

        private static void FillWords(List<string> lines, List<string> keys, Sheet sheet, List<string> languages,
	        Dictionary<string, Dictionary<string, string>> wordDictionary)
        {
	        for (int i = 1; i < lines.Count; i++)
	        {
		        List<string> columns = GetColumns(lines[i]);
		        string key = columns[0];

		        if (!ValidateKey(keys, sheet, key))
		        {
			        continue;
		        }

		        keys.Add(key);

		        for (int j = 1; j < languages.Count; j++)
		        {
			        if (wordDictionary[languages[j]].ContainsKey(key))
			        {
				        Debug.LogWarning($"Duplicated key `{key}` in `{sheet.Name}`.");
				        continue;
			        }

			        wordDictionary[languages[j]].Add(key, columns[j]);
		        }
	        }
        }

        private static bool ValidateKey(List<string> keys, Sheet sheet, string key)
        {
	        if (string.IsNullOrEmpty(key))
	        {
		        Debug.LogWarning($"Empty key in `{sheet.Name}`");
		        return false;
	        }

	        if (keys.Contains(key))
	        {
		        Debug.LogWarning($"Duplicated key `{key}` found in `{sheet.Name}`. This key is not loaded.");
		        return false;
	        }

	        return true;
        }

        private static List<string> GetLines(string text)
		{
			string _text = text.Replace("\r\n", "\n").Replace("\"\"", "[_quote_]");
			MatchCollection matches = Regex.Matches(_text, "\"[\\s\\S]+?\"");

			foreach (Match match in matches)
			{
				_text = _text.Replace(
					match.Value,
					match.Value
						.Replace("\"", null)
						.Replace(",", "[_comma_]")
						.Replace("\n", "[_newline_]"));
			}

			return _text.Split('\n').Where(i => i != "").ToList();
		}

		private static List<string> GetColumns(string line)
		{
			return line
				.Split(',')
				.Select(j => j.Trim())
				.Select(j => j
					.Replace("[_quote_]", "\"")
					.Replace("[_comma_]", ",")
					.Replace("[_newline_]", "\n"))
				.ToList();
		}
	}
}