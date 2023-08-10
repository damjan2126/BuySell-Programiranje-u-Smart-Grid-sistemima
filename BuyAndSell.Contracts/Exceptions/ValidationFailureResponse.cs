using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Exceptions
{
    public class ValidationFailureResponse
    {
        public List<string> Errors { get; init; } = new();
    }
}
