using FEHandler;
using FEHandler.Eva_Manage;
using FEModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FE_Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //var t = File.ReadAllLines(@"E:\vs_work2\FE_Test\TextFile1.txt");
            var ksp =Eva_ManageHandler. Get_Eva_RegularHelper(1);
           //var s = Constant.Indicator_List;
           //var d = Constant.IndicatorType_List;



        }

    }
}


public class helper
{
    public static void Init_()
    {
        //foreach (var item in t)
        //{
        //    var a = item.Split('\t');


        //    var student = Constant.Student_List.FirstOrDefault(i => i.UniqueNo == a[4]);
        //    if (student != null)
        //    {
        //        student.Name = a[5];
        //        student.Major_Id = a[0];
        //        student.Departent_Name = a[1];
        //        student.SubDepartmentID = a[9];
        //        student.SubDepartmentName = a[10];
        //        student.MajorID = a[7];
        //        student.MajorName = a[8];
        //        student.Sex = a[6] == "男" ? (byte)0 : (byte)1;
        //        student.ClassNo = a[2];
        //        student.ClassName = a[3];
        //        Constant.StudentService.Update(student);
        //    }
        //    else
        //    {
        //        Student Student = new Student()
        //        {
        //            Name = a[5],
        //            Major_Id = a[0],
        //            Departent_Name = a[1],
        //            SubDepartmentID = a[9],
        //            SubDepartmentName = a[10],
        //            MajorID = a[7],
        //            MajorName = a[8],
        //            Sex = a[6] == "男" ? (byte)0 : (byte)1,
        //            ClassNo = a[2],
        //            ClassName = a[3],
        //            CreateTime = DateTime.Now,
        //            CreateUID = "admin",
        //            EditTime = DateTime.Now,
        //            EditUID = "admin",
        //            IsDelete = 0,
        //            IsEnable = 0,
        //            StuNo = a[4],
        //            UniqueNo = a[4],

        //        };
        //        Constant.StudentService.Add(Student);
        //    }
        //}
    }

    public static void Init_T()
    {
        //foreach (var item in t)
        //{
        //    var a = item.Split('\t');


        //    var teacher = Constant.Teacher_List.FirstOrDefault(i => i.UniqueNo == a[4]);
        //    if (teacher != null)
        //    {
        //        teacher.Name = a[5];
        //        teacher.Status = a[7];
        //        teacher.SubDepartmentID = a[2];
        //        teacher.SubDepartmentName = a[3];
        //        Constant.TeacherService.Update(teacher);
        //    }
        //    else
        //    {
        //        int b = 0;
        //    }
        //}
    }

    public static void Init_S()
    {
        //foreach (var item in t)
        //{
        //    var a = item.Split('\t');

        //    var major = Constant.Major_List.FirstOrDefault(i => i.Major_Name == a[28]);
        //    var submajor = Constant.SubMajor_List.FirstOrDefault(i => i.Major_Name == a[29]);

        //    var course = Constant.CourseRoom_List.FirstOrDefault(i => i.Name == a[2] && i.TeacherUID == a[25] && i.Coures_Id == a[23] && i.Calss_Id == a[24]);
        //    if (course != null)
        //    {
        //        course.Year = Convert.ToInt32(a[0]);
        //        course.Season = a[1];
        //        course.Name = a[2];
        //        course.Name = a[3];
        //        course.GradeName = a[4];
        //        course.RoomDepartmentID = a[5];

        //        course.CourseProperty = a[27];
        //        course.CourseType = a[26];
        //        course.Major_Id = major != null ? major.Id : "";
        //        course.DepartmentName = major != null ? major.Major_Name : "";
        //        course.SubDepartmentID = submajor != null ? submajor.Id : "";
        //        course.SubDepartmentName = submajor != null ? submajor.Major_Name : "";
        //        course.TeacherName = a[6];
        //        course.TeacherProperty = a[8];
        //        course.TeacherPropertyID = a[7];
        //        course.TeacherDepartmentID = a[30];
        //        course.TeacherJobTitle = a[33];
        //        course.TeacherSubDepartmentID = a[31];
        //        course.StudentCount = Convert.ToInt32(a[32]);

        //        Constant.CourseRoomService.Update(course);
        //    }
        //    else
        //    {
        //        int b = 0;
        //    }
        //}
    }

    public static void Init_SubMajor()
    {
        //foreach (var item in t)
        //{
        //    var a = item.Split('\t');

        //    var submajor = Constant.SubMajor_List.FirstOrDefault(i => i.Id == a[2]);
        //    if (submajor != null)
        //    {
        //        //submajor.PID = a[0];
        //        //submajor.PName = a[1];
        //        //submajor.Id = a[2];
        //        //submajor.Major_Name = a[3];                
        //        //Constant.SubMajorService.Update(submajor);
        //    }
        //    else
        //    {
        //        Constant.SubMajorService.Add_IncludeId(new FEModel.SubMajor()
        //        {
        //            PID = a[0] == "null" ? "" : a[0],
        //            PName = a[1] == "null" ? "" : a[1],
        //            Id = a[2] == "null" ? "" : a[2],
        //            Major_Name = a[3] == "null" ? "" : a[3],
        //            CreateTime = DateTime.Now,
        //            EditTime = DateTime.Now,
        //            CreateUID = "admin",
        //            IsDelete = 0,
        //            EditUID = "admin",
        //        });
        //    }
        //}
    }

    public static void Init_Course()
    {
        //foreach (var item in t)
        //{
        //    var a = item.Split('\t');

        //    var course = Constant.Course_List.FirstOrDefault(i => i.UniqueNo == a[4]);
        //    if(course != null)
        //    {
        //        course.DepartMentID = a[0];
        //        course.DepartmentName = a[1];
        //        course.SubDepartmentID = a[2];
        //        course.SubDepartmentName = a[3];
        //        course.CourseTypeName = a[6];
        //        course.CourseProperty = a[7];

        //        Constant.CourseService.Update(course); 
        //    }
        //    else
        //    {
        //        int b= 0;
        //    }
        //}
    }
}