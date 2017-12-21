<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEvalTable.aspx.cs" Inherits="FEWeb.SysSettings.AddEvalTable" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>新增评价表</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/layout.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>


    <style type="text/css">
        .number {
            width: 50px;
            height: 30px;
            border: 1px solid #cccccc;
            border-radius: 3px;
            margin: 0px 10px;
            text-indent: 10px;
            color: #009706;
        }

        #list2 .iconfont {
            width: 34px;
            height: 34px;
            display: inline-block;
            line-height: 34px;
            text-align: center;
            cursor: pointer;
        }
    </style>


</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            <div class="sort_nav" id="threenav">
               
            </div>
            <h1 class="title mb10">
                <a style="cursor: pointer" class="reback">表格设计</a><span>&gt;</span><a href="javascript:;" class="crumbs">新增表格</a>
            </h1>
            <div class="selectwrap clearfix pr" style="margin: 0;">
                <div class="fl evaltable_left">
                    <div class="search_toobar clearfix">
                        <div class="fl">
                            <label for="">评价表名称:</label>
                            <input type="text" name="" id="Name" fl="评价表名称" isrequired="true" placeholder="请填写评价表名称" value="" class="text" style="border-right: 1px solid #cccccc; width: 500px; align-content: center">
                        </div>
                        <div class="fl ml10 checkbox">
                            <input type="checkbox" name="" id="IsScore" class="magic-checkbox">
                            <label for="IsScore">记分</label>
                            <input type="checkbox" name="" id="disalbe" class="magic-checkbox ">
                            <label for="disalbe" class="ml10">启用</label>
                        </div>

                    </div>
                </div>
                <div class="fr" style="position: absolute; right: 10px; top: 10px;">
                    <input type="button" name="" id="" value="选择指标" class="btn ml10" onclick="openIndicator()" />
                </div>
                <div class="evalheader_left fl" style="margin-top: 5px; margin-bottom: 5px">
                    <span style="float: left; margin-top: 7px">表格分项比例系数设置：</span>
                    <em class="fl">
                        <label for="A">A</label>
                        <input type="number" placeholder="0.95" value="0.95" onkeydown="onlyNum();" class="number" min="0" id="A" step="0.01">
                    </em>
                    <em class="fl">
                        <label for="B">B</label>
                        <input type="number" placeholder="0.8" value="0.8" onkeydown="onlyNum();" class="number" min="0" id="B" step="0.01">
                    </em>
                    <em class="fl">
                        <label for="C">C</label>
                        <input type="number" placeholder="0.6" value="0.6" onkeydown="onlyNum();" class="number" min="0" id="C" step="0.01">
                    </em>
                    <em class="fl">
                        <label for="D">D</label>
                        <input type="number" placeholder="0.4" value="0.4" onkeydown="onlyNum();" class="number" min="0" id="D" step="0.01">
                    </em>
                    <em class="fl">
                        <label for="E">E</label>
                        <input type="number" placeholder="0.2" value="0.2" onkeydown="onlyNum();" class="number" min="0" id="E" step="0.01">
                    </em>
                    <em class="fl">
                        <label for="F">F</label>
                        <input type="number" placeholder="0" value="0" onkeydown="onlyNum();" class="number" min="0" id="F">
                    </em>
                    <input type="button" name="" id="" value="确定" onclick="save_score_parameter();" class="btn fl ml10" style="min-width: 70px">
                </div>


            </div>
            <div class="table_header mt10 clearfix" style="min-height: 98px">
                <div class="table_header_left clearfix" style="min-height: 49px" id="list">
                    <%--<div class="fl">
                        <label for="">学院：</label>
                        <span>【计算机学院】</span>
                    </div>
                    <div class="fl">
                        <label for="">课程名称：</label>
                        <span>【计算机学院】</span>
                    </div>
                    <div class="fl">
                        <label for="">教师姓名：</label>
                        <span>【李立三】</span>
                    </div>--%>
                </div>

                <div class="table_header_left clearfix" style="min-height: 49px" id="list2">
                    <%--<div class="fl">
                        <label for="">
                            <input type="text" name="name" value="调查人数：" />
                        </label>
                        <input type="text" name="name" value="" />
                    </div>--%>
                    <%-- <div class="fl">
                        <label for="">教师姓名：</label>
                        <input type="text" name="name" value="" />
                    </div>--%>
                </div>
                <div class="table_header_right fr">
                    <input type="button" name="name" value="选择表头" class="btn2" onclick="OpenIFrameWindow('选择表头', './SelTabelHead.aspx', '700px', '340px')" />
                    <input type="button" name="name" value="自定义表头" class="btn2 mt10" onclick="add_checkItem2();" />
                </div>
            </div>
            <div class="test_module mt10">
                <div class="evalheader clearfix">
                    <div class="all fl">
                        <i class="iconfont">&#xe62c;</i>
                        全部
                    </div>
                    <div class="nodes fl">
                        <div id="sheets" style="float: left">
                            <%--  <span>教学态度</span>
                            <span>教学态度</span>--%>
                        </div>
                        <div style="float: right">
                            <input type="text" name="name" id="inputTitle" onkeydown="add_root_keydown();" value="" placeholder="名称可以为空" />
                            <input type="button" class="" onclick="add_root();" value="添加节点" />
                        </div>
                    </div>
                    <div class="evalheader_right fr">
                        <div class="fl total" style="margin-right: 10px;">
                            <span class="isscore">实时试卷总分：
							    <span><b id="total">0</b>分</span></span>
                        </div>
                        <%-- <div class="fr">
                            <input type="button" name="" value="新增指标" class="btn2" onclick="openIndicator();" />

                        </div>--%>
                    </div>
                </div>
                <div class="test_lists">
                    <ul id="text_list1">
                        <li id="default_li" style="min-height: 475px; background: #fff url(/images/no.jpg) no-repeat center center; border-bottom: none;"></li>
                    </ul>
                </div>


            </div>
            <div class="btnwrap" style="position: static; border: 1px solid #ccc; border-top: none; box-sizing: border-box;">
                <input type="button" value="保存" onclick="submit()" class="btn" />
                <input type="button" value="取消" class="btna" id="cancel" />
            </div>
        </div>
    </div>
    <footer id="footer"></footer>

