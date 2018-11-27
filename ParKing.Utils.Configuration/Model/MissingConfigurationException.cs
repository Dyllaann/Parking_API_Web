using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ParKing.Utils.Configuration.Model
{
    public class MissingConfigurationException : Exception
    {
        public MissingConfigurationException()
        {
        }

        protected MissingConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MissingConfigurationException(string message) : base(message)
        {
        }

        public MissingConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
