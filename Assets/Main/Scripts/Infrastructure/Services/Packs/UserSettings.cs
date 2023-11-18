using System;

namespace Main.Scripts.Infrastructure.Services.Packs
{
    [Serializable]
    public class UserSettings
    {
        public string SelectedLanguage;

        public UserSettings(string selectedLanguage)
        {
            SelectedLanguage = selectedLanguage;
        }
    }
}