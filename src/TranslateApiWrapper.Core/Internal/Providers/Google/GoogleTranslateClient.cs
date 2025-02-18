using GoogleTranslateWrapper.Abstractions;
using GoogleTranslateWrapper.Core.Internal.Constants;
using GoogleTranslateWrapper.Core.Internal.Utils;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GoogleTranslateWrapper.Core.Internal.Providers.Google
{
    public class GoogleTranslateClient : ITranslateClient
    {
        private readonly GoogleTranslateApiSettings _apiSettings;
        private readonly HttpClient _googleApiHttpClient;

        public GoogleTranslateClient(GoogleTranslateApiSettings apiSettings, HttpClient httpClient)
        {
            _apiSettings = apiSettings;

            httpClient.BaseAddress = new Uri(apiSettings.GoogleTranslateApiBaseUrl);
            httpClient.Timeout = TimeSpan.FromMilliseconds(apiSettings.Timeout);
            _googleApiHttpClient = httpClient;
        }

        public async Task<TranslationResult> TranslateAsync(string text, Language sourceLanguage, Language destinationLanguage, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            try
            {
                var request = GenerateRequestMessage(text, sourceLanguage, destinationLanguage);
                var response = await _googleApiHttpClient.SendAsync(request, cancellationToken);
                var result = await response.Content.ReadAsStringAsync();

                return DeserializeTranslationResult(sourceLanguage, destinationLanguage, result);

            }
            catch (GoogleApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GoogleApiException("Error occured during call", ex);
            }
        }

        private HttpRequestMessage GenerateRequestMessage(string text, Language sourceLanguage, Language destinationLanguage)
        {
            var requestUrl = GetUrl(text, sourceLanguage, destinationLanguage);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("User-Agent", _apiSettings.UserAgent);
            return request;
        }

        private static TranslationResult DeserializeTranslationResult(Language sourceLanguage, Language destinationLanguage, string result)
        {
            try
            {
                using (var jsonDocument = JsonSerializer.Deserialize<JsonDocument>(result))
                {
                    if (jsonDocument.RootElement.ValueKind != JsonValueKind.Array)
                        throw new Exception("Invalid result: result root element not an array");

                    string translatedText = string.Empty;
                    string sourceText = string.Empty;

                    var resultArray = jsonDocument.RootElement.EnumerateArray().First();

                    if (resultArray.ValueKind != JsonValueKind.Array)
                        throw new Exception("Invalid result: first level element is not an array");

                    foreach (var item in resultArray.EnumerateArray())
                    {
                        if (item.ValueKind != JsonValueKind.Array)
                            throw new Exception("Invalid result: second level element is not an array");

                        translatedText += item[0];
                        sourceText += item[1];
                    }

                    return new TranslationResult()
                    {
                        TranslatedText = translatedText,
                        SourceText = sourceText,
                        DestinationLanguage = destinationLanguage,
                        SourceLanguage = sourceLanguage
                    };

                }
            }
            catch (Exception Ex)
            {
                throw new GoogleApiException($"Error occured during deserialization", Ex);
            }
        }

        private string GetUrl(string text, Language sourceLanguage, Language destinationLanguage)
        {
            return _apiSettings.GoogleTranslateApiUrlTemplate
                .Replace(UrlConstants.TranslateableTextPlaceholder, Uri.EscapeDataString(text))
                .Replace(UrlConstants.SourceLangueagePlaceholder, LanguageUtils.GetLanguageCode(sourceLanguage))
                .Replace(UrlConstants.DestinationLangueagePlaceholder, LanguageUtils.GetLanguageCode(destinationLanguage));
        }
    }
}
