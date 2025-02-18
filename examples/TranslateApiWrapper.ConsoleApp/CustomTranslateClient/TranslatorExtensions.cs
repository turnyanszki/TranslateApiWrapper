using TranslateApiWrapper.Abstractions;

namespace TranslateApiWrapper.ConsoleApp.CustomTranslateClient
{
    internal static class TranslatorExtensions
    {
        internal static Task<TranslationResult> TranslateAsync(this ITranslator translator, string text, Language sourceLanguage, Language destinationLanguage, CustomTranslateProvider customTranslateProvider, CancellationToken cancellationToken = default)
        {
            return translator.TranslateAsync(text, sourceLanguage, destinationLanguage, (TranslateProvider)customTranslateProvider, cancellationToken);
        }
    }
}
