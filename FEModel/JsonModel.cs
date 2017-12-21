using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEModel
{
    public class JsonModel
    {
        public object retData { get; set; }
        public string errMsg { get; set; }
        public int errNum { get; set; }
        public string status { get; set; }

        /// <summary>
        ///页面数据回调
        /// </summary>
        /// <param name="errNum"></param>
        /// <param name="errMsg"></param>
        /// <param name="retData"></param>
        /// <returns></returns>
        public static JsonModel get_jsonmodel(int errNum, string errMsg, object retData)
        {
            return new JsonModel()
            {
                errNum = errNum,
                errMsg = errMsg,
                retData = retData,
            };
        }
        /// <summary>
        ///页面数据回调
        /// </summary>
        /// <param name="errNum"></param>
        /// <param name="errMsg"></param>
        /// <param name="retData"></param>
        /// <returns></returns>
        public static JsonModel get_jsonmodel(int errNum, string errMsg, object retData, string status)
        {
            return new JsonModel()
            {
                errNum = errNum,
                errMsg = errMsg,
                retData = retData,
                status = status
            };
        }
    }

    public class JsonModelNum : JsonModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }
        public int RowCount { get; set; }

        public static JsonModelNum GetJsonModel_o(int errNum, string erMsg, object refdata)
        {
            JsonModelNum jsonModel = new JsonModelNum()
            {
                errNum = errNum,
                errMsg = erMsg,
                retData = refdata
            };
            return jsonModel;
        }

        public static JsonModelNum GetJsonModel_o(int errNum, string erMsg)
        {
            JsonModelNum jsonModel = new JsonModelNum()
            {
                errNum = errNum,
                errMsg = erMsg,
                retData = ""
            };
            return jsonModel;
        }
    }
}
