using FEIDAL;
using FEModel;
using FEUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEDAL
{
    public partial class TPM_RewardLevelDal : BaseDal<TPM_RewardLevel>, ITPM_RewardLevelDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select * from TPM_RewardLevel  where 1=1");
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("EID") && !string.IsNullOrEmpty(ht["EID"].SafeToString()))
                {
                    str.Append(" and EID=" + ht["EID"].SafeToString());
                }
                if (ht.ContainsKey("LID") && !string.IsNullOrEmpty(ht["LID"].SafeToString()))
                {
                    if (!ht.ContainsKey("EID"))
                    {
                        str.Append(" and EID in (select id from TPM_RewardEdition where lid=" + ht["LID"].SafeToString());
                        if (!ht.ContainsKey("DefindDate"))
                        {
                            str.Append(" and convert(varchar(10),'" + ht["DefindDate"].SafeToString() + "',21) between convert(varchar(10),BeginTime,21) and convert(varchar(10),EndTime,21) ");
                        }
                        str.Append(")");
                    }
                }

                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                {
                    str.Append(" and Id=" + ht["Id"].SafeToString());
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "Sort", StartIndex,
                    EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }

        #region 调整业绩等级顺序
        public string TPM_RewardLevelSort(int id, string SortType)
        {
            SqlParameter[] param = {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@SortType",SortType)
            };
            object obj = SQLHelp.ExecuteScalar("TPM_RewardLevelSort", CommandType.StoredProcedure, param);
            return obj.ToString();
        }

        #endregion
    }
}
