<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeaDatabase.aspx.cs" Inherits="FEWeb.SysSettings.SeaDatabase" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="renderer" content="webkit" />
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>编辑指标</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/layout.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>


    <script type="text/x-jquery-tmpl" id="item_indicator_s">
        <div class="input-wrap" id="indicator">
            <label>指标名称：</label><input type="text" name="Name" isrequired="true" fl="指标名称" class="text" placeholder="请填写指标名称" value="${Name}" style="width: 500px;" />
            <ul class="option_lists clearfix" id="item_list">
                <li>
                    <i class="radio"></i>
                    <input type="text" name="OptionA" placeholder="" isrequired="true" fl="选项A" value="${OptionA}" class="text" />
                    <%-- <i class="iconfont" onclick="remove(this)">&#xe611;</i>--%>
                </li>
                <li>
                    <i class="radio"></i>
                    <input type="text" name="OptionB" placeholder="" value="${OptionB}" isrequired="true" fl="选项B" class="text" />
                    <%--  <i class="iconfont" onclick="remove(this)">&#xe611;</i>--%>
                </li>
                <li><i class="radio"></i>
                    <input type="text" name="OptionC" placeholder="" value="${OptionC}" isrequired="true" fl="选项C" class="text" />
                {{if OptionD!=""}}
                <li><i class="radio"></i>
                    <input type="text" name="OptionD" placeholder="" value="${OptionD}" isrequired="true" fl="选项D" class="text" />
                {{/if}}
                 {{if OptionE!=""}}
                <li><i class="radio"></i>
                    <input type="text" name="OptionE" placeholder="" value="${OptionE}" isrequired="true" fl="选项E" class="text" />
                {{/if}}
                 {{if OptionF!=""}}
                <li><i class="radio"></i>
                    <input type="text" name="OptionF" placeholder="" value="${OptionF}" isrequired="true" fl="选项F" class="text" />
                {{/if}}
            </ul>

        </div>
    </script>
    <script type="text/x-jquery-tmpl" id="item_indicator_w">
        <div class="input-wrap" id="indicator_w">
            <label style="line-height: 60px">指标名称：</label>
            <ul class="option_lists clearfix" id="item_list_w" style="display: inline-block; padding-left: 0; float: left;">
                <li>
                    <textarea name="Name" placeholder="请填写指标名称" isrequired="true" style="width: 595px;">${Name}</textarea>
                </li>
            </ul>
        </div>
    </script>
</head>
<body >
    <input type="hidden" name="Func" value="Edit_Indicator" />
    <!--这两个必须放在上边-->

    <input type="hidden" name="id" id="id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="001" />
    <input type="hidden" name="EditUID" value="001" />


    <div class="main" style="margin-top: 10px; min-height: 355px;" id="tb_indicator">
    </div>
    <div class="btnwrap">
        <input type="button" value="关闭" onclick="cancel()" class="btna" />
    </div>
