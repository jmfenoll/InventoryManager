using System.Runtime.Serialization;

namespace InventoryManager.CrossCutting.Exceptions
{
    /// <summary>
    /// Exception used when data is not found
    /// </summary>
    [Serializable]
    public class DataNotFoundException : ApplicationException
    {
        public DataNotFoundException()
        {
        }

        public DataNotFoundException(string? message) : base(message)
        {
        }

        public DataNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}