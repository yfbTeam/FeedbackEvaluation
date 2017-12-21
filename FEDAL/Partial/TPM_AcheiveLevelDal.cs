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
    public partial class TPM_AcheiveLevelDal : BaseDal<TPM_AcheiveLevel>, ITPM_AcheiveLevelDal
    {
        #region 新增-修改 业绩等级
        /// <summary>
        /// 新增-修改 业绩等级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string TPM_AcheiveLevelAdd(TPM_AcheiveLevel model)
        {
            SqlParameter[] param = {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Pid", model.Pid),
                    new SqlParameter("@ID", model.Id),
            };
            object obj = SQLHelp.ExecuteScalar("TPM_AcheiveLevelAdd", CommandType.StoredProcedure, param);
            return obj.ToString();
        }
        #endregion

        #region 调整业绩等级顺序
        public string TPM_AcheiveLevelSort(int id, string SortType)
        {
            SqlParameter[] param = {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@SortType",SortType)
            };
            object obj = SQLHelp.ExecuteScalar("TPM_AcheiveLevelSort", CommandType.StoredProcedure, param);
            return obj.ToString();
        }

        #endregion
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {

            {
                RowCount = 0;
                DataTable dt = new DataTable();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append(@"select * from TPM_AcheiveLevel  where 1=1");
                    int StartIndex = 0;
                    int EndIndex = 0;
                    if (ht.ContainsKey("Pid") && !string.IsNullOrEmpty(ht["Pid"].SafeToString()))
                    {
                        str.Append(" and Pid=" + ht["Pid"].SafeToString());
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
        }
    }
}
