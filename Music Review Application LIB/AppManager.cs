using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Music_Review_Application_LIB
{
    public class AppManager
    {
        #region Constants and Fields

        public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MRA_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #endregion

        public AppManager()
        {

        }

        public string GetSqlString(string value)
        {
            if (value != null)
            {
                return value.Replace("'", "''");
            }

            return value;
        }

        public void LetUserChooseImage()
        {
            
        }

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
