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
    public partial class TPM_RewardLevelService : BaseService<TPM_RewardLevel>, ITPM_RewardLevelService
    {
        TPM_RewardLevelDal dal = new TPM_RewardLevelDal();
        

        #region 调整奖项等级顺序
        public JsonModel TPM_RewardLevelSort(int id, string SortType)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.TPM_RewardLevelSort(id, SortType);
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
    }
   
}
