<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAchMember.aspx.cs" Inherits="FEWeb.TeaAchManage.AddAchMember" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>用户管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
</head>
<script type="text/x-jquery-tmpl" id="itemData">
    <tr>
        <td><input type='checkbox' value="${UniqueNo}" name="ss"/></td>
        <td>${UniqueNo}</td>
        <td>${Name}</td>
        <td>${Sex}</td>
        <td>${MajorName}</td>
    </tr>
</script>
<body>
    <div class="main">
        <div class="search_toobar clearfix">            
            <div class="fr">
                <div class="fl ml10">
                    <label for="">系(院):</label>
                    <select class="select" id="college" onchange="BindTeacher();" style="width: 178px"></select>
                </div>
                <div class="fl ml10">
                    <input type="text" name="key" id="key" placeholder="请输入姓名" value="" class="text fl">
                    <a class="search fl" href="javascript:search();" onclick="BindTeacher();"><i class="iconfont">&#xe600;</i></a>
                </div>
            </div>
        </div>
        <div class="table">
            <table>
                <thead>
                    <tr>
                        <th style="text-align: center; width: 40px;"><input type="checkbox" id="ck_head"/></th>
                        <th style="width: 200px">学号/教工编号</th>
                        <th style="width: 150px">用户名</th>
                        <th style="width: 150px">性别</th>
                        <th>系(院)</th>
                    </tr>
                </thead>
                <tbody id="tb_indicator"></tbody>
            </table>
            <div id="test1" class="pagination none"></div>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="确定" class="btn" onclick="SubmitInfo();" />
        <input type="button" value="取消" class="btna" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
</body>
</html>
<script src="../Scripts/Common.js"></script>
<script src="../scripts/public.js"></script>
<script src="../Scripts/linq.min.js"></script>
<script src="../Scripts/layer/layer.js"></script>
<script src="../Scripts/jquery.tmpl.js"></script>

<link href="../Scripts/pagination/pagination.css" rel="stylesheet" />
<script src="../Scripts/pagination/jquery.pagination.js"></script>
<script src="BaseUse.js"></script>
<script>
    var Teacher_Data = [], select_uniques = [];
    var pageIndex = 0, pageSize = 10;
    var UrlDate = new GetUrlDate();
    var par_tb = UrlDate.tb || 'tb_Member';
    $(function () {
        GetProfessInfo();
        GetTeachers();
        select_uniques = [];
    });
    //绑定用户
    function GetTeachers() {
        $.ajax({
            url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
            type: "post",
            dataType: "json",           
            data: {func: "GetTeachers"},
            success: function (json) {
                if (json.result.errNum.toString() == "0") {                   
                    Teacher_Data = json.result.retData;
                    var $par_Member = parent.$('#' + par_tb);//父页面已存在的成员
                    if ($par_Member) {
                        var mem_tr = $par_Member.find('tr');
                        if (mem_tr.length) {
                            var mem_Array = [];
                            $(mem_tr).each(function (i, n) {
                                mem_Array.push($(this).attr('un'));
                            });
                            Teacher_Data = Enumerable.From(Teacher_Data).Where(function (i) {
                                if ($.inArray(i.UniqueNo, mem_Array) == -1) { return i }
                            }).ToArray();
                        }
                    }
                }
                BindTeacher();
            },
            error: function (errMsg) {}
        });
    }
    var curlist = [];
    function BindTeacher() {
        $('#tb_indicator').empty();
        curlist = Teacher_Data;       
        var collegeid = $("#college").val();
        if (collegeid.length) {
            curlist = Enumerable.From(curlist).Where("x=>x.Major_ID=='" + collegeid + "'").ToArray();
        }
        var sw = $("#key").val();
        if (sw != "") {
            curlist = Enumerable.From(curlist).Where("x=>x.Name.indexOf('" + sw + "')!=-1").ToArray();
        }        
        $("#itemData").tmpl(curlist).appendTo("#tb_indicator");       
        if (curlist.length == 0) {
            $("#test1").hide();
            nomessage('#tb_indicator', 'tr',19,430);
            return;
        } else {
            $("#test1").show();
        }
        fenye(curlist.length)
    }
    function SubmitInfo() {
        if (select_uniques.length <= 0) {
            layer.msg("请勾选成员！");
            return;
        }
        var addmem = Teacher_Data;
        addmem = Enumerable.From(addmem).Where(function (i) {
            if ($.inArray(i.UniqueNo, select_uniques) != -1) { return i }
        }).ToArray();      
        parent.$("#itemData").tmpl(addmem).appendTo("#" + par_tb);
        if (par_tb == 'AuthorInfo') { parent.IsDisabled_IsOneAuthor(); }
        parent.CloseIFrameWindow();
    }
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
        $("#tb_indicator").empty();
        var arrRes = Enumerable.From(curlist).Skip(index * pageSize).Take(pageSize).ToArray();
        $("#itemData").tmpl(arrRes).appendTo("#tb_indicator");
        Table_CheckAll($('input:checkbox'));
        $("#ck_head").removeAttr('checked');
        if (select_uniques.length > 0) {
            var $check_Sub = $('input:checkbox[name=ss]');
            var seltimes = 0;
            $check_Sub.each(function () {               
                if ($.inArray(this.value, select_uniques) != -1) {
                    $(this).attr("checked", true);
                    seltimes++;
                }
            });
            $('input:checkbox')[0].checked = seltimes == $check_Sub.length;
        }
    }
    function Table_CheckAll(oInput) {
        var isCheckAll = function () {
            for (var i = 1, n = 0; i < oInput.length; i++) {
                oInput[i].checked && n++
            }
            oInput[0].checked = n == oInput.length - 1;
        };
        //全选
        oInput[0].onchange = function () {
            for (var i = 1; i < oInput.length; i++) {
                oInput[i].checked = this.checked;               
                AddORDelCkNo(oInput[i].value, $(this).is(':checked'));
            }
            isCheckAll()
        };
        //根据复选个数更新全选框状态
        for (var i = 1; i < oInput.length; i++) {
            oInput[i].onchange = function () {   //单选              
                AddORDelCkNo(this.value, $(this).is(':checked'));
                isCheckAll()
            }
        }
    }
    //数组添加或移除编号
    function AddORDelCkNo(val_No, ischeck) {
        var cindex = $.inArray(val_No, select_uniques);
        if (!ischeck) { //取消选中          
            if (cindex > -1) {
                select_uniques.splice(cindex, 1);
            }
        }
        else { //选中  
            if (cindex == -1) {
                select_uniques.push(val_No);
            }
        }
    }
</script>

