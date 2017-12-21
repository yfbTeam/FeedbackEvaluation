/// <reference path="../../SysSettings/EvalTableAllot.aspx" />
/// <reference path="../layer/layer.js" />
/// <reference path="../jquery-1.11.2.min.js" />
/// <reference path="../jquery-1.8.3.min.js" />
/// <reference path="../../SysSettings/Allot_Add_Table.aspx" />
var index = parent.layer.getFrameIndex(window.name);
//var first_controlCount = 0;
function CheckEventInit() {

    //var data = parent.CourseType_Table_Dat;
    //data.filter(function (item) {
    //    if (item.TableId == inNo && course_TypeId == item.CourseTypeId) {
    //        if (select_uniques.indexOf(Number(item.TableId)) == -1) {
    //            select_uniques.push(item.TableId);
    //        }

    //    }
    //});

    var data = parent.CourseType_Table_Dat;
    data.filter(function (item) {
        
        if (course_TypeId == item.CourseTypeId) {
            if (select_uniques.indexOf(Number(item.TableId)) == -1) {
                select_uniques.push(item.TableId);
            }

        }
    });


    $('input:checkbox[name=se]').each(function () {
        var No = $(this).attr('No');
        var inNo = Number(No);
        if (select_uniques.indexOf(inNo) > -1) {
            $(this).prop('checked', 'checked');
        }

        $(this).off('click');
        $(this).on('click', function () {
            var check = $(this).prop('checked');
            var Id = $(this).attr('No');
            if (check) {
                select_uniques.push(Number(Id))
                $(this).prop('checked', 'checked');
                SameCountDealWidth();
            }
            else {
                select_uniques.remove(Number(Id));
                $(this).prop('checked', false);
                IsAll_Select = false;
                PageChange_Check();
            }
        })

        SameCountDealWidth();
    });

    function SameCountDealWidth() {
        var count = 0;
        for (var i in reUserinfoByselect) {
            if (reUserinfoByselect[i].t != undefined && reUserinfoByselect[i].t != null) {
                var id = reUserinfoByselect[i].t.Id;
                if (select_uniques.indexOf(id) > -1) {
                    count++;
                }
            }
        }

        if (count >= reUserinfoByselect.length) {
            IsAll_Select = true;
            PageChange_Check();
        }
        else {
            IsAll_Select = false;
            PageChange_Check();
        }
    }
}

function CloseW() {
    parent.layer.close(index);
}

function Check_All() {
    if (IsAll_Select) {
        $('.table').find('input:checkbox[name=se]').prop('checked', false);
        reUserinfoByselect.filter(function (item) {
            var id = Number(item.t.Id);
            if (select_uniques.indexOf(id) > -1) {
                select_uniques.remove(id)
            }

        });
        IsAll_Select = false;
    }
    else {
        $('.table').find('input:checkbox[name=se]').prop('checked', true);
        reUserinfoByselect.filter(function (item) {
            var id = Number(item.t.Id);
            if (select_uniques.indexOf(id) == -1) {
                select_uniques.push(Number(id))
            }

        });
        IsAll_Select = true;
    }

    PageChange_Check();
};
function PageChange_Check() {
    if (IsAll_Select) {
        $('#cb_all').prop('checked', true);
    }
    else {
        $('#cb_all').prop('checked', false);
    }
};

function SelectByWhere() {
    UI_Table.initdata(true);
}

var PageType = 'Allot_Add_Table';
function Add_CourseType_TableCompleate() { };
function Add_CourseType_Table(CourseTypeId, TableList, SectionId) {
    var postData = { func: "AddCourseType_Table", "CourseTypeId": CourseTypeId, "TableList": JSON.stringify(TableList), "SectionId": SectionId };
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
        data: postData,
        dataType: "json",
        success: function (json) {
            if (json.result.errMsg == "success") {
                switch (PageType) {
                    case 'Allot_Add_Table':
                        layer.msg('操作成功');

                        parent.numIndex = $('option:selected', '#_currsele').index();
                        setTimeout(function () { parent.reflesh(); parent.layer.close(index); }, 500);

                        break;
                    default:

                }
                //retData = json.result.retData;
                //$("#item_College").tmpl(retData).appendTo("#college");
            }
        },
        error: function (errMsg) {
            
            layer.msg("绑定开课单位失败");
        }
    });
}

function Delete_CourseType_Table(Id) {

    layer.confirm('您确定要删除？', {
        btn: ['确定', '取消'], //按钮
        title: '操作'
    }, function () {
        var postData = { func: "DeleteCourseType_Table", "Id": Id };
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            data: postData,
            dataType: "json",
            success: function (json) {
                if (json.result.errMsg == "success") {
                    switch (PageType) {
                        case 'EvalTableAllot':
                            layer.msg('操作成功');
                            reflesh();
                            break;
                        default:

                    }
                    //retData = json.result.retData;
                    //$("#item_College").tmpl(retData).appendTo("#college");
                }
            },
            error: function (errMsg) {
                
                layer.msg("绑定开课单位失败");
            }
        });
    });
}


//--------指定元素进删除---------------------------------------------------------------
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};


////================================================================================获取信息=============================================================================================

function GetCourseType_Table(CourseTypeId, SectionId) {
    var postData = { func: "GetCourseType_Table", "CourseTypeId": CourseTypeId, SectionId: SectionId };
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
        data: postData,
        dataType: "json",
        async: false,
        success: function (returnVal) {
            if (returnVal.result.errMsg == "success") {

                $("#tb_eva").empty();
                CourseType_Table_Dat = returnVal.result.retData;

                var key = $('#key').val();
                var daselect = CourseType_Table_Dat.filter(function (item) { return item.Name.indexOf(key) > -1 });
                if (daselect.length == 0) {
                    nomessage('#tb_eva', '', 25, 360);
                    return;
                }
                $("#item_eva").tmpl(daselect).appendTo("#tb_eva");
                tableSlide();
                $('.table').kkPages({
                    PagesClass: 'tbody tr', //需要分页的元素
                    PagesMth: 10, //每页显示个数
                    PagesNavMth: 4 //显示导航个数
                });
            }
            else {
                if (daselect.length == 0) {
                    nomessage('#tb_eva', '', 25, 360);
                    return;
                }
            }
        },
        error: function (errMsg) {
            alert("失败2");
        }
    });
}

function SelectByWhereList() {

    $("#tb_eva").empty();
    var key = $('#key').val();
    var daselect = CourseType_Table_Dat.filter(function (item) { return item.Name.indexOf(key) > -1 });
    if (daselect.length == 0) {
        nomessage('#tb_eva');
        return;
    }
    $("#item_eva").tmpl(daselect).appendTo("#tb_eva");
    tableSlide();
    $('.table').kkPages({
        PagesClass: 'tbody tr', //需要分页的元素
        PagesMth: 10, //每页显示个数
        PagesNavMth: 4 //显示导航个数
    });
}


