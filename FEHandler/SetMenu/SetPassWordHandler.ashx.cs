using FEBLL;
using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;


namespace FEHandler.SetMenu
{
    /// <summary>
    /// SetPassWordHandler 的摘要说明
    /// </summary>
    public class SetPassWordHandler : IHttpHandler
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

                    case "SetPassWord": SetPassWord(context); break;
                    case "SetUserInfo": SetUserInfo(context); break;
                    case "InsertUserInfo": InsertUserInfo(context); break;
                    case "SendEmail": SendEmail(context); break;
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

        public void SendEmail(HttpContext context)
        {
          
            try
            {
                  HttpRequest Request = context.Request;
                  string Recipient_address = RequestHelper.string_transfer(Request, "Recipient_address");
                  //string Sender_address = RequestHelper.string_transfer(Request, "Sender_address");
                  string Sender_name = RequestHelper.string_transfer(Request, "Sender_name");
                  string Email_Title = RequestHelper.string_transfer(Request, "Email_Title");
                  string Email_Body = RequestHelper.string_transfer(Request, "Email_Body");

                  string Sender_address="gthtest01@163.com";
                  string pwd = "gthtest01";
                  //string Sender_name = "系统";
                  Email_Body = context.Server.UrlDecode(Email_Body);
                MailMessage msg = new MailMessage();

                foreach(string i in Recipient_address.Split(';'))
                {
                    if (i != "") { msg.To.Add(i); }//收件人地址  
                }
                
                //msg.CC.Add("cc@qq.com");//抄送人地址  

                msg.From = new MailAddress(Sender_address, Sender_name);//发件人邮箱，名称  

                msg.Subject = Email_Title;//邮件标题  
                msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8  

                msg.Body = Email_Body;//邮件内容  
                msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8  
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();

                client.Host = "smtp.163.com";//SMTP服务器地址  
                client.Port = 25;//SMTP端口，QQ邮箱填写587  

                client.EnableSsl = true;//启用SSL加密  

                client.Credentials = new NetworkCredential(Sender_address, pwd);//发件人邮箱账号，密码  

                client.Send(msg);//发送邮件  
                jsonModel.errMsg = "success";
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
        public void SetUserInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                string UserName = RequestHelper.string_transfer(Request, "UserNamae");
                string TelePhone = RequestHelper.string_transfer(Request, "TelePhone");
                string UniqueNo = RequestHelper.string_transfer(Request, "Userid");
                UserInfo edit_userInfo = Constant.UserInfo_List.FirstOrDefault(u => u.UniqueNo == UniqueNo);
            
                    UserInfo UserInfo_ = Constant.Clone<UserInfo>(edit_userInfo);
                    UserInfo_.Name = UserName;
                    UserInfo_.Phone = TelePhone;
                    //改数据库
                    jsonModel = Constant.UserInfoService.Update(UserInfo_);
                    if (jsonModel.errNum == 0)
                    {
                        //改缓存
                        edit_userInfo.Name = UserName;
                        edit_userInfo.Phone = TelePhone;
                    }
                
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
        public void InsertUserInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
              
                string Name = RequestHelper.string_transfer(Request, "Name");
                string UserType = RequestHelper.string_transfer(Request, "UserType");
                string PassWord = RequestHelper.string_transfer(Request, "PassWord");
                string Phone = RequestHelper.string_transfer(Request, "Phone");
                string Email = RequestHelper.string_transfer(Request, "Email");
                //OldPassWord

      //          [UniqueNo],[UserType],[Name],[Sex],[LoginName],[Phone],[Email] ,[CreateUID]
      //,[CreateTime]
      //,[EditUID]
      //,[EditTime]
      //,[IsEnable]
      //,[IsDelete]
               
                    UserInfo UserInfo_ = new UserInfo();
                    UserInfo_.UniqueNo = Guid.NewGuid().ToString();
                    UserInfo_.UserType = 1;
                    UserInfo_.Name = Name;
                    UserInfo_.Sex = 1;
                    UserInfo_.LoginName = Name;
                    UserInfo_.Phone = Phone;
                    UserInfo_.Email = Email;
                    UserInfo_.CreateUID = "1";
                    UserInfo_.CreateTime = DateTime.Now;
                    UserInfo_.EditUID = "1";
                    UserInfo_.EditTime = DateTime.Now;
                    UserInfo_.IsEnable = 0;
                    UserInfo_.IsDelete = 0;
                    UserInfo_.ClearPassword = "123456";
                    UserInfo_.Password = "123456";
                    //改数据库
                    jsonModel = new UserInfoService().Add(UserInfo_);
                    Constant.UserInfo_List.Add(UserInfo_);;
                
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
        public void SetPassWord(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                string UniqueNo = RequestHelper.string_transfer(Request, "Userid");
                string NewPassWord = RequestHelper.string_transfer(Request, "NewPassWord");
                //OldPassWord
                string OldPassWord = RequestHelper.string_transfer(Request, "OldPassWord");
                UserInfo edit_userInfo = Constant.UserInfo_List.FirstOrDefault(u => u.UniqueNo == UniqueNo);
                if (edit_userInfo.ClearPassword != OldPassWord)
                {
                    jsonModel.errMsg = "ClearPasswordError";
                }
                else
                {
                    UserInfo UserInfo_ = Constant.Clone<UserInfo>(edit_userInfo);
                    UserInfo_.ClearPassword = NewPassWord;
                    //改数据库
                    jsonModel = Constant.UserInfoService.Update(UserInfo_);
                    if (jsonModel.errNum == 0)
                    {
                        //改缓存
                        edit_userInfo.ClearPassword = NewPassWord;
                    }
                }
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