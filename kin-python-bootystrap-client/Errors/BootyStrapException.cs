using System;
using kin_python_bootystrap_client.Models;

namespace kin_python_bootystrap_client.Errors
{
    public class BootyStrapException : Exception
    {
        public ErrorCodeTypes ErrorCode { get; }

        public BootyStrapException(string message, ErrorCodeTypes errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}