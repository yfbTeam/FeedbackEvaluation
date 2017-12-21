var select_sectionid = null;

//============================================================================================
function PrepareInit() {
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
}

function GetSection() {
    var Eva_Role = get_Eva_Role_by_rid();
    $.ajax({
        url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: { Func: "Get_StudySection" },
        success: function (json) {
            SectionList = json.result.retData;

            $('#item_StudySection').tmpl(SectionList).appendTo('#section');
            $('.menu_lists li').click(function () {
                $(this).addClass('selected').siblings().removeClass('selected');
            })
        },
        error: function () {
            //接口错误时需要执行的

        }
    })
}

function Get_Eva_Regular(SectionId) {

    var HasSection = true;
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
        data: { func: "Get_Eva_Regular", "SectionId": SectionId, "Type": 1 },
        dataType: "json",
        async: false,
        success: function (json) {
            if (json.result.errMsg == "success") {
                
                var retData = json.result.retData;
                var retdata = Enumerable.From(retData).GroupBy(function (x) { return x.SectionId }).ToArray();
                //retdata = Enumerable.From(retdata).OrderByDescending(function (child) { child.source[0].SectionId }).ToArray()
                var data = [];
                for (var i in retdata) {
                    var da = retdata[i].source;
                    var objst = Enumerable.From(da).OrderBy(function (item) {
                        return item.Sort;
                    }).ToArray();
                    data.push({ course_parent: da[0], objectlist: objst });
                    continue;
                }
                for (var i in data) {
                    if (data[i].course_parent.Study_IsEnable == 1) {
                        select_sectionid = data[i].course_parent.SectionId;
                    }
                }
                $("#menu_listscours").empty();
                $("#course_item").tmpl(data).appendTo("#menu_listscours");
                $('.menu_list').find('li:has(ul)').children('span').each(function () {
                    if ($(this).parent('li').attr('sectionid') == select_sectionid) {
                        var $next = $(this).next('ul');
                        $(this).addClass('selected');
                        $next.stop().slideDown();
                        $(this).parent('li').find('li:first').addClass('selected');
                        $(this).parent('li').find('li:first').find('em').trigger('click')
                    }
                })
            }
        },
        error: function (errMsg) {
            layer.msg("绑定课程类别失败");
        }
    });
}

//点击课程分类
function GetCourseinfoBySortMan(key, value, SectionId) {
    //select_CourseTypeId = key;
    //select_CourseTypeName = value;
    //select_sectionid = SectionId;
    //pageIndex = 0;
    //UI_Course.select_CourseTypeId = key;
    //UI_Course.select_CourseTypeName = value;
    //var key = $("#select_where").val();
    //UI_Course.GetCourseInfo(pageIndex, select_sectionid, key, select_CourseTypeId);
}

function Add_Eva_RegularCompleate() { }
function Add_Eva_Regular(Type) {
    var index_layer = layer.load(1, {
        shade: [0.1, '#fff'] //0.1透明度的白色背景
    });

    var postData = {
        func: "Add_Eva_Regular", "Name": $('#name').val(), "StartTime": $('#StartTime').val(), "EndTime": $('#EndTime').val(), "LookType": '0',
        "Look_StartTime": '', "Look_EndTime": '', "MaxPercent": '', "MinPercent": '', "Remarks": '', "CreateUID": cookie_Userinfo.LoginName
        , "EditUID": cookie_Userinfo.LoginName, "Section_Id": select_sectionid, "Type": Type, "TableID": '', "DepartmentIDs": ''
    };
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
        data: postData,
        dataType: "json",
        success: function (returnVal) {
            if (returnVal.result.errMsg == "success") {
                layer.msg('操作成功');
                Add_Eva_RegularCompleate();
                setTimeout(function () {
                    layer.close(index_layer);
                    parent.CloseIFrameWindow();
                }, 100)
            }
            else {
                layer.close(index_layer);
                layer.msg(returnVal.result.retData);
            }
        },
        error: function (errMsg) {

        }
    });
}
function Delete_Eva_RegularCompleate() { }
function Delete_Eva_Regular(Id) {
  
    var postData = {
        func: "Delete_Eva_Regular", "Id": Id
    };
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
        data: postData,
        dataType: "json",
        success: function (returnVal) {
            if (returnVal.result.errMsg == "success") {
                layer.msg('操作成功');
                Delete_Eva_RegularCompleate();              
            }
            else {
                layer.msg(returnVal.result.retData);
            }
        },
        error: function (errMsg) {

        }
    });
}
