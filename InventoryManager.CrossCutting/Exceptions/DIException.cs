using System.Runtime.Serialization;

namespace InventoryManager.CrossCutting.Exceptions
{
/// <summary>
/// Exception used when try to use services not injected
/// </summary>
    [Serializable]
    public class DIException : ApplicationException
    {
        public DIException()
        {
        }

        public DIException(string? message) : base(message)
        {
        }

        public DIException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}