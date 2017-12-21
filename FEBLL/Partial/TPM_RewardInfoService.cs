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
    public partial class TPM_RewardInfoService : BaseService<TPM_RewardInfo>, ITPM_RewardInfoService
    {
        TPM_RewardInfoDal dal = new TPM_RewardInfoDal();
        TPM_RewardBatchDal bat_dal = new TPM_RewardBatchDal();

        #region 调整业绩等级顺序
        public JsonModel TPM_RewardInfoSort(int id, string SortType)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.TPM_RewardInfoSort(id, SortType);
                if (result == "")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = result,
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
        public JsonModel AddRewardDash(int Id, int Award)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.AddRewardDash(Id, Award);
                if (result >0)
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

        #region 获取奖项是否使用
        public int GetReward_UseCount(int Reward_Id)
        {
            return dal.GetReward_UseCount(Reward_Id);
        }
        #endregion

        #region 获取奖项分数是否使用
        public int GetRewardScore_UseCount(int Reward_Id)
        {
            return dal.GetRewardScore_UseCount(Reward_Id);
        }
        #endregion

        #region 获取奖金是否使用
        public int GetRewardMoney_UseCount(int RewardBatch_Id)
        {
            return bat_dal.GetRewardMoney_UseCount(RewardBatch_Id);
        }
        #endregion 
    }
}
