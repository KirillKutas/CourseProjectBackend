using System;
using System.Net;
using Newtonsoft.Json;

namespace API.Common.Exceptions
{
    internal class ExceptionResponse
    {
        public string Error { get; set; }

        public int Code { get; set; }

        public ExceptionResponse(Exception ex)
        {
            Error = ex.Message;
            Code = (int)HttpStatusCode.InternalServerError;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
