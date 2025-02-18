using GoogleTranslateWrapper.Abstractions;
using GoogleTranslateWrapper.Core;
using GoogleTranslateWrapper.Core.Internal.Providers;
using GoogleTranslateWrapper.Core.Internal.Providers.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleTranslateWrapper.DI
{
    public static class GoogleTranslateWrapperServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleTranslateWrapper(this IServiceCollection services, IConfiguration configuration)
        {
            var googleTranslateApiSettings = configuration?.GetSection("GoogleTranslateClientSettings").Get<GoogleTranslateApiSettings>() ?? new GoogleTranslateApiSettings();
            services.AddSingleton(googleTranslateApiSettings);
            services.AddHttpClient();
            services.AddKeyedScoped<ITranslateClient, GoogleTranslateClient>(TranslateProviders.Google.ToString());
            services.AddSingleton<ITranslator, Translator>();
            return services;
        }
    }
}
