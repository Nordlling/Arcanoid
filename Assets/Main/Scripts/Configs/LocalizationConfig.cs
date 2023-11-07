using System.Collections.Generic;
using Main.Scripts.Localization;
using UnityEngine;

namespace Main.Scripts.Configs
{
	[CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Configs/Localization")]
	public class LocalizationConfig : ScriptableObject
	{
		public string TableId;
		public List<Sheet> Sheets = new();
		public Object SaveFolder;
		public SystemLanguage defaultLanguage;
		
	}
}