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
    public partial class TPM_RewardBatchDal : BaseDal<TPM_RewardBatch>, ITPM_RewardBatchDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@" select r_bat.*,(select count(1) from TPM_AuditReward where IsDelete=0 and Status in(1,3) and RewardBatch_Id=r_bat.Id)UseCount ");
                if (ht["IsOnlyBase"].SafeToString()=="1") //查询关联表
                {
                    str.Append(@" ,info.Id AchieveId,isnull(aud.Id,0) AuditId,isnull(aud.Status,0)AuditStatus 
                      ,isnull((select sum(AllotMoney) from TPM_AllotReward where Audit_Id=aud.Id),0)HasAllot
                     from TPM_RewardBatch r_bat
                     left join TPM_AcheiveRewardInfo info on r_bat.Reward_Id=info.Rid and info.IsDelete=0
                     left join TPM_AuditReward aud on r_bat.Id=aud.RewardBatch_Id and info.Id=aud.Acheive_Id and aud.IsDelete=0 ");
                }
                else //只查询基础表
                {
                    str.Append(@" from TPM_RewardBatch r_bat ");
                }
                str.Append(@" where r_bat.IsDelete=0 ");
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("Reward_Id") && !string.IsNullOrEmpty(ht["Reward_Id"].SafeToString()))
                {
                    str.Append(" and r_bat.Reward_Id=@Reward_Id ");
                    pms.Add(new SqlParameter("@Reward_Id", ht["Reward_Id"].ToString()));
                }
                if (ht.ContainsKey("AchieveId") && !string.IsNullOrEmpty(ht["AchieveId"].SafeToString()))
                {
                    str.Append(" and info.Id=@AchieveId ");
                    pms.Add(new SqlParameter("@AchieveId", ht["AchieveId"].ToString()));
                }
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                {
                    str.Append(" and r_bat.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("AuditStatus") && !string.IsNullOrEmpty(ht["AuditStatus"].SafeToString()))
                {
                    str.Append(" and isnull(aud.Status,0) " + ht["AuditStatus"].ToString());                   
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                string orderby = "Id";
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, orderby, StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }

        #region 获取奖金是否使用
        public int GetRewardMoney_UseCount(int RewardBatch_Id)
        {
            string str = "select count(1) from TPM_AuditReward where IsDelete=0 and Status in(1,3) and RewardBatch_Id=@RewardBatch_Id";
            List<SqlParameter> op_pms = new List<SqlParameter>();
            op_pms.Add(new SqlParameter("@RewardBatch_Id", RewardBatch_Id));
            int result =Convert.ToInt32(SQLHelp.ExecuteScalar(str, CommandType.Text, op_pms.ToArray()));
            return result;
        }
        #endregion  
    }
}
