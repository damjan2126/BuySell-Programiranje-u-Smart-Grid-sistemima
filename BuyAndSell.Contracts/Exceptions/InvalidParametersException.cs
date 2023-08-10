using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class InvalidParametersException : Exception
    {
        public const string ExceptionMessage = "Bad request from custom middleware";
        public const HttpStatusCode HttpResponseCode = HttpStatusCode.BadRequest;

        public InvalidParametersException() : base(ExceptionMessage)
        {
        }

        public InvalidParametersException(string message) : base(message)
        {
        }

        public InvalidParametersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
