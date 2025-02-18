# TranslateApiWrapper# TranslateApiWrapper

## Overview
TranslateApiWrapper is a .NET 8 library designed to provide a simple and efficient way to interact with various translation APIs. It abstracts the complexities of different translation services, offering a unified interface for developers to easily integrate translation capabilities into their applications.

## Features
- Support for multiple translation APIs (e.g., Google Translate) - IN PROGRESS
- Easy-to-use interface for translating text
- Asynchronous operations for improved performance
- Error handling and logging
- Extensible design for adding new translation services

## Installation
To install TranslateApiWrapper, you can use the NuGet Package Manager:
- dotnet add package TranslateApiWrapper.DI## Usage

## Usage
Here is a basic example of how to use TranslateApiWrapper in your .NET application:

```csharp
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
```

### Extending TranslateApiWrapper
TranslateApiWrapper is designed to be extensible. You can add support for new translation services by implementing the `ITranslateClient` interface and registering it with the appropriate key.


