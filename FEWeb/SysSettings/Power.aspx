<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Power.aspx.cs" Inherits="FEWeb.SysSettings.Power" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>权限分配</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
         .menu .menu_lists li{position:relative;}
        .menu .menu_lists li .operates {
            position: absolute;
            right: 10px;
            top: 8px;
            line-height: 22px;
            z-index: 11;
        }
    </style>
    <script type="text/x-jquery-tmpl" id="item_tr">
        <tr>
            <td>${num()}</td>
            <td>${UniqueNo}</td>
            <td>${Name}</td>
            <td>${Sex}</td>
            <td>${MajorName}</td>
            <td></td>
            <td></td>
        </tr>
    </script>
    
    <script type="text/x-jquery-tmpl" id="li_role">
        <li roleid="${RoleId}" rolename="${RoleName}" onclick="BindDataTo_GetUserinfo(${RoleId},'${RoleName}');">
            <em title="${RoleName}">${RoleName}</em>
                    
            <div class="operates">
                <div class="operate" onclick="Group_User_Add(2,'${RoleId}','${RoleName}');">
                    <i class="iconfont color_purple">&#xe632;</i>
                    <a class='operate_none bg_purple'>设置</a>
                </div>
                <div class="operate ml5" onclick="remove(${RoleId});">
                    <i class="iconfont color_purple">&#xe61b;</i>
                    <a class='operate_none bg_purple'>删除</a>
                </div>
            </div>
           
        </li>
    </script>
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            
            <div class="sortwrap clearfix">
                <div class="menu fl">
                    <h1 class="titlea">用户组管理
                    </h1>
                    <ul class="menu_lists" id="ShowUserGroup">
                    </ul>
                    <input type="button" value="新增用户组管理" class="new" onclick="Group_User_Add(1);" />
                </div>
                <div class="sort_right fr">
                    <div class="search_toobar clearfix">
                        <div class="fl">
                            <input type="text" name="" id="select_where" placeholder="请输入用户名" value="" class="text fl">
                            <a class="search fl" href="javascript:;" onclick="SelectByWhere()"><i class="iconfont">&#xe600;</i></a>
                        </div>
                        <div class="fr" id="">
                            <input type="button" name="" id="" value="权限分配" class="btn" onclick="PowerAssign();">
                            <input type="button" name="" id="allot" value="分配人员" class="btn ml10" onclick="OpenIFrameWindow('分配人员', 'AllotPeople.aspx', '1000px', '700px')" style="display: none;">
                        </div>
                    </div>
                    <div class="table">
                        <table id="fenyeShowUserInfo">
                            <thead>
                                <tr>
                                    <th width="60px">序号	</th>
                                    <th>教职工号</th>
                                    <th>用户名</th>
                                    <th>性别</th>
                                    <th>部门</th>
                                    <th>子部门</th>
                                 <%--   <th>操作</th>--%>
                                </tr>
                            </thead>
                            <tbody id="ShowUserInfo">
                            </tbody>
                        </table>
                    </div>
                    <div id="test1" class="pagination"></div>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/linq.js"></script>
    <link href="../Scripts/pagination/pagination.css" rel="stylesheet" />
    <script src="../Scripts/pagination/jquery.pagination.js"></script>
    <script src="../Scripts/WebCenter/Power.js"></script>
    <script>
        var reUserinfo,
            //点击选中的用户数据
            reUserinfoByselect,
            pageNum = 1,
            pageIndex = 0,
            pageSize = 10,
            pageCount,
            students,
            teachers,
            allDataMain = [],
            allDataMain_select = [],
            other_data_otherRole = [];

        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            UI_Power.BeginInit();
        })

        function num() {
            return UI_Power.num();
        }
        //绑定用户信息
        function BindDataTo_GetUserinfo(RoleId, RoleName) {
            UI_Power.BindDataTo_GetUserinfo(RoleId, RoleName);
        }

        function PowerAssign() {
            UI_Power.PowerAssign();
        }
        //搜索
        function SelectByWhere() {
            UI_Power.SelectByWhere();
        }

        function get_reUserinfoByselect_Ids() {
            var UniqueNos = [];
            reUserinfoByselect.filter(function (item) { UniqueNos.push(item.UniqueNo) });
            return UniqueNos;
        }

        //供子窗体使用
        function get_teachers() {
            return UI_Power.get_teachers();
        }
        //供子窗体使用
        function GetCurrentRoleid() {
            return UI_Power.GetCurrentRoleid();
        }
        //供子窗体使用
        function GetCurrentRoleName() {
            return UI_Power.GetCurrentRoleName();
        }
        //-----//获取用户信息[配合子窗体]-------------------------------------------------------------------------    

        function SelectGourp_Init() {
            UI_Power.GetUserinfo_Select();
        }

        //添加用户组
        function Group_User_Add(type, RoleId, RoleName) {
            switch (type) {
                case 1:
                    OpenIFrameWindow('新增用户组', 'Group_User_Add.aspx?type=' + type, '550px', '260px');
                    break;
                case 2:

                    BindDataTo_GetUserinfo(RoleId, RoleName);
                    UI_Power.Save();
                    OpenIFrameWindow('编辑用户组', 'Group_User_Add.aspx?type=' + type, '550px', '260px')
                    break;
                default:
            }
        }

        function remove(Id) {
            UI_Power.remove_Compleate = function () {
                SelectGourp_Init();
            };
            UI_Power.remove(Id);
        }
    </script>

</body>
</html>
