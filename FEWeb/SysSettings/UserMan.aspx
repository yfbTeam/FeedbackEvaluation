<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMan.aspx.cs" Inherits="FEWeb.SysSettings.UserMan" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>用户管理</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            <div class="sort_nav" id="threenav">
                <a href="UserMan.aspx" class="selected" tarurl="/SysSettings/UserMan.aspx">角色管理</a>
                <a href="ClassInfo.aspx" tarurl="/SysSettings/UserMan.aspx">教学安排</a>
                <a href="CourSortMan.aspx" tarurl="/SysSettings/UserMan.aspx">课程分类管理</a>
               
            </div>
            <div class="sortwrap clearfix">
                <div class="menu fl">
                    <h1 class="titlea">
                        角色组
                    </h1>
                    <ul class="menu_lists" id="ShowUserGroup">
                    </ul>
                    <input type="button" value="新增用户组" style="display: none" class="new" />
                </div>
                <div class="sort_right fr">
                    <div class="search_toobar clearfix">
                        <div class="fl">
                            <input type="text" name="" id="select_where" placeholder="请输用户名称" value="" class="text fl">
                            <a class="search fl" href="javascript:;" onclick="SelectByWhere()"><i class="iconfont">&#xe600;</i></a>
                        </div>
                        <div class="fr" id="operator">
                            <input type="button" name="" id="" value="分配人员" class="btn" onclick="OpenIFrameWindow('分配人员', 'AllotPeople.aspx', '1000px', '700px')">
                        </div>
                        
                    </div>
                    <div class="table">
                        <table id="fenyeShowUserInfo">
                            <thead>
                                <tr>
                                    <th width="5%">序号	
                                    </th>
                                    <th width="10%">姓名
                                    </th>
                                    <th width="10%">性别
                                    </th>
                                  <%--  <th>专业
                                    </th>--%>
                                    <th width="20%">学号/教职工号
                                    </th>
                                    <th width="20%">院系
                                    </th>
                                   <%-- <th width="20%">邮箱地址
                                    </th>--%>
                                    <th width="20%">角色
                                    </th>
                                    <th width="20%">操作
                                    </th>
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
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
    </script>
    <script>
        $(function () {
            tableSlide();
        })
        var students;
        var teachers;
        //基础数据【学生、教师】+ 角色
        var allDataMain=[];
        var allDataMain_select=[];
		    $(function () {
            GetUserinfo(true);
		        ShowUserGroup();
		        
            GetStudents(false);
            GetTeachers(true);
            GetGetUserinfoByWhere(2, '学生');
		    })
		    var reUserinfo;
		    var reUserinfoByselect;
		    var CurrentRoleName;
		    var CurrentRoleid;
        //其他角色的用户
        var other_data_otherRole = [];
		     
        //绑定用户信息
        function BindDataTo_GetUserinfo(index, bindData) {
                         
            $("#ShowUserInfo").empty();
          
		        var strall = "";
            $(bindData).each(function () {
		            var strchakan
		            if (this.Roleid == "2") {
		                strchakan = '<td class="operate_wrap caozuo"><div class="operate" onclick="GetPwd(' + this.Pwd + ',' + this.Roleid + ')" style="width:66px;height:20px;"><i class="iconfont color_blue">&#xe60b;</i> <span class="operate_none bg_blue" style="width:66px;">查看密码</span></div></td>';
		            }
                else {
		                strchakan = "<td></td>";
		            }
                var str = "<tr>" +
                                        "<td>" + index + "</td>" +
                                        "<td>" + this.Name + "</td>" +
			                            "<td>" + this.Sex + "</td>" +
			                            //"<td>" + this.College_Name + "</td>" +
			                            //"<td>" + this.Department_Name + "</td>" +
			                            //"<td>" + this.Major_Name + "</td>" +
			                            "<td>" + this.LoginName + "</td>" +
			                            "<td>" + this.MajorName + "</td>" +
			                            //"<td>" + this.Email + "</td>" + 
		            "<td><div title='" + this.RoleName + "' style='width:186px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;'>" + this.RoleName + "</div></td>" +
                                        strchakan +

                                    "</tr>";

		             strall = strall + str;
		             index++;
		            
		        });
            if (Enumerable.From(bindData).ToArray().length == 0) {
                nomessage('#ShowUserInfo');
		        }
		        $("#ShowUserInfo").append(strall);
		        tableSlide();
        }
    
        //查看密码
        function GetPwd(pwd, Roleid) {
		        if (Roleid == "2" || Roleid == "7" || Roleid == "16") {
                    
		            layer.open({
		                type: 1,
		                area: ['320px', '200px'], //宽高
		                content: '<div style="color:#4DB552;text-align:center;line-height:150px"><span style="color:#333">该学生密码为：</span>' + pwd + '</div>'
		            });

		        }
		        else {
                    layer.open({
		                type: 1,
		                area: ['320px', '200px'], //宽高
		                content: '<div style="color:#4DB552;text-align:center;line-height:150px">只可以查看学生</div>'
		            });
		        }
		    }

        //根据角色获取数据
        function GetGetUserinfoByWhere(i, name) {
            CurrentRoleName = name;
            CurrentRoleid = i;
            if (i == 2 ) {//如果是学生 隐藏
                $("#operator").hide();
                $(".caozuo").show()           
                fenye_Student(students.length);
            }
            else if(i==3)
            {
                $(".caozuo").hide()                
                fenye_Teacher(teachers.length);
            }
            else {
                $("#operator").show();
                $(".caozuo").hide()
            reUserinfoByselect = Enumerable.From(reUserinfo).Where("x=>x.Roleid=='" + i + "'").ToArray();
            fenye(reUserinfoByselect.length);
            }           
        }
        //供子窗体使用
        function GetCurrentRoleName() {
            return CurrentRoleName;
        }
        //供子窗体使用
        function GetCurrentRoleid() {
            return CurrentRoleid;
        }
        //弹出子窗体
        function AllotPeople() {
            var url = 'AllotPeople';
            OpenIFrameWindow('角色信息', url, '800px', '650px')
		    }

        //显示用户组
        function ShowUserGroup() {
            var postData = { func: "Get_UserGroup" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                async: true,
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        BindDataTo_GetUserGroup(returnVal.result.retData);

                        $('.menu_lists li').eq(0).addClass('selected').siblings().removeClass('selected');
                    }
                },
                error: function (errMsg) {
                    alert("失败2");
                }
            });
        }

        //绑定数据
        function BindDataTo_GetUserGroup(bindData) {
            var login_User = GetLoginUser();
            var Role = login_User.Sys_Role_Id;
            //
            if (Role == 16) {
                $("#ShowUserGroup").empty();
                $(bindData).each(function () {
                    if (this.RoleId == 2 || this.RoleId == 3) {
                        var str = "<li onclick=\"GetGetUserinfoByWhere('" + this.RoleId + "','" + this.RoleName + "');\" title='" + this.RoleName + "'>" + this.RoleName + "</li>"
                        $("#ShowUserGroup").append(str);
                    }
                });
            }
            else if (Role == 10) {
                $("#ShowUserGroup").empty();
                $(bindData).each(function () {
                    if (this.RoleId == 2 || this.RoleId == 3 || this.RoleId == 9 || this.RoleId == 19 || this.RoleId == 8 || this.RoleId == 7 || this.RoleId == 17) {
                        var str = "<li onclick=\"GetGetUserinfoByWhere('" + this.RoleId + "','" + this.RoleName + "');\">" + this.RoleName + "</li>"
                        $("#ShowUserGroup").append(str);
                    }
                });
            }
            else if (Role == 15 || Role == 1) {
                $("#ShowUserGroup").empty();
                $(bindData).each(function () {

                    var str = "<li onclick=\"GetGetUserinfoByWhere('" + this.RoleId + "','" + this.RoleName + "');\">" + this.RoleName + "</li>"
                    $("#ShowUserGroup").append(str);

                });
            }
            $('.menu_lists li').click(function () {

                $(this).addClass('selected').siblings().removeClass('selected');
            })


        }
        //----------搜索--------------------------------------------------------------
        function SelectByWhere() {
            var sw = $("#select_where").val();           
            $(".caozuo").show()
            //x.Name!='" + null + "'&&.Where("x=>x.Name.indexOf('" + sw + "')!=-1").ToArray();
            allDataMain_select = Enumerable.From(allDataMain).Where("x=>x.Name.indexOf('" + sw + "')!=-1").ToArray();
            fenye_Alldata(allDataMain_select.length)

            //reUserinfoByselect = Enumerable.From(reUserinfo).Where("x=>x.Name.indexOf('" + sw + "')!=-1").ToArray();

        }
        //----------提供子窗体信息--------------------------------------------------------------
        //function get_reUserinfoByselect() {

        //    var aa = Enumerable.From(reUserinfo).Except(reUserinfoByselect).ToArray();
        //    return aa;
        //    //var onlyInFirstSet = $from(reUserinfo).except(reUserinfoByselect);
        //}
        function get_students() {

            var data=[];
            for (var i = 0; i < teachers.length; i++) {
               
                data.push(teachers[i]);
            }
            for (var i = 0; i < students.length; i++) {
                data.push(students[i]);
            }
            //
            for (var i = 0; i < data.length; i++) {
                data[i].other_roleid = data[i].Roleid;
                for (var j = 0; j < other_data_otherRole.length; j++) {
                    if (data[i].UniqueNo == other_data_otherRole[j].UniqueNo) {
                        data[i].other_roleid = other_data_otherRole[j].Roleid
                    }
                }
        
        }
          
            return data;
            //var onlyInFirstSet = $from(reUserinfo).except(reUserinfoByselect);
        }
        function get_teachers() {         
            return teachers;
            //var onlyInFirstSet = $from(reUserinfo).except(reUserinfoByselect);
        }

        //----------其他组分页--------------------------------------------------------------
        var pageIndex = 0;
        var pageSize = 10;
        var pageCount;
        function fenye(pageCount) {
           
            $("#test1").pagination(pageCount, {
                callback: PageCallback,
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        }
   
    
         //翻页调用
        function PageCallback(index, jq) {
          
            var arrRes = Enumerable.From(reUserinfoByselect).Skip(index * pageSize).Take(pageSize).ToArray();
            BindDataTo_GetUserinfo(index * pageSize + 1, arrRes);
            if (reUserinfoByselect.length < pageSize) {
                $('.pagination').hide();
            }
            else {
                $('.pagination').show();
            }
         }

        //------学生基础数据分页---------------------------------------------------------------------------------------------------
        var pageIndex = 0;
        var pageSize = 10;
        var pageCount;
        function fenye_Student(pageCount) {

            $("#test1").pagination(pageCount, {
                callback: PageCallback_Student,
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        }


        //翻页调用
        function PageCallback_Student(index, jq) {

            var arrRes = Enumerable.From(students).Skip(index * pageSize).Take(pageSize).ToArray();
            BindDataTo_GetUserinfo(index * pageSize + 1, arrRes);
            if (students.length < pageSize) {
                $('.pagination').hide();
            }
            else {
                $('.pagination').show();
            }
        }

        //---------教师数据分页---------------------------------------------------------------------------------------------------
        var pageIndex = 0;
        var pageSize = 10;
        var pageCount;
        function fenye_Teacher(pageCount) {

            $("#test1").pagination(pageCount, {
                callback: PageCallback_Teacher,
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        }


        //翻页调用
        function PageCallback_Teacher(index, jq) {

            var arrRes = Enumerable.From(teachers).Skip(index * pageSize).Take(pageSize).ToArray();
            BindDataTo_GetUserinfo(index * pageSize + 1, arrRes);
            if (teachers.length < pageSize) {
                $('.pagination').hide();
            }
            else {
                $('.pagination').show();
            }
        }
        //---------所有数据进行分组---------------------------------------------------------------------------------------------------
        var pageIndex = 0;
        var pageSize = 10;
        var pageCount;
        function fenye_Alldata(pageCount) {

            $("#test1").pagination(pageCount, {
                callback: PageCallback_Alldata,
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        }


        //翻页调用
        function PageCallback_Alldata(index, jq) {
            
            var arrRes = Enumerable.From(allDataMain_select).Skip(index * pageSize).Take(pageSize).ToArray();
            BindDataTo_GetUserinfo(index * pageSize + 1, arrRes);
            if (allDataMain_select.length < pageSize) {
                $('.pagination').hide();
            }
            else {
                $('.pagination').show();
            }
        }

        //-----------获取学生、教师---------------------------------------------------------------------------------------
        function GetStudents(async) {
            var postData = { func: "GetStudents" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                async: async,
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        students = returnVal.result.retData;
                        for (var i = 0; i < students.length; i++) {
                            allDataMain.push(students[i]);
                            allDataMain_select.push(students[i]);
                        }

                     
                    }

                   
                },
                error: function (errMsg) {
                }
            });
        }

        function GetTeachers(async) {
            //var index_layer = layer.load(1, {
            //    shade: [0.1, '#fff'] //0.1透明度的白色背景
            //});
            var postData = { func: "GetTeachers" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                async: async,
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        teachers = returnVal.result.retData;
                        for (var i = 0; i < teachers.length; i++) {
                            allDataMain.push(teachers[i]);
                            allDataMain_select.push(teachers[i]);
                        }
                    }
                },
                error: function (errMsg) {
                }
            });
        }

        //获取用户信息
        function GetUserinfo(async) {
            //var index_layer = layer.load(1, {
            //    shade: [0.1, '#fff'] //0.1透明度的白色背景
            //});
            var postData = { func: "Get_UserInfo_List" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                async: async,
                success: function (returnVal) {
                    // console.log(JSON.stringify(returnVal));
                    if (returnVal.result.errMsg == "success") {
                        reUserinfo = returnVal.result.retData;
                        reUserinfoByselect = reUserinfo;                                           
                        for (var i = 0; i < reUserinfo.length; i++) {
                            if (reUserinfo[i].Roleid == 2 || reUserinfo[i].Roleid == 3)
                                continue;
                            allDataMain.push(reUserinfo[i]);
                            allDataMain_select.push(reUserinfo[i]);

                            other_data_otherRole.push(reUserinfo[i]);
                        }
                        
                        pageCount = Enumerable.From(reUserinfo).ToArray().length;

                        //BindDataTo_GetUserinfo(1,reUserinfo);

                        //fenye(pageCount);
                        //layer.close(index_layer);		                 
                    }
                },
                error: function (errMsg) {
                    //alert("失败2");
                }
            });
        }


        //-----//获取用户信息[配合子窗体]-------------------------------------------------------------------------      
        function GetUserinfo_Select() {

            allDataMain = [];
            allDataMain_select = []
            other_data_otherRole =[]
            GetTeachers(true);
            GetStudents(true);
            GetUserinfo(false);
            GetGetUserinfoByWhere(CurrentRoleid, CurrentRoleName);
        }

    </script>

</body>
</html>

