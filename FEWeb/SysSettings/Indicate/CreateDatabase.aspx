<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDatabase.aspx.cs" Inherits="FEWeb.Evaluation.CreateDatabase" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>选择指标</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/layout.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/x-jquery-tmpl" id="item_indicator_s">
        <div class="input-wrap" id="indicator">
            <label>指标名称：</label><input type="text" name="Name" id="Name" isrequired="true" fl="指标名称" class="text" placeholder="请填写指标名称" value="" style="width: 500px;" />
            <ul class="option_lists clearfix" id="item_list">
                <li>
                    <i class="radio"></i>
                    <input type="text" <%--name="OptionA"--%> placeholder="请填写选项A" isrequired="true" fl="选项A" value="" class="text" />
                    <%-- <i class="iconfont" onclick="remove(this)">&#xe611;</i>--%>
                </li>
                <li>
                    <i class="radio"></i>
                    <input type="text" <%--name="OptionB"--%> placeholder="请填写选项B" value="" isrequired="true" fl="选项B" class="text" />
                    <%--  <i class="iconfont" onclick="remove(this)">&#xe611;</i>--%>
                </li>
                <li><i class="radio"></i>
                    <input type="text" <%--name="OptionC"--%> placeholder="请填写选项C" value="" isrequired="true" fl="选项C" class="text" /><%--<i class="iconfont" onclick="remove(this)">&#xe611;</i>--%></li>
            </ul>
            <a href="javascript:newItem();" class="newoption"><i class="iconfont">&#xe61b;</i>新增选项</a>
        </div>
    </script>
    <script type="text/x-jquery-tmpl" id="item_indicator_w">
        <div class="input-wrap" id="indicator_w">
            <label style="line-height:60px">指标名称：</label>
            <ul class="option_lists clearfix" id="item_list_w" style="display: inline-block; padding-left: 0; float: left;">
                <li>
                    <textarea name="Name" id="Name" isrequired="true" placeholder="请填写指标名称"></textarea>
                </li>
            </ul>
        </div>
    </script>
</head>
<body>
    <input type="hidden" name="Func" value="Add_Indicator" />
    <!--这两个必须放在上边-->

    <input type="hidden" name="id" id="id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="001" />
    <input type="hidden" name="EditUID" value="001" />
    <input type="hidden" name="Type" id="indicator_Type" value="0" />

    <div class="selectwrap clearfix">
        <span class="fl cursele">当前选择：</span>
        <div class="search_toobar clearfix fl">
            <%-- <div class="fl">
                <label for="">指标分类:</label>
                <select class="select" name="IndicatorType_Id_2" isrequired="true" fl="指标分类" id="indicator_type">
                    <option value="0">--请选择--</option>
                </select>
            </div>
            <div class="fl ml10">
                <select class="select" name="IndicatorType_Id" isrequired="true" fl="指标分类" id="indicator_type_2">
                    <option value="0">--请选择--</option>
                </select>
            </div>--%>
            <div class="fl ml10">
                <label for="">题型:</label>
                <select class="select" name="QuesType_Id" id="QuesType_Id">
                    <option value="1">单选题</option>
                    <%--<option value="2">多选题</option>--%>
                    <option value="3">问答题</option>
                </select>
            </div>
            <div class="fl ml10">
                <label for="">备注:</label>
                <input type="text" name="Remarks" id="Remarks" placeholder="备注" value="" class="text" style="border-right: 1px solid #cccccc;">
            </div>
        </div>
    </div>
    <div class="main" style=" min-height: 270px;">
        <%--<div class="input-wrap" id="indicator">
            <label>指标名称：</label><input type="text" name="Name" class="text" placeholder="学生对课程的满意度调查" value="" style="width:500px;" />
            <ul class="option_lists clearfix" id="item_list">
                <li>
                    <i class="radio"></i>
                    <input type="text" name="OptionA" placeholder="非常满意" value="" class="text" />
                     <i  class="iconfont">&#xe611;</i>
                </li>
                <li>
                    <i class="radio"></i>
                    <input type="text" name="OptionB" placeholder="满意" value="" class="text" />
                    <i class="iconfont">&#xe611;</i>
                </li>
                <li><i class="radio"></i><input type="text" name="OptionC" placeholder="一般" value="" class="text" /><i class="iconfont">&#xe611;</i></li>
            </ul>
            <a href="javascript:newItem();" class="newoption"><i class="iconfont">&#xe649;</i>新增选项</a>
        </div>--%>
    </div>
    <div class="btnwrap">
        <input type="button" value="保存" onclick="submit()" class="btn" />
        <input type="button" value="取消" onclick="cancel()" class="btna" />
    </div>
