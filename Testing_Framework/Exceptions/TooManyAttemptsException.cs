using System;
using System.Runtime.Serialization;

namespace Testing_Framework.Exceptions {

    public class TooManyAttemptsException : Exception {

        public TooManyAttemptsException() {
        }

        public TooManyAttemptsException(string message) : base(message) {
        }

        public TooManyAttemptsException(string message, Exception innerException) : base(message, innerException) {
        }

        protected TooManyAttemptsException(SerializationInfo info, StreamingContext context) : base(info, context) {

        }

    }

}