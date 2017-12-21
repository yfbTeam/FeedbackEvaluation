using FEModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEIBLL
{
    public interface IBaseService<T> where T : class, new()
    {
        JsonModel Add(T entity);

        JsonModel Update(T entity, SqlTransaction trans = null);

        JsonModel DeleteFalse(int id);

        JsonModel DeleteBatchFalse(int isenable, params string[] ids);

        JsonModel Delete(int id);

        JsonModel DeleteBatch(params string[] ids);

        JsonModel GetEntityById(int id);

        JsonModel GetEntityListByField(string filed, string value);

        JsonModel GetPage(Hashtable ht, bool IsPage = true, string Where = "");
        DataTable GetData(Hashtable ht, bool IsPage = true, string Where = "");
        bool GetInfoById(int id);
        JsonModel IsNameExists(string fieldvalue, Int32 Id = 0, string fieldname = "Name"); //判断某字段的值是否已存在
    }
}
