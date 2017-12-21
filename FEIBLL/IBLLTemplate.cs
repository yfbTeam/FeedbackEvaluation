using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEModel;
namespace FEIBLL
{

	/// </summary>
	///	
	/// </summary>
    public interface ISys_MessageService:IBaseService<Sys_Message>
    {

    }	

	/// </summary>
	///	角色业务类1
	/// </summary>
    public interface ISys_RoleService:IBaseService<Sys_Role>
    {

    }	

	/// </summary>
	///	角色菜单关系业务类2
	/// </summary>
    public interface ISys_RoleOfMenuService:IBaseService<Sys_RoleOfMenu>
    {

    }	

	/// </summary>
	///	系统账号业务类3
	/// </summary>
    public interface ISys_SystemInfoService:IBaseService<Sys_SystemInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IAssetManagementService:IBaseService<AssetManagement>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IClass_StudentInfoService:IBaseService<Class_StudentInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardBatchService:IBaseService<TPM_RewardBatch>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_Table_HeaderService:IBaseService<Eva_Table_Header>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IClassInfoService:IBaseService<ClassInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITMP_RewardRankService:IBaseService<TMP_RewardRank>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AcheiveLevelService:IBaseService<TPM_AcheiveLevel>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AcheiveRewardInfoService:IBaseService<TPM_AcheiveRewardInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_DistributionService:IBaseService<Eva_Distribution>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_QuestionAnswerService:IBaseService<Eva_QuestionAnswer>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AllotRewardService:IBaseService<TPM_AllotReward>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_QuestionAnswer_DetailService:IBaseService<Eva_QuestionAnswer_Detail>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_RegularService:IBaseService<Eva_Regular>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ICourseRelService:IBaseService<CourseRel>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AuditRewardService:IBaseService<TPM_AuditReward>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TaskService:IBaseService<Eva_Task>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ISys_DocumentService:IBaseService<Sys_Document>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TaskAnswerService:IBaseService<Eva_TaskAnswer>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TeacherAnswerService:IBaseService<Eva_TeacherAnswer>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IUserInfoService:IBaseService<UserInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_ModifyRecordService:IBaseService<TPM_ModifyRecord>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardInfoService:IBaseService<TPM_RewardInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ICourseRoomService:IBaseService<CourseRoom>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardLevelService:IBaseService<TPM_RewardLevel>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ICourseService:IBaseService<Course>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardUserInfoService:IBaseService<TPM_RewardUserInfo>
    {

    }	

	/// </summary>
	///	菜单信息业务类4
	/// </summary>
    public interface ISys_MenuInfoService:IBaseService<Sys_MenuInfo>
    {

    }	

	/// </summary>
	///	角色用户关系业务类5
	/// </summary>
    public interface ISys_RoleOfUserService:IBaseService<Sys_RoleOfUser>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardEditionService:IBaseService<TPM_RewardEdition>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ISys_DictionaryService:IBaseService<Sys_Dictionary>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IMajorService:IBaseService<Major>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IStudySectionService:IBaseService<StudySection>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IGradeInfoService:IBaseService<GradeInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IStudentService:IBaseService<Student>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ISubMajorService:IBaseService<SubMajor>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IIndicatorService:IBaseService<Indicator>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_BookStoryService:IBaseService<TPM_BookStory>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TableDetailService:IBaseService<Eva_TableDetail>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IIndicatorTypeService:IBaseService<IndicatorType>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ILinkManInfoService:IBaseService<LinkManInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TableService:IBaseService<Eva_Table>
    {

    }	

	/// </summary>
	///	菜单按钮类型业务类6
	/// </summary>
    public interface ISys_ButtonTypeService:IBaseService<Sys_ButtonType>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ITeacherService:IBaseService<Teacher>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ISys_LetterService:IBaseService<Sys_Letter>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_CourseType_TableService:IBaseService<Eva_CourseType_Table>
    {

    }	

	/// </summary>
	///	系统日志业务类7
	/// </summary>
    public interface ISys_LogInfoService:IBaseService<Sys_LogInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEva_Table_Header_CustomService:IBaseService<Eva_Table_Header_Custom>
    {

    }	
}