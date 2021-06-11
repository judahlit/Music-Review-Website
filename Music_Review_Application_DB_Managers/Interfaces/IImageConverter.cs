using System.Drawing;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface IImageConverter
    {
        byte[] ImageToByteArray(Image imageIn);

        Image ByteArrayToImage(byte[] bytesIn);
    }
}
