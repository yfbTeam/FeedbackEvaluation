using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SSSWeb.Scripts.Webuploader
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {
        public HttpContext Context;
        public void ProcessRequest(HttpContext context)
        {
            Context = context;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            SavePhoto();
            context.Response.End();
        }

        #region 文件保存操作
        private string SavePhoto()
        {
            string result = string.Empty;
            try
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;


                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile File = files[i];
                    string photoName = File.FileName;//获取初始文件名
                    string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                    string onlyName = photoName.Substring(0, photoName.LastIndexOf("."));
                    //if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
                    //{
                    //    result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败,请选择图片文件！\", \"id\" : \"id\"}";
                    //    break;
                    //}
                    string serRootFolder = Context.Server.MapPath("/UploadImgFile");
                    if (!Directory.Exists(serRootFolder))
                    {
                        Directory.CreateDirectory(serRootFolder);
                    }
                    string serfolder = Context.Server.MapPath("/UploadImgFile/AlbumPic");
                    if (!Directory.Exists(serfolder))
                    {
                        Directory.CreateDirectory(serfolder);
                    }
                    string url = "/UploadImgFile/AlbumPic/" + onlyName + "_" + DateTime.Now.ToFileTime().ToString() + photoExt;
                    string fullPath = Context.Server.MapPath(url);
                    File.SaveAs(fullPath);

                }
            }
            catch (Exception ex)
            {
                result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败\", \"id\" : \"id\"}";
            }
            return result;
        }
        #endregion
        public bool IsReusable
        {
            get { return true; }
        }
    }
}