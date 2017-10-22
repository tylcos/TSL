using System;
using System.Runtime.Serialization;

namespace Turtle_Tokenizer
{
    [Serializable]
    internal class InvalidCharException : Exception
    {
        public InvalidCharException()
        {
        }

        public InvalidCharException(string message) : base(message)
        {
        }

        public InvalidCharException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCharException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}