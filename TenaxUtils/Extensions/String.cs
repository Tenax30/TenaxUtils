using System;
using System.Text;

namespace TenaxUtils.Extensions
{
    public static class TenaxString
    {
        private const int CharTypeNumber = 4;
        private const int AsciiSpecialCharSetNumber = 4;

        private static Random _rnd;

        private enum CharType
        {
            Digit,
            Uppercase,
            Lowercase,
            Special
        }

        private enum AsciiSpecialCharSet
        {
            First,
            Second,
            Third,
            Fourth
        }

        static TenaxString()
        {
            _rnd = new Random();
        }

        public static string GetValueFromText(string text, string leftSubstring, string rightSubstring)
        {
            if(string.IsNullOrEmpty(text))
            {
                return null;
            }

            var startIndex = string.IsNullOrEmpty(leftSubstring) ? 0 
                : text.IndexOf(leftSubstring, StringComparison.Ordinal);

            if(startIndex == -1)
            {
                return null;
            }

            var endIndex = string.IsNullOrEmpty(rightSubstring) ? text.Length
                : text.IndexOf(rightSubstring, startIndex, StringComparison.Ordinal);

            if(endIndex == -1)
            {
                return null;
            }

            var resultLength = endIndex - startIndex;
            return text.Substring(startIndex, resultLength);
        }

        public static string GenerateRandomString(int minLength, int maxLength, bool includeSpecialChars = false)
        {
            if(minLength > maxLength)
            {
                throw new ArgumentException("The minimum string length must be less than the maximum.");
            }

            if(minLength < 1)
            {
                throw new ArgumentException("The length of the resulting string must be a positive number.");
            }

            var resultLength = _rnd.Next(minLength, maxLength + 1);
            var currentCharTypeNumber = includeSpecialChars ? CharTypeNumber : CharTypeNumber - 1;
            var builder = new StringBuilder(resultLength);

            for(int i = 0; i < resultLength; i++)
            {
                var nextCharType = (CharType)_rnd.Next(currentCharTypeNumber);
                char nextChar;

                switch(nextCharType)
                {
                    case CharType.Digit:
                        nextChar = (char)_rnd.Next(48, 58);
                        break;
                    case CharType.Uppercase:
                        nextChar = (char)_rnd.Next(65, 91);
                        break;
                    case CharType.Lowercase:
                        nextChar = (char)_rnd.Next(97, 123);
                        break;
                    case CharType.Special:
                        nextChar = GetRandomAsciiSpecialChar();
                        break;
                    default:
                        nextChar = default;
                        break;
                }

                builder.Append(nextChar);
            }

            return builder.ToString();
        }

        private static char GetRandomAsciiSpecialChar()
        {
            var nextCharSet = (AsciiSpecialCharSet)_rnd.Next(AsciiSpecialCharSetNumber);

            switch (nextCharSet)
            {
                case AsciiSpecialCharSet.First:
                    return (char)_rnd.Next(33, 48);
                case AsciiSpecialCharSet.Second:
                    return (char)_rnd.Next(58, 65);
                case AsciiSpecialCharSet.Third:
                    return (char)_rnd.Next(91, 97);
                case AsciiSpecialCharSet.Fourth:
                    return (char)_rnd.Next(123, 127);
                default:
                    return default;
            }
        }
    }
}
