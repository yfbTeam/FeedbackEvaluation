using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEUtility;
using FEModel;
using System.Configuration;
namespace FEIDAL
{

	/// </summary>
	///	
	/// </summary>
    public interface ISys_MessageDal: IBaseDal<Sys_Message>
    {
		
    }

	/// </summary>
	///	角色数据处理接口类1
	/// </summary>
    public interface ISys_RoleDal: IBaseDal<Sys_Role>
    {
		
    }

	/// </summary>
	///	角色菜单关系数据处理接口类2
	/// </summary>
    public interface ISys_RoleOfMenuDal: IBaseDal<Sys_RoleOfMenu>
    {
		
    }

	/// </summary>
	///	系统账号数据处理接口类3
	/// </summary>
    public interface ISys_SystemInfoDal: IBaseDal<Sys_SystemInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAssetManagementDal: IBaseDal<AssetManagement>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IClass_StudentInfoDal: IBaseDal<Class_StudentInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardBatchDal: IBaseDal<TPM_RewardBatch>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_Table_HeaderDal: IBaseDal<Eva_Table_Header>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IClassInfoDal: IBaseDal<ClassInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITMP_RewardRankDal: IBaseDal<TMP_RewardRank>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AcheiveLevelDal: IBaseDal<TPM_AcheiveLevel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AcheiveRewardInfoDal: IBaseDal<TPM_AcheiveRewardInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_DistributionDal: IBaseDal<Eva_Distribution>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_QuestionAnswerDal: IBaseDal<Eva_QuestionAnswer>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AllotRewardDal: IBaseDal<TPM_AllotReward>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_QuestionAnswer_DetailDal: IBaseDal<Eva_QuestionAnswer_Detail>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_RegularDal: IBaseDal<Eva_Regular>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICourseRelDal: IBaseDal<CourseRel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_AuditRewardDal: IBaseDal<TPM_AuditReward>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TaskDal: IBaseDal<Eva_Task>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISys_DocumentDal: IBaseDal<Sys_Document>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TaskAnswerDal: IBaseDal<Eva_TaskAnswer>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TeacherAnswerDal: IBaseDal<Eva_TeacherAnswer>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IUserInfoDal: IBaseDal<UserInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_ModifyRecordDal: IBaseDal<TPM_ModifyRecord>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardInfoDal: IBaseDal<TPM_RewardInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICourseRoomDal: IBaseDal<CourseRoom>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardLevelDal: IBaseDal<TPM_RewardLevel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICourseDal: IBaseDal<Course>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardUserInfoDal: IBaseDal<TPM_RewardUserInfo>
    {
		
    }

	/// </summary>
	///	菜单信息数据处理接口类4
	/// </summary>
    public interface ISys_MenuInfoDal: IBaseDal<Sys_MenuInfo>
    {
		
    }

	/// </summary>
	///	角色用户关系数据处理接口类5
	/// </summary>
    public interface ISys_RoleOfUserDal: IBaseDal<Sys_RoleOfUser>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_RewardEditionDal: IBaseDal<TPM_RewardEdition>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISys_DictionaryDal: IBaseDal<Sys_Dictionary>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IMajorDal: IBaseDal<Major>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IStudySectionDal: IBaseDal<StudySection>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IGradeInfoDal: IBaseDal<GradeInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IStudentDal: IBaseDal<Student>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISubMajorDal: IBaseDal<SubMajor>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IIndicatorDal: IBaseDal<Indicator>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITPM_BookStoryDal: IBaseDal<TPM_BookStory>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TableDetailDal: IBaseDal<Eva_TableDetail>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IIndicatorTypeDal: IBaseDal<IndicatorType>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ILinkManInfoDal: IBaseDal<LinkManInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_TableDal: IBaseDal<Eva_Table>
    {
		
    }

	/// </summary>
	///	菜单按钮类型数据处理接口类6
	/// </summary>
    public interface ISys_ButtonTypeDal: IBaseDal<Sys_ButtonType>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ITeacherDal: IBaseDal<Teacher>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISys_LetterDal: IBaseDal<Sys_Letter>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_CourseType_TableDal: IBaseDal<Eva_CourseType_Table>
    {
		
    }

	/// </summary>
	///	系统日志数据处理接口类7
	/// </summary>
    public interface ISys_LogInfoDal: IBaseDal<Sys_LogInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEva_Table_Header_CustomDal: IBaseDal<Eva_Table_Header_Custom>
    {
		
    }
}