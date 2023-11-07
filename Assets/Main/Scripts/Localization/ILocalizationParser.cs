using System.Collections.Generic;

namespace Main.Scripts.Localization
{
    public interface ILocalizationParser
    {
        Dictionary<string, Dictionary<string, string>> ParseLocalization(List<Sheet> sheets);
    }
}