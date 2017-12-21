using FEModel;
using FEUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FEWeb.CommonHandler
{
    /// <summary>
    /// UploadHtml5Handler 的摘要说明
    /// </summary>
    public class UploadHtml5Handler : IHttpHandler
    {
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        JsonModel jsonModel = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            string FuncName = context.Request["Func"].SafeToString();
            string result = string.Empty;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            if (FuncName != null && FuncName != "")
            {
                try
                {
                    string oFolder = ConfigHelper.GetConfigString("FileManageName");
                    switch (FuncName)
                    {
                        case "Upload_AcheiveReward":
                            Upload_Batch(context, oFolder + "/AcheiveReward");
                            break;
                        case "Upload_RewardEdition":
                            Upload_Batch(context, oFolder+"/RewardEdition");
                            break;
                        case "Upload_BookStory":
                            Upload_Batch(context, oFolder + "/Upload_BookStory");
                            break;
                        case "Upload_AuditReward":
                            Upload_Batch(context, oFolder + "/AuditReward");
                            break;
                        case "Del_Document":
                            Del_Document(context);
                            break;
                        default:
                            jsonModel = JsonModel.get_jsonmodel(5, "没有此方法", "");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    jsonModel = JsonModel.get_jsonmodel(400, ex.Message, "");
                    LogService.WriteErrorLog(ex.Message);
                    context.Response.Write(result);
                    context.Response.End();
                }
            }
        }
        #region 上传图片
        public void Upload_Img(HttpContext context, string imgPath, bool isHtml5 = true)
        {
            string result = "";
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string DirePath = context.Server.MapPath(imgPath);
            if (!Directory.Exists(DirePath))
            {
                Directory.CreateDirectory(DirePath);
            }
            string ext = System.IO.Path.GetExtension(file.FileName);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.Ticks + ext;
            string p = imgPath + "/" + fileName;
            string path = context.Server.MapPath(p);
            file.SaveAs(path);
            if (isHtml5)
            {
                JsonModel jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "",
                    retData = p
                };
                result = string.IsNullOrEmpty(result) ? "{\"result\":" + jss.Serialize(jsonModel) + "}" : result;
            }
            else
            {
                result = "{\"error\":0,\"url\":\"" + p + "\"}";
            }
            context.Response.Write(result);
            context.Response.End();
        }
        #endregion       

        #region 批量文件保存
        private void Upload_Batch(HttpContext context, string imgPath)
        {
            string result = string.Empty;
            try
            {
                List<string> pathList = new List<string>();
                string loginUID = context.Request["LoginUID"];                
                HttpFileCollection files = HttpContext.Current.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile curfile = files[i];
                    string fileName = curfile.FileName;//获取初始文件名
                    string ext = fileName.Substring(fileName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                    string onlyName = fileName.Substring(0, fileName.LastIndexOf("."));                   
                    string serfolder = context.Server.MapPath(imgPath);
                    if (!Directory.Exists(serfolder))
                    {
                        Directory.CreateDirectory(serfolder);
                    }                    
                    string url = imgPath + "/" + onlyName + "_" + DateTime.Now.ToFileTime().ToString() + ext;
                    string fullPath = context.Server.MapPath(url);
                    curfile.SaveAs(fullPath);
                    string strDocName = Path.GetFileName(files[i].FileName);
                    pathList.Add(url);                    
                }
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = pathList
                };
            }
            catch (Exception ex)
            {
                jsonModel = JsonModel.get_jsonmodel(400, ex.Message, "");
            }
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }
        #endregion

        #region 文件删除       
        private void Del_Document(HttpContext context)
        {                  
            try
            {               
                int type = Convert.ToInt32(context.Request["Type"]??"2");//1文件夹；2文件
                List<string> file_paths = JsonConvert.DeserializeObject<List<string>>(context.Request["FilePath"]);
                foreach(string path in file_paths)
                {
                    if (type == 1)
                    {
                        string foldurl = context.Server.MapPath(path);
                        if (!Directory.Exists(foldurl))
                            FileHelper.DeleteDirectory(foldurl);
                    }
                    else
                    {
                        FileHelper.DeleteFile(context.Server.MapPath(path));
                    }
                }                
                jsonModel= JsonModel.get_jsonmodel(0,"success", "");
            }
            catch (Exception ex)
            {
                jsonModel = JsonModel.get_jsonmodel(400, ex.Message, "");
                LogService.WriteErrorLog(ex.Message);
            }
            string result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}