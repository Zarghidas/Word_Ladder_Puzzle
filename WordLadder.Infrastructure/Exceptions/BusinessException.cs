using System;

namespace WordLadder.Infrastructure.Exceptions
{
    public class BusinessException : Exception
    {
        /// <summary>
        /// Exception Level
        /// </summary>
        public ExceptionLevel Level { get; protected set; }

        public BusinessException()
        {
        }

        public BusinessException(string message, ExceptionLevel level, Exception innerException = null)
            : base(message, innerException)
        {
            this.Level = level;
        }

        public BusinessException(string message)
            : this(message, ExceptionLevel.Message)
        {
        }
    }
}
