namespace TranslateApiWrapper.Core.Internal.Providers.Google
{
    public class GoogleApiException : ApiException
    {
        public GoogleApiException() : base("GoogleTranslateApi")
        {
        }

        public GoogleApiException(string message, Exception innerException) : base("GoogleTranslateApi", message, innerException)
        {
        }
    }
}
