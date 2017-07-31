using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class ResponseWrapper
    {
        public dynamic data { get; set; }
        public ResponseWrapper(dynamic _data)
        {
            data = _data;
        }
    }
}