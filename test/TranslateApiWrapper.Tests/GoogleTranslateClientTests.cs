using System.Net;
using TranslateApiWrapper.Abstractions;
using TranslateApiWrapper.Core.Internal.Providers.Google;

namespace TranslateApiWrapper.Tests
{
    public class GoogleTranslateClientTests
    {
        private readonly GoogleTranslateApiSettings _apiSettings;
        private readonly HttpClient _httpClient;
        private readonly GoogleTranslateClient _googleTranslateClient;

        public GoogleTranslateClientTests()
        {
            _apiSettings = new GoogleTranslateApiSettings
            {
                GoogleTranslateApiBaseUrl = "https://translation.googleapis.com/",
                GoogleTranslateApiUrlTemplate = "language/translate/v2?key=API_KEY&q={text}&source={source}&target={target}"
            };

            var httpMessageHandler = new MockHttpMessageHandler();
            _httpClient = new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri(_apiSettings.GoogleTranslateApiBaseUrl)
            };

            _googleTranslateClient = new GoogleTranslateClient(_apiSettings, _httpClient);
        }

        [Fact]
        public async Task TranslateAsync_ValidInput_ReturnsTranslationResult()
        {
            // Arrange
            string text = "Hello";
            var sourceLanguage = Language.English;
            var destinationLanguage = Language.German;
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[[[\"Hallo\",\"Hello\"]]]")
            };

            var httpMessageHandler = new MockHttpMessageHandler(responseMessage);
            var httpClient = new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri(_apiSettings.GoogleTranslateApiBaseUrl)
            };

            var googleTranslateClient = new GoogleTranslateClient(_apiSettings, httpClient);

            // Act
            var result = await googleTranslateClient.TranslateAsync(text, sourceLanguage, destinationLanguage);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Hallo", result.TranslatedText);
            Assert.Equal("Hello", result.SourceText);
            Assert.Equal(sourceLanguage, result.SourceLanguage);
            Assert.Equal(destinationLanguage, result.DestinationLanguage);
        }

        [Fact]
        public async Task TranslateAsync_EmptyText_ThrowsArgumentNullException()
        {
            // Arrange
            string text = "";
            var sourceLanguage = Language.English;
            var destinationLanguage = Language.German;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _googleTranslateClient.TranslateAsync(text, sourceLanguage, destinationLanguage));
        }

        [Fact]
        public async Task TranslateAsync_InvalidJsonResponse_ThrowsGoogleApiException()
        {
            // Arrange
            string text = "Hello";
            var sourceLanguage = Language.English;
            var destinationLanguage = Language.German;
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{ invalid json }")
            };

            var httpMessageHandler = new MockHttpMessageHandler(responseMessage);
            var httpClient = new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri(_apiSettings.GoogleTranslateApiBaseUrl)
            };

            var googleTranslateClient = new GoogleTranslateClient(_apiSettings, httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<GoogleApiException>(() =>
                googleTranslateClient.TranslateAsync(text, sourceLanguage, destinationLanguage));
        }

        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage _responseMessage;

            public MockHttpMessageHandler(HttpResponseMessage responseMessage = null)
            {
                _responseMessage = responseMessage;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_responseMessage ?? new HttpResponseMessage(HttpStatusCode.OK));
            }
        }
    }
}
