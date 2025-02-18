using Microsoft.Extensions.DependencyInjection;
using TranslateApiWrapper.Abstractions;
using TranslateApiWrapper.Core.Internal.Providers;

namespace TranslateApiWrapper.ConsoleApp.CustomTranslateClient
{
    internal static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomTranslateClient(this IServiceCollection services, int key)
        {
            if (services.FirstOrDefault(x => x.ServiceType == typeof(ITranslator)) is null)
                throw new InvalidOperationException("Please add TranslateApiWrapper services first.");

            services.AddHttpClient();
            services.AddKeyedScoped<ITranslateClient, CustomTranslateClient>(key);

            return services;
        }
    }
}
