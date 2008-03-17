using System;
using System.Collections.Generic;
using System.Text;

namespace CTViewer.Model.IO
{
    class CTReaderException : ApplicationException
    {
        public CTReaderException()
        {
            //Nothing
        }

        public CTReaderException(string message)
            : base(message)
        {
            //Nothing
        }

        public CTReaderException(string message, Exception inner)
            : base(message, inner)
        {
            //Nothing
        }
    }
}
