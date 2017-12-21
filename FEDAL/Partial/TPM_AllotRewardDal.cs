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
    public partial class TPM_AllotRewardDal : BaseDal<TPM_AllotReward>, ITPM_AllotRewardDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@" select allot.*,aud.Acheive_Id,aud.RewardBatch_Id,aud.Status from TPM_AllotReward allot
                     left join TPM_AuditReward aud on allot.Audit_Id=aud.Id
                     where allot.IsDelete=0 ");               
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("RewardBatch_Id") && !string.IsNullOrEmpty(ht["RewardBatch_Id"].SafeToString()))
                {
                    str.Append(" and aud.RewardBatch_Id=@RewardBatch_Id ");
                    pms.Add(new SqlParameter("@RewardBatch_Id", ht["RewardBatch_Id"].ToString()));
                }
                if (ht.ContainsKey("AchieveId") && !string.IsNullOrEmpty(ht["AchieveId"].SafeToString()))
                {
                    str.Append(" and aud.Acheive_Id=@AchieveId ");
                    pms.Add(new SqlParameter("@AchieveId", ht["AchieveId"].ToString()));
                }
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                {
                    str.Append(" and allot.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }

        #region 保存奖金分配信息
        public int[] Oper_AuditAllotReward(TPM_AuditReward audModel, List<TPM_AllotReward> items)
        {
            int result = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            StringBuilder str = new StringBuilder();
            pms.Add(new SqlParameter("@Acheive_Id", audModel.Acheive_Id));
            pms.Add(new SqlParameter("@RewardBatch_Id", audModel.RewardBatch_Id));
            string sql = "select Id from TPM_AuditReward where Acheive_Id=@Acheive_Id and RewardBatch_Id=@RewardBatch_Id and IsDelete=0";
            int auditid = Convert.ToInt32(SQLHelp.ExecuteScalar(sql, CommandType.Text, pms.ToArray()));
            if (auditid > 0)
            {
                pms.Add(new SqlParameter("@Status", audModel.Status));               
                str.Append("update TPM_AuditReward set Status=@Status where Id=@Audit_Id;");
            }
            else
            {
                auditid = new TPM_AuditRewardDal().Add(audModel);
            }
            pms.Add(new SqlParameter("@Audit_Id", auditid));
            if (items.Count() > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    TPM_AllotReward item = items[i];                    
                    if (item.AllotMoney == null)
                    {
                        pms.Add(new SqlParameter("@AllotMoney" + i, DBNull.Value));
                    }
                    else
                    {
                        pms.Add(new SqlParameter("@AllotMoney" + i, item.AllotMoney));
                    }
                    pms.Add(new SqlParameter("@RewardUser_Id" + i, item.RewardUser_Id));
                    pms.Add(new SqlParameter("@CreateUID" + i, item.CreateUID));
                    pms.Add(new SqlParameter("@EditUID" + i, item.CreateUID));
                    string allotsql = "select Id from TPM_AllotReward where Audit_Id=@Audit_Id and RewardUser_Id=@RewardUser_Id" + i + " and IsDelete=0";
                    int allotid = Convert.ToInt32(SQLHelp.ExecuteScalar(allotsql, CommandType.Text, pms.ToArray()));
                    if (allotid > 0)
                    {
                        pms.Add(new SqlParameter("@Id" + i, allotid));
                        str.Append("update TPM_AllotReward set AllotMoney=@AllotMoney" + i + ",EditUID=@EditUID" + i + " where Id=@Id" + i + ";");
                    }
                    else
                    {
                        str.Append(@"insert into TPM_AllotReward(Audit_Id,AllotMoney,RewardUser_Id,CreateUID) 
                            values(@Audit_Id,@AllotMoney" + i + ",@RewardUser_Id" + i + ",@CreateUID" + i + ");");
                    }
                }
                result = SQLHelp.ExecuteNonQuery(str.ToString(), CommandType.Text, pms.ToArray());
            }
            return new int[]{ result, auditid };
        }
        #endregion

    }
}
