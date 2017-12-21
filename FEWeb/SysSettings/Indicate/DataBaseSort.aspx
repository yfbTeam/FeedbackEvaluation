<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseSort.aspx.cs" Inherits="FEWeb.SysSettings.DataBaseSort" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>导航设置</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/layout.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .menu_right ul li:first-child {
            border-bottom: 1px solid #ccc;
            padding-bottom: 15px;
            margin-bottom: 15px;
        }

        .menu_right {
            width: 620px;
            float: right;
        }

            .menu_right ul li {
                margin-bottom: 20px;
            }

        .operates {
            height: 35px;
            margin-left: 10px;
        }

        .operate {
            min-width: 40px;
            height: 35px;
            line-height: 35px;
            text-align: center;
        }

        .text {
            width: 480px;
            height: 33px;
            border: 1px solid #dcdcdc;
            border-radius: 3px;
            text-indent: 10px;
        }

        .operate_none {
            height: 35px;
            line-height: 35px;
            min-width: 40px;
        }

        .menu_list li.selected span {
            background: #fff;
            border-left: 2px solid #ffac32;
            border-top: 1px solid #EDEDED;
        }

        .menu {
            border: 1px solid #e5f8f4;
        }
    </style>
   
</head>
<body style="background: #fff;" onkeydown="Key_Add();">
    <div class="main clearfix" style=" min-height: 517px;">
        <div class="menu fl">
            <ul class="menu_list">
            </ul>
            <div style="margin-left: 45px; margin-top: 20px">
                <input type="button" value="新增指标分类" onclick="Add_Parent();" class="btn fl " />
            </div>
        </div>
        <div class="menu_right">
            <div class=" clearfix mb20">
                <input type="button" value="新增指标项" onclick="Add();" class="btn fr ml20" />

            </div>
            <ul id="Child" class="mt10">
            </ul>
        </div>
    </div>
</body>
</html>

<script src="../../Scripts/Common.js"></script>
<script src="../../Scripts/public.js"></script>
<script src="../../Scripts/linq.min.js"></script>
<script src="../../scripts/jquery.tmpl.js"></script>
<script src="../../Scripts/layer/layer.js"></script>
<script src="../../Scripts/WebCenter/DatabaseMan.js"></script>
 <script type="text/x-jquery-tmpl" id="item_indicatorType">
        <li>
            <span>${self.Name}<input type="hidden" value="${self.Id}" /></span>
        </li>
    </script>
<script type="text/x-jquery-tmpl" id="item_Indicate_Type">
    <li class="clearfix">
        <input type="text" name="" onkeydown="Key_Edit(${Id},${Parent_Id})" value="${Name}" id="${Id}" class="text fl" />
        <div class="operates fl">
            <div class="operate" onclick="Edit(${Id},${Parent_Id})">
                <i class="iconfont color_purple">&#xe631;</i><span class="operate_none bg_purple">保存</span>
            </div>
            <div class="operate" onclick="Remove(${Id},${Parent_Id})">
                <i class="iconfont color_purple">&#xe61b;</i><span class="operate_none bg_purple">删除</span>
            </div>
        </div>
    </li>
</script>

<script type="text/x-jquery-tmpl" id="item_Indicate_Type_No_Delete">
    <li class="clearfix">
        <input type="text" name="" onkeydown="Key_Edit(${Id},${Parent_Id})" value="${Name}" id="${Id}" class="text fl" />
        <div class="operates fl">
            <div class="operate" onclick="Edit(${Id},${Parent_Id})">
                <i class="iconfont color_purple">&#xe631;</i><span class="operate_none bg_purple">保存</span>
            </div>
            <div class="operate">
                <i class="iconfont color_gray">&#xe61b;</i>
            </div>
        </div>
    </li>
</script>
<script>
    //选择的指标库分类ID
    var type_id = Number(getQueryString('type_id'));
    var Userinfo_json = GetLoginUser();
    var Sys_Role = Userinfo_json.Sys_Role_Id;
    var index = parent.layer.getFrameIndex(window.name);
    //自我集合
    var self_list = [];
    $(function () {
        IndicateType_Model.init();
    })
    function reflesh() {
        $('.menu_list').find('input[value=' + type_id + ']').trigger('click');
    }

    function Key_Edit(Id, Parent_Id) {
        if (event.keyCode == '13') {
            Edit(Id, Parent_Id);
        }
    }

    function Key_Add() {
        if (event.keyCode == '187') {
            Add();
        }
    }

    function Edit(Id, Parent_Id) {
        IndicateType_Model.Id = Id;
        IndicateType_Model.Name = $('#' + Id).val();
        IndicateType_Model.EditUID = GetLoginUser().UniqueNo;
        IndicateType_Model.Edit_Data_Compleate = function (result) {
            if (result) {
                if (Parent_Id == 0) {
                    IndicateType_Model.init();
                }
                parent.reflesh_Left(type_id);
            }
            else {
                IndicateType_Model.init();
            }
        };
        IndicateType_Model.Edit_Data();
    }
    function Remove(Id, Parent_Id) {
        layer.confirm('确定要删除？', {
            btn: ['确定', '取消'], //按钮
            title: '操作'
        }, function () {
            IndicateType_Model.Id = Id;
            IndicateType_Model.Remove_Data_Compleate = function (result) {
                layer.msg('操作成功');
                if (Parent_Id == 0) {
                    IndicateType_Model.init();
                    //最后一个进行模拟点击
                    $('.menu_list li:last').trigger('click');
                    parent.reflesh_Left(type_id);
                }
                else {
                    IndicateType_Model.init();
                    parent.reflesh_Left(type_id);
                }
            };
            IndicateType_Model.Remove_Data();
        });
    }
    function Add() {
        IndicateType_Model.Name = "新建指标库";
        IndicateType_Model.P_Id = type_id;
        IndicateType_Model.CreateUID = GetLoginUser().UniqueNo;
        IndicateType_Model.Add_Data_Compleate = function () {
            layer.msg('操作成功');
            IndicateType_Model.init();
            parent.reflesh_Left(type_id);
        };
        IndicateType_Model.Add_Data();
    }
    function Add_Parent() {
        IndicateType_Model.Name = "新建指标库分类";
        IndicateType_Model.P_Id = 0;
        IndicateType_Model.CreateUID = GetLoginUser().UniqueNo;
        IndicateType_Model.Add_Data_Compleate = function () {
            layer.msg('操作成功');
            IndicateType_Model.init();

            $('.menu_list li:last').trigger('click');
            parent.reflesh_Left(type_id);
        };
        IndicateType_Model.Add_Data();
    }
</script>
