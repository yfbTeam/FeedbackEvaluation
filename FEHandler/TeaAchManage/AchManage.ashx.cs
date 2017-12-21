using FEBLL;
using FEModel;
using FEUtility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FEHandler.TeaAchManage
{
    /// <summary>
    /// AchManage 的摘要说明
    /// </summary>
    public class AchManage : IHttpHandler
    {
        //业绩等级
        TPM_AcheiveLevelService AcheiveLevel_bll = new TPM_AcheiveLevelService();
        TPM_RewardEditionService RewardEdition_bll = new TPM_RewardEditionService();
        TPM_RewardLevelService RewardLevel_bll = new TPM_RewardLevelService();
        TPM_RewardInfoService RewardInfo_bll = new TPM_RewardInfoService();
        TPM_RewardBatchService RewardBatch_bll = new TPM_RewardBatchService();
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
                    /*业绩等级*/
                    case "GetAcheiveLevelData":
                        GetAcheiveLevelData(context);
                        break;
                    case "AddAcheiveLevelData":
                        AddAcheiveLevelData(context);
                        break;
                    case "DelAcheiveLevelData":
                        DelAcheiveLevelData(context);
                        break;
                    case "TPM_AcheiveLevelSort":
                        TPM_AcheiveLevelSort(context);
                        break;
                    case "ChangeAwardSwichStatus":
                        ChangeAwardSwichStatus(context);
                        break;
                    /*奖励项目版本*/
                    case "GetRewardEditionData":
                        GetRewardEditionData(context);
                        break;
                    case "AddRewardEditionData":
                        AddRewardEditionData(context);
                        break;
                    case "ChangeRewardEditionAllot":
                        ChangeRewardEditionAllot(context);
                        break;
                    case "Del_RewardEdition":
                        Del_RewardEdition(context);
                        break;
                    /*奖励项目等级*/
                    case "GetRewardLevelData":
                        GetRewardLevelData(context);
                        break;
                    case "AddRewardLevelData":
                        AddRewardLevelData(context);
                        break;
                    case "SortRewardLevelData":
                        SortRewardLevelData(context);
                        break;
                    /*奖项管理*/
                    case "GetRewardInfoData":
                        GetRewardInfoData(context);
                        break;
                    case "AddRewardInfoData":
                        AddRewardInfoData(context);
                        break;
                    case "SortRewardInfoData":
                        SortRewardInfoData(context);
                        break;
                    case "AddRewardDash":
                        AddRewardDash(context);
                        break;
                    case "Del_RewardBatch":
                        Del_RewardBatch(context);
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

        #region 正常操作方式

        #region 业绩等级
        private void GetAcheiveLevelData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TableName", " TPM_AcheiveLevel");
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("Pid", context.Request["Pid"]);
                jsonModel = AcheiveLevel_bll.GetPage(ht, false);

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
        private void AddAcheiveLevelData(HttpContext context)
        {
            try
            {
                TPM_AcheiveLevel model = new TPM_AcheiveLevel();
                model.Name = context.Request["Name"];
                model.Pid = RequestHelper.int_transfer(context.Request, "Pid");
                model.Id = RequestHelper.int_transfer(context.Request, "Id");

                jsonModel = AcheiveLevel_bll.TPM_AcheiveLevelAdd(model);

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
        private void TPM_AcheiveLevelSort(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                string SortType = context.Request["SortType"];
                jsonModel = AcheiveLevel_bll.TPM_AcheiveLevelSort(Id, SortType);
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
        private void DelAcheiveLevelData(HttpContext context)
        {
            try
            {
                int itemid = Convert.ToInt32(context.Request["ItemId"]);
                jsonModel = AcheiveLevel_bll.Delete(itemid);
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
        /// <summary>
        /// 修改业绩金额分配状态
        /// </summary>
        /// <param name="context"></param>
        private void ChangeAwardSwichStatus(HttpContext context)
        {
            try
            {
                int Id = Convert.ToInt32(context.Request["Id"]);
                TPM_AcheiveLevel model = AcheiveLevel_bll.GetEntityById(Id).retData as TPM_AcheiveLevel;
                model.AwardSwich = Convert.ToByte(context.Request["AwardSwich"]);
                jsonModel = AcheiveLevel_bll.Update(model);
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

        #region 奖励项目版本
        private void GetRewardEditionData(HttpContext context)
        {
            try
            {
                int LID = RequestHelper.int_transfer(context.Request, "LID");
                bool Ispage = true;
                if (context.Request["IsPage"].SafeToString() != "")
                {
                    Ispage = Convert.ToBoolean(context.Request["IsPage"]);
                }
                Hashtable ht = new Hashtable();
                ht.Add("TableName", "TPM_RewardEdition");
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                jsonModel = RewardEdition_bll.GetPage(ht, Ispage, " and IsDelete=0 and lid=" + LID);
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
        private void AddRewardEditionData(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                int lid= RequestHelper.int_transfer(context.Request, "LID");
                DateTime beginTime= RequestHelper.DateTime_transfer(context.Request, "BeginTime");
                DateTime endTime = RequestHelper.DateTime_transfer(context.Request, "EndTime");
                //" BeginTime>" +endTime + " or EndTime<"+beginTime 时间不重叠（not取非,获取不重叠）
                string id_str = Id == 0 ? "" : " and Id!="+Id;
                Hashtable ht = new Hashtable();
                ht.Add("TableName", "TPM_RewardEdition");
                JsonModel edition_result =RewardEdition_bll.GetPage(ht, false, id_str+" and IsDelete=0 and lid=" + lid+ " and not( convert(varchar(10),BeginTime,21)>'" + context.Request["endTime"] + "' or convert(varchar(10),EndTime,21)<'" + context.Request["beginTime"]+ "')" );
                if (edition_result.errNum==0)
                {
                    jsonModel = JsonModel.get_jsonmodel(-1, "与其他版本时间交叉！", edition_result.retData);
                    return;
                }
                if (Id == 0)
                {
                    TPM_RewardEdition model = new TPM_RewardEdition();
                    model.Name = context.Request["Name"];
                    model.LID = lid;
                    model.BeginTime = beginTime;
                    model.EndTime = endTime;                   
                    jsonModel = RewardEdition_bll.Add(model);                   
                    Id =Convert.ToInt32(jsonModel.retData);
                }
                else
                {
                    TPM_RewardEdition model = RewardEdition_bll.GetEntityById(Id).retData as TPM_RewardEdition;
                    model.Name = context.Request["Name"];
                    model.BeginTime = beginTime;
                    model.EndTime = endTime;                   
                    jsonModel = RewardEdition_bll.Update(model);
                }
                if (jsonModel.errNum == 0)
                {
                    string add_Path = RequestHelper.string_transfer(context.Request, "Add_Path");
                    string edit_PathId = RequestHelper.string_transfer(context.Request, "Edit_PathId");
                    if (!string.IsNullOrEmpty(add_Path) || !string.IsNullOrEmpty(edit_PathId))
                    {
                        List<Sys_Document> pathlist = new List<Sys_Document>();
                        if (!string.IsNullOrEmpty(add_Path))
                        {
                            pathlist = JsonConvert.DeserializeObject<List<Sys_Document>>(add_Path);
                        }
                        new Sys_DocumentService().OperDocument(pathlist, edit_PathId, Id);
                    }
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

        #region 修改项目版本金额分配状态
        private void ChangeRewardEditionAllot(HttpContext context)
        {
            try
            {
                int Id = Convert.ToInt32(context.Request["Id"]);
                TPM_RewardEdition model = RewardEdition_bll.GetEntityById(Id).retData as TPM_RewardEdition;
                model.IsMoneyAllot = Convert.ToByte(context.Request["IsMoneyAllot"]);
                jsonModel = RewardEdition_bll.Update(model);
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

        #region 删除项目版本
        private void Del_RewardEdition(HttpContext context)
        {
            try
            {
                int itemid = Convert.ToInt32(context.Request["ItemId"]);
                //是否已经引用
                int useCount = Convert.ToInt32(SQLHelp.ExecuteScalar("select count(1) from TPM_AcheiveRewardInfo where IsDelete=0 and LID=" + RequestHelper.int_transfer(context.Request, "LID")+" and convert(varchar(10),DefindDate,21) between '" + context.Request["beginTime"] + "' and '"+ context.Request["endTime"] + "'", CommandType.Text, null));
                if (useCount > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(-1, "该版本已经被使用！", "");
                    return;
                }
                jsonModel = RewardEdition_bll.DeleteFalse(itemid);
                if (jsonModel.errNum == 0)
                {
                    new Sys_DocumentService().DelDocByRelId(1, itemid);//删除版本相关文件
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
        #endregion

        #region 奖励项目等级
        private void GetRewardLevelData(HttpContext context)
        {
            try
            {
                bool IsPage = true;
                if (context.Request["IsPage"] != "" && context.Request["IsPage"] != "undefined")
                {
                    IsPage = Convert.ToBoolean(context.Request["IsPage"]);
                }
                int Id = RequestHelper.int_transfer(context.Request, "Id");

                int EID = RequestHelper.int_transfer(context.Request, "EID");
                int LID = RequestHelper.int_transfer(context.Request, "LID");
                string DefindDate = RequestHelper.string_transfer(context.Request, "DefindDate");
                Hashtable ht = new Hashtable();
                if (EID > 0)
                {
                    ht.Add("EID", EID);
                }
                if (LID > 0)
                {
                    ht.Add("LID", LID);
                }
                if (Id > 0)
                {
                    ht.Add("Id", Id);
                }
                if(!string.IsNullOrEmpty(DefindDate))
                {
                    ht.Add("DefindDate", RequestHelper.DateTime_transfer(context.Request, "DefindDate"));
                }               
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                jsonModel = RewardLevel_bll.GetPage(ht, IsPage);

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
        private void AddRewardLevelData(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                if (Id == 0)
                {
                    TPM_RewardLevel model = new TPM_RewardLevel();
                    model.Name = context.Request["Name"];
                    model.EID = RequestHelper.int_transfer(context.Request, "EID");
                    model.Sort = RequestHelper.int_transfer(context.Request, "Sort");

                    jsonModel = RewardLevel_bll.Add(model);
                }
                else
                {
                    TPM_RewardLevel model = RewardLevel_bll.GetEntityById(Id).retData as TPM_RewardLevel;
                    string Name = context.Request["Name"].SafeToString();
                    int Sort = RequestHelper.int_transfer(context.Request, "Sort");
                    model.Name = Name;
                    model.Sort = Sort;

                    jsonModel = RewardLevel_bll.Update(model);

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
        private void SortRewardLevelData(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                string SortType = context.Request["SortType"];
                jsonModel = RewardLevel_bll.TPM_RewardLevelSort(Id, SortType);
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

        #region 奖项管理
        private void GetRewardInfoData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                int LID = RequestHelper.int_transfer(context.Request, "LID");
                if (LID > 0)
                {
                    ht.Add("LID", LID);
                }
                string Id = context.Request["Id"].SafeToString();
                if (Id != "0" && Id.Length > 0)
                {
                    ht.Add("Id", Id);
                }
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                bool IsPage = true;
                if (context.Request["IsPage"] != "" && context.Request["IsPage"] != "undefined")
                {
                    IsPage = Convert.ToBoolean(context.Request["IsPage"]);
                }
                jsonModel = RewardInfo_bll.GetPage(ht, IsPage);
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
        private void AddRewardInfoData(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                if (Id == 0)
                {
                    TPM_RewardInfo model = new TPM_RewardInfo();
                    model.Name = context.Request["Name"];
                    model.LID = RequestHelper.int_transfer(context.Request, "LID");
                    model.Award = RequestHelper.decimal_transfer(context.Request, "Award");
                    model.Score = RequestHelper.int_transfer(context.Request, "Score");
                    model.ScoreType = Convert.ToByte(context.Request["ScoreType"]);
                    model.Sort = RequestHelper.int_transfer(context.Request, "Sort");
                    jsonModel = RewardInfo_bll.Add(model);
                    if (jsonModel.errNum==0)
                    {
                        TPM_RewardBatch r_batch = new TPM_RewardBatch();
                        r_batch.Reward_Id = Convert.ToInt32(jsonModel.retData);
                        r_batch.Money = RequestHelper.decimal_transfer(context.Request, "Award");
                        r_batch.CreateUID = RequestHelper.string_transfer(context.Request, "CreateUID");
                        RewardBatch_bll.Add(r_batch);                        
                    }
                }
                else
                {
                    int Batch_Id = RequestHelper.int_transfer(context.Request, "Batch_Id");
                    int useCount = RewardInfo_bll.GetReward_UseCount(Id);//奖项是否已经引用
                    if (useCount > 0)
                    {
                        jsonModel = JsonModel.get_jsonmodel(-1, "该奖项已经被使用！", "");
                        return;
                    }
                    int score_Count = RewardInfo_bll.GetRewardScore_UseCount(Id);//奖项分数是否已经引用
                    if (score_Count > 0)
                    {
                        jsonModel = JsonModel.get_jsonmodel(-2, "该奖金分数已经被使用！", "");
                        return;
                    }
                    int money_Count = RewardInfo_bll.GetRewardMoney_UseCount(Batch_Id);//奖项金额是否已经引用
                    if (money_Count > 0)
                    {
                        jsonModel = JsonModel.get_jsonmodel(-3, "该奖项金额已经被使用！", "");
                        return;
                    }
                    TPM_RewardInfo model = RewardInfo_bll.GetEntityById(Id).retData as TPM_RewardInfo;
                    decimal Award = RequestHelper.decimal_transfer(context.Request["Award"]);
                    int Score = RequestHelper.int_transfer(context.Request, "Score");
                    model.Award = Award;
                    model.Score = Score;
                    model.Name = context.Request["Name"];
                    model.ScoreType = Convert.ToByte(context.Request["ScoreType"]);
                    model.Sort = RequestHelper.int_transfer(context.Request, "Sort");
                    jsonModel = RewardInfo_bll.Update(model);
                    if (jsonModel.errNum == 0)
                    {                        
                        TPM_RewardBatch r_batch = RewardBatch_bll.GetEntityById(Batch_Id).retData as TPM_RewardBatch;
                        r_batch.Money= Award;
                        RewardBatch_bll.Update(r_batch);
                    }

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

        private void SortRewardInfoData(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                string SortType = context.Request["SortType"];
                jsonModel = RewardInfo_bll.TPM_RewardInfoSort(Id, SortType);
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
        private void AddRewardDash(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");                
                int Reward_Id = RequestHelper.int_transfer(context.Request["Reward_Id"]);
                decimal AddAward = RequestHelper.decimal_transfer(context.Request["AddAward"]); 
                if(Id==0)
                {
                    TPM_RewardBatch r_batch = new TPM_RewardBatch();
                    r_batch.Reward_Id = Reward_Id;
                    r_batch.Money = AddAward;
                    r_batch.CreateUID = RequestHelper.string_transfer(context.Request, "CreateUID");
                    jsonModel= RewardBatch_bll.Add(r_batch);
                }
                else
                {
                    int useCount = RewardInfo_bll.GetRewardMoney_UseCount(Id);//是否已经引用
                    if (useCount > 0)
                    {
                        jsonModel = JsonModel.get_jsonmodel(-1, "该奖金已经被使用！", "");
                        return;
                    }
                    TPM_RewardBatch model = RewardBatch_bll.GetEntityById(Id).retData as TPM_RewardBatch;                   
                    model.Money = AddAward;                   
                    jsonModel = RewardBatch_bll.Update(model);
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

        #region 删除追加奖金
        private void Del_RewardBatch(HttpContext context)
        {
            try
            {
                int itemid = Convert.ToInt32(context.Request["ItemId"]);           
                int useCount = RewardInfo_bll.GetRewardMoney_UseCount(itemid);//是否已经引用
                if (useCount > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(-1, "该奖金已经被使用！", "");
                    return;
                }
                jsonModel = RewardBatch_bll.DeleteFalse(itemid);                
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
        #endregion

        #endregion
        #region 缓存操作方式
        /*
        #region 业绩等级
        private void GetAcheiveLevelData(HttpContext context)
        {
            try
            {
                //ID
                int id = RequestHelper.int_transfer(context.Request, "Id");
                //分类名称
                string Name = HttpUtility.UrlDecode(context.Request["Name"].SafeToString());
                var list = Constant.TPM_AcheiveLevel_List.OrderBy(m => m.Sort).ToList();
                if (Name.Length > 0)
                {
                    list = list.Where(o => o.Name == Name).ToList();
                }
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
        private void AddAcheiveLevelData(HttpContext context)
        {
            try
            {
                TPM_AcheiveLevel model = new TPM_AcheiveLevel();
                model.Name = context.Request["Name"];
                model.Pid = RequestHelper.int_transfer(context.Request, "Pid");
                model.Id = RequestHelper.int_transfer(context.Request, "Id");

                jsonModel = AcheiveLevel_bll.TPM_AcheiveLevelAdd(model);
                if (jsonModel.errNum == 0)
                {
                    model.Id = Convert.ToInt32(jsonModel.retData);
                    model.CreateTime = DateTime.Now;
                    if (!Constant.TPM_AcheiveLevel_List.Contains(model))
                    {
                        Constant.TPM_AcheiveLevel_List.Add(model);
                    }
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
        private void TPM_AcheiveLevelSort(HttpContext context)
        {
            try
            {
                int Id = RequestHelper.int_transfer(context.Request, "Id");
                string SortType = context.Request["SortType"];

                jsonModel = AcheiveLevel_bll.TPM_AcheiveLevelSort(Id, SortType);
                if (jsonModel.errNum == 0)
                {
                    #region 修改缓存数据
                    var Cur = Constant.TPM_AcheiveLevel_List.Where(t => t.Id == Id).FirstOrDefault();

                    if (SortType == "up")
                    {
                        var list = from m in Constant.TPM_AcheiveLevel_List
                                   orderby m.Sort descending
                                   select new { m };
                        var Other = list.Where(obj => obj.m.Sort < Cur.Sort).FirstOrDefault();
                        Constant.TPM_AcheiveLevel_List.Where(t => t.Id == Id).FirstOrDefault();
                        Cur.Sort = Other.m.Sort;
                        Other.m.Sort = Cur.Sort;
                    }
                    else
                    {
                        var list = from m in Constant.TPM_AcheiveLevel_List
                                   orderby m.Sort
                                   select new { m };
                        var Other = list.Where(obj => obj.m.Sort < Cur.Sort).FirstOrDefault();
                        Constant.TPM_AcheiveLevel_List.Where(t => t.Id == Id).FirstOrDefault();
                        Cur.Sort = Other.m.Sort;
                        Other.m.Sort = Cur.Sort;
                    }


                    #endregion
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
        private void DelAcheiveLevelData(HttpContext context)
        {
            try
            {
                int itemid = Convert.ToInt32(context.Request["ItemId"]);
                jsonModel = AcheiveLevel_bll.Delete(itemid);
                if (jsonModel.errNum == 0)
                {
                    TPM_AcheiveLevel model = Constant.TPM_AcheiveLevel_List.FirstOrDefault(t => t.Id == itemid);
                    Constant.TPM_AcheiveLevel_List.Remove(model);
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

        #region 奖励项目版本
        private void GetRewardEditionData(HttpContext context)
        {
            try
            {
                //ID
                int LID = RequestHelper.int_transfer(context.Request, "LID");
                //分类名称
                string Name = HttpUtility.UrlDecode(context.Request["Name"].SafeToString());
                var level = from obj in Constant.TPM_RewardEdition_List.Where(t => t.LID == LID)
                            orderby obj.CreateTime descending
                            select new { obj };

                var list = level.ToList();
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
        private void AddRewardEditionData(HttpContext context)
        {
            try
            {
                TPM_RewardEdition model = new TPM_RewardEdition();
                model.Name = context.Request["Name"];
                model.LID = RequestHelper.int_transfer(context.Request, "LID");
                model.BeginTime = RequestHelper.DateTime_transfer(context.Request, "BeginTime");
                model.EndTime = RequestHelper.DateTime_transfer(context.Request, "EndTime");

                jsonModel = RewardEdition_bll.Add(model);
                if (jsonModel.errNum == 0)
                {
                    model.Id = Convert.ToInt32(jsonModel.retData);
                    model.CreateTime = DateTime.Now;
                    if (!Constant.TPM_RewardEdition_List.Contains(model))
                    {
                        Constant.TPM_RewardEdition_List.Add(model);
                    }
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

        #region 奖励项目等级
        private void GetRewardLevelData(HttpContext context)
        {
            try
            {
                //ID
                int EID = RequestHelper.int_transfer(context.Request, "EID");
                //分类名称
                string Name = HttpUtility.UrlDecode(context.Request["Name"].SafeToString());
                var level = from obj in Constant.TPM_RewardLevel_List.Where(t => t.EID == EID)
                            orderby obj.Sort
                            select new { obj };

                var list = level.ToList();
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
        private void AddRewardLevelData(HttpContext context)
        {
            try
            {
                TPM_RewardLevel model = new TPM_RewardLevel();
                model.Name = context.Request["Name"];
                model.EID = RequestHelper.int_transfer(context.Request, "EID");
                model.Sort = RequestHelper.int_transfer(context.Request, "Sort");

                jsonModel = RewardLevel_bll.Add(model);
                if (jsonModel.errNum == 0)
                {
                    model.Id = Convert.ToInt32(jsonModel.retData);
                    model.CreateTime = DateTime.Now;
                    if (!Constant.TPM_RewardLevel_List.Contains(model))
                    {
                        Constant.TPM_RewardLevel_List.Add(model);
                    }
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

        #region 奖项管理
        private void GetRewardInfoData(HttpContext context)
        {
            try
            {
                //ID
                int LID = RequestHelper.int_transfer(context.Request, "LID");
                //分类名称
                string Name = HttpUtility.UrlDecode(context.Request["Name"].SafeToString());
                var level = from obj in Constant.TPM_RewardInfo_List.Where(t => t.LID == LID)
                            orderby obj.Id descending
                            select new { obj };

                var list = level.ToList();
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
        private void AddRewardInfoData(HttpContext context)
        {
            try
            {
                TPM_RewardInfo model = new TPM_RewardInfo();
                model.Name = context.Request["Name"];
                model.LID = RequestHelper.int_transfer(context.Request, "LID");
                model.Award = RequestHelper.decimal_transfer(context.Request, "Award");
                model.Score = RequestHelper.int_transfer(context.Request, "Score");
                model.ScoreType = Convert.ToByte(context.Request["ScoreType"]);

                jsonModel = RewardInfo_bll.Add(model);
                if (jsonModel.errNum == 0)
                {
                    model.Id = Convert.ToInt32(jsonModel.retData);
                    model.CreateTime = DateTime.Now;
                    if (!Constant.TPM_RewardInfo_List.Contains(model))
                    {
                        Constant.TPM_RewardInfo_List.Add(model);
                    }
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
        */
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