using FEIDAL;
using FEModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FEDAL
{


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Sys_MessageDal:BaseDal<Sys_Message>,ISys_MessageDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_MessageDal GetSys_MessageDal()
            {
                return new Sys_MessageDal();
            }
	    }


	     /// </summary>
	     ///	角色数据处理类1
	     /// </summary>
		 public partial class Sys_RoleDal:BaseDal<Sys_Role>,ISys_RoleDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_RoleDal GetSys_RoleDal()
            {
                return new Sys_RoleDal();
            }
	    }


	     /// </summary>
	     ///	角色菜单关系数据处理类2
	     /// </summary>
		 public partial class Sys_RoleOfMenuDal:BaseDal<Sys_RoleOfMenu>,ISys_RoleOfMenuDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_RoleOfMenuDal GetSys_RoleOfMenuDal()
            {
                return new Sys_RoleOfMenuDal();
            }
	    }


	     /// </summary>
	     ///	系统账号数据处理类3
	     /// </summary>
		 public partial class Sys_SystemInfoDal:BaseDal<Sys_SystemInfo>,ISys_SystemInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_SystemInfoDal GetSys_SystemInfoDal()
            {
                return new Sys_SystemInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class AssetManagementDal:BaseDal<AssetManagement>,IAssetManagementDal
         {


         }	

        public partial class DalFactory
        {
            public static IAssetManagementDal GetAssetManagementDal()
            {
                return new AssetManagementDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Class_StudentInfoDal:BaseDal<Class_StudentInfo>,IClass_StudentInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static IClass_StudentInfoDal GetClass_StudentInfoDal()
            {
                return new Class_StudentInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_RewardBatchDal:BaseDal<TPM_RewardBatch>,ITPM_RewardBatchDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_RewardBatchDal GetTPM_RewardBatchDal()
            {
                return new TPM_RewardBatchDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_Table_HeaderDal:BaseDal<Eva_Table_Header>,IEva_Table_HeaderDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_Table_HeaderDal GetEva_Table_HeaderDal()
            {
                return new Eva_Table_HeaderDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class ClassInfoDal:BaseDal<ClassInfo>,IClassInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static IClassInfoDal GetClassInfoDal()
            {
                return new ClassInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TMP_RewardRankDal:BaseDal<TMP_RewardRank>,ITMP_RewardRankDal
         {


         }	

        public partial class DalFactory
        {
            public static ITMP_RewardRankDal GetTMP_RewardRankDal()
            {
                return new TMP_RewardRankDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_AcheiveLevelDal:BaseDal<TPM_AcheiveLevel>,ITPM_AcheiveLevelDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_AcheiveLevelDal GetTPM_AcheiveLevelDal()
            {
                return new TPM_AcheiveLevelDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_AcheiveRewardInfoDal:BaseDal<TPM_AcheiveRewardInfo>,ITPM_AcheiveRewardInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_AcheiveRewardInfoDal GetTPM_AcheiveRewardInfoDal()
            {
                return new TPM_AcheiveRewardInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_DistributionDal:BaseDal<Eva_Distribution>,IEva_DistributionDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_DistributionDal GetEva_DistributionDal()
            {
                return new Eva_DistributionDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_QuestionAnswerDal:BaseDal<Eva_QuestionAnswer>,IEva_QuestionAnswerDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_QuestionAnswerDal GetEva_QuestionAnswerDal()
            {
                return new Eva_QuestionAnswerDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_AllotRewardDal:BaseDal<TPM_AllotReward>,ITPM_AllotRewardDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_AllotRewardDal GetTPM_AllotRewardDal()
            {
                return new TPM_AllotRewardDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_QuestionAnswer_DetailDal:BaseDal<Eva_QuestionAnswer_Detail>,IEva_QuestionAnswer_DetailDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_QuestionAnswer_DetailDal GetEva_QuestionAnswer_DetailDal()
            {
                return new Eva_QuestionAnswer_DetailDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_RegularDal:BaseDal<Eva_Regular>,IEva_RegularDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_RegularDal GetEva_RegularDal()
            {
                return new Eva_RegularDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class CourseRelDal:BaseDal<CourseRel>,ICourseRelDal
         {


         }	

        public partial class DalFactory
        {
            public static ICourseRelDal GetCourseRelDal()
            {
                return new CourseRelDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_AuditRewardDal:BaseDal<TPM_AuditReward>,ITPM_AuditRewardDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_AuditRewardDal GetTPM_AuditRewardDal()
            {
                return new TPM_AuditRewardDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_TaskDal:BaseDal<Eva_Task>,IEva_TaskDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_TaskDal GetEva_TaskDal()
            {
                return new Eva_TaskDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Sys_DocumentDal:BaseDal<Sys_Document>,ISys_DocumentDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_DocumentDal GetSys_DocumentDal()
            {
                return new Sys_DocumentDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_TaskAnswerDal:BaseDal<Eva_TaskAnswer>,IEva_TaskAnswerDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_TaskAnswerDal GetEva_TaskAnswerDal()
            {
                return new Eva_TaskAnswerDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_TeacherAnswerDal:BaseDal<Eva_TeacherAnswer>,IEva_TeacherAnswerDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_TeacherAnswerDal GetEva_TeacherAnswerDal()
            {
                return new Eva_TeacherAnswerDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static IUserInfoDal GetUserInfoDal()
            {
                return new UserInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_ModifyRecordDal:BaseDal<TPM_ModifyRecord>,ITPM_ModifyRecordDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_ModifyRecordDal GetTPM_ModifyRecordDal()
            {
                return new TPM_ModifyRecordDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_RewardInfoDal:BaseDal<TPM_RewardInfo>,ITPM_RewardInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_RewardInfoDal GetTPM_RewardInfoDal()
            {
                return new TPM_RewardInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class CourseRoomDal:BaseDal<CourseRoom>,ICourseRoomDal
         {


         }	

        public partial class DalFactory
        {
            public static ICourseRoomDal GetCourseRoomDal()
            {
                return new CourseRoomDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_RewardLevelDal:BaseDal<TPM_RewardLevel>,ITPM_RewardLevelDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_RewardLevelDal GetTPM_RewardLevelDal()
            {
                return new TPM_RewardLevelDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class CourseDal:BaseDal<Course>,ICourseDal
         {


         }	

        public partial class DalFactory
        {
            public static ICourseDal GetCourseDal()
            {
                return new CourseDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_RewardUserInfoDal:BaseDal<TPM_RewardUserInfo>,ITPM_RewardUserInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_RewardUserInfoDal GetTPM_RewardUserInfoDal()
            {
                return new TPM_RewardUserInfoDal();
            }
	    }


	     /// </summary>
	     ///	菜单信息数据处理类4
	     /// </summary>
		 public partial class Sys_MenuInfoDal:BaseDal<Sys_MenuInfo>,ISys_MenuInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_MenuInfoDal GetSys_MenuInfoDal()
            {
                return new Sys_MenuInfoDal();
            }
	    }


	     /// </summary>
	     ///	角色用户关系数据处理类5
	     /// </summary>
		 public partial class Sys_RoleOfUserDal:BaseDal<Sys_RoleOfUser>,ISys_RoleOfUserDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_RoleOfUserDal GetSys_RoleOfUserDal()
            {
                return new Sys_RoleOfUserDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_RewardEditionDal:BaseDal<TPM_RewardEdition>,ITPM_RewardEditionDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_RewardEditionDal GetTPM_RewardEditionDal()
            {
                return new TPM_RewardEditionDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Sys_DictionaryDal:BaseDal<Sys_Dictionary>,ISys_DictionaryDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_DictionaryDal GetSys_DictionaryDal()
            {
                return new Sys_DictionaryDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class MajorDal:BaseDal<Major>,IMajorDal
         {


         }	

        public partial class DalFactory
        {
            public static IMajorDal GetMajorDal()
            {
                return new MajorDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class StudySectionDal:BaseDal<StudySection>,IStudySectionDal
         {


         }	

        public partial class DalFactory
        {
            public static IStudySectionDal GetStudySectionDal()
            {
                return new StudySectionDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class GradeInfoDal:BaseDal<GradeInfo>,IGradeInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static IGradeInfoDal GetGradeInfoDal()
            {
                return new GradeInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class StudentDal:BaseDal<Student>,IStudentDal
         {


         }	

        public partial class DalFactory
        {
            public static IStudentDal GetStudentDal()
            {
                return new StudentDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class SubMajorDal:BaseDal<SubMajor>,ISubMajorDal
         {


         }	

        public partial class DalFactory
        {
            public static ISubMajorDal GetSubMajorDal()
            {
                return new SubMajorDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class IndicatorDal:BaseDal<Indicator>,IIndicatorDal
         {


         }	

        public partial class DalFactory
        {
            public static IIndicatorDal GetIndicatorDal()
            {
                return new IndicatorDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TPM_BookStoryDal:BaseDal<TPM_BookStory>,ITPM_BookStoryDal
         {


         }	

        public partial class DalFactory
        {
            public static ITPM_BookStoryDal GetTPM_BookStoryDal()
            {
                return new TPM_BookStoryDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_TableDetailDal:BaseDal<Eva_TableDetail>,IEva_TableDetailDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_TableDetailDal GetEva_TableDetailDal()
            {
                return new Eva_TableDetailDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class IndicatorTypeDal:BaseDal<IndicatorType>,IIndicatorTypeDal
         {


         }	

        public partial class DalFactory
        {
            public static IIndicatorTypeDal GetIndicatorTypeDal()
            {
                return new IndicatorTypeDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class LinkManInfoDal:BaseDal<LinkManInfo>,ILinkManInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ILinkManInfoDal GetLinkManInfoDal()
            {
                return new LinkManInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_TableDal:BaseDal<Eva_Table>,IEva_TableDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_TableDal GetEva_TableDal()
            {
                return new Eva_TableDal();
            }
	    }


	     /// </summary>
	     ///	菜单按钮类型数据处理类6
	     /// </summary>
		 public partial class Sys_ButtonTypeDal:BaseDal<Sys_ButtonType>,ISys_ButtonTypeDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_ButtonTypeDal GetSys_ButtonTypeDal()
            {
                return new Sys_ButtonTypeDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class TeacherDal:BaseDal<Teacher>,ITeacherDal
         {


         }	

        public partial class DalFactory
        {
            public static ITeacherDal GetTeacherDal()
            {
                return new TeacherDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Sys_LetterDal:BaseDal<Sys_Letter>,ISys_LetterDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_LetterDal GetSys_LetterDal()
            {
                return new Sys_LetterDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_CourseType_TableDal:BaseDal<Eva_CourseType_Table>,IEva_CourseType_TableDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_CourseType_TableDal GetEva_CourseType_TableDal()
            {
                return new Eva_CourseType_TableDal();
            }
	    }


	     /// </summary>
	     ///	系统日志数据处理类7
	     /// </summary>
		 public partial class Sys_LogInfoDal:BaseDal<Sys_LogInfo>,ISys_LogInfoDal
         {


         }	

        public partial class DalFactory
        {
            public static ISys_LogInfoDal GetSys_LogInfoDal()
            {
                return new Sys_LogInfoDal();
            }
	    }


	     /// </summary>
	     ///	
	     /// </summary>
		 public partial class Eva_Table_Header_CustomDal:BaseDal<Eva_Table_Header_Custom>,IEva_Table_Header_CustomDal
         {


         }	

        public partial class DalFactory
        {
            public static IEva_Table_Header_CustomDal GetEva_Table_Header_CustomDal()
            {
                return new Eva_Table_Header_CustomDal();
            }
	    }
}