namespace GoogleTranslateWrapper.Abstractions
{
    public interface ITranslator
    {
        Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, TranslateProviders translateProviders = TranslateProviders.Google, CancellationToken cancellationToken = default);
    }
}
