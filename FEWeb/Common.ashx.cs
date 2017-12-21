using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEWeb
{
    /// <summary>
    /// Common 的摘要说明
    /// </summary>
    public class Common : IHttpHandler
    {
        string result = "";
        public void ProcessRequest(HttpContext context)
        {
            string urlHeader = ConfigHelper.GetConfigString("HttpService").ToString();

            string HttpMethod = context.Request.HttpMethod;
            if (HttpMethod.ToUpper() == "POST")
            {
                int parmLen = context.Request.Form.AllKeys.Length;
                string FirstKey = context.Request.Form.AllKeys[0];
                string FirstParm = context.Request.Form[0];
                string parms = context.Request.Form.ToString();
                PostResult(urlHeader + FirstParm, parms.Substring(FirstParm.Length + FirstKey.Length + 2, parms.Length - FirstParm.Length - FirstKey.Length - 2));
            }
            else
            {
                string url = context.Request.Url.ToString();
                int index = url.IndexOf("PageName");
                url = urlHeader + url.Substring(index + 9, url.Length - index - 9).Replace("ashx&", "ashx?");
                GetResult(url);
            }
            context.Response.Write(result);
            context.Response.End();
        }
        private void PostResult(string url, string Content)
        {
            result = NetHelper.RequestPostUrl(url, Content);
        }
        private void GetResult(string url)
        {
            result = NetHelper.RequestGetUrl(url);
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