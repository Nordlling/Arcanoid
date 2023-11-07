using System;
using UnityEngine;

namespace Main.Scripts.Localization
{
    public interface ILocalizationManager
    {
        event Action LocalizationChanged;
        string CurrentLanguage { get; }
        string[] AllLanguages { get; }
        void ChangeLanguage(SystemLanguage language);
        string Localize(string localizationKey);
    }
}