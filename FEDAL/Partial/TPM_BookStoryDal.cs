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
    public partial class TPM_BookStoryDal : BaseDal<TPM_BookStory>, ITPM_BookStoryDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            {
                RowCount = 0;
                List<SqlParameter> pms = new List<SqlParameter>();
                DataTable dt = new DataTable();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append(@"select a.*,u.Name  as EditName,maj.Major_Name as MEditorDepart_Name 
                                ,(select count(1) from TPM_AcheiveRewardInfo where IsDelete=0 and Status>=3 and BookId=a.Id)PrizeCount ");
                    int StartIndex = 0;
                    int EndIndex = 0;
                    if (ht.ContainsKey("LoginMajor_ID") && !string.IsNullOrEmpty(ht["LoginMajor_ID"].SafeToString()))
                    {
                        str.Append(@",(select count(1) from TPM_RewardUserInfo ruser left join UserInfo u on  ruser.UserNo = u.UniqueNo
             where ruser.IsDelete=0 and ruser.BookId!=0 and ruser.BookId=a.Id and u.Major_ID=@LoginMajor_ID) MajorCount ");
                        pms.Add(new SqlParameter("@LoginMajor_ID", ht["LoginMajor_ID"].SafeToString()));
                    }
                    str.Append(@" from TPM_BookStory a 
                                left join UserInfo u on  a.MEditor=u.UniqueNo 
                                left join Major maj on a.MEditorDepart=maj.Id
                                where a.IsDelete=0 ");
                    if (ht.ContainsKey("Status") && !string.IsNullOrEmpty(ht["Status"].SafeToString()))
                    {
                        str.Append(" and a.Status in (" + ht["Status"].SafeToString() + ")");
                    }
                    if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].SafeToString()))
                    {
                        str.Append(" and a.Id=@Id ");
                        pms.Add(new SqlParameter("@Id", ht["Id"].SafeToString()));
                    }
                    if (ht.ContainsKey("BookType") && !string.IsNullOrEmpty(ht["BookType"].SafeToString()))
                    {
                        str.Append(" and BookType=@BookType ");
                        pms.Add(new SqlParameter("@BookType", ht["BookType"].SafeToString()));
                    }
                    if (ht.ContainsKey("IsPlanBook") && !string.IsNullOrEmpty(ht["IsPlanBook"].SafeToString()))
                    {
                        str.Append(" and a.IsPlanBook=@IsPlanBook ");
                        pms.Add(new SqlParameter("@IsPlanBook", ht["IsPlanBook"].SafeToString()));
                    }                    
                    if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                    {
                        str.Append(" and (a.ISBN like '%" + ht["Name"].SafeToString() + "%' or a.Name like '%" + ht["Name"].SafeToString() + "%' or u.Name like '%" + ht["Name"].SafeToString() + "%')");
                    }
                    if (ht.ContainsKey("Author_SelfNo") && !string.IsNullOrEmpty(ht["Author_SelfNo"].SafeToString()))//自己创建或者是主编、参编
                    {
                        str.Append(@" and ((a.CreateUID=@Author_SelfNo and a.Status in(0,1)) or ( 
a.Id in(select distinct BookId from TPM_RewardUserInfo where IsDelete=0 and UserNo=@Author_SelfNo) and a.Status in(2,3)))");
                        pms.Add(new SqlParameter("@Author_SelfNo", ht["Author_SelfNo"].SafeToString()));
                    }
                    if (ht.ContainsKey("AuthorNo") && !string.IsNullOrEmpty(ht["AuthorNo"].SafeToString()))//自己是主编、参编
                    {
                        str.Append(@" and a.Id in(select distinct BookId from TPM_RewardUserInfo where IsDelete=0 and UserNo=@AuthorNo) ");
                        pms.Add(new SqlParameter("@AuthorNo", ht["AuthorNo"].SafeToString()));
                    }
                    if (ht.ContainsKey("Major_ID") && !string.IsNullOrEmpty(ht["Major_ID"].SafeToString()))
                    {
                        str.Append(@" and a.Id in(select distinct ruser.BookId from TPM_RewardUserInfo ruser
                              left join UserInfo u on  ruser.UserNo = u.UniqueNo
                              where ruser.IsDelete = 0 and ruser.BookId != 0 and u.Major_ID=@Major_ID)");
                        pms.Add(new SqlParameter("@Major_ID", ht["Major_ID"].SafeToString()));
                    }
                    if (ht.ContainsKey("IdentifyCol") && !string.IsNullOrEmpty(ht["IdentifyCol"].SafeToString()))
                    {
                        str.Append(" and a.IdentifyCol=@IdentifyCol ");
                        pms.Add(new SqlParameter("@IdentifyCol", ht["IdentifyCol"].SafeToString()));
                    }                    
                    if (IsPage)
                    {
                        StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                        EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                    }
                    dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex,EndIndex, IsPage, pms.ToArray(), out RowCount);
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
