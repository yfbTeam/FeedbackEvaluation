<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeveAdd.aspx.cs" Inherits="FEWeb.TeaAchManage.LeveAdd" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>新增奖项等级</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>   
</head>
<body>
    <input type="hidden" name="Func" value="AddRewardLevelData" />
    <!--这两个必须放在上边-->
    <input type="hidden" name="EID" id="EID"/>
    <input type="hidden" name="Id" id="Id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="EditUID" id="EditUID" value="011" />
    <div class="main" >
        <div class="search_toobar clearfix">
            
            <div class="input-wrap">
                <label>奖项等级名称：</label><input type="text" isrequired="true" fl="奖项等级名称" id="Name" name="Name" class="text" placeholder="请输入奖项等级名称" />
            </div>
            <div class="input-wrap">
                <label>等级排序：</label>
                <input type="text" class="text" name="Sort" isrequired="true" fl="等级排序" id="Sort" placeholder="请输入奖项等级排序" style="margin-left:15px;"/>
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
    
    <script src="../Scripts/layer/layer.js"></script>
<script type="text/javascript">
    var UrlDate = new GetUrlDate();
    var index = parent.layer.getFrameIndex(window.name);
    $(function () {
        $("#CreateUID").val(GetLoginUser().UniqueNo);

        $("#EID").val(UrlDate.EID);
        if (UrlDate.Id != undefined && UrlDate.Id != "") {
            $("#Id").val(UrlDate.Id);
            BindData();
        }
    })
    function BindData() {
        $.ajax({
            url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
            type: "post",
            dataType: "json",
            data: {"Func":"GetRewardLevelData","EID":UrlDate.EID,"Id":UrlDate.Id,"IsPage":"false"},
            success: function (json) {
                if (json.result.errMsg == "success") {
                    $(json.result.retData).each(function () {
                        $("#Name").val(this.Name);
                        $("#Sort").val(this.Sort);
                    });
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
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
                if (json.result.errMsg == "success") {
                    parent.layer.msg('操作成功!');
                    parent.BindRewardLevelData(UrlDate.EID);
                    parent.CloseIFrameWindow();
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
</script>
