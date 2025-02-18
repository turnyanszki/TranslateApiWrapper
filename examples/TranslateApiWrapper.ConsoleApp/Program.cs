using GoogleTranslateWrapper.Abstractions;
using GoogleTranslateWrapper.DI;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleTranslateWrapper.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddGoogleTranslateWrapper(null);
            using (var provider = services.BuildServiceProvider())

            {
                var translator = provider.GetRequiredService<ITranslator>();
                var result = await translator.TranslateAsync("Example translatable text. Do you know any translatable text?", Language.English, Language.German, TranslateProviders.Google);
                Console.WriteLine(result.TranslatedText);
            }

        }
    }
}
