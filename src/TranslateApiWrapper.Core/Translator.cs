﻿using Microsoft.Extensions.DependencyInjection;
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

        public Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, TranslateProviders translateProviders = TranslateProviders.Google, CancellationToken cancellationToken = default)
        {
            var translateClient = _serviceProvider.GetRequiredKeyedService<ITranslateClient>(translateProviders.ToString());
            return translateClient.TranslateAsync(text, sourceLanguage, destinationLanguage, cancellationToken);
        }
    }
}
