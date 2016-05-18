using System.IO;
using System.Web.Mvc;

namespace DemoServer.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public JsonResult Index()
        {
            var httpPostedFile = System.Web.HttpContext.Current.Request.Files["file"];

            if (httpPostedFile != null)
            {
                var filename = System.DateTime.Now.ToString("ddMMyyyyhhmmssfff") + "_";
                var path = "~/temp/uploaded/";

                bool exists = Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path));
                if (!exists)
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));

                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(path), filename + httpPostedFile.FileName + Path.GetExtension(httpPostedFile.FileName));
                httpPostedFile.SaveAs(fileSavePath);
                return new JsonResult() { Data = path.Replace("~", string.Empty) + "/" + filename + httpPostedFile.FileName + Path.GetExtension(httpPostedFile.FileName), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            return new JsonResult() { Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}