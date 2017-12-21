  
using System;
namespace FEModel
{    

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_Message
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Receiver { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Receiver_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Href { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsSend { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Receiver_Email { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Timing { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FilePath { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_Message))
            {
                Sys_Message _obj = obj as Sys_Message;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	角色实体类
	/// </summary>
	[Serializable]
    public partial class Sys_Role
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		///角色名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_Role))
            {
                Sys_Role _obj = obj as Sys_Role;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	角色菜单关系实体类
	/// </summary>
	[Serializable]
    public partial class Sys_RoleOfMenu
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Role_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Menu_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_RoleOfMenu))
            {
                Sys_RoleOfMenu _obj = obj as Sys_RoleOfMenu;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class UserInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? UserType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Birthday { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ClearPassword { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string HeadPic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Pic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string IDCard { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(UserInfo))
            {
                UserInfo _obj = obj as UserInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	系统账号实体类
	/// </summary>
	[Serializable]
    public partial class Sys_SystemInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///系统名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///账号 
		/// </summary>
		public string AccountNo { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_SystemInfo))
            {
                Sys_SystemInfo _obj = obj as Sys_SystemInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class AssetManagement
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string AssetModel { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Number { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string AdressName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UseUnits { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WarrantyDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Principal { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AcquisitionDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SourceEquipment { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Creator { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Editor { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime { get; set; }
		/// <summary>
		///0 正常;1 删除;2归档 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		///0 未使用;1 使用 
		/// </summary>
		public Byte? UseStatus { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(AssetManagement))
            {
                AssetManagement _obj = obj as AssetManagement;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Class_StudentInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Class_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DisPlayName { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Class_StudentInfo))
            {
                Class_StudentInfo _obj = obj as Class_StudentInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_RewardBatch
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 奖项Id 
		/// </summary>
		public int? Reward_Id { get; set; }
		/// <summary>
		///金额 
		/// </summary>
		public decimal? Money { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_RewardBatch))
            {
                TPM_RewardBatch _obj = obj as TPM_RewardBatch;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_Table_Header
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Table_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name_Key { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name_Value { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Custom_Code { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_Table_Header))
            {
                Eva_Table_Header _obj = obj as Eva_Table_Header;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ClassInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ClassNO { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Class_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Grade_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StudySection_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(ClassInfo))
            {
                ClassInfo _obj = obj as ClassInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TMP_RewardRank
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? RId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? RankNum { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TMP_RewardRank))
            {
                TMP_RewardRank _obj = obj as TMP_RewardRank;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_AcheiveLevel
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///0:普通类型2：等级递减3.教材建设4.教师指导类3.教学名师类 
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? AwardSwich { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_AcheiveLevel))
            {
                TPM_AcheiveLevel _obj = obj as TPM_AcheiveLevel;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_AcheiveRewardInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Gid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? GPid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? BookId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeaUNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Lid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Rid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Year { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ResponsMan { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DepartMent { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileEdionNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileNames { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DefindDepart { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DefindDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileInfo { get; set; }
		/// <summary>
		///0:待提交；1信息待审核；2信息不通过；3分数待分配；4分数待提交；5分数待审核；6分数不通过；7审核通过；8奖金待分配；9奖金待提交；10奖金待审核；11奖金不通过；12审核通过 
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_AcheiveRewardInfo))
            {
                TPM_AcheiveRewardInfo _obj = obj as TPM_AcheiveRewardInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_Distribution
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Evaluate_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CousrseType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Table_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Eva_Role { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_Distribution))
            {
                Eva_Distribution _obj = obj as Eva_Distribution;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_QuestionAnswer
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Eva_Distribution_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StudentUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseTypeId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SectionId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_QuestionAnswer))
            {
                Eva_QuestionAnswer _obj = obj as Eva_QuestionAnswer;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_AllotReward
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///审核钱的Id 
		/// </summary>
		public int? Audit_Id { get; set; }
		/// <summary>
		///奖项成员Id 
		/// </summary>
		public int? RewardUser_Id { get; set; }
		/// <summary>
		///分配的金额 
		/// </summary>
		public decimal? AllotMoney { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_AllotReward))
            {
                TPM_AllotReward _obj = obj as TPM_AllotReward;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_QuestionAnswer_Detail
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? TableDetail_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Eva_TaskAnswer_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Selection { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Answer { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_QuestionAnswer_Detail))
            {
                Eva_QuestionAnswer_Detail _obj = obj as Eva_QuestionAnswer_Detail;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class CourseRel
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Course_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StudySection_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(CourseRel))
            {
                CourseRel _obj = obj as CourseRel;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_AuditReward
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///业绩Id 
		/// </summary>
		public int? Acheive_Id { get; set; }
		/// <summary>
		///奖金批次Id 
		/// </summary>
		public int? RewardBatch_Id { get; set; }
		/// <summary>
		///审核状态 0待提交；1待审核；2审核不通过；3审核通过 
		/// </summary>
		public Byte? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_AuditReward))
            {
                TPM_AuditReward _obj = obj as TPM_AuditReward;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_Task
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Table_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Class_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseRoom_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Range { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsScore { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? FullScore { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsSued { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsPublish { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Equal_Flg { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_Task))
            {
                Eva_Task _obj = obj as Eva_Task;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_Document
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///0 业绩扫描文件；1版本相关文件；2教材扫描文件；3业绩分数分配附件；4奖金分配附件（TPM_AuditReward 的Id） 
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_Document))
            {
                Sys_Document _obj = obj as Sys_Document;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_TaskAnswer
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Task_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? TableDetail_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Answer { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_TaskAnswer))
            {
                Eva_TaskAnswer _obj = obj as Eva_TaskAnswer;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_TeacherAnswer
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Eva_Distribution_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Indicator_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Answer { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_TeacherAnswer))
            {
                Eva_TeacherAnswer _obj = obj as Eva_TeacherAnswer;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_ModifyRecord
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型 0 分数；1奖金 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///分数-空；奖金- 奖金表（TPM_RewardBatch）id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///业绩Id 
		/// </summary>
		public int? Acheive_Id { get; set; }
		/// <summary>
		///修改内容 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///修改原因 
		/// </summary>
		public string EditReason { get; set; }
		/// <summary>
		///被修改人 
		/// </summary>
		public string ModifyUID { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_ModifyRecord))
            {
                TPM_ModifyRecord _obj = obj as TPM_ModifyRecord;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_RewardInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? LID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Award { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? ScoreType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? AddAward { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_RewardInfo))
            {
                TPM_RewardInfo _obj = obj as TPM_RewardInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class CourseRoom
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StudySection_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Year { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Season { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string RoomDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Coures_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CouresName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseProperty { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string GradeID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string GradeName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Calss_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DepartmentName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherPropertyID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherProperty { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherJobTitle { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeacherSubDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StudentCount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(CourseRoom))
            {
                CourseRoom _obj = obj as CourseRoom;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_RewardLevel
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? EID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_RewardLevel))
            {
                TPM_RewardLevel _obj = obj as TPM_RewardLevel;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Course
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DepartMentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DepartmentName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseProperty { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseTypeName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Course))
            {
                Course _obj = obj as Course;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_RewardUserInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? RIId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? BookId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UserNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? Award { get; set; }
		/// <summary>
		///0独著  1主编  2参编 3其他人员 
		/// </summary>
		public Byte? ULevel { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DepartMent { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? WordNum { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_RewardUserInfo))
            {
                TPM_RewardUserInfo _obj = obj as TPM_RewardUserInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	菜单信息实体类
	/// </summary>
	[Serializable]
    public partial class Sys_MenuInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///菜单名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///父级Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///菜单Url 
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string PageTitle { get; set; }
		/// <summary>
		///菜单描述 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///是否菜单 0菜单；1按钮 
		/// </summary>
		public Byte? IsMenu { get; set; }
		/// <summary>
		///是否显示菜单 
		/// </summary>
		public Byte? IsShow { get; set; }
		/// <summary>
		///排序 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		///菜单编码 
		/// </summary>
		public string MenuCode { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string IconClass { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Navicate_Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_MenuInfo))
            {
                Sys_MenuInfo _obj = obj as Sys_MenuInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	角色用户关系实体类
	/// </summary>
	[Serializable]
    public partial class Sys_RoleOfUser
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Role_Id { get; set; }
		/// <summary>
		///用户唯一值 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_RoleOfUser))
            {
                Sys_RoleOfUser _obj = obj as Sys_RoleOfUser;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_RewardEdition
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? LID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsMoneyAllot { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_RewardEdition))
            {
                TPM_RewardEdition _obj = obj as TPM_RewardEdition;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_Dictionary
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SectionId { get; set; }
		/// <summary>
		///父级Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///关键 
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		///值 
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		///类型 
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_Dictionary))
            {
                Sys_Dictionary _obj = obj as Sys_Dictionary;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Major
    {

		/// <summary>
		/// 
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Major))
            {
                Major _obj = obj as Major;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_Regular
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		///查看时间 0 始终（默认）；1设置 
		/// </summary>
		public Byte? LookType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DepartmentIDs { get; set; }
		/// <summary>
		///查看开始时间 
		/// </summary>
		public DateTime? Look_StartTime { get; set; }
		/// <summary>
		///查看结束时间 
		/// </summary>
		public DateTime? Look_EndTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Section_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MaxPercent { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MinPercent { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? TableID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_Regular))
            {
                Eva_Regular _obj = obj as Eva_Regular;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class StudySection
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DisPlayName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Academic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Semester { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(StudySection))
            {
                StudySection _obj = obj as StudySection;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class GradeInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Grade_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(GradeInfo))
            {
                GradeInfo _obj = obj as GradeInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Student
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StuNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_Id { get; set; }
		/// <summary>
		///系名称 
		/// </summary>
		public string Departent_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MajorID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MajorName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ClassNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ClassName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Student))
            {
                Student _obj = obj as Student;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class SubMajor
    {

		/// <summary>
		/// 
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string PID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string PName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(SubMajor))
            {
                SubMajor _obj = obj as SubMajor;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Indicator
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? IndicatorType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? QuesType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionA { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionB { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionC { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionD { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionE { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionF { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? UseTimes { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Indicator))
            {
                Indicator _obj = obj as Indicator;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class TPM_BookStory
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///教材名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///教材类型 1立项教材；2已出版教材 
		/// </summary>
		public Byte? BookType { get; set; }
		/// <summary>
		///分册情况  1单册；2多册 
		/// </summary>
		public Byte? IsOneVolum { get; set; }
		/// <summary>
		///是否独著 0否；1是 
		/// </summary>
		public Byte? IsOneAuthor { get; set; }
		/// <summary>
		///审核状态  0未提交；1待审核；2审核不通过；3审核通过 
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		///主编名称 
		/// </summary>
		public string MEditor { get; set; }
		/// <summary>
		///书号/ ISBN号 
		/// </summary>
		public string ISBN { get; set; }
		/// <summary>
		///主编单位 
		/// </summary>
		public string MEditorDepart { get; set; }
		/// <summary>
		///使用对象 
		/// </summary>
		public string UseObj { get; set; }
		/// <summary>
		///出版社 
		/// </summary>
		public string Publisher { get; set; }
		/// <summary>
		///国家规划教材 0否；1是 
		/// </summary>
		public Byte? IsPlanBook { get; set; }
		/// <summary>
		///立项类型 1北京市精品教材立项；2国家级精品教材立项 
		/// </summary>
		public Byte? ProjectType { get; set; }
		/// <summary>
		///出版时间 
		/// </summary>
		public DateTime? PublisthTime { get; set; }
		/// <summary>
		///版次 
		/// </summary>
		public int? EditionNo { get; set; }
		/// <summary>
		///扫描文件 
		/// </summary>
		public string FileInfo { get; set; }
		/// <summary>
		///丛书名称 
		/// </summary>
		public string SeriesBookName { get; set; }
		/// <summary>
		///代表ISBN号 
		/// </summary>
		public string MainISBN { get; set; }
		/// <summary>
		///本丛书本书 
		/// </summary>
		public int? SeriesBookNum { get; set; }
		/// <summary>
		///标识列 立项教材（0 未出版；1已出版）   已出版教材（立项教材id） 
		/// </summary>
		public int? IdentifyCol { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(TPM_BookStory))
            {
                TPM_BookStory _obj = obj as TPM_BookStory;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_TableDetail
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Root { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Eva_table_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Indicator_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? QuesType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? IndicatorType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string IndicatorType_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionA { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionB { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionC { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionD { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionE { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OptionF { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? OptionA_S { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? OptionB_S { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? OptionC_S { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? OptionD_S { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? OptionE_S { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? OptionF_S { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_TableDetail))
            {
                Eva_TableDetail _obj = obj as Eva_TableDetail;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class IndicatorType
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Parent_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? P_Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(IndicatorType))
            {
                IndicatorType _obj = obj as IndicatorType;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class LinkManInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UserUniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LinkManUniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LinkManName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LinkManDepartMent { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LinkManEmail { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LinkManRemarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(LinkManInfo))
            {
                LinkManInfo _obj = obj as LinkManInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_Table
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CousrseType_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Eva_Role { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsScore { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? UseTimes { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_Table))
            {
                Eva_Table _obj = obj as Eva_Table;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	菜单按钮类型实体类
	/// </summary>
	[Serializable]
    public partial class Sys_ButtonType
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///名称 
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		///值 
		/// </summary>
		public string Value { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_ButtonType))
            {
                Sys_ButtonType _obj = obj as Sys_ButtonType;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Teacher
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		///系编号 
		/// </summary>
		public string Departent_Id { get; set; }
		/// <summary>
		///系名称 
		/// </summary>
		public string Departent_Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SubDepartmentName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Major_ID { get; set; }
		/// <summary>
		///院名称 
		/// </summary>
		public string Major_Name { get; set; }
		/// <summary>
		///学历 
		/// </summary>
		public string Education { get; set; }
		/// <summary>
		///学位 
		/// </summary>
		public string Degree { get; set; }
		/// <summary>
		///职称 
		/// </summary>
		public string JobTitle { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday { get; set; }
		/// <summary>
		///教学日期 
		/// </summary>
		public string TeachDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Teacher))
            {
                Teacher _obj = obj as Teacher;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_Letter
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///标题 
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		///内容 
		/// </summary>
		public string Contents { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ReceiveUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ReceiveName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Reply_Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsRead { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_Letter))
            {
                Sys_Letter _obj = obj as Sys_Letter;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_CourseType_Table
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseTypeId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? TableId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StudySection_Id { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_CourseType_Table))
            {
                Eva_CourseType_Table _obj = obj as Eva_CourseType_Table;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	系统日志实体类
	/// </summary>
	[Serializable]
    public partial class Sys_LogInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///用户登录名 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		///当前IP 
		/// </summary>
		public string IP { get; set; }
		/// <summary>
		///操作内容 
		/// </summary>
		public string Operation { get; set; }
		/// <summary>
		///日志类型 
		/// </summary>
		public string LogType { get; set; }
		/// <summary>
		///备注 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Sys_LogInfo))
            {
                Sys_LogInfo _obj = obj as Sys_LogInfo;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Eva_Table_Header_Custom
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Header { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Header_Value { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }

          public override bool Equals(object obj)
        {
            bool result = false;
            if (obj.GetType() == typeof(Eva_Table_Header_Custom))
            {
                Eva_Table_Header_Custom _obj = obj as Eva_Table_Header_Custom;
                if (_obj.Id == this.Id)
                {
                    result = true;
                }
            }
            return result;
        }

	    public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
