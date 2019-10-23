using System;
using System.Runtime.Serialization;

namespace JWT.Controllers
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        private object p;

        public HttpResponseException()
        {
        }

        public HttpResponseException(object p)
        {
            this.p = p;
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