</body>
</html>


<%--固定表头--%>
<script type="text/x-jquery-tmpl" id="item_check">
    <div class="fl">
        <label for="">${name}：</label>
        <span>【${description}】</span>
    </div>
</script>

<%--自由表头--%>
<script type="text/x-jquery-tmpl" id="item_check2">
    <div class="fl">
        <label for="">
            <input type="text" name="name" t_id="${t_Id}" value="${title}" />
        </label>
        <input readonly="readonly" v_id="${t_Id}" type="text" name="name" value="" />

        <i t_id="${t_Id}" style="cursor: pointer" class="iconfont">&#xe672;</i>
    </div>
</script>

<script type="text/x-jquery-tmpl" id="item_sheet">
    <div style="float: left">
        <span style="cursor: pointer" t_id="${t_Id}">${title}</span>
        <i t_id="${t_Id}" style="cursor: pointer; align-content: center" class="iconfont">&#xe672;</i>
    </div>
</script>

<script type="text/x-jquery-tmpl" id="item_indicator_title_1">
    <div class="indicator_type" ques="${QuesType_Id}">

        <%--  <h1 class="test_title clearfix">
                <input type="text" name="" id="indicator_type_tid_${indicator_type_tid}" value="${indicator_type_tname}" />
                {{if QuesType_Id!="3"}}
                <b class="b_${indicator_type_tid}">(<input type="text" id="h_${indicator_type_tid}" readonly="readonly" value="${indicator_type_value}" min="0" class="sss"/>分)</b>
                
                {{/if}}
                <div class="operates">
                    <i class="iconfont color_green up_t" onclick="t_up(this)">&#xe626;</i>
                    <i class="iconfont color_green down_t" onclick="t_down(this)">&#xe603;</i>
                </div>
            </h1>--%>
        <%--<span><b>(<input type="text" id="t_{{= $value.Id}}" value="{{= $value.OptionF_S_Max}}" onkeydown="onlyNum();" flg="sum" min="0" class="sss" />分)</b></span>--%>
        <div class="test_lists">
            <ul>
                {{each (indicator_list)}}
                <li class="sort_helper" sort="{{= $value.Id}}">
                    <input type="hidden" name="name_flg" value="{{= $value.flg}}" /><input type="hidden" name="name_in" value="{{= $value.Id}}" /><input type="hidden" name="name_title" value="${indicator_type_tid}" />
                    <h2 class="title"><span id="sp_{{= $value.flg}}"></span>、{{= $value.Name}}
                    {{if $value.QuesType_Id!="3"}}
                       
                         <span class="isscore"><b>(<input type="number" fl="{{= $value.Name}}下的分数" step="0.01" isrequired="true" id="t_${Id}" value="${OptionF_S_Max}" onkeydown="onlyNum();" flg="sum" class="number" min="0" />分)</b></span>
                        {{/if}}
                    </h2>
                    <div class="test_desc clearfix">
                        {{if $value.QuesType_Id!="3"}}
                        {{if $value.OptionA!=""}}
                        <span class="fl">
                            <input disabled="disabled" type="radio" name="radio_{{= $value.Id}}" id="radio_{{= $value.Id}}-1" value="" />
                            <label for="radio_{{= $value.Id}}-1">{{= $value.OptionA}}</label>
                            <b class="isscore">(<input type="number" step="0.01" fl="{{= $value.Name}}下的选项{{= $value.OptionA}}" isrequired="true" id="OptionA_{{= $value.Id}}" value="{{= $value.OptionA_S}}" onkeydown="onlyNum();" class="number" min="0" />分)</b>
                        </span>
                        {{/if}}
                        {{if $value.OptionB!=""}}
                        <span class="fl">
                            <input disabled="disabled" type="radio" name="radio_{{= $value.Id}}" id="radio_{{= $value.Id}}-2" value="" />
                            <label for="radio_{{= $value.Id}}-2">{{= $value.OptionB}}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionB_{{= $value.Id}}" fl="{{= $value.Name}}下的选项{{= $value.OptionB}}" isrequired="true" value="{{= $value.OptionB_S}}" onkeydown="onlyNum();" class="number" min="0" />分)</b>
                        </span>
                        {{/if}}
                        {{if $value.OptionC!=""}}
                        <span class="fl">
                            <input disabled="disabled" type="radio" name="radio_{{= $value.Id}}" id="radio_{{= $value.Id}}-3" value="" />
                            <label for="radio_{{= $value.Id}}-3">{{= $value.OptionC}}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionC_{{= $value.Id}}" fl="{{= $value.Name}}下的选项{{= $value.OptionC}}" isrequired="true" value="{{= $value.OptionC_S}}" onkeydown="onlyNum();" class="number" min="0" />分)</b>
                        </span>
                        {{/if}}
                        {{if $value.OptionD!=""}}
                        <span class="fl">
                            <input disabled="disabled" type="radio" name="radio_{{= $value.Id}}" id="radio_{{= $value.Id}}-4" value="" />
                            <label for="radio_{{= $value.Id}}-4">{{= $value.OptionD}}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionD_{{= $value.Id}}" fl="{{= $value.Name}}下的选项{{= $value.OptionD}}" isrequired="true" value="{{= $value.OptionD_S}}" onkeydown="onlyNum();" class="number" min="0" />分)</b>
                        </span>
                        {{/if}}
                        {{if $value.OptionE!=""}}
                        <span class="fl">
                            <input disabled="disabled" type="radio" name="radio_{{= $value.Id}}" id="radio_{{= $value.Id}}-5" value="" />
                            <label for="radio_{{= $value.Id}}-5">{{= $value.OptionE}}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionE_{{= $value.Id}}" fl="{{= $value.Name}}下的选项{{= $value.OptionE}}" isrequired="true" value="{{= $value.OptionE_S}}" onkeydown="onlyNum();" class="number" min="0" />分)</b>
                        </span>
                        {{/if}}
                        {{if $value.OptionF!=""}}
                        <span class="fl">
                            <input disabled="disabled" type="radio" name="radio_{{= $value.Id}}" id="radio_{{= $value.Id}}-6" value="" />
                            <label for="radio_{{= $value.Id}}-6">{{= $value.OptionF}}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionF_{{= $value.Id}}" fl="{{= $value.Name}}下的选项{{= $value.OptionF}}" isrequired="true" value="{{= $value.OptionF_S}}" onkeydown="onlyNum();" class="number" min="0" />分)</b>
                        </span>
                        {{/if}}
                        {{else}}
                        <textarea disabled="disabled" id="txt_{{= $value.Id}}"></textarea>
                        {{else}}
                        {{/if}}
                    </div>
                    <div class="operates">
                        <i class="iconfont color_purple up" onclick="up(this)">&#xe629;</i>
                        <i class="iconfont color_purple down" onclick="down(this)">&#xe62d;</i>
                        <i class="iconfont color_purple" onclick="remove1(this,{{= $value.Id}})">&#xe6e3;</i>
                    </div>
                </li>
                {{/each}}
            </ul>

        </div>
    </div>

