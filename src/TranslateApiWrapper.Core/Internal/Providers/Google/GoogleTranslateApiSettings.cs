using TranslateApiWrapper.Core.Internal.Constants;

namespace TranslateApiWrapper.Core.Internal.Providers.Google
{
    public class GoogleTranslateApiSettings
    {
        /// <summary>
        /// Google translate api base url
        /// </summary>
        public string GoogleTranslateApiBaseUrl { get; set; } = "https://translate.googleapis.com/";

        /// <summary>
        /// Google translate api url template
        /// </summary>
        public string GoogleTranslateApiUrlTemplate { get; set; } = $"translate_a/single?client=gtx&sl={UrlConstants.SourceLangueagePlaceholder}&tl={UrlConstants.DestinationLangueagePlaceholder}&dt=t&q={UrlConstants.TranslateableTextPlaceholder}";

        /// <summary>
        /// query time out (ms)
        /// </summary>
        public int Timeout { get; set; } = 2000;

        /// <summary>
        /// user agent
        /// </summary>
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
    }
}
