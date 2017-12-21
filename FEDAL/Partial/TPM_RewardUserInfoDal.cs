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
   
    public partial class TPM_RewardUserInfoDal : BaseDal<TPM_RewardUserInfo>, ITPM_RewardUserInfoDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                if(ht["IsStatistic"].SafeToString()=="0")
                {
                     str.Append(@"select a.*,b.Name,m.Major_Name,u.Name as CreateName,r.Score as UnitScore,l.name as GName
                                from TPM_RewardUserInfo a 
                                inner join UserInfo b on a.UserNo=b.UniqueNo 
                                left join Major m on b.Major_ID=m.Id 
                                left join UserInfo u on a.CreateUID=u.UniqueNo 
                                left join TPM_AcheiveRewardInfo ri on ri.Id=a.RIId and ri.IsDelete=0                               
                                left join TPM_AcheiveLevel l on ri.gpid=l.Id  
                                left join TPM_RewardInfo r on r.Id=ri.Rid
                                where a.IsDelete=0");
                }else if(ht["IsStatistic"].SafeToString() == "1") //统计
                {
                    if (ht.ContainsKey("AllScore"))
                    {
                        str.Append("select isnull(sum(a.Score),0) as AllScore ");
                    }else
                    {
                        str.Append("select a.UserNo,b.Name,m.Major_Name,isnull(sum(a.Score),0) as SumScore");
                    }
                    str.Append(@" from TPM_RewardUserInfo a 
                            inner join UserInfo b on a.UserNo=b.UniqueNo 
                            left join Major m on b.Major_ID=m.Id 
                            left join TPM_AcheiveRewardInfo ri on ri.Id=a.RIId and ri.IsDelete=0
                            where a.IsDelete=0 and a.RIId!=0  
                        ");
                }else
                {
                    str.Append(@"select a.*,b.Name,m.Major_Name,u.Name as CreateName,ri.Year,r.Score as UnitScore,l.name as GName
                                ,al.Name as GidName,uu.Name as ResponsName,case when ri.GPid=6 then bk.Name else ri.Name end as AchiveName
                                from TPM_RewardUserInfo a 
                                inner join UserInfo b on a.UserNo=b.UniqueNo 
                                left join Major m on b.Major_ID=m.Id 
                                left join UserInfo u on a.CreateUID=u.UniqueNo 
                                left join TPM_AcheiveRewardInfo ri on ri.Id=a.RIId and ri.IsDelete=0
                                left join UserInfo uu on ri.ResponsMan=uu.UniqueNo
                                left join TPM_AcheiveLevel l on ri.gpid=l.Id 
                                left join TPM_AcheiveLevel al on al.Id=ri.Gid 
                                left join TPM_RewardInfo r on r.Id=ri.Rid
                                left join TPM_BookStory bk on ri.bookid=bk.id where a.IsDelete=0");
                }
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                {
                    str.Append(" and a.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].SafeToString()));
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and b.Name like '%" + ht["Name"].SafeToString() + "%'");
                }
                if (ht.ContainsKey("RIId") && !string.IsNullOrEmpty(ht["RIId"].SafeToString()))
                {
                    str.Append(" and a.RIId=@RIId ");
                    pms.Add(new SqlParameter("@RIId", ht["RIId"].SafeToString()));
                }
                if (ht.ContainsKey("Static_RIId") && !string.IsNullOrEmpty(ht["Static_RIId"].SafeToString()))
                {
                    str.Append(" and a.RIId!=0 ");                    
                }
                if (ht.ContainsKey("BookId") && !string.IsNullOrEmpty(ht["BookId"].SafeToString()))
                {
                    str.Append(" and a.BookId=@BookId ");
                    pms.Add(new SqlParameter("@BookId", ht["BookId"].SafeToString()));
                }
                if (ht.ContainsKey("DepartMent") && !string.IsNullOrEmpty(ht["DepartMent"].SafeToString()))
                {
                    str.Append(" and b.Major_ID=@Major_ID ");
                    pms.Add(new SqlParameter("@Major_ID", ht["DepartMent"].SafeToString()));
                }
                if (ht.ContainsKey("BeginTime") && !string.IsNullOrEmpty(ht["BeginTime"].SafeToString()))
                {
                    str.Append(" and convert(varchar(4),ri.Year,21)>=convert(varchar(4),@BeginTime,21) ");
                    pms.Add(new SqlParameter("@BeginTime", ht["BeginTime"].SafeToString()));
                }
                if (ht.ContainsKey("EndTime") && !string.IsNullOrEmpty(ht["EndTime"].SafeToString()))
                {
                    str.Append(" and convert(varchar(4),ri.Year,21)<=convert(varchar(4),@EndTime,21)");
                    pms.Add(new SqlParameter("@EndTime", ht["EndTime"].SafeToString()));
                }
                if (ht.ContainsKey("UserNos") && !string.IsNullOrEmpty(ht["UserNos"].SafeToString()))
                {
                    StringBuilder strFirst = new StringBuilder();
                    string[] UserNos = ht["UserNos"].SafeToString().Split(',');
                    for (int i = 0; i < UserNos.Length; i++)
                    {
                        strFirst.Append("@UserNo" + i + ",");
                        pms.Add(new SqlParameter("@UserNo" + i, UserNos[i]));
                    }
                    str.Append(string.Format(" and a.UserNo in({0})", strFirst.ToString().TrimEnd(',')));
                }
                if (ht.ContainsKey("Status_Com") && !string.IsNullOrEmpty(ht["Status_Com"].SafeToString()))
                {
                    str.Append(" and ri.Status" + ht["Status_Com"].SafeToString());
                }
                string orderBy = "ULevel asc,Sort";
                if (ht.ContainsKey("AllScore"))
                {
                    orderBy = "AllScore";
                }
                else if (ht["IsStatistic"].SafeToString() == "1")
                {                   
                    str.Append(" group by a.UserNo,b.Name,m.Major_Name ");
                    orderBy = "SumScore desc,Major_Name";
                }               
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, orderBy, StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #region 新增用户
        /// <summary>
        /// 新增-修改 业绩等级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string TPM_RewardUserAdd(TPM_RewardUserInfo model)
        {
            SqlParameter[] param = {
                    new SqlParameter("@UserNo", model.UserNo),
                    new SqlParameter("@RIId", model.RIId),
                    new SqlParameter("@BookId", model.BookId),
                    new SqlParameter("@DepartMent", model.DepartMent.SafeToString()),
                    new SqlParameter("@Score", model.Score),
                    new SqlParameter("@Award",model.Award),
                    new SqlParameter("@Sort", model.Sort),
                    new SqlParameter("@ULevel", model.ULevel),
                    new SqlParameter("@WordNum", model.WordNum),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@Id",model.Id)

            };
            object obj = SQLHelp.ExecuteScalar("TPM_RewardUserAdd", CommandType.StoredProcedure, param);
            return obj.ToString();
        }
        #endregion

        #region 批量修改作者信息
        public int Edit_RewardUserInfo(List<TPM_RewardUserInfo> items, int BookId)
        {
            int result = 1;
            List<SqlParameter> up_pms = new List<SqlParameter>(), pms = new List<SqlParameter>();
            up_pms.Add(new SqlParameter("@BookId", BookId));
            SQLHelp.ExecuteNonQuery("update TPM_RewardUserInfo set IsDelete=1 where BookId=@BookId ", CommandType.Text, up_pms.ToArray());
            string str = "";
            if (items.Count() > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    TPM_RewardUserInfo item = items[i];
                    str += "update TPM_RewardUserInfo set ULevel=@ULevel" + i + ",Sort=@Sort" + i+ ",WordNum=@WordNum" + i+ ",EditUID=@EditUID" + i + ",IsDelete=0 where Id=@Id" + i + ";";
                    pms.Add(new SqlParameter("@Id" + i, item.Id));
                    pms.Add(new SqlParameter("@ULevel" + i, item.ULevel));
                    pms.Add(new SqlParameter("@Sort" + i, item.Sort));
                    pms.Add(new SqlParameter("@WordNum" + i, item.WordNum));
                    pms.Add(new SqlParameter("@EditUID" + i, item.EditUID));
                }
                result = SQLHelp.ExecuteNonQuery(str, CommandType.Text, pms.ToArray());
            }
            return result;
        }
        #endregion

        #region 批量修改成员
        public int Edit_AcheiveMember(List<TPM_RewardUserInfo> items, int riid, int bookId)
        {
            int result = 1;
            List<SqlParameter> up_pms = new List<SqlParameter>(), pms = new List<SqlParameter>();
            up_pms.Add(new SqlParameter("@RIId", riid));
            SQLHelp.ExecuteNonQuery("update TPM_RewardUserInfo set IsDelete=1 where RIId=@RIId ", CommandType.Text, up_pms.ToArray());
            string str = "";
            if (bookId == 0) //非教材建设类
            {
                if (items.Count() > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        TPM_RewardUserInfo item = items[i];
                        str += "update TPM_RewardUserInfo set Score=@Score" + i + ",Sort=@Sort" + i + ",EditUID=@EditUID" + i + ",IsDelete=0 where Id=@Id" + i + ";";
                        pms.Add(new SqlParameter("@Id" + i, item.Id));
                        if (item.Score == null)
                        {
                            pms.Add(new SqlParameter("@Score" + i, DBNull.Value));
                        }
                        else
                        {
                            pms.Add(new SqlParameter("@Score" + i, item.Score));
                        }
                        pms.Add(new SqlParameter("@Sort" + i, item.Sort));
                        pms.Add(new SqlParameter("@EditUID" + i, item.EditUID));
                    }
                    result = SQLHelp.ExecuteNonQuery(str, CommandType.Text, pms.ToArray());
                }
            }
            else //教材建设类
            {
                str = @"insert into TPM_RewardUserInfo(RIId,UserNo,Score,Award,ULevel,Sort,DepartMent,WordNum,CreateUID)
                    select @RIId,UserNo,Score,Award,ULevel,Sort,DepartMent,WordNum,CreateUID from TPM_RewardUserInfo where BookId=@BookId ";
                pms.Add(new SqlParameter("@RIId", riid));
                pms.Add(new SqlParameter("@BookId", bookId));
                result = SQLHelp.ExecuteNonQuery(str, CommandType.Text, pms.ToArray());
            }            
            return result;
        }
        #endregion
    }

}
