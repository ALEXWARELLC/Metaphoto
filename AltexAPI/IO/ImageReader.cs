using System;

namespace AltexAPI.IO
{
    public class ImageReader
    {
        public static ImageReader? Instance { get; private set; }

        public ImageReader()
        {
            Instance = this;
        }

        
    }
}
