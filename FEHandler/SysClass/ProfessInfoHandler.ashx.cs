using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SysClass
{
    /// <summary>
    /// ProfessInfoHandler 的摘要说明
    /// </summary>
    public class ProfessInfoHandler : IHttpHandler
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
                    //获取所有课程
                    case "GetProfessInfo": GetProfessInfo(context); break;

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
        public void GetProfessInfo(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {

                //var query1 = from ClassInfo_ in Constant.ClassInfo_List
                //             join Major_ in Constant.Major_List on ClassInfo_.Major_Id equals Major_.Id
                //             select new
                //             {
                //                 Major_Name = Major_.Major_Name,
                //             };
                //int index = query1.Count();
                //var query = from p in query1
                //            group p by p.Major_Name into allg
                //            select new
                //            {
                //                College_Name = allg.Max(p => p.Major_Name),
                //                //课程分类id
                //                College_Id = "1",
                //            };
                var query = from Major_ in Constant.Major_List
                            select new
                            {
                                College_Name = Major_.Major_Name,
                                Id =  Major_.Id,
                            };
                

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