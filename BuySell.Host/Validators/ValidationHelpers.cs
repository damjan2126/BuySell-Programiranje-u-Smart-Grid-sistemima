using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace BuySell.Host.Validators
{
    public static class ValidationHelpers
    {
        public static IEnumerable<ValidationFailure> GenerateValidationError(string paramName, string message)
        {
            return new[]
            {
            new ValidationFailure(paramName, message)
            };
        }

        public static bool ValidNumber(string number)
        {
            return long.TryParse(number, out _);
        }

        public static bool ValidImage(IFormFile? image)
        {
            return image is null
                   || image.ContentType.Equals("image/jpeg")
                   || image.ContentType.Equals("image/jpg")
                   || image.ContentType.Equals("image/png");
        }

        public static bool HasLowercase(string password)
        {
            var lowercase = new Regex("[a-z]+");

            return lowercase.IsMatch(password);
        }

        public static bool HasUppercase(string password)
        {
            var uppercase = new Regex("[A-Z]+");

            return uppercase.IsMatch(password);
        }

        public static bool HasDigit(string password)
        {
            var digit = new Regex("(\\d)+");

            return digit.IsMatch(password);
        }

        public static bool HasSymbol(string password)
        {
            var symbol = new Regex("(\\W)+");

            return symbol.IsMatch(password);
        }
    }
}
