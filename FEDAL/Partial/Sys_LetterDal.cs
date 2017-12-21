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
    public partial class Sys_LetterDal : BaseDal<Sys_Letter>, ISys_LetterDal
    {
        #region 发送站内信
        public int Send_Letter(Sys_Letter model,string Major_Id)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Title", model.Title),
                    new SqlParameter("@Contents", model.Contents),
                    new SqlParameter("@ReceiveUID",model.ReceiveUID),
                    new SqlParameter("@ReceiveName", model.ReceiveName),
                    new SqlParameter("@Reply_Id", model.Reply_Id),
                    new SqlParameter("@IsRead", model.IsRead),
                    new SqlParameter("@CreateUID", model.CreateUID),                    
                    new SqlParameter("@Major_Id", Major_Id)
            };
            object obj = SQLHelp.ExecuteScalar("Send_Letter", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
