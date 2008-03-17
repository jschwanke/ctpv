using System;
using System.Collections.Generic;
using System.Text;

namespace CTViewer.Model.Image
{
    public class CTImageException : ApplicationException
    {
        public CTImageException()
        {
            //Nothing
        }

        public CTImageException(string message)
            : base(message)
        {
            //Nothing
        }

        public CTImageException(string message, Exception inner)
            : base(message, inner)
        {
            //Nothing
        }
    }
}
