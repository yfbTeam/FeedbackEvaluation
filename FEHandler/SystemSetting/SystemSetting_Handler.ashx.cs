using FEModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SystemSetting
{
    /// <summary>
    /// SystemSetting_Handler 的摘要说明
    /// </summary>
    public class SystemSetting_Handler : IHttpHandler
    {
        JsonModel jsonModel = new JsonModel();
      

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");


            jsonModel = JsonModel.get_jsonmodel(5, "请到首页进行刷新", "");
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