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
    public partial class TMP_RewardRankService : BaseService<TMP_RewardRank>, ITMP_RewardRankService
    {
        TMP_RewardRankDal dal = new TMP_RewardRankDal();
        public JsonModel GenerateRank(string RId, string RankNum, string HighScore, string OneRank, string OneScore, string TwoRank, string TwoScore, string CreateUID)
        {

            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.GenerateRank(RId, RankNum, HighScore, OneRank, OneScore, TwoRank, TwoScore, CreateUID);
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

        public JsonModel Edit_Rank(List<TMP_RewardRank> items)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.Edit_Rank(items);
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
    }
}
