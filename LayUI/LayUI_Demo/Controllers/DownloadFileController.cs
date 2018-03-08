﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUI_Demo.Controllers
{
    public class DownloadFileController : Controller
    {
        // GET: DownloadFile
        public ActionResult Index()
        {
            return View();
        }

        //流方式下载 (下载大文件报错 iis Express 100多M、iis 最多2G)，然后下载大文件非常耗内存，很容易把服务器搞挂，不建议使用此方法
        public void FileDownload2()
        {
            string fileName = "新建文件夹2.zip";//客户端保存的文件名  
            string filePath = Server.MapPath("/App_Data/新建文件夹2.zip");//要被下载的文件路径   

            Response.ContentType = "application/octet-stream";//二进制流
            //通知浏览器下载文件而不是打开  
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));

            //以字符流的形式下载文件  
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                Response.AddHeader("Content-Length", fs.Length.ToString());
                //这里容易内存溢出
                //理论上数组最大长度 int.MaxValue 2147483647 
                //（实际分不到这么多，不同的程序能分到值也不同，本人机器，winfrom( 2147483591 相差56)、iis(也差不多2G)、iis Express（只有100多MB））
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                Response.BinaryWrite(bytes);
            }
            Response.Flush();
            Response.End();
        }

        //WriteFile (下载大文件报错 iis Express 100多M、iis 最多2G)，然后下载大文件非常耗内存，很容易把服务器搞挂，不建议使用此方法
        public void FileDownload3()
        {
            string fileName = "新建文件夹2.zip";//客户端保存的文件名  
            string filePath = Server.MapPath("/App_Data/新建文件夹2.zip");//要被下载的文件路径  
            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "\"");
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());//文件大小
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(fileInfo.FullName);//大小参数必须介于零和最大的 Int32 值之间(也就是最大2G，不过这个操作非常耗内存)
            //这里容易内存溢出
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 分块下载 
        /// </summary>
        public void FileDownload4()
        {
            string fileName = "新建文件夹2.zip";//客户端保存的文件名  
            string filePath = Server.MapPath("/App_Data/新建文件夹2.zip");//要被下载的文件路径   

            if (System.IO.File.Exists(filePath))
            {
                const long ChunkSize = 102400; //100K 每次读取文件，只读取100K，这样可以缓解服务器的压力  
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                {
                    long fileSize = fileStream.Length; //文件大小  
                    Response.ContentType = "application/octet-stream"; //二进制流
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));//下载保存的文件名
                    Response.AddHeader("Content-Length", fileStream.Length.ToString());//文件总大小
                    while (fileSize > 0 && Response.IsClientConnected)//判断客户端是否还连接了服务器
                    {
                        //实际读取的大小  
                        int readSize = fileStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));
                        Response.OutputStream.Write(buffer, 0, readSize);
                        Response.Flush();//如果客户端 暂停下载时，这里会阻塞。
                        fileSize = fileSize - readSize;//文件剩余大小
                    }
                }
                Response.Close();
            }
        }

        //TransmitFile
        public void FileDownload5()
        {
            //前面可以做用户登录验证、用户权限验证等。

            string filename = "新建文件夹2.zip";   //客户端保存的文件名  
            string filePath = Server.MapPath("/App_Data/新建文件夹2.zip");//要被下载的文件路径 

            var range = Request.Headers["Range"];
            if (!string.IsNullOrWhiteSpace(range))//如果遵守协议，支持断点续传
            {
                var fileLength = new FileInfo(filePath).Length;//文件的总大小
                long begin;//文件的开始位置
                long end;//文件的结束位置
                long.TryParse(range.Split('=')[1].Split('-')[0], out begin);
                long.TryParse(range.Split('-')[1], out end);
                end = end - begin > 0 ? end : (fileLength - 1);

                //表头 表明  下载文件的开始、结束位置 和文件总大小
                Response.AddHeader("Content-Range", "bytes " + begin + "-" + end + "/" + fileLength);
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.TransmitFile(filePath, begin, (end - begin));//发送 文件开始位置读取的大小
            }
            else
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.TransmitFile(filePath);
            }
        }
    }
}