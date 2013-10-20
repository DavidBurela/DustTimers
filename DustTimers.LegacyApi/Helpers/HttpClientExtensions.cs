using System.Net;
using System.Net.Http;

namespace DustTimers.LegacyApi.Helpers
{
    public static class HttpClientExtensions
    {
        public static HttpClient CreateGzipEnabledClient()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }
            var httpClient = new HttpClient(handler);
            return httpClient;
        }
    }
}
