using GoogleTranslateWrapper.Abstractions;

namespace GoogleTranslateWrapper.Core.Internal.Providers
{
    public interface ITranslateClient
    {
        Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage);
    }
}
