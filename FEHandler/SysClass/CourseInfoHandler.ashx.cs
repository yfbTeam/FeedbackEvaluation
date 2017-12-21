using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SysClass
{
    /// <summary>
    /// CourseInfoHandler 的摘要说明
    /// </summary>
    public class CourseInfoHandler : IHttpHandler
    {
        #region 中心入口点

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
                    case "GetCourseInfo": GetCourseInfo(context); break;

                    //获取所有未分配课程
                    case "GetNoDis_CourseInfo": GetNoDis_CourseInfo(context); break;


                    //移除分配课程
                    case "DeleteCourseDis": DeleteCourseDis(context); break;

                    //删除制定课程类型
                    case "Delete_CourseType": Delete_CourseType(context); break;
                    //编辑制定课程类型
                    case "Edit_CourseType": Edit_CourseType(context); break;

                    //获取所有课堂
                    case "GetCourseRoomInfo": GetCourseRoomInfo(context); break;
                    //获取所有课程性质
                    case "GetCourseProperty": GetCourseProperty(context); break;
                    //课程编辑
                    case "EditCourse": EditCourse(context); break;
                    //课程类型添加
                    case "AddCourseType": AddCourseType(context); break;
                    //设置课程分类为未分类
                    case "SetCourseRelToNull": SetCourseRelToNull(context); break;
                    //SetCourseSort
                    case "SetCourseSort": SetCourseSort(context); break;
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

        #endregion

        #region 分配课程

        public void SetCourseRelToNull(HttpContext context)
        {
            try
            {
                List<CourseRel> CourseRel_List = Constant.CourseRel_List;
                HttpRequest request = context.Request;
                string Course_No = RequestHelper.string_transfer(request, "Course_No");
                var CourseRel_ = CourseRel_List.Where(i => i.Course_Id == Course_No).FirstOrDefault();
                CourseRel_.CourseType_Id = "";

                jsonModel = Constant.CourseRelService.Update(CourseRel_);
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
        public void SetCourseSort(HttpContext context)
        {
            try
            {
                List<CourseRel> CourseRel_List = Constant.CourseRel_List;
                HttpRequest request = context.Request;
                string Course_Nos = RequestHelper.string_transfer(request, "Course_No");
                string Course_Typeid = RequestHelper.string_transfer(request, "CourseTypeId");
                int StudySection_Id = RequestHelper.int_transfer(request, "StudySection_Id");

                foreach (string Course_No in Course_Nos.Split(','))
                {
                    var CourseRel_ = CourseRel_List.Where(i => i.Course_Id == Course_No && i.StudySection_Id == StudySection_Id).FirstOrDefault();
                    if (CourseRel_ != null)
                    {
                        CourseRel_.CourseType_Id = Course_Typeid;
                        jsonModel = Constant.CourseRelService.Update(CourseRel_);
                    }
                    else
                    {
                        CourseRel courseRef = new CourseRel()
                        {
                            Course_Id = Course_No,
                            CourseType_Id = Course_Typeid,
                            StudySection_Id = StudySection_Id,
                            CreateTime = DateTime.Now,
                            CreateUID = "",
                            EditTime = DateTime.Now,
                            EditUID = "",
                            IsDelete = (int)IsDelete.No_Delete,
                        };
                        jsonModel = Constant.CourseRelService.Add(courseRef);
                        if (jsonModel.errNum == 0)
                        {
                            courseRef.Id = Convert.ToInt32(jsonModel.retData);
                            Constant.CourseRel_List.Add(courseRef);
                        }
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

        #endregion

        #region 移除课程分配

        public void DeleteCourseDis(HttpContext context)
        {
            try
            {
                HttpRequest request = context.Request;
                int CourseRelID = RequestHelper.int_transfer(request, "CourseRelID");
                jsonModel = DeleteCourseDisHelper(CourseRelID);
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
        public static JsonModel DeleteCourseDisHelper(int CourseRelID)
        {
            JsonModel jsm = new JsonModel();
            try
            {
                var courseref = Constant.CourseRel_List.FirstOrDefault(i => i.Id == CourseRelID);
                if (courseref != null)
                {
                    courseref.IsDelete = (int)IsDelete.Delete;
                    jsm = Constant.CourseRelService.Update(courseref);
                    if (jsm.errNum == 0)
                    {
                        Constant.CourseRel_List.Remove(courseref);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return jsm;
        }

        #endregion

        #region 删除课程类型

        public void Delete_CourseType(HttpContext context)
        {
            try
            {
                HttpRequest request = context.Request;
                int Id = RequestHelper.int_transfer(request, "Id");

                Sys_Dictionary dic = Constant.Sys_Dictionary_List.FirstOrDefault(di => di.Id == Id);

                if (dic != null)
                {
                    int count = (from s in Constant.CourseRel_List where s.CourseType_Id == dic.Key select s).Count();
                    if (count == 0)
                    {
                        jsonModel = Constant.Sys_DictionaryService.Delete(Id);
                        Constant.Sys_Dictionary_List.Remove(dic);
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "该课程类型已有关联的课程");
                    }

                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "该课程类型不存在");
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

        #region 编辑课程类型

        public void Edit_CourseType(HttpContext context)
        {
            try
            {
                HttpRequest request = context.Request;
                int Id = RequestHelper.int_transfer(request, "Id");
                string Name = RequestHelper.string_transfer(request, "Name");
                int IsEnable = RequestHelper.int_transfer(request, "IsEnable");
                Sys_Dictionary dic = Constant.Sys_Dictionary_List.FirstOrDefault(di => di.Id == Id);
                if (dic != null)
                {
                    Sys_Dictionary dic_clone = Constant.Clone<Sys_Dictionary>(dic);
                    dic_clone.Value = Name;
                    dic_clone.IsEnable = (byte)IsEnable;
                    if (dic_clone != null)
                    {

                        jsonModel = Constant.Sys_DictionaryService.Update(dic_clone);
                        if (jsonModel.errNum == 0)
                        {
                            dic.Value = Name;
                            dic.IsEnable = (byte)IsEnable;
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "操作失败");
                        }

                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "该课程类型不存在");
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

        #region 获取课程

        public void GetCourseInfo(HttpContext context)
        {
            try
            {
                HttpRequest request = context.Request;
                int SectionId = RequestHelper.int_transfer(request, "SectionId");
                string Key = RequestHelper.string_transfer(request, "Key");
                string CourseTypeID = RequestHelper.string_transfer(request, "CourseTypeID");

                int PageIndex = RequestHelper.int_transfer(request, "PageIndex");
                int PageSize = RequestHelper.int_transfer(request, "PageSize");

                jsonModel = GetCourseInfoHelper(PageIndex, PageSize, SectionId, Key, CourseTypeID);
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

        public static JsonModelNum GetCourseInfoHelper(int PageIndex, int PageSize, int SectionId, string Key, string CourseTypeID)
        {
            JsonModelNum jsm = new JsonModelNum();
            int intSuccess = (int)errNum.Success;
            try
            {
                List<Course> Course_List = Constant.Course_List;
                List<CourseRel> CourseRel_List = Constant.CourseRel_List;
                DictionType_Enum dictiontype = DictionType_Enum.Course_Type;
                List<Sys_Dictionary> Sys_Dictionary_List = Constant.Sys_Dictionary_List;
                string dictiontypevalue = Convert.ToString((int)dictiontype);

                var courrel = (from CourseRel_ in CourseRel_List
                               where CourseRel_.StudySection_Id == SectionId && CourseRel_.CourseType_Id == CourseTypeID
                               join Sys_Dictionary_ in Sys_Dictionary_List.Where(i => i.Type == dictiontypevalue) on CourseRel_.CourseType_Id equals Sys_Dictionary_.Key
                               where Sys_Dictionary_.SectionId == SectionId
                               select new { CourseRel_.CourseType_Id, CourseRel_.Course_Id, Sys_Dictionary_.Value, Sys_Dictionary_.IsEnable, CourseRelID = CourseRel_.Id }).ToList();
                var query = (from Course_ in Course_List
                             where Course_.Name.Contains(Key) || Course_.UniqueNo.Contains(Key)
                             join cr in courrel on Course_.UniqueNo equals cr.Course_Id
                             orderby Course_.IsEnable
                             select new
                             {
                                 Course_Id = Course_.Id,
                                 //课程名称
                                 Course_Name = Course_.Name,
                                 //课程编号
                                 Course_No = Course_.UniqueNo,
                                 //课程分类id
                                 CourseRel_Id = cr == null ? "" : cr.CourseType_Id,
                                 CourseRel_Name = (cr == null ? "未分类" : cr.Value),

                                 CourseRelID = cr == null ? 0 : cr.CourseRelID,
                                 Course_.IsEnable,

                                 DepartMentID = Course_.DepartMentID,
                                 DepartmentName = Course_.DepartmentName,
                                 SubDepartmentID = Course_.SubDepartmentID,
                                 SubDepartmentName = Course_.SubDepartmentName,
                                 CourseProperty = Course_.CourseProperty,
                             }).ToList();
                var query_last = (from an in query select an).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                //jsm = JsonModel.get_jsonmodel(intSuccess, "success", query_last);

                //返回所有表格数据
                jsm = JsonModelNum.GetJsonModel_o(intSuccess, "success", query_last);
                jsm.PageIndex = PageIndex;
                jsm.PageSize = PageSize;
                jsm.PageCount = (int)Math.Ceiling((double)query.Count() / PageSize);
                jsm.RowCount = query.Count();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return jsm;
        }

        #endregion

        #region 获取未分配课程

        public void GetNoDis_CourseInfo(HttpContext context)
        {
            try
            {
                HttpRequest request = context.Request;
                int SectionId = RequestHelper.int_transfer(request, "SectionId");
                string Key = RequestHelper.string_transfer(request, "Key");
                string Major_Id = RequestHelper.string_transfer(request, "Major_Id");
                int PageIndex = RequestHelper.int_transfer(request, "PageIndex");
                int PageSize = RequestHelper.int_transfer(request, "PageSize");

                jsonModel = GetNoDis_CourseInfoHelper(PageIndex, PageSize, Major_Id, SectionId, Key);
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

        public static JsonModelNum GetNoDis_CourseInfoHelper(int PageIndex, int PageSize, string Major_Id, int SectionId, string Key)
        {
            JsonModelNum jsm = new JsonModelNum();
            int intSuccess = (int)errNum.Success;
            try
            {
                List<Course> Course_List = Constant.Course_List;
                List<CourseRel> CourseRel_List = Constant.CourseRel_List;

                if (Major_Id != "")
                {
                    Course_List = (from course in Course_List where course.DepartMentID == Major_Id select course).ToList();
                }
                if (Key != "")
                {
                    Course_List = (from course in Course_List where course.Name.Contains(Key) || course.UniqueNo.Contains(Key) select course).ToList();
                }

                var query = (from Course_ in Course_List
                             join cr in CourseRel_List on new
                             {
                                 UniqueNo = Course_.UniqueNo,
                                 SectionId = SectionId
                             } equals new
                             {
                                 UniqueNo = cr.Course_Id,
                                 SectionId = (int)cr.StudySection_Id
                             } into courelfs_
                             from courel in courelfs_.DefaultIfEmpty()
                             where courel == null
                             orderby Course_.IsEnable
                             select new
                             {
                                 Course_Id = Course_.Id,
                                 //课程名称
                                 Course_Name = Course_.Name,
                                 //课程编号
                                 Course_No = Course_.UniqueNo,
                                 //课程分类id
                                 //CourseRel_Id = cr == null ? "" : cr.CourseType_Id,
                                 //CourseRel_Name = (cr == null ? "未分类" : cr.Value),

                                 //CourseRelID = cr == null ? 0 : cr.CourseRelID,
                                 Course_.IsEnable,

                                 DepartMentID = Course_.DepartMentID,
                                 DepartmentName = Course_.DepartmentName,
                                 SubDepartmentID = Course_.SubDepartmentID,
                                 SubDepartmentName = Course_.SubDepartmentName,
                                 CourseProperty = Course_.CourseProperty,
                             }).ToList();
                var query_last = (from an in query select an).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                //jsm = JsonModel.get_jsonmodel(intSuccess, "success", query_last);

                //返回所有表格数据
                jsm = JsonModelNum.GetJsonModel_o(intSuccess, "success", query_last);
                jsm.PageIndex = PageIndex;
                jsm.PageSize = PageSize;
                jsm.PageCount = (int)Math.Ceiling((double)query.Count() / PageSize);
                jsm.RowCount = query.Count();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return jsm;
        }

        #endregion

        #region 获取课堂

        public void GetCourseRoomInfo(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest request = context.Request;
                string TeacherUID = RequestHelper.string_transfer(request, "TeacherUID");

                List<Course> Course_List = Constant.Course_List;
                List<CourseRel> CourseRel_List = Constant.CourseRel_List;
                var query = new object();
                if (!string.IsNullOrEmpty(TeacherUID))
                {
                    query = from Course_ in Course_List
                            join CourseRel_ in CourseRel_List on Course_.UniqueNo equals CourseRel_.Course_Id
                            join CR in Constant.CourseRoom_List on Course_.UniqueNo equals CR.Coures_Id
                            //where CR.StudySection_Id == sectionId
                            where CR.TeacherUID == TeacherUID
                            select new
                            {
                                //课程名称
                                Course_Name = Course_.Name,
                                //课程编号
                                Course_No = Course_.UniqueNo,
                                //课程分类id
                                CourseRel_Id = CourseRel_.CourseType_Id,
                                //
                                TeacharUniqueNo = CR.TeacherUID,

                            };
                }
                else
                {
                    query = from Course_ in Course_List
                            join CourseRel_ in CourseRel_List on Course_.UniqueNo equals CourseRel_.Course_Id
                            join CR in Constant.CourseRoom_List on Course_.UniqueNo equals CR.Coures_Id
                            //where CR.StudySection_Id == sectionId
                            select new
                            {
                                //课程名称
                                Course_Name = Course_.Name,
                                //课程编号
                                Course_No = Course_.UniqueNo,
                                //课程分类id
                                CourseRel_Id = CourseRel_.CourseType_Id,
                                //
                                TeacharUniqueNo = CR.TeacherUID,

                            };
                }

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

        #region 获取课程性质【公修、选修、必修】

        public void GetCourseProperty(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                var query = from Teacher_ in Constant.Course_List select Teacher_;
                var query2 = from p in query
                             group p by p.CourseProperty into allg
                             select new
                             {
                                 CourseProperty = allg.Max(p => p.CourseProperty) == "" ? "未录入" : allg.Max(p => p.CourseProperty),
                             };

                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query2);
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

        #region 课程启用、禁用  1、启用、禁用   【ref ok】

        public void EditCourse(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest request = context.Request;

            string Ids = RequestHelper.string_transfer(request, "Ids");
            int Enable = RequestHelper.int_transfer(request, "Enable");
            int CourseTypeId = RequestHelper.int_transfer(request, "CourseTypeId");
            int Operation = RequestHelper.int_transfer(request, "Operation");
            try
            {
                //课程变更【启用禁用】
                if (!string.IsNullOrEmpty(Ids))
                {
                    int[] Ids_ints = Split_Hepler.str_to_ints(Ids);

                    foreach (var id in Ids_ints)
                    {
                        //设置课程启用禁用
                        if (Operation == 1)
                        {
                            jsonModel = Constant.CourseService.GetEntityById(id);
                            if (jsonModel.errNum == 0)
                            {
                                Course s_course = jsonModel.retData as Course;
                                s_course.IsEnable = (byte)Enable;
                                JsonModel model = Constant.CourseService.Update(s_course);
                                if (jsonModel.errNum == 0)
                                {
                                    Course course = Constant.Course_List.FirstOrDefault(cour => cour.Id == id);
                                    course.IsEnable = s_course.IsEnable;
                                }
                            }
                        }
                        if (Operation == 2)
                        {
                            CourseRel courseRef = Constant.CourseRel_List.FirstOrDefault(re => re.Course_Id == Convert.ToString(id));
                            if (courseRef != null && CourseTypeId != 0)
                            {
                                CourseRel courseRef_Clone = Constant.Clone<CourseRel>(courseRef);
                                courseRef_Clone.CourseType_Id = Convert.ToString(CourseTypeId);
                                JsonModel model = Constant.CourseRelService.Update(courseRef_Clone);
                            }
                        }
                    }
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", "0");
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel((int)errNum.Failed, "failed", "数据出现异常");
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

        #region 新增课程分类

        public void AddCourseType(HttpContext context)
        {
            HttpRequest request = context.Request;
            string CourseTypeName = RequestHelper.string_transfer(request, "CourseTypeName");
            string CreateUID = RequestHelper.string_transfer(request, "CreateUID");
            int SectionId = RequestHelper.int_transfer(request, "SectionId");
            int IsEnable = RequestHelper.int_transfer(request, "IsEnable");
            try
            {
                //课程变更【启用禁用】
                if (!string.IsNullOrEmpty(CourseTypeName))
                {
                    //获取课程类型 课程的值 +1
                    Sys_Dictionary dic_max = (from t in Constant.Sys_Dictionary_List where t.Type == "0" orderby Convert.ToInt32(t.Key) descending select t).ToList()[0];
                    if (dic_max != null)
                    {
                        int va = Convert.ToInt32(dic_max.Key) + 1;
                        Sys_Dictionary dictionary = new Sys_Dictionary()
                        {
                            Pid = 0,
                            Key = Convert.ToString(va),
                            Value = CourseTypeName,
                            Type = "0",
                            SectionId = SectionId,
                            CreateTime = DateTime.Now,
                            EditTime = DateTime.Now,
                            CreateUID = CreateUID,
                            EditUID = CreateUID,
                            IsDelete = 0,
                            Sort = 0,
                            IsEnable = (byte)IsEnable,
                        };
                        //添加一个课程类型
                        jsonModel = Constant.Sys_DictionaryService.Add(dictionary);

                        if (jsonModel.errNum == 0)
                        {
                            dictionary.Id = Convert.ToInt32(jsonModel.retData);
                            Constant.Sys_Dictionary_List.Add(dictionary);
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel((int)errNum.Failed, "failed", "数据出现异常");
                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel((int)errNum.Failed, "failed", "数据出现异常");
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

        #region 辅助

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

    }
}