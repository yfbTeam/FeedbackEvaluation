<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RankSet.aspx.cs" Inherits="FEWeb.TeaAchManage.RankSet" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>奖项名次设置</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script type="text/x-jquery-tmpl" id="tr_Info">
        <tr id="tr_rank_${Id}" {{if UseCount==0}}class="edit_rank"{{/if}}>
            <td>第<span class="rankname">${ReplaceName(Name)}</span>名</td>
            <td><input type="number" class="text" isrequired="true" fl="分数" style="width:80px" min="1" value="${Score}" {{if UseCount>0}} disabled="disabled" {{/if}}/></td>
            <td class="operate_wrap">
                {{if UseCount==0}}
                    <div class="operate" onclick="DelRank(${Id})">
                        <i class="iconfont color_purple">&#xe61b;</i>
                        <span class="operate_none bg_purple">删除</span>
                    </div>
                {{/if}}
            </td>
        </tr>
    </script>
</head>
<body>   
    <div class="main" >
        <div class="search_toobar clearfix">
            <button class="btn fr" onclick="Add_NewRank();">新增</button>
        </div>
        <div class="table">
            <table>
                <thead>
                    <tr>
                        <th>排名</th>
                        <th>分数</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tb_Rank"></tbody>
            </table>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="确定" onclick="Save();" class="btn" />
        <input type="button" value="取消" onclick="javascript: parent.CloseIFrameWindow();" class="btna" />
    </div>
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="BaseUse.js"></script>
    <script type="text/javascript">
        var UrlDate=new GetUrlDate();
        $(function () {                  
            BindRank();
        });
        function BindRank()
        { 
            $("#tb_Rank").empty();            
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetRank", "IsPage": "false", "RId": UrlDate.RId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $("#tr_Info").tmpl(json.result.retData).appendTo("#tb_Rank");
                    } 
                },
                error: function (errMsg) {}
            });            
        }
        function Add_NewRank() {           
            var valid_flag = validateForm($('#tb_Rank tr input[type="number"]'));
            if (valid_flag != "0")
            {
                return false;
            }           
            $("#tb_Rank").prepend('<tr class="add_newrank">\
                        <td>第<input type="number" class="text rankname" isrequired="true" fl="排名" style="width:60px;margin:0px 10px;" min="1"/>名</td>\
                        <td><input type="number" class="text" isrequired="true" fl="分数" style="width:80px" min="1" /></td>\
                        <td class="operate_wrap">\
                            <div class="operate" onclick="javascript:$(this).parents(\'tr\').remove();">\
                               <i class="iconfont color_purple">&#xe61b;</i>\
                               <span class="operate_none bg_purple">删除</span>\
                            </div>\
                        </td>\
                    </tr>');//step="0.01"
        }
        function DelRank(Id)
        {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "DelRank",  "Id": Id},
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $("#tr_rank_" + Id).remove();
                    } 
                    else{
                        layer.msg(json.result.errMsg );
                    }
                },
                error: function (errMsg) {}//alert(errMsg);
            });
        }
        function Save()
        {
            var valid_flag = validateForm($('#tb_Rank tr input[type="number"]'));
            if (valid_flag != "0") {
                return false;
            }
            if (JudgeRank()) { return;}
            var addtr = $('.add_newrank'), edittr = $('.edit_rank');
            var addArray = [],editArray=[];
            $(addtr).each(function (i, n) {
                var sub_a= new Object();
                sub_a.Name ='第'+ $(this).find('input:eq(0)').val()+'名';
                sub_a.RankNum = $(this).find('input:eq(0)').val();
                sub_a.Score = $(this).find('input:eq(1)').val();
                addArray.push(sub_a)
            });
            $(edittr).each(function (i, n) {
                var sub_e = new Object();
                sub_e.Id = n.id.replace('tr_rank_', '');
                sub_e.Score = $(this).find('input').val();
                editArray.push(sub_e)
            });            
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { Func: "OperRank", RId: UrlDate.RId, AddList: addArray.length > 0 ? JSON.stringify(addArray) : '', EditList: editArray.length > 0 ? JSON.stringify(editArray) : '', CreateUID: GetLoginUser().UniqueNo },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        parent.layer.msg('操作成功!');
                        BindRank();
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function (errMsg) {layer.msg(errMsg); }
            });
        }
        function JudgeRank() {
            var isrepeat = false;
            var addElem = $('input.rankname'), editElem = $('span.rankname');
            var addArray=[],allArray = [];
            $(addElem).each(function (i, n) {
                var rval = parseInt(n.value);addArray.push(rval); allArray.push(rval);
            });
            $(editElem).each(function (i, n) {
                allArray.push(parseInt($(this).html()));
            });
            var repartArray =[];
            for (var i = 0; i < addArray.length; i++) {
                var count = 0;
                for (var j = 0; j < allArray.length; j++) {
                    if (addArray[i] == allArray[j]) { count++;}
                }
                if (count > 1) {
                    if ($.inArray(repartArray, addArray[i]) == -1) {
                        repartArray.push(addArray[i]);
                    }
                    isrepeat = true;
                }
            }
            if (repartArray.length) {            
                layer.msg('第' + $.unique(repartArray).join(',') + '名重复了！');                
            }
            return isrepeat;
        }
        function ReplaceName(name) {
            return name.replace('第', '').replace('名','');
        }
    </script>
</body>
</html>
