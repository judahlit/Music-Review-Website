using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Services.Interfaces
{
    public interface IImageConverter
    {
        byte[] ImageToByteArray(Image imageIn);

        Image ByteArrayToImage(byte[] bytesIn);
    }
}
