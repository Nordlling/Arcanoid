using System;

namespace Main.Scripts.Localization
{
    public interface ILocalizationManager
    {
        event Action LocalizationChanged;
        string CurrentLanguage { get; }
        string[] AllLanguages { get; }
        void ChangeLanguage(string language);
        string Localize(string localizationKey);
    }
}