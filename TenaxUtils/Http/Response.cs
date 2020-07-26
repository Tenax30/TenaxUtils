using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TenaxUtils.Http
{
    public class HttpResponse<T> where T: class
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
    }
}
