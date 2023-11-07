using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Constants;
using Main.Scripts.Localization;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Main.Scripts.Editor
{
    [CustomEditor(typeof(LocalizationConfig))]
    public class LocalizationSettingsEditor : UnityEditor.Editor
    {
        private const string _urlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";
        private DateTime _timestamp;
        private LocalizationConfig _localizationConfig;

        public override void OnInspectorGUI()
        {
	        _localizationConfig = (LocalizationConfig)target;

            DrawDefaultInspector();
            DisplayButtons();
        }

        private void DisplayButtons()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, fixedHeight = 30 };

            if (GUILayout.Button("Fill Sheets", buttonStyle))
            {
                ResolveGoogleSheets();
            }

            if (GUILayout.Button("Download Localization", buttonStyle))
            {
                DownloadGoogleSheets();
            }

            if (GUILayout.Button("Open Google Sheets", buttonStyle))
            {
                OpenGoogleSheets();
            }
        }

        private async void ResolveGoogleSheets()
        {
	        await ResolveGoogleSheetsAsync();
        }

        private async void DownloadGoogleSheets()
		{
			await DownloadGoogleSheetsAsync();
		}

        private void OpenGoogleSheets()
        {
	        if (string.IsNullOrEmpty(_localizationConfig.TableId))
	        {
		        Debug.LogWarning("Table ID is empty.");
	        }
	        else
	        {
		        Application.OpenURL(string.Format(LocalizationConstants.TableUrlPattern, _localizationConfig.TableId));
	        }
        }

        private async Task DownloadGoogleSheetsAsync(bool silent = false)
		{
			if (CheckErrors())
			{
				return;
			}

			_timestamp = DateTime.UtcNow;

			if (!silent)
			{
				ClearSaveFolder();
			}

			for (int i = 0; i < _localizationConfig.Sheets.Count; i++)
			{
				Sheet sheet = _localizationConfig.Sheets[i];
				string url = string.Format(_urlPattern, _localizationConfig.TableId, sheet.Id);

				Debug.Log($"Downloading <color=grey>{url}</color>");

				UnityWebRequest request = UnityWebRequest.Get(url);
				float progress = (float)(i + 1) / _localizationConfig.Sheets.Count;

				if (EditorUtility.DisplayCancelableProgressBar("Downloading sheets...",
					    $"[{(int)(100 * progress)}%] [{i + 1}/{_localizationConfig.Sheets.Count}] Downloading {sheet.Name}...", progress))
				{
					return;
				}

				await request.SendWebRequest();

				string error = request.error ?? (request.downloadHandler.text.Contains("signin/identifier")
					? "It seems that access to this document is denied."
					: null);

				if (string.IsNullOrEmpty(error))
				{
					string path = Path.Combine(AssetDatabase.GetAssetPath(_localizationConfig.SaveFolder), sheet.Name + ".csv");

					File.WriteAllBytes(path, request.downloadHandler.data);
					AssetDatabase.Refresh();
					_localizationConfig.Sheets[i].TextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
					EditorUtility.SetDirty(this);
					Debug.LogFormat(
						$"Sheet <color=yellow>{sheet.Name}</color> ({sheet.Id}) saved to <color=grey>{path}</color>");
				}
				else
				{
					EditorUtility.ClearProgressBar();
					EditorUtility.DisplayDialog("Error", error.Contains("404") ? "Table Id is wrong!" : error, "OK");
					return;
				}
			}

			await Task.Yield();

			AssetDatabase.Refresh();
			EditorUtility.ClearProgressBar();

			if (!silent)
			{
				EditorUtility.DisplayDialog("Message", $"{_localizationConfig.Sheets.Count} localization sheets downloaded!", "OK");
			}
		}

        private bool CheckErrors()
		{
			if (string.IsNullOrEmpty(_localizationConfig.TableId) || _localizationConfig.Sheets.Count == 0)
			{
				EditorUtility.DisplayDialog("Error", "Table Id is empty.", "OK");
				return true;
			}

			if (_localizationConfig.SaveFolder == null)
			{
				EditorUtility.DisplayDialog("Error", "Save Folder is not set.", "OK");
				return true;
			}

			if (_localizationConfig.Sheets.Count == 0)
			{
				EditorUtility.DisplayDialog("Error", "Sheets are empty.", "OK");
				return true;
			}

			if ((DateTime.UtcNow - _timestamp).TotalSeconds < 2)
			{
				if (EditorUtility.DisplayDialog("Message", "Too many requests! Try again later.", "OK"))
				{
					return true;
				}
			}

			return false;
		}

        private void ClearSaveFolder()
		{
			string[] files = Directory.GetFiles(AssetDatabase.GetAssetPath(_localizationConfig.SaveFolder));

			foreach (string file in files)
			{
				File.Delete(file);
			}
		}

        private async Task ResolveGoogleSheetsAsync()
		{
			if (string.IsNullOrEmpty(_localizationConfig.TableId))
			{
				EditorUtility.DisplayDialog("Error", "Table Id is empty.", "OK");
				return;
			}

			using UnityWebRequest request = UnityWebRequest.Get($"{LocalizationConstants.SheetResolverUrl}?tableUrl={_localizationConfig.TableId}");

			if (EditorUtility.DisplayCancelableProgressBar("Resolving sheets...", "Executing Google App Script...", 1))
			{
				return;
			}

			await request.SendWebRequest();

			EditorUtility.ClearProgressBar();

			if (request.error == null)
			{
				string error = GetInternalError(request);

				if (error != null)
				{
					EditorUtility.DisplayDialog("Error", "Table not found or public read permission not set.", "OK");

					return;
				}

				Dictionary<string, long> sheetsDict = JsonConvert.DeserializeObject<Dictionary<string, long>>(request.downloadHandler.text);

				_localizationConfig.Sheets.Clear();

				foreach (KeyValuePair<string, long> item in sheetsDict)
				{
					_localizationConfig.Sheets.Add(new Sheet { Id = item.Value, Name = item.Key });
				}

				EditorUtility.DisplayDialog("Message", $"{_localizationConfig.Sheets.Count}", "OK");
			}
			else
			{
				throw new Exception(request.error);
			}
		}

		private static string GetInternalError(UnityWebRequest request)
		{
			MatchCollection matches = Regex.Matches(request.downloadHandler.text, @">(?<Message>.+?)<\/div>");

			if (matches.Count == 0 && !request.downloadHandler.text.Contains("Google Script ERROR:"))
			{
				return null;
			}

			string error = matches.Count > 0 ? matches[1].Groups["Message"].Value.Replace("quot;", "") : request.downloadHandler.text;

			return error;
		}
    }
}