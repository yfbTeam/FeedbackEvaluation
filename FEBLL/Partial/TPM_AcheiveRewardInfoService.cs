using FEDAL;
using FEIBLL;
using FEModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEBLL
{
    public partial class TPM_AcheiveRewardInfoService : BaseService<TPM_AcheiveRewardInfo>, ITPM_AcheiveRewardInfoService
    {
        TPM_AcheiveRewardInfoDal dal = new TPM_AcheiveRewardInfoDal();


        #region 新增业绩
        /// <summary>
        /// 新增业绩
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel TPM_AcheiveLevelAdd(TPM_AcheiveRewardInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.AddAcheiveRewardInfo(model);
                string[] proinfo = result.Split('|');
                if (proinfo[1] == "")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = proinfo[0]
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = proinfo[1],
                        retData = 0
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }

        }
        #endregion

        #region 批量修改成员
        public JsonModel Edit_AcheiveMember(List<TPM_RewardUserInfo> items, int riid = 0,int bookId = 0)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = new TPM_RewardUserInfoDal().Edit_AcheiveMember(items, riid, bookId);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "数据更新失败",
                        retData = ""
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion        

        #region 批量修改作者信息
        public JsonModel Edit_RewardUserInfo(List<TPM_RewardUserInfo> items, int BookId = 0)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = new TPM_RewardUserInfoDal().Edit_RewardUserInfo(items, BookId);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "数据更新失败",
                        retData = ""
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion        

        #region 保存奖金分配信息
        public JsonModel Oper_AuditAllotReward(TPM_AuditReward audModel, List<TPM_AllotReward> items)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int[] retInfo = new TPM_AllotRewardDal().Oper_AuditAllotReward(audModel, items);
                if (retInfo[0] > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = retInfo[1]
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "数据更新失败",
                        retData = ""
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 修改业绩状态
        public JsonModel Edit_AchieveStatus(int id, int status)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.Edit_AchieveStatus(id,status);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "数据更新失败",
                        retData = ""
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion
    }
}
