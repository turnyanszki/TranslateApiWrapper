using Microsoft.Extensions.DependencyInjection;
using TranslateApiWrapper.Abstractions;
using TranslateApiWrapper.DI;

namespace GoogleTranslateWrapper.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTranslateApiWrapper()
                    .AddGoogleTranslateClient(configure =>
                    {
                        configure.Timeout = 2000;
                    });

            using (var provider = services.BuildServiceProvider())
            {
                var translator = provider.GetRequiredService<ITranslator>();
                var result = await translator.TranslateAsync("Example translatable text. Do you know any translatable text?", Language.English, Language.German, TranslateProviders.Google);
                Console.WriteLine(result.TranslatedText);
            }

        }
    }
}
