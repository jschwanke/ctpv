using System;

namespace InterfaceLayer.IO
{
    public class ReaderException : ApplicationException
    {
        public ReaderException()
        {
            //Nothing
        }

        public ReaderException(string message)
            : base(message)
        {
            //Nothing
        }

        public ReaderException(string message, Exception inner)
            : base(message, inner)
        {
            //Nothing
        }
    }
}
