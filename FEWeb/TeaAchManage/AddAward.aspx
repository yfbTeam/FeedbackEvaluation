<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAward.aspx.cs" Inherits="FEWeb.TeaAchManage.AddAward" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>奖金管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/x-jquery-tmpl" id="tr_Award">
        <tr id="tr_award_${Id}">
            <td>${rowNum}</td>
            <td><input type="number" class="text" isrequired="true" regtype="money" fl="金额" style="width:120px" value="${Money}" {{if UseCount>0}} disabled="disabled"{{/if}} min="0" step="0.01"/></td>
            <td class="operate_wrap">
                {{if UseCount>0}}
                   <div class="operate">
                         <i class="iconfont color_gray">&#xe867;</i>
                         <span class="operate_none bg_gray">保存</span>
                     </div>
                     {{if rowNum>1}} 
                    <div class="operate">
                             <i class="iconfont color_gray">&#xe61b;</i>
                             <span class="operate_none bg_gray">删除</span>
                         </div>
                    {{/if}}
                {{else}}
                    <div class="operate" onclick="submit(${Id});">
                        <i class="iconfont color_purple">&#xe867;</i>
                        <span class="operate_none bg_purple">保存</span>
                    </div>
                    {{if rowNum>1}} 
                    <div class="operate" onclick="DelReward(${Id});">
                        <i class="iconfont color_purple">&#xe61b;</i>
                        <span class="operate_none bg_purple">删除</span>
                    </div>
                    {{/if}}
                {{/if}}
            </td>
        </tr>
    </script>
</head>
<body >
    <input type="hidden" name="Func" value="AddRewardLevelData" />
    <!--这两个必须放在上边-->
    <input type="hidden" name="EID" id="EID" />
    <input type="hidden" name="Id" id="Id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="EditUID" id="EditUID" value="011" />
    <div class="main" >
        <div class="search_toobar clearfix">
            <div class="input-wrap" style="margin-bottom:10px;">
                <input type="number" fl="追加金额" isrequired="true" id="Award" regtype="money" class="text" min="0" step="0.01"  placeholder="请输入追加金额" style="margin-left:0px;"/>
                <button class="btn fl" onclick="submit(0);" style="height:33px;padding:0px;min-width:60px;margin-left:10px;">追加</button>
            </div>
        </div>
        <div class="table">
            <table>
                <thead>
                    <tr>
                        <th>批次</th>
                        <th>金额（万元）</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tb_Award"></tbody>
            </table>
        </div>
    </div>
    <div class="btnwrap">       
        <input type="button" value="关闭" class="btna" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
</body>
</html>
<script src="../Scripts/Common.js"></script>
<script src="../scripts/public.js"></script>
<script src="../Scripts/jquery.tmpl.js"></script>
<script src="../Scripts/layer/layer.js"></script>
<script type="text/javascript">
    var UrlDate = new GetUrlDate();
    $(function () {
        $("#CreateUID").val(GetLoginUser().UniqueNo);
        Get_RewardBatchData();
    });
    //绑定奖项奖金信息
    function Get_RewardBatchData() {        
        $("#tb_Award").empty();
        $.ajax({
            url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
            type: "post",
            dataType: "json",
            data: { "Func": "Get_RewardBatchData", "IsPage": "false", Reward_Id: UrlDate.Id, IsOnlyBase:0 },
            success: function (json) {
                if (json.result.errMsg == "success") {
                    $("#tr_Award").tmpl(json.result.retData).appendTo("#tb_Award");                    
                }
            },
            error: function () {}
        });
    }
    function submit(id) {
        var $award = $("#Award");
        if (id != 0) {
            $award = $("#tr_award_" + id).find("input[type='number']");
        }
        var valid_flag = validateForm($award);
        if (valid_flag != "0")
        {
            return false;
        }
        $.ajax({
            url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
            type: "post",
            dataType: "json",
            data: { "Func": "AddRewardDash", Id: id, Reward_Id: UrlDate.Id, AddAward: $award.val()},
            success: function (json) {
                if (json.result.errNum == 0) {
                    layer.msg('操作成功!');
                    if (id == 0) { $("#Award").val(''); }
                    Get_RewardBatchData();
                    parent.BindReward(UrlDate.LID);                    
                } else {
                    layer.msg(json.result.errMsg);
                }
            },
            error: function () {}
        });
    }
    function DelReward(id) {
        layer.confirm('确认删除么吗？', {
            btn: ['确定', '取消'], //按钮
            title: '操作'
        }, function (index) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "Del_RewardBatch", ItemId: id },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        layer.msg('操作成功!');
                        Get_RewardBatchData();
                        parent.BindReward(UrlDate.LID);
                    } else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function () {}
            });
        }, function () { });        
    }
</script>
