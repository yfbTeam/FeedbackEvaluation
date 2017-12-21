using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.Report
{
    public class UM
    {
        public string username{get;set;}
        public string userinfo{get;set;}

        public string time { get; set; }

        public string department { get; set; }

        public string class_ { get; set; }

        public string id { get; set; }
        public string state { get; set; }
        
    }
    /// <summary>
    /// ReportHandlerNew 的摘要说明
    /// </summary>
    public class ReportHandlerNew : IHttpHandler
    {
        private static List<UM> CONNECT_POOL1 = new List<UM>();//用户连接池
        private static Dictionary<string, string> CONNECT_POOL = new Dictionary<string, string>();//用户连接池
        private static Dictionary<string, string> MESSAGE_POOL = new Dictionary<string, string>();//离线消息池
        JsonModel jsonModel = new JsonModel();
        public void ProcessRequest(HttpContext context)
        {

            HttpRequest Request = context.Request;
            string func = RequestHelper.string_transfer(Request, "func");
            try
            {
                switch (func)
                {

                    case "SetUserAndInfo": SetUserAndInfo(context); break;
                    case "GetUserAndInfo": GetUserAndInfo(context); break;
                    //SetInfoRead
                    case "SetInfoRead": SetInfoRead(context); break;
                    default:
                        jsonModel = JsonModel.get_jsonmodel(5, "没有此方法", "");
                        context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
                        break;
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                jsonModel = JsonModel.get_jsonmodel(7, "出现异常,请通知管理员", "");
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }



         
        }

        public void GetUserAndInfo(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", CONNECT_POOL1);
            context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
           
        }
        public void SetUserAndInfo(HttpContext context)
        {
            HttpRequest Request = context.Request;

            string user = RequestHelper.string_transfer(Request, "user");
            string info = RequestHelper.string_transfer(Request, "info");
            string UniqueNo = RequestHelper.string_transfer(Request, "UniqueNo");
            string Major_ID = Constant.UserInfo_List.Where(a => a.UniqueNo == UniqueNo).FirstOrDefault().Major_ID;
            string classid = Constant.Class_StudentInfo_List.Where(a => a.UniqueNo == UniqueNo).FirstOrDefault().Class_Id;
            //获取院系名称和班级名称
            Major Major = Constant.Major_List.Where(a => a.Id == Major_ID).FirstOrDefault();
            string Major_Name = Major == null ? "" : Major.Major_Name;
            ClassInfo Class_ = Constant.ClassInfo_List.Where(a => a.ClassNO == classid).FirstOrDefault();
            int intSuccess = (int)errNum.Success;

            string Class_Name = Class_ == null ? null : Class_.Class_Name;

            UM um=new UM();
            um.username=user;
            info = context.Server.UrlDecode(info);
            um.userinfo=info;
            um.department = Major_Name;
            um.class_ = Class_Name;
            um.time = DateTime.Now.ToShortDateString(); ;
            um.id = Guid.NewGuid().ToString();
            um.state = "未读"; 
            CONNECT_POOL1.Add(um);
            jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", CONNECT_POOL1);
            context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
        }
        //SetInfoRead
        public void SetInfoRead(HttpContext context)
        {
            HttpRequest Request = context.Request;
            int intSuccess = (int)errNum.Success;
            string id = RequestHelper.string_transfer(Request, "id");

            UM um = CONNECT_POOL1.Where(i => i.id == id).FirstOrDefault();
            um.state = "已读";
            jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", CONNECT_POOL1);
            context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
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