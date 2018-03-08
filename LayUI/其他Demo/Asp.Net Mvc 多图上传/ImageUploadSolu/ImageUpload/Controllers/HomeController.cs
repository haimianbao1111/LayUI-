using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageUpload.Controllers
{
    public class HomeController : Controller
    {
        static string urlPath = string.Empty;
        public HomeController() 
        {
            var applicationPath = VirtualPathUtility.ToAbsolute("~") == "/" ? "" : VirtualPathUtility.ToAbsolute("~");
            urlPath = applicationPath + "/Upload";
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpLoadProcess()
        {
            try
            {
                string fileName = Request["name"];
                string fileRelName = fileName.Substring(0, fileName.LastIndexOf('.'));//设置临时存放文件夹名称
                int index = Convert.ToInt32(Request["chunk"]);//当前分块序号
                int maxChunk = Convert.ToInt32(Request["maxChunk"]);//所有块数
                var guid = Request["guid"];//前端传来的GUID号
                var dir = Server.MapPath("~/Upload");//文件上传目录
                dir = Path.Combine(dir, fileRelName);//临时保存分块的目录
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);
                string filePath = Path.Combine(dir, index.ToString());//分块文件名为索引名，更严谨一些可以加上是否存在的判断，防止多线程时并发冲突
                var data = Request.Files["file"];//表单中取得分块文件
                string extension = data.FileName.Substring(data.FileName.LastIndexOf(".") + 1,
                    (data.FileName.Length - data.FileName.LastIndexOf(".") - 1));//获取文件扩展名
               
                data.SaveAs(filePath + fileName);

                //如果是最后一个分块， 则合并文件
                if (index == maxChunk - 1)
                {
                    Merge(fileName, guid);
                    return Json(new { msg = "ok" });

                }
                else
                {
                    return Json(new { msg = "next" });
                }
            }
            catch (Exception)
            {
                return Json(new { msg = "error" });
            }

            
        
        }
        /// <summary>
        /// 合并文件
        /// </summary>
        /// <returns></returns>
        public void Merge(string fileName, string guid)
        {
            var uploadDir = Server.MapPath("~/Upload");//Upload 文件夹
            string fileRelName = fileName.Substring(0, fileName.LastIndexOf('.'));
            var dir = Path.Combine(uploadDir, fileRelName);//临时文件夹          
            var files = System.IO.Directory.GetFiles(dir);//获得下面的所有文件
            var finalPath = Path.Combine(uploadDir, fileName);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
            var fs = new FileStream(finalPath, FileMode.Create);
            foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))//排一下序，保证从0-N Write
            {
                var bytes = System.IO.File.ReadAllBytes(part);
                fs.Write(bytes, 0, bytes.Length);
                bytes = null;
                System.IO.File.Delete(part);//删除分块
            }
            fs.Flush();
            fs.Close();
            System.IO.Directory.Delete(dir);//删除文件夹

        }


    }
}
