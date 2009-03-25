using System;

namespace InterfaceLayer.Image
{
    class ImageCubeException : ApplicationException
    {
        public ImageCubeException()
        {
            //Nothing
        }

        public ImageCubeException(string message)
            : base(message)
        {
            //Nothing
        }

        public ImageCubeException(string message, Exception inner)
            : base(message, inner)
        {
            //Nothing
        }
    }
}
