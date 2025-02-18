namespace TranslateApiWrapper.Abstractions
{
    public class TranslationResult
    {
        public string SourceText { get; set; }
        public string TranslatedText { get; set; }
        public Language SourceLanguage { get; set; }
        public Language DestinationLanguage { get; set; }
    }
}
