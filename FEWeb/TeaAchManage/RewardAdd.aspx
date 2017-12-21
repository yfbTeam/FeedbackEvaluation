<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RewardAdd.aspx.cs" Inherits="FEWeb.TeaAchManage.RewardAdd" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>新增奖项</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
</head>
<body>
    <input type="hidden" name="Func" value="AddRewardInfoData" />
    <!--这两个必须放在上边-->
    <input type="hidden" name="LID" id="LID" />
    <input type="hidden" name="Id" id="Id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="EditUID" id="EditUID" value="011" />
    <input type="hidden" name="Batch_Id" id="Batch_Id" value=""/>
    <div class="main">
            <div class="input-wrap">
                <label for="">奖项名称:</label>
                <input type="text" class="text" name="Name" id="Name" isrequired="true" fl="奖项名称" placeholder="请输入奖项名称" />
            </div>
            <div class="input-wrap">
                <label for="">记分标准:</label>
                <select class="select fl ml10" style="width: 252px" name="ScoreType" id="ScoreType" isrequired="true" fl="记分规则" onchange="ChangeUnit()">
                    <option value="1">固定分数</option>
                    <option value="2">分/万字</option>
                    <option value="3">等级递减</option>
                </select>
            </div>
            <div class="input-wrap">
                <label>奖项分数：</label><input type="number" id="Score" regtype="money" isrequired="true" fl="奖项分数" name="Score" class="text" placeholder="请输入奖项分数" min="0" step="0.01"/><span>分</span><span id="Unit" class="none">/万字</span>
            </div>
            <div class="input-wrap">
                <label>奖项金额：</label>
                <input type="text" class="text" id="Award" name="Award" regtype="money" fl="奖项金额" placeholder="请输入奖项金额" min="0" step="0.01"/><span>万元</span>
            </div>
            <div class="input-wrap">
                <label>排序：</label>
                <input type="number" class="text" id="Sort" name="Sort" isrequired="true" regtype="integer" fl="排序" placeholder="请输入排序号" min="0" step="0"/>
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
    var UrlDate = new GetUrlDate();
    var index = parent.layer.getFrameIndex(window.name);
    $(function () {
        $("#CreateUID").val(GetLoginUser().UniqueNo);
        $("#Batch_Id").val(UrlDate.batid||0);
        $("#LID").val(UrlDate.LID);
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
            data: { "Func": "GetRewardInfoData", "LID": UrlDate.LID, "Id": UrlDate.Id, "IsPage": "false" },
            success: function (json) {
                if (json.result.errMsg == "success") {
                    $(json.result.retData).each(function () {
                        $("#Name").val(this.Name);
                        if (this.RewardCount > 0) {
                            $("#ScoreType").attr('disabled', 'disabled');
                        }
                        $("#ScoreType").val(this.ScoreType);
                        if (this.ScoreCount > 0) {
                            $("#Score").attr('disabled', 'disabled');
                        }
                        $("#Score").val(this.Score);                        
                        if (this.AddRewCount > 0) {
                            $("#Award").attr('disabled', 'disabled');
                        }
                        $("#Award").val(this.Award);
                        $("#Sort").val(this.Sort);
                    })
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
    function ChangeUnit() {
        if ($("#ScoreType").val() == "2") {
            $("#Unit").show();
        }
        else {
            $("#Unit").hide();
        }
    }
    //提交按钮
    function submit() {
        //验证为空项或其他
        var valid_flag = validateForm($('select,input[type="text"],input[type="number"]'));
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
                if (json.result.errNum == 0) {
                    parent.layer.msg('操作成功!');
                    parent.BindReward(UrlDate.LID);
                    parent.CloseIFrameWindow();
                } else {
                    layer.msg(json.result.errMsg);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
</script>

