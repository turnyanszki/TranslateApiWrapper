using TranslateApiWrapper.Abstractions;
using TranslateApiWrapper.Core.Internal.Providers;

namespace TranslateApiWrapper.ConsoleApp.CustomTranslateClient
{
    internal class CustomTranslateClient : ITranslateClient
    {
        public Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new TranslationResult
            {
                SourceLanguage = sourceLanguage,
                DestinationLanguage = destinationLanguage,
                SourceText = text,
                TranslatedText = "Hello World!"
            });
        }
    }
}
