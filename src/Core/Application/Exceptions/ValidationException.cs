﻿using Atomiv.Core.Domain;
using System;
using System.Runtime.Serialization;

namespace Atomiv.Core.Application
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValidationException(Exception innerException) : base(innerException)
        {
        }

        public ValidationException(RequestValidationResult result)
        {
            Result = result;
        }

        public ValidationException(ValidationResult result)
            : this(RequestValidationResult.From(result))
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RequestValidationResult Result { get; private set; }
    }
}