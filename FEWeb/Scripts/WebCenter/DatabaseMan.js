

var DataBaseMainModel =
    {
        PageType: 'DatabaseMan',  //DatabaseMan指标库分类   SelectDataBase//选择指标库

        child_list: null,
        indicator_array: null,
        index_1: null,
        flg: 1,
        select_Array: null,


        Root_SingleMode: true,//单个节点只有一个类型
        FillData: function () {
            //DataBaseMainModel.FillData = null;

            var data = JSON.stringify(DataBaseMainModel);
            localStorage.setItem('DataBaseMainModel', data);
        },

        GetData: function () {
            var data = localStorage.getItem('DataBaseMainModel')
            if (data != null) {
                data = JSON.parse(data);
                DataBaseMainModel.child_list = data.child_list;
                DataBaseMainModel.indicator_array = data.indicator_array;
                DataBaseMainModel.index_1 = data.index_1;
                DataBaseMainModel.flg = data.flg;
                DataBaseMainModel.select_Array = data.select_Array;
            }
        },
        //Clear: function () {
        //    DataBaseMainModel.Check_All = null;
        //    DataBaseMainModel.copy = null;
        //    DataBaseMainModel.txing = null;
        //    DataBaseMainModel.SumbitPrepare = null;
        //    DataBaseMainModel.set_F = null;
        //    DataBaseMainModel.storage_array = null;
        //    DataBaseMainModel.removeDuplicatedItem = null;
        //    DataBaseMainModel.delete_indicator = null;
        //    DataBaseMainModel.fenye = null;
        //    DataBaseMainModel.PageCallback = null;
        //    DataBaseMainModel.init_IndicatorType_data = null;
        //    DataBaseMainModel.get_menus = null;
        //    DataBaseMainModel.indicator_type_click = null;
        //    DataBaseMainModel.GetData = null;
        //    DataBaseMainModel.initdata = null;
        //    DataBaseMainModel.menu_list = null;
        //},

        //获取左侧指标分类
        init_IndicatorType_data: function () {
            var that = this;
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Get_IndicatorType" },
                success: function (json) {

                    retData_type = json.result.retData;
                    var P_List = [];
                    $(".menu_list").html('');
                    retData_type = Enumerable.From(retData_type).OrderBy('$.Id').ToArray();//按Id进行升序排列
                    var i_index = 0;

                    for (var i = 0; i < retData_type.length; i++) {
                        if (retData_type[i].Parent_Id == 0) {//获取分类父Id
                            var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                            P_List.push({ self: retData_type[i], child_list: child_list });
                        }
                    }
                    $("#item_indicatorType").tmpl(P_List).appendTo(".menu_list");
                    DataBaseMainModel.menu_list();

                  
                    //默认选择第一条内容【约定俗成】
                    $('.menu_list li:eq(0)').find('span').trigger('click');
                    $('.menu_list li:eq(0)').find('li:eq(0)').trigger('click');
                    $('.menu_list li:eq(0)').find('li:eq(0)').addClass('selected');
                    //默认执行第一个分类下的第一个；
                    var Id = P_List[0].child_list[0].Id;
                 
                    that.initdata(Id);
                },
                error: function () {
                    //接口错误时需要执行的
                }


            });
        },
        //删除指标
        delete_indicator: function (id) {
            layer.confirm('确定要删除？', {
                btn: ['确定', '取消'], //按钮
                title: '操作'
            }, function () {
                $.ajax({
                    url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                    type: "post",

                    dataType: "json",
                    data: { Func: "Delete_Indicator", Id: id },//组合input标签
                    success: function (json) {
                        //console.log(JSON.stringify(json));
                        if (json.result.errMsg == "success") {
                            layer.msg('操作成功!');
                            DataBaseMainModel.initdata(indicator_type_id);
                        }
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            })
        },
        menu_list: function () {

            $('.menu_list').find('li:has(ul)').children('span').click(function () {
                var $next = $(this).next('ul');
                if ($next.is(':hidden')) {
                    $(this).addClass('selected');
                    $next.stop().slideDown();
                    $next.find('li:first').addClass('selected');
                    $next.find('li:first').trigger('click');
                    if ($(this).parent('li').siblings().children('ul').is(':visible')) {
                        $(this).parent('li').siblings().children('span').removeClass('selected');
                        $(this).parent('li').siblings().children('ul').stop().slideUp();
                    }
                } else {
                    $(this).removeClass('selected');
                    $next.stop().slideUp();
                }
            });
            $('.menu_list').find('li:has(ul)').find('li').click(function () {
                $('.menu_list').find('li:has(ul)').find('li').removeClass('selected');
                $(this).addClass('selected')
                type_id = $(this).children("input[type='hidden']:eq(1)").val();
                type_child_id = $(this).children("input[type='hidden']:eq(0)").val();
            });
        },

        initdata: function (IndicatorType_Id) {
            var type = arguments[1] || 0;//0本页面；1弹框页面加载
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Get_Indicator", IndicatorType_Id: IndicatorType_Id },
                success: function (json) {
                    $('#test1').show();
                    $("#tb_indicator").empty();
                    retDataCache = json.result.retData;
                    retDataCache = Enumerable.From(retDataCache).Where("item=>item.IsDelete==0").OrderByDescending('$.Id').ToArray();//按Id进行降序排列
                    switch (DataBaseMainModel.PageType) {
                        case 'DatabaseMan':
                            $('#ss').show();
                            var key = $("#key").val().trim();
                            if (key != "") {
                                retDataCache = Enumerable.From(retDataCache).Where("item=>item.Name.indexOf('" + key + "')>-1").ToArray();
                            }
                            if (retDataCache.length == 0) {
                                nomessage('#tb_indicator');
                                $('#test1').hide();
                                return;
                            }
                            DataBaseMainModel.fenye(Enumerable.From(retDataCache).ToArray().length);

                            tableSlide();
                            if (IndicatorType_Id != type_child_id && type == 1) {
                                var hiddenobj = $('.menu_list').find('input[type=hidden][value=' + IndicatorType_Id + ']');
                                if (hiddenobj) {
                                    if (hiddenobj.parents('ul').is(":hidden")) {
                                        $('.menu_list').find('li:has(ul)').children('span').removeClass('active');
                                        $('.menu_list').find('li:has(ul)').children('ul').hide();
                                        hiddenobj.parents('ul').slideDown();
                                    }
                                    hiddenobj.parents('ul').prev('span').addClass('selected');
                                    hiddenobj.parent('li').addClass('selected').siblings().removeClass('selected');
                                }
                            }
                            break;
                        case 'SelectDataBase':

                            var key = $("#key").val();
                            if (key != "") {
                                retDataCache = Enumerable.From(retDataCache).Where("item=>item.Name.indexOf('" + key + "')>-1").Where("x=>x.IndicatorType_Id==" + IndicatorType_Id + "").ToArray();
                            }

                            $("#item_indicator").tmpl(retDataCache).appendTo("#tb_indicator");
                            if (retDataCache.length == 0) {
                                nomessage('#tb_indicator');
                                return;
                            }
                            var select_Array = DataBaseMainModel.select_Array;
                            if (select_Array.length > 0) {
                                for (var i = 0; i < select_Array.length; i++) {
                                    $("#cb_" + select_Array[i]).attr("checked", true);
                                    check_arr.push(select_Array[i] + "");
                                    //DataBaseMainModel.storage_array($("#cb_" + select_Array[i]));
                                    //indicator_array.push(DataBaseMainModel.indicator_array)
                                }
                            }
                            set_F();
                            $("input[name='cb_item']").click(function () {
                                if ($(this).is(":checked") == false) {
                                    check_arr.splice(check_arr.indexOf($(this).val()), 1);
                                }
                                set_F();
                            })

                            //选中文字，复选框选中
                            $("#tb_indicator .td_select").click(function () {
                                if ($(this).parent("tr").find("input[type='checkbox']").is(":checked")) {
                                    $(this).parent("tr").find("input[type='checkbox']").prop("checked", false);
                                    var _this = $(this).parent("tr").find("input[type='checkbox']");
                                    DataBaseMainModel.storage_array(_this);
                                }
                                else {
                                    $(this).parent("tr").find("input[type='checkbox']").prop("checked", true);
                                    var _this = $(this).parent("tr").find("input[type='checkbox']");
                                    DataBaseMainModel.storage_array(_this);
                                }
                                set_F();
                            })
                            $('.table').kkPages({
                                PagesClass: 'tbody tr', //需要分页的元素
                                PagesMth: 10, //每页显示个数
                                PagesNavMth: 4, //显示导航个数
                                IsShow: true
                            });
                            //ck_check();//选中checkbox后，重新写此方法
                            break;
                        default:
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        },

        //翻页调用
        PageCallback: function (index, jq) {

            var arrRes = Enumerable.From(retDataCache).Skip(index * pageSize).Take(pageSize).ToArray();
            $("#tb_indicator").empty();
            $("#item_indicator").tmpl(arrRes).appendTo("#tb_indicator");
            //按身份禁用控件
        },

        copy: function (id) {
            layer.confirm('确定要复制？', {
                btn: ['确定', '取消'], //按钮
                title: '操作'
            }, function () {
                //查找retDataCache id的信息，并存储在cache_data 数组中
                var cache_data = Enumerable.From(retDataCache).Where("item=>item.Id==" + id + "").ToArray();//按Id进行降序排列
                var obj = {};//新建一个对象，给此对象赋值
                obj.Func = "Add_Indicator";
                obj.Name = cache_data[0].Name + "副本";
                obj.IndicatorType_Id = cache_data[0].IndicatorType_Id;
                obj.QuesType_Id = cache_data[0].QuesType_Id;
                obj.OptionA = cache_data[0].OptionA;
                obj.OptionB = cache_data[0].OptionB;
                obj.OptionC = cache_data[0].OptionC;
                obj.OptionD = cache_data[0].OptionD;
                obj.OptionE = cache_data[0].OptionE;
                obj.OptionF = cache_data[0].OptionF;
                obj.UseTimes = 0;
                obj.Remarks = cache_data[0].Remarks;
                obj.CreateUID = cache_data[0].CreateUID;
                obj.CreateTime = DateTimeConvert(cache_data[0].CreateTime, 'yyyy-MM-dd HH:mm:ss', true);
                obj.EditUID = cache_data[0].EditUID;
                obj.EditTime = DateTimeConvert(cache_data[0].EditTime, 'yyyy-MM-dd HH:mm:ss', true);
                obj.IsEnable = cache_data[0].IsEnable;
                obj.IsDelete = cache_data[0].IsDelete;
                //console.log(JSON.stringify(obj));
                $.ajax({
                    url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: obj,//组合input标签
                    success: function (json) {
                        //console.log(JSON.stringify(json));
                        if (json.result.errMsg == "success") {
                            layer.msg('操作成功!');
                            DataBaseMainModel.initdata(indicator_type_id);
                        }
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            })
        },

        //暂时使用 题型的转换
        txing: function (_val) {
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
        },
        fenye: function (pageCount) {
            $("#test1").pagination(pageCount, {
                callback: DataBaseMainModel.PageCallback,
                prev_text: '上一页',
                next_text: '下一页',
                items_per_page: pageSize,
                num_display_entries: 4,//连续分页主体部分分页条目数
                current_page: pageIndex,//当前页索引
                num_edge_entries: 1//两侧首尾分页条目数
            });
        },



        //===========目前纯粹SelectDataBase 使用===================
        SumbitPrepare: function () {
            
            if (tname.length == 0) {
                layer.msg("请选择指标分类");
                return false;
            }
            if (DataBaseMainModel.indicator_array.length == 0) {
                layer.msg("您未选择指标");
                return false;
            }
            return true;
        },

        //全选
        Check_All: function () {
            if ($("#cb_all").is(":checked")) {
                //$("input[name='cb_item']").each(function () {
                //    for (var i = 0; i < check_arr.length; i++) {
                //        if ($(this).is("checked")) {
                //            check_arr.splice(i, 1);
                //        }
                //    }
                //})

                $("input[name='cb_item']").prop("checked", true);
                $("input[name='cb_item']").each(function () {
                    DataBaseMainModel.storage_array(this);//存储数据
                })
            } else {
                $("input[name='cb_item']").prop("checked", false);
                $("input[name='cb_item']").each(function () {
                    for (var i = 0; i < check_arr.length; i++) {
                        if ($(this).val() == check_arr[i]) {
                            check_arr.splice(i, 1)
                        }
                    }
                    DataBaseMainModel.storage_array(this);//存储数据
                })
            }
        },
        removeDuplicatedItem: function (ar) {
            var ret = [];
            for (var i = 0, j = ar.length; i < j; i++) {
                if (ret.indexOf(ar[i]) === -1) {
                    ret.push(ar[i]);
                }
            }
            return ret;
        },

        set_F: function () {
            var fl = 0;
            $("input[name='cb_item']").each(function () {
                if ($(this).is(":checked")) {
                    fl++;
                }
            })
            if (fl == $("input[name='cb_item']").length) {
                $("#cb_all").prop("checked", true);
            }
            else {
                $("#cb_all").prop("checked", false);
            }
        },

        //左侧的特效
        get_menus: function () {
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
        },
        //指标库分类点击获取指标列表
        indicator_type_click: function (Id, Name, Parent_Id, Type) {
            $("#cb_all").prop("checked", false);
            tname = Name;
            tid = Id;
            indicatorType_Id = Id;
            IndicatorType_type = Type;
            indicator_p_type = parseInt(Parent_Id);//不能交叉使用的判断 先把父ID存下了
            DataBaseMainModel.initdata(Id);
            
            ck_click();
            for (var i = 0; i < check_arr.length; i++) {
                $("input[name='cb_item']").each(function () {
                    if (check_arr[i] == $(this).val()) {
                        $(this).attr("checked", true);
                    }
                })
            }
            var fl = 0;
            $("input[name='cb_item']").each(function () {
                if ($(this).is(":checked")) {
                    fl++;
                }
            })
            if (fl == $("input[name='cb_item']").length) {
                $("#cb_all").prop("checked", true);
            }
            else {
                $("#cb_all").prop("checked", false);
            }
        },
        //选择后存储一个子父级的一个大的数组
        storage_array: function (_this) {

            
            //console.log(check_arr);
            //console.log($(_this).val());
            //console.log(typeof (parseInt($(_this).val())));
            //console.log(check_arr.indexOf($(_this).val()));
            if ($(_this).is(":checked") && check_arr.indexOf($(_this).val()) == -1) {
                DataBaseMainModel.index_1++;
                check_arr.push($(_this).val());

                select_Array = Enumerable.From(retDataCache).Where("x=>x.Id==" + $(_this).val() + "").ToArray();
                select_Array[0]["flg"] = DataBaseMainModel.flg;

                select_Array[0]["OptionA_S"] = "";//赋值
                select_Array[0]["OptionB_S"] = "";
                select_Array[0]["OptionC_S"] = "";
                select_Array[0]["OptionD_S"] = "";
                select_Array[0]["OptionE_S"] = "";
                select_Array[0]["OptionF_S"] = "";
                select_Array[0]["OptionF_S_Max"] = "";

                var indicator_obj = new Object();
                indicator_obj.indicator_type_tname = tname;
                indicator_obj.indicator_type_tid = tid;
                indicator_obj.indicator_type_value = "";//
                indicator_obj.indicator_p_type = parseInt(indicator_p_type);////不能交叉使用的判断 先把父ID存下了
                indicator_obj.indicator_type_type = IndicatorType_type;//
                indicator_obj.index = DataBaseMainModel.index_1;//实时总分  若为1表示 是第一条数据，只在第一个循环时显示实时总分
                indicator_obj.total_value = 0;//实时总分需要显示
                indicator_obj.QuesType_Id = select_Array[0].QuesType_Id;//父类加题型，为了父页面的问答题的排序或问答题的序号去掉

                indicator_obj.indicator_list = [];


                //重复的不允许添加
                var obj_exit = Enumerable.From(DataBaseMainModel.indicator_array).Where("x=>x.indicator_type_tid=='" + tid + "'").ToArray();

                if (obj_exit.length > 0 && obj_exit[0] != undefined && page == 0) {//此处加一个page=0 表示是定期情况下才有大类型，否则就依次排列
                    Array.prototype.push.call(obj_exit[0].indicator_list, select_Array[0]);
                    //obj_exit[0].indicator_list.push(select_Array[0]); //此方法不支持IE  改为上边的代码
                    DataBaseMainModel.flg = DataBaseMainModel.flg + 1;//保证不重复即可
                }
                else {
                    DataBaseMainModel.flg = DataBaseMainModel.flg + 2;//保证不重复即可
                    indicator_obj.indicator_type_tname = tname;
                    indicator_obj.indicator_type_tid = parseInt(tid);
                    indicator_obj.indicator_type_value = "";
                    indicator_obj.indicator_p_type = parseInt(indicator_p_type);////不能交叉使用的判断 先把父ID存下了
                    indicator_obj.indicator_type_type = IndicatorType_type;//
                    indicator_obj.indicator_list = select_Array;
                    DataBaseMainModel.indicator_array.push(indicator_obj);//indicator_array 回调父页面的参数
                }
            }
            else {

                if ($("#cb_all").is(":checked") == false) {
                    check_arr.splice(check_arr.indexOf($(_this).val()), 1);
                    for (var i = 0; i < DataBaseMainModel.select_Array.length; i++) {
                        if (DataBaseMainModel.select_Array[i] == $(_this).val()) {
                            DataBaseMainModel.select_Array.splice(i, 1);

                            $(_this).prop("checked", false);
                        }
                    }
                    for (var i = 0; i < DataBaseMainModel.indicator_array.length; i++) {
                        for (var j = 0; j < DataBaseMainModel.indicator_array[i].indicator_list.length; j++) {
                            if (DataBaseMainModel.indicator_array[i].indicator_list[j].Id == $(_this).val()) {
                                Array.prototype.splice.call(DataBaseMainModel.indicator_array[i].indicator_list, j, 1);
                                //DataBaseMainModel.indicator_array[i].indicator_list.splice(j, 1);//此方法IE不支持 换成上边的代码
                                DataBaseMainModel.flg = DataBaseMainModel.flg + 5;//保证不重复即可
                            }
                        }
                        if (DataBaseMainModel.indicator_array[i].indicator_list.length == 0) {//如果长度为0了表示 要删除类型标题行的
                            DataBaseMainModel.indicator_array.splice(i, 1);
                        }
                    }
                }

            }


        },
    };


//==========================指标库类型信息===================================================
var IndicateType_Model = {
    PageType: 'DatabaseMan',  //DataBaseSort指标库分类维护 
    Id: null,
    Name: null,
    EditUID: null,
    CreateUID: null,
    P_Id: null,
    //总数据
    Data: [],

    Indicate_list: [],

    //init_Type:1,//1 为编辑  0 添加

    init: function (init_Type) {
        IndicateType_Model.init_IndicatorType_data_Compleate = function (P_List) {
            self_list = [];
            $("#item_indicatorType").tmpl(P_List).appendTo(".menu_list");
            if (type_id == 0) {
                type_id = P_List[0].self.Id;
            }
            for (var i = 0; i < P_List.length; i++) {
                self_list.push(P_List[i].self);
            }

            //$('.menu_list li:eq(0)').addClass('selected');                   
            $('.menu_list li').click(function () {
                $(this).addClass('selected').siblings().removeClass('selected');
                type_id = $(this).find('input[type=hidden]').val();
                var name = $(this).find('span').text();
                $('#indicate_type_1').val(name);
                IndicateType_Model.P_Id = type_id;
                IndicateType_Model.Load_Data_Compleate = function () {
                    var refdata = IndicateType_Model.Data;
                    IndicateType_Model.Indicate_list = refdata;
                    $("#Child").empty();
                    var root = self_list.filter(function (item) { return item.Id == type_id });
                    //有子集不可直接进行删除操作
                    if (refdata.length > 0) {
                        $("#item_Indicate_Type_No_Delete").tmpl(root).appendTo("#Child");
                    }
                    else {
                        $("#item_Indicate_Type").tmpl(root).appendTo("#Child");
                    }
                    refdata = Enumerable.From(refdata).OrderBy('$.Id').ToArray();//按Id进行升序排列
                    $("#item_Indicate_Type").tmpl(refdata).appendTo("#Child");

                    tableSlide();
                };
                IndicateType_Model.Load_Data();
            });
            if (type_id == 0) {
                $('.menu_list li:eq(0)').trigger('click');
            }
            else {
                $('.menu_list').find('input[value=' + type_id + ']').trigger('click');
            }
        };
        IndicateType_Model.init_IndicatorType_data();
    },

    //获取数据
    Load_Data: function () {
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Get_IndicatorType", P_Id: IndicateType_Model.P_Id },
            success: function (json) {
                if (json.result.errMsg == "success") {
                    IndicateType_Model.Data = json.result.retData;
                    IndicateType_Model.Load_Data_Compleate();
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },

    Add_Data: function () {
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Add_IndicatorType", Parent_Id: IndicateType_Model.P_Id, Name: IndicateType_Model.Name, CreateUID: IndicateType_Model.CreateUID },
            success: function (json) {
                if (json.result.errMsg == "success") {
                    IndicateType_Model.Add_Data_Compleate();
                }
                else {
                    layer.msg(json.result.retData);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },
    Add_Data_Compleate: function () { },

    Edit_Data: function () {
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Edit_IndicatorType", Id: IndicateType_Model.Id, Name: IndicateType_Model.Name, EditUID: IndicateType_Model.EditUID },
            success: function (json) {
                if (json.result.errMsg == "success") {
                    layer.msg('操作成功');
                    IndicateType_Model.Edit_Data_Compleate(true);
                }
                else {
                    layer.msg(json.result.retData);
                    IndicateType_Model.Edit_Data_Compleate(false);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },
    Edit_Data_Compleate: function (result) { },
    Remove_Data: function () {
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Delete_Indicator_Type", Id: IndicateType_Model.Id },
            success: function (json) {
                if (json.result.errMsg == "success") {
                    IndicateType_Model.Remove_Data_Compleate();
                }
                else {
                    layer.msg(json.result.retData);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },
    Remove_Data_Compleate: function () { },

    //获取数据完成时执行【类似于异步委托】
    Load_Data_Compleate: function () { },


    //获取左侧指标分类
    init_IndicatorType_data: function () {
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: { Func: "Get_IndicatorType" },
            success: function (json) {

                retData_type = json.result.retData;
                var P_List = [];
                $(".menu_list").html('');
                retData_type = Enumerable.From(retData_type).OrderBy('$.Id').ToArray();//按Id进行升序排列


                var i_index = 0;
                for (var i = 0; i < retData_type.length; i++) {
                    if (Sys_Role == 10) {

                        //获取分类父Id
                        //if (retData_type[i].Name == '督导(专家)指标库' || retData_type[i].Name == '信息员指标库' || retData_type[i].Name == '院校领导干部和教学管理人员指标库') {
                        if (retData_type[i].Parent_Id == 0 && retData_type[i].P_Type == 2) {
                            i_index++;
                            //数据
                            var parent = new Object();
                            //设置当前分类
                            parent.self = retData_type[i];
                            //设置子分类
                            var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                            console.log(child_list);
                            parent.child_list = child_list;

                            if (i_index == 1) {
                                for (var j = 0; j < child_list.length; j++) {
                                    indicator_arr.push(child_list[j].Id);
                                }
                            }
                            //列表添加
                            P_List.push(parent);
                        }
                    }
                    else if (Sys_Role == 16) {
                        //if (retData_type[i].Parent_Id == 0 && retData_type[i].Name == '公用指标库') {//获取分类父Id
                        if (retData_type[i].Parent_Id == 0 && retData_type[i].P_Type == 1) {
                            i_index++;
                            //数据
                            var parent = new Object();
                            //设置当前分类
                            parent.self = retData_type[i];
                            //设置子分类
                            var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                            parent.child_list = child_list;
                            if (i_index == 1) {

                                for (var j = 0; j < child_list.length; j++) {
                                    indicator_arr.push(child_list[j].Id);
                                }
                            }


                            //列表添加
                            P_List.push(parent);
                        }
                    }
                    else {
                        if (retData_type[i].Parent_Id == 0) {//获取分类父Id
                            //数据
                            var parent = new Object();
                            //设置当前分类
                            parent.self = retData_type[i];
                            //设置子分类
                            var child_list = Enumerable.From(retData_type).Where(function (x) { return x.Parent_Id == retData_type[i].Id; }).ToArray();
                            parent.child_list = child_list;
                            //列表添加
                            P_List.push(parent);
                        }
                    }
                }
                IndicateType_Model.init_IndicatorType_data_Compleate(P_List);

            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },

    init_IndicatorType_data_Compleate: function (P_List) { },


    //存储数据
    SaveData: function () {
        //
        localStorage.setItem('IndicateType_Model', JSON.stringify(IndicateType_Model));
    },
    GetData: function () {
        var data = JSON.parse(localStorage.getItem('IndicateType_Model'));
        //
        localStorage.removeItem('IndicateType_Model');
        return data;
    },
}
//=============================================================================


var UI_Database_Set =
{
    //指标一级分类
    set_indicator_type: function () {
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
                if (typeid != 0) {
                    //父指标id赋值
                    $("#indicator_type").val(indicator_Parent_Id[0].Id);
                    set_indicator_type_2("one");
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    },

    //指标二级分类
    set_indicator_type_2: function () {
        type = arguments[0] || "";//""change时调用；"one"第一次加载
        var indicator_type = $("#indicator_type").val();
        $("#indicator_type_2").empty();
        if (indicator_type != 0) {
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
                    $("#indicator_type_2").append('<option value="0">--请选择--</option>');
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
                    if (typeid != 0 && type == "one") {
                        $("#indicator_type_2").val(indicator_name[0].Id);
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        else {
            $("#indicator_type_2").append('<option value="0">--请选择--</option>');
        }

    },
}