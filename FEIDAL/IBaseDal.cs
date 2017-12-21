using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEIDAL
{
    public interface IBaseDal<T> where T : class, new()
    {
        int Add(T entity);
        int Add_IncludeId(T entity);

        bool Update(T entity, SqlTransaction trans = null);

        bool DeleteFalse(T entity, int id);

        int DeleteBatchFalse(T entity, int isenable, params string[] ids);

        bool Delete(T entity, int id);

        int DeleteBatch(T entity, params string[] ids);

        T GetEntityById(T entity, int id);
        bool GetInfoById(T entity, int id);

        List<T> GetEntityListByField(T entity, string filed, string value);

        DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "");
        bool IsNameExists(T entity, string fieldvalue, Int32 Id = 0, string fieldname = "Name");//判断某字段的值是否已存在
    }
}
