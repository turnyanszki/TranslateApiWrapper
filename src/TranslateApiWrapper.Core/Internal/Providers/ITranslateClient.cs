using TranslateApiWrapper.Abstractions;

namespace TranslateApiWrapper.Core.Internal.Providers
{
    public interface ITranslateClient
    {
        Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, CancellationToken cancellationToken = default);
    }
}
