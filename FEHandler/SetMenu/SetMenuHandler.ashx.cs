using FEModel;
using FEUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEHandler.SetMenu
{
    /// <summary>
    /// SetMenuHandler 的摘要说明
    /// </summary>
    public class SetMenuHandler : IHttpHandler
    {

        JsonModel jsonModel = new JsonModel();
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            string func = RequestHelper.string_transfer(Request, "func");
            try
            {
                switch (func)
                {

                    case "SetMenuInfo": SetMenuInfo(context); break;
                    case "SetTwoMenuInfo": SetTwoMenuInfo(context); break;
                    //TestGetMenuInfo
                    case "TestGetMenuInfo": TestGetMenuInfo(context); break;
                    //TestGetPagelimits
                    case "GetPagelimits": TestGetPagelimits(context); break;
                    //GetAllMenuInfo
                    case "GetAllMenuInfo": GetAllMenuInfo(context); break;
                    //SetRole_MenuInfo
                    case "SetRole_MenuInfo": SetRole_MenuInfo(context); break;
                    case "Get_MenuBtnInfo": Get_MenuBtnInfo(context); break;
                    default:
                        jsonModel = JsonModel.get_jsonmodel(5, "没有此方法", "");
                        context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
                        break;
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                jsonModel = JsonModel.get_jsonmodel(7, "出现异常,请通知管理员", "");
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void SetTwoMenuInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                //int Roleid = RequestHelper.int_transfer(Request, "Roleid");
                int Roleid = 1;
                int MenuPid = RequestHelper.int_transfer(Request, "MenuPid");

                //返回所有用户信息
                List<Sys_MenuInfo> MenuInfo_List = Constant.Sys_MenuInfo_List;
                List<Sys_RoleOfMenu> RoleOfMenu_List = Constant.Sys_RoleOfMenu_List;
                var query = from MenuInfo_ in MenuInfo_List
                            join RoleOfMenu_ in RoleOfMenu_List on MenuInfo_.Id equals RoleOfMenu_.Menu_Id
                            where RoleOfMenu_.Role_Id == Roleid
                            where MenuInfo_.Pid == MenuPid
                            orderby MenuInfo_.Sort
                            select new
                            {
                                Name = MenuInfo_.Name,
                                Url = MenuInfo_.Url
                            };
                jsonModel = JsonModel.get_jsonmodel(0, "success", query);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void SetMenuInfo(HttpContext context)
        {
            int intSuccess = 0;
            try
            {
                HttpRequest Request = context.Request;
                int Roleid = RequestHelper.int_transfer(Request, "Roleid");

                //返回所有用户信息
                List<Sys_MenuInfo> MenuInfo_List = Constant.Sys_MenuInfo_List;
                List<Sys_RoleOfMenu> RoleOfMenu_List = Constant.Sys_RoleOfMenu_List;
                var query = from MenuInfo_ in MenuInfo_List
                            join RoleOfMenu_ in RoleOfMenu_List on MenuInfo_.Id equals RoleOfMenu_.Menu_Id
                            where RoleOfMenu_.Role_Id == Roleid
                            where MenuInfo_.Pid == 0

                            orderby MenuInfo_.Sort
                            select new
                            {
                                Name = MenuInfo_.Name,
                                Url = MenuInfo_.Url,
                                Pid = MenuInfo_.Id
                            };
                jsonModel = JsonModel.get_jsonmodel(intSuccess, "success", query);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void TestGetPagelimits(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                string Roleid = RequestHelper.string_transfer(Request, "Rid");
                string name = RequestHelper.string_transfer(Request, "Name");
                //返回所有用户信息
                List<Sys_MenuInfo> MenuInfo_List = Constant.Sys_MenuInfo_List;
                List<Sys_RoleOfMenu> RoleOfMenu_List = Constant.Sys_RoleOfMenu_List;
                List<int> list = Split_Hepler.str_to_ints(Roleid).ToList();
                var query = from RoleOfMenu_ in RoleOfMenu_List
                            join MenuInfo_ in MenuInfo_List on RoleOfMenu_.Menu_Id equals MenuInfo_.Id
                            where list.Contains((int)RoleOfMenu_.Role_Id)
                            //where MenuInfo_.IsMenu=='1'
                            where MenuInfo_.Name == name
                            orderby MenuInfo_.Sort

                            select new
                            {
                                //Role_Id=RoleOfMenu_.Role_Id,
                                Name = MenuInfo_.Name,
                                Url = MenuInfo_.Url,
                                Pid = MenuInfo_.Pid,
                                ID = MenuInfo_.Id,
                                Description = MenuInfo_.Description,
                                IsShow = MenuInfo_.IsShow,
                                MenuCode = MenuInfo_.MenuCode
                            };
                int a = query.Count();
                var query2 = from p in query
                             group p by p.Name into allg
                             select new
                             {
                                 //Role_Id=allg.Max(p => p.Role_Id),
                                 Name = allg.Max(p => p.Name),
                                 Url = allg.Max(p => p.Url),
                                 Pid = allg.Max(p => p.Pid),
                                 ID = allg.Max(p => p.ID),
                                 Description = allg.Max(p => p.Description),
                                 IsShow = allg.Max(p => p.IsShow),
                                 MenuCode = allg.Max(p => p.MenuCode)
                             };
                int a1 = query2.Count();
                jsonModel = JsonModel.get_jsonmodel(0, "success", query2);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void TestGetMenuInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                string Roleid = RequestHelper.string_transfer(Request, "Rid");
                string isMenu= RequestHelper.string_transfer(Request, "IsMenu");
                //返回所有用户信息
                List<Sys_MenuInfo> MenuInfo_List = Constant.Sys_MenuInfo_List;
                if (!string.IsNullOrEmpty(isMenu))
                {
                    MenuInfo_List = MenuInfo_List.Where(t=>t.IsMenu==Convert.ToByte(isMenu)).ToList();
                }
                List<Sys_RoleOfMenu> RoleOfMenu_List = Constant.Sys_RoleOfMenu_List;
                List<int> list = Split_Hepler.str_to_ints(Roleid).ToList();
                var query = from RoleOfMenu_ in RoleOfMenu_List
                            join MenuInfo_ in MenuInfo_List on RoleOfMenu_.Menu_Id equals MenuInfo_.Id
                            where list.Contains((int)RoleOfMenu_.Role_Id)
                            orderby MenuInfo_.Sort
                            select new
                            {
                                IconClass=MenuInfo_.IconClass,
                                Name = MenuInfo_.Name,
                                Url = MenuInfo_.Url,
                                Pid = MenuInfo_.Pid,
                                ID = MenuInfo_.Id
                            };
                var query2 = from p in query
                             group p by p.Name into allg
                             select new
                             {
                                 IconClass = allg.Max(p => p.IconClass),
                                 Name = allg.Max(p => p.Name),
                                 Url = allg.Max(p => p.Url),
                                 Pid = allg.Max(p => p.Pid),
                                 ID = allg.Max(p => p.ID),
                             };
                jsonModel = JsonModel.get_jsonmodel(0, "success", query2);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void GetAllMenuInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;

                //返回所有用户信息
                List<Sys_MenuInfo> MenuInfo_List = Constant.Sys_MenuInfo_List;
                List<Sys_RoleOfMenu> RoleOfMenu_List = Constant.Sys_RoleOfMenu_List;

                var query = from MenuInfo_ in MenuInfo_List                         
                            orderby MenuInfo_.Sort
                            select new
                            {                              
                                Name = MenuInfo_.Name,
                                Url = MenuInfo_.Url,
                                Pid = MenuInfo_.Pid,
                                ID = MenuInfo_.Id
                            };
                var query2 = from p in query
                             group p by p.Name into allg
                             select new
                             {                              
                                 Name = allg.Max(p => p.Name),
                                 Url = allg.Max(p => p.Url),
                                 Pid = allg.Max(p => p.Pid),
                                 ID = allg.Max(p => p.ID),
                             };
                jsonModel = JsonModel.get_jsonmodel(0, "success", query2);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
        }
        public void SetRole_MenuInfo(HttpContext context)
        {
            try
            {
                List<Sys_RoleOfMenu> RoleOfMenu_List = Constant.Sys_RoleOfMenu_List;
                HttpRequest Request = context.Request;
                string Roleid = RequestHelper.string_transfer(Request, "Rid");
                string MenuId = RequestHelper.string_transfer(Request, "MenuId");
                var query = RoleOfMenu_List.Where(i => i.Role_Id.ToString() == Roleid).ToList();
                var oldChaJi = Constant.Sys_RoleOfMenu_List.Except(query).ToList();//1,3
                List<Sys_RoleOfMenu> sr_ = new List<Sys_RoleOfMenu>();
                //删除数据库
                foreach (var i in query)
                {
                    Constant.Sys_RoleOfMenuService.Delete((int)i.Id);

                }
                foreach (var i in oldChaJi)
                {
                    //Constant.Sys_RoleOfMenuService.Delete((int)i.Id);
                    sr_.Add(i);
                }
                Constant.Sys_RoleOfMenu_List = sr_;
                foreach (string i in MenuId.Split(','))
                {
                    Sys_RoleOfMenu sr = new Sys_RoleOfMenu();
                    sr.Menu_Id = Convert.ToInt32(i);
                    sr.Role_Id = Convert.ToInt32(Roleid);
                    sr.IsDelete = 0;
                    sr.EditUID = "1";
                    sr.EditTime = DateTime.Now;
                    sr.CreateUID = "1";
                    sr.CreateTime = DateTime.Now;
                    //添加数据库
                    var re = Constant.Sys_RoleOfMenuService.Add(sr);
                    sr.Id = (int)re.retData;
                    Constant.Sys_RoleOfMenu_List.Add(sr);
                }

                jsonModel.errMsg = "success";

            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }
            context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            //Constant.Sys_RoleOfMenuService.Add();

        }

        #region 获取按钮信息
        public void Get_MenuBtnInfo(HttpContext context)
        {
            try
            {
                HttpRequest Request = context.Request;
                string Roleid = RequestHelper.string_transfer(Request, "Rid");                             
                List<int> rid_list = Split_Hepler.str_to_ints(Roleid).ToList();               
                var roleOfMenu_List = Constant.Sys_RoleOfMenu_List.Where(p=> rid_list.Contains(Convert.ToInt32(p.Role_Id)));
                var sel_menu = Roleid=="1"?(from menu in Constant.Sys_MenuInfo_List                                          
                                            select new
                                            {
                                                Id = menu.Id,
                                                Pid = menu.Pid,
                                                Name = menu.Name,
                                                Url = menu.Url,
                                                IsMenu = menu.IsMenu,
                                                IconClass = menu.IconClass,
                                                MenuCode = menu.MenuCode
                                            }).Distinct():(from role in roleOfMenu_List                      
                            join menu in Constant.Sys_MenuInfo_List on role.Menu_Id equals menu.Id                            
                            select new
                            {
                                Id = menu.Id,
                                Pid = menu.Pid,
                                Name = menu.Name,
                                Url = menu.Url,
                                IsMenu=menu.IsMenu,
                                IconClass = menu.IconClass,
                                MenuCode =menu.MenuCode                                
                            }).Distinct();
                var menulist =from menu in sel_menu
                              where menu.IsMenu==0&& sel_menu.Where(p => p.Pid == menu.Id&&p.IsMenu==1).Count()>0
                              select new
                              {
                                  Id = menu.Id,
                                  Url = menu.Url
                              };
                var btnlist = sel_menu.Where(p => p.IsMenu == 1);
                var query = from menu in menulist                         
                            select new
                            {
                                 menu.Id,
                                 menu.Url,
                                 Btn= btnlist.Where(t=>t.Pid==menu.Id)
                            };
                if (query.Count() > 0)
                {
                    jsonModel = JsonModel.get_jsonmodel(0, "success", query.ToList());
                }
                else
                {
                    jsonModel = JsonModel.get_jsonmodel(3, "failed", "没有数据");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                //无论后端出现什么问题，都要给前端有个通知【为防止jsonModel 为空 ,全局字段 jsonModel 特意声明之后进行初始化】
                context.Response.Write("{\"result\":" + Constant.jss.Serialize(jsonModel) + "}");
            }
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