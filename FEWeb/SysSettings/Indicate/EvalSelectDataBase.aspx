<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalSelectDataBase.aspx.cs" Inherits="FEWeb.Evaluation.EvalSelectDataBase" %>
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
    
    <script type="text/x-jquery-tmpl" id="item_indicator">
        <tr>
            <td>
                <input type="checkbox" id="cb_${Id}" name="cb_item" value="${Id}" /><input type="hidden" value="" /></td>
            <td style="text-align: left; padding-left: 20px;">${Name}</td>
            <td>${questionType(QuesType_Id)}</td>
            <td>${Remarks}</td>
            <td>${DateTimeConvert(EditTime,'yyyy-MM-dd',true)}</td>
        </tr>
    </script>


    <script type="text/x-jquery-tmpl" id="item_indicatorType">
        <li>
            <span>${self.Name}<i class="iconfont">&#xe643;</i></span>
            <ul>
                {{each child_list}}
                <li onclick="indicator_type_click('{{= $value.Id}}','{{= $value.Name}}','{{= $value.Parent_Id}}','{{= $value.Type}}');">
                    <input type="hidden" value="{{= $value.Id}}" />
                    <input type="hidden" value="{{= $value.Parent_Id}}" />
                    ${$value.Name}
                </li>
                {{/each}}
            </ul>
        </li>
    </script>
