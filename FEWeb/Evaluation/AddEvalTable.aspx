<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEvalTable.aspx.cs" Inherits="FEWeb.Evaluation.AddEvalTable" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    

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
    </style>
    <script type="text/x-jquery-tmpl" id="item_indicator_title_1">
        <li>
            <input type="hidden" name="name_in" value="${Id}" /><input type="hidden" name="name_Indicator_Id" value="${Indicator_Id}" /><input type="hidden" name="name_flg" value="${flg}" />
            <h2 class="title"><span id="sp_${flg}"></span>、${Name}
                    {{if QuesType_Id!="3"}}
                        <span class="isscore"><b>(<input type="text" id="t_${Id}" value="${OptionF_S_Max}" readonly="readonly" min="0" class="sss"/>分)</b></span>
                {{/if}}
            </h2>
            <div class="test_desc">
                {{if QuesType_Id!="3"}}
                        {{if OptionA!=""}}
                        <span>
                            <input type="radio" name="radio_${Id}" id="radio_${Id}-1" value="" />
                            <label for="radio_${Id}-1">${OptionA}</label>
                            <b class="isscore">(<input type="number" step="0.01" fl="${Name}下的选项${OptionA}" isrequired="true" id="OptionA_${flg}" value="${OptionA_S}" class="number" min="0" />分)</b>
                        </span>
                {{/if}}
                        {{if OptionB!=""}}
                        <span>
                            <input type="radio" name="radio_${Id}" id="radio_${Id}-2" value="" />
                            <label for="radio_${Id}-2">${OptionB}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionB_${flg}" fl="${Name}下的选项${OptionB}" isrequired="true" value="${OptionB_S}" class="number" min="0" />分)</b>
                        </span>
                {{/if}}
                        {{if OptionC!=""}}
                        <span>
                            <input type="radio" name="radio_${Id}" id="radio_${Id}-3" value="" />
                            <label for="radio_${Id}-3">${OptionC}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionC_${flg}" fl="${Name}下的选项${OptionC}" isrequired="true" value="${OptionC_S}" class="number" min="0" />分)</b>
                        </span>
                {{/if}}
                        {{if OptionD!="" && OptionD!=undefined}}
                        <span>
                            <input type="radio" name="radio_${Id}" id="radio_${Id}-4" value="" />
                            <label for="radio_${Id}-4">${OptionD}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionD_${flg}" fl="${Name}下的选项${OptionD}" isrequired="true" value="${OptionD_S}" class="number" min="0" />分)</b>
                        </span>
                {{/if}}
                        {{if OptionE!="" && OptionE!=undefined}}
                        <span>
                            <input type="radio" name="radio_${Id}" id="radio_${Id}-5" value="" />
                            <label for="radio_${Id}-5">${OptionE}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionE_${flg}" fl="${Name}下的选项${OptionE}" isrequired="true" value="${OptionE_S}" class="number" min="0" />分)</b>
                        </span>
                {{/if}}
                        {{if OptionF!="" && OptionE!=undefined}}
                        <span>
                            <input type="radio" name="radio_${Id}" id="radio_${Id}-6" value="" />
                            <label for="radio_${Id}-6">${OptionF}</label>
                            <b class="isscore">(<input type="number" step="0.01" id="OptionF_${flg}" fl="${Name}下的选项${OptionF}" isrequired="true" value="${OptionF_S}" class="number" min="0" />分)</b>
                        </span>
                {{/if}}
                        {{else}}
                        <textarea id="txt_${flg}"></textarea>
                {{else}}
                        {{/if}}
            </div>
            <div class="operates">
                <i class="iconfont color_purple up" onclick="up(this)">&#xe629;</i>
                <i class="iconfont color_purple down" onclick="down(this)">&#xe62d;</i>
                <i class="iconfont color_purple" onclick="remove1(this,${Id})">&#xe61b;</i>
            </div>
        </li>
    </script>
