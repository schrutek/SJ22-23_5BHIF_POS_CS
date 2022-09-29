using System;
using System.Runtime.Serialization;

namespace SPG_Fachtheorie.Aufgabe2
{
    [Serializable]
    internal class WhatEverException : Exception
    {
        public WhatEverException()
        {
        }

        public WhatEverException(string message) : base(message)
        {
        }

        public WhatEverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WhatEverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}