using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UIHelper
{
    public class PicToBase64
    {
        //图片 转为    base64编码的文本
        private string ToBase64(string Imagefilename)
        {
            Bitmap bmp = new Bitmap(Imagefilename);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(arr);
        }

        private string ToPostBase64(string base64)
        {
            byte[] buffer = Convert.FromBase64String(base64);
            return Convert.ToBase64String(buffer).Replace("+", "%2B");
        }

        private string ToBase64(Bitmap bitmap)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(arr, 0, (int)memStream.Length);
            memStream.Close();
            return Convert.ToBase64String(arr);
        }
    }
}