</script>
<script src="../../Scripts/Common.js"></script>
<script src="../../Scripts/public.js"></script>
<script src="../../Scripts/linq.min.js"></script>
<script src="../../Scripts/layer/layer.js"></script>
<script src="../../Scripts/jquery.tmpl.js"></script>
<link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
<script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
<script src="../../Scripts/WebCenter/TableDesigin.js"></script>
<script src="../../Scripts/WebCenter/DatabaseMan.js"></script>
<script type="text/javascript">

    var type = getQueryString('type'); //1:编辑 1其他的则视为添加
    var table_Id = getQueryString('table_id');//表格的Id
    var indicator_array = [];//回调参数 用于数据的显示和提交
    var flg = 1;//用于生成不重复的序号，子页面进行++     
    var lisss = []; //自定义表头        
    var list_sheets = [];//试卷节点    t_Id    indicator_array    
    var select_sheet_Id;//当前选择的试卷节点     
    var select_sheet = [];   //当前的试题   
    //------------------------添加指标【打开窗体】----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    var index_1 = 0;//主要为了实时统计分，在index为1 的时候显示，否则不显示
    //var select_Array = [];//已经选择的指标，存入此数组，根据此数组，选中已选择的项
    $(function () {
        $('#top').load('/header.html');
        $('#footer').load('/footer.html');
        $('#threenav a').eq(getQueryString('selected')).addClass('selected').siblings().removeClass('selected');
        UI_Table_Create.Type = (type != '' && type != null && type != undefined) ? Number(type) : 0;
        if (UI_Table_Create.Type == 1) {
            $('.crumbs').html('编辑表格');
            UI_Table_View.PageType = 'AddEvalTable';
            UI_Table_View.Get_Eva_TableDetail_Compleate = function (data) { };
            UI_Table_View.Get_Eva_TableDetail();
        }
        //初始化准备
        UI_Table_Create.PrepareInit();
        //取消
        $("#cancel,.reback").click(function () { history.go(-1); })
    });
    //-----------数据填充----------------------------------------------------------
    //回调函数（子页面调的回调函数）
    function callback() {
        DataBaseMainModel.GetData();
        indicator_array = DataBaseMainModel.indicator_array;

        flg = DataBaseMainModel.flg;
        //select_Array = DataBaseMainModel.select_Array;
        index_1 = DataBaseMainModel.index_1;
        UI_Table_Create.CallBack_Hepler();
    }
    function save_score_parameter() {
        UI_Table_Create.save_score_parameter();
    }
    //-------------------------------------------新添节点【自定义表头】
    function add_checkItem2() {
        UI_Table_Create.add_checkItem2();
    }
    //-------------------------------------添加节点【试题】
    function add_root() {
        UI_Table_Create.add_root();
    }
    function add_root_keydown() {
        if (event.keyCode == '13') {
            add_root()
        }
    }
    //-----------选择表头【子窗体使用】------------------------------------------
    function tablehead(headvalue) {
        
        UI_Table_Create.head_value = headvalue;
        $("#list").html('');
        $("#item_check").tmpl(headvalue).appendTo("#list");
    }
    //-----------获取表头【子窗体使用】------------------------------------------
    function get_tablehead() {
        
        return UI_Table_Create.head_value;
    }
    //----------------------------------------------------------------------------------------提交表格设计
    function submit() {
        UI_Table_Create.submit_Compleate = function () {
            history.go(-1);
        };
        UI_Table_Create.submit();
    }
    function sel_CousrseType() {
        UI_Table_Create.sel_CousrseType();
    }
    //---------------------------------移除试题-----------------------------------------------------
    //移除
    function remove1(_this, id) {
       
        UI_Table_Create.remove1(_this, id);
    }
    //试题向上排序
    function up(_this) {
        UI_Table_Create.up(_this);
    }
    //试题向下排序
    function down(_this) {
        UI_Table_Create.down(_this);
    }
    //标题的向上排序
    function t_up(_this) {
        UI_Table_Create.t_up(_this);
    }
    //标题的向下排序
    function t_down(_this) {
        UI_Table_Create.t_down(_this);
    }
    //计算离开文本框的方法
    function text_blur() {
        UI_Table_Create.text_blur();
    }
    function text_change_event(element) {
        UI_Table_Create.text_change_event(element);
    }
    //选择指标
    function openIndicator() {

        if (select_sheet_Id != null && select_sheet_Id != undefined) {
            DataBaseMainModel.select_Array = [];
            if (select_sheet.indicator_array.length > 0) {
                select_sheet.indicator_array[0].indicator_list.filter(function (item) {
                    if (item.Indicator_Id == undefined) {
                        DataBaseMainModel.select_Array.push(item.Id)//比如2207
                    }
                    else {
                        DataBaseMainModel.select_Array.push(item.Indicator_Id)
                    }
                });
            }
            DataBaseMainModel.flg = flg;
            DataBaseMainModel.index_1 = index_1;
            DataBaseMainModel.indicator_array = select_sheet.indicator_array;
            DataBaseMainModel.child_list = [];
            DataBaseMainModel.FillData();
            UI_Table_Create.openIndicator();
        }
        else {
            layer.msg('请先选择一个节点');
        }
    }
    function onlyNum() {
        UI_Table_Create.onlyNum();
    }

</script>
