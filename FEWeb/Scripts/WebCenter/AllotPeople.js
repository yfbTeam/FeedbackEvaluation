/// <reference path="../jquery-1.8.3.min.js" />
/// <reference path="../jquery-1.11.2.min.js" />
var UI_Allot =
{
    PageType: 'AllotPeople',//AllotPeople分配人员   SortCourse分配课程
    prepare_init: function () {
        $("#rolenametext").text(CurrentRoleName);
        $('#teacher_student').on('change', function () {
            roleid = $('#teacher_student').val();
            UI_Allot.data_init(roleid);
        });
        $('#college').on('change', function () {
            UI_Allot.all_change();
        })
    },

    //--------开课单位-------------------------------------------------
    //绑定开课单位
    GetProfessInfo: function () {
        $("#sel_TeaProfess").html("");
        $("#sel_TeaProfess").append('<option value="">全部</option>');
        var postData = { func: "GetProfessInfo" };
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/SysClass/ProfessInfoHandler.ashx",
            data: postData,
            dataType: "json",
            success: function (json) {
                if (json.result.errMsg == "success") {
                    switch (UI_Allot.PageType) {
                        case 'AllotPeople':
                            break;
                        case 'SortCourse':
                            break;

                        default:

                    }
                    retData = json.result.retData;
                    $("#item_College").tmpl(retData).appendTo("#college");
                }
            },
            error: function (errMsg) {
                layer.msg("绑定开课单位失败");
            }
        });
    },

    all_change: function () {
        reUserinfoByselect = selectreUserinfoAll;
      
        //课程类型
        var college = $("#college").val();
        if (college != "" && college != null && college != undefined) {
            reUserinfoByselect = Enumerable.From(reUserinfoByselect).Where("x=>x.Major_ID=='" + college + "'").ToArray();
        }
     
        var sw = $("#key").val();
        if (sw != "") {
            reUserinfoByselect = Enumerable.From(reUserinfoByselect).Where("x=>x.Roleid=='" + roleid + "'&&x.Name.indexOf('" + sw + "')!=-1").ToArray();
        }
        else {
            //reUserinfoByselect = Enumerable.From(reUserinfoByselect).ToArray();
        }
      
     
        UI_Allot.fenye(reUserinfoByselect.length);

        
        if (IsAll_Select) {
            select_uniques = [];
            reUserinfoByselect.filter(function (item) { select_uniques.push(item.UniqueNo) });
        }
    },
    //-----------分页---------------------------------------------------------------------------------------

    fenye: function (pageCount) {

        $("#test1").pagination(pageCount, {
            callback: UI_Allot.PageCallback,
            prev_text: '上一页',
            next_text: '下一页',
            items_per_page: pageSize,
            num_display_entries: 4,//连续分页主体部分分页条目数
            current_page: pageIndex,//当前页索引
            num_edge_entries: 1//两侧首尾分页条目数
        });
    },

    //翻页调用
    PageCallback: function (index, jq) {
      
        var arrRes = Enumerable.From(reUserinfoByselect).Skip(index * pageSize).Take(pageSize).ToArray();
        UI_Allot.BindDataTo_GetUserinfo(arrRes);
        UI_Allot.PageChange_Check();
    },
    data_init: function (roleid) {

        //reUserinfoByselect = Enumerable.From(reUserinfoAll).Where("x=>x.Roleid=='" + roleid + "'").ToArray();

        reUserinfoByselect = reUserinfoAll.filter(function (item) { return item.Roleid != 2 });
        //alert(roleid);
        //alert(reUserinfoByselect.length)
        select_uniques = [];
        //数组合并
        for (var i = 0; i < reUserinfoByselect.length; i++) {
            var index = isHasElement(reUserinfoByselect_uniques, reUserinfoByselect[i].UniqueNo);
            if (index > -1) {
                select_uniques.push(reUserinfoByselect[i].UniqueNo);
                reUserinfoByselect[i].Roleid = CurrentRoleid;
            }
        }

        


        //临时集合存储
        selectreUserinfoAll = reUserinfoByselect;
       
        UI_Allot.all_change();
    },

    //-----------绑定数据--------------------------------------------------------------------------------------
    BindDataTo_GetUserinfo: function (bindData) {

        

        $("#tb_indicator").empty();
        $("#itemData").tmpl(bindData).appendTo("#tb_indicator");
        //$("#tb_indicator").append(strall);
        if (Enumerable.From(bindData).ToArray().length == 0) {
            nomessage('#tb_indicator');
        }
        tableSlide();
        $('input:checkbox[name=se]').each(function () {
            ;
            $(this).on('click', function () {
                var check = $(this).attr('checked');
                var UniqueNo = $(this).attr('UniqueNo');
                if (check != undefined) {
                    select_uniques.remove(UniqueNo);
                    $(this).removeAttr('checked');

                }
                else {
                    select_uniques.push(UniqueNo)
                    $(this).attr('checked', 'checked');
                }
            })
        });
        //var ischeck = $(this).attr('checked')
    },

    //-----------提交信息---------------------------------------------------------------------------------------  
    SubmitUserinfo: function () {
        //var CurrentRoleName = parent.GetCurrentRoleName();
        var UniqueNos = "";
        for (var i = 0; i < select_uniques.length; i++) {
            if (i == 0) {
                UniqueNos = select_uniques[i];
            } else {
                UniqueNos += ("," + select_uniques[i]);
            }
        }
        var postData = { func: "SetUserToRole", UniqueNo: UniqueNos, Roleid: CurrentRoleid, BackRoleid: roleid };
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
            data: postData,
            dataType: "json",
            async: true,
            success: function (returnVal) {
                if (returnVal.result.errMsg == "success") {
                    UI_Allot.SubmitUserinfo_Compleate(true);
                }
            },
            error: function (errMsg) {
                UI_Allot.SubmitUserinfo_Compleate(false);
            }
        });
    },
    SubmitUserinfo_Compleate: function (result) { },

    Check_All: function () {
        select_uniques = [];
        if (IsAll_Select) {
            $('.table').find('input[type="checkbox"]').attr('checked', false);            
            IsAll_Select = false;
        }
        else {
            $('.table').find('input[type="checkbox"]').attr('checked', true);
            reUserinfoByselect.filter(function (item) { select_uniques.push(item.UniqueNo) });
            IsAll_Select = true;
        }

        UI_Allot.PageChange_Check();
    },

    PageChange_Check: function () {
        if (IsAll_Select) {
            $('.table').find('input[type="checkbox"]').attr('checked', true);
        }
    },
};

//--------指定元素进删除---------------------------------------------------------------
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};






