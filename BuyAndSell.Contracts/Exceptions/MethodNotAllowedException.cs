using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class MethodNotAllowedException : Exception
    {
        public const string ExceptionMessage = "Methos not allowed from custom middleware";
        public const HttpStatusCode HttpResponseCode = HttpStatusCode.BadRequest;

        public MethodNotAllowedException() : base(ExceptionMessage)
        {
        }

        public MethodNotAllowedException(string message) : base(message)
        {
        }

        public MethodNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MethodNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
