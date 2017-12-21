using FEBLL;
using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.Email
{
    /// <summary>
    /// EmailHandler 的摘要说明
    /// </summary>
    public class EmailHandler : IHttpHandler
    {
        JsonModel jsonModel = new JsonModel();
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string func = RequestHelper.string_transfer(Request, "func");
            try
            {
                switch (func)
                {
                
                    case "AddLinkMan": AddLinkMan(context); break;
                    case "GetLinkManList": GetLinkManList(context); break;
                
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
        public void AddLinkMan(HttpContext context)
        {

            try
            {
                HttpRequest Request = context.Request;
                string UserUniqueNo = RequestHelper.string_transfer(Request, "UserUniqueNo");
                string LinkManName = RequestHelper.string_transfer(Request, "LinkManName");
                string LinkManDepartMent = RequestHelper.string_transfer(Request, "LinkManDepartMent");
                string LinkManEmail = RequestHelper.string_transfer(Request, "LinkManEmail");
                string LinkManRemarks = RequestHelper.string_transfer(Request, "LinkManRemarks");


                LinkManInfo LinkManInfo_ = new LinkManInfo();
                LinkManInfo_.UserUniqueNo = UserUniqueNo;
                LinkManInfo_.LinkManName = LinkManName;
                LinkManInfo_.LinkManDepartMent = LinkManDepartMent;
                LinkManInfo_.LinkManEmail = LinkManEmail;
                LinkManInfo_.LinkManRemarks = LinkManRemarks;
                LinkManInfo_.IsDelete = 0;
                //改数据库
                jsonModel = new LinkManInfoService().Add(LinkManInfo_);
                Constant.LinkManInfo_List.Add(LinkManInfo_); ;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void GetLinkManList(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string UserUniqueNo = RequestHelper.string_transfer(Request, "UserUniqueNo");

                var query = from LM in Constant.LinkManInfo_List
                            where LM.UserUniqueNo == UserUniqueNo
                            select new
                            {
                               
                                LinkManName = LM.LinkManName,
                             
                                LinkManDepartMent = LM.LinkManDepartMent,
                               
                                LinkManEmail = LM.LinkManEmail,
                             
                                LinkManRemarks = LM.LinkManRemarks,
                            };
                int a = query.Count();
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
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