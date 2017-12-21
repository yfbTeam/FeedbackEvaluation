using FEBLL;
using FEHandler.Eva_Manage;
using FEModel;
using FEUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Web;
using System.Web.Script.Serialization;

namespace FEHandler
{
    public static class Constant
    {
        #region 院系专业

        public static MajorService MajorService = new MajorService();
        public static SubMajorService SubMajorService = new SubMajorService();

        private static List<Major> major_List = null;
        /// <summary>
        /// 院系列表
        /// </summary>
        public static List<Major> Major_List
        {
            get
            {
                if (major_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Major");
                    DataTable dt = MajorService.GetData(hs, false, "and IsDelete =0");
                    major_List = ConverList<Major>.ConvertToList(dt);
                }
                return major_List;
            }
            set { Constant.major_List = value; }
        }


        private static List<SubMajor> subMajor_List = null;
        /// <summary>
        /// 子院系列表
        /// </summary>
        public static List<SubMajor> SubMajor_List
        {
            get
            {
                if (subMajor_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "SubMajor");
                    DataTable dt = SubMajorService.GetData(hs, false, "and IsDelete =0");
                    subMajor_List = ConverList<SubMajor>.ConvertToList(dt);
                }
                return subMajor_List;
            }
            set { Constant.subMajor_List = value; }
        }

        #endregion

        #region 评价

        /// <summary>
        /// 数据库服务
        /// </summary>
        public static IndicatorService IndicatorService = new IndicatorService();
        public static IndicatorTypeService IndicatorTypeService = new IndicatorTypeService();     
        public static Eva_DistributionService Eva_DistributionService = new Eva_DistributionService();
        public static Eva_TableService Eva_TableService = new Eva_TableService();
        public static Eva_Table_HeaderService Eva_Table_HeaderService = new Eva_Table_HeaderService();
        public static Eva_Table_Header_CustomService Eva_Table_Header_CustomService = new Eva_Table_Header_CustomService();

        public static Eva_TableDetailService Eva_TableDetailService = new Eva_TableDetailService();
        public static Eva_RegularService Eva_RegularService = new Eva_RegularService();
        public static Eva_TaskService Eva_TaskService = new Eva_TaskService();
        public static Eva_TaskAnswerService Eva_TaskAnswerService = new Eva_TaskAnswerService();

        public static Eva_QuestionAnswerService Eva_QuestionAnswerService = new Eva_QuestionAnswerService();
        public static Eva_QuestionAnswer_DetailService Eva_QuestionAnswer_DetailService = new Eva_QuestionAnswer_DetailService();
        public static Eva_TeacherAnswerService Eva_TeacherAnswerService = new Eva_TeacherAnswerService();

        private static List<Indicator> indicator_List = null;
        /// <summary>
        /// 指标库表
        /// </summary>
        public static List<Indicator> Indicator_List
        {
            get
            {
                if (indicator_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Indicator");
                    DataTable dt = IndicatorService.GetData(hs, false, "and IsDelete =0");
                    indicator_List = ConvertToList_Indicator(dt);
                }
                return indicator_List;
            }
            set { Constant.indicator_List = value; }
        }

