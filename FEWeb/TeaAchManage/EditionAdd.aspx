<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditionAdd.aspx.cs" Inherits="FEWeb.TeaAchManage.EditionAdd" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>新增版本</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>    
</head>
<body>
    <input type="hidden" name="Func" value="AddRewardEditionData" />
    <!--这两个必须放在上边-->
    <input type="hidden" name="id" id="id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="EditUID" id="EditUID" value="011" />
    <input type="hidden" name="LID" id="LID"/>
    <div class="main">
        <div class="search_toobar clearfix">
            <div class="input-wrap">
                <label>版本名称：</label><input type="text" isrequired="true" fl="版本名称" name="Name" class="text" placeholder="请输入版本名称" />
            </div>
            <div class="input-wrap">
                <label>起止时间：</label>
                <input type="text" class="text Wdate" name="BeginTime" isrequired="true" fl="开始时间" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',position:{left:0,top:-231}})" style="width: 150px;" />
                <span style="padding-left: 10px;">~</span>
                <input type="text" class="text Wdate" name="EndTime" isrequired="true" fl="结束时间" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',position:{left:0,top:-231}})" style="width: 150px;" />
            </div>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="保存" onclick="submit()" class="btn" />
        <input type="button" value="取消" class="btna" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
</body>
</html>
    <script src="../Scripts/Common.js"></script>
    <script src="../scripts/public.js"></script>
    <script src="../Scripts/jquery.linq.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>   
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    var UrlDate =new GetUrlDate();
    var index = parent.layer.getFrameIndex(window.name);
    $(function () {
        $("#CreateUID").val(GetLoginUser().UniqueNo);
        $("#LID").val(UrlDate.LID);
    })
    //提交按钮
    function submit() {
        //验证为空项或其他
        var valid_flag = validateForm($('select,input[type="text"]'));
        if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有 isrequired 值为true或false 和fl 值为不为空的名称两个属性
        {
            return false;
        }
        $.ajax({
            url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
            type: "post",
            dataType: "json",
            data: getFromValue(),//组合input标签
            success: function (json) {
                if (json.result.errNum ==0) {
                    parent.layer.msg('操作成功!');
                    parent.BindEdition($("#LID").val());
                    parent.layer.close(index);
                } else if (json.result.errNum == -1) {
                    var warn = "";
                    $(json.result.retData).each(function (i,n) {
                        warn += "与版本-" + n.Name + "时间交叉（" + DateTimeConvert(n.BeginTime, 'yyyy-MM-dd') + "~" + DateTimeConvert(n.EndTime, 'yyyy-MM-dd') + "）；";
                    });
                    parent.layer.msg(warn);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
</script>