</head>
<body>
    <div id="top">
       
    </div>
   <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            <div class="selectwrap clearfix pr" style="margin: 0;">
                <div class="fl evaltable_left">
                    <div class="search_toobar clearfix">
                        <div class="fl">
                            <label for="">评价表名称:</label>
                            <input type="text" name="Name" id="Name" fl="评价表名称" isrequired="true" placeholder="请填写评价表名称" value="" class="text" style="border-right: 1px solid #cccccc;width:200px">
                        </div>
                        <%-- <div class="fl ml10">
                            <label for="">起止时间:</label>
                            <input type="text" class="text Wdate" id="StartTime" name="StartTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 110px; border-right: 1px solid #cccccc;" />
                            <span>~</span>
                            <input type="text" class="text Wdate" id="EndTime" name="EndTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 110px; border-right: 1px solid #cccccc;" />
                        </div>--%>
                        <div class="fl ml10">
                            <label for="">是否记分:</label>
                            <input type="radio" checked="checked" name="IsScore" id="IsScore">
                            <label for="">是</label>
                            <input type="radio" name="IsScore" id="isNotScore">
                            <label for="">否</label>
                        </div>
                        <div class="fl ml10">
                            <label for="">备注:</label>
                            <input type="text" name="Remarks" id="Remarks" maxlength="10" fl="备注" placeholder="限填十字以内" value="" class="text" style="border-right: 1px solid #cccccc; width: 140px;width:200px">
                        </div>
                    </div>
                </div>
                <div class="fr add">
                    <input type="button" name="" id="" value="选择指标" class="btn" onclick="openIndicator()">
                </div>
            </div>

            <div class="test_module mt10">
                <div class="evalheader clearfix">
                    <%-- <div class="all fl">
                        <i class="iconfont">&#xe62c;</i>
                        全部
                    </div>--%>
                    <div class="evalheader_right fr">
                        <div class="fl total">
                            <span class="isscore">实时试卷总分：
							    <span><b id="total">0</b>分</span></span>
                        </div>
                        <div class="fr">
                            <input type="button" name="" id="" value="创建指标" class="btn" onclick="openIndicator1()" />
                        </div>
                    </div>
                </div>
                <%--                 <h1 class="test_title clearfix">
                    <input type="text" name="" id="" value="教学态度" />
                    <b>(80分)</b>
                    <div class="operates">
                        <i class="iconfont color_gray">&#xe626;</i>
                        <i class="iconfont color_green">&#xe603;</i>
                    </div>
                </h1>--%>
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
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
   
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        var indicator_array = [];//回调参数 用于数据的显示和提交
        var flg = 1;//用于生成不重复的序号，子页面进行++

        var name_in = 9999999;


        //回调函数（子页面调的回调函数）
        function callback() {

            $("#text_list1").empty();//先清空目标的元素
            $("#default_li").hide();//默认的图片隐藏
            console.log(JSON.stringify(indicator_array));
            indicator_array = Enumerable.From(indicator_array).OrderBy("item=>item.QuesType_Id").ToArray();//按题的类型排序，若是问答题则 排列在最下边
            $("#item_indicator_title_1").tmpl(indicator_array).appendTo("#text_list1");//填充数据
            $("#text_list1 li").each(function (index) {
                var Id = $(this).children("input[name='name_in']").val();
                var name_flg = $(this).children("input[name='name_flg']").val();
                $("#sp_" + name_flg).html((index + 1));
                if ($(this).index() == 0) {//试题首行的图标置灰
                    $(this).find('.iconfont:eq(0)').removeClass("color_green").addClass("color_gray");

                }
                if ($(this).index() == f_v - 1) {//试题尾行的图标置灰
                    $(this).find('.iconfont:eq(1)').removeClass("color_green").addClass("color_gray");

                }
            })
            //for (var i = 0; i < indicator_array.length; i++) {
            //    $("#item_indicator_title_1").tmpl(indicator_array[i].indicator_list).appendTo("#text_list1");//填充数据
            //}
            if ($("#isNotScore").is(":checked") == true) {
                $(".isscore").hide();
            }

            //文本框的数字显示
            text_blur();

            //// 设置默认的第一个和最后一个置灰\
            var f_v = $("#text_list1 li").length;

        }
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            $("#isNotScore").click(function () {
                $(".isscore").hide();
            })
            $("#IsScore").click(function () {
                $(".isscore").show();
            })
            //SetMenu();
        })


        //提交
        function submit() {
            for (var i = 0; i < indicator_array.length; i++) {
                var Eva_TableDetail = indicator_array[i];
                var Id = Eva_TableDetail.Id;//获取指标的id
                var n_flg = Eva_TableDetail.flg;//获取指标的id
                Eva_TableDetail["OptionA_S"] = $("#OptionA_" + n_flg).val();//赋值
                Eva_TableDetail["OptionB_S"] = $("#OptionB_" + n_flg).val();
                Eva_TableDetail["OptionC_S"] = $("#OptionC_" + n_flg).val();
                Eva_TableDetail["IndicatorType_Id"] = 0;
                Eva_TableDetail["IndicatorType_Name"] = "";
                var d_v = "0";
                if ($("#OptionD_" + n_flg).val() != undefined) {//值为undifined的情况
                    d_v = $("#OptionD_" + n_flg).val();
                }
                Eva_TableDetail["OptionD_S"] = d_v;

                var e_v = "0";
                if ($("#OptionE_" + n_flg).val() != undefined) {
                    e_v = $("#OptionE_" + n_flg).val();
                }
                Eva_TableDetail["OptionE_S"] = e_v;
                var f_v = "0";
                if ($("#OptionF_" + n_flg).val() != undefined) {
                    f_v = $("#OptionF_" + n_flg).val();
                }
                Eva_TableDetail["OptionF_S"] = f_v;

                //排序后的索引字段  按指标类型和 题的组合
                if (Eva_TableDetail.QuesType_Id != 3) {
                    var Sort = ($("#OptionA_" + n_flg).parents('li').index() + 1);
                    Eva_TableDetail["Sort"] = Sort;
                }
                else {
                    var Sort = ($("#txt_" + n_flg).parents('li').index() + 1);
                    Eva_TableDetail["Sort"] = Sort;
                }
            }
            console.log(indicator_array);
            var Name = $("#Name").val();
            var IsScore = "0";
            if ($("#IsScore").prop("checked")) {
                IsScore = "0";
            }
            else {
                IsScore = "1";
            }
            var Remarks = $("#Remarks").val();
            //var StartTime = $("#StartTime").val();
            //var EndTime = $("#EndTime").val();
            var CreateUID = GetLoginUser().UniqueNo;
            var EditUID = GetLoginUser().UniqueNo;
            //验证 为0表示验证通过
            if ($("#isNotScore").is(":checked") == false) {
                var valid_flag = validateForm($('input[type="text"],input[class="number"]'));
                if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有isrequired 值为true或false 和fl 值为不为空的名称两个属性
                {
                    return false;
                }
            }
            else {
                var valid_flag = validateForm($('#Name'));
                if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有isrequired 值为true或false 和fl 值为不为空的名称两个属性
                {
                    return false;
                }
            }

            if (indicator_array.length <= 0) {
                layer.msg('您还未选择指标！');
                return false;
            }

            var submit_data = new Object();
            submit_data.func = "Add_Eva_Common";
            submit_data.Name = Name;
            submit_data.IsScore = IsScore;
            submit_data.Remarks = Remarks;
            submit_data.CreateUID = CreateUID;
            submit_data.EditUID = EditUID;
            submit_data.List = JSON.stringify(indicator_array);
            //submit_data.StartTime = StartTime;
            //submit_data.EndTime = EndTime;
            submit_data.TeacherUID = GetLoginUser().UniqueNo;
            submit_data.CourseRoom_Id = 1;
            submit_data.FullScore = 100;
            submit_data.Range = '';
            submit_data.Status = 0;
            submit_data.StudySection_Id = 1;
            submit_data.Type = 0;

            console.log(submit_data);

            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: submit_data,//组合input标签
                success: function (json) {
                    //console.log('aa');
                    //console.log(json);
                    //console.log('aa');
                    if (json.result.errMsg == "success") {
                        window.location.href = "ClassEval.aspx?Id=" + getQueryString('Id') + '&Iid=' + getQueryString('Iid');//操作成功后跳转列表页
                        layer.msg('操作成功!');
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }

        //取消
        $("#cancel").click(function () {
            window.location.href = "ClassEval.aspx?Id=" + getQueryString('Id') + '&Iid=' + getQueryString('Iid');//操作成功后跳转列表页
        })

        //移除
        function remove1(_this, id) {
            //移除已加入select_Array中的数组，这样就不会在选择指标时再显示移除的元素了
            for (var i = 0; i < select_Array.length; i++) {
                if (select_Array[i] == id) {
                    select_Array.splice(i, 1);
                }
            }
            //移除数组中的内容，才算具体的删除
            for (var i = 0; i < indicator_array.length; i++) {
                if (indicator_array[i].Id == id) {//如果遍历的数组的id和当前的id一样，然后删除这个内容splice
                    //indicator_array[i].indicator_list.splice(j, 1);//不支持ie
                    Array.prototype.splice.call(indicator_array, i, 1);
                }
            }
            if (indicator_array.length == 0) {
                $(".test_module").html('<div class="test_lists"> <ul id="text_list1"><li id="default_li" style="min-height: 475px; background: #fff url(/images/no.jpg) no-repeat center center; border-bottom: none;"></li></ul></div>');
            }
            $(_this).parents("li").remove();//移除当前的试题
        }

        //试题向上排序
        function up(_this) {
            var _li = $(_this).parents("li");
            var len = _li.length;
            if (_li.index() != 0) {
                _li.fadeOut().fadeIn();
                _li.prev().before(_li);
                $(_this).parents("ul").children('li').find('.down').removeClass("color_gray").addClass("color_green");
                $(_this).parents("ul").children('li').find('.down').last().removeClass("color_green").addClass("color_gray");
            }
            if (_li.index() == 0) {
                $(_this).parents("ul").children('li').find('.up').removeClass("color_gray").addClass("color_green");
                common_sort(_this);
            }

            indicator_sort(_this);
        }
        //排序后 重新写序号
        function indicator_sort(_this) {
            $(_this).parents("ul").children('li').each(function () {
                var flg = $(this).children("input[name='name_flg']").val();
                $("#sp_" + flg).html(($(this).index() + 1));
            })
        }

        //试题向下排序
        function down(_this) {
            var _li = $(_this).parents("li");
            var len = $(_this).parents("ul").find("li").length;
            if (_li.index() != len - 1) {
                _li.fadeOut().fadeIn();
                _li.next().after(_li);
                $(_this).parents("ul").children('li').find('.up').removeClass("color_gray").addClass("color_green");
                $(_this).parents("ul").children('li').find('.up').first().removeClass("color_green").addClass("color_gray");
            }
            if (_li.index() == len - 1) {
                $(_this).parents("ul").children('li').find('.down').removeClass("color_gray").addClass("color_green");
                common_sort(_this);
            }

            indicator_sort(_this);
        }

        //标题的向上排序
        function t_up(_this) {
            var _div = $(_this).parents(".indicator_type");
            var len = _div.length;
            //alert(_div.index() + ":" + len);
            if (_div.index() != 0) {
                _div.fadeOut().fadeIn();
                _div.prev().before(_div);
                $(_this).parents(".test_module").find("h1").find('.down_t').removeClass("color_gray").addClass("color_green");
                $(_this).parents(".test_module").find("h1").find('.down_t').last().removeClass("color_green").addClass("color_gray");
            }
            if (_div.index() == 0) {
                //alert($(_this).parents(".test_module").find("h1").find('.iconfont:eq(0)').length);
                $(_this).parents(".test_module").find("h1").find('.up_t').removeClass("color_gray").addClass("color_green");
                common_sort(_this);
            }
        }

        //标题的向下排序
        function t_down(_this) {
            //获取最大类型的div indicator_type
            var _div = $(_this).parents(".indicator_type");
            //获取总的h1的长度
            var len = $(_this).parents(".test_module").find("h1").length;
            //如果不是最后一个就进行顺序的互换
            if (_div.index() != len - 1) {
                _div.fadeOut().fadeIn();//淡入淡出
                _div.next().after(_div);//互换

                //预防错误
                $(_this).parents(".test_module").find("h1").find('.up_t').removeClass("color_gray").addClass("color_green");
                $(_this).parents(".test_module").find("h1").find('.up_t').first().removeClass("color_green").addClass("color_gray");
            }
            if (_div.index() == len - 1) {
                $(_this).parents(".test_module").find("h1").find('.down_t').eq(1).removeClass("color_gray").addClass("color_green");
                common_sort(_this);
            }
        }

        //公用排序
        function common_sort(_this) {
            //当前选中的图标置灰
            $(_this).removeClass("color_green").addClass("color_gray");
            //取消click事件
            $(_this).off("click");
        }

        //计算离开文本框的方法
        function text_blur() {
            $(".number").blur(function () {
                var all_array = [];//定义类型需要存的数组
                var Id = $(this).parents("li").children("input[name='name_in']").val();//获取试题的id
                var Title = $(this).parents("li").children("input[name='name_title']").val();//获取试题类型的id
                var num_array = [];//定义标题内文本框需要存的数组

                //循环每一个试题选项的文本框的值，存在数组num_array中
                $(this).parents("li").find('input[class="number"]').each(function () {
                    num_array.push($(this).val());
                })

                //为标题内的文本框赋最大值
                var max = Math.max.apply(null, num_array);
                $("#t_" + Id).val(max == 0 ? "" : max);

                //类型的文本框为标题文本框的求和
                var len = $(this).parents(".indicator_type").find("h2").find('input[type="text"]').length;
                //循环每个标题文本框的text控件，并把值赋予all_array
                $(this).parents(".indicator_type").find("h2").find('input[type="text"]').each(function () {
                    if ($(this).val() != "") {
                        all_array.push($(this).val());
                    }
                })

                //对all_array进行遍历，进行求和
                var sum = 0;
                for (var i = 0; i < all_array.length; i++) {
                    sum=numAdd(sum,all_array[i]);//防止值为字符串 导致计算错误
                }
                //赋值
                $("#h_" + Title).val(sum == 0 ? "" : sum);

                //实时总分
                var total = 0;
                $("#text_list1 li h2").each(function () {
                    var total_1 = $(this).find('input[type="text"]').val();
                    if (total_1 == "" || total_1 == undefined) {
                        total_1 = 0;
                    }
                    total=numAdd(total,total_1);
                })
                $("#total").html(total.toFixed(2) + '');
            })

        }

        var index_1 = 0;

        //选择指标
        var select_Array = [];//已经选择的指标，存入此数组，根据此数组，选中已选择的项

        function openIndicator1() {
            for (var i = 0; i < indicator_array.length; i++) {
                var Eva_TableDetail = indicator_array[i];
                indicator_array[i]["indicator_type_value"] = $("#h_" + indicator_array[i].indicator_type_tid).val();//赋值
                indicator_array[i]["total_value"] = $("#total").html();//实时总分的赋值
                var Id = Eva_TableDetail.Id;//获取指标的id
                var n_flg = Eva_TableDetail.flg;//获取指标的id
                Eva_TableDetail["OptionA_S"] = $("#OptionA_" + n_flg).val();//赋值
                Eva_TableDetail["OptionB_S"] = $("#OptionB_" + n_flg).val();
                Eva_TableDetail["OptionC_S"] = $("#OptionC_" + n_flg).val();
                var d_v = "";
                if ($("#OptionD_" + n_flg).val() != undefined) {//值为undifined的情况
                    d_v = $("#OptionD_" + n_flg).val();
                }
                Eva_TableDetail["OptionD_S"] = d_v;

                var e_v = "";
                if ($("#OptionE_" + n_flg).val() != undefined) {
                    e_v = $("#OptionE_" + n_flg).val();
                }
                Eva_TableDetail["OptionE_S"] = e_v;
                var f_v = "";
                if ($("#OptionF_" + n_flg).val() != undefined) {
                    f_v = $("#OptionF_" + n_flg).val();
                }
                Eva_TableDetail["OptionF_S"] = f_v;


                var t_v = "";
                if ($("#t_" + Id).val() != undefined) {
                    t_v = $("#t_" + Id).val();
                }
                Eva_TableDetail["OptionF_S_Max"] = t_v;

                //排序后的索引字段  按指标类型和 题的组合
                if (indicator_array[i].QuesType_Id != 3) {
                    var Sort = ($("#OptionA_" + Id).parents('li').index() + 1);
                    indicator_array[i]["Sort"] = Sort;
                }
                else {
                    var Sort = ($("#txt_" + Id).parents('li').index() + 1);
                    indicator_array[i]["Sort"] = Sort;
                }
            }
            OpenIFrameWindow('创建指标', 'CreateDatabase.aspx?page=1', '800px', '600px');
        }

        function openIndicator() {
            for (var i = 0; i < indicator_array.length; i++) {
                var Eva_TableDetail = indicator_array[i];
                indicator_array[i]["indicator_type_value"] = $("#h_" + indicator_array[i].indicator_type_tid).val();//赋值
                indicator_array[i]["total_value"] = $("#total").html();//实时总分的赋值
                var Id = Eva_TableDetail.Id;//获取指标的id
                if (select_Array.indexOf(Eva_TableDetail.Indicator_Id) == -1) {
                    select_Array.push(Eva_TableDetail.Indicator_Id);
                }

                var n_flg = Eva_TableDetail.flg;//获取指标的id
                Eva_TableDetail["OptionA_S"] = $("#OptionA_" + n_flg).val();//赋值
                Eva_TableDetail["OptionB_S"] = $("#OptionB_" + n_flg).val();
                Eva_TableDetail["OptionC_S"] = $("#OptionC_" + n_flg).val();
                var d_v = "";
                if ($("#OptionD_" + n_flg).val() != undefined) {//值为undifined的情况
                    d_v = $("#OptionD_" + n_flg).val();
                }
                Eva_TableDetail["OptionD_S"] = d_v;

                var e_v = "";
                if ($("#OptionE_" + n_flg).val() != undefined) {
                    e_v = $("#OptionE_" + n_flg).val();
                }
                Eva_TableDetail["OptionE_S"] = e_v;
                var f_v = "";
                if ($("#OptionF_" + n_flg).val() != undefined) {
                    f_v = $("#OptionF_" + n_flg).val();
                }
                Eva_TableDetail["OptionF_S"] = f_v;


                var t_v = "";
                if ($("#t_" + Id).val() != undefined) {
                    t_v = $("#t_" + Id).val();
                }
                Eva_TableDetail["OptionF_S_Max"] = t_v;

                //排序后的索引字段  按指标类型和 题的组合
                if (Eva_TableDetail.QuesType_Id != 3) {
                    var Sort = ($("#OptionA_" + Id).parents('li').index() + 1);
                    Eva_TableDetail["Sort"] = Sort;
                }
                else {
                    var Sort = ($("#txt_" + Id).parents('li').index() + 1);
                    Eva_TableDetail["Sort"] = Sort;
                }
            }

            OpenIFrameWindow('选择指标', 'EvalSelectDataBase.aspx?page=1', '1170px', '700px');
        }
    </script>
</body>
</html>

