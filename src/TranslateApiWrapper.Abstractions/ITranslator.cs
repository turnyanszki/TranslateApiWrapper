namespace TranslateApiWrapper.Abstractions
{
    public interface ITranslator
    {
        Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, TranslateProvider translateProvider = TranslateProvider.Google, CancellationToken cancellationToken = default);
    }
}
