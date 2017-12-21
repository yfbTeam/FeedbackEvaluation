using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.UserMan
{
    /// <summary>
    /// UserManHandler 的摘要说明
    /// </summary>
    public class UserManHandler : IHttpHandler
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
                    case "Get_UserInfo_List": Get_UserInfo_List(context); break;
                    //操作用户组
                    case "Ope_UserGourp": Ope_UserGourp(context); break;

                    //获取指标库
                    case "Get_UserGroup": Get_UserGroup(context); break;
                    //获取用户信息【根据类型】
                    case "GetUserByType": GetUserByType(context); break;
                    //获取用户信息【根据类型】+课程【教师组】
                    case "GetUserByType_Course": GetUserByType_Course(context); break;
                    case "SetUserToRole": SetUserToRole(context); break;
                    case "GetTeachers": GetTeachers(context); break;
                    case "GetStudents": GetStudents(context); break;
                    case "GetMajors": GetMajors(context); break;
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
        #region 部门
        public void GetMajors(HttpContext context)
        {
            List<Major> Major_List = Constant.Major_List;
            int intSuccess = (int)errNum.Success;
            jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", Major_List);
            context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
        }
        #endregion

        public void SetUserToRole(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string UniqueNos = RequestHelper.string_transfer(Request, "UniqueNo");
                int Roleid = RequestHelper.int_transfer(Request, "Roleid");
                string[] us = Split_Hepler.str_to_stringss(UniqueNos);

                //-----------------------------------------------------------------------
                //先把选中的 角色进行判断性的变更【若不为改类型则设置为该类型】
                foreach (var unique in us)
                {
                    Sys_RoleOfUser roleofuser = Constant.Sys_RoleOfUser_List.FirstOrDefault(r => r.UniqueNo == unique);
                    if (roleofuser != null && roleofuser.Role_Id != Roleid)
                    {
                        Sys_RoleOfUser RoleOfUser_Clone = Constant.Clone<Sys_RoleOfUser>(roleofuser);
                        RoleOfUser_Clone.Role_Id = Roleid;
                        //改数据库
                        JsonModel m1 = Constant.Sys_RoleOfUserService.Update(RoleOfUser_Clone);
                        if (m1.errNum == 0)
                        {
                            //改缓存
                            roleofuser.Role_Id = Roleid;
                        }
                    }
                }

                //-----------------------------------------------------------------------
                //若不为改类型则进行剔除（获取已存在的该类型角色 查看这次是否剔除）
                var roles = (from r in Constant.Sys_RoleOfUser_List where r.Role_Id == Roleid select r).ToList();
                foreach (Sys_RoleOfUser role in roles)
                {
                    //Sys_RoleOfUser Sys_RoleOfUser_=new Sys_RoleOfUser();
                    //Sys_RoleOfUser_.UniqueNo=
                    if (!us.Contains(role.UniqueNo))
                    {
                        Teacher teacher = Constant.Teacher_List.FirstOrDefault(t => t.UniqueNo == role.UniqueNo);
                        Student student = Constant.Student_List.FirstOrDefault(s => s.UniqueNo == role.UniqueNo);

                        int roleID = 2;
                        if (teacher != null)
                        {
                            roleID = 3;
                        }
                        else if (student != null)
                        {
                            roleID = 2;
                        }
                        Sys_RoleOfUser edit_RoleOfUser = Constant.Sys_RoleOfUser_List.FirstOrDefault(u => u.UniqueNo == role.UniqueNo);
                        if (edit_RoleOfUser != null)
                        {
                            Sys_RoleOfUser role_u_clone = Constant.Clone<Sys_RoleOfUser>(edit_RoleOfUser);
                            role_u_clone.Role_Id = roleID;
                            //改数据库
                            JsonModel m2 = Constant.Sys_RoleOfUserService.Update(role_u_clone);
                            if (m2.errNum == 0)
                            {
                                //改缓存
                                edit_RoleOfUser.Role_Id = roleID;
                            }

                        }
                    }
                }

                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", "0");

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
        public void Get_UserGroup(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                //返回所有用户信息

                List<Sys_Role> Sys_Role_List = Constant.Sys_Role_List;
                var query = from srl in Sys_Role_List
                            where srl.Name != "超级管理员"
                            orderby srl.Sort
                            select new
                            {
                                RoleId = srl.Id,
                                RoleName = srl.Name,
                                Code = srl.Code,

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
            //LogHelper.Info("Get_UserGroup:结束");
        }

        #region 添加\编辑\删除 用户组

        static object Ope_UserGourp_Obj = new object();

        public void Ope_UserGourp(HttpContext context)
        {
            lock (Ope_UserGourp_Obj)
            {
                try
                {
                    HttpRequest Request = context.Request;
                    //返回所有用户信息

                    int Id = RequestHelper.int_transfer(Request, "Id");
                    string Name = RequestHelper.string_transfer(Request, "Name");
                    int Type = RequestHelper.int_transfer(Request, "Type");//1：添加   2：编辑 3：删除
                    string UniqueNo = RequestHelper.string_transfer(Request, "UniqueNo");
                    switch (Type)
                    {
                        case 1:
                            User_Gourp_Add_Helper(Name, UniqueNo);

                            break;
                        case 2:
                            User_Gourp_Edit_Helper(Id, Name, UniqueNo);

                            break;
                        case 3:
                            User_Gourp_Delete_Helper(Id, UniqueNo);

                            break;
                        default:
                            break;
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
        }

        private void User_Gourp_Edit_Helper(int Id, string Name, string UniqueNo)
        {
            try
            {

                if (Id > 0)
                {
                    //编辑
                    var role_edit = Constant.Sys_Role_List.FirstOrDefault(r => r.Id == Id);
                    if (role_edit != null)
                    {
                        var role_Clone = Constant.Clone<Sys_Role>(role_edit);
                        if (role_Clone != null)
                        {
                            role_Clone.Name = Name;
                            role_Clone.EditTime = DateTime.Now;
                            role_Clone.EditUID = UniqueNo;
                            jsonModel = Constant.Sys_RoleService.Update(role_Clone);
                            if (jsonModel.errNum == 0)
                            {
                                role_edit.Name = Name;
                                role_edit.EditTime = DateTime.Now;
                                role_edit.EditUID = UniqueNo;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void User_Gourp_Delete_Helper(int Id, string UniqueNo)
        {
            try
            {
                if (Id > 0)
                {
                    //编辑
                    var role_edit = Constant.Sys_Role_List.FirstOrDefault(r => r.Id == Id);
                    if (role_edit != null)
                    {
                        var r_Count = Constant.Sys_RoleOfUser_List.Count(i => i.Role_Id == Id);//角色成员
                        if (r_Count == 0)
                        {
                            var role_Clone = Constant.Clone<Sys_Role>(role_edit);
                            if (role_Clone != null)
                            {
                                role_Clone.IsDelete = 1;
                                role_Clone.EditUID = UniqueNo;
                                role_Clone.EditTime = DateTime.Now;
                                jsonModel = Constant.Sys_RoleService.Update(role_Clone);
                                if (jsonModel.errNum == 0)
                                {
                                    role_edit.IsDelete = 1;
                                    role_edit.EditUID = UniqueNo;
                                    role_edit.EditTime = DateTime.Now;
                                    Constant.Sys_Role_List.Remove(role_edit);
                                }
                            }
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "已分配成员不可删除", 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void User_Gourp_Add_Helper(string Name, string UniqueNo)
        {
            try
            {
                //排序
                var sortMax = Constant.Sys_Role_List.Max(r => r.Sort);
                //添加用户组
                Sys_Role role_new = new Sys_Role()
                {
                    Code = "0",// 0:非固化[可编辑 删除] 1:固化
                    CreateUID = UniqueNo,
                    CreateTime = DateTime.Now,
                    IsDelete = 0,
                    Name = Name,
                    Sort = sortMax + 1,
                };
                jsonModel = Constant.Sys_RoleService.Add(role_new);
                if (jsonModel.errNum == 0)
                {
                    role_new.Id = Convert.ToInt32(jsonModel.retData);
                    Constant.Sys_Role_List.Add(role_new);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        //获取用户信息
        public void Get_UserInfo_List(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                //返回所有用户信息

                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                List<Sys_Role> Sys_Role_List = Constant.Sys_Role_List;
                List<Sys_RoleOfUser> Sys_RoleOfUser_List = Constant.Sys_RoleOfUser_List;
                List<Major> Major_List = Constant.Major_List;
                //LogHelper.Info("Get_UserInfo_List:开始");
                var query = from ul in UserInfo_List_
                            join SysRoleOfUser in Sys_RoleOfUser_List on ul.UniqueNo equals SysRoleOfUser.UniqueNo
                            join SysRole in Sys_Role_List on SysRoleOfUser.Role_Id equals SysRole.Id
                            //join SysMajor in Major_List on ul.Major_ID equals SysMajor.Id
                            where SysRole.Name != "超级管理员"
                            orderby SysRole.Sort
                            select new
                 {
                     id = ul.Id,
                     Name = (ul.Name == null) ? string.Empty : ul.Name,
                     Sex = GetSex(Convert.ToString(ul.Sex)),
                     //Department_Name = (ul.Department_Name == null) ? string.Empty : ul.Department_Name,
                     //Major_Name = (ul.Major_Name == null) ? string.Empty : ul.Major_Name,
                     //College_Name = (ul.College_Name == null) ? string.Empty : ul.College_Name,
                     LoginName = (ul.LoginName == null) ? string.Empty : ul.LoginName,
                     Phone = (ul.Phone == null) ? string.Empty : ul.Phone,
                     Email = (ul.Email == null) ? string.Empty : ul.Email,
                     UserType = ul.UserType,
                     Roleid = SysRole.Id,
                     RoleName = SysRole.Name,
                     UniqueNo = ul.UniqueNo,
                     Pwd = ul.ClearPassword,
                     //MajorName=SysMajor.Major_Name
                     MajorName = "",
                     Major_ID = ul.Major_ID
                 };

                var query1 = from q in query
                             join SysMajor in Major_List on q.Major_ID equals SysMajor.Id
                             into gj
                             from lf in gj.DefaultIfEmpty()

                             select new
                             {
                                 id = q.id,
                                 Name = (q.Name == null) ? string.Empty : q.Name,
                                 Sex = q.Sex,
                                 //Department_Name = (ul.Department_Name == null) ? string.Empty : ul.Department_Name,
                                 //Major_Name = (ul.Major_Name == null) ? string.Empty : ul.Major_Name,
                                 //College_Name = (ul.College_Name == null) ? string.Empty : ul.College_Name,
                                 LoginName = (q.LoginName == null) ? string.Empty : q.LoginName,
                                 Phone = (q.Phone == null) ? string.Empty : q.Phone,
                                 Email = (q.Email == null) ? string.Empty : q.Email,
                                 UserType = q.UserType,
                                 Roleid = q.Roleid,
                                 RoleName = q.RoleName,
                                 UniqueNo = q.UniqueNo,
                                 Pwd = q.Pwd,
                                 //MajorName=SysMajor.Major_Name
                                 MajorName = (lf == null ? "" : lf.Major_Name),
                                 Major_ID = q.Major_ID
                             };



                int count = query1.Count();

                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query1);
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
        public string GetSex(string sexflg)
        {
            if (sexflg == "0") { return "女"; }
            else if (sexflg == "1") { return "男"; }
            else { return ""; }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        #region 指定类型获取用户信息

        public void GetUserByType(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string type = RequestHelper.string_transfer(Request, "type");
                if (!string.IsNullOrEmpty(type))
                {
                    int[] types = Split_Hepler.str_to_ints(type);

                    List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                    List<Sys_Role> Sys_Role_List = Constant.Sys_Role_List;
                    List<Sys_RoleOfUser> Sys_RoleOfUser_List = Constant.Sys_RoleOfUser_List;
                    var query = (from ul in UserInfo_List_
                                 //where ul.UserType ==type
                                 join SysRoleOfUser in Sys_RoleOfUser_List on ul.UniqueNo equals SysRoleOfUser.UniqueNo
                                 where types.Contains((int)SysRoleOfUser.Role_Id)
                                 join SysRole in Sys_Role_List on SysRoleOfUser.Role_Id equals SysRole.Id
                                 orderby SysRole.Sort
                                 select new
                                 {
                                     id = ul.Id,
                                     Name = (ul.Name == null) ? string.Empty : ul.Name,
                                     Sex = GetSex(Convert.ToString(ul.Sex)),
                                     LoginName = (ul.LoginName == null) ? string.Empty : ul.LoginName,
                                     Phone = (ul.Phone == null) ? string.Empty : ul.Phone,
                                     Email = (ul.Email == null) ? string.Empty : ul.Email,
                                     UserType = ul.UserType,
                                     Roleid = SysRole.Id,
                                     Pwd = ul.ClearPassword,
                                     UniqueNo = ul.UniqueNo,
                                     MajorId = ul.Major_ID,

                                 }).ToList();

                    int count = query.Count();

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

        #endregion

        #region 指定类型获取用户信息【包含教师所教课程】

        public void GetUserByType_Course(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                var college_TeacherUID = RequestHelper.string_transfer(Request, "college_TeacherUID");
                object query = new object();
                //if (string.IsNullOrEmpty(college_TeacherUID))
                //{

                //    var major_id = (from t in Constant.Teacher_List where t.UniqueNo == college_TeacherUID select t.Major_ID).First();

                //    int type = RequestHelper.int_transfer(Request, "type");
                //    List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                //    List<Sys_Role> Sys_Role_List = Constant.Sys_Role_List;
                //    List<Sys_RoleOfUser> Sys_RoleOfUser_List = Constant.Sys_RoleOfUser_List;
                //    query = (from ul in UserInfo_List_
                //             where ul.Major_ID== major_id
                //             join SysRoleOfUser in Sys_RoleOfUser_List on ul.UniqueNo equals SysRoleOfUser.UniqueNo
                //             where SysRoleOfUser.Role_Id == type
                //             join SysRole in Sys_Role_List on SysRoleOfUser.Role_Id equals SysRole.Id
                //             join t in Constant.Teacher_List on ul.UniqueNo equals t.UniqueNo                            
                //             join major in Constant.Major_List on t.Major_ID equals major.Id

                //             orderby SysRole.Sort
                //             select new
                //             {
                //                 a_Id = ul.Id,
                //                 a_Name = (ul.Name == null) ? string.Empty : ul.Name,
                //                 a_Sex = GetSex(Convert.ToString(ul.Sex)),
                //                 a_LoginName = (ul.LoginName == null) ? string.Empty : ul.LoginName,
                //                 a_Phone = (ul.Phone == null) ? string.Empty : ul.Phone,
                //                 a_Email = (ul.Email == null) ? string.Empty : ul.Email,
                //                 a_UserType = ul.UserType,
                //                 a_Roleid = SysRole.Id,
                //                 a_Pwd = ul.ClearPassword,
                //                 a_UniqueNo = ul.UniqueNo,
                //                 list = (from c1 in Constant.CourseRoom_List.Distinct()
                //                         where c1.TeacherUID == ul.UniqueNo
                //                         join c2 in Constant.Course_List on c1.Coures_Id equals c2.UniqueNo
                //                         select c2).Distinct().ToList()
                //             }).ToList();
                //}
                //else
                //{

                int type = RequestHelper.int_transfer(Request, "type");
                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                List<Sys_Role> Sys_Role_List = Constant.Sys_Role_List;
                List<Sys_RoleOfUser> Sys_RoleOfUser_List = Constant.Sys_RoleOfUser_List;
                query = (from ul in UserInfo_List_
                         join SysRoleOfUser in Sys_RoleOfUser_List on ul.UniqueNo equals SysRoleOfUser.UniqueNo
                         where SysRoleOfUser.Role_Id == type
                         join SysRole in Sys_Role_List on SysRoleOfUser.Role_Id equals SysRole.Id
                         join t in Constant.Teacher_List on ul.UniqueNo equals t.UniqueNo
                         join major in Constant.Major_List on t.Major_ID equals major.Id
                         orderby SysRole.Sort
                         select new
                         {
                             a_Id = ul.Id,
                             a_Name = (ul.Name == null) ? string.Empty : ul.Name,
                             a_Sex = GetSex(Convert.ToString(ul.Sex)),
                             a_LoginName = (ul.LoginName == null) ? string.Empty : ul.LoginName,
                             a_Phone = (ul.Phone == null) ? string.Empty : ul.Phone,
                             a_Email = (ul.Email == null) ? string.Empty : ul.Email,
                             a_UserType = ul.UserType,
                             a_Roleid = SysRole.Id,
                             a_Pwd = ul.ClearPassword,
                             a_UniqueNo = ul.UniqueNo,
                             list = (from c1 in Constant.CourseRoom_List.Distinct()
                                     where c1.TeacherUID == ul.UniqueNo
                                     join c2 in Constant.Course_List on c1.Coures_Id equals c2.UniqueNo
                                     select c2).Distinct().ToList()
                         }).ToList();
                //}

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

        #endregion

        #region 获取学生

        public void GetStudents(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                //返回所有用户信息

                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                List<Major> Major_List = Constant.Major_List;
                //LogHelper.Info("Get_UserInfo_List:开始");
                var query = from ul in UserInfo_List_
                            join s in Constant.Student_List on ul.UniqueNo equals s.UniqueNo
                            select new
                            {
                                id = ul.Id,
                                Name = (ul.Name == null) ? string.Empty : ul.Name,
                                Sex = GetSex(Convert.ToString(ul.Sex)),
                                //Department_Name = (ul.Department_Name == null) ? string.Empty : ul.Department_Name,
                                //Major_Name = (ul.Major_Name == null) ? string.Empty : ul.Major_Name,
                                //College_Name = (ul.College_Name == null) ? string.Empty : ul.College_Name,
                                LoginName = (ul.LoginName == null) ? string.Empty : ul.LoginName,
                                Phone = (ul.Phone == null) ? string.Empty : ul.Phone,
                                Email = (ul.Email == null) ? string.Empty : ul.Email,
                                UserType = ul.UserType,
                                Roleid = 2,
                                RoleName = "学生",
                                UniqueNo = ul.UniqueNo,
                                Pwd = ul.ClearPassword,
                                //MajorName=SysMajor.Major_Name
                                MajorName = "",
                                Major_ID = ul.Major_ID
                            };

                var query1 = from q in query
                             join SysMajor in Major_List on q.Major_ID equals SysMajor.Id
                             into gj
                             from lf in gj.DefaultIfEmpty()

                             select new
                             {
                                 id = q.id,
                                 Name = (q.Name == null) ? string.Empty : q.Name,
                                 Sex = q.Sex,
                                 //Department_Name = (ul.Department_Name == null) ? string.Empty : ul.Department_Name,
                                 //Major_Name = (ul.Major_Name == null) ? string.Empty : ul.Major_Name,
                                 //College_Name = (ul.College_Name == null) ? string.Empty : ul.College_Name,
                                 LoginName = (q.LoginName == null) ? string.Empty : q.LoginName,
                                 Phone = (q.Phone == null) ? string.Empty : q.Phone,
                                 Email = (q.Email == null) ? string.Empty : q.Email,
                                 UserType = q.UserType,
                                 Roleid = q.Roleid,
                                 RoleName = q.RoleName,
                                 UniqueNo = q.UniqueNo,
                                 Pwd = q.Pwd,
                                 //MajorName=SysMajor.Major_Name
                                 MajorName = (lf == null ? "" : lf.Major_Name),
                                 Major_ID = q.Major_ID
                             };



                int count = query1.Count();

                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query1);
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

        #endregion

        #region 获取教师

        public void GetTeachers(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string Major_ID = RequestHelper.string_transfer(context.Request, "Major_ID");
                //返回所有用户信息
                List<UserInfo> UserInfo_List_ = Constant.UserInfo_List;
                if (!string.IsNullOrEmpty(Major_ID))
                {
                    UserInfo_List_ = UserInfo_List_.Where(t=>t.Major_ID==Major_ID).ToList();
                }
                List<Major> Major_List = Constant.Major_List;
                //LogHelper.Info("Get_UserInfo_List:开始");
                var query = from ul in UserInfo_List_
                            join s in Constant.Teacher_List on ul.UniqueNo equals s.UniqueNo
                            select new
                            {
                                id = ul.Id,
                                Name = (ul.Name == null) ? string.Empty : ul.Name,
                                Sex = GetSex(Convert.ToString(ul.Sex)),
                                //Department_Name = (ul.Department_Name == null) ? string.Empty : ul.Department_Name,
                                //Major_Name = (ul.Major_Name == null) ? string.Empty : ul.Major_Name,
                                //College_Name = (ul.College_Name == null) ? string.Empty : ul.College_Name,
                                LoginName = (ul.LoginName == null) ? string.Empty : ul.LoginName,
                                Phone = (ul.Phone == null) ? string.Empty : ul.Phone,
                                Email = (ul.Email == null) ? string.Empty : ul.Email,
                                UserType = ul.UserType,
                                Roleid = 3,
                                RoleName = "教师",
                                UniqueNo = ul.UniqueNo,
                                Pwd = ul.ClearPassword,
                                //MajorName=SysMajor.Major_Name
                                MajorName = "",
                                Major_ID = ul.Major_ID
                            };

                var query1 = from q in query
                             join SysMajor in Major_List on q.Major_ID equals SysMajor.Id
                             into gj
                             from lf in gj.DefaultIfEmpty()

                             select new
                             {
                                 id = q.id,
                                 Name = (q.Name == null) ? string.Empty : q.Name,
                                 Sex = q.Sex,
                                 //Department_Name = (ul.Department_Name == null) ? string.Empty : ul.Department_Name,
                                 //Major_Name = (ul.Major_Name == null) ? string.Empty : ul.Major_Name,
                                 //College_Name = (ul.College_Name == null) ? string.Empty : ul.College_Name,
                                 LoginName = (q.LoginName == null) ? string.Empty : q.LoginName,
                                 Phone = (q.Phone == null) ? string.Empty : q.Phone,
                                 Email = (q.Email == null) ? string.Empty : q.Email,
                                 UserType = q.UserType,
                                 Roleid = q.Roleid,
                                 RoleName = q.RoleName,
                                 UniqueNo = q.UniqueNo,
                                 Pwd = q.Pwd,
                                 //MajorName=SysMajor.Major_Name
                                 MajorName = (lf == null ? "" : lf.Major_Name),
                                 Major_ID = q.Major_ID
                             };



                int count = query1.Count();

                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query1);
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

        #endregion
    }
}