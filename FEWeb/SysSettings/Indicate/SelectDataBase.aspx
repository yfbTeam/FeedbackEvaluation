<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDataBase.aspx.cs" Inherits="FEWeb.SysSettings.SelectDataBase" %>

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
            <span title="${self.Name}">${self.Name}<i class="iconfont">&#xe643;</i></span>
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
<body style="background: #FFF;">
    <div class="content clearix" style="padding: 20px;">
        <div class="sortwrap clearfix">
            <div class="menu fl">
                <h1 class="titlea">
                    <i class="iconfont">&#xe60d;</i>
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
                                <th style="width: 10%;">题型
                                </th>
                                <th style="width: 20%; text-overflow: ellipsis; overflow: hidden; white-space: nowrap;">备注
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
    <script src="../../Scripts/public.js"></script>

    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
    <script src="../../Scripts/WebCenter/DatabaseMan.js"></script>
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
        var Sys_Role = Userinfo_json.Sys_Role_Id;

        $(function () {

            DataBaseMainModel.GetData();
            DataBaseMainModel.PageType = 'SelectDataBase';
            //初始化指标库分类
            DataBaseMainModel.init_IndicatorType_data();
            if (page == 0) {
                $("#newIndicator").show();
            }
            $("#submit").click(function () {
                var result = DataBaseMainModel.SumbitPrepare();
                if (!result) {
                    return false;
                }
                DataBaseMainModel.FillData();
                parent.callback();//父页面的回调函数，很重要
                parent.layer.close(index);
                //获取选中的指标的内容
            })
            
            ck_click();//复选框点击事件，点击完 进行存储
        })

        //全选
        function Check_All() {
            DataBaseMainModel.Check_All();
        }
        //左侧的特效
        function get_menus() {
            DataBaseMainModel.get_menus();
        }

        //指标库分类点击获取指标列表
        function indicator_type_click(Id, Name, Parent_Id, Type) {        
            DataBaseMainModel.indicator_type_click(Id, Name, Parent_Id, Type);
        }

        //复选框点击事件
        function ck_click() {
            
            //选择后存储一个子父级的一个大的数组
            $("input[name='cb_item']").on('click', function () {
                DataBaseMainModel.storage_array(this);
            });           
        }
        //新增指标 打开窗口页
        function newIndicator() {
            OpenIFrameWindow('新增指标', '/SysSettings/AddDatabase.aspx?typeid=' + type_child_id + "&page=" + page, '900px', '500px')
        }
        function search() {
            initdata(indicatorType_Id);
            ck_click();
        }
        //根据指标库分类去获取指定的指标库信息
        function initdata(IndicatorType_Id) {
            DataBaseMainModel.initdata(IndicatorType_Id);
        }
        function set_F() {
            DataBaseMainModel.set_F();
        }
        //暂时使用 题型的转换
        function questionType(_val) {
            return DataBaseMainModel.txing(_val);
        }
    </script>
</body>
</html>
