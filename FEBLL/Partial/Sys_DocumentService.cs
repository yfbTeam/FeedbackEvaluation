using FEDAL;
using FEIBLL;
using FEModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEBLL
{
    public partial class Sys_DocumentService : BaseService<Sys_Document>, ISys_DocumentService
    {
        Sys_DocumentDal dal = new Sys_DocumentDal();
        #region 批量操作文件
        public JsonModel OperDocument(List<Sys_Document> pathlist, string edit_PathId,int relationid=0)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result =dal.OperDocument(pathlist, edit_PathId, relationid);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "数据更新失败",
                        retData = ""
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
        #endregion        

        #region 根据关联Id删除信息
        public JsonModel DelDocByRelId(int type, int relationid)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.DelDocByRelId(type, relationid);
                if (result > 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "数据更新失败",
                        retData = ""
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
        #endregion        
    }
}
