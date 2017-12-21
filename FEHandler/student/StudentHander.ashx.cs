using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.student
{
    /// <summary>
    /// StudentHander 的摘要说明
    /// </summary>
    public class StudentHander : IHttpHandler
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
                    case "Get_StudySection_List": Get_StudySection_List(context); break;
                    //获取指标库
                    case "Get_Eva_Task": Get_Eva_Task(context); break;
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
        //获取学年学期
        public void Get_StudySection_List(HttpContext context)
        {
            try
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
               List<StudySection> StudySection=Constant.StudySection_List;
                 var query = from ss in StudySection
                             select new
                             {
                                 StudySection = ss.Academic + "年" + GetSemester(Convert.ToInt32(ss.Semester)),
                                 Id=ss.Id
                             };

                //返回所有指标库数据
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
        //获取学生评价表
        public void Get_Eva_Task(HttpContext context)
        {
            try
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                string UserUniqueNo = RequestHelper.string_transfer(Request, "UserUniqueNo");
                //List<Eva_Task> Eva_Task = Constant.Eva_Task_List;
                //List<StudySection> StudySection = Constant.StudySection_List;
                //List<Teacher> Teacher = Constant.Teacher_List;
                var query = from ed in Constant.Eva_Distribution_List
                            //获取评价名称
                            join er in Constant.Eva_Regular_List on ed.Evaluate_Id equals er.Id
                            //获取学期名称
                            join ss in Constant.StudySection_List on er.Section_Id equals ss.Id
                            //获取课程名称
                            join sc in Constant.CourseRel_List on ed.CousrseType_Id equals sc.CourseType_Id
                            join scr in Constant.CourseRoom_List  on sc.Course_Id equals scr.Coures_Id
                            join cif in Constant.ClassInfo_List on scr.Calss_Id equals cif.ClassNO
                            //获取教师名称
                            join t in Constant.UserInfo_List on scr.TeacherUID equals t.UniqueNo
                            //获取那些学生能看到
                            join c in Constant.Course_List on scr.Coures_Id equals c.UniqueNo
                            join csr in Constant.Class_StudentInfo_List on scr.Calss_Id equals csr.Class_Id
                            where csr.UniqueNo == UserUniqueNo
                            select new
                            {
                                Eva_Distribution_Id=ed.Id,
                                Course_Id=c.Id,
                                Type = er.Name,
                                StudySection = ss.Academic + "年" + GetSemester(Convert.ToInt32(ss.Semester)),
                                Course_Name = c.Name,
                                TeacherName = t.Name,
                            };
                 
                //返回所有指标库数据
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
        public void Get_Eva_Task_jishisaoma(HttpContext context)
        {
            //var list = from e in Constant.Eva_Task_List
            //           join t in Constant.Teacher_List on e.TeacherUID equals t.UniqueNo
            //           //获取那些学生能看到
            //           join c in Constant.Course_List on e.Coures_Id equals c.Id
            //           join csr in Constant.Class_StudentInfo_List on c.Id equals csr.Class_Id
            //           //where csr.UniqueNo == UserUniqueNo
            //           where e.Type == "0" && e.TeacherUID == TeacherUID 
            //           select e;
        }
        public string GetSemester(int i)
        {
            string re = "";
            if (i == 1)
            {
                re = "春";
            }
            else if (i == 2)
            {
                re = "秋";
            }
            return re;
        }
        public string GetTypeName(string i)
        {
            string re = "";
            if (i == "1")
            {
                re = "即时评价";
            }
            else if (i == "2")
            {
                re = "扫码评价";
            }
            else if (i == "3")
            {
                re = "定期评价";
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