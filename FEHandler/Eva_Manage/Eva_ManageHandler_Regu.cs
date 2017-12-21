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
    partial class Eva_ManageHandler
    {
        static List<string> types = new List<string>() { Convert.ToString((int)Dictionary_Type.Common_Course_Type), Convert.ToString((int)Dictionary_Type.Edu_Course_Type), Convert.ToString((int)Dictionary_Type.Leader_Course_Type) };

        #region 获取定期评价

        /// <summary>
        ///获取课程类型
        /// </summary>
        public void Get_Eva_Regular(HttpContext context)
        {

            HttpRequest Request = context.Request;
            int Type = RequestHelper.int_transfer(Request, "Type");
            try
            {
                jsonModel = Get_Eva_RegularHelper(Type);
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

        public static JsonModel Get_Eva_RegularHelper(int Type)
        {
            JsonModel model = new JsonModel();
            int intSuccess = (int)errNum.Success;
            try
            {
                var list = (from regu in Constant.Eva_Regular_List
                            where regu.Type == Type
                            join section in Constant.StudySection_List on regu.Section_Id equals section.Id
                            select new
                            {
                                SectionId = section.Id,
                                Value = regu.Name,
                                section.DisPlayName,
                                Id = regu.Id,
                                Study_IsEnable = section.IsEnable,
                                section.EndTime,
                            }).ToList();
                foreach (var item in Constant.StudySection_List)
                {
                    var li = list.FirstOrDefault(i => i.SectionId == item.Id);
                    if (li == null)
                    {
                        list.Add(new
                        {
                            SectionId = item.Id,
                            Value = "",
                            item.DisPlayName,
                            Id = (int?)0,
                            Study_IsEnable = item.IsEnable,
                            item.EndTime,
                        });
                    }
                }

                list = (from li in list orderby li.EndTime select li).ToList();
                //返回所有表格数据
                model = JsonModel.get_jsonmodel(intSuccess, "success", list);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return model;
        }

        #endregion

        #region 删除定期评价

        /// <summary>
        /// 删除定期评价
        /// </summary>
        public void Delete_Eva_Regular(HttpContext context)
        {
            HttpRequest Request = context.Request;
            //指标的ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                jsonModel = Delete_Eva_RegularHelper(Id);
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

        public static JsonModel Delete_Eva_RegularHelper(int Id)
        {
            JsonModel model = new JsonModel();
            int intSuccess = (int)errNum.Success;
            try
            {
                //获取指定要删除的定期评价
                Eva_Regular Eva_Regular_delete = Constant.Eva_Regular_List.FirstOrDefault(i => i.Id == Id);
                if (Eva_Regular_delete != null)
                {
                    //数据库操作成功再改缓存
                    model = Constant.Eva_RegularService.Delete((int)Eva_Regular_delete.Id);
                    if (model.errNum == intSuccess)
                    {
                        Eva_Regular_delete.IsDelete = (int)IsDelete.Delete;
                        Constant.Eva_Regular_List.Remove(Eva_Regular_delete);
                    }
                    else
                    {
                        model = JsonModel.get_jsonmodel(3, "failed", "删除失败");
                    }
                }
                else
                {
                    model = JsonModel.get_jsonmodel(3, "failed", "该评价不存在");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return model;
        }

        #endregion

        #region 定期评价管理

        //上锁
        static object obj_Add_Eva_Regular = new object();
        /// <summary>
        /// 新增定期评价
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Add_Eva_Regular(HttpContext context)
        {
            lock (obj_Add_Eva_Regular)
            {
                HttpRequest Request = context.Request;
                //名称 开始时间 结束时间  最大百分比 最小百分比
                string Name = RequestHelper.string_transfer(Request, "Name");
                DateTime StartTime = RequestHelper.DateTime_transfer(Request, "StartTime");
                DateTime EndTime = RequestHelper.DateTime_transfer(Request, "EndTime");
                byte LookType = Convert.ToByte(Request["LookType"]);
                DateTime Look_StartTime = RequestHelper.DateTime_transfer(Request, "Look_StartTime");
                DateTime Look_EndTime = RequestHelper.DateTime_transfer(Request, "Look_EndTime");
                string MaxPercent = RequestHelper.string_transfer(Request, "MaxPercent");
                string MinPercent = RequestHelper.string_transfer(Request, "MinPercent");
                string Remarks = RequestHelper.string_transfer(Request, "Remarks");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                int Section_Id = RequestHelper.int_transfer(Request, "Section_Id");
                int Type = RequestHelper.int_transfer(Request, "Type");
                int TableID = RequestHelper.int_transfer(Request, "TableID");
                string DepartmentIDs = RequestHelper.string_transfer(Request, "DepartmentIDs");
                bool hasAcross = false;
                try
                {
                    #region 查看是否交叉

                    foreach (Eva_Regular regu in Constant.Eva_Regular_List)
                    {
                        if (StartTime <= regu.StartTime && EndTime >= regu.EndTime && regu.Type == Type)
                        {
                            hasAcross = true;
                            break;
                        }
                        if (StartTime >= regu.StartTime && StartTime <= regu.EndTime && regu.Type == Type)
                        {
                            hasAcross = true;
                            break;
                        }
                        if (EndTime >= regu.StartTime && EndTime <= regu.EndTime && regu.Type == Type)
                        {
                            hasAcross = true;
                            break;
                        }
                    }

                    #endregion

                    if (!hasAcross)
                    {
                        //新建定期评价
                        Eva_Regular Eva_Regular_Add = new Eva_Regular()
                        {
                            Name = Name,
                            Section_Id = Section_Id,
                            StartTime = StartTime,
                            EndTime = EndTime,
                            LookType = LookType,
                            Look_StartTime = Look_StartTime,
                            Look_EndTime = Look_EndTime,
                            MaxPercent = MaxPercent,
                            MinPercent = MinPercent,
                            Remarks = Remarks,
                            CreateTime = DateTime.Now,
                            CreateUID = CreateUID,
                            EditTime = DateTime.Now,
                            EditUID = EditUID,
                            IsEnable = (int)IsEnable.Enable,
                            IsDelete = (int)IsDelete.No_Delete,
                            Type = Type,
                            TableID = TableID,
                            DepartmentIDs = DepartmentIDs,
                        };
                        //数据库添加
                        jsonModel = Constant.Eva_RegularService.Add(Eva_Regular_Add);
                        if (jsonModel.errNum == 0)
                        {
                            //从数据库返回的ID绑定
                            Eva_Regular_Add.Id = Convert.ToInt32(jsonModel.retData);
                            //缓存添加
                            Constant.Eva_Regular_List.Add(Eva_Regular_Add);
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "添加失败");
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "和已知的定期评价时间交叉");
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

        private static void Reg_Table_Add_Helper(string CreateUID, string EditUID, Eva_Regular Eva_Regular, int[] tables, int[] eva_roles, string[] course_types)
        {
            try
            {
                //匹配并进行关联
                for (int i = 0; i < tables.Count(); i++)
                {
                    //定期评价添加
                    Eva_Distribution distribution = new Eva_Distribution()
                    {
                        CousrseType_Id = course_types[i],
                        Evaluate_Id = Eva_Regular.Id,
                        Eva_Role = eva_roles[i],
                        CreateTime = DateTime.Now,
                        CreateUID = CreateUID,
                        EditTime = DateTime.Now,
                        EditUID = EditUID,
                        IsDelete = (int)IsDelete.No_Delete,
                        IsEnable = (int)IsEnable.Enable,
                        Table_Id = tables[i]
                    };


                    JsonModel model = Constant.Eva_DistributionService.Add(distribution);

                    if (model.errNum == (int)errNum.Success)
                    {
                        if (!Constant.Eva_Distribution_List.Contains(distribution))
                        {
                            Constant.Eva_Distribution_List.Add(distribution);
                            distribution.Id = RequestHelper.int_transfer(Convert.ToString(model.retData));

                            //表格的计数
                            Eva_Table table = Constant.Eva_Table_List.FirstOrDefault(t => t.Id == tables[i]);
                            //直接进行更改
                            if (table != null)
                            {
                                table.UseTimes += 1;
                                JsonModel m = Constant.Eva_TableService.Update(table);
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

        /// <summary>
        /// 修改定期评价
        /// </summary>
        public void Edit_Eva_Regular(HttpContext context)
        {
            HttpRequest Request = context.Request;
            //指标的ID
            int Id = RequestHelper.int_transfer(Request, "Id");

            //指标名称
            string Name = RequestHelper.string_transfer(Request, "Name");
            int intSuccess = (int)errNum.Success;
            DateTime StartTime = RequestHelper.DateTime_transfer(Request, "StartTime");
            DateTime EndTime = RequestHelper.DateTime_transfer(Request, "EndTime");
            byte LookType = Convert.ToByte(Request["LookType"]);
            DateTime Look_StartTime = RequestHelper.DateTime_transfer(Request, "Look_StartTime");
            DateTime Look_EndTime = RequestHelper.DateTime_transfer(Request, "Look_EndTime");
            string MaxPercent = RequestHelper.string_transfer(Request, "MaxPercent");
            string MinPercent = RequestHelper.string_transfer(Request, "MinPercent");
            string Remarks = RequestHelper.string_transfer(Request, "Remarks");
            string EditUID = RequestHelper.string_transfer(Request, "EditUID");
            int Section_Id = RequestHelper.int_transfer(Request, "Section_Id");
            try
            {
                //获取指定要删除的定期评价
                Eva_Regular Eva_Regular_edit = Constant.Eva_Regular_List.FirstOrDefault(i => i.Id == Id);

                if (Eva_Regular_edit != null)
                {


                    //克隆该定期评价
                    Eva_Regular indic = Constant.Clone<Eva_Regular>(Eva_Regular_edit);
                    indic.Name = Name;
                    indic.StartTime = StartTime;
                    indic.EndTime = EndTime;
                    indic.LookType = LookType;
                    indic.Look_StartTime = Look_StartTime;
                    indic.Look_EndTime = Look_EndTime;
                    indic.MaxPercent = MaxPercent;
                    indic.MinPercent = MinPercent;
                    indic.Remarks = Remarks;
                    indic.EditUID = EditUID;
                    indic.Section_Id = Section_Id;

                    //数据库操作成功再改缓存
                    jsonModel = Constant.Eva_RegularService.Update(indic);
                    if (jsonModel.errNum == intSuccess)
                    {
                        Eva_Regular_edit.Name = Name;
                        Eva_Regular_edit.StartTime = StartTime;
                        Eva_Regular_edit.EndTime = EndTime;
                        Eva_Regular_edit.LookType = LookType;
                        Eva_Regular_edit.Look_StartTime = Look_StartTime;
                        Eva_Regular_edit.Look_EndTime = Look_EndTime;
                        Eva_Regular_edit.MaxPercent = MaxPercent;
                        Eva_Regular_edit.MinPercent = MinPercent;
                        Eva_Regular_edit.Remarks = Remarks;
                        Eva_Regular_edit.EditUID = EditUID;
                        Eva_Regular_edit.Section_Id = Section_Id;

                        //表格ID
                        string Table_IDs = RequestHelper.string_transfer(Request, "Table_IDs");
                        string Course_TypeIDs = RequestHelper.string_transfer(Request, "Course_TypeIDs");
                        string Eva_Roles = RequestHelper.string_transfer(Request, "Eva_Roles");
                        //类型转换
                        int[] tables = Split_Hepler.str_to_ints(Table_IDs);
                        string[] course_types = Split_Hepler.str_to_stringss(Course_TypeIDs);

                        int[] eva_roles = Split_Hepler.str_to_ints(Eva_Roles);
                        //获取相关的分配
                        List<Eva_Distribution> distri_list = (from dis in Constant.Eva_Distribution_List
                                                              where dis.Evaluate_Id == Eva_Regular_edit.Id
                                                              select dis).ToList();
                        foreach (Eva_Distribution item in distri_list)
                        {
                            item.IsDelete = (int)IsDelete.Delete;
                            JsonModel m2 = Constant.Eva_DistributionService.Update(item);
                            if (m2.errNum == intSuccess)
                            {
                                Constant.Eva_Distribution_List.Remove(item);

                                TableUsingAdd(item.Table_Id);
                            }
                        }
                        //添加分配
                        Reg_Table_Add_Helper(EditUID, EditUID, Eva_Regular_edit, tables, eva_roles, course_types);
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "修改失败");
                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "该评价不存在");
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
        /// 获取评价名称
        /// </summary>
        /// <param name="context"></param>
        public void Get_Eva_Regular_Name(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;

                var _list = (from t in Constant.Eva_Regular_List
                             where t.StartTime < DateTime.Now
                             select new { Name = t.Name }).Distinct();
                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", _list);
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

        //上锁int score
        static object obj_Add_Eva_QuestionAnswer = new object();
        /// <summary>
        /// 新增学生答题
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Add_Eva_QuestionAnswer(HttpContext context)
        {
            lock (obj_Add_Eva_QuestionAnswer)
            {
                HttpRequest Request = context.Request;
                //分配表的ID
                int Eva_Distribution_Id = RequestHelper.int_transfer(Request, "Eva_Distribution_Id");
                //学生【用户唯一标识符】
                string StudentUID = RequestHelper.string_transfer(Request, "StudentUID");
                //课程的ID
                string CourseId = RequestHelper.string_transfer(Request, "CourseId");
                //教师的ID
                string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
                //学年学期的ID
                int SectionId = RequestHelper.int_transfer(Request, "SectionId");
                //得分
                decimal Score = RequestHelper.decimal_transfer(Request, "Score");
                //创建者
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                int intSuccess = (int)errNum.Success;
                //课程分类
                string CourseTypeId = Constant.CourseRel_List.FirstOrDefault(i => i.Course_Id == CourseId).CourseType_Id;
                int Type = RequestHelper.int_transfer(Request, "Type");
                try
                {

                    //学生回答任务信息表
                    Eva_QuestionAnswer Eva_QuestionAnswer = new Eva_QuestionAnswer()
                    {
                        Eva_Distribution_Id = Eva_Distribution_Id,
                        StudentUID = StudentUID,
                        CourseTypeId = CourseTypeId,
                        CourseId = CourseId,
                        TeacherUID = TeacherUID,
                        SectionId = SectionId,
                        Score = Score,
                        CreateUID = CreateUID,
                        CreateTime = DateTime.Now,
                        EditTime = DateTime.Now,
                        EditUID = CreateUID,
                        IsDelete = (int)IsDelete.No_Delete,
                        IsEnable = (int)IsEnable.Enable
                    };

                    //表单明细
                    string List = RequestHelper.string_transfer(Request, "List");
                    //序列化表单详情列表
                    List<Eva_QuestionAnswer_Detail> Eva_QuestionAnswer_Detail_List = JsonConvert.DeserializeObject<List<Eva_QuestionAnswer_Detail>>(List);

                    jsonModel = Constant.Eva_QuestionAnswerService.Add(Eva_QuestionAnswer);

                    Eva_QuestionAnswer.Id = Convert.ToInt32(jsonModel.retData);
                    if (jsonModel.errNum == intSuccess && !Constant.Eva_QuestionAnswer_List.Contains(Eva_QuestionAnswer))
                    {
                        Constant.Eva_QuestionAnswer_List.Add(Eva_QuestionAnswer);

                        //答题任务详情填充
                        foreach (Eva_QuestionAnswer_Detail item in Eva_QuestionAnswer_Detail_List)
                        {
                            item.Eva_TaskAnswer_Id = Eva_QuestionAnswer.Id;
                            item.CreateTime = DateTime.Now;
                            item.CreateUID = StudentUID;
                            item.EditUID = StudentUID;
                            item.EditTime = DateTime.Now;
                            item.IsDelete = (int)IsDelete.No_Delete;
                            item.IsEnable = (int)IsEnable.Enable;
                            //数据库插入
                            JsonModel jsm = Constant.Eva_QuestionAnswer_DetailService.Add(item);
                            //插入成功入缓存
                            if (jsm.errNum == intSuccess)
                            {
                                item.Id = Convert.ToInt32(jsm.retData);
                                if (!Constant.Eva_QuestionAnswer_Detail_List.Contains(item))
                                {
                                    Constant.Eva_QuestionAnswer_Detail_List.Add(item);
                                }
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
        }

        #endregion

    }
}