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
        #region 学年学期管理

        /// <summary>
        /// 获取学年学期
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Get_StudySection(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                //返回学年学期
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", Constant.StudySection_List);
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
        static object obj_Add_StudySection = new object();
        /// <summary>
        /// 新增学年学期
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Add_StudySection(HttpContext context)
        {
            lock (obj_Add_StudySection)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                string Academic = RequestHelper.string_transfer(Request, "Academic");
                string Semester = RequestHelper.string_transfer(Request, "Semester");
                DateTime StartTime = RequestHelper.DateTime_transfer(Request, "StartTime");
                DateTime EndTime = RequestHelper.DateTime_transfer(Request, "EndTime");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                string StudyDisPlayName = RequestHelper.string_transfer(Request, "StudyDisPlayName");
                try
                {
                    StudySection StudySectione_Add = new StudySection()
                    {
                        Academic = Academic,
                        Semester = Semester,
                        StartTime = StartTime,
                        EndTime = EndTime,

                        CreateTime = DateTime.Now,
                        CreateUID = CreateUID,
                        EditTime = DateTime.Now,
                        EditUID = EditUID,
                        IsEnable = (int)IsEnable.Enable,
                        DisPlayName = StudyDisPlayName,
                        IsDelete = (int)IsDelete.No_Delete
                    };
                    //数据库添加
                    jsonModel = new FEBLL.StudySectionService().Add(StudySectione_Add);
                    //从数据库返回的ID绑定
                    StudySectione_Add.Id = RequestHelper.int_transfer(Convert.ToString(jsonModel.retData));
                    if (jsonModel.errNum == intSuccess && !Constant.StudySection_List.Contains(StudySectione_Add))
                    {
                        //缓存添加
                        Constant.StudySection_List.Add(StudySectione_Add);
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

        #region 获取课程类型

        /// <summary>
        ///获取课程类型
        /// </summary>
        public void GetCourse_Type(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //课程类型
            DictionType_Enum dictiontype = DictionType_Enum.Course_Type;
            int SectionId = RequestHelper.int_transfer(Request, "SectionId");
            try
            {
                bool HasSection = RequestHelper.bool_transfer(Request, "HasSection");

                //带学年学期的课程
                if (HasSection)
                {
                    if (SectionId > 0)
                    {
                        var list = from dic in Constant.Sys_Dictionary_List
                                   join section in Constant.StudySection_List on dic.SectionId equals section.Id
                                   where dic.Type == Convert.ToString((int)dictiontype) && dic.SectionId == section.Id && section.Id == SectionId
                                   orderby section.EndTime descending
                                   select new { dic.SectionId, dic.Value, section.DisPlayName, dic.Id, dic.Key, Study_IsEnable = section.IsEnable, dic.IsEnable };
                        //返回所有表格数据
                        jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                    }
                    else
                    {
                        var list = from dic in Constant.Sys_Dictionary_List
                                   join section in Constant.StudySection_List on dic.SectionId equals section.Id
                                   where dic.Type == Convert.ToString((int)dictiontype) && dic.SectionId == section.Id
                                   orderby section.EndTime descending
                                   select new { dic.SectionId, dic.Value, section.DisPlayName, dic.Id, dic.Key, Study_IsEnable = section.IsEnable, dic.IsEnable };
                        //返回所有表格数据
                        jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                    }

                }
                else
                {
                    StudySection section = Constant.StudySection_List.FirstOrDefault(item => item.IsEnable == 1);
                    if (section != null)
                    {
                        var list = from dic in Constant.Sys_Dictionary_List
                                   join sect in Constant.StudySection_List on dic.SectionId equals sect.Id
                                   where dic.Type == Convert.ToString((int)dictiontype)
                                   orderby section.EndTime descending
                                   select new { dic.SectionId, dic.Sort, dic.Type, dic.Value, dic.CreateTime, dic.Id, dic.Key, dic.Pid, sect.IsEnable };
                        //返回所有表格数据
                        jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                    }
                    else
                    {
                        //返回所有表格数据
                        jsonModel = JsonModel.get_jsonmodel((int)errNum.Failed, "failed", 0);
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

        /// <summary>
        ///获取设计表【通过课程类型】
        /// </summary>
        public void GetTable_ByCourseType(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                //课程类型
                //DictionType_Enum dictiontype = DictionType_Enum.Course_Type;
                int Eva_Role = RequestHelper.int_transfer(Request, "Eva_Role");
                List<Table_CourseType> Table_CourseType_List = new List<Table_CourseType>();


                List<Sys_Dictionary> list = (from dic in Constant.Sys_Dictionary_List
                                             where dic.Type == Convert.ToString((int)Dictionary_Type.Common_Course_Type)
                                             select dic).ToList();
                foreach (Sys_Dictionary item in list)
                {
                    Table_CourseType Table_CourseType = new Table_CourseType();
                    Table_CourseType.Course_Key = item.Key;
                    Table_CourseType.Course_Value = item.Value;
                    Table_CourseType.Eva_Role = Eva_Role;
                    Table_CourseType.Eva_Table_List = (from table in Constant.Eva_Table_List
                                                       where table.Eva_Role == Eva_Role && table.IsDelete == (int)IsDelete.No_Delete
                                                       //where Convert.ToString(table.CourseType_Id) == item.Key || table.CourseType_Id == 4//不用一一对应  显示四张表即可（2017.3.30新改动，总监让改的）
                                                       select table).ToList();
                    Table_CourseType_List.Add(Table_CourseType);
                }


                if (Eva_Role == 2)
                {
                    List<Sys_Dictionary> list2 = (from dic in Constant.Sys_Dictionary_List
                                                  where dic.Type == Convert.ToString((int)Dictionary_Type.Edu_Course_Type)
                                                  select dic).ToList();
                    foreach (Sys_Dictionary item in list2)
                    {
                        Table_CourseType Table_CourseType = new Table_CourseType();
                        Table_CourseType.Course_Key = item.Key;
                        Table_CourseType.Course_Value = item.Value;
                        Table_CourseType.Eva_Role = 3;
                        Table_CourseType.Eva_Table_List = (from table in Constant.Eva_Table_List
                                                           where table.Eva_Role == Eva_Role
                                                           select table).ToList();
                        Table_CourseType_List.Add(Table_CourseType);
                    }

                    List<Sys_Dictionary> list3 = (from dic in Constant.Sys_Dictionary_List
                                                  where dic.Type == Convert.ToString((int)Dictionary_Type.Leader_Course_Type)
                                                  select dic).ToList();
                    foreach (Sys_Dictionary item in list3)
                    {
                        Table_CourseType Table_CourseType = new Table_CourseType();
                        Table_CourseType.Course_Key = item.Key;
                        Table_CourseType.Course_Value = item.Value;
                        Table_CourseType.Eva_Role = 4;
                        Table_CourseType.Eva_Table_List = (from table in Constant.Eva_Table_List
                                                           where table.Eva_Role == Eva_Role
                                                           select table).ToList();
                        Table_CourseType_List.Add(Table_CourseType);
                    }
                }


                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", Table_CourseType_List);
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

        #region 指标库管理



        /// <summary>
        /// 获取指标库分类
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Get_IndicatorType(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                int P_Type = RequestHelper.int_transfer(Request, "P_Type");
                int P_Id = RequestHelper.int_transfer(Request, "P_Id");
                List<IndicatorType> list = null;
                if (P_Type > 0)
                {
                    list = (from i in Constant.IndicatorType_List where i.P_Type == P_Type select i).ToList();
                }
                else
                {
                    list = Constant.IndicatorType_List;
                }
                //根据父ID获取指标库
                if (P_Id > 0)
                {
                    list = (from i in Constant.IndicatorType_List where i.Parent_Id == P_Id select i).ToList();
                }

                //返回所有指标库数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
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
        static object obj_Edit_IndicatorType = new object();
        /// <summary>
        /// 修改指标分类
        /// </summary>
        public void Edit_IndicatorType(HttpContext context)
        {
            lock (obj_Edit_IndicatorType)
            {
                int intSuccess = (int)errNum.Success;
                try
                {
                    HttpRequest Request = context.Request;
                    //指标的ID
                    int Id = RequestHelper.int_transfer(Request, "Id");

                    //获取指定要删除的指标
                    IndicatorType IndicatorType_edit = Constant.IndicatorType_List.FirstOrDefault(i => i.Id == Id);

                    if (IndicatorType_edit != null)
                    {
                        string Name = RequestHelper.string_transfer(Request, "Name");
                        string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                        var count = Constant.Indicator_List.Count(i => i.IndicatorType_Id == Id && i.UseTimes > 0);
                        if (count == 0)
                        {
                            //克隆该指标
                            IndicatorType indic = Constant.Clone<IndicatorType>(IndicatorType_edit);
                            indic.Name = Name;
                            indic.EditUID = EditUID;

                            //数据库操作成功再改缓存
                            jsonModel = Constant.IndicatorTypeService.Update(indic);
                            if (jsonModel.errNum == intSuccess)
                            {
                                IndicatorType_edit.Name = Name;
                                IndicatorType_edit.EditUID = EditUID;
                            }
                            else
                            {
                                jsonModel = JsonModel.get_jsonmodel(3, "failed", "修改失败");
                            }
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "已组卷不可编辑");
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "该指标分类不存在");
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
        /// 删除指标分类
        /// </summary>
        public void Delete_Indicator_Type(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //指标的ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                //获取指定要删除的指标分类
                IndicatorType IndicatorType_delete = Constant.IndicatorType_List.FirstOrDefault(i => i.Id == Id);
                if (IndicatorType_delete != null)
                {

                    var count = Constant.Indicator_List.Count(i => i.IndicatorType_Id == Id);
                    var count2 = Constant.IndicatorType_List.Count(i => i.Parent_Id == Id);
                    if (count == 0 && count2 == 0)
                    {
                        //克隆该指标
                        IndicatorType indic = Constant.Clone<IndicatorType>(IndicatorType_delete);
                        indic.IsDelete = (int)IsDelete.Delete;

                        //数据库操作成功再改缓存
                        jsonModel = Constant.IndicatorTypeService.Update(indic);
                        if (jsonModel.errNum == intSuccess)
                        {
                            IndicatorType_delete.IsDelete = (int)IsDelete.Delete;
                            Constant.IndicatorType_List.Remove(IndicatorType_delete);
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "删除失败");
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "有关联的指标项无法删除");
                    }

                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "该指标分类不存在");
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
        static object obj_Add_IndicatorType = new object();
        /// <summary>
        /// 新增指标库分类
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Add_IndicatorType(HttpContext context)
        {
            lock (obj_Add_IndicatorType)
            {
                int intSuccess = (int)errNum.Success;
                try
                {
                    HttpRequest Request = context.Request;

                    int Parent_Id = RequestHelper.int_transfer(Request, "Parent_Id");
                    string Name = RequestHelper.string_transfer(Request, "Name");
                    string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                    //string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                    IndicatorType IndicatorType_Add = new IndicatorType()
                    {
                        Type = 0,
                        Parent_Id = Parent_Id,
                        Name = Name,
                        CreateTime = DateTime.Now,
                        CreateUID = CreateUID,
                        EditTime = DateTime.Now,
                        //EditUID = EditUID,
                        IsEnable = (int)IsEnable.Enable,
                        IsDelete = (int)IsDelete.No_Delete,

                        EditUID = "",
                        P_Type = 0,
                    };
                    //数据库添加
                    jsonModel = new IndicatorTypeService().Add(IndicatorType_Add);

                    //从数据库返回的ID绑定
                    IndicatorType_Add.Id = RequestHelper.int_transfer(Convert.ToString(jsonModel.retData));
                    if (jsonModel.errNum == intSuccess && !Constant.IndicatorType_List.Contains(IndicatorType_Add))
                    {
                        //缓存添加
                        Constant.IndicatorType_List.Add(IndicatorType_Add);
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
        /// 获取指标库【指定类型】
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Get_Indicator(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //指定的指标库分类ID
            int IndicatorType_Id = RequestHelper.int_transfer(Request, "IndicatorType_Id");
            string Type = RequestHelper.string_transfer(Request, "Type");
            int intType = RequestHelper.int_transfer(Request, "Type");
            try
            {
                if (IndicatorType_Id > 0)
                {
                    //获取某指标库类型下的指标
                    List<Indicator> indicator_List = null;
                    if (!string.IsNullOrEmpty(Type))
                    {
                        indicator_List = (from indicator in Constant.Indicator_List where indicator.IndicatorType_Id == IndicatorType_Id && indicator.Type == intType select indicator).ToList();
                    }
                    else
                    {
                        indicator_List = (from indicator in Constant.Indicator_List where indicator.IndicatorType_Id == IndicatorType_Id select indicator).ToList();
                    }

                    //返回所有指标库数据
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", indicator_List);
                }
                else
                {
                    //返回所有指标库数据
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", Constant.Indicator_List);
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
        static object obj_Add_Indicator = new object();
        /// <summary>
        /// 新增指标库
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Add_Indicator(HttpContext context)
        {
            lock (obj_Add_Indicator)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                //指标名称
                string Name = RequestHelper.string_transfer(Request, "Name");
                //指标分类编号
                int IndicatorType_Id = RequestHelper.int_transfer(Request, "IndicatorType_Id");
                int QuesType_Id = RequestHelper.int_transfer(Request, "QuesType_Id");
                string OptionA = RequestHelper.string_transfer(Request, "OptionA");
                string OptionB = RequestHelper.string_transfer(Request, "OptionB");
                string OptionC = RequestHelper.string_transfer(Request, "OptionC");
                string OptionD = RequestHelper.string_transfer(Request, "OptionD");
                string OptionE = RequestHelper.string_transfer(Request, "OptionE");
                string OptionF = RequestHelper.string_transfer(Request, "OptionF");
                int UseTimes = RequestHelper.int_transfer(Request, "UseTimes");
                string Remarks = RequestHelper.string_transfer(Request, "Remarks");

                int Type = RequestHelper.int_transfer(Request, "Type");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                try
                {
                    Indicator Indicator_Add = new Indicator()
                    {
                        Name = Name,
                        IndicatorType_Id = IndicatorType_Id,
                        QuesType_Id = QuesType_Id,
                        OptionA = OptionA,
                        OptionB = OptionB,
                        OptionC = OptionC,
                        OptionD = OptionD,
                        OptionE = OptionE,
                        OptionF = OptionF,
                        UseTimes = UseTimes,
                        Remarks = Remarks,
                        Type = Type,
                        CreateUID = CreateUID,
                        CreateTime = DateTime.Now,
                        EditUID = EditUID,
                        EditTime = DateTime.Now,
                        IsEnable = (int)IsEnable.Enable,
                        IsDelete = (int)IsDelete.No_Delete,
                       
                    };
                    //数据库添加
                    jsonModel = new IndicatorService().Add(Indicator_Add);
                    //从数据库返回的ID绑定
                    Indicator_Add.Id = RequestHelper.int_transfer(Convert.ToString(jsonModel.retData));
                    if (jsonModel.errNum == intSuccess && !Constant.Indicator_List.Contains(Indicator_Add))
                    {
                        //缓存添加
                        Constant.Indicator_List.Add(Indicator_Add);
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
        static object obj_Edit_Indicator = new object();
        /// <summary>
        /// 修改指标
        /// </summary>
        public void Edit_Indicator(HttpContext context)
        {
            lock (obj_Edit_Indicator)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                //指标的ID
                int Id = RequestHelper.int_transfer(Request, "Id");
                int QuesType_Id = RequestHelper.int_transfer(Request, "QuesType_Id");
                int IndicatorType_Id = RequestHelper.int_transfer(Request, "IndicatorType_Id");
                string OptionA = RequestHelper.string_transfer(Request, "OptionA");
                string OptionB = RequestHelper.string_transfer(Request, "OptionB");
                string OptionC = RequestHelper.string_transfer(Request, "OptionC");
                string OptionD = RequestHelper.string_transfer(Request, "OptionD");
                string OptionE = RequestHelper.string_transfer(Request, "OptionE");
                string OptionF = RequestHelper.string_transfer(Request, "OptionF");
                int UseTimes = RequestHelper.int_transfer(Request, "UseTimes");
                string Remarks = RequestHelper.string_transfer(Request, "Remarks");
                int Type = RequestHelper.int_transfer(Request, "Type");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                try
                {
                    //获取指定要删除的指标
                    Indicator Indicator_edit = Constant.Indicator_List.FirstOrDefault(i => i.Id == Id);
                    if (Indicator_edit != null)
                    {
                        //指标名称
                        string Name = RequestHelper.string_transfer(Request, "Name");
                        //克隆该指标
                        Indicator indic = Constant.Clone<Indicator>(Indicator_edit);
                        indic.Name = Name;
                        indic.QuesType_Id = QuesType_Id;
                        indic.IndicatorType_Id = IndicatorType_Id;
                        indic.OptionA = OptionA;
                        indic.OptionB = OptionB;
                        indic.OptionC = OptionC;
                        indic.OptionD = OptionD;
                        indic.OptionE = OptionE;
                        indic.OptionF = OptionF;

                        indic.Remarks = Remarks;
                        indic.UseTimes = UseTimes;
                        indic.Type = Type;
                        indic.EditUID = EditUID;
                        //数据库操作成功再改缓存
                        jsonModel = Constant.IndicatorService.Update(indic);
                        if (jsonModel.errNum == intSuccess)
                        {
                            Indicator_edit.Name = Name;

                            Indicator_edit.QuesType_Id = QuesType_Id;
                            Indicator_edit.IndicatorType_Id = IndicatorType_Id;
                            Indicator_edit.OptionA = OptionA;
                            Indicator_edit.OptionB = OptionB;
                            Indicator_edit.OptionC = OptionC;
                            Indicator_edit.OptionD = OptionD;
                            Indicator_edit.OptionE = OptionE;
                            Indicator_edit.OptionF = OptionF;
                            Indicator_edit.Remarks = Remarks;
                            Indicator_edit.UseTimes = UseTimes;
                            Indicator_edit.Type = Type;
                            Indicator_edit.EditUID = EditUID;
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "修改失败");
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "该指标不存在");
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
        /// 删除指标
        /// </summary>
        public void Delete_Indicator(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //指标的ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                //获取指定要删除的指标
                Indicator Indicator_delete = Constant.Indicator_List.FirstOrDefault(i => i.Id == Id);
                if (Indicator_delete != null)
                {
                    //克隆该指标
                    Indicator indic = Constant.Clone<Indicator>(Indicator_delete);
                    indic.IsDelete = (int)IsDelete.Delete;
                    //数据库操作成功再改缓存
                    jsonModel = Constant.IndicatorService.Update(indic);
                    if (jsonModel.errNum == intSuccess)
                    {
                        Indicator_delete.IsDelete = (int)IsDelete.Delete;
                        Constant.Indicator_List.Remove(Indicator_delete);
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "删除失败");
                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "该指标不存在");
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

        #region 表格设计管理

        /// <summary>
        /// 获取表格
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Get_Eva_Table(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            int Eva_Role = RequestHelper.int_transfer(Request, "Eva_Role");
            try
            {
                if (Eva_Role > 0)
                {
                    var list = Table_Submiter_EvaRole(Eva_Role);
                    //返回所有表格数据
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
                }
                else
                {
                    var list = Table_Submiter();
                    //返回所有表格数据
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
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
        /// 获取表格头部规则
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Get_Eva_Table_Header_Custom_List(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            try
            {
                var list = (from c in Constant.Eva_Table_Header_Custom_List select new { Code = c.Code, id = c.Code, name = c.Header, description = c.Header_Value }).ToList();
                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", list);
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
        /// 获取设计表详情【学生答题的初始化答卷】
        /// </summary>
        /// <param name="context"></param>
        public void Get_Eva_TableDetail(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //表格ID
            int table_Id = RequestHelper.int_transfer(Request, "table_Id");
            //没有教学反馈
            bool no_eduType = RequestHelper.bool_transfer(Request, "no_eduType");
            Table_View table_view = new Table_View();
            try
            {
                //首先指定试卷
                var table = Constant.Eva_Table_List.FirstOrDefault(i => i.Id == table_Id);
                if (table != null)
                {
                    table_view.Table_Id = (int)table.Id;
                    table_view.IsEnable = (int)table.IsEnable;
                    table_view.Name = table.Name;
                    table_view.IsScore = (int)table.IsScore;
                    //搜集试卷题
                    List<Eva_TableDetail> details = (from TableDetail in Constant.Eva_TableDetail_List
                                                     where TableDetail.Eva_table_Id == table_Id
                                                     orderby TableDetail.Id
                                                     select TableDetail).ToList();
                    //表详情
                    table_view.Table_Detail_Dic_List = (from ps in details
                                                        group ps by ps.Root
                                                            into g
                                                            select new Table_Detail_Dic { Root = g.Key, Eva_TableDetail_List = g.ToList() }).ToList();

                    //表头
                    table_view.Table_Header_List = (from header in Constant.Eva_Table_Header_List
                                                    where header.Table_Id == table.Id
                                                    select new
                                                        Table_Header { Header = header.Name_Key, CustomCode = header.Custom_Code, Value = header.Name_Value, Type = (int)header.Type, Id = (int)header.Id }).ToList();
                }

                //返回所有表格数据
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", table_view);
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
        static object obj_Add_Eva_Table = new object();
        /// <summary>
        /// 新增表格
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Add_Eva_Table(HttpContext context)
        {
            lock (obj_Add_Eva_Table)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                string Name = RequestHelper.string_transfer(Request, "Name");
                int IsScore = RequestHelper.int_transfer(Request, "IsScore");
                int IsEnable = RequestHelper.int_transfer(Request, "IsEnable");
                string Remarks = RequestHelper.string_transfer(Request, "Remarks");
                string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
                string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                string CourseType_Id = RequestHelper.string_transfer(Request, "CourseType_Id");
                int Eva_Role = RequestHelper.int_transfer(Request, "Eva_Role");
                try
                {
                    Eva_Table Eva_Table_Add = new Eva_Table()
                    {
                        Name = Name,
                        IsScore = (byte)IsScore,
                        CousrseType_Id = CourseType_Id,
                        Eva_Role = Eva_Role,
                        //Type = 2,//新加的  主要区别于即时(1)和扫码(0)
                        Remarks = Remarks,
                        UseTimes = 0,
                        CreateTime = DateTime.Now,
                        CreateUID = CreateUID,
                        EditTime = DateTime.Now,
                        EditUID = EditUID,
                        IsEnable = (byte)IsEnable,
                        IsDelete = (int)IsDelete.No_Delete
                    };

                    //表单明细
                    string List = RequestHelper.string_transfer(Request, "List");
                    //序列化表单详情列表
                    List<Table_A> Table_A_List = JsonConvert.DeserializeObject<List<Table_A>>(List);

                    //数据库添加
                    jsonModel = Constant.Eva_TableService.Add(Eva_Table_Add);
                    if (jsonModel.errNum == 0)
                    {
                        //从数据库返回的ID绑定
                        Eva_Table_Add.Id = RequestHelper.int_transfer(Convert.ToString(jsonModel.retData));
                        string head_value = RequestHelper.string_transfer(Request, "head_value");
                        string lisss = RequestHelper.string_transfer(Request, "lisss");
                        //添加头部内容
                        Table_Header((int)Eva_Table_Add.Id, head_value, lisss);
                        if (jsonModel.errNum == intSuccess)
                        {
                            //缓存添加
                            Constant.Eva_Table_List.Add(Eva_Table_Add);
                            //解析正常才可进行操作
                            if (Table_A_List != null)
                            {
                                //表格详情表创建添加
                                Eva_TableAdd_Helper(CreateUID, EditUID, Eva_Table_Add, Table_A_List);
                            }
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "添加失败");
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

        static object obj_Copy_Eva_Table = new object();
        /// <summary>
        /// 复制表格
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void Copy_Eva_Table(HttpContext context)
        {
            lock (obj_Copy_Eva_Table)
            {
                HttpRequest Request = context.Request;
                int table_Id = RequestHelper.int_transfer(Request, "table_Id");
                try
                {
                    Eva_Table Eva_Table_Need_Copy = Constant.Eva_Table_List.FirstOrDefault(t => t.Id == table_Id);
                    if (Eva_Table_Need_Copy != null)
                    {
                        Eva_Table Eva_Table_Copy_Clone = Constant.Clone<Eva_Table>(Eva_Table_Need_Copy);
                        Eva_Table_Copy_Clone.Id = null;
                        Eva_Table_Copy_Clone.UseTimes = 0;
                        jsonModel = Constant.Eva_TableService.Add(Eva_Table_Copy_Clone);
                        if (jsonModel.errNum == 0)
                        {
                            Eva_Table_Copy_Clone.Id = Convert.ToInt32(jsonModel.retData);

                            //添加克隆出来的表
                            Constant.Eva_Table_List.Add(Eva_Table_Copy_Clone);

                            //表头复制
                            var table_headers = (from t_h in Constant.Eva_Table_Header_List where t_h.Table_Id == table_Id select t_h).ToList();
                            foreach (var header in table_headers)
                            {
                                Eva_Table_Header h_Clone = Constant.Clone<Eva_Table_Header>(header);
                                h_Clone.Table_Id = Eva_Table_Copy_Clone.Id;
                                h_Clone.Id = null;
                                var jsonmodel = Constant.Eva_Table_HeaderService.Add(h_Clone);
                                if (jsonmodel.errNum == 0)
                                {
                                    h_Clone.Id = Convert.ToInt32(jsonmodel.retData);
                                    Constant.Eva_Table_Header_List.Add(h_Clone);
                                }
                            }

                            //详情题干添加
                            var table_details = (from detail in Constant.Eva_TableDetail_List where detail.Eva_table_Id == table_Id select detail).ToList();
                            foreach (var detail in table_details)
                            {
                                Eva_TableDetail detail_clone = Constant.Clone<Eva_TableDetail>(detail);
                                detail_clone.Eva_table_Id = Eva_Table_Copy_Clone.Id;
                                detail_clone.Id = null;
                                var jsonmodel = Constant.Eva_TableDetailService.Add(detail_clone);
                                if (jsonmodel.errNum == 0)
                                {
                                    detail_clone.Id = Convert.ToInt32(jsonmodel.retData);
                                    Constant.Eva_TableDetail_List.Add(detail_clone);
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

        /// <summary>
        /// 移除头部内容
        /// </summary>
        /// <param name="table_Id"></param>
        /// <param name="Request"></param>
        public static void Remove_Header(int table_Id)
        {
            try
            {
                List<Eva_Table_Header> headers = (from h in Constant.Eva_Table_Header_List where h.Table_Id == table_Id select h).ToList();
                foreach (var item in headers)
                {
                    var model = Constant.Eva_Table_HeaderService.Delete((int)item.Id);
                    if (model.errNum == 0)
                    {
                        Constant.Eva_Table_Header_List.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 添加头部内容
        /// </summary>
        /// <param name="table_Id"></param>
        /// <param name="Request"></param>
        public static void Table_Header(int table_Id, string head_value, string lisss)
        {
            try
            {

                if (!string.IsNullOrEmpty(head_value))
                {
                    //序列化表表头固化
                    List<head_value> head_values = JsonConvert.DeserializeObject<List<head_value>>(head_value);
                    foreach (var item in head_values)
                    {
                        Eva_Table_Header header = new Eva_Table_Header() { Custom_Code = item.Code, Name_Key = item.description, Name_Value = item.name, Table_Id = table_Id, Type = Convert.ToInt32(item.id) };
                        var josnmodel = Constant.Eva_Table_HeaderService.Add(header);
                        if (josnmodel.errNum == 0)
                        {
                            header.Id = Convert.ToInt32(josnmodel.retData);
                            Constant.Eva_Table_Header_List.Add(header);
                        }
                    }

                }


                if (!string.IsNullOrEmpty(lisss))
                {
                    //序列化表表头固化
                    List<lisss> lisss_s = JsonConvert.DeserializeObject<List<lisss>>(lisss);

                    foreach (var item in lisss_s)
                    {
                        Eva_Table_Header header = new Eva_Table_Header() { Name_Key = item.title, Name_Value = item.name, Table_Id = table_Id, Type = 0 };
                        var josnmodel = Constant.Eva_Table_HeaderService.Add(header);
                        if (josnmodel.errNum == 0)
                        {
                            header.Id = Convert.ToInt32(josnmodel.retData);
                            Constant.Eva_Table_Header_List.Add(header);
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
        /// 表格详情表创建添加
        /// </summary>
        /// <param name="CreateUID"></param>
        /// <param name="EditUID"></param>
        /// <param name="Eva_Table_Add"></param>
        /// <param name="Table_A_List"></param>
        private static void Eva_TableAdd_Helper(string CreateUID, string EditUID, Eva_Table Eva_Table_Add, List<Table_A> Table_A_List)
        {
            var Table_Id = Eva_Table_Add.Id;
            //开启线程操作数据库
            new Thread(() =>
            {

                try
                {
                    foreach (Table_A table_A in Table_A_List)
                    {
                        //表明细列表保证不为null513
                        if (table_A.indicator_list == null) continue;
                        foreach (indicator_list item in table_A.indicator_list)
                        {
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
                                Root = item.Root,
                                Eva_table_Id = Table_Id,
                                //评价任务 1，表格设计 0
                                Type = Convert.ToString((int)TableDetail_Type.Table),

                            };

                            //入库
                            JsonModel model2 = Constant.Eva_TableDetailService.Add(Eva_TableDetail);
                            if (model2.errNum == 0)
                            {
                                //添加到缓存
                                Constant.Eva_TableDetail_List.Add(Eva_TableDetail);
                                Eva_TableDetail.Id = RequestHelper.int_transfer(Convert.ToString(model2.retData));

                                Indicate_Using_Add((int)item.Id);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }) { IsBackground = true }.Start();
        }

        /// <summary>
        /// 指标库引用增1
        /// </summary>
        /// <param name="Id"></param>
        private static void Indicate_Using_Add(int Id)
        {
            try
            {
                //指标库的计数
                Indicator Indicator = Constant.Indicator_List.FirstOrDefault(t => t.Id == Id);
                Indicator Indicator_clone = Constant.Clone<Indicator>(Indicator);
                Indicator_clone.UseTimes += 1;
                //直接进行更改
                if (Indicator_clone != null)
                {
                    JsonModel m = Constant.IndicatorService.Update(Indicator);
                    if (m.errNum == 0)
                    {
                        Indicator.UseTimes += 1;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        /// <summary>
        /// 指标库引用减1
        /// </summary>
        /// <param name="Id"></param>
        private static void Indicate_Using_Reduce(int Id)
        {
            try
            {
                //指标库的计数
                Indicator Indicator = Constant.Indicator_List.FirstOrDefault(t => t.Id == Id);
                Indicator Indicator_clone = Constant.Clone<Indicator>(Indicator);
                Indicator_clone.UseTimes -= 1;
                //直接进行更改
                if (Indicator_clone != null)
                {
                    JsonModel m = Constant.IndicatorService.Update(Indicator);
                    if (m.errNum == 0)
                    {
                        Indicator.UseTimes -= 1;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 删除表格
        /// </summary>
        public void Delete_Eva_Table(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            HttpRequest Request = context.Request;
            //指标的ID
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {

                //获取指定要删除的表格
                Eva_Table Eva_Table_delete = Constant.Eva_Table_List.FirstOrDefault(i => i.Id == Id);
                if (Eva_Table_delete != null)
                {
                    //克隆该表格
                    Eva_Table indic = Constant.Clone<Eva_Table>(Eva_Table_delete);
                    indic.IsDelete = (int)IsDelete.Delete;
                    //数据库操作成功再改缓存
                    jsonModel = Constant.Eva_TableService.Update(indic);
                    if (jsonModel.errNum == intSuccess)
                    {
                        Eva_Table_delete.IsDelete = (int)IsDelete.Delete;

                        //开启线程操作数据库
                        new Thread(() =>
                        {
                            List<Eva_Table_Header> Eva_Table_Header_List = Constant.Eva_Table_Header_List.Where(t => t.Table_Id == Id).ToList();
                            foreach (Eva_Table_Header item in Eva_Table_Header_List)
                            {
                                item.IsDelete = (int)IsDelete.Delete;
                                var jm = Constant.Eva_Table_HeaderService.Update(item);
                                if (jm.errNum == 0)
                                {
                                    Constant.Eva_Table_Header_List.Remove(item);
                                }
                            }

                            List<Eva_TableDetail> Eva_TableDetail_List = Constant.Eva_TableDetail_List.Where(t => t.Eva_table_Id == Id).ToList();
                            foreach (Eva_TableDetail item in Eva_TableDetail_List)
                            {
                                item.IsDelete = (int)IsDelete.Delete;
                                var jsonmodel = Constant.Eva_TableDetailService.Update(item);
                                if (jsonmodel.errNum == 0)
                                {
                                    Constant.Eva_TableDetail_List.Remove(item);
                                    Indicate_Using_Reduce((int)item.Indicator_Id);
                                }
                            }
                        }) { IsBackground = true }.Start();
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "删除失败");
                    }
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "该表格不存在");
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
        static object obj_Edit_Eva_Table = new object();
        /// <summary>
        /// 编辑表格
        /// </summary>
        public void Edit_Eva_Table(HttpContext context)
        {
            lock (obj_Edit_Eva_Table)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                //表格Id
                int table_Id = RequestHelper.int_transfer(Request, "table_Id");
                try
                {
                    //获取指定要删除的表格
                    Eva_Table Eva_Table_edit = Constant.Eva_Table_List.FirstOrDefault(i => i.Id == table_Id);
                    if (Eva_Table_edit != null)
                    {
                        string Name = RequestHelper.string_transfer(Request, "Name");

                        int IsScore = RequestHelper.int_transfer(Request, "IsScore");
                        string Remarks = RequestHelper.string_transfer(Request, "Remarks");
                        string EditUID = RequestHelper.string_transfer(Request, "EditUID");
                        string CourseType_Id = RequestHelper.string_transfer(Request, "CourseType_Id");

                        int IsEnable = RequestHelper.int_transfer(Request, "IsEnable");
                        //克隆该表格
                        Eva_Table Eva_Table_clone = Constant.Clone<Eva_Table>(Eva_Table_edit);
                        Eva_Table_clone.Name = Name;
                        Eva_Table_clone.IsScore = (byte)IsScore;
                        Eva_Table_clone.Remarks = Remarks;
                        Eva_Table_clone.EditTime = DateTime.Now;
                        Eva_Table_clone.EditUID = EditUID;
                        Eva_Table_clone.IsEnable = (byte)IsEnable;
                        //数据库操作成功再改缓存
                        jsonModel = Constant.Eva_TableService.Update(Eva_Table_clone);
                        if (jsonModel.errNum == intSuccess)
                        {
                            Eva_Table_edit.Name = Name;
                            Eva_Table_edit.IsScore = (byte)IsScore;
                            Eva_Table_edit.Remarks = Remarks;
                            Eva_Table_edit.EditTime = DateTime.Now;
                            Eva_Table_edit.EditUID = EditUID;
                            Eva_Table_edit.IsEnable = (byte)IsEnable;
                            //表单明细
                            string List = RequestHelper.string_transfer(Request, "List");

                            //序列化表单详情列表
                            List<Table_A> Table_A_List = JsonConvert.DeserializeObject<List<Table_A>>(List);
                            //先进行删除，然后进行添加
                            List<Eva_TableDetail> table_list = (from t in Constant.Eva_TableDetail_List where t.Eva_table_Id == Eva_Table_edit.Id select t).ToList();

                            //删除详情表
                            foreach (Eva_TableDetail item in table_list)
                            {
                                JsonModel m = Constant.Eva_TableDetailService.Delete((int)item.Id);
                                if (m.errNum == intSuccess)
                                {
                                    Constant.Eva_TableDetail_List.Remove(item);
                                    Indicate_Using_Reduce((int)item.Indicator_Id);
                                }
                            }
                            Remove_Header((int)Eva_Table_edit.Id);

                            //解析正常才可进行操作
                            if (Table_A_List != null)
                            {
                                //表格详情表创建添加
                                Eva_TableAdd_Helper(Eva_Table_edit.EditUID, EditUID, Eva_Table_edit, Table_A_List);

                                string head_value = RequestHelper.string_transfer(Request, "head_value");
                                string lisss = RequestHelper.string_transfer(Request, "lisss");
                                //表头信息添加
                                Table_Header((int)Eva_Table_edit.Id, head_value, lisss);
                            }
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "编辑失败");
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "该表格不存在");
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
        static object obj_Enable_Eva_Table = new object();
        /// <summary>
        /// 编辑表格
        /// </summary>
        public void Enable_Eva_Table(HttpContext context)
        {
            lock (obj_Enable_Eva_Table)
            {
                int intSuccess = (int)errNum.Success;
                HttpRequest Request = context.Request;
                //表格Id
                int table_Id = RequestHelper.int_transfer(Request, "table_Id");
                int IsEnable = RequestHelper.int_transfer(Request, "IsEnable");
                try
                {
                    //获取指定要删除的表格
                    Eva_Table Eva_Table_edit = Constant.Eva_Table_List.FirstOrDefault(i => i.Id == table_Id);
                    if (Eva_Table_edit != null)
                    {

                        //克隆该表格
                        Eva_Table Eva_Table_clone = Constant.Clone<Eva_Table>(Eva_Table_edit);
                        Eva_Table_clone.IsEnable = (byte)IsEnable;
                        //数据库操作成功再改缓存
                        jsonModel = Constant.Eva_TableService.Update(Eva_Table_clone);
                        if (jsonModel.errNum == intSuccess)
                        {
                            Eva_Table_edit.IsEnable = (byte)IsEnable;
                        }
                        else
                        {
                            jsonModel = JsonModel.get_jsonmodel(3, "failed", "编辑失败");
                        }
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "该表格不存在");
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

        #region 根据老师获取课程

        public void GetCourseByTeacherUID(HttpContext context)
        {
            int intSuccess = (int)errNum.Success;
            try
            {
                HttpRequest Request = context.Request;
                string TeacherUID = RequestHelper.string_transfer(Request, "TeacherUID");
                int distribution_Id = RequestHelper.int_transfer(Request, "Eva_Distribution_Id");
                Eva_Distribution dis = Constant.Eva_Distribution_List.FirstOrDefault(t => t.Id == distribution_Id);
                if (dis != null)
                {
                    var query = Teacher_Course_Dis(TeacherUID, dis);
                    jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query);
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

        #region 课程类型-表管理

        public void GetCourseType_Table(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string CourseTypeId = RequestHelper.string_transfer(Request, "CourseTypeId");
            int SectionId = RequestHelper.int_transfer(Request, "SectionId");

            try
            {
                var data = (from s in Constant.Sys_Dictionary_List
                            where s.Key == CourseTypeId && s.Type == "0" && s.SectionId == SectionId
                            join tb in Constant.Eva_CourseType_Table_List on s.Key equals tb.CourseTypeId
                            where tb.StudySection_Id == SectionId
                            join b in Constant.Eva_Table_List on tb.TableId equals b.Id
                            where b.IsEnable == (int)IsEnable.Enable
                            join user in Constant.UserInfo_List on b.CreateUID equals user.UniqueNo into users_
                            from u in users_.DefaultIfEmpty()
                            select new { tb.Id, b.Name, b.IsScore, b.UseTimes, b.CreateUID, b.CreateTime, b.IsEnable, UserName = u != null ? u.Name : "", TableId = b.Id, CourseTypeId = s.Key }).ToList();
                jsonModel = JsonModel.get_jsonmodel(0, "success", data);
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

        public void AddCourseType_Table(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string CourseTypeId = RequestHelper.string_transfer(Request, "CourseTypeId");
            //表格列表
            string TableList = RequestHelper.string_transfer(Request, "TableList");
            string CreateUID = RequestHelper.string_transfer(Request, "CreateUID");
            int SectionId = RequestHelper.int_transfer(Request, "SectionId");
            try
            {
                //序列化表单详情列表
                List<int> Table_A_List = JsonConvert.DeserializeObject<List<int>>(TableList);
                jsonModel = AddCourseType_TableHelper(CourseTypeId, CreateUID, Table_A_List, SectionId);
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

        public JsonModel AddCourseType_TableHelper(string CourseTypeId, string CreateUID, List<int> tableList, int SectionId)
        {
            int intSuccess = (int)errNum.Success;
            JsonModel jsmodel = new JsonModel();
            try
            {
                jsmodel = JsonModel.get_jsonmodel(intSuccess, "success", 0);

                var tablss_add = (from vir_t in tableList
                                  join tab_c in Constant.Eva_CourseType_Table_List on new
                                  {
                                      TableId = vir_t,
                                      CourseTypeId = CourseTypeId
                                  } equals new
                                  {
                                      TableId = (int)tab_c.TableId,
                                      CourseTypeId = tab_c.CourseTypeId
                                  } into tabc_cs
                                  from tac_ in tabc_cs.DefaultIfEmpty()
                                  where tac_ == null
                                  select new { tac_, vir_t }).ToList();

                var tablss_delete = (from tab_c in Constant.Eva_CourseType_Table_List
                                     where tab_c.CourseTypeId == CourseTypeId && tab_c.StudySection_Id == SectionId
                                     join t in Constant.Eva_Table_List on tab_c.TableId equals t.Id
                                     join vir_t in tableList on t.Id equals vir_t into vir_ts
                                     from vir_t_ in vir_ts.DefaultIfEmpty()
                                     where vir_t_ == 0
                                     select new { tab_c, vir_t_ }).ToList();

                foreach (var item in tablss_add)
                {
                    Eva_CourseType_Table ect = new Eva_CourseType_Table()
                    {
                        CourseTypeId = CourseTypeId,
                        CreateTime = DateTime.Now,
                        CreateUID = CreateUID,
                        IsDelete = (int)IsDelete.No_Delete,
                        TableId = item.vir_t,
                        StudySection_Id = SectionId,
                    };
                    var jsmo = Constant.Eva_CourseType_TableService.Add(ect);
                    if (jsmo.errNum == 0)
                    {
                        Constant.Eva_CourseType_Table_List.Add(ect);
                        ect.Id = Convert.ToInt32(jsmo.retData);
                        TableUsingAdd(ect.TableId);
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "内部错误");
                        break;
                    }
                }

                foreach (var item in tablss_delete)
                {
                    Eva_CourseType_Table eva_CourseType_Table_clone = Constant.Clone<Eva_CourseType_Table>(item.tab_c);
                    eva_CourseType_Table_clone.IsDelete = (int)IsDelete.Delete;
                    var jsm = Constant.Eva_CourseType_TableService.Update(eva_CourseType_Table_clone);
                    if (jsm.errNum == 0)
                    {
                        Constant.Eva_CourseType_Table_List.Remove(item.tab_c);
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "内部错误");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                jsonModel = JsonModel.get_jsonmodel(3, "failed", ex.Message);
                LogHelper.Error(ex);
            }
            return jsmodel;
        }


        public void DeleteCourseType_Table(HttpContext context)
        {
            HttpRequest Request = context.Request;
            int Id = RequestHelper.int_transfer(Request, "Id");
            try
            {
                jsonModel = DeleteCourseType_TableHelper(Id);
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

        public JsonModel DeleteCourseType_TableHelper(int Id)
        {
            int intSuccess = (int)errNum.Success;
            JsonModel jsmodel = new JsonModel();
            try
            {
                Eva_CourseType_Table delete_Eva_CourseType_Table = Constant.Eva_CourseType_Table_List.FirstOrDefault(i => i.Id == Id);

                if (delete_Eva_CourseType_Table != null)
                {
                    Eva_CourseType_Table delete_Eva_CourseType_Table_clone = Constant.Clone<Eva_CourseType_Table>(delete_Eva_CourseType_Table);
                    delete_Eva_CourseType_Table_clone.IsDelete = (int)IsDelete.Delete;
                    var jsmo = Constant.Eva_CourseType_TableService.Update(delete_Eva_CourseType_Table_clone);
                    if (jsmo.errNum == 0)
                    {
                        Constant.Eva_CourseType_Table_List.Remove(delete_Eva_CourseType_Table);
                        jsmodel = JsonModel.get_jsonmodel(intSuccess, "success", 0);

                        TableUsingReduce(delete_Eva_CourseType_Table_clone.TableId);
                    }
                    else
                    {
                        jsonModel = JsonModel.get_jsonmodel(3, "failed", "该项分配不存在");
                    }
                }
            }
            catch (Exception ex)
            {
                jsonModel = JsonModel.get_jsonmodel(3, "failed", ex.Message);
                LogHelper.Error(ex);
            }
            return jsmodel;
        }

        #endregion

    }
}