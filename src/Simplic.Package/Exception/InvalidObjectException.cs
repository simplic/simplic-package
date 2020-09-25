﻿using System;

namespace Simplic.Package
{
    internal class InvalidObjectException : Exception
    {
        public InvalidObjectException()
        {
        }

        public InvalidObjectException(string message) : base(message)
        {
        }

        public InvalidObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}