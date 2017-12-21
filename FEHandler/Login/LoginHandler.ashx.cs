using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.Login
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler
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
                    //获取学年学期
                    case "Login": Login(context); break;
                    //教师
                    case "IsTeacher": IsTeacher(context); break;
                    //学生
                    case "IsStudent": IsStudent(context); break;
                    //获取指标库
                    //case "Get_Indicator": Get_Indicator(context); break;
                    //新增指标库
                    //case "Add_Indicator": Add_Indicator(context); break;
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

        private void IsStudent(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string UniqueNoId = RequestHelper.string_transfer(Request, "UniqueNo");
                //返回所有用户信息
                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                List<Student> Student_List = Constant.Student_List;
                var query = from UserInfo in UserInfo_List_
                            where UserInfo.UniqueNo == UniqueNoId.Trim()
                            join Student in Student_List on UserInfo.UniqueNo.Trim() equals Student.UniqueNo.Trim()
                            select new
                            {
                                LoginName = UserInfo.LoginName,
                                Sys_Role = "学生",
                                Sys_Role_Id = 2,
                                UniqueNo = UserInfo.UniqueNo,
                                UserType = UserInfo.UserType,
                                Name = UserInfo.Name,
                                HeadPic = UserInfo.HeadPic,
                                Phone = UserInfo.Phone,
                                Email = UserInfo.Email,
                                Major_ID = UserInfo.Major_ID
                            };
                if (query.Count() == 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "fail", query);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query.FirstOrDefault());
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

        private void IsTeacher(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string UniqueNoId = RequestHelper.string_transfer(Request, "UniqueNo");
                //返回所有用户信息
                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                List<Teacher> Teacher_List = Constant.Teacher_List;
                var query = from UserInfo in UserInfo_List_
                             where UserInfo.UniqueNo == UniqueNoId.Trim()
                             join Teacher in Teacher_List on UserInfo.UniqueNo.Trim() equals Teacher.UniqueNo.Trim()
                             select new
                             {
                                 LoginName = UserInfo.LoginName,
                                 Sys_Role = "教师",
                                 Sys_Role_Id = 3,
                                 UniqueNo = UserInfo.UniqueNo,
                                 UserType = UserInfo.UserType,
                                 Name = UserInfo.Name,
                                 HeadPic = UserInfo.HeadPic,
                                 Phone = UserInfo.Phone,
                                 Email = UserInfo.Email,
                                 Major_ID = UserInfo.Major_ID
                             };
                if (query.Count() == 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "fail", query);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query.FirstOrDefault());
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
        public void Login(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string Name = RequestHelper.string_transfer(Request, "loginName");
                string PassWord = RequestHelper.string_transfer(Request, "passWord");
                //返回所有用户信息
                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                List<Sys_Role> Sys_Role_List = Constant.Sys_Role_List;
                List<Sys_RoleOfUser> Sys_RoleOfUser_List = Constant.Sys_RoleOfUser_List;
                var query = (from UserInfo in UserInfo_List_
                             where Name.Trim() == UserInfo.LoginName.Trim() && PassWord.Trim() == UserInfo.ClearPassword.Trim()
                             join Sys_RoleOfUser in Sys_RoleOfUser_List on UserInfo.UniqueNo.Trim() equals Sys_RoleOfUser.UniqueNo.Trim()
                             join Sys_Role in Sys_Role_List on Sys_RoleOfUser.Role_Id equals Sys_Role.Id
                             select new
                             {
                                 LoginName = UserInfo.LoginName,
                                 Sys_Role = Sys_Role.Name,
                                 Sys_Role_Id = Sys_Role.Id,
                                 UniqueNo = UserInfo.UniqueNo,
                                 UserType = UserInfo.UserType,
                                 Name = UserInfo.Name,
                                 HeadPic = UserInfo.HeadPic,
                                 Phone = UserInfo.Phone,
                                 Email = UserInfo.Email,
                                 Major_ID = UserInfo.Major_ID
                             }).ToList();
                if (query.Count() == 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "fail", query);
                }
                else
                {
                    //多个角色
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query);
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