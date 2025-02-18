using Microsoft.Extensions.DependencyInjection;
using TranslateApiWrapper.Abstractions;
using TranslateApiWrapper.Core.Internal.Providers;

namespace TranslateApiWrapper.Core
{
    public class Translator : ITranslator
    {
        private readonly IServiceProvider _serviceProvider;

        public Translator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, TranslateProvider translateProvider = TranslateProvider.Google, CancellationToken cancellationToken = default)
        {
            ITranslateClient translateClient;

            if (Enum.IsDefined(translateProvider))
                translateClient = _serviceProvider.GetRequiredKeyedService<ITranslateClient>(translateProvider);
            else
                translateClient = _serviceProvider.GetRequiredKeyedService<ITranslateClient>((int)translateProvider);

            return translateClient.TranslateAsync(text, sourceLanguage, destinationLanguage, cancellationToken);
        }
    }
}
