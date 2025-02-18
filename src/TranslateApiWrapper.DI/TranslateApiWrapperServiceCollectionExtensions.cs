using Microsoft.Extensions.DependencyInjection;
using TranslateApiWrapper.Abstractions;
using TranslateApiWrapper.Core;
using TranslateApiWrapper.Core.Internal.Providers;
using TranslateApiWrapper.Core.Internal.Providers.Google;

namespace TranslateApiWrapper.DI
{
    public static class TranslateApiWrapperServiceCollectionExtensions
    {
        public static IServiceCollection AddTranslateApiWrapper(this IServiceCollection services)
        {
            services.AddSingleton<ITranslator, Translator>();

            return services;
        }

        public static IServiceCollection AddGoogleTranslateClient(this IServiceCollection services, Action<GoogleTranslateApiSettings> configure = default)
        {
            if (services.FirstOrDefault(x => x.ServiceType == typeof(ITranslator)) is null)
                throw new InvalidOperationException("Please add TranslateApiWrapper services first.");

            var googleTranslateApiSettings = new GoogleTranslateApiSettings();
            configure?.Invoke(googleTranslateApiSettings);

            services.AddSingleton(googleTranslateApiSettings);
            services.AddHttpClient();
            services.AddKeyedScoped<ITranslateClient, GoogleTranslateClient>(TranslateProvider.Google);

            return services;
        }
    }
}
