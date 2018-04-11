using System;
using System.Runtime.Serialization;

using Testing_Framework.Components;

namespace Testing_Framework.Exceptions {

    public class ComparisonFailException : Exception {

        private Sequence sequence;

        public ComparisonFailException() {
        }

        public ComparisonFailException(string message) : base(message) {
        }

        public ComparisonFailException(string message, Sequence sequence) : base(message) {
            this.sequence = sequence;
        }

        public ComparisonFailException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ComparisonFailException(SerializationInfo info, StreamingContext context) : base(info, context) {

        }

        public Sequence GetSequence() {
            return sequence;
        }

    }

}