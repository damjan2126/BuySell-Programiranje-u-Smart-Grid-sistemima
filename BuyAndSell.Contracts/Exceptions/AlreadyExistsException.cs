using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public const string ExceptionMessage = "Already exists from custom middleware";
        public const HttpStatusCode HttpResponseCode = HttpStatusCode.BadRequest;

        public AlreadyExistsException() : base(ExceptionMessage)
        {
        }

        public AlreadyExistsException(string message) : base(message)
        {
        }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
