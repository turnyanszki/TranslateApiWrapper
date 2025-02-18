namespace TranslateApiWrapper.Core.Internal.Providers
{
    public class ApiException : Exception
    {
        public string ExternalApi { get; set; }
        public ApiException(string externalApi)
        {
            ExternalApi = externalApi;
        }

        public ApiException(string externalApi, string message, Exception innerException) : base(message, innerException)
        {
            ExternalApi = externalApi;
        }
    }
}
