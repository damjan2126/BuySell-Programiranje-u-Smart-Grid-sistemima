using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class NotFoundException : Exception
    {
        public const string ExceptionMessage = "Not found from custom middleware";
        public const HttpStatusCode HttpResponseCode = HttpStatusCode.NotFound;

        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
