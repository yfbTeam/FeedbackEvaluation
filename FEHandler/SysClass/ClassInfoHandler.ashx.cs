using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SysClass
{
    /// <summary>
    /// ClassInfoHandler 的摘要说明
    /// </summary>
    public class ClassInfoHandler : IHttpHandler
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
                    //获取课程信息
                    case "GetClassInfo": GetClassInfo(context); break;
                    //获取学年学期
                    case "Get_StudySection_List": Get_StudySection_List(context); break;
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
        public void Get_StudySection_List(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                int intSuccess = (int)errNum.Success;

                List<StudySection> StudySection_List = Constant.StudySection_List;

                //
                var query = from StudySection_ in StudySection_List
                            select new
                            {
                                //年度
                                Academic = StudySection_.Academic,
                                //级别
                                Semester = StudySection_.Semester,
                                DisPlayName = StudySection_.DisPlayName,
                            };
                Dictionary<object, object> map_Academic = new Dictionary<object, object>();
                Dictionary<object, object> map_Semester = new Dictionary<object, object>();
                Dictionary<object, object> map_DisPlayName = new Dictionary<object, object>();
                foreach (var i in query)
                {
                    if (!map_Academic.ContainsKey(i.Academic))
                    {
                        map_Academic.Add(i.Academic, 1);
                    };
                    if (!map_Semester.ContainsKey(i.Semester))
                    {
                        map_Semester.Add(i.Semester, 1);
                    };
                    if (!map_DisPlayName.ContainsKey(i.DisPlayName))
                    {
                        map_DisPlayName.Add(i.DisPlayName, 1);
                    };
                }
                Dictionary<string, object> remap = new Dictionary<string, object>();
                remap.Add("Academic", map_Academic.Keys);
                remap.Add("Semester", map_Semester.Keys);
                remap.Add("DisPlayName", map_DisPlayName.Keys);
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", remap);
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
        public void GetClassInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                int intSuccess = (int)errNum.Success;
                string TeacherUID = RequestHelper.string_transfer(context.Request, "TeacherUID");
                //返回所有用户信息
                // <th>年度</th>
                //<th>季别</th>
                //<th>课堂名称</th>
                //<th>合班</th>
                //<th>年级</th>
                //<th>专业部门名称</th>
                //<th>教师姓名</th>
                //<th>教师性质</th>
                //<th>操作</th>
                List<Course> Course_List = Constant.Course_List;
                List<CourseRoom> CourseRoom_List = Constant.CourseRoom_List;
                List<Teacher> Teacher_List = Constant.Teacher_List;
                List<StudySection> StudySection_List = Constant.StudySection_List;
                List<ClassInfo> ClassInfo_List = Constant.ClassInfo_List;
                List<GradeInfo> GradeInfo_List = Constant.GradeInfo_List;
                List<UserInfo> UserInfo_List = Constant.UserInfo_List;
                if (!string.IsNullOrEmpty(TeacherUID))
                {
                    CourseRoom_List = CourseRoom_List.Where(t=>t.TeacherUID==TeacherUID).ToList();
                }
                var query = from CourseRoom_ in CourseRoom_List
                            join Course_ in Course_List on CourseRoom_.Coures_Id equals Course_.UniqueNo
                            join Teacher_ in Teacher_List on CourseRoom_.TeacherUID equals Teacher_.UniqueNo
                            join StudySection_ in StudySection_List on CourseRoom_.StudySection_Id equals StudySection_.Id
                            join ClassInfo_ in ClassInfo_List on CourseRoom_.Calss_Id equals ClassInfo_.ClassNO
                            //join GradeInfo_ in GradeInfo_List on ClassInfo_.Grade_Id equals GradeInfo_.Id
                            join userinfo in Constant.UserInfo_List on Teacher_.UniqueNo equals userinfo.UniqueNo
                            select new
                            {
                                //年度
                                Academic = StudySection_.Academic,
                                //级别
                                Semester = StudySection_.Semester,
                                //学年学期
                                DisPlayName = StudySection_.DisPlayName,
                                //课堂名称
                                Course_Name = Course_.Name,
                                //班
                                ClassInfo_Name = ClassInfo_.Class_Name,
                                //年纪
                                GradeInfo_Name = "",
                                //教师姓名
                                Teacher_Name = userinfo.Name,
                                //教师性质
                                Teacher_JobTitle = Teacher_.JobTitle,
                                //教师id
                                //TeacherUID=Teacher_.UniqueNo
                            };
                int a = query.Count();
                ////左连接部门名称
                //var query2 = from Class_Info in query
                //             join UserInfo_ in UserInfo_List on Class_Info.TeacherUID equals UserInfo_.UniqueNo
                //             into gj from subpet in gj.DefaultIfEmpty()
                //             select new
                //             {
                //                //年度
                //                 Academic = Class_Info.Academic,
                //                //级别
                //                 Semester = GetSemester(Class_Info.Semester),
                //                //课堂名称
                //                 Course_Name = Class_Info.Course_Name,
                //                //班
                //                 ClassInfo_Name = Class_Info.ClassInfo_Name,
                //                //年纪
                //                 GradeInfo_Name = Class_Info.GradeInfo_Name,
                //                //教师姓名
                //                 Teacher_Name = Class_Info.Teacher_Name,
                //                //教师性质
                //                 Teacher_JobTitle = (Class_Info.Teacher_JobTitle == null) ? string.Empty : Class_Info.Teacher_JobTitle,
                //                 //专业部门名称
                //                // Department_Name = (subpet == null ? String.Empty :  subpet.Department_Name)

                //             };
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
        public string GetSemester(int? i)
        {
            string re = "";
            if (i == 1)
            {
                re = "上学期";
            }
            else
            {
                re = "下学期";
            }
            return re;
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