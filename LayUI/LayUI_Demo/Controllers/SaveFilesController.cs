using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUI_Demo.Controllers
{
    public class SaveFilesController : Controller
    {
        // GET: SaveFiles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveFile1()
        {
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(Server.MapPath("~/App_Data/") + Path.GetFileName(Request.Files[0].FileName));
                return Content("保存成功");
            }
            return Content("没有读到文件");
        }

        public ActionResult SaveFile2()
        {
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(Server.MapPath("~/App_Data/") + Path.GetFileName(Request.Files[0].FileName));
                return Json("保存成功。", JsonRequestBehavior.AllowGet);
            }
            return Json("没有读到文件。", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveFile4()
        {
            //这里发现只能得到一个网络流，没有其他信息了。（比如，文件大小、文件格式、文件名等）
            Request.SaveAs(Server.MapPath("~/App_Data/SaveFile4.data") + "", false);
            return Json("保存成功。", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveFile6()
        {
            //保存文件到根目录 App_Data + 获取文件名称和格式
            var filePath = Server.MapPath("~/App_Data/") + Request.Form["fileName"];
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

        public ActionResult SaveFile8()
        {
            //保存文件到根目录 App_Data + 获取文件名称和格式
            var filePath = Server.MapPath("~/App_Data/") + Request.Form["fileName"];
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(filePath);
                return Json("保存成功。", JsonRequestBehavior.AllowGet);
            }
            return Json("没有读到文件。", JsonRequestBehavior.AllowGet);
        }
    }
}