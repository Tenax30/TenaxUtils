using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TenaxUtils.Http
{
    public class HttpClientWrapper : IDisposable
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException("HttpClient client must not be null");
        }

        public void AddDefaultHeader(string name, string value)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains(name))
            {
                _httpClient.DefaultRequestHeaders.Add(name, value);
            }
        }

        public void AddDefaultHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                AddDefaultHeader(header.Key, header.Value);
            }
        }

        public async Task<HttpResponse<T>> SendAsync<T>(HttpMethod method, Uri uri, HttpContent content = null)
            where T:class
        {
            if (typeof(T) != typeof(string) && typeof(T) != typeof(byte[])
                                            && typeof(T) != typeof(Stream))
            {
                throw new NotSupportedException($"Type {nameof(T)} is not supported.");
            }
            
            HttpResponse<T> res = new HttpResponse<T>();

            using (var request = new HttpRequestMessage(method, uri))
            {
                if (content != null)
                {
                    request.Content = content;
                }

                using (var response = await _httpClient.SendAsync(request))
                {
                    res.StatusCode = response.StatusCode;

                    if (typeof(T) == typeof(string))
                    {
                        res.Content = await response.Content.ReadAsStringAsync() as T;
                    }
                    else if (typeof(T) == typeof(byte[]))
                    {
                        res.Content = await response.Content.ReadAsByteArrayAsync() as T;
                    }
                    else if(typeof(T) == typeof(Stream))
                    {
                        res.Content = await response.Content.ReadAsStreamAsync() as T;
                    }

                    return res;
                }
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
