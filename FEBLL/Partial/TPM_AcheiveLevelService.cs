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
    public partial class TPM_AcheiveLevelService : BaseService<TPM_AcheiveLevel>, ITPM_AcheiveLevelService
    {
        TPM_AcheiveLevelDal dal = new TPM_AcheiveLevelDal();
        #region 新增-修改 业绩等级
        /// <summary>
        /// 新增-修改 业绩等级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel TPM_AcheiveLevelAdd(TPM_AcheiveLevel model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.TPM_AcheiveLevelAdd(model);
                if (result.Split('-')[1] == "")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = result.Split('-')[0]
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = result.Split('-')[1],
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

        #region 调整业绩等级顺序
        public JsonModel TPM_AcheiveLevelSort(int id, string SortType)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.TPM_AcheiveLevelSort(id, SortType);
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
