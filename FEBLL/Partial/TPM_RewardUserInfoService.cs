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
    public partial class TPM_RewardUserInfoService : BaseService<TPM_RewardUserInfo>, ITPM_RewardUserInfoService
    {
        TPM_RewardUserInfoDal dal = new TPM_RewardUserInfoDal();
        #region 新增用户
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel TPM_RewardUserAdd(TPM_RewardUserInfo model)
        {
           
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.TPM_RewardUserAdd(model);
                if (result == "")
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
                        errMsg = result,
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
    }
}
