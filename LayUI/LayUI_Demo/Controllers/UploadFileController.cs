using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUI_Demo.Controllers
{
    public class UploadFileController : Controller
    {
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveFile()
        {
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(Server.MapPath("~/App_Data/") + Path.GetFileName(Request.Files[0].FileName));
                return Json("保存成功。", JsonRequestBehavior.AllowGet);
            }
            return Json("没有读到文件。", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SveFile2()
        {
            //保存文件到根目录 App_Data + 获取文件名称和格式
            var filePath = Server.MapPath("~/App_Data/") + Path.GetFileName(Request.Files[0].FileName);
            //创建一个追加（FileMode.Append）方式的文件流
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    //读取文件流
                    BinaryReader br = new BinaryReader(Request.Files[0].InputStream);
                    //将文件留转成字节数组
                    byte[] bytes = br.ReadBytes((int)Request.Files[0].InputStream.Length);
                    //将字节数组追加到文件
                    bw.Write(bytes);
                }
            }
            return Json("保存成功。", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SveFile3()
        {
            var chunk = Request.Form["chunk"];//当前是第多少片 
            var filePath = string.Empty;
            var path = Server.MapPath("~/App_Data/") + Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
            if (!Directory.Exists(path))//判断是否存在此路径，如果不存在则创建
            {
                Directory.CreateDirectory(path);
            }
            //保存文件到根目录 App_Data + 获取文件名称和格式
            if (!string.IsNullOrEmpty(chunk))
            {
                filePath = path + "/" + chunk;
            }
            else
            {
                filePath = path;
            }
           
            //创建一个追加（FileMode.Append）方式的文件流
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        //读取文件流
                        BinaryReader br = new BinaryReader(Request.Files[0].InputStream);
                        //将文件留转成字节数组
                        byte[] bytes = br.ReadBytes((int)Request.Files[0].InputStream.Length);
                        //将字节数组追加到文件
                        bw.Write(bytes);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return Json("保存成功。", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult FileMerge()
        {
            var fileName = Request.Form["fileName"];
            var path = Server.MapPath("~/App_Data/") + Path.GetFileNameWithoutExtension(fileName);

            //这里排序一定要正确，转成数字后排序（字符串会按1 10 11排序，默认10比2小）
            foreach (var filePath in Directory.GetFiles(path).OrderBy(t => int.Parse(Path.GetFileNameWithoutExtension(t))))
            {
                using (FileStream fs = new FileStream(Server.MapPath("~/App_Data/") + fileName, FileMode.Append, FileAccess.Write))
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);//读取文件到字节数组
                    fs.Write(bytes, 0, bytes.Length);//写入文件
                }
                System.IO.File.Delete(filePath);
            }
            Directory.Delete(path);
            return Json(new
            {
                ResultBool = true
            });
        }
    }
}