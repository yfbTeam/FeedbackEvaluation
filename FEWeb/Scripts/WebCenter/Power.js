/// <reference path="../public.js" />
/// <reference path="../Common.js" />

var UI_Power =
    {
        PageType: 'Power',  //Power用户组管理
        CurrentRoleid: 3,
        CurrentRoleName: '教师',
        num: function () {
            return pageNum++;
        },

        BeginInit: function () {

            UI_Power.GetUserinfoCompleate = function () {
                UI_Power.BindDataTo_GetUserinfo(UI_Power.CurrentRoleid, UI_Power.CurrentRoleName);
            }

            //显示用户组
            UI_Power.ShowUserGroup();
            //获取用户信息
            UI_Power.GetUserinfo();
            //获取学生
            //UI_Power.GetStudents();
            //获取教师
            UI_Power.GetTeachers();
        },

        //显示用户组
        ShowUserGroup: function () {
            var postData = { func: "Get_UserGroup" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                async: true,
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        $('#ShowUserGroup').empty();
                        var lists = returnVal.result.retData;
                        if (lists != null && lists.length > 0) {

                            $('#li_role').tmpl(lists).appendTo('#ShowUserGroup');

                            $('.menu_lists li:eq(0)').addClass('selected');
                            $('.menu_lists li').click(function () {
                                $(this).addClass('selected').siblings().removeClass('selected');
                            })
                            tableSlide();
                            $('#ShowUserGroup').find('li[roleid=' + UI_Power.CurrentRoleid + ']').trigger("click");
                            //$('#ShowUserGroup').find('li[roleid=' + UI_Power.CurrentRoleid + ']').removeClass("")
                        }
                    }
                },
                error: function (errMsg) {
                    alert("失败2");
                }
            });
        },
        ShowUserGroup_Compleate:function(){},
        //供子窗体使用
        GetCurrentRoleid: function () {
            return UI_Power.CurrentRoleid;
        },
        //供子窗体使用
        GetCurrentRoleName: function () {
            return UI_Power.CurrentRoleName;
        },
        //获取用户信息
        GetUserinfo: function () {
            var postData = { func: "Get_UserInfo_List" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        reUserinfo = returnVal.result.retData;
                        UI_Power.GetUserinfoCompleate();
                    }
                },
                error: function (errMsg) {

                }
            });
        },
        GetUserinfoCompleate: function () {
           
            UI_Power.BindDataTo_GetUserinfo(3, '教师');
        },
        //绑定用户信息
        BindDataTo_GetUserinfo: function (RoleId, RoleName) {
          
            UI_Power.CurrentRoleName = RoleName;
            UI_Power.CurrentRoleid = RoleId;

            //教师不可进行分配人员
            if (UI_Power.CurrentRoleid != 3) {
                $('#allot').show();
            } else {
                $('#allot').hide();
            }
            reUserinfoByselect = Enumerable.From(reUserinfo).Where("x=>x.Roleid=='" + RoleId + "'").ToArray();
            pageCount = reUserinfoByselect.length;
            UI_Power.fenye(pageCount);
        },
        fenye: function (pageCount) {
            $("#test1").pagination(pageCount, {
                callback: UI_Power.PageCallback,
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        },
        initclear: function (index, arrRes) {
            $("#ShowUserInfo").html('');
            if (reUserinfoByselect.length < pageSize) {
                $('.pagination').hide();
            }
            else {
                $('.pagination').show();
            }
            if (arrRes.length == 0) {
                nomessage('#ShowUserInfo');
                return;
            }
            if (index == 0) {
                pageNum = 1;
            }
            $('#item_tr').tmpl(arrRes).appendTo('#ShowUserInfo');
        },
        //翻页调用
        PageCallback: function (index, jq) {
            var arrRes = Enumerable.From(reUserinfoByselect).Skip(index * pageSize).Take(pageSize).ToArray();
            UI_Power.initclear(index, arrRes);
        },
        PowerAssign: function () {
            if (UI_Power.CurrentRoleid != null) {
                OpenIFrameWindow('权限分配', 'PowerAssign.aspx?type=' + UI_Power.CurrentRoleid + '', '600px', '500px')
            }
        },
        //搜索
        SelectByWhere: function () {
            var sw = $("#select_where").val();
            allDataMain_select = Enumerable.From(reUserinfoByselect).Where("x=>x.Name.indexOf('" + sw + "')!=-1").ToArray();
            $("#test1").pagination(allDataMain_select.length, {
                callback: function (index, jq) {
                    var arrRes = Enumerable.From(allDataMain_select).Skip(index * pageSize).Take(pageSize).ToArray();
                    UI_Power.initclear(index, arrRes);
                },
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        },
     
        get_teachers: function () {
            return teachers;

        },
      
        GetTeachers: function () {

            var postData = { func: "GetTeachers" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
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
        },
        //-----//获取用户信息[配合子窗体]-------------------------------------------------------------------------      
        GetUserinfo_Select: function () {

            allDataMain = [];
            allDataMain_select = []
            other_data_otherRole = []
            other_data_otherRole = []

            UI_Power.ShowUserGroup();
            UI_Power.GetTeachers();
            //UI_Power.GetStudents();
            //获取用户信息
            UI_Power.GetUserinfo();

        },

        remove: function (Id) {
            layer.confirm('确定要删除？', {
                btn: ['确定', '取消'], //按钮
                title: '操作'
            }, function () {
                var postData = { func: "Ope_UserGourp", Type: 3, Id: Id, UniqueNo: cookie_Userinfo.UniqueNo };
                $.ajax({
                    type: "Post",
                    url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                    data: postData,
                    dataType: "json",
                    success: function (returnVal) {
                        if (returnVal.result.errMsg == "success") {
                            layer.msg('操作成功!');
                            UI_Power.remove_Compleate();                           
                        }
                        else
                        {                         
                            layer.msg(returnVal.result.errMsg);                           
                        }
                        layer.close();
                    },
                    error: function (errMsg) {
                    }
                });
            });

        },
        remove_Compleate: function () { },
        Edit: function (Id, Name) {

            var postData = { func: "Ope_UserGourp", Type: 2, Id: Id, Name: Name, UniqueNo: cookie_Userinfo.UniqueNo };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        UI_Power.Edit_Compleate();
                    }
                },
                error: function (errMsg) {
                }
            });
        },
        Edit_Compleate: function () { },
        Add: function (Name) {

            var postData = { func: "Ope_UserGourp", Type: 1, Name: Name, UniqueNo: cookie_Userinfo.UniqueNo };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                data: postData,
                dataType: "json",
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        UI_Power.Add_Compleate();
                    }
                },
                error: function (errMsg) {
                }
            });
        },
        Add_Compleate: function () { },

        Save: function () {
            sessionStorage.setItem('UI_Power', JSON.stringify(UI_Power));
        },
        Get: function () {
            var data = JSON.parse(sessionStorage.getItem('UI_Power'));
            UI_Power.CurrentRoleid = data.CurrentRoleid;
            UI_Power.CurrentRoleName = data.CurrentRoleName;
            UI_Power.Type = data.Type;
        },

    };

