using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FECache_Center.SystemSetting
{
    public class SystemSetting_C
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        public static List<Department> Department_List = new List<Department>();

        /// <summary>
        /// 系列表
        /// </summary>
        public static List<College> College_List = new List<College>();

        /// <summary>
        /// 专业列表
        /// </summary>
        public static List<Major> Major_List = new List<Major>();


        #region 部门操作

        public static void Department_Add(Department department)
        {
            try
            {
                if (!Department_List.Contains(department))
                {
                    Department_List.Add(department);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public static void Department_Remove(int Id)
        {
            try
            {
                Department department = Department_List.FirstOrDefault(t => t.Id == Id);
                if (department != null)
                {
                    if (Department_List.Contains(department))
                    {
                        Department_List.Remove(department);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public static void Department_Update(Department department)
        {
            try
            {

                if (department != null)
                {
                    if (Department_List.Contains(department))
                    {
                        Department_List.Remove(department);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        #region 指标库

        public static void IndicatorGet()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion
    }
}
