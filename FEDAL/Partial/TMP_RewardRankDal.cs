using FEIDAL;
using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEDAL
{
    public partial class TMP_RewardRankDal : BaseDal<TMP_RewardRank>, ITMP_RewardRankDal
    {
        /// <summary>
        /// 生成排序
        /// </summary>
        /// <param name="RId"></param>
        /// <param name="RankNum"></param>
        /// <param name="HighScore"></param>
        /// <param name="OneRank"></param>
        /// <param name="OneScore"></param>
        /// <param name="TwoRank"></param>
        /// <param name="TwoScore"></param>
        /// <returns></returns>
        public string GenerateRank(string RId, string RankNum, string HighScore, string OneRank, string OneScore, string TwoRank, string TwoScore, string CreateUID)
        {
            SqlParameter[] param = {
                    new SqlParameter("@RId", RId),
                    new SqlParameter("@RankNum",RankNum),
                    new SqlParameter("@HighScore", HighScore),
                    new SqlParameter("@OneRank",OneRank),
                    new SqlParameter("@OneScore", OneScore),
                    new SqlParameter("@TwoRank",TwoRank),
                    new SqlParameter("@TwoScore", TwoScore),
                    new SqlParameter("@CreateUID",CreateUID)
            };
            object obj = SQLHelp.ExecuteScalar("TPM_GenerateRank", CommandType.StoredProcedure, param);
            return obj.ToString();
        }

        #region 修改等级排名
        public int Edit_Rank(List<TMP_RewardRank> items)
        {
            string str = "";
            List<SqlParameter> pms = new List<SqlParameter>();
            for (int i=0;i< items.Count; i++)
            {
                TMP_RewardRank item = items[i];
                str += "update TMP_RewardRank set Score=@Score"+i+" where Id=@Id"+i+";";
                pms.Add(new SqlParameter("@Id"+i, item.Id));
                pms.Add(new SqlParameter("@Score"+i, item.Score));
            }           
            return SQLHelp.ExecuteNonQuery(str, CommandType.Text, pms.ToArray());
        }
        #endregion
    }
}