        private static List<IndicatorType> indicatorType_List = null;
        /// <summary>
        /// 指标库分类表
        /// </summary>
        public static List<IndicatorType> IndicatorType_List
        {
            get
            {
                if (indicatorType_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "IndicatorType");
                    DataTable dt = IndicatorTypeService.GetData(hs, false, "and IsDelete =0");
                    indicatorType_List = ConvertToList_IndicatorType(dt);
                }
                return indicatorType_List;
            }
            set { Constant.indicatorType_List = value; }
        }

        private static List<Eva_Distribution> eva_Distribution_List = null;
        /// <summary>
        /// 定期评价分配表
        /// </summary>
        public static List<Eva_Distribution> Eva_Distribution_List
        {
            get
            {
                if (eva_Distribution_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_Distribution");
                    DataTable dt = Eva_DistributionService.GetData(hs, false, "and IsDelete =0");
                    eva_Distribution_List = ConverList<Eva_Distribution>.ConvertToList(dt);
                }
                return eva_Distribution_List;
            }
            set { Constant.eva_Distribution_List = value; }
        }

        private static List<Eva_Regular> eva_Regular_List = null;
        /// <summary>
        /// 定期评价表
        /// </summary>
        public static List<Eva_Regular> Eva_Regular_List
        {
            get
            {
                if (eva_Regular_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_Regular");
                    DataTable dt = Eva_RegularService.GetData(hs, false, "and IsDelete =0");
                    eva_Regular_List = ConverList<Eva_Regular>.ConvertToList(dt);
                }
                return eva_Regular_List;
            }
            set { Constant.eva_Regular_List = value; }
        }

        private static List<Eva_Table> eva_Table_List = null;
        /// <summary>
        /// 表格设计表
        /// </summary>
        public static List<Eva_Table> Eva_Table_List
        {
            get
            {
                if (eva_Table_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_Table");
                    DataTable dt = Eva_TableService.GetData(hs, false, "and IsDelete =0");
                    eva_Table_List = ConverList<Eva_Table>.ConvertToList(dt);
                }
                return eva_Table_List;
            }
            set { Constant.eva_Table_List = value; }
        }

        private static List<Eva_Table_Header> eva_Table_Header_List = null;
        /// <summary>
        /// 表格设计表
        /// </summary>
        public static List<Eva_Table_Header> Eva_Table_Header_List
        {
            get
            {
                if (eva_Table_Header_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_Table_Header");
                    DataTable dt = Eva_Table_HeaderService.GetData(hs, false, "and IsDelete =0");
                    eva_Table_Header_List = ConverList<Eva_Table_Header>.ConvertToList(dt);
                }
                return eva_Table_Header_List;
            }
            set { Constant.eva_Table_Header_List = value; }
        }

        private static List<Eva_Table_Header_Custom> eva_Table_Header_Custom_List = null;
        /// <summary>
        /// 表格设计表
        /// </summary>
        public static List<Eva_Table_Header_Custom> Eva_Table_Header_Custom_List
        {
            get
            {
                if (eva_Table_Header_Custom_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_Table_Header_Custom");
                    DataTable dt = Eva_Table_Header_CustomService.GetData(hs, false, "and IsDelete =0");
                    eva_Table_Header_Custom_List = ConverList<Eva_Table_Header_Custom>.ConvertToList(dt);
                }
                return eva_Table_Header_Custom_List;
            }
            set { Constant.eva_Table_Header_Custom_List = value; }
        }


        private static List<Eva_TableDetail> eva_TableDetail_List = null;
        /// <summary>
        /// 评价表详情
        /// </summary>
        public static List<Eva_TableDetail> Eva_TableDetail_List
        {
            get
            {
                if (eva_TableDetail_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_TableDetail");
                    DataTable dt = Eva_TableDetailService.GetData(hs, false, "and IsDelete =0");
                    eva_TableDetail_List = ConverList<Eva_TableDetail>.ConvertToList(dt);
                }
                return eva_TableDetail_List;
            }
            set { Constant.eva_TableDetail_List = value; }
        }

        private static List<Eva_Task> eva_Task_List = null;
        /// <summary>
        /// 评价任务表
        /// </summary>
        public static List<Eva_Task> Eva_Task_List
        {
            get
            {
                if (eva_Task_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_Task");
                    DataTable dt = Eva_TaskService.GetData(hs, false, "and IsDelete =0");
                    eva_Task_List = ConverList<Eva_Task>.ConvertToList(dt);
                }
                return eva_Task_List;
            }
            set { Constant.eva_Task_List = value; }
        }

        private static List<Eva_TaskAnswer> eva_TaskAnswer_List = null;
        /// <summary>
        /// 评价答题表
        /// </summary>
        public static List<Eva_TaskAnswer> Eva_TaskAnswer_List
        {
            get
            {
                if (eva_TaskAnswer_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_TaskAnswer");
                    DataTable dt = Eva_TaskAnswerService.GetData(hs, false, "and IsDelete =0");
                    eva_TaskAnswer_List = ConverList<Eva_TaskAnswer>.ConvertToList(dt);
                }
                return eva_TaskAnswer_List;
            }
            set { Constant.eva_TaskAnswer_List = value; }
        }


        private static List<Eva_TeacherAnswer> eva_TeacherAnswer_List = null;
        /// <summary>
        /// 教师反馈表
        /// </summary>
        public static List<Eva_TeacherAnswer> Eva_TeacherAnswer_List
        {
            get
            {
                if (eva_TeacherAnswer_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_TeacherAnswer");
                    DataTable dt = Eva_TeacherAnswerService.GetData(hs, false, "and IsDelete =0");
                    eva_TeacherAnswer_List = ConverList<Eva_TeacherAnswer>.ConvertToList(dt);
                }
                return eva_TeacherAnswer_List;
            }
            set { Constant.eva_TeacherAnswer_List = value; }
        }

        private static List<Eva_QuestionAnswer_Detail> eva_QuestionAnswer_Detail_List = null;
        /// <summary>
        /// 学生答题基础信息表
        /// </summary>
        public static List<Eva_QuestionAnswer_Detail> Eva_QuestionAnswer_Detail_List
        {
            get
            {
                if (eva_QuestionAnswer_Detail_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_QuestionAnswer_Detail");
                    DataTable dt = Eva_QuestionAnswer_DetailService.GetData(hs, false, "and IsDelete =0");
                    eva_QuestionAnswer_Detail_List = ConverList<Eva_QuestionAnswer_Detail>.ConvertToList(dt);
                }
                return eva_QuestionAnswer_Detail_List;
            }
            set { Constant.eva_QuestionAnswer_Detail_List = value; }
        }

        private static List<Eva_QuestionAnswer> eva_QuestionAnswer_List = null;
        /// <summary>
        /// 学生答题表
        /// </summary>
        public static List<Eva_QuestionAnswer> Eva_QuestionAnswer_List
        {
            get
            {
                if (eva_QuestionAnswer_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_QuestionAnswer");
                    DataTable dt = Eva_QuestionAnswerService.GetData(hs, false, "and IsDelete =0");
                    eva_QuestionAnswer_List = ConverList<Eva_QuestionAnswer>.ConvertToList(dt);
                }
                return eva_QuestionAnswer_List;
            }
            set { Constant.eva_QuestionAnswer_List = value; }
        }




        #endregion

        #region 人员表

        public static StudentService StudentService = new StudentService();
        public static TeacherService TeacherService = new TeacherService();
        public static UserInfoService UserInfoService = new UserInfoService();

        private static List<Student> student_List = null;
        /// <summary>
        /// 学生表
        /// </summary>
        public static List<Student> Student_List
        {
            get
            {
                if (student_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Student");
                    DataTable dt = StudentService.GetData(hs, false, "and IsDelete =0");
                    student_List = ConvertToList_Student(dt);
                }
                return student_List;
            }
            set { Constant.student_List = value; }
        }

        private static List<Teacher> teacher_List = null;
        /// <summary>
        /// 教师信息表
        /// </summary>
        public static List<Teacher> Teacher_List
        {
            get
            {
                if (teacher_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Teacher");
                    DataTable dt = TeacherService.GetData(hs, false, "and IsDelete =0");
                    teacher_List = ConvertToList_Teacher(dt);
                }
                return teacher_List;
            }
            set { Constant.teacher_List = value; }
        }

        private static List<UserInfo> userInfo_List = null;
        /// <summary>
        /// 用户信息表
        /// </summary>
        public static List<UserInfo> UserInfo_List
        {
            get
            {
                if (userInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "UserInfo");
                    DataTable dt = UserInfoService.GetData(hs, false, "and IsDelete =0");
                    userInfo_List = ConvertToList_UserInfo(dt);
                }
                return userInfo_List;
            }
            set { Constant.userInfo_List = value; }
        }

        private static List<LinkManInfo> linkmaninfo_List = null;
        /// <summary>
        /// 用户信息表
        /// </summary>
        public static List<LinkManInfo> LinkManInfo_List
        {
            get
            {
                if (linkmaninfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "LinkManInfo");
                    DataTable dt = UserInfoService.GetData(hs, false, "and IsDelete =0");
                    linkmaninfo_List = ConverList<LinkManInfo>.ConvertToList(dt);
                }
                return linkmaninfo_List;
            }
            set { Constant.linkmaninfo_List = value; }
        }
        #endregion

        #region 班级、课程、课堂

        public static ClassInfoService ClassInfoService = new ClassInfoService();
        public static CourseService CourseService = new CourseService();

        public static Eva_CourseType_TableService Eva_CourseType_TableService = new Eva_CourseType_TableService();

        public static CourseRelService CourseRelService = new CourseRelService();
        public static CourseRoomService CourseRoomService = new CourseRoomService();
        public static Class_StudentInfoService Class_StudentInfoService = new Class_StudentInfoService();
        public static GradeInfoService GradeInfoService = new GradeInfoService();

        private static List<GradeInfo> gradeInfo_List = null;
        /// <summary>
        /// 年级表
        /// </summary>
        public static List<GradeInfo> GradeInfo_List
        {
            get
            {
                if (gradeInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "GradeInfo");
                    DataTable dt = GradeInfoService.GetData(hs, false, "and IsDelete =0");
                    gradeInfo_List = ConverList<GradeInfo>.ConvertToList(dt);
                }
                return gradeInfo_List;
            }
            set { Constant.gradeInfo_List = value; }
        }

        private static List<ClassInfo> classInfo_List = null;
        /// <summary>
        /// 班级表
        /// </summary>
        public static List<ClassInfo> ClassInfo_List
        {
            get
            {
                if (classInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "ClassInfo");
                    DataTable dt = ClassInfoService.GetData(hs, false, "and IsDelete =0");
                    classInfo_List = ConverList<ClassInfo>.ConvertToList(dt);
                }
                return classInfo_List;
            }
            set { Constant.classInfo_List = value; }
        }


        private static List<Course> course_List = null;
        /// <summary>
        /// 课程表
        /// </summary>
        public static List<Course> Course_List
        {
            get
            {
                if (course_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Course");
                    DataTable dt = CourseService.GetData(hs, false, "and IsDelete =0");
                    course_List = ConvertToList_Course(dt);
                }
                return course_List;
            }
            set { Constant.course_List = value; }
        }


        private static List<Eva_CourseType_Table> eva_CourseType_Table_List = null;
        /// <summary>
        /// 课程表
        /// </summary>
        public static List<Eva_CourseType_Table> Eva_CourseType_Table_List
        {
            get
            {
                if (eva_CourseType_Table_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Eva_CourseType_Table");
                    DataTable dt = Eva_CourseType_TableService.GetData(hs, false, "and IsDelete =0");
                    Eva_CourseType_Table_List = ConverList<Eva_CourseType_Table>.ConvertToList(dt);
                }
                return eva_CourseType_Table_List;
            }
            set { Constant.eva_CourseType_Table_List = value; }
        }

        private static List<CourseRel> courseRel_List = null;
        /// <summary>
        /// 课程关系表
        /// </summary>
        public static List<CourseRel> CourseRel_List
        {
            get
            {
                if (courseRel_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "CourseRel");
                    DataTable dt = CourseRelService.GetData(hs, false, "and IsDelete =0");
                    courseRel_List = ConvertToList_CourseRel(dt);
                }
                return courseRel_List;
            }
            set { Constant.courseRel_List = value; }
        }


        private static List<CourseRoom> courseRoom_List = null;
        /// <summary>
        /// 课堂信息表
        /// </summary>
        public static List<CourseRoom> CourseRoom_List
        {
            get
            {
                if (courseRoom_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "CourseRoom");
                    DataTable dt = CourseRoomService.GetData(hs, false, "and IsDelete =0");
                    courseRoom_List = ConvertToList_CourseRoom(dt);
                }
                return courseRoom_List;
            }
            set { Constant.courseRoom_List = value; }
        }

        private static List<Class_StudentInfo> class_StudentInfo_List = null;
        /// <summary>
        /// 班级学生关系表
        /// </summary>
        public static List<Class_StudentInfo> Class_StudentInfo_List
        {
            get
            {
                if (class_StudentInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Class_StudentInfo");
                    DataTable dt = Class_StudentInfoService.GetData(hs, false, "and IsDelete =0");
                    class_StudentInfo_List = ConverList<Class_StudentInfo>.ConvertToList(dt);
                }
                return class_StudentInfo_List;
            }
            set { Constant.class_StudentInfo_List = value; }
        }

        #endregion

        #region 系统设置

        public static StudySectionService StudySectionService = new StudySectionService();
        public static Sys_DictionaryService Sys_DictionaryService = new Sys_DictionaryService();
        public static Sys_LogInfoService Sys_LogInfoService = new Sys_LogInfoService();
        public static Sys_MenuInfoService Sys_MenuInfoService = new Sys_MenuInfoService();
        public static Sys_MessageService Sys_MessageService = new Sys_MessageService();

        public static Sys_LetterService Sys_LetterService = new Sys_LetterService();

        public static Sys_RoleService Sys_RoleService = new Sys_RoleService();
        public static Sys_RoleOfMenuService Sys_RoleOfMenuService = new Sys_RoleOfMenuService();
        public static Sys_RoleOfUserService Sys_RoleOfUserService = new Sys_RoleOfUserService();

        private static List<StudySection> studySection_List = null;
        /// <summary>
        /// 学期表
        /// </summary>
        public static List<StudySection> StudySection_List
        {
            get
            {
                if (studySection_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "StudySection");
                    DataTable dt = StudySectionService.GetData(hs, false, "and IsDelete =0");
                    studySection_List = ConverList<StudySection>.ConvertToList(dt);
                }
                return studySection_List;
            }
            set { Constant.studySection_List = value; }
        }


        private static List<Sys_Dictionary> sys_Dictionary_List = null;
        /// <summary>
        /// 字典表
        /// </summary>
        public static List<Sys_Dictionary> Sys_Dictionary_List
        {
            get
            {
                if (sys_Dictionary_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_Dictionary");
                    DataTable dt = Sys_DictionaryService.GetData(hs, false, "and IsDelete =0");
                    sys_Dictionary_List = ConverList<Sys_Dictionary>.ConvertToList(dt);
                }
                return sys_Dictionary_List;
            }
            set { Constant.sys_Dictionary_List = value; }
        }

        private static List<Sys_LogInfo> sys_LogInfo_List = null;
        /// <summary>
        /// 系统日志
        /// </summary>
        public static List<Sys_LogInfo> Sys_LogInfo_List
        {
            get
            {
                if (sys_LogInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_LogInfo");
                    DataTable dt = Sys_LogInfoService.GetData(hs, false, "and IsDelete =0");
                    sys_LogInfo_List = ConverList<Sys_LogInfo>.ConvertToList(dt);
                }
                return sys_LogInfo_List;
            }
            set { Constant.sys_LogInfo_List = value; }
        }

        private static List<Sys_MenuInfo> sys_MenuInfo_List = null;
        /// <summary>
        /// 菜单信息表
        /// </summary>
        public static List<Sys_MenuInfo> Sys_MenuInfo_List
        {
            get
            {
                if (sys_MenuInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_MenuInfo");
                    DataTable dt = Sys_MenuInfoService.GetData(hs, false, "and IsDelete =0");
                    sys_MenuInfo_List = ConverList<Sys_MenuInfo>.ConvertToList(dt);
                }
                return sys_MenuInfo_List;
            }
            set { Constant.sys_MenuInfo_List = value; }
        }


        private static List<Sys_Message> sys_Message_List = null;
        /// <summary>
        /// 消息表
        /// </summary>
        public static List<Sys_Message> Sys_Message_List
        {
            get
            {
                if (sys_Message_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_Message");
                    DataTable dt = Sys_MessageService.GetData(hs, false, "and IsDelete =0");
                    sys_Message_List = ConverList<Sys_Message>.ConvertToList(dt);
                }
                return sys_Message_List;
            }
            set { Constant.sys_Message_List = value; }
        }

        #region 站内信

        private static List<Sys_Letter> sys_Letter_List = null;
        /// <summary>
        /// 站内信
        /// </summary>
        public static List<Sys_Letter> Sys_Letter_List
        {
            get
            {
                if (sys_Letter_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_Letter");
                    DataTable dt = Sys_LetterService.GetData(hs, false, "and IsDelete =0");
                    sys_Letter_List = ConverList<Sys_Letter>.ConvertToList(dt);
                }
                return sys_Letter_List;
            }
            set { Constant.sys_Letter_List = value; }
        }
        #endregion

        private static List<Sys_Role> sys_Role_List = null;
        /// <summary>
        /// 角色表
        /// </summary>
        public static List<Sys_Role> Sys_Role_List
        {
            get
            {
                if (sys_Role_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_Role");
                    DataTable dt = Sys_RoleService.GetData(hs, false, "and IsDelete =0");
                    sys_Role_List = ConverList<Sys_Role>.ConvertToList(dt);
                }
                return sys_Role_List;
            }
            set { Constant.sys_Role_List = value; }
        }


        private static List<Sys_RoleOfMenu> sys_RoleOfMenu_List = null;
        /// <summary>
        /// 角色菜单表
        /// </summary>
        public static List<Sys_RoleOfMenu> Sys_RoleOfMenu_List
        {
            get
            {
                if (sys_RoleOfMenu_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_RoleOfMenu");
                    DataTable dt = Sys_RoleOfMenuService.GetData(hs, false, "and IsDelete =0");
                    sys_RoleOfMenu_List = ConverList<Sys_RoleOfMenu>.ConvertToList(dt);
                }
                return sys_RoleOfMenu_List;
            }
            set { Constant.sys_RoleOfMenu_List = value; }
        }

        private static List<Sys_RoleOfUser> sys_RoleOfUser_List = null;
        /// <summary>
        /// 角色用户表
        /// </summary>
        public static List<Sys_RoleOfUser> Sys_RoleOfUser_List
        {
            get
            {
                if (sys_RoleOfUser_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "Sys_RoleOfUser");
                    DataTable dt = Sys_RoleOfUserService.GetData(hs, false, "and IsDelete =0");
                    sys_RoleOfUser_List = ConverList<Sys_RoleOfUser>.ConvertToList(dt);
                }
                return sys_RoleOfUser_List;
            }
            set { Constant.sys_RoleOfUser_List = value; }
        }

        #endregion

        #region 教师业绩
        public static TPM_AcheiveLevelService AcheiveLevelService = new TPM_AcheiveLevelService();
        public static TPM_RewardEditionService RewardEditionService = new TPM_RewardEditionService();
        public static TPM_RewardLevelService RewardLevelService = new TPM_RewardLevelService();
        public static TPM_RewardInfoService RewardInfoService = new TPM_RewardInfoService();

        #region 奖项管理TPM_RewardInfoService
        private static List<TPM_RewardInfo> tPM_RewardInfo_List = null;
        /// <summary>
        /// 奖励项目版本
        /// </summary>
        public static List<TPM_RewardInfo> TPM_RewardInfo_List
        {
            get
            {
                if (tPM_RewardInfo_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "TPM_RewardInfo");
                    DataTable dt = RewardEditionService.GetData(hs, false);
                    tPM_RewardInfo_List = ConverList<TPM_RewardInfo>.ConvertToList(dt);
                }
                return tPM_RewardInfo_List;
            }
            set { Constant.tPM_RewardInfo_List = value; }
        }

        #endregion

        #region 奖励项目等级TPM_RewardLevelService
        private static List<TPM_RewardLevel> tPM_RewardLevel_List = null;
        /// <summary>
        /// 业绩等级（业绩项目-奖励项目）
        /// </summary>
        public static List<TPM_RewardLevel> TPM_RewardLevel_List
        {
            get
            {
                if (tPM_RewardLevel_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "TPM_RewardLevel");
                    DataTable dt = AcheiveLevelService.GetData(hs, false);
                    tPM_RewardLevel_List = ConverList<TPM_RewardLevel>.ConvertToList(dt);
                }
                return tPM_RewardLevel_List;
            }
            set { Constant.tPM_RewardLevel_List = value; }
        }

        #endregion

        #region 奖励项目版本TPM_RewardEditionService
        private static List<TPM_RewardEdition> tPM_RewardEdition_List = null;
        /// <summary>
        /// 奖励项目版本
        /// </summary>
        public static List<TPM_RewardEdition> TPM_RewardEdition_List
        {
            get
            {
                if (tPM_RewardEdition_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "TPM_RewardEdition");
                    DataTable dt = RewardEditionService.GetData(hs, false);
                    tPM_RewardEdition_List = ConverList<TPM_RewardEdition>.ConvertToList(dt);
                }
                return tPM_RewardEdition_List;
            }
            set { Constant.tPM_RewardEdition_List = value; }
        }

        #endregion

        #region 业绩等级（业绩项目-奖励项目）TPM_AcheiveLevelService
        private static List<TPM_AcheiveLevel> tPM_AcheiveLevel_List = null;
        /// <summary>
        /// 业绩等级（业绩项目-奖励项目）
        /// </summary>
        public static List<TPM_AcheiveLevel> TPM_AcheiveLevel_List
        {
            get
            {
                if (tPM_AcheiveLevel_List == null)
                {
                    //数据库获取
                    Hashtable hs = new Hashtable();
                    hs.Add("TableName", "TPM_AcheiveLevel");
                    DataTable dt = AcheiveLevelService.GetData(hs, false);
                    tPM_AcheiveLevel_List = ConverList<TPM_AcheiveLevel>.ConvertToList(dt);
                }
                return tPM_AcheiveLevel_List;
            }
            set { Constant.tPM_AcheiveLevel_List = value; }
        }

        #endregion

        #endregion

        #region 序列化（转json,实例化一次即可，全局通用）

        /// <summary>
        /// js辅助
        /// </summary>
        public static JavaScriptSerializer jss = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };

        #endregion

        #region 定时释放垃圾

        static System.Timers.Timer Glob_Timer = null;

        /// <summary>
        /// 释放用不上的缓存（计算机运算时产生的缓存垃圾）【不包含从数据库去除的缓存数据】
        /// </summary>
        public static void Dispose_All()
        {
            if (Glob_Timer == null)
            {
                Glob_Timer = new System.Timers.Timer();
                Glob_Timer.Interval = 1000 * 60 * 1;
                Glob_Timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    GC_Helper();
                };
            }
        }

        public static void GC_Helper()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

        #region 使用反射进行数据的克隆

        /// <summary>
        /// 使用反射进行数据的克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Clone<T>(T data)
        {
            //定义默认类型对象
            T obj = default(T);
            try
            {
                //获取对象的类型
                Type type = typeof(T);
                //获取该类的属性集
                PropertyInfo[] propertyInfos = type.GetProperties();
                //创建实例
                obj = Activator.CreateInstance<T>();

                //遍历属性值
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    //获取当前属性的名称
                    string propertyInfoName = propertyInfo.Name;
                    var value = propertyInfo.GetValue(data);
                    try
                    {
                        //给该字段设置值
                        propertyInfo.SetValue(obj, value, null);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return obj;
        }

        #endregion

        #region 辅助工具


        /// <summary>
        ///
        /// </summary>
        public static void Update_StudentMajorByTeacher()
        {
            try
            {
                foreach (var item in Constant.Student_List)
                {
                    var t = Eva_ManageHandler.Get_Teacher_List2(item.UniqueNo);

                    if (t.Count > 0)
                    {
                        item.Major_Id = t[0].Major_ID;
                    }
                    Constant.StudentService.Update(item);


                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        #region 快速转换为实体

        /// <summary>
        /// 用户表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<UserInfo> ConvertToList_UserInfo(DataTable dt)
        {

            // 定义集合  
            List<UserInfo> ts = new List<UserInfo>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    UserInfo t = new UserInfo()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Address = Convert.ToString(dr["Address"]),
                        Birthday = Convert.ToString(dr["Birthday"]),
                        ClearPassword = Convert.ToString(dr["ClearPassword"]),
                        Email = Convert.ToString(dr["Email"]),
                        HeadPic = Convert.ToString(dr["HeadPic"]),
                        IDCard = Convert.ToString(dr["IDCard"]),
                        LoginName = Convert.ToString(dr["LoginName"]),
                        Major_ID = Convert.ToString(dr["Major_ID"]),
                        Name = Convert.ToString(dr["Name"]),
                        Nickname = Convert.ToString(dr["Nickname"]),
                        Password = Convert.ToString(dr["Password"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Pic = Convert.ToString(dr["Pic"]),
                        Remarks = Convert.ToString(dr["Remarks"]),

                        Sex = Convert.ToByte(dr["Sex"]),
                        UniqueNo = Convert.ToString(dr["UniqueNo"]),
                        UserType = Convert.ToByte(dr["UserType"]),


                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };


                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        /// <summary>
        /// 教师信息表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Teacher> ConvertToList_Teacher(DataTable dt)
        {

            // 定义集合  
            List<Teacher> ts = new List<Teacher>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    Teacher t = new Teacher()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Birthday = Convert.ToDateTime(dr["Birthday"]),
                        Major_ID = Convert.ToString(dr["Major_ID"]),
                        Name = Convert.ToString(dr["Name"]),
                        Sex = Convert.ToByte(dr["Sex"]),
                        UniqueNo = Convert.ToString(dr["UniqueNo"]),
                        Degree = Convert.ToString(dr["Degree"]),
                        Departent_Id = Convert.ToString(dr["Departent_Id"]),
                        Departent_Name = Convert.ToString(dr["Departent_Name"]),
                        Education = Convert.ToString(dr["Education"]),
                        JobTitle = Convert.ToString(dr["JobTitle"]),
                        Major_Name = Convert.ToString(dr["Major_Name"]),
                        Status = Convert.ToString(dr["Status"]),
                        SubDepartmentID = Convert.ToString(dr["SubDepartmentID"]),
                        SubDepartmentName = Convert.ToString(dr["SubDepartmentName"]),
                        TeachDate = Convert.ToString(dr["TeachDate"]),

                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };


                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        /// <summary>
        /// 学生信息表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Student> ConvertToList_Student(DataTable dt)
        {

            // 定义集合  
            List<Student> ts = new List<Student>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    Student t = new Student()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Sex = Convert.ToByte(dr["Sex"]),
                        UniqueNo = Convert.ToString(dr["UniqueNo"]),
                        Departent_Name = Convert.ToString(dr["Departent_Name"]),
                        SubDepartmentID = Convert.ToString(dr["SubDepartmentID"]),
                        SubDepartmentName = Convert.ToString(dr["SubDepartmentName"]),

                        ClassName = Convert.ToString(dr["ClassName"]),
                        ClassNo = Convert.ToString(dr["ClassNo"]),
                        Major_Id = Convert.ToString(dr["Major_Id"]),
                        MajorID = Convert.ToString(dr["MajorID"]),
                        MajorName = Convert.ToString(dr["MajorName"]),
                        StuNo = Convert.ToString(dr["StuNo"]),

                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };


                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }


        /// <summary>
        /// 教学安排
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<CourseRoom> ConvertToList_CourseRoom(DataTable dt)
        {

            // 定义集合  
            List<CourseRoom> ts = new List<CourseRoom>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    CourseRoom t = new CourseRoom()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        UniqueNo = Convert.ToString(dr["UniqueNo"]),
                        SubDepartmentID = Convert.ToString(dr["SubDepartmentID"]),
                        SubDepartmentName = Convert.ToString(dr["SubDepartmentName"]),
                        Major_Id = Convert.ToString(dr["Major_Id"]),

                        Calss_Id = Convert.ToString(dr["Calss_Id"]),
                        Coures_Id = Convert.ToString(dr["Coures_Id"]),
                        CouresName = Convert.ToString(dr["CouresName"]),
                        CourseProperty = Convert.ToString(dr["CourseProperty"]),
                        CourseType = Convert.ToString(dr["CourseType"]),
                        DepartmentName = Convert.ToString(dr["DepartmentName"]),
                        GradeID = Convert.ToString(dr["GradeID"]),
                        GradeName = Convert.ToString(dr["GradeName"]),
                        RoomDepartmentID = Convert.ToString(dr["RoomDepartmentID"]),
                        Season = Convert.ToString(dr["Season"]),
                        StudentCount = Convert.ToInt32(dr["StudentCount"]),
                        StudySection_Id = Convert.ToInt32(dr["StudySection_Id"]),
                        TeacherDepartmentID = Convert.ToString(dr["TeacherDepartmentID"]),
                        TeacherJobTitle = Convert.ToString(dr["TeacherJobTitle"]),
                        TeacherName = Convert.ToString(dr["TeacherName"]),
                        TeacherProperty = Convert.ToString(dr["TeacherProperty"]),
                        TeacherPropertyID = Convert.ToString(dr["TeacherPropertyID"]),
                        TeacherSubDepartmentID = Convert.ToString(dr["TeacherSubDepartmentID"]),
                        TeacherUID = Convert.ToString(dr["TeacherUID"]),
                        Year = Convert.ToInt32(dr["Year"]),

                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };


                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        /// <summary>
        /// 课程信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Course> ConvertToList_Course(DataTable dt)
        {

            // 定义集合  
            List<Course> ts = new List<Course>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    Course t = new Course()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        UniqueNo = Convert.ToString(dr["UniqueNo"]),
                        SubDepartmentID = Convert.ToString(dr["SubDepartmentID"]),
                        SubDepartmentName = Convert.ToString(dr["SubDepartmentName"]),
                        CourseProperty = Convert.ToString(dr["CourseProperty"]),
                        CourseType = Convert.ToString(dr["CourseType"]),
                        DepartmentName = Convert.ToString(dr["DepartmentName"]),

                        CourseTypeName = Convert.ToString(dr["CourseTypeName"]),
                        DepartMentID = Convert.ToString(dr["DepartMentID"]),

                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };


                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        /// <summary>
        /// 课程分配表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<CourseRel> ConvertToList_CourseRel(DataTable dt)
        {

            // 定义集合  
            List<CourseRel> ts = new List<CourseRel>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    CourseRel t = new CourseRel()
                    {
                        Id = Convert.ToInt32(dr["Id"]),

                        Course_Id = Convert.ToString(dr["Course_Id"]),
                        CourseType_Id = Convert.ToString(dr["CourseType_Id"]),
                        StudySection_Id = Convert.ToInt32(dr["StudySection_Id"]),

                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                    };
                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        /// <summary>
        /// 指标库表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Indicator> ConvertToList_Indicator(DataTable dt)
        {

            // 定义集合  
            List<Indicator> ts = new List<Indicator>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    Indicator t = new Indicator()
                    {
                        Id = Convert.ToInt32(dr["Id"]),

                        IndicatorType_Id = Convert.ToInt32(dr["IndicatorType_Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        OptionA = Convert.ToString(dr["OptionA"]),
                        OptionB = Convert.ToString(dr["OptionB"]),
                        OptionC = Convert.ToString(dr["OptionC"]),
                        OptionD = Convert.ToString(dr["OptionD"]),
                        OptionE = Convert.ToString(dr["OptionE"]),
                        OptionF = Convert.ToString(dr["OptionF"]),
                        QuesType_Id = Convert.ToInt32(dr["QuesType_Id"]),
                        Remarks = Convert.ToString(dr["Remarks"]),
                        Type = Convert.ToInt32(dr["Type"]),
                        UseTimes = Convert.ToInt32(dr["UseTimes"]),                          

                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };
                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        /// <summary>
        /// 指标库分类表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<IndicatorType> ConvertToList_IndicatorType(DataTable dt)
        {

            // 定义集合  
            List<IndicatorType> ts = new List<IndicatorType>();

            try
            {
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    IndicatorType t = new IndicatorType()
                    {
                        Id = Convert.ToInt32(dr["Id"]),                     
                        Name = Convert.ToString(dr["Name"]),                      
                        Type = Convert.ToInt32(dr["Type"]),
                        P_Type = Convert.ToInt32(dr["P_Type"]),
                        Parent_Id = Convert.ToInt32(dr["Parent_Id"]),
                       
                        CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                        CreateUID = Convert.ToString(dr["CreateUID"]),
                        EditTime = Convert.ToDateTime(dr["EditTime"]),
                        EditUID = Convert.ToString(dr["EditUID"]),
                        IsDelete = Convert.ToByte(dr["IsDelete"]),
                        IsEnable = Convert.ToByte(dr["IsEnable"]),
                    };
                    //对象添加到泛型集合中  
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }

            return ts;

        }

        #endregion
    }
}