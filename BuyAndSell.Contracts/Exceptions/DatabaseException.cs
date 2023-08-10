using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class DatabaseException : Exception
    {
        public const string ExceptionMessage = "Database exception from custom middleware";
        public const HttpStatusCode HttpResponseCode = HttpStatusCode.BadRequest;

        public DatabaseException() : base()
        {

        }

        public DatabaseException(string message) : base(message)
        {

        }

        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
