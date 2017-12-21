using FEIDAL;
using FEModel;
using FEUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace FEDAL
{
    public partial class TPM_AcheiveRewardInfoDal : BaseDal<TPM_AcheiveRewardInfo>, ITPM_AcheiveRewardInfoDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            List<SqlParameter> pms = new List<SqlParameter>();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select a.*,uu.Name as ResponsName,u.Name as CreateName,l.name as GName,al.Name as GidName,
                    case when a.GPid=6 then bk.Name else a.Name end as AchiveName,
                    l.Type as AchieveType,ll.Name as LevelName,r.Name as RewadName,
                    (select STUFF((select ',' + CAST(Major_Name AS NVARCHAR(MAX)) from Major where Id in(select value from func_split(a.DepartMent,',')) FOR xml path('')), 1, 1, '')) as Major_Name,                   
                    case when a.GPid=1 then isnull((select Score from TMP_RewardRank where a.Sort=Id),0) else r.Score end as TotalScore,
                    r.ScoreType,r.Award,r.AddAward,bk.Name as BookName,bk.BookType,case when bk.BookType=1 then '无' else bk.ISBN end as ISBN ");
                if (ht.ContainsKey("LoginMajor_ID") && !string.IsNullOrEmpty(ht["LoginMajor_ID"].SafeToString()))
                {
                    str.Append(@",(select count(1) from TPM_RewardUserInfo ruser left join UserInfo u on ruser.UserNo = u.UniqueNo
             where ruser.IsDelete=0 and ruser.RIId=a.Id and u.Major_ID=@LoginMajor_ID) MajorCount ");
                    pms.Add(new SqlParameter("@LoginMajor_ID", ht["LoginMajor_ID"].SafeToString()));
                }
                str.Append(@" from TPM_AcheiveRewardInfo a 
                    inner join UserInfo u on a.CreateUid=u.UniqueNo 
                    inner join UserInfo uu on a.ResponsMan=uu.UniqueNo
                    left join Major m on a.DepartMent=m.Id 
                    inner join TPM_AcheiveLevel l on a.gpid=l.Id  
                    left join TPM_AcheiveLevel al on al.Id=a.Gid
                    inner join TPM_RewardLevel ll on a.Lid=ll.Id 
                    inner join TPM_RewardInfo r on a.Rid=r.Id 
                    left join TPM_BookStory bk on a.bookid=bk.id where a.IsDelete=0 ");
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("Status") && !string.IsNullOrEmpty(ht["Status"].SafeToString()))
                {
                    str.Append(" and a.Status in (" + ht["Status"].SafeToString() + ")");
                }
                if (ht.ContainsKey("Status_Com") && !string.IsNullOrEmpty(ht["Status_Com"].SafeToString()))
                {
                    str.Append(" and a.Status" + ht["Status_Com"].SafeToString());
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and a.Name like '%" + ht["Name"].SafeToString() + "%'");
                }
                if (ht.ContainsKey("ResponName") && !string.IsNullOrEmpty(ht["ResponName"].SafeToString()))
                {
                    str.Append(" and uu.Name like '%" + ht["ResponName"].SafeToString() + "%'"); //负责人名称
                }
                if (ht.ContainsKey("MyUno") && !string.IsNullOrEmpty(ht["MyUno"].SafeToString()))
                {
                    str.Append(" and (a.ResponsMan = '" + ht["MyUno"].SafeToString() + "' or (a.Id in(select distinct RIId from TPM_RewardUserInfo where IsDelete = 0 and UserNo = '" + ht["MyUno"].SafeToString() + "')))");
                }
                if (ht.ContainsKey("CreateUID") && !string.IsNullOrEmpty(ht["CreateUID"].SafeToString()))
                {
                    str.Append(" and a.CreateUID =@CreateUID ");
                    pms.Add(new SqlParameter("@CreateUID", ht["CreateUID"].SafeToString()));
                }
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                {
                    str.Append(" and a.Id =@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].SafeToString()));
                }
                if (ht.ContainsKey("GPid") && !string.IsNullOrEmpty(ht["GPid"].SafeToString()))
                {
                    str.Append(" and a.GPid =@GPid ");
                    pms.Add(new SqlParameter("@GPid", ht["GPid"].SafeToString()));
                }
                if (ht.ContainsKey("Gid") && !string.IsNullOrEmpty(ht["Gid"].SafeToString()))
                {
                    str.Append(" and a.Gid=@Gid ");
                    pms.Add(new SqlParameter("@Gid", ht["Gid"].SafeToString()));
                }                
                if (ht.ContainsKey("BookId") && !string.IsNullOrEmpty(ht["BookId"].SafeToString()))
                {
                    str.Append(" and a.BookId =@BookId ");
                    pms.Add(new SqlParameter("@BookId", ht["BookId"].SafeToString()));
                }
                if (ht.ContainsKey("DepartMent") && !string.IsNullOrEmpty(ht["DepartMent"].SafeToString()))
                {
                    str.Append(" and a.DepartMent =@DepartMent ");
                    pms.Add(new SqlParameter("@DepartMent", ht["DepartMent"].SafeToString()));
                }
                if (ht.ContainsKey("BeginTime") && !string.IsNullOrEmpty(ht["BeginTime"].SafeToString()))
                {
                    str.Append(" and a.CreateTime > '" + ht["BeginTime"].SafeToString() + "'");
                }
                if (ht.ContainsKey("EndTime") && !string.IsNullOrEmpty(ht["EndTime"].SafeToString()))
                {
                    str.Append(" and a.CreateTime < '" + ht["EndTime"].SafeToString() + "'");
                }
                if (ht.ContainsKey("Major_ID") && !string.IsNullOrEmpty(ht["Major_ID"].SafeToString()))
                {
                    str.Append(@" and a.Id in(select distinct ruser.RIId from TPM_RewardUserInfo ruser
                              left join UserInfo u on ruser.UserNo = u.UniqueNo
                              where ruser.IsDelete = 0 and ruser.RIId!= 0 and u.Major_ID=@Major_ID)");
                    pms.Add(new SqlParameter("@Major_ID", ht["Major_ID"].SafeToString()));
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
        public string AddAcheiveRewardInfo(TPM_AcheiveRewardInfo entity)
        {

            SqlParameter[] param = { 
                                  new SqlParameter("@Name",entity.Name),
                                  new SqlParameter("@Gid",entity.Gid),
                                  new SqlParameter("@GPid",entity.GPid),
                                  new SqlParameter("@BookId",entity.BookId),
                                  new SqlParameter("@TeaUNo",entity.TeaUNo),
                                  new SqlParameter("@Lid",entity.Lid),
                                  new SqlParameter("@Rid",entity.Rid),
                                  new SqlParameter("@Sort",entity.Sort),
                                  new SqlParameter("@Year",entity.Year),
                                  new SqlParameter("@ResponsMan",entity.ResponsMan),
                                  new SqlParameter("@DepartMent",entity.DepartMent),
                                  new SqlParameter("@FileEdionNo",entity.FileEdionNo),
                                  new SqlParameter("@FileNames",entity.FileNames),
                                  new SqlParameter("@DefindDepart",entity.DefindDepart),
                                  new SqlParameter("@DefindDate",entity.DefindDate),
                                  new SqlParameter("@FileInfo",entity.FileInfo),
                                  new SqlParameter("@Status",entity.Status),
                                  new SqlParameter("@CreateUID",entity.CreateUID)
                                  };
            object obj = SQLHelp.ExecuteScalar("TPM_AddAcheiveRewardInfo", CommandType.StoredProcedure, param);
            return obj.ToString();
        }

        #region 修改业绩状态
        public int Edit_AchieveStatus(int id,int status)
        {
            string str = "update TPM_AcheiveRewardInfo set Status=@Status where Id=@Id";
            List<SqlParameter> pms = new List<SqlParameter>();
            pms.Add(new SqlParameter("@Id", id));
            pms.Add(new SqlParameter("@Status", status));
            return SQLHelp.ExecuteNonQuery(str, CommandType.Text, pms.ToArray());
        }
        #endregion
    }
}
