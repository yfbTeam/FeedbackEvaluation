using FEBLL;
using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SystemSetting
{
    /// <summary>
    /// Sys_LetterHandler 的摘要说明
    /// </summary>
    public class Sys_LetterHandler : IHttpHandler
    {
       
        Sys_LetterService letter_bll = new Sys_LetterService();
        JsonModel jsonModel = JsonModel.get_jsonmodel(0, "success", "");
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"];
            string result = string.Empty;
            try
            {
                switch (func)
                {                                    
                    case "GetSys_LetterData":
                        GetSys_LetterData(context);
                        break;
                    case "GetSys_LetterById":
                        GetSys_LetterById(context);
                        break;
                    case "Send_Letter":
                        Send_Letter(context);
                        break;                                        
                    default:
                        jsonModel = JsonModel.get_jsonmodel(5, "没有此方法", "");
                        break;
                }
                LogService.WriteLog(func);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                jsonModel = JsonModel.get_jsonmodel(7, "出现异常,请通知管理员", "");
            }
            finally
            {
                result = "{\"result\":" + Constant.jss.Serialize(jsonModel) + "}";
                context.Response.Write(result);
                context.Response.End();
            }
        }
       

     

        #region 获取站内信的分页数据
        private void GetSys_LetterData(HttpContext context)
        {
            try
            {
                int id = RequestHelper.int_transfer(context.Request, "Id");
                string loginUID = RequestHelper.string_transfer(context.Request, "LoginUID");
                string receiveUID = RequestHelper.string_transfer(context.Request, "ReceiveUID");                 
                List<Sys_Letter> let_Select = Constant.Sys_Letter_List;
                if (id != 0)
                {
                    let_Select = let_Select.Where(t => t.Id == id).ToList();
                }
                if (!string.IsNullOrEmpty(loginUID)) 
                {
                    let_Select= let_Select.Where(t=>t.CreateUID==loginUID).ToList();
                }
                if (!string.IsNullOrEmpty(receiveUID))
                {
                    let_Select = let_Select.Where(t => t.ReceiveUID == receiveUID).ToList();
                }                
                var list = let_Select.ToList();
                if (list.Count > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(0, "success", list);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
                }
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion

        #region 根据Id获取站内信详情
        private void GetSys_LetterById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            var let_Select = (from lett in Constant.Sys_Letter_List where lett.Reply_Id == itemid && lett.Id <= itemid select lett);
            jsonModel = JsonModel.get_jsonmodel(0, "success", let_Select.ToList()); ;
        }
        #endregion 

        #region 发送站内信
        private void Send_Letter(HttpContext context)
        {
            Sys_Letter item = new Sys_Letter();
            string Major_Id = RequestHelper.string_transfer(context.Request, "Major_Id");
            int reply_Id = RequestHelper.int_transfer(context.Request, "Reply_Id");//为0表示第一次发送，否则为回复
            item.Id = 0;
            item.Title = context.Request["Title"];
            item.Contents = HttpUtility.UrlDecode(context.Request["Contents"]);
            item.ReceiveUID = context.Request["ReceiveUID"];
            item.ReceiveName = context.Request["ReceiveName"];
            item.Reply_Id = reply_Id;
            item.IsRead = 0;
            item.CreateUID = context.Request["LoginUID"];
            item.CreateTime = DateTime.Now;
            item.IsDelete = 0;
            jsonModel = letter_bll.Send_Letter(item, Major_Id);
            if (jsonModel.errNum == 0)
            {                
                //Constant.Eva_Class_Allot_List.RemoveAll(t=>t.Major_Id==Major_Id);//删除教学信息员分配信息
                item.Id = Convert.ToInt32(jsonModel.retData);
                if (reply_Id == 0) //修改回复Id
                {
                    item.Reply_Id = item.Id;                   
                }
                if (!Constant.Sys_Letter_List.Contains(item))
                {
                    Constant.Sys_Letter_List.Add(item);
                }
            }
        }
        #endregion      
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}