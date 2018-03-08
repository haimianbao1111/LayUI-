using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PageOffice4_MVC_Razor.Models;
using System.Web.UI;
using System.Text;
using System.IO;

namespace PageOffice4_MVC_Razor.Controllers
{
    public class WordController : Controller
    {
        // GET: Word
        public ActionResult Index()
        {
            return View();
        }

        public void SaveDoc()
        {
            ViewBag.Message = "Your app description page.";
            string filePath = Server.MapPath("/test.doc");
            PageOffice.FileSaver fs = new PageOffice.FileSaver();
            fs.SaveToFile(filePath);
            fs.Close();

        }

        public ActionResult Word()
        {
            ViewBag.Message = "Your contact page.";

            PageOffice.PageOfficeCtrl pc = new PageOffice.PageOfficeCtrl();
            pc.AddCustomToolButton("保存", "Save()", 1);
            pc.SaveFilePage = "/Word/SaveDoc";
            pc.ServerPage = "/pageoffice/server.aspx";
       
            pc.WebOpen("/test.doc", PageOffice.OpenModeType.docAdmin, "s");

            ViewBag.EditorHtml = pc.GetHtmlCode("PageOfficeCtrl1");

            return View();
        }
    }
}