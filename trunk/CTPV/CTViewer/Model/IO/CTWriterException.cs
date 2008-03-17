using System;
using System.Collections.Generic;
using System.Text;

namespace CTViewer.Model.IO
{
    class CTWriterException : ApplicationException
    {
        public CTWriterException()
        {
            //Nothing
        }

        public CTWriterException(string message)
            : base(message)
        {
            //Nothing
        }

        public CTWriterException(string message, Exception inner)
            : base(message, inner)
        {
            //Nothing
        }
    }
}