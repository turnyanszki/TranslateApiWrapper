using TranslateApiWrapper.Abstractions;

namespace TranslateApiWrapper.Core.Internal.Utils
{
    internal static class LanguageUtils
    {
        private static readonly Dictionary<Language, string> _languageMapping = new Dictionary<Language, string>
        {
            {Language.English, "en" },
            {Language.German, "de" },
            {Language.French, "fr" }
        };

        public static string GetLanguageCode(Language language)
        {
            if (!_languageMapping.ContainsKey(language))
            {
                throw new ArgumentException($"Language {language} is not supported");
            }

            return _languageMapping[language];
        }
    }
}
