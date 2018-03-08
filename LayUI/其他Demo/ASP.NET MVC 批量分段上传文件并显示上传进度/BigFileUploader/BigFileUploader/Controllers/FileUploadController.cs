using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigFileUploader.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SubmitForm(HttpPostedFileBase fileData, FormCollection collection)
        {
            string name = collection["name"];
            string passowrd = collection["password"];
            string image = fileData.FileName.ToString();
            string imageGuid = Guid.NewGuid().ToString() + ".jpg";
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Upload/") + imageGuid;//只是用于演示简单的上传删除方法
            fileData.SaveAs(path);
            
        }
    }
}