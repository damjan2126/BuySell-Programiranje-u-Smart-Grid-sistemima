using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public const string ExceptionMessage = "Invalid token from custom middleware";
        public const HttpStatusCode HttpResponseCode = HttpStatusCode.Unauthorized;

        public InvalidTokenException() : base(ExceptionMessage)
        {
        }

        public InvalidTokenException(string message) : base(message)
        {
        }

        public InvalidTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
