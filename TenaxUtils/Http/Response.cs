using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TenaxUtils.Http
{
    public class HttpResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
    }

    public class StringHttpResponse : HttpResponse<string> { }
}
