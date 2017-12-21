using FEIDAL;
using FEModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEBLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IBaseDal<T> CurrentDal;//依赖抽象的接口。

        private T currentEntity;

        public T CurrentEntity
        {
            get { return new T(); }
        }

        public BaseService()
        {
            //执行给当前CurrentDal赋值。
            //强迫子类给当前类的CurrentDal属性赋值。
            SetCurrentDal();//调用了一个抽象方法。
        }

        //纯抽象方法：子类必须重写此方法。
        public abstract void SetCurrentDal();

        public virtual JsonModel Add(T entity)
        {
            JsonModel jsonModel = null;
            try
            {
                int result = CurrentDal.Add(entity);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = result
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 5,
                        errMsg = "名称重复",
                        retData = result
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }

        }

        public virtual JsonModel Add_IncludeId(T entity)
        {
            JsonModel jsonModel = null;
            try
            {
                int result = CurrentDal.Add_IncludeId(entity);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = result
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 5,
                        errMsg = "名称重复",
                        retData = result
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }

        }

        /// <summary>
        /// 更新单挑数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual JsonModel Update(T entity, SqlTransaction trans = null)
        {
            JsonModel jsonModel = null;
            try
            {
                bool result = CurrentDal.Update(entity, trans);

                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }

        }
        /// <summary>
        /// 更新单挑数据(事务)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        /// <summary>
        /// 伪删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual JsonModel DeleteFalse(int id)
        {
            JsonModel jsonModel = null;
            try
            {
                bool result = CurrentDal.DeleteFalse(CurrentEntity, id);

                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }

        }

        /// <summary>
        /// 批量伪删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual JsonModel DeleteBatchFalse(int isenable, params string[] ids)
        {
            JsonModel jsonModel = null;
            try
            {
                int result = CurrentDal.DeleteBatchFalse(CurrentEntity, isenable, ids);

                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }

        }


        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual JsonModel Delete(int id)
        {
            JsonModel jsonModel = null;
            try
            {
                bool result = CurrentDal.Delete(CurrentEntity, id);

                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual JsonModel DeleteBatch(params string[] ids)
        {
            JsonModel jsonModel = null;
            try
            {
                int result = CurrentDal.DeleteBatch(CurrentEntity, ids);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
            //return CurrentDal.DeleteBatch(CurrentEntity, ids);

        }

        public virtual JsonModel GetEntityById(int id)
        {
            JsonModel jsonModel = null;
            try
            {
                T entity = CurrentDal.GetEntityById(CurrentEntity, id);
                if (entity != null)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = entity
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "获取数据失败",
                        retData = null
                    };
                }

                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }

        public virtual JsonModel GetEntityListByField(string filed, string value)
        {
            JsonModel jsonModel = null;
            try
            {
                List<T> list = CurrentDal.GetEntityListByField(CurrentEntity, filed, value);
                if (list != null && list.Count == 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "无数据",
                        retData = list
                    };
                    return jsonModel;
                }
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = list
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }

        public virtual JsonModel GetPage(Hashtable ht, bool IsPage = true, string where = "")
        {
            int RowCount = 0;
            BLLCommon common = new BLLCommon();
            try
            {
                int PageIndex = 0;
                int PageSize = 0;
                if (IsPage)
                {
                    //增加起始条数、结束条数
                    ht = common.AddStartEndIndex(ht);
                    PageIndex = Convert.ToInt32(ht["PageIndex"]);
                    PageSize = Convert.ToInt32(ht["PageSize"]);
                }

                DataTable modList = CurrentDal.GetListByPage(ht, out RowCount, IsPage, where);

                //定义分页数据实体
                PagedDataModel<Dictionary<string, object>> pagedDataModel = null;
                //定义JSON标准格式实体中
                JsonModel jsonModel = null;
                if (modList == null || modList.Rows.Count <= 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "无数据",
                        retData = ""
                    };
                    return jsonModel;
                }
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                list = common.DataTableToList(modList);

                if (IsPage)
                {

                    //list.Add(common.DataTableToJson(modList));
                    //总条数
                    //int RowCount = modList.Rows.Count;// CurrentDal.GetRecordCount(ht, null);

                    //总页数
                    int PageCount = (int)Math.Ceiling(RowCount * 1.0 / PageSize);
                    //将数据封装到PagedDataModel分页数据实体中
                    pagedDataModel = new PagedDataModel<Dictionary<string, object>>()
                    {
                        PageCount = PageCount,
                        PagedData = list,
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        RowCount = RowCount
                    };
                    //将分页数据实体封装到JSON标准实体中
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = pagedDataModel
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = list
                    };
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        public virtual DataTable GetData(Hashtable ht, bool IsPage = true, string where = "")
        {
            DataTable modList = null;
            int RowCount = 0;
            BLLCommon common = new BLLCommon();
            try
            {
                modList = new DataTable();
                int PageIndex = 0;
                int PageSize = 0;
                if (IsPage)
                {
                    //增加起始条数、结束条数
                    ht = common.AddStartEndIndex(ht);
                    PageIndex = Convert.ToInt32(ht["PageIndex"]);
                    PageSize = Convert.ToInt32(ht["PageSize"]);
                }

                modList = CurrentDal.GetListByPage(ht, out RowCount, IsPage, where);
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
            }
            return modList;
        }

        public virtual bool GetInfoById(int id)
        {
            bool Flag = CurrentDal.GetInfoById(currentEntity, id);
            return Flag;
        }

        #region 判断名称是否已存在
        /// <summary>
        /// 判断名称是否已存在
        /// </summary>
        /// <param name="fieldvalue">字段值</param>
        /// <param name="fieldname">字段名称</param>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public JsonModel IsNameExists(string fieldvalue, Int32 Id = 0, string fieldname = "Name")
        {
            JsonModel jsonModel = null;
            try
            {
                bool result = CurrentDal.IsNameExists(CurrentEntity, fieldvalue, Id, fieldname);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 根据DataTable获取JsonModel
        public JsonModel GetJsonModelByDataTable(DataTable modList)
        {
            try
            {
                //定义JSON标准格式实体中
                JsonModel jsonModel = null;
                if (modList.Rows.Count <= 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "无数据",
                        retData = ""
                    };
                    return jsonModel;
                }
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                list = new BLLCommon().DataTableToList(modList);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = list
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion
    }
}
