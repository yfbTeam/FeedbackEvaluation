using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SetMenu
{
    /// <summary>
    /// SetUserPicHandler 的摘要说明
    /// </summary>
    public class SetUserPicHandler : IHttpHandler
    {
        JsonModel jsonModel = new JsonModel();

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            HttpRequest Request = context.Request;
        
         

                string func = RequestHelper.string_transfer(Request, "func");
                try
                {
                    switch (func)
                    {

                        case "UplodeMag": UplodeMag(context); break;
                        case "UplodeSubmit": UplodeSubmit(context); break;
                        default:
                            jsonModel = JsonModel.get_jsonmodel(5, "没有此方法", "");
                            context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
                            break;
                    }

                }


            
            catch (Exception ex)
            {

            }
            finally
            {
               
            }
        }
        public void UplodeMag(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string UniqueNo = RequestHelper.string_transfer(Request, "UniqueNo");
            string url = "";
            string ValueUrl = "";

            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string filename = file.FileName;
                url = "~/UserPic/" + filename;
                string root = HttpContext.Current.Server.MapPath(url);
                file.SaveAs(root);
                ValueUrl = "/UserPic/" + filename;
                
            }
            context.Response.Write(Constant.jss.Serialize(new { redata = ValueUrl }));
        }
        public void UplodeSubmit(HttpContext context) 
        {
            HttpRequest Request = context.Request;
            string UniqueNo = RequestHelper.string_transfer(Request, "UniqueNo");
            string ValueUrl = RequestHelper.string_transfer(Request, "ValueUrl");
            string url = "";
            

           

                UserInfo edit_userInfo = Constant.UserInfo_List.FirstOrDefault(u => u.UniqueNo == UniqueNo);
                UserInfo UserInfo_ = Constant.Clone<UserInfo>(edit_userInfo);
                UserInfo_.HeadPic = ValueUrl;
                //改数据库
                jsonModel = Constant.UserInfoService.Update(UserInfo_);
                if (jsonModel.errNum == 0)
                {
                    //改缓存
                    edit_userInfo.HeadPic = ValueUrl;
                }
            context.Response.Write(Constant.jss.Serialize(new { Result = ValueUrl, redata = ValueUrl }));
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