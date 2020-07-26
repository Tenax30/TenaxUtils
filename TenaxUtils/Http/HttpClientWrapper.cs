using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TenaxUtils.Http
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StringHttpResponse> SendAsync(HttpMethod method, Uri uri, HttpContent content = null)
        {
            using (var request = new HttpRequestMessage(method, uri))
            {
                if (content != null)
                {
                    request.Content = content;
                }

                using (var response = await _httpClient.SendAsync(request))
                {
                    return new StringHttpResponse
                    {
                        StatusCode = response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
            }
        }
    }
}
