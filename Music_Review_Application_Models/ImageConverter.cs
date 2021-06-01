using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class ImageConverter
    {
        public byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn is null)
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public Image ByteArrayToImage(byte[] bytesIn)
        {
            byte[] emptyBytes = new byte[0];

            if (bytesIn.Length == 0)
            {
                return null;
            }

            using (var ms = new MemoryStream(bytesIn))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
