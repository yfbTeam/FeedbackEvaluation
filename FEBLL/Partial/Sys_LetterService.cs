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
    public partial class Sys_LetterService : BaseService<Sys_Letter>, ISys_LetterService
    {
        Sys_LetterDal dal = new Sys_LetterDal();
        BLLCommon common = new BLLCommon();
        #region 发送站内信        
        public JsonModel Send_Letter(Sys_Letter model, string Major_Id)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.Send_Letter(model, Major_Id);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "",
                    retData = result
                };
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