</body>
</html>
<script src="../../Scripts/Common.js"></script>
 <script src="../../scripts/public.js"></script>
    
    <script src="../../Scripts/jquery.linq.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
<script type="text/javascript">
    var page = getQueryString("page");
    $("#indicator_Type").val(page);
    var index = parent.layer.getFrameIndex(window.name);//索引
    var typeid = getQueryString("typeid");//指标分类
    typeid = typeid == "undefined" ? 0 : typeid;
    //retData_type 为父页面的指标分类，indicator_name获取当前指标的信息
    var indicator_name, indicator_Parent_Id;

    $("#CreateUID").val(GetLoginUser().UniqueNo);//给creteid赋值

    var users = [{}];//随机定义的，为了使用模板，先默认单选类型的显示，
    $("#item_indicator_s").tmpl(users).appendTo(".main");//追加到main中


    $("#QuesType_Id").change(function () {//题型改变时 使用不同的模板
        $(".main").empty();//置空前把main清空
        if ($(this).val() == "3") {//如果是问答题的话
            $("#item_indicator_w").tmpl(users).appendTo(".main");//追加问答题的模板到main中
        }
        else {
            $("#item_indicator_s").tmpl(users).appendTo(".main");//追加单选或多选的模板到main中
        }
    })



    //新增选项
    function newItem() {
        if ($("#item_list li").length <= 5) {
            $("#item_list").append('<li><i class="radio"></i><input type="text" placeholder=""  value="" class="text"/><i class="iconfont" onclick="remove1(this)">&#xe61b;</i></li>');
        }
        else {
            layer.msg("超出数量,无法添加");
        }
    }

    //提交按钮
    function submit() {

        var array = ['A', 'B', 'C', 'D', 'E', 'F'];//备选项 最多为6个
        $("#indicator li").each(function (_index) {//循环选项 
            $(this).children("input[type='text']").attr("name", "Option" + array[_index]);//动态为选项赋name的值
        })

        var valid_flag = validateForm($('select,input[type="text"]'));
        if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有isrequired 值为true或false 和fl 值为不为空的名称两个属性
        {
            return false;
        }

        var select_Array_List = [];

        var select_Array_obj = new Object();

        select_Array_obj.Id = parent.flg;
        select_Array_obj.Name = $("#Name").val();

        select_Array_obj.QuesType_Id = $("#QuesType_Id").val();
        select_Array_obj.Remarks = $("#Remarks").val();
        parent.flg = parent.flg + 1;
        select_Array_obj["flg"] = parent.flg;


        $("#item_list li").each(function () {
            var txt_val = $(this).find("input[type='text']").val();
            var txt_opt = $(this).find("input[type='text']").attr("name");
            select_Array_obj[txt_opt] = txt_val;

        })
        if ($("#item_list li").length < 6) {
            select_Array_obj["OptionF"] = "";
            if ($("#item_list li").length < 5) {
                select_Array_obj["OptionE"] = "";
            }
            if ($("#item_list li").length < 4) {
                select_Array_obj["OptionD"] = "";
            }


        }
        select_Array_List.push(select_Array_obj);

        parent.indicator_array.push(select_Array_List[0]);//indicator_array 回调父页面的参数
        console.log(parent.indicator_array);

        parent.callback();//父页面的回调函数，很重要
        parent.layer.close(index);
    }

    //取消按钮
    function cancel() {
        parent.layer.close(index);
    }

    //移除选项
    function remove1(_this) {
        $(_this).parent().remove();//移除当前i元素的父元素li
    }
</script>
