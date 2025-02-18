using GoogleTranslateWrapper.Abstractions;
using GoogleTranslateWrapper.Core;
using NSubstitute;
using Xunit;
using System;
using System.Threading.Tasks;
using GoogleTranslateWrapper.Core.Internal.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace TranslateApiWrapper.Tests
{
    public class TranslatorTests
    {
        private readonly IKeyedServiceProvider _serviceProvider;
        private readonly ITranslateClient _translateClient;
        private readonly Translator _translator;

        public TranslatorTests()
        {
            _serviceProvider = Substitute.For<IKeyedServiceProvider>();
            _translateClient = Substitute.For<ITranslateClient>();

            _serviceProvider.GetRequiredKeyedService(typeof(ITranslateClient), Arg.Any<string>())
                .Returns((object)_translateClient);

            _translator = new Translator(_serviceProvider);
        }

        [Fact]
        public async Task TranslateAsync_ValidInput_ReturnsTranslationResult()
        {
            // Arrange
            string text = "Hello";
            var sourceLanguage = Language.English;
            var destinationLanguage = Language.German;
            var translationResult = new TranslationResult
            {
                SourceText = text,
                TranslatedText = "Hallo",
                SourceLanguage = sourceLanguage,
                DestinationLanguage = destinationLanguage
            };

            _translateClient.TranslateAsync(text, sourceLanguage, destinationLanguage)
                .Returns(Task.FromResult(translationResult));

            // Act
            var result = await _translator.TranslateAsync(text, sourceLanguage, destinationLanguage);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Hallo", result.TranslatedText);
            Assert.Equal(sourceLanguage, result.SourceLanguage);
            Assert.Equal(destinationLanguage, result.DestinationLanguage);
        }

        [Fact]
        public async Task TranslateAsync_InvalidProvider_ThrowsException()
        {
            // Arrange
            string text = "Hello";
            var sourceLanguage = Language.English;
            var destinationLanguage = Language.German;

            _serviceProvider.GetRequiredKeyedService(typeof(ITranslateClient), Arg.Any<string>())
                .Returns(x => { throw new InvalidOperationException("No service for type 'ITranslateClient'"); });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _translator.TranslateAsync(text, sourceLanguage, destinationLanguage));
        }
    }
}

