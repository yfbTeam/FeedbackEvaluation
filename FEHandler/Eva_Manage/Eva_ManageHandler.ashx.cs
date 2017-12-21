using FEBLL;
using FEModel;
using FEUtility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FEHandler.Eva_Manage
{
    /// <summary>
    /// Eva_ManageHandler 的摘要说明
    /// </summary>
    public partial class Eva_ManageHandler : IHttpHandler
    {
        #region 字段

        /// <summary>
        /// 数据传输对象
        /// </summary>
        JsonModel jsonModel = new JsonModel();

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region 中心入口点

        /// <summary>
        /// 中心入口点
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;

            //清除缓存【自动判断是否清除】
            Constant.Dispose_All();
            string func = RequestHelper.string_transfer(Request, "func");
            try
            {
                switch (func)
                {
                    #region 学年学期

                    //获取学年学期
                    case "Get_StudySection": Get_StudySection(context); break;
                    //添加学年学期
                    case "Add_StudySection": Add_StudySection(context); break;

                    #endregion

                    #region 指标库

                    //获取指标库分类
                    case "Get_IndicatorType": Get_IndicatorType(context); break;
                    //新增指标库分类
                    case "Add_IndicatorType": Add_IndicatorType(context); break;
                    //编辑指标库分类
                    case "Edit_IndicatorType": Edit_IndicatorType(context); break;
                    //删除指标库分类
                    case "Delete_Indicator_Type": Delete_Indicator_Type(context); break;

                    //获取指标库
                    case "Get_Indicator": Get_Indicator(context); break;
                    //新增指标库
                    case "Add_Indicator": Add_Indicator(context); break;
                    //编辑指标库
                    case "Edit_Indicator": Edit_Indicator(context); break;
                    //删除指标库
                    case "Delete_Indicator": Delete_Indicator(context); break;

                    #endregion

                    #region 表格

                    //获取表格
                    case "Get_Eva_Table": Get_Eva_Table(context); break;
                    //获取表格详情
                    case "Get_Eva_TableDetail": Get_Eva_TableDetail(context); break;
                    //添加表格
                    case "Add_Eva_Table": Add_Eva_Table(context); break;
                    //复制表格
                    case "Copy_Eva_Table": Copy_Eva_Table(context); break;

                    //编辑表格
                    case "Edit_Eva_Table": Edit_Eva_Table(context); break;

                    //编辑表格
                    case "Enable_Eva_Table": Enable_Eva_Table(context); break;
                    //删除表格
                    case "Delete_Eva_Table": Delete_Eva_Table(context); break;
                    //获取表格头部
                    case "Get_Eva_Table_Header_Custom_List": Get_Eva_Table_Header_Custom_List(context); break;

                    #endregion

                    #region 课程

                    //获取课程类型
                    case "GetCourse_Type": GetCourse_Type(context); break;
                    //获取课程类型（包含设计表）
                    case "GetTable_ByCourseType": GetTable_ByCourseType(context); break;

                    #endregion

                    #region 任务分配

                    //获取教师、课程、授课班
                    case "GetTeacherInfo_Course_Cls": GetTeacherInfo_Course_Cls(context); break;

                    #endregion

                    #region 即时扫码

                    //添加即时评价、扫码评价
                    case "Add_Eva_Common": Add_Eva_Common(context); break;
                    //编辑即时评价和扫码评价
                    case "Edit_Eva_Common": Edit_Eva_Common(context); break;

                    //获取即时评价、扫码评价
                    case "Get_Eva_Common": Get_Eva_Common(context); break;

                    //获取即时评价、扫码评价【统计分析】
                    case "Get_Eva_Common_A_By_Id": Get_Eva_Common_A_By_Id(context); break;

                    //查看即时评价、扫码评价
                    case "Get_Eva_Common_ById": Get_Eva_Common_ById(context); break;
                    case "Get_Eva_Common_ById_Mobile": Get_Eva_Common_ById_Mobile(context); break;

                    //分配即时评价、扫码评价
                    case "Distribution_Eva_Common": Distribution_Eva_Common(context); break;
                    //删除即时评价、扫码评价
                    case "Delete_Eva_Common": Delete_Eva_Common(context); break;

                    //即时和扫码 判断是否已经答过题
                    case "GetIsAnswer_Eva_ById": GetIsAnswer_Eva_ById(context); break;
                    
                    #endregion

                    #region 定期设置

                    case "Get_Eva_Regular": Get_Eva_Regular(context); break;
                    //新增定期评价
                    case "Add_Eva_Regular": Add_Eva_Regular(context); break;
                    //编辑定期评价
                    case "Edit_Eva_Regular": Edit_Eva_Regular(context); break;
                    //删除定期评价
                    case "Delete_Eva_Regular": Delete_Eva_Regular(context); break;
                    //获取评价名称【定期评价】
                    case "Get_Eva_Regular_Name": Get_Eva_Regular_Name(context); break;
                    
                    #endregion

                    #region 答题
                   
                    //定期评价-学生答题表
                    case "Add_Eva_QuestionAnswer": Add_Eva_QuestionAnswer(context); break;

                    //获取设计表详情【学生答题的初始化答卷,已答卷】
                    case "Get_Eva_TableDetail_HasAnswer": Get_Eva_TableDetail_HasAnswer(context); break;

                    #endregion


                    #region other


                    //获取当前教师的班级
                    case "Get_Teacher_Class": Get_Teacher_Class(context); break;

                    //即时和扫码 答题
                    case "AddEva_TaskAnswer": AddEva_TaskAnswer(context); break;
                    //即时和扫码-学生查看试卷
                    case "Get_Eva_TaskAnswer_ById": Get_Eva_TaskAnswer_ById(context); break;
                    case "Get_Eva_TaskAnswer_ById_Mobile": Get_Eva_TaskAnswer_ById_Mobile(context); break;
                    //教学反馈获取
                    case "Get_Eva_TeacherAnswer": Get_Eva_TeacherAnswer(context); break;
                    //教学反馈添加
                    case "Add_Eva_TeacherAnswer": Add_Eva_TeacherAnswer(context); break;

                
                    //根据老师获取课程
                    case "GetCourseByTeacherUID": GetCourseByTeacherUID(context); break;

                    //判断是否有专家定期评价的数据
                    case "CheckHasExpertRegu": CheckHasExpertRegu(context); break;

                    //获取未评价的调查与测验
                    case "Get_Eva_Common_NoAnserStudents": Get_Eva_Common_NoAnserStudents(context); break;

                    //获取未评价的调查与测验
                    case "Get_Eva_Common_JustAnserStudents": Get_Eva_Common_JustAnserStudents(context); break;

                    #endregion

                    #region 课程类型——表

                    case "GetCourseType_Table": GetCourseType_Table(context); break;
                        
                    //添加
                    case "AddCourseType_Table": AddCourseType_Table(context); break;
                    //删除
                    case "DeleteCourseType_Table": DeleteCourseType_Table(context); break;

                    #endregion

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

        #region 获取教师信息【携带 课程-授课班】

        public void GetTeacherInfo_Course_Cls(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            string teacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            try
            {
                List<T_C_Model> list = Teacher_Course_ClassInfo();
                if (list.Count > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                }

                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        #region 调查与测验

        /// <summary>
        /// 获取即时评价，扫码评价
        /// </summary>
        public void Get_Eva_Common(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //教师身份
            string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            //学生身份
            string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");
            List<Ime_Model> list = new List<Ime_Model>();
            try
            {
                if (!string.IsNullOrEmpty(TeacherUID))
                {
                    list = Get_Eva_Common_Teacher(TeacherUID, list);
                }
                else if (!string.IsNullOrEmpty(StudentUID))
                {
                    list = Get_Eva_Common_Student(StudentUID, list);
                }
                if (list.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                string json = Constant.jss.Serialize(jsonModel);

                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + json + "}");
            }
        }

        /// <summary>
        /// 调查与测验获取【学生】
        /// </summary>
        /// <param name="TeacherUID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<Ime_Model> Get_Eva_Common_Student(string StudentUID, List<Ime_Model> list)
        {
            try
            {
                //获取当前学生的班级
                List<ClassInfo> ex = Student_ClassInfo(StudentUID);

                //通过班级获取即时扫码评价
                if (ex.Count > 0)
                {
                    ClassInfo classInfo = ex[0];

                    list = Teacher_Task();

                    //筛选（下发，不是该班级的去除）
                    for (int i = list.Count - 1; i > -1; i--)
                    {
                        Ime_Model lid = list[i];
                        if (lid.Eva_Task.IsSued == (byte)IsSued.Yes)
                        {
                            if (!lid.Eva_Task.Class_Id.Contains(classInfo.ClassNO))
                            {
                                list.Remove(lid);
                            }
                        }
                    }

                    foreach (Ime_Model li in list)
                    {
                        List<Eva_TaskAnswer> list_get = Constant.Eva_TaskAnswer_List.Where(i => i.Task_Id == li.Eva_Task.Id).Where(i => i.CreateUID == StudentUID).ToList();
                        double score = 0;
                        foreach (Eva_TaskAnswer sc in list_get)
                        {
                            if (sc.Score != null)
                            {
                                score += (double)sc.Score;
                            }
                        }
                        //评价答题表
                        int list_list_c = (from tsc in Constant.Eva_TaskAnswer_List
                                           where tsc.CreateUID == StudentUID && tsc.Task_Id == li.Eva_Task.Id
                                           select tsc.CreateUID).Distinct().ToList().Count;

                        li.is_answer = list_list_c > 0 ? true : false;
                        li.answer_count = list_list_c;
                        if (list_list_c > 0)
                        {
                            li.ave_score = Math.Round(score / list_list_c, 2);
                        }

                    }
                    //筛选（未下发，待评价的）
                    for (int i = list.Count - 1; i > -1; i--)
                    {
                        Ime_Model lid = list[i];
                        if (lid.Eva_Task.IsSued == (byte)IsSued.No && !lid.is_answer)
                        {
                            list.Remove(lid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return list;
        }

        /// <summary>
        /// 调查与测验获取【教师】
        /// </summary>
        /// <param name="TeacherUID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<Ime_Model> Get_Eva_Common_Teacher(string TeacherUID, List<Ime_Model> list)
        {
            try
            {
                //去除重复的数据【进行合班】
                List<Eva_Task> ts_List = new List<Eva_Task>();
                List<string> stringlist = new List<string>();
                foreach (Eva_Task item in Constant.Eva_Task_List)
                {
                    if (!stringlist.Contains(item.Equal_Flg))
                    {
                        ts_List.Add(item);
                        if (!string.IsNullOrEmpty(item.Equal_Flg))
                        {
                            stringlist.Add(item.Equal_Flg);
                        }
                    }
                }
                //合班的数据
                list = Teacher_Task_ByTeacher(TeacherUID, ts_List);

                foreach (Ime_Model li in list)
                {
                    List<Eva_TaskAnswer> list_get = Constant.Eva_TaskAnswer_List.Where(i => i.Task_Id == li.Eva_Task.Id).ToList();
                    double score = 0;
                    foreach (Eva_TaskAnswer sc in list_get)
                    {
                        if (sc.Score != null)
                        {
                            score += (double)sc.Score;
                        }
                    }
                    int list_list_c = (from tsc in Constant.Eva_TaskAnswer_List
                                       where tsc.Task_Id == li.Eva_Task.Id
                                       select tsc.CreateUID).Distinct().ToList().Count;
                    li.is_answer = list_list_c > 0 ? true : false;
                    li.answer_count = list_list_c;
                    if (list_list_c > 0)
                    {
                        li.ave_score = Math.Round(score / list_list_c, 2);
                    }
                    if (li.answer_percent == double.NaN)
                    {
                        li.answer_percent = 0.0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return list;
        }

        /// <summary>
        /// 获取未评价的调查与测验
        /// </summary>
        public void Get_Eva_Common_NoAnserStudents(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;

            //教师身份
            string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            int Task_Id = RequestHelper.int_transfer(Request, "Task_Id");
            Eva_Answer_Model_T Eva_Answer_Model_T = new global::Eva_Answer_Model_T();
            List<Eva_Answer_Model> list = new List<Eva_Answer_Model>();
            List<Eva_Task_Answer_Model> modelList = new List<Eva_Task_Answer_Model>();
            try
            {
                if (!string.IsNullOrEmpty(TeacherUID))
                {
                    Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Task_Id);
                    if (task != null)
                    {
                        //获取所有当前教师所教学生
                        List<C_T> student_cls_list = Get_Students_ContainsClassInfo(TeacherUID, task);

                        //答题的学生
                        List<string> list_list_c = (from tsc in Constant.Eva_TaskAnswer_List
                                                    where tsc.Task_Id == Task_Id
                                                    select tsc.CreateUID).Distinct().ToList();
                        //学生答的详题
                        List<Eva_TaskAnswer> list_list = (from tsc in Constant.Eva_TaskAnswer_List
                                                          where tsc.Task_Id == Task_Id
                                                          select tsc).ToList();

                        //遍历答题的学生（收集分数）
                        foreach (string item in list_list_c)
                        {
                            List<Eva_TaskAnswer> _unique_list = (from s in list_list
                                                                 where s.CreateUID == item
                                                                 select s).ToList();
                            double Score = 0.0;
                            foreach (Eva_TaskAnswer scc in _unique_list)
                            {
                                if (scc.Score != null)
                                {
                                    Score += (double)scc.Score;
                                }
                            }
                            Eva_Task_Answer_Model e_model = new Eva_Task_Answer_Model()
                                {

                                    UniqueNo = item,
                                    Score = Score,
                                };
                            if (_unique_list.Count > 0)
                            {
                                e_model.CreateTime = (DateTime)_unique_list[0].CreateTime;
                            }
                            modelList.Add(e_model);
                        }
                        Eva_Answer_Model_T.IsScore = (byte)task.IsScore;


                        Chek_NoAnswer_Helper(Eva_Answer_Model_T, task, TeacherUID, list, modelList);
                        Chek_Answer_Helper(Eva_Answer_Model_T, list, modelList);

                        foreach (var item in student_cls_list)
                        {
                            foreach (Eva_Task_Answer_Model model in modelList)
                            {

                            }
                        }
                        list = list.OrderBy(t => t.IsAnswer).OrderByDescending(t => t.Score).ToList();
                        Eva_Answer_Model_T.List = list;
                    }
                }
                if (list.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", Eva_Answer_Model_T);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        /// <summary>
        /// （指定的班级评论【未进行评论的人进行统计】）
        /// </summary>
        /// <param name="Eva_Answer_Model_T"></param>
        /// <param name="list"></param>
        /// <param name="modelList"></param>
        private static void Chek_NoAnswer_Helper(Eva_Answer_Model_T Eva_Answer_Model_T, Eva_Task task, string TeacherUID, List<Eva_Answer_Model> list, List<Eva_Task_Answer_Model> modelList)
        {
            var s_all = Get_Students_ContainsClassInfo(TeacherUID, task);
            foreach (var c in s_all)
            {
                foreach (var model in modelList)
                {
                    if (c.stu.UniqueNo != model.UniqueNo)
                    {
                        list.Add(new Eva_Answer_Model()
                        {
                            Class_Name = c.cs.Class_Name,
                            Major_Name = c.major.Major_Name,
                            Name = c.user.Name,
                            StuNo = c.stu.StuNo,
                            IsAnswer = (int)IsAnswer.No,
                        });
                        Eva_Answer_Model_T.NoAnswerCount++;
                        break;
                    }
                }

            }
        }

        /// <summary>
        /// 附加（未指定的班级评论）
        /// </summary>
        /// <param name="Eva_Answer_Model_T"></param>
        /// <param name="list"></param>
        /// <param name="modelList"></param>
        private static void Chek_Answer_Helper(Eva_Answer_Model_T Eva_Answer_Model_T, List<Eva_Answer_Model> list, List<Eva_Task_Answer_Model> modelList)
        {
            var s_all = Get_Students_All();
            var coll2 = (from s in s_all
                         join model in modelList on s.stu.UniqueNo equals model.UniqueNo
                         select new
                         {
                             s,
                             model,
                         });

            foreach (var c in coll2)
            {
                DateTime CreateTime = c.model.CreateTime;
                if (c.s.stu.UniqueNo == c.model.UniqueNo)
                {
                    list.Add(new Eva_Answer_Model()
                    {
                        Class_Name = c.s.cs.Class_Name,
                        Major_Name = c.s.major.Major_Name,
                        Name = c.s.user.Name,
                        StuNo = c.s.stu.StuNo,
                        CreateTime = c.model.CreateTime,
                        Score = c.model.Score,
                        IsAnswer = (int)IsAnswer.Yes,
                    });
                    Eva_Answer_Model_T.HaswerCount++;
                }
            }
        }

        /// <summary>
        /// 未下发的获取已答题的学生信息
        /// </summary>
        /// <param name="context"></param>
        public void Get_Eva_Common_JustAnserStudents(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            try
            {
                //教师身份
                string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
                int Task_Id = RequestHelper.int_transfer(Request, "Task_Id");
                Eva_Answer_Model_T Eva_Answer_Model_T = new global::Eva_Answer_Model_T();
                List<Eva_Answer_Model> list = new List<Eva_Answer_Model>();
                List<Eva_Task_Answer_Model> modelList = new List<Eva_Task_Answer_Model>();
                if (!string.IsNullOrEmpty(TeacherUID))
                {
                    Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Task_Id);
                    if (task != null)
                    {
                        List<C_T> student_cls_list = Get_Students(TeacherUID);

                        //答题的学生
                        List<string> list_list_c = (from tsc in Constant.Eva_TaskAnswer_List
                                                    where tsc.Task_Id == Task_Id
                                                    select tsc.CreateUID).Distinct().ToList();
                        //学生答的详题
                        List<Eva_TaskAnswer> list_list = (from tsc in Constant.Eva_TaskAnswer_List
                                                          where tsc.Task_Id == Task_Id
                                                          select tsc).ToList();

                        //遍历答题的学生（收集分数）
                        foreach (string item in list_list_c)
                        {
                            List<Eva_TaskAnswer> _unique_list = (from s in list_list
                                                                 where s.CreateUID == item
                                                                 select s).ToList();
                            double Score = 0.0;
                            foreach (Eva_TaskAnswer scc in _unique_list)
                            {
                                if (scc.Score != null)
                                {
                                    Score += (double)scc.Score;
                                }
                            }
                            Eva_Task_Answer_Model e_model = new Eva_Task_Answer_Model()
                            {
                                UniqueNo = item,
                                Score = Score,
                            };
                            if (_unique_list.Count > 0)
                            {
                                e_model.CreateTime = (DateTime)_unique_list[0].CreateTime;
                            }

                            modelList.Add(e_model);
                        }
                        Eva_Answer_Model_T.IsScore = (byte)task.IsScore;

                        Chek_Answer_Helper(Eva_Answer_Model_T, list, modelList);

                        list = list.OrderByDescending(t => t.Score).ToList();
                        Eva_Answer_Model_T.List = list;
                    }
                }
                if (list.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", Eva_Answer_Model_T);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        /// <summary>
        /// 即时扫码评价统计
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Get_Eva_Common_A_By_Id(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            int Task_Id = RequestHelper.int_transfer(Request, "Task_Id");
            string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            try
            {
                //获取即时或扫码评价
                Eva_Task Eva_Task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == (int)Task_Id);
                //获取即时或扫码评价的题
                List<Eva_TableDetail> data = (from deatail in Constant.Eva_TableDetail_List
                                              where deatail.Eva_table_Id == Task_Id
                                              orderby deatail.Sort
                                              select deatail).ToList();
                SellModel_Common common = new SellModel_Common();
                List<Score_Model> list = new List<Score_Model>();
                common.feed_anony_model_list = new List<Feed_Anony_Model>();
                common.task = Eva_Task;
                common.TList = new List<List<Score_Model>>();
                common.score_list = new List<double>();

                double allcount_ave = 0.0;
                int answercount = 0;

                List<Eva_TaskAnswer> answers = new List<Eva_TaskAnswer>();
                foreach (Eva_TableDetail item in data)
                {
                    answers = (from an in Constant.Eva_TaskAnswer_List
                               where an.TableDetail_Id == item.Id
                               select an).ToList();
                    Score_Model Score_Model = new Score_Model();
                    //收集答题
                    Feed_Anony_Model model = new Feed_Anony_Model()
                    {
                        Eva_TableDetailId = (int)item.Id,
                        TeacherUID = TeacherUID,
                    };
                    //选项转为人数统计
                    item.OptionA_S = 0;
                    item.OptionB_S = 0;
                    item.OptionC_S = 0;
                    item.OptionD_S = 0;
                    item.OptionE_S = 0;
                    item.OptionF_S = 0;
                    answercount = answers.Count;
                    foreach (Eva_TaskAnswer a in answers)
                    {

                        if (Eva_Task.IsScore == (int)IsScore.Yes)
                        {
                            #region 统计分数

                            if (a.Score != null)
                            {
                                Score_Model.person_score += (double)a.Score;
                            }
                            else
                            {
                                Score_Model.person_score += 0;
                            }
                            Score_Model.self_teacher_count++;

                            #endregion
                        }

                        #region 若为简答题，进行收集 【】

                        //若为简答题，进行收集
                        if (item.QuesType_Id == 3)
                        {
                            //反馈
                            Feed_Anony_Model_Answer Answer = new Feed_Anony_Model_Answer();
                            Answer.AnswerId = (int)a.Id;
                            Answer.Answer = a.Answer;
                            Answer.CreateTime = (DateTime)item.CreateTime;
                            //Answer.AnswerContent =item
                            //详情
                            if (model.Feed_Anony_Model_Answer_List == null)
                            {
                                model.Feed_Anony_Model_Answer_List = new List<Feed_Anony_Model_Answer>();
                            }
                            //填充答案
                            model.Feed_Anony_Model_Answer_List.Add(Answer);
                        }
                        else
                        {
                            //算答题人数
                            switch (a.Answer)
                            {
                                case "OptionA":
                                    item.OptionA_S += 1;
                                    break;
                                case "OptionB":
                                    item.OptionB_S += 1;
                                    break;
                                case "OptionC":
                                    item.OptionC_S += 1;
                                    break;
                                case "OptionD":
                                    item.OptionD_S += 1;
                                    break;
                                case "OptionE":
                                    item.OptionE_S += 1;
                                    break;
                                case "OptionF":
                                    item.OptionF_S += 1;
                                    break;
                                default:
                                    break;
                            }
                        }

                        #endregion
                    }

                    Score_Model.t = item;
                    if (Eva_Task.IsScore == (int)IsScore.Yes)
                    {
                        if (Score_Model.person_score == 0.0)
                        {
                            Score_Model.person_score = 0;
                        }
                        else
                        {
                            Score_Model.person_score = Math.Round(Score_Model.person_score / Score_Model.self_teacher_count, 2);
                            allcount_ave += Score_Model.person_score;
                        }
                    }
                    list.Add(Score_Model);
                    common.feed_anony_model_list.Add(model);


                }

                //获取所有当前教师所教学生
                List<Class_StudentInfo> student_cls_list = Get_Students_Common(TeacherUID, Eva_Task);
                common.TList.Add(list);
                common.score_list.Add(allcount_ave);
                common.score_list.Add(student_cls_list.Count());
                common.score_list.Add(answers.Count);

                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", common);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(3, "failed", "出现异常");
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }

        //上锁
        static object obj_Add_Eva_Common = new object();
        /// <summary>
        /// 新建即时评价
        /// </summary>AddEva_TaskAnswer
        public void Add_Eva_Common(HttpContext context)
        {
            lock (obj_Add_Eva_Common)
            {
                int intSuccess = (int)errNum.Success;
                ////以下为新改动的代码
                HttpRequest Request = context.Request;

                //名称 开始时间 结束时间  最大百分比 最小百分比
                string Name = RequestHelper.string_transfer(Request, "Name");

                string Remarks = RequestHelper.string_transfer(Request, "Remarks");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");

                int FullScore = RequestHelper.int_transfer(Request, "FullScore");
                int IsScore = RequestHelper.int_transfer(Request, "IsScore");
                string Range = RequestHelper.string_transfer(Request, "Range");
                int Status = RequestHelper.int_transfer(Request, "Status");
                string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
                string Type = RequestHelper.string_transfer(Request, "Type");

                try
                {
                    #region 即时评价表格设计表添加

                    //新建即时评价
                    Eva_Task Eva_Task_Add = new Eva_Task()
                    {
                        Name = Name,
                        FullScore = FullScore,
                        IsScore = (byte)IsScore,
                        Range = Range,
                        Status = (byte)Status,
                        Table_Id = 0,
                        //Class_Id = Class_Id,
                        TeacherUID = TeacherUID,
                        Type = Type,
                        //不到分配，不需要时间设置
                        //StartTime = StartTime,
                        //EndTime = EndTime,
                        Remarks = Remarks,
                        CreateTime = DateTime.Now,
                        CreateUID = CreateUID,
                        EditTime = DateTime.Now,
                        EditUID = EditUID,
                        IsPublish = (int)IsPublish.No,
                        IsSued = (int)IsSued.No,
                        IsEnable = (int)IsEnable.Enable,
                        IsDelete = (int)IsDelete.No_Delete,
                    };

                    #endregion

                    jsonModel = Constant.Eva_TaskService.Add(Eva_Task_Add);
                    Eva_Task_Add.Id = RequestHelper.int_transfer(Convert.ToString(jsonModel.retData));
                    Eva_Task_Add.Table_Id = Eva_Task_Add.Id;
                    Constant.Eva_TaskService.Update(Eva_Task_Add);

                    if (jsonModel.errNum == intSuccess && !Constant.Eva_Task_List.Contains(Eva_Task_Add))
                    {

                        //缓存添加
                        Constant.Eva_Task_List.Add(Eva_Task_Add);

                        #region 表格设计详情表添加

                        //表单明细
                        string List = RequestHelper.string_transfer(Request, "List");
                        //序列化表单详情列表
                        List<indicator_list> Table_A_List = JsonConvert.DeserializeObject<List<indicator_list>>(List);

                        //解析正常才可进行操作
                        if (Table_A_List != null)
                        {
                            //开启线程操作数据库
                            new Thread(() =>
                            {
                                foreach (indicator_list item in Table_A_List)
                                {
                                    //调查与测验
                                    string type_table = "2";//

                                    Eva_TableDetail Eva_TableDetail = new Eva_TableDetail()
                                    {
                                        CreateTime = DateTime.Now,
                                        EditTime = DateTime.Now,
                                        CreateUID = CreateUID,
                                        EditUID = EditUID,
                                        Indicator_Id = (int)item.Id,
                                        IndicatorType_Id = (int)item.IndicatorType_Id,
                                        IndicatorType_Name = item.IndicatorType_Name,
                                        OptionA = item.OptionA,
                                        OptionB = item.OptionB,
                                        OptionC = item.OptionC,
                                        OptionD = item.OptionD,
                                        OptionE = item.OptionE,
                                        OptionF = item.OptionF,
                                        OptionA_S = RequestHelper.decimal_transfer(item.OptionA_S),
                                        OptionB_S = RequestHelper.decimal_transfer(item.OptionB_S),
                                        OptionC_S = RequestHelper.decimal_transfer(item.OptionC_S),
                                        OptionD_S = RequestHelper.decimal_transfer(item.OptionD_S),
                                        OptionE_S = RequestHelper.decimal_transfer(item.OptionE_S),
                                        OptionF_S = RequestHelper.decimal_transfer(item.OptionF_S),
                                        //Indicator_Name = item.Name,
                                        IsDelete = (int)IsDelete.No_Delete,
                                        IsEnable = (int)IsEnable.Enable,
                                        Name = item.Name,
                                        QuesType_Id = (int)item.QuesType_Id,
                                        Remarks = item.Remarks,
                                        Sort = RequestHelper.int_transfer(item.Sort),
                                        Eva_table_Id = Eva_Task_Add.Id,
                                        //评价任务 1，表格设计 0
                                        Type = type_table,

                                    };

                                    //添加到缓存
                                    Constant.Eva_TableDetail_List.Add(Eva_TableDetail);
                                    //入库
                                    JsonModel model2 = new Eva_TableDetailService().Add(Eva_TableDetail);
                                    if (model2.errNum == intSuccess)
                                    {
                                        Eva_TableDetail.Id = RequestHelper.int_transfer(Convert.ToString(model2.retData));
                                    }

                                }
                            }) { IsBackground = true }.Start();
                        }

                        #endregion
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "添加失败");
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

        //上锁
        static object obj_Edit_Eva_Common = new object();
        /// <summary>
        /// 评价编辑【即时扫码】
        /// </summary>
        /// <param name="context"></param>
        public void Edit_Eva_Common(HttpContext context)
        {
            lock (obj_Edit_Eva_Common)
            {
                int intSuccess = (int)errNum.Success;
                ////以下为新改动的代码
                HttpRequest Request = context.Request;

                //名称 开始时间 结束时间  最大百分比 最小百分比
                string Name = RequestHelper.string_transfer(Request, "Name");
                string Remarks = RequestHelper.string_transfer(Request, "Remarks");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                int FullScore = RequestHelper.int_transfer(Request, "FullScore");
                int IsScore = RequestHelper.int_transfer(Request, "IsScore");
                string Range = RequestHelper.string_transfer(Request, "Range");
                int Status = RequestHelper.int_transfer(Request, "Status");
                string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
                string Type = RequestHelper.string_transfer(Request, "Type");

                int Id = RequestHelper.int_transfer(Request, "Id");

                try
                {


                    Eva_Task Eva_Task_Edit = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);
                    if (Eva_Task_Edit != null)
                    {
                        //克隆该即时扫码评价
                        Eva_Task Eva_Task_Clone = Constant.Clone<Eva_Task>(Eva_Task_Edit);
                        Eva_Task_Clone.Name = Name;
                        Eva_Task_Clone.FullScore = FullScore;
                        Eva_Task_Clone.IsScore = (byte)IsScore;
                        Eva_Task_Clone.Range = Range;
                        Eva_Task_Clone.Status = (byte)Status;
                        Eva_Task_Clone.Table_Id = 0;
                        Eva_Task_Clone.TeacherUID = TeacherUID;
                        Eva_Task_Clone.Type = Type;
                        Eva_Task_Clone.Remarks = Remarks;
                        Eva_Task_Clone.EditTime = DateTime.Now;
                        Eva_Task_Clone.EditUID = EditUID;

                        jsonModel = new Eva_TaskService().Update(Eva_Task_Clone);
                        if (jsonModel.errNum == intSuccess)
                        {
                            Eva_Task_Edit.Name = Name;
                            Eva_Task_Edit.FullScore = FullScore;
                            Eva_Task_Edit.IsScore = (byte)IsScore;
                            Eva_Task_Edit.Range = Range;
                            Eva_Task_Edit.Status = (byte)Status;
                            Eva_Task_Edit.Table_Id = 0;
                            Eva_Task_Edit.TeacherUID = TeacherUID;
                            Eva_Task_Edit.Type = Type;
                            Eva_Task_Edit.Remarks = Remarks;
                            Eva_Task_Edit.EditTime = DateTime.Now;
                            Eva_Task_Edit.EditUID = EditUID;

                            #region 表格设计详情表添加

                            //表单明细
                            string List = RequestHelper.string_transfer(Request, "List");
                            //序列化表单详情列表
                            List<indicator_list> Table_A_List = JsonConvert.DeserializeObject<List<indicator_list>>(List);

                            //解析正常才可进行操作
                            if (Table_A_List != null)
                            {
                                //开启线程操作数据库
                                new Thread(() =>
                                {
                                    List<Eva_TableDetail> list_need_clear = (from s in Constant.Eva_TableDetail_List
                                                                             where s.Eva_table_Id == Eva_Task_Edit.Id
                                                                             select s).ToList();
                                    //删除原有的题
                                    foreach (Eva_TableDetail item in list_need_clear)
                                    {
                                        item.IsDelete = (int)IsDelete.Delete;
                                        JsonModel m2 = Constant.Eva_TableDetailService.Update(item);
                                        if (m2.errNum == intSuccess)
                                        {
                                            Constant.Eva_TableDetail_List.Remove(item);
                                        }
                                    }

                                    #region 新增的题

                                    foreach (indicator_list item in Table_A_List)
                                    {
                                        //调查与测验
                                        string type_table = "2";//
                                        Eva_TableDetail Eva_TableDetail = new Eva_TableDetail()
                                        {
                                            CreateTime = DateTime.Now,
                                            EditTime = DateTime.Now,
                                            CreateUID = CreateUID,
                                            EditUID = EditUID,
                                            Indicator_Id = (int)item.Id,
                                            IndicatorType_Id = (int)item.IndicatorType_Id,
                                            IndicatorType_Name = item.IndicatorType_Name,
                                            OptionA = item.OptionA,
                                            OptionB = item.OptionB,
                                            OptionC = item.OptionC,
                                            OptionD = item.OptionD,
                                            OptionE = item.OptionE,
                                            OptionF = item.OptionF,
                                            OptionA_S = RequestHelper.decimal_transfer(item.OptionA_S),
                                            OptionB_S = RequestHelper.decimal_transfer(item.OptionB_S),
                                            OptionC_S = RequestHelper.decimal_transfer(item.OptionC_S),
                                            OptionD_S = RequestHelper.decimal_transfer(item.OptionD_S),
                                            OptionE_S = RequestHelper.decimal_transfer(item.OptionE_S),
                                            OptionF_S = RequestHelper.decimal_transfer(item.OptionF_S),
                                            //Indicator_Name = item.Name,
                                            IsDelete = (int)IsDelete.No_Delete,
                                            IsEnable = (int)IsEnable.Enable,
                                            Name = item.Name,
                                            QuesType_Id = (int)item.QuesType_Id,
                                            Remarks = item.Remarks,
                                            Sort = RequestHelper.int_transfer(item.Sort),
                                            Eva_table_Id = Eva_Task_Edit.Id,
                                            //评价任务 1，表格设计 0
                                            Type = type_table,

                                        };



                                        //入库
                                        JsonModel model2 = new Eva_TableDetailService().Add(Eva_TableDetail);
                                        if (model2.errNum == intSuccess)
                                        {
                                            Eva_TableDetail.Id = RequestHelper.int_transfer(Convert.ToString(model2.retData));
                                            //添加到缓存
                                            Constant.Eva_TableDetail_List.Add(Eva_TableDetail);
                                        }
                                    }
                                    #endregion
                                }) { IsBackground = true }.Start();
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "添加失败");
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

        /// <summary>
        /// 删除即时评价，扫码评价
        /// </summary>
        public void Delete_Eva_Common(HttpContext context)
        {
            HttpRequest Request = context.Request;
            int intSuccess = (int)errNum.Success;
            //ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                //即时评价，扫码评价
                Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);
                if (task != null)
                {
                    //克隆该评价
                    Eva_Task task_Clone = Constant.Clone<Eva_Task>(task);
                    task_Clone.IsDelete = (int)IsDelete.Delete;
                    //扫描即时评价变更
                    JsonModel model1 = Constant.Eva_TaskService.Update(task_Clone);

                    if (model1.errNum == intSuccess)
                    {
                        Constant.Eva_Task_List.Remove(task);

                        #region 删除表格设计信息

                        ////开启线程操作数据库
                        //new Thread(() =>
                        //{
                        //    Eva_Table table_get = Constant.Eva_Table_List.FirstOrDefault(p => p.Id == task.Table_Id);
                        //    if (table_get != null)
                        //    {
                        //        table_get.IsDelete = 1;
                        //        JsonModel mdoel = Constant.Eva_TableService.Update(table_get);
                        //        if (mdoel.errNum == 0)
                        //        {
                        //            Constant.Eva_Table_List.Remove(table_get);

                        //        }
                        //    }
                        //}) { IsBackground = true }.Start();

                        #endregion

                        jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", "删除成功");
                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        /// <summary>
        /// 查看即时评价，扫码评价
        /// </summary>
        public void Get_Eva_Common_ById(HttpContext context)
        {
            HttpRequest Request = context.Request;
            int intSuccess = (int)errNum.Success;
            //即时、扫码评价ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);
                if (task != null)
                {
                    eva eva = new eva();
                    eva.Eva_Task = task;
                    List<Eva_TableDetail> collection = (from TableDetail in Constant.Eva_TableDetail_List
                                                        where TableDetail.Eva_table_Id == task.Id
                                                        orderby TableDetail.Sort
                                                        select TableDetail).ToList();

                    //去除重复
                    List<int> id_lists = new List<int>();
                    List<Eva_TableDetail> t_list = new List<Eva_TableDetail>();

                    //去除重复
                    foreach (Eva_TableDetail item in collection)
                    {
                        int id = Convert.ToInt32(item.IndicatorType_Id);
                        if (!id_lists.Contains(id))
                        {
                            id_lists.Add(id);
                            t_list.Add(item);
                        }
                    }
                    //收集
                    foreach (Eva_TableDetail t in t_list)
                    {
                        if (eva.eva_detail_list == null)
                        {
                            eva.eva_detail_list = new List<eva_detail>();
                        }
                        eva_detail eva_detail = new eva_detail() { indicator_type_tid = Convert.ToString(t.IndicatorType_Id), indicator_type_tname = t.IndicatorType_Name };
                        eva.eva_detail_list.Add(eva_detail);

                        eva_detail.indicator_list = (from cl in collection
                                                     where cl.IndicatorType_Id == t.IndicatorType_Id
                                                     select cl);
                    }

                    //返回所有表格数据
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", eva);

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
        public void Get_Eva_Common_ById_Mobile(HttpContext context)
        {
            HttpRequest Request = context.Request;
            int intSuccess = (int)errNum.Success;
            //即时、扫码评价ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);
                if (task != null)
                {
                    eva eva = new eva();
                    eva.Eva_Task = task;
                    List<Eva_TableDetail> collection = (from TableDetail in Constant.Eva_TableDetail_List
                                                        where TableDetail.Eva_table_Id == task.Table_Id && TableDetail.Type == Convert.ToString((int)TableDetail_Type.Check)
                                                        orderby TableDetail.Sort
                                                        select TableDetail).ToList();
                    //--------------------------------------------------------------------------------------------------------------------------------
                    var last_Select = new { Eva_Task = task, eva_detail_list = collection };
                    //返回所有表格数据
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", last_Select);
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
        /// <summary>
        /// 分配即时评价
        /// </summary>
        public void Distribution_Eva_Common(HttpContext context)
        {
            HttpRequest Request = context.Request;

            int intSuccess = (int)errNum.Success;
            //即时、扫码评价ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            DateTime StartTime = RequestHelper.DateTime_transfer(Request, "StartTime");
            DateTime EndTime = RequestHelper.DateTime_transfer(Request, "EndTime");
            //班级ID
            string Class_Ids = RequestHelper.string_transfer(Request, "Class_Ids");
            //班级id集合
            string[] class_ids_int = Split_Hepler.str_to_stringss(Class_Ids);
            int IsSued = RequestHelper.int_transfer(Request, "IsSued");
            int IsPublish = RequestHelper.int_transfer(Request, "IsPublish");
            try
            {
                Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);
                if (task != null)
                {
                    if (class_ids_int.Count() > 0)
                    {
                        task.StartTime = StartTime;
                        task.EndTime = EndTime;
                        task.Class_Id = Class_Ids;
                        task.Status = 2;
                        task.Equal_Flg = System.Guid.NewGuid().ToString("N");
                        task.IsPublish = (byte)IsPublish;
                        task.IsSued = (byte)IsSued;

                        jsonModel = Constant.Eva_TaskService.Update(task);
                        if (jsonModel.errNum == intSuccess)
                        {
                            jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", "分配成功");
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "分配失败");
                        }
                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        /// <summary>
        /// 获取当前教师的班级
        /// </summary>
        public void Get_Teacher_Class(HttpContext context)
        {
            HttpRequest Request = context.Request;
            int intSuccess = (int)errNum.Success;
            string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            try
            {
                //获取当前教师的班级               
                List<ClassInfo> list = Course_ClassInfo_ByTeacher(TeacherUID);
                if (list.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        //上锁
        static object obj_AddEva_TaskAnswer = new object();
        /// <summary>
        /// 新建即时和扫码答题
        /// </summary>
        public void AddEva_TaskAnswer(HttpContext context)
        {
            lock (obj_AddEva_TaskAnswer)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                int Task_Id = RequestHelper.int_transfer(Request, "Task_Id");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                //表单明细
                string List = RequestHelper.string_transfer(Request, "List");
                try
                {
                    #region 即时和扫码答题

                    //序列化表单详情列表
                    List<Eva_TaskAnswer> Eva_QuestionAnswer_Detail_List = JsonConvert.DeserializeObject<List<Eva_TaskAnswer>>(List);

                    //答题任务详情填充
                    foreach (Eva_TaskAnswer item in Eva_QuestionAnswer_Detail_List)
                    {
                        item.Task_Id = Task_Id;
                        item.TableDetail_Id = item.TableDetail_Id;
                        item.Answer = item.Answer;
                        item.Score = item.Score;
                        item.CreateTime = DateTime.Now;
                        item.CreateUID = CreateUID;
                        item.EditTime = DateTime.Now;
                        item.EditUID = EditUID;
                        item.IsEnable = (int)IsEnable.Enable;
                        item.IsDelete = (int)IsDelete.No_Delete;

                        //数据库插入
                        jsonModel = new Eva_TaskAnswerService().Add(item);
                        //插入成功入缓存
                        if (jsonModel.errNum == intSuccess)
                        {
                            item.Id = Convert.ToInt32(jsonModel.retData);
                            Constant.Eva_TaskAnswer_List.Add(item);
                        }
                    }

                    #endregion
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

        #region 即时和扫码 判断是否已经答过题
        public void GetIsAnswer_Eva_ById(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //即时、扫码评价ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");
            try
            {
                Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);//即时、扫码任务
                if (task != null)
                {
                    if (DateTime.Now.Date < Convert.ToDateTime(task.StartTime).Date)
                    {
                        jsonModel = JsonModel.get_jsonmodel(2, "failed", "评价还未开始");
                        return;
                    }
                    if (DateTime.Now.Date > Convert.ToDateTime(task.EndTime).Date)
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "二维码已失效");
                        return;
                    }
                    int ansCount = Constant.Eva_TaskAnswer_List.Where(t => t.Task_Id == Id && t.CreateUID == StudentUID).Count();
                    if (ansCount > 0)
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "success", "您已经答过该评价了");
                        return;
                    }
                    if (!string.IsNullOrEmpty(task.Class_Id))
                    {
                        string[] classids = (from rel in Constant.Class_StudentInfo_List
                                             where rel.UniqueNo == StudentUID
                                             select rel.Class_Id).ToArray();//班级                    
                        int claCount = task.Class_Id.Split(',').Intersect(classids).Count();
                        if (claCount <= 0)
                        {
                            jsonModel = JsonModel.get_jsonmodel(5, "failed", "您不在调查范围内！");
                            return;
                        }
                    }
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", 0);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(4, "failed", "没有该评价");
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

        #region 即时和扫码-学生查看试卷

        public void Get_Eva_TaskAnswer_ById(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //即时、扫码评价ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);//即时、扫码任务
            string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");
            string type = Convert.ToString((int)TableDetail_Type.Check);
            if (!String.IsNullOrEmpty(RequestHelper.string_transfer(Request, "Type")))
            {
                type = RequestHelper.string_transfer(Request, "Type");
            }
            try
            {
                if (task != null)
                {
                    //试卷详情
                    List<Eva_TableDetail> collection = Constant.Eva_TableDetail_List.Where(p => p.Eva_table_Id == Id && p.Type == type).ToList();
                    //学生答题详情
                    List<Eva_TaskAnswer> ans_list = Constant.Eva_TaskAnswer_List.Where(t => t.Task_Id == Id && t.CreateUID == StudentUID).ToList();
                    //获取指标分类
                    var inid_Select = (from col in collection
                                       orderby col.Sort
                                       select new { col.IndicatorType_Id, col.IndicatorType_Name }).Distinct();
                    var ques_Select = (from inid in inid_Select
                                       select new
                                       {
                                           indicator_type_tid = inid.IndicatorType_Id.ToString(),
                                           indicator_type_tname = inid.IndicatorType_Name,
                                           indicator_list = TableDetail_TaskAnswerByIndicatorType_Id(collection, inid.IndicatorType_Id, ans_list)
                                       }).ToList();
                    var last_Select = new { Eva_Task = task, eva_detail_list = ques_Select };
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", last_Select);
                }
                else { jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据"); }
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

        #region 即时和扫码-学生查看试卷-手机端
        public void Get_Eva_TaskAnswer_ById_Mobile(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            try
            {
                //即时、扫码评价ID
                int Id = RequestHelper.int_transfer(Request, "Id");
                Eva_Task task = Constant.Eva_Task_List.FirstOrDefault(t => t.Id == Id);//即时、扫码任务
                string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");

                if (task != null)
                {
                    //试卷详情
                    List<Eva_TableDetail> collection = Constant.Eva_TableDetail_List.Where(p => p.Eva_table_Id == task.Table_Id && p.Type == Convert.ToString((int)TableDetail_Type.Check)).OrderBy(t => t.Sort).ToList();
                    //学生答题详情
                    List<Eva_TaskAnswer> ans_list = Constant.Eva_TaskAnswer_List.Where(t => t.Task_Id == Id && t.CreateUID == StudentUID).ToList();
                    var ques_Select = TableDetail_TaskAnswer(collection, ans_list);
                    var last_Select = new { Eva_Task = task, eva_detail_list = ques_Select };
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", last_Select);
                }
                else { jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据"); }
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

        #region 获取当前学生的老师信息

        /// <summary>
        /// 获取当前学生的老师信息
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void GetTeacherBy_Student(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //学生身份
            string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");

            try
            {
                //获取当前学生的所有授课老师
                List<UserInfo> teacher_List = Get_Teacher_List(StudentUID);
                if (teacher_List.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", teacher_List);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "success", teacher_List);
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

        #region 答卷初始化【学生\专家\领导答题的初始化答卷】

        /// <summary>
        /// 获取设计表详情【学生答题的初始化答卷,已答卷】
        /// </summary>
        /// <param name="context"></param>
        public void Get_Eva_TableDetail_HasAnswer(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;

            HttpRequest Request = context.Request;
            //表格ID
            int table_Id = RequestHelper.int_transfer(Request, "table_Id");

            //教师
            string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            //学生
            string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");


            //教学评教员
            string EduStudentUID = RequestHelper.string_transfer(Request, "EduStudentUID");
            //课程
            string CourseId = RequestHelper.string_transfer(Request, "CourseId");
            int Eva_Distribution_Id = RequestHelper.int_transfer(Request, "Eva_Distribution_Id");
            try
            {
                eva_Answer eva_Answer = new eva_Answer();
                eva_Answer.Eva_Table = Constant.Eva_Table_List.FirstOrDefault(i => i.Id == table_Id);
                if (eva_Answer.Eva_Table != null)
                {
                    //详细题收集
                    List<Eva_TableDetail> collection = (from TableDetail in Constant.Eva_TableDetail_List
                                                        where TableDetail.Eva_table_Id == table_Id
                                                        orderby TableDetail.Sort
                                                        select TableDetail).ToList();
                    //去除重复
                    List<int> id_lists = new List<int>();
                    List<Eva_TableDetail> t_list = new List<Eva_TableDetail>();
                    //去除重复
                    foreach (Eva_TableDetail item in collection)
                    {
                        int id = Convert.ToInt32(item.IndicatorType_Id);
                        if (!id_lists.Contains(id))
                        {
                            id_lists.Add(id);
                            t_list.Add(item);
                        }
                    }
                    t_list = (from l in t_list
                              orderby l.Sort
                              select l).ToList();
                    //收集
                    foreach (Eva_TableDetail t in t_list)
                    {
                        if (string.IsNullOrEmpty(EduStudentUID))
                        {
                            IndicatorType select = Constant.IndicatorType_List.FirstOrDefault(p => p.Id == t.IndicatorType_Id);

                            if (select.Type == (int)IndicatorType_Type.Common)
                            {
                                Get_Eva_List_Helper(TeacherUID, StudentUID, CourseId, eva_Answer, collection, t);
                            }
                            else if (select.Type == (int)IndicatorType_Type.Edu_Back)
                            {
                                Get_Eva_List_Helper2(TeacherUID, StudentUID, CourseId, eva_Answer, collection, t, Eva_Distribution_Id);
                            }
                        }

                    }
                }

                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", eva_Answer);
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

        private static void Get_Eva_List_Helper(string TeacherUID, string StudentUID, string CourseId, eva_Answer eva_Answer, List<Eva_TableDetail> collection, Eva_TableDetail t)
        {
            try
            {
                if (eva_Answer.eva_detail_list == null)
                {
                    eva_Answer.eva_detail_list = new List<eva_detail_answer>();
                }
                eva_detail_answer eva_detail = new eva_detail_answer() { indicator_type_tid = Convert.ToString(t.IndicatorType_Id), indicator_type_tname = t.IndicatorType_Name };
                eva_Answer.eva_detail_list.Add(eva_detail);

                eva_detail.indicator_list = (from cl in collection
                                             where cl.IndicatorType_Id == t.IndicatorType_Id
                                             select cl).ToList();

                Eva_QuestionAnswer answer_model = Constant.Eva_QuestionAnswer_List.FirstOrDefault(tc => tc.TeacherUID == TeacherUID && tc.StudentUID == StudentUID
                    && tc.CourseId == CourseId);
                if (answer_model != null)
                {

                    eva_detail.answer_detail_list = (from answer in Constant.Eva_QuestionAnswer_Detail_List
                                                     where answer.Eva_TaskAnswer_Id == answer_model.Id
                                                     select answer).ToList();

                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private static void Get_Eva_List_Helper2(string TeacherUID, string StudentUID, string CourseId, eva_Answer eva_Answer, List<Eva_TableDetail> collection, Eva_TableDetail t, int Eva_Distribution_Id)
        {
            try
            {
                if (eva_Answer.eva_detail_list_2 == null)
                {
                    eva_Answer.eva_detail_list_2 = new List<eva_detail_answer>();
                }
                eva_detail_answer eva_detail = new eva_detail_answer() { indicator_type_tid = Convert.ToString(t.IndicatorType_Id), indicator_type_tname = t.IndicatorType_Name };
                eva_Answer.eva_detail_list_2.Add(eva_detail);

                eva_detail.indicator_list = (from cl in collection where cl.IndicatorType_Id == t.IndicatorType_Id select cl).ToList();
                eva_detail.answer_detail_list = (from f_ans in Constant.Eva_TeacherAnswer_List
                                                 where f_ans.TeacherUID == TeacherUID && f_ans.Eva_Distribution_Id == Eva_Distribution_Id && f_ans.CourseId == CourseId
                                                 select new Eva_QuestionAnswer_Detail
                                                 {
                                                     TableDetail_Id = f_ans.Indicator_Id,
                                                     Answer = f_ans.Answer
                                                 }).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }



        #endregion

        #endregion

        #region 教师反馈答题维护

        //上锁
        static object obj_Add_Eva_TeacherAnswer = new object();
        public void Add_Eva_TeacherAnswer(HttpContext context)
        {
            lock (obj_Add_Eva_TeacherAnswer)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;

                //教师身份
                string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
                bool result = true;
                //具体
                string List = RequestHelper.string_transfer(Request, "List");
                string CourseId = RequestHelper.string_transfer(Request, "CourseId");
                try
                {
                    //序列化表单详情列表
                    List<Eva_TeacherAnswer> Eva_TeacherAnswer_List = JsonConvert.DeserializeObject<List<Eva_TeacherAnswer>>(List);

                    foreach (Eva_TeacherAnswer ans in Eva_TeacherAnswer_List)
                    {
                        //Indicator_Id = Indicator_Id,
                        //Eva_Distribution_Id = Eva_Distribution_Id,
                        //ans.Answer = Answer,
                        ans.CourseId = CourseId;
                        ans.TeacherUID = TeacherUID;
                        ans.CreateTime = DateTime.Now;
                        ans.IsDelete = (int)IsDelete.No_Delete;
                        ans.IsEnable = (int)IsEnable.Enable;
                        ans.EditTime = DateTime.Now;
                        ans.EditUID = TeacherUID;
                        ans.CreateUID = TeacherUID;

                        JsonModel Model = new Eva_TeacherAnswerService().Add(ans);
                        if (Model.errNum == intSuccess)
                        {
                            Constant.Eva_TeacherAnswer_List.Add(ans);
                        }
                        else
                        {
                            result = false;
                        }
                    }

                    if (result)
                    {
                        jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", "添加成功");
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "添加失败");
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

        public void Get_Eva_TeacherAnswer(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //教师身份
            string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
            int Eva_Distribution_Id = RequestHelper.int_transfer(Request, "Eva_Distribution_Id");
            int Indicator_Id = RequestHelper.int_transfer(Request, "Indicator_Id");

            try
            {
                //获取教师反馈的具体列表
                List<Eva_TeacherAnswer> list = (from t in Constant.Eva_TeacherAnswer_List
                                                where t.TeacherUID == TeacherUID && t.Eva_Distribution_Id == Eva_Distribution_Id
                                                select t).ToList();
                if (list.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
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

        #region 判断是否有专家评教的数据

        public void CheckHasExpertRegu(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;

            int Eva_Role = RequestHelper.int_transfer(Request, "Eva_Role");
            try
            {
                //教师          
                List<Eva_Regular> sele2 = GetLastRegu(Eva_Role);
                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", sele2.Count);
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

      
        #region 获取近期的定期评价


        /// <summary>
        /// 定期评价  Eva_Role 1:学生评教师  2:专家评教师 【专家、领导、教学信息员】
        /// </summary>
        /// <param name="Eva_Role"></param>
        /// <returns></returns>
        public static List<Eva_Regular> GetLastRegu(int Eva_Role)
        {
            List<Eva_Regular> list = new List<Eva_Regular>();
            try
            {
                list = (
                       from regu in Constant.Eva_Regular_List
                       where regu.StartTime < DateTime.Now && ((DateTime)regu.EndTime).AddDays(1) > DateTime.Now 
                       select regu).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return list;
        }


        #endregion

    }

}



