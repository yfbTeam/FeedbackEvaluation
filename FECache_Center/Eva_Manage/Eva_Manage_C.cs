using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEModel;
using FEUtility;

namespace FECache_Center.Eva_Manage
{
    public class Eva_Manage_C
    {
        /// <summary>
        /// 评价表
        /// </summary>
        public static List<Eva_Table> Eva_Table_List = new List<Eva_Table>();

        #region 评价表

        public static void Eva_Table_Add(Eva_Table Eva_Table)
        {
            try
            {
                if (!Eva_Table_List.Contains(Eva_Table))
                {
                    Eva_Table_List.Add(Eva_Table);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public static void Eva_Table_Remove(int Id)
        {
            try
            {
                Eva_Table Eva_Table = Eva_Table_List.FirstOrDefault(t => t.Id == Id);
                if (Eva_Table != null)
                {
                    if (Eva_Table_List.Contains(Eva_Table))
                    {
                        Eva_Table_List.Remove(Eva_Table);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public static void Eva_Table_Update(Eva_Table Eva_Table)
        {
            try
            {

                if (Eva_Table != null)
                {
                    if (Eva_Table_List.Contains(Eva_Table))
                    {
                        Eva_Table_List.Remove(Eva_Table);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion
    }
}
