<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReleaseTask.aspx.cs" Inherits="FEWeb.Evaluation.ReleaseTask" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
        
    <script type="text/x-jquery-tmpl" id="item_classes">
        <span>
            <input type="checkbox" name="ck_class" id="" value="${ClassNO}" />
            ${Class_Name}
        </span>
    </script>
    <style>
        .row_content span input[type=radio] {
            width: 16px;
            height: 16px;
            float: left;
            display: block;
            margin: 11px 7px 9px 5px;
        }
    </style>
</head>
<body>
    <div class="main">
        <div class="input-wrap">
            <label>评价名称：</label>
            <input type="text" class="text" readonly="readonly" id="name" value="" placeholder="请填写评价名称" />
        </div>
        <div class="input-wrap">
            <label>起止时间：</label>
            <input type="text" id="StartTime" name="StartTime" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 150px; margin-left: 10px;" />
            <span style="padding-left: 10px;">~</span>
            <input type="text" id="EndTime" name="EndTime" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 150px;" />
        </div>
         <div class="input-wrap">
            <label>下发任务：</label>
             <div class="fl row_content ml5" id="radio_div">
                 <span><input type="radio" name="all" id="" value="1" checked="checked"><label for="">否</label></span>
                 <span><input type="radio" name="all" id="" value="0"><label for="">是</label></span>
            </div>
        </div>
        <div class="input-wrap pr" id="diaocha" style="display: none">
            <label>调查对象：</label>
            <div class="fl row_content ml10">
                <div class="clearfix fl" id="tb_classes">
                </div>
            </div>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="确认发布" class="btn" onclick="submit()" />
        <input type="button" value="返回修改" class="btna" onclick="parent.CloseIFrameWindow();" />
    </div>
</body>
</html>
<script src="../Scripts/Common.js"></script>
<script src="../scripts/public.js"></script>
    
    <script src="../scripts/jquery.linq.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../scripts/layer/layer.js"></script>
    <script src="../scripts/jquery.tmpl.js"></script>
    <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
    <script type="text/javascript" src="../scripts/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    var Id = getQueryString("id");
    var index = parent.layer.getFrameIndex(window.name);//索引
    var type = getQueryString("type");
    var name = getQueryString("name");
    var IsSued = 1;
    $(function () {
        $("#name").val(name);
        if (type == 0) {
            $("#diaocha").show();
        }
        $('#radio_div input[type=radio]').click(function () {

            //if ($(this).attr('value') == 0) {
            //    $('#type_ss').attr('class', 'completed').text('（必填）');
            //} else {
            //    $('#type_ss').attr('class', 'invest').text('（选填）');
            //}
            IsSued = $(this).val();

        })
        initdata();
    })
    function initdata() {
        var TeacherUID = GetLoginUser().UniqueNo;
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Get_Teacher_Class", TeacherUID: TeacherUID },
            success: function (json) {
                console.log(json);
                $("#item_classes").tmpl(json.result.retData).appendTo("#tb_classes");

            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
    function submit() {
        var TeacherUID = GetLoginUser().UniqueNo;
        var submit_data = new Object();
        submit_data.Func = "Distribution_Eva_Common";
        submit_data.Id = Id;
        submit_data.StartTime = $("#StartTime").val();
        submit_data.EndTime = $("#EndTime").val();
        submit_data.IsSued = IsSued;
        submit_data.IsPublish = 0;
        var Class_Ids = [];

        //循环获取班级的id 不是NO
        $("input[name='ck_class']").each(function () {
            if ($(this).is(":checked")) {
                Class_Ids.push($(this).val())
            }
        })

        if (IsSued == 0) {
           
            if (Class_Ids.length <= 0) {
                layer.msg("未分配班级或教师无班级");
                return false;
            }
        }

        if ($("#StartTime").val() == "") {
            layer.msg("开始时间不允许为空");
            return false;
        }
        if ($("#EndTime").val() == "") {
            layer.msg("结束时间不允许为空");
            return false;
        }

        if ($("#EndTime").val() < $("#StartTime").val()) {
            layer.msg("开始时间不允许大于结束时间");
            return false;
        }

        submit_data.Class_Ids = Class_Ids.join(',');

        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: submit_data,
            success: function (json) {
                //console.log(JSON.stringify(json));
                if (json.result.errMsg == "success") {
                    parent.layer.msg('发布成功');
                    parent.initdata('');

                    parent.CloseIFrameWindow();

                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
</script>
