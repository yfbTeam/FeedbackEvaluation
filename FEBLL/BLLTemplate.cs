using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEUtility;
using FEDAL;
using FEModel;
using FEIBLL;
namespace FEBLL
{

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_MessageService:BaseService<Sys_Message>,ISys_MessageService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_MessageDal();
        }
    }	

	/// </summary>
	///	角色业务类1
	/// </summary>
    public partial class Sys_RoleService:BaseService<Sys_Role>,ISys_RoleService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_RoleDal();
        }
    }	

	/// </summary>
	///	角色菜单关系业务类2
	/// </summary>
    public partial class Sys_RoleOfMenuService:BaseService<Sys_RoleOfMenu>,ISys_RoleOfMenuService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_RoleOfMenuDal();
        }
    }	

	/// </summary>
	///	系统账号业务类3
	/// </summary>
    public partial class Sys_SystemInfoService:BaseService<Sys_SystemInfo>,ISys_SystemInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_SystemInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class AssetManagementService:BaseService<AssetManagement>,IAssetManagementService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAssetManagementDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Class_StudentInfoService:BaseService<Class_StudentInfo>,IClass_StudentInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetClass_StudentInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_RewardBatchService:BaseService<TPM_RewardBatch>,ITPM_RewardBatchService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_RewardBatchDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_Table_HeaderService:BaseService<Eva_Table_Header>,IEva_Table_HeaderService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_Table_HeaderDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ClassInfoService:BaseService<ClassInfo>,IClassInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetClassInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TMP_RewardRankService:BaseService<TMP_RewardRank>,ITMP_RewardRankService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTMP_RewardRankDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_AcheiveLevelService:BaseService<TPM_AcheiveLevel>,ITPM_AcheiveLevelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_AcheiveLevelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_AcheiveRewardInfoService:BaseService<TPM_AcheiveRewardInfo>,ITPM_AcheiveRewardInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_AcheiveRewardInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_DistributionService:BaseService<Eva_Distribution>,IEva_DistributionService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_DistributionDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_QuestionAnswerService:BaseService<Eva_QuestionAnswer>,IEva_QuestionAnswerService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_QuestionAnswerDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_AllotRewardService:BaseService<TPM_AllotReward>,ITPM_AllotRewardService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_AllotRewardDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_QuestionAnswer_DetailService:BaseService<Eva_QuestionAnswer_Detail>,IEva_QuestionAnswer_DetailService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_QuestionAnswer_DetailDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_RegularService:BaseService<Eva_Regular>,IEva_RegularService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_RegularDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class CourseRelService:BaseService<CourseRel>,ICourseRelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourseRelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_AuditRewardService:BaseService<TPM_AuditReward>,ITPM_AuditRewardService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_AuditRewardDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_TaskService:BaseService<Eva_Task>,IEva_TaskService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_TaskDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_DocumentService:BaseService<Sys_Document>,ISys_DocumentService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_DocumentDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_TaskAnswerService:BaseService<Eva_TaskAnswer>,IEva_TaskAnswerService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_TaskAnswerDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_TeacherAnswerService:BaseService<Eva_TeacherAnswer>,IEva_TeacherAnswerService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_TeacherAnswerDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetUserInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_ModifyRecordService:BaseService<TPM_ModifyRecord>,ITPM_ModifyRecordService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_ModifyRecordDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_RewardInfoService:BaseService<TPM_RewardInfo>,ITPM_RewardInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_RewardInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class CourseRoomService:BaseService<CourseRoom>,ICourseRoomService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourseRoomDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_RewardLevelService:BaseService<TPM_RewardLevel>,ITPM_RewardLevelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_RewardLevelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class CourseService:BaseService<Course>,ICourseService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourseDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_RewardUserInfoService:BaseService<TPM_RewardUserInfo>,ITPM_RewardUserInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_RewardUserInfoDal();
        }
    }	

	/// </summary>
	///	菜单信息业务类4
	/// </summary>
    public partial class Sys_MenuInfoService:BaseService<Sys_MenuInfo>,ISys_MenuInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_MenuInfoDal();
        }
    }	

	/// </summary>
	///	角色用户关系业务类5
	/// </summary>
    public partial class Sys_RoleOfUserService:BaseService<Sys_RoleOfUser>,ISys_RoleOfUserService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_RoleOfUserDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_RewardEditionService:BaseService<TPM_RewardEdition>,ITPM_RewardEditionService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_RewardEditionDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_DictionaryService:BaseService<Sys_Dictionary>,ISys_DictionaryService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_DictionaryDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class MajorService:BaseService<Major>,IMajorService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetMajorDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class StudySectionService:BaseService<StudySection>,IStudySectionService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetStudySectionDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class GradeInfoService:BaseService<GradeInfo>,IGradeInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetGradeInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class StudentService:BaseService<Student>,IStudentService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetStudentDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class SubMajorService:BaseService<SubMajor>,ISubMajorService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSubMajorDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class IndicatorService:BaseService<Indicator>,IIndicatorService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetIndicatorDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TPM_BookStoryService:BaseService<TPM_BookStory>,ITPM_BookStoryService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTPM_BookStoryDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_TableDetailService:BaseService<Eva_TableDetail>,IEva_TableDetailService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_TableDetailDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class IndicatorTypeService:BaseService<IndicatorType>,IIndicatorTypeService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetIndicatorTypeDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class LinkManInfoService:BaseService<LinkManInfo>,ILinkManInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetLinkManInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_TableService:BaseService<Eva_Table>,IEva_TableService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_TableDal();
        }
    }	

	/// </summary>
	///	菜单按钮类型业务类6
	/// </summary>
    public partial class Sys_ButtonTypeService:BaseService<Sys_ButtonType>,ISys_ButtonTypeService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_ButtonTypeDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class TeacherService:BaseService<Teacher>,ITeacherService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetTeacherDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_LetterService:BaseService<Sys_Letter>,ISys_LetterService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_LetterDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_CourseType_TableService:BaseService<Eva_CourseType_Table>,IEva_CourseType_TableService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_CourseType_TableDal();
        }
    }	

	/// </summary>
	///	系统日志业务类7
	/// </summary>
    public partial class Sys_LogInfoService:BaseService<Sys_LogInfo>,ISys_LogInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_LogInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Eva_Table_Header_CustomService:BaseService<Eva_Table_Header_Custom>,IEva_Table_Header_CustomService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEva_Table_Header_CustomDal();
        }
    }	
}