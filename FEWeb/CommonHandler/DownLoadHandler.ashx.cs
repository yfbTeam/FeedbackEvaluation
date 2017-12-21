using FEUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FEWeb.CommonHandler
{
    /// <summary>
    /// DownLoadHandler 的摘要说明
    /// </summary>
    public class DownLoadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string filepath = RequestHelper.string_transfer(context.Request, "filepath");
            try
            {
                int index = filepath.LastIndexOf('\\') + 1;
                filepath = context.Request.MapPath(filepath);
                string filename = System.IO.Path.GetFileName(filepath);
                int charindex = filename.LastIndexOf('_');//适用于有时间戳的文件
                filename = charindex > -1 ? filename.Substring(0, charindex) + Path.GetExtension(filepath) : filename;
                if (filename.IndexOf(" ") != -1)
                {
                    filename = filename.Replace(" ", "_");
                }
                filename = context.Server.UrlDecode(filename);
                FileInfo info = new FileInfo(filepath);
                long filesize = info.Length;
                context.Response.Clear();
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.ContentType = "application/octet-stream";
                //火狐浏览器不需将中文文件名进行编码格式转换
                if (context.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") == -1)
                {
                    filename = HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);
                }
                context.Response.AddHeader("Content-Disposition", "attachement;filename=" + filename.ToString());
                context.Response.AddHeader("Content-Length", filesize.ToString());
                context.Response.ContentType = "application/ms-word";//指定返回的是一个不能被客户端读取的流，必须被下载 
                context.Response.WriteFile(filepath, 0, filesize);
                context.Response.Flush();
                context.Response.Close();
            }
            catch (Exception ex)
            {
                context.Response.Write("-1");
                context.Response.End();
            }
        }
        //处理文件名中出现的空格    
        //其中%20是空格在UTF8下的编码  
        public static string encodingFileName(string fileName)
        {
            String returnFileName = "";
            try
            {
                returnFileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
                returnFileName = returnFileName.Replace("+", "%20");
                if (returnFileName.Length > 150)
                {

                    //returnFileName = new string(fileName.GetBytes("GB2312"), "ISO8859-1");
                    Encoding ISO88591Encoding = Encoding.GetEncoding("ISO-8859-1");
                    Encoding GB2312Encoding = Encoding.GetEncoding("GB2312");
                    byte[] srcBytes = ISO88591Encoding.GetBytes(returnFileName);
                    byte[] dstBytes = Encoding.Convert(GB2312Encoding, ISO88591Encoding, srcBytes);
                    char[] dstChars = new char[ISO88591Encoding.GetCharCount(dstBytes, 0, dstBytes.Length)];
                    ISO88591Encoding.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);
                    returnFileName = new string(dstChars);
                    returnFileName = returnFileName.Replace(" ", "%20");
                }
            }
            catch (Exception ex)
            {
                return fileName;
            }
            return returnFileName;
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