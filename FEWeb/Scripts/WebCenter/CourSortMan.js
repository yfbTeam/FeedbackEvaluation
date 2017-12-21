/// <reference path="../jquery-1.8.3.min.js" />
/// <reference path="../jquery-1.11.2.min.js" />

//===========================课程分类=================================================

var UI_Course = {
    PageType: 'CourSortMan',  //CourSortMan课程分类主页     SortCourse课程分配
    //点击选中的元素
    select_sectionid: null,
    //当前选择的课程类型
    select_CourseTypeId: null,

    //当前选择的课程类型
    select_CourseTypeName: null,
    menu_list: function () {
        //菜单中找到有ul元素的子集，并绑定click事件
        $('.menu_list').find('li:has(ul)').children('span').click(function () {

            var $next = $(this).next('ul');
            if ($next.is(':hidden')) {
                $(this).addClass('selected');
                $next.stop().slideDown();
                $next.find('li:first').addClass('selected');
                $next.find('li:first').children('em').trigger('click');
                if ($(this).parent('li').siblings().children('ul').is(':visible')) {
                    $(this).parent('li').siblings().children('span').removeClass('selected');
                    $(this).parent('li').siblings().children('ul').stop().slideUp();
                }
            } else {
                $(this).removeClass('selected');
                $next.stop().slideUp();
            }

        });



        //划过事件
        tableSlide();
        //点击样式事件
        $('.menu_list').find('li:has(ul)').find('li').click(function () {
            $('.menu_list').find('li:has(ul)').find('li').removeClass('selected');
            $(this).parent('li').addClass('selected');
            $(this).addClass('selected');

            select_sectionid = $(this).parent().parent('li').attr('sectionid');
        });
    },
    GetCourse_Type: function (SectionId) {
      
        var HasSection = true;
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            data: { func: "GetCourse_Type", "HasSection": HasSection, "SectionId": SectionId },
            dataType: "json",
            //async:true,
            success: function (json) {
                if (json.result.errMsg == "success") {


                    var retData = json.result.retData;
                    if (retData.length > 0) {
                        switch (UI_Course.PageType) {
                            case 'CourSortMan':
                                var retdata = Enumerable.From(retData).GroupBy(function (x) { return x.SectionId }).ToArray();
                                retdata = Enumerable.From(retdata).OrderByDescending(function (child) { child.source[0].SectionId }).ToArray()
                                var data = [];
                                for (var i in retdata) {
                                    var da = retdata[i].source;
                                    var objst = Enumerable.From(da).OrderBy(function (item) {
                                        return item.Sort;
                                    }).ToArray();
                                    data.push({ course_parent: da[i], objectlist: objst });
                                    continue;
                                }
                                
                                for (var i in data) {
                                    if (data[i].course_parent.Study_IsEnable == 1) {
                                        select_sectionid = data[i].course_parent.SectionId;
                                    }
                                 }
                                
                                $("#menu_listscours").empty();
                                $("#course_item").tmpl(data).appendTo("#menu_listscours");
                                UI_Course.menu_list();

                                //获取第一个课程分类的数据 
                                UI_Course.select_CourseTypeId = data[0].objectlist[0].Key;
                                UI_Course.select_CourseTypeName = data[0].objectlist[0].Value;
                                //UI_Course.GetCourseinfoBySortMan(UI_Course.select_CourseTypeId, UI_Course.select_CourseTypeName, '');
                                console.log(select_sectionid)
                                $('.menu_list').find('li:has(ul)').children('span').each(function () {
                                    if ($(this).parent('li').attr('sectionid') == select_sectionid) {
                                        var $next = $(this).next('ul');
                                        $(this).addClass('selected');
                                        $next.stop().slideDown();
                                        $(this).parent('li').find('li:first').find('em').trigger('click')
                                    }
                                })
                               
                                break;

                            case 'SortCourse':
                                //可以不用，既然是子窗体用到同样的数据，可以通过这种方式直接获取
                                $("#item_courseType").tmpl(retData).appendTo("#_currsele");
                                $("#_currsele option").each(function () {
                                    if ($(this).text() == parent.select_CourseTypeName) {
                                        $(this).attr('selected', 'selected')
                                    }
                                });
                                UI_Course.GetCourse_Type_Compleate();
                                break;

                            case 'Allot_Add_Table':
                                //可以不用，既然是子窗体用到同样的数据，可以通过这种方式直接获取
                                $("#item_courseType").tmpl(retData).appendTo("#_currsele");
                                $("#_currsele option").each(function () {
                                    if ($(this).text() == parent.select_CourseTypeName) {
                                        $(this).attr('selected', 'selected')
                                    }
                                });
                                UI_Course.GetCourse_Type_Compleate();
                                break;

                            default:
                        }

                    }
                }
            },
            error: function (errMsg) {
                layer.msg("绑定课程类别失败");
            }
        });
    },
    GetCourse_Type_Compleate: function () { },

    //设置课程分类  启用/禁用
    SetIsEnable: function (Course_Id, isenable) {

        var that = this;
        var postData = { func: "EditCourse", Ids: Course_Id, Enable: isenable, Operation: 1 };
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
            data: postData,
            dataType: "json",
            success: function (returnVal) {
                if (returnVal.result.errMsg == "success") {
                    parent.layer.msg('成功!');

                    var key = $("#select_where").val();
                    UI_Course.GetCourseInfo(pageIndex, select_sectionid, key, select_CourseTypeId);
                }
            },
            error: function (errMsg) {
                alert("失败");
            }
        });
    },

    GetCourseInfo: function (PageIndex, SectionId, Key, CourseTypeID) {

        var postData = {
            func: "GetCourseInfo",
            "SectionId": SectionId,
            "Key": Key,
            "CourseTypeID": CourseTypeID,
            "PageIndex": PageIndex,
            "PageSize": pageSize,

        };
        layer_index = layer.load(1, {
            shade: [0.1, '#fff'] //0.1透明度的白色背景
        });
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
            data: postData,
            dataType: "json",
            success: function (returnVal) {
                if (returnVal.result.errMsg == "success") {
                    reUserinfoByselect = returnVal.result.retData;

                    $('#ShowCourseInfo').empty();

                    layer.close(layer_index);
                    if (reUserinfoByselect.length <= 0) {
                        nomessage('#ShowCourseInfo');
                        $('#pageBar').hide();
                        return;
                    }
                    else {
                        $('#pageBar').show();
                    }
                    $("#Courseinfo_tmpl").tmpl(reUserinfoByselect).appendTo("#ShowCourseInfo");
                    tableSlide();

                    laypage({
                        cont: 'pageBar', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                        pages: returnVal.result.PageCount, //通过后台拿到的总页数
                        curr: returnVal.result.PageIndex || 1, //当前页
                        skip: true, //是否开启跳页
                        skin: '#CA90B0',
                        groups: 10,
                        jump: function (obj, first) { //触发分页后的回调
                            if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr                                       
                                UI_Course.GetCourseInfo(obj.curr, SectionId, Key, CourseTypeID)
                                pageIndex = obj.curr;
                            }
                        }
                    });
                    $("#itemCount").tmpl(returnVal.result).appendTo(".laypage_total");
                }
            },
            error: function (errMsg) {
                alert("失败2");
            }
        });
    },

    remove: function (id) {

        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
            data: { func: "Delete_CourseType", "Id": id },
            dataType: "json",
            success: function (json) {
                var result = json.result;
                if (result.errMsg == "success") {
                    UI_Course.GetCourse_Type();
                    layer.msg('操作成功');
                } else {
                    layer.msg(result.retData);
                }

            },
            error: function (errMsg) {
                layer.msg("操作失败");
            }
        });
    },

    Id: null,
    Name: null,
    IsEnable: null,
    Edit_Data: function () {

        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
            data: { func: "Edit_CourseType", "Id": UI_Course.Id, "Name": UI_Course.Name, "IsEnable": UI_Course.IsEnable },
            dataType: "json",
            success: function (json) {
                var result = json.result;
                var flg = false;
                if (result.errMsg == "success") {
                    flg = true;
                } else {
                    flg = false;
                }
                UI_Course.Edit_Data_Compeate(flg, result.retData);
            },
            error: function (errMsg) {
                layer.msg("操作失败");
            }
        });
    },
    Edit_Data_Compeate: function () { },

    CourseTypeName: null,
    CreateUID: null,
    IsEnable: null,
    SectionId: null,
    Add_Data: function () {
        UI_Course.CreateUID = GetLoginUser().UniqueNo;
        var postData = { func: "AddCourseType", CourseTypeName: UI_Course.CourseTypeName, CreateUID: UI_Course.CreateUID, "IsEnable": UI_Course.IsEnable, SectionId: UI_Course.SectionId };
        $.ajax({
            url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
            type: "post",
            dataType: "json",
            data: postData,//组合input标签
            success: function (json) {
                var result = json.result;
                var flg = false;
                if (result.errMsg == "success") {
                    flg = true;
                } else {
                    flg = false;
                }
                UI_Course.Add_Data_Compeate(flg, result.retData);
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },
    Add_Data_Compeate: function () { },

};


//========================================================移除课程分配========================================================
function removeCourseDis(CourseRelID, Course_Name) {
    layer.confirm('确定移除“' + Course_Name + '”吗？', {
        btn: ['确定', '取消'], //按钮
        title: '操作'
    }, function () {
        var postData = { func: "DeleteCourseDis", "CourseRelID": CourseRelID };
        $.ajax({
            url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
            type: "post",
            dataType: "json",
            data: postData,//组合input标签
            success: function (json) {
                var result = json.result;
                if (result.errMsg == "success") {
                    layer.msg('操作成功');

                    var key = $("#select_where").val();
                    UI_Course.GetCourseInfo(pageIndex, select_sectionid, key, select_CourseTypeId);
                } else {
                    layer.msg('操作失败');
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    });
}

