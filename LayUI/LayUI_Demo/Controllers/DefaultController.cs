using BLL;
using Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIHelper;

namespace LayUI_Demo.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 存放文件的物理根路径
        /// </summary>
        string RootFolder = "files";
        string UPLOADFILE_SERVICE_URL = "http://.....";//最终接收文件上传的服务接口
        
        public ActionResult RequestSave1(HttpContext context)
        {
            string temSavePath = context.Server.MapPath(@"~\") + RootFolder;
            if (!Directory.Exists(temSavePath))
            {
                Directory.CreateDirectory(temSavePath);
            }
            if (context.Request.Files != null && context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string saveFileName = String.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddhhmmssffff"), Path.GetExtension(file.FileName));//保存文件名称
                string fileName = String.Format(@"{0}\{1}", temSavePath, saveFileName);
                file.SaveAs(fileName);
                MsMultiPartFormData form = new MsMultiPartFormData();
                string decodeName = HttpUtility.UrlDecode(Path.GetFileName(fileName));//最终服务器会按原文件名保存文件，所以需要将文件名编码下，防止中文乱码
                string rst = PostDataServer.HttpPostData(UPLOADFILE_SERVICE_URL, form, fileName, decodeName);
                context.Response.Write(rst);//输出上传文件返回结果
                context.Response.End();
            }
            return null;
        }

        #region 关键字搜索
        public string swhere(string keyWord)
        {
            string cond = "1=1";
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = UIHelper.Tool.GetSafeSQL(keyWord);
                cond += $" and title like '%{keyWord}%'";
            }
            return cond;
        }
        #endregion

        #region 分页获取数据显示
        /// <summary>
        /// TableRender 渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult TableByRender()
        {
            return View();
        }
        [ActionName("NewIndex")]
        public ActionResult Index(string keyWord, int limit, int page)
        {
            string cond = swhere(keyWord);
            var pageData = VideoDAL.GetVideoPageList("*", "id desc", limit, page, cond);
            var result = new LayPadding<VideoMDL>()
            {
                code = 0,
                msg = "success",
                data = pageData.data,
                count = VideoDAL.SelectCount(cond)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// tmpl模板引擎渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult TableByTmpl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TableByTmpl(int limit, int page, string keyWord)
        {
            string cond = swhere(keyWord);
            var pageData = VideoDAL.GetVideoPageList("*", "id desc", limit, page, cond);
            var result = new LayPadding<VideoMDL>()
            {
                code = 0,
                msg = "success",
                data = pageData.data,
                count = VideoDAL.SelectCount(cond)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 拼接Table
        /// </summary>
        /// <returns></returns>
        public ActionResult TableByJson()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TableByJson(int limit, int page, string keyWord)
        {
            string cond = swhere(keyWord);
            var pageData = VideoDAL.GetVideoPageList("*", "id desc", limit, page, cond);
            var result = new LayPadding<VideoMDL>()
            {
                code = 0,
                msg = "success",
                data = pageData.data,
                count = VideoDAL.SelectCount(cond)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// X.PagedList插件渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult TableByChaJ(int? page)
        {
            int PageNumber = page ?? 1;//page为null时默认值为1
            int PageSize = 5;
            ViewData["page"] = PageNumber;

            var test = VideoDAL.GetListPage().ToPagedList(PageNumber, PageSize);
            return View(test);
        }
        #endregion

        #region 添加 or 查看
        [HttpGet]
        public ActionResult FormShow()
        {
            return View();
        }
        #endregion

        #region 编辑显示数据
        [HttpPost]
        public ActionResult GetForm()
        {
            int Id = Convert.ToInt32(Request["primaryKey"]);
            var entity = VideoDAL.GetObject(Id);
            return Content(entity.ToJson());
        }
        #endregion

        #region 编辑保存 or 添加保存
        [HttpPost]
        public ActionResult RequestSave(VideoMDL model)
        {
            HttpPostedFileBase pathfile = HttpContext.Request.Files["img"] as HttpPostedFileBase;
            string path = "/Upload/MyFile";
            //获取上传目录 转换为物理路径
            string uploadPath = Server.MapPath(path);
            //判断目录是否存在
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            //保存文件的物理路径
            string saveFile = uploadPath + pathfile.FileName;
            //保存图片到服务器
            try
            {
                pathfile.SaveAs(saveFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            int res = 0;
            model.img = "../../Upload/Movies_Img/3.jpg";
            if (string.IsNullOrEmpty(Convert.ToString(model.id)))
            {
                model.img = "../../"+ pathfile.FileName;
                res = VideoDAL.Insert(model);
            }
            else {
                model.img = "../../" + pathfile.FileName;
                res = VideoDAL.Update(model);
            }
            var msg = res > 0 ? "OK" : "Fail";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 删除
        [HttpPost]
        public ActionResult Delete(string primaryKey)
        {
            var res = VideoDAL.Delete(primaryKey.ToStrArray());
            var msg = res > 0 ? "删除成功" : "删除失败";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}