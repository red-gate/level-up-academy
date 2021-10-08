using System;

namespace AccountCalculator
{
    public class ParsePurchaseFailedException : Exception
    {
        public ParsePurchaseFailedException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}