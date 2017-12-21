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
    public partial class TPM_RewardInfoDal : BaseDal<TPM_RewardInfo>, ITPM_RewardInfoDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select *,(select Money from TPM_RewardBatch where Id=R_Info.FirstId) as FirstMoney,
                isnull((select STUFF((select '+' + CAST(Money AS NVARCHAR(MAX)) from TPM_RewardBatch where IsDelete=0 and Reward_Id=R_Info.Id and Id!=R_Info.FirstId FOR xml path('')), 1, 1, '')),0) as AddMoney
                ,(select count(1) from TPM_AcheiveRewardInfo where IsDelete=0 and (Status=1 or Status>2) and Rid=R_Info.Id)RewardCount
                ,(select count(1) from TPM_AcheiveRewardInfo where IsDelete=0 and (Status=5 or Status>6) and Rid=R_Info.Id)ScoreCount
                ,(select count(1) from TPM_AuditReward where IsDelete=0 and Status in(1,3) and RewardBatch_Id=FirstId)AddRewCount
                from
                (select *,(select top 1 Id from TPM_RewardBatch where Reward_Id=info.Id and IsDelete=0 order by Id) as FirstId
                 from TPM_RewardInfo info) as R_Info where 1=1");
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("LID") && !string.IsNullOrEmpty(ht["LID"].SafeToString()))
                {
                    str.Append(" and LID=" + ht["LID"].SafeToString());
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
        public string TPM_RewardInfoSort(int id, string SortType)
        {
            SqlParameter[] param = {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@SortType",SortType)
            };
            object obj = SQLHelp.ExecuteScalar("TPM_RewardInfoSort", CommandType.StoredProcedure, param);
            return obj.ToString();
        }

        #endregion
        #region 追加奖金
        public int AddRewardDash(int Id, int Award)
        {
            string str = "update TPM_RewardInfo set AddAward=AddAward+" + Award + " where Id=" + Id;
            return SQLHelp.ExecuteNonQuery(str, CommandType.Text, null);
        }
        #endregion

        #region 获取奖项是否使用
        public int GetReward_UseCount(int Reward_Id)
        {
            string str = "select count(1) from TPM_AcheiveRewardInfo where IsDelete=0 and (Status=1 or Status>2) and Rid=@Rid";
            List<SqlParameter> op_pms = new List<SqlParameter>();
            op_pms.Add(new SqlParameter("@Rid", Reward_Id));
            int result = Convert.ToInt32(SQLHelp.ExecuteScalar(str, CommandType.Text, op_pms.ToArray()));
            return result;
        }
        #endregion

        #region 获取奖项分数是否使用
        public int GetRewardScore_UseCount(int Reward_Id)
        {
            string str = "select count(1) from TPM_AcheiveRewardInfo where IsDelete=0 and (Status=5 or Status>6) and Rid=@Rid";
            List<SqlParameter> op_pms = new List<SqlParameter>();
            op_pms.Add(new SqlParameter("@Rid", Reward_Id));
            int result = Convert.ToInt32(SQLHelp.ExecuteScalar(str, CommandType.Text, op_pms.ToArray()));
            return result;
        }
        #endregion
    }
}
