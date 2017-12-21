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
    public partial class Sys_DocumentDal : BaseDal<Sys_Document>, ISys_DocumentDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select sdo.*
                               from Sys_Document sdo                              
                               where sdo.IsDelete=0 and 1=1");
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                {
                    str.Append(" and sdo.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("Type") && !string.IsNullOrEmpty(ht["Type"].SafeToString()))
                {
                    str.Append(" and sdo.Type=@Type ");
                    pms.Add(new SqlParameter("@Type", ht["Type"].ToString()));
                }
                if (ht.ContainsKey("RelationId") && !string.IsNullOrEmpty(ht["RelationId"].SafeToString()))
                {
                    str.Append(" and sdo.RelationId=@RelationId ");
                    pms.Add(new SqlParameter("@RelationId", ht["RelationId"].ToString()));
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

        #region 批量操作文件
        public int OperDocument(List<Sys_Document> pathlist, string edit_PathId, int relationid)
        {
            int result = 0;
            string str = "";
            List<SqlParameter> op_pms = new List<SqlParameter>();
            if (pathlist.Count()>0)
            {                
                op_pms.Add(new SqlParameter("@RelationId", relationid));
                for (int i = 0; i < pathlist.Count; i++)
                {
                    Sys_Document item = pathlist[i];
                    str += "insert into Sys_Document(Type,RelationId,Name,Url,CreateUID) values(@Type"+i+ ",@RelationId,@Name" + i + ",@Url" + i + ",@CreateUID" + i + ");";
                    op_pms.Add(new SqlParameter("@Type" + i, item.Type));
                    op_pms.Add(new SqlParameter("@Name" + i, item.Name));
                    op_pms.Add(new SqlParameter("@Url" + i, item.Url));
                    op_pms.Add(new SqlParameter("@CreateUID" + i, item.CreateUID));
                }
            }
            if (!string.IsNullOrEmpty(edit_PathId))
            {
                string[] eids = edit_PathId.Split(',');
                StringBuilder strFirst = new StringBuilder();
                foreach (string id in eids)
                {
                    strFirst.Append("@id" + id.ToString() + ",");
                    op_pms.Add(new SqlParameter("@id" + id.ToString(), id));
                }
                str = str+ string.Format("update Sys_Document set IsDelete=1 where id in({0});", strFirst.ToString().TrimEnd(','));
               
            }
            if (!string.IsNullOrEmpty(str))
            {
                result = SQLHelp.ExecuteNonQuery(str, CommandType.Text, op_pms.ToArray());
            }
            return result;
        }
        #endregion        

        #region 根据关联Id删除信息
        public int DelDocByRelId(int type,int relationid)
        {
            string str = "update Sys_Document set IsDelete=1 where Type=@Type and RelationId=@RelationId";
            List<SqlParameter> op_pms = new List<SqlParameter>();
            op_pms.Add(new SqlParameter("@Type", type));
            op_pms.Add(new SqlParameter("@RelationId", relationid));
            int result = SQLHelp.ExecuteNonQuery(str, CommandType.Text, op_pms.ToArray());
            return result;
        }
        #endregion        
    }
}
