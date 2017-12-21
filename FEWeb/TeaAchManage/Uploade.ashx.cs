using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FEUtility;
using System.Web.Script.Serialization;
namespace FEWeb.TeaAchManage
{
    /// <summary>
    /// Uploade 的摘要说明
    /// </summary>
    public class Uploade : IHttpHandler
    {
        public static JavaScriptSerializer jss = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Func = context.Request["Func"].SafeToString();
            switch (Func)
            {
                case"AchImage":
                    AchImage(context);
                    break;
                default:
                    break;
            }
        }
        public void AchImage(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string UniqueNo = RequestHelper.string_transfer(Request, "UniqueNo");
            string url = "";
            string ValueUrl = "";
            string FoldUrl = "/FileManage/AchImage/";
            if (context.Request.Files.Count > 0)
            {
                if (!FileHelper.IsExistDirectory(context.Server.MapPath(FoldUrl)))
                {
                    FileHelper.CreateDirectory(context.Server.MapPath(FoldUrl));
                }
                HttpPostedFile file = context.Request.Files[0];
                string filename = file.FileName;
                url = FoldUrl + filename;
                string root =context.Server.MapPath(url);
                file.SaveAs(root);

            }
            context.Response.Write(jss.Serialize(new { url = url }));
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