</head>
<body>
    <div class="main clearix">
        <div class="sortwrap clearfix">
             <div class="menu fl">
                <h1 class="titlea">
                    指标库管理
                </h1>
                <ul class="menu_list">
                </ul>
                <%--<input type="button" value="新增指标分类" class="new" id="Indicator_Add" />--%>
            </div>
             <div class="sort_right fr" style="width: 890px;">
                <div class="search_toobar clearfix">
                    <div class="fl">
                        <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl">
                        <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                    </div>
                    <div class="fr" style="margin-left: 20px; display: none" id="newIndicator">
                        <input type="button" name="" id="" value="新增指标" class="btn" onclick="newIndicator()">
                    </div>
                    <div class="fr" style="margin-left: 20px;">
                        <input type="button" name="submit" id="submit" value="确定" class="btn">
                    </div>
                </div>
                <div class="table">
                    <table>
                        <thead>
                            <tr>
                                <th style="text-align: center; width: 40px;">
                                    <input type="checkbox" id="cb_all" onclick="Check_All()" />
                                </th>
                                <th style="text-align: left; padding-left: 20px; width: 50%;">指标名称	 	    	 		 				 					 					   	
                                </th>
                                <th style="width:10%;">题型
                                </th>
                                <th style="width:20%;text-overflow:ellipsis;overflow:hidden;white-space:nowrap;">备注
                                </th>
                                <th>修改时间
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tb_indicator">

                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    </div>
     <script src="../../Scripts/Common.js"></script>
    <script src="../../scripts/public.js"></script>
   
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
    <script type="text/javascript">
        var index = parent.layer.getFrameIndex(window.name);//弹框的索引
        //选择的指标库分类ID
        var type_id;
        //选择的具体指定指标
        var type_child_id;
        var select_Array = [];//所有的指标的内容

        var check_arr = [];//存储选中的值


        var tname = '';//指标分类的名称
        var tid = 0;//指标分类的id
        var indicatorType_Id = 0;//指标类型id

        var first_indicator = 0;//第一个指标的id

        var page = getQueryString('page');//若是定期 （传0 ）公用和专用根据角色区别对待   若是（传1）即时和(2)扫码 只有公用

        var IndicatorType_type = 0;//教学反馈 的类型，若只添加他 则不允许添加 必须有除此之外的数据  表格设计中不能只有教师反馈，至少应该还有别的题


        var indicator_p_type = 0;

        var cookie_Userinfo = localStorage.getItem('Userinfo_LG');
        var Userinfo_json = JSON.parse(cookie_Userinfo);
        var Sys_Role = Userinfo_json.Sys_Role;

        $(function () {
            if (page == 0) {
                $("#newIndicator").show();
            }

            $("#submit").click(function () {
                
                if (tname.length == 0) {
                    layer.msg("请选择指标分类");
                    return false;
                }


                if (parent.indicator_array.length == 0) {
                    layer.msg("您未选择指标");
                    return false;
                }

                if (Sys_Role == "超级管理员" && page == 0) {//有两种指标并且是定期评价的才进行不能交叉使用的判断  
                    var indicator_type_type_arr = [];

                    for (var i = 0; i < parent.indicator_array.length; i++) {
                        indicator_type_type_arr.push(parent.indicator_array[i].indicator_p_type);

                    }
                    if (removeDuplicatedItem(indicator_type_type_arr).join(',').length > 1) {
                        layer.msg("公共与专业指标库不能交叉使用");
                        return false;
                    }
                }

                parent.callback();//父页面的回调函数，很重要
                parent.layer.close(index);
                //获取选中的指标的内容
            })

            ck_click();//复选框点击事件，点击完 进行存储
        })

        //全选
        function Check_All() {

            if ($("#cb_all").is(":checked")) {
                $("input[name='cb_item']").prop("checked", true);
                $("input[name='cb_item']").each(function () {
                    storage_array(this);//存储数据
                })
            } else {
                $("input[name='cb_item']").prop("checked", false);
                $("input[name='cb_item']").each(function () {
                    storage_array(this);//存储数据
                })
            }
        }




        //左侧的特效
        function get_menus() {
            $('.menu_list li:eq(0)').children('span').addClass('selected');
            $('.menu_list li:eq(0)').children('ul').slideDown();
            $('.menu_list').find('li:has(ul)').children('span').click(function () {
                var $next = $(this).next('ul');
                if ($next.is(':hidden')) {
                    $(this).addClass('selected');
                    $next.stop().slideDown();
                } else {
                    $(this).removeClass('selected');
                    $next.stop().slideUp();
                }
            });
        }


        //初始化指标库分类
        init_IndicatorType_data();

        //指标库分类点击获取指标列表
        function indicator_type_click(Id, Name, Parent_Id, Type) {
            $("#cb_all").prop("checked", false);
            tname = Name;
            tid = Id;
            indicatorType_Id = Id;
            IndicatorType_type = Type;
            indicator_p_type = parseInt(Parent_Id);//不能交叉使用的判断 先把父ID存下了
            initdata(Id);
            ck_click();
            for (var i = 0; i < check_arr.length; i++) {
                $("input[name='cb_item']").each(function () {
                    if (check_arr[i] == $(this).val()) {
                        $(this).attr("checked", true);

                    }
                })
            }

        }

        function removeDuplicatedItem(ar) {
            var ret = [];

            for (var i = 0, j = ar.length; i < j; i++) {
                if (ret.indexOf(ar[i]) === -1) {
                    ret.push(ar[i]);
                }
            }

            return ret;
        }

        //选择后存储一个子父级的一个大的数组
        function storage_array(_this) {
            if ($(_this).is(":checked")) {
                parent.flg = parent.flg + 1;
                parent.index_1++;
                check_arr.push($(_this).val());

                select_Array = Enumerable.From(retData).Where("x=>x.Id==" + $(_this).val() + "").ToArray();
                select_Array[0]["flg"] = parent.flg;
                select_Array[0]["Indicator_Id"] = $(_this).val();
                select_Array[0]["OptionA_S"] = "";//赋值
                select_Array[0]["OptionB_S"] = "";
                select_Array[0]["OptionC_S"] = "";
                select_Array[0]["OptionD_S"] = "";
                select_Array[0]["OptionE_S"] = "";
                select_Array[0]["OptionF_S"] = "";
                select_Array[0]["OptionF_S_Max"] = "";

                parent.indicator_array.push(select_Array[0]);//indicator_array 回调父页面的参数
            }
            else {
                for (var i = 0; i < parent.select_Array.length; i++) {
                    if (parent.select_Array[i] == $(_this).val()) {
                        parent.select_Array.splice(i, 1);
                        $(_this).prop("checked", false);
                    }
                }
                for (var i = 0; i < parent.indicator_array.length; i++) {
                    if (parent.indicator_array[i].Id == $(_this).val()) {
                        Array.prototype.splice.call(parent.indicator_array, i, 1);
                        //parent.indicator_array[i].indicator_list.splice(j, 1);//此方法IE不支持 换成上边的代码
                        parent.flg = parent.flg--;//保证不重复即可
                    }
                }
            }
        }

        //复选框点击事件
        function ck_click() {
            $("input[name='cb_item']").click(function () {
                storage_array(this);
            })
        }

        //新增指标 打开窗口页
        function newIndicator() {
            OpenIFrameWindow('新增指标', '/SysSettings/AddDatabase.aspx?typeid=' + type_child_id + "&page=" + page, '800px', '600px')
        }

        //获取左侧指标分类


        function init_IndicatorType_data() {
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Get_IndicatorType" },
                success: function (json) {
                    var retData_type = json.result.retData;
                    retData_type = Enumerable.From(retData_type).OrderBy('$.Id').ToArray();//按Id进行升序排列

                    var P_List = [];

                    for (var i = 0; i < retData_type.length; i++) {

                        if (page == 0) {
                            if (Sys_Role == "超级管理员" || Sys_Role == "学生评价管理员") {
                                if (retData_type[i].Parent_Id == 0 && retData_type[i].Name == '公用指标库') {//获取分类父Id
                                    //数据
                                    var parent = new Object();
                                    //设置当前分类
                                    parent.self = retData_type[i];
                                    //设置子分类
                                    var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                                    //console.log(child_list[0].Id);
                                    first_indicator = child_list[0].Id;
                                    indicatorType_Id = first_indicator;
                                    IndicatorType_type = child_list[0].Type;
                                    tname = child_list[0].Name;
                                    tid = child_list[0].Id;
                                    indicator_p_type = parseInt(retData_type[i].Id);////不能交叉使用的判断 先把父ID存下了
                                    parent.child_list = child_list;

                                    //列表添加
                                    P_List.push(parent);
                                }
                            }
                            if (Sys_Role == "超级管理员" || Sys_Role == "督导管理员") {
                                if (retData_type[i].Parent_Id == 0 && retData_type[i].Name == '专用指标库') {//获取分类父Id
                                    //数据
                                    var parent = new Object();
                                    //设置当前分类
                                    parent.self = retData_type[i];
                                    //设置子分类
                                    var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                                    //console.log(child_list[0].Id);
                                    if (Sys_Role != '超级管理员') {
                                        first_indicator = child_list[0].Id;
                                        tname = child_list[0].Name;
                                        tid = child_list[0].Id;
                                        indicator_p_type = parseInt(retData_type[i].Id);//不能交叉使用的判断 先把父ID存下了
                                        IndicatorType_type = child_list[0].Type;
                                    }


                                    parent.child_list = child_list;
                                    //列表添加
                                    P_List.push(parent);
                                }
                            }
                        }
                        else {

                            if (retData_type[i].Parent_Id == 0 && retData_type[i].Name == '公用指标库') {//获取分类父Id 
                                //数据

                                var parent = new Object();
                                //设置当前分类
                                parent.self = retData_type[i];
                                //设置子分类
                                //var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                                var child_list = Enumerable.From(retData_type).Where('item=>item.Parent_Id=="' + retData_type[i].Id + '"').ToArray();// 教学反馈不显示在扫码和即时中 4.7改
                                //console.log(child_list)
                                first_indicator = child_list[0].Id;
                                indicatorType_Id = first_indicator;
                                tname = child_list[0].Name;
                                tid = child_list[0].Id;
                                IndicatorType_type = child_list[0].Type;
                                parent.child_list = child_list;
                                indicator_p_type = parseInt(retData_type[i].Id);//不能交叉使用的判断 先把父ID存下了
                                //列表添加
                                P_List.push(parent);
                            }
                        }
                    }
                    $("#item_indicatorType").tmpl(P_List).appendTo(".menu_list");
                    //初始化数据时，获取所有指标
                    initdata(first_indicator);
                    $('.menu_list li').eq(0).children('ul').children('li').eq(0).addClass('selected');
                    $('.menu_list').find('li:has(ul)').find('li').click(function () {
                        $('.menu_list').find('li:has(ul)').find('li').removeClass('selected');
                        $(this).addClass('selected');
                    })
                    get_menus();
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }


        function search() {
            initdata(indicatorType_Id);
            ck_click();
        }
        //根据指标库分类去获取指定的指标库信息
        function initdata(IndicatorType_Id) {
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Get_Indicator", IndicatorType_Id: IndicatorType_Id },
                success: function (json) {
                    retData = json.result.retData;
                    $("#tb_indicator").empty();
                    $(".Pagination").remove();
                    retDataCache = retData;

                    retDataCache = Enumerable.From(retDataCache).Where("item=>item.IsDelete==0").OrderByDescending('$.Id').ToArray();//按Id进行降序排列
                    var key = $("#key").val();
                    if (key != "") {
                        //console.log(IndicatorType_Id);
                        //console.log(retDataCache);
                        retDataCache = Enumerable.From(retDataCache).Where("item=>item.Name.indexOf('" + key + "')>-1").Where("x=>x.IndicatorType_Id==" + IndicatorType_Id + "").ToArray();
                    }

                    $("#item_indicator").tmpl(retDataCache).appendTo("#tb_indicator");
                    if (retDataCache.length == 0) {
                        nomessage('#tb_indicator');
                        return;
                    }
                    if (parent.select_Array.length > 0) {
                        for (var i = 0; i < parent.select_Array.length; i++) {
                            $("#cb_" + parent.select_Array[i]).attr("checked", true);
                        }
                    }
                    $('.table').kkPages({
                        PagesClass: 'tbody tr', //需要分页的元素
                        PagesMth: 10, //每页显示个数
                        PagesNavMth: 4, //显示导航个数
                        IsShow: true
                    });
                    //ck_check();//选中checkbox后，重新写此方法
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //暂时使用 题型的转换
        function questionType(_val) {
            var _return = '';
            switch (_val) {
                case 1:
                    _return = '单选题'
                    break;
                case 2:
                    _return = '多选题'
                    break;
                case 3:
                    _return = '问答题'
                    break;
                default:
                    _return = '无';
                    break;
            }
            return _return;
        }
    </script>
</body>
</html>
