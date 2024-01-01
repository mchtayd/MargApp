using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Shared
{
    public class BaseResponse
    {
        public string Message { get; private set; }
        public bool IsSuccess { get; private set; }
        public object Content { get; private set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public void ClearMessage()
        {
            Message = string.Empty;
        }

        public BaseResponse SetResponse(string message, bool isSuccess, object content = null, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            IsSuccess = isSuccess;
            Message = message;
            Content = content;
            HttpStatusCode = httpStatusCode;
            return this;
        }
    }
}
