using System;

namespace TSL
{
    [Serializable]
    internal class InvalidCharException : Exception
    {
        public InvalidCharException ()
        {
        }

        public InvalidCharException (string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException()
        {
        }

        public InvalidSyntaxException(string message) : base(message)
        {
        }
    }
}