</body>
</html>
<script src="../../Scripts/public.js"></script>
<script src="../../Scripts/Common.js"></script>
<script src="../../scripts/jquery.linq.js"></script>
<script src="../../Scripts/linq.min.js"></script>
<script src="../../Scripts/layer/layer.js"></script>
<script src="../../Scripts/jquery.tmpl.js"></script>
<script type="text/javascript">
    var index = parent.layer.getFrameIndex(window.name);
    var typeid = getQueryString("typeid");//指标分类
    //获取要编辑的id
    var id = getQueryString("id");
    //指标的缓存，从父页面得到的
    var cache_data = [];
    cache_data = Enumerable.From(parent.retDataCache).Where("item=>item.Id==" + id + "").ToArray();//从父页面获取的缓存数据
    //为问题类型赋值
    $("#QuesType_Id").val(cache_data[0]["QuesType_Id"]);
    //为备注赋值
    $("#Remarks").val(cache_data[0]["Remarks"]);

    var users = [{}];//随机定义的，为了使用模板，先默认单选类型的显示
    //如果是问答的类型（3表示问答），则是一个文本域
    if (cache_data[0]["QuesType_Id"] == "3") {
        $("#item_indicator_w").tmpl(cache_data[0]).appendTo(".main");//追加到main中
    }
    else {
        $("#item_indicator_s").tmpl(cache_data[0]).appendTo("#tb_indicator");
    }
    //提交时需要此id 在此赋值
    $("#id").val(id);
    //retData_type 为父页面的指标分类，indicator_name获取当前指标的信息
    var indicator_name = Enumerable.From(parent.retData_type).Where("item=>item.Id==" + cache_data[0]["IndicatorType_Id"] + "").ToArray();//从父页面获取的缓存数据
    //获取当前指标下的父指标信息（一条完整的信息）
    var indicator_Parent_Id = Enumerable.From(parent.retData_type).Where("item=>item.Id==" + indicator_name[0].Parent_Id + "").ToArray();//从父页面获取的缓存数据
    //指标分类下拉框
    set_indicator_type();
    $("#CreateUID").val(GetLoginUser().UniqueNo);//给creteid赋值

    $("#indicator_type").change(function () {//一二级指标分类的 级联选择
        $("#indicator_type_2").empty();
        $("#indicator_type_2").append('<option value="0">--请选择--</option>');
        if ($(this).val() != 0) {
            set_indicator_type_2();
        }
    })
    $("#QuesType_Id").change(function () {//题型改变时 使用不同的模板
        $(".main").empty();//置空前把main清空
        if ($(this).val() == "3") {//如果是问答题的话
            $("#item_indicator_w").tmpl(users).appendTo(".main");//追加问答题的模板到main中
        }
        else {
            $("#item_indicator_s").tmpl(cache_data[0]).appendTo(".main");//追加单选或多选的模板到main中
        }
    })
    //指标一级分类
    function set_indicator_type() {
        var P_Type = get_IndicatorType_by_rid();
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Get_IndicatorType", P_Type: P_Type },
            success: function (json) {
                var retData = json.result.retData;
                retData = Enumerable.From(retData).OrderBy('$.Id').ToArray();//按Id进行升序排列
                var data_length = retData.length;
                for (var i = 0; i < data_length; i++) {
                    if (retData[i].Parent_Id == 0) {//获取分类父Id
                        //for (var j = 0; j < data_length; j++) {
                        //    if (retData[j].Parent_Id == retData[i].Id) {
                        //        $("#indicator_type").append("<option value='" + retData[j].Id + "'>" + retData[i].Name + '-' + retData[j].Name + "</option>");
                        //        $("#indicator_type").val(typeid);
                        //    }
                        //}
                        $("#indicator_type").append("<option value='" + retData[i].Id + "'>" + retData[i].Name + "</option>");
                    }
                }
                //父指标id赋值
                $("#indicator_type").val(indicator_Parent_Id[0].Id);
                set_indicator_type_2("one");
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
    //指标二级分类
    function set_indicator_type_2(type) {
        type = arguments[0] || "";//""change时调用；"one"第一次加载
        var indicator_type = $("#indicator_type").val();
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Get_IndicatorType" },
            success: function (json) {
                var retData = json.result.retData;
                retData = Enumerable.From(retData).OrderBy('$.Id').ToArray();//按Id进行升序排列
                var data_length = retData.length;
                for (var i = 0; i < data_length; i++) {
                    if (retData[i].Parent_Id == indicator_type) {//获取分类父Id
                        //for (var j = 0; j < data_length; j++) {
                        //    if (retData[j].Parent_Id == retData[i].Id) {
                        //        $("#indicator_type").append("<option value='" + retData[j].Id + "'>" + retData[i].Name + '-' + retData[j].Name + "</option>");
                        //        $("#indicator_type").val(typeid);
                        //    }
                        //}
                        $("#indicator_type_2").append("<option value='" + retData[i].Id + "'>" + retData[i].Name + "</option>");
                    }
                }
                if (type == "one") {
                    $("#indicator_type_2").val(indicator_name[0].Id);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }


    //取消按钮
    function cancel() {
        parent.layer.close(index);
    }

</script>
