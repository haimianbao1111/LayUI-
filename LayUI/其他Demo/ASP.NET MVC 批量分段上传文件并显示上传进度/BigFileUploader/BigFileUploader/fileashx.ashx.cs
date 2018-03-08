using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BigFileUploader
{
    /// <summary>
    /// fileashx 的摘要说明
    /// </summary>
    public class fileashx : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Request.Form.AllKeys.Any(m => m == "chunk"))
            {
                int chunk = Convert.ToInt32(context.Request.Form["chunk"]);     // 当前分片
                int chunks = Convert.ToInt32(context.Request.Form["chunks"]);   // 分片总计

                string folder = context.Server.MapPath("~/Upload/");
                string filename = folder + context.Request.Form["guid"];

                // FileStream addFile = new FileStream(filename, FileMode.Append, FileAccess.Write);  // 前端 threads= 1 可用
                FileStream addFile = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 1024, true); // 前端 threads值> 1 可用
                BinaryWriter AddWriter = new BinaryWriter(addFile);

                HttpPostedFile file = context.Request.Files[0];
                Stream stream = file.InputStream;

                BinaryReader TempReader = new BinaryReader(stream);
                AddWriter.Write(TempReader.ReadBytes((int)stream.Length));

                TempReader.Close();
                stream.Close();
                AddWriter.Close();
                addFile.Close();

                TempReader.Dispose();
                stream.Dispose();
                AddWriter.Dispose();
                addFile.Dispose();
                //如果是最后一个分片，则重命名临时文件为上传的文件名
                if (chunk == (chunks - 1))
                {
                    FileInfo fileinfo = new FileInfo(filename);
                    fileinfo.MoveTo(context.Server.MapPath("~/Upload/" + context.Request.Files[0].FileName));
                }
            }
            else // 没有分片直接保存
            {
                context.Request.Files[0].SaveAs(context.Server.MapPath("~/Upload/" + context.Request.Files[0].FileName));
            }
            context.Response.Write("success");
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}