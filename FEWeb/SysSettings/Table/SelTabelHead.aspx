<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelTabelHead.aspx.cs" Inherits="FEWeb.SysSettings.SelTabelHead" %>

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
    <style>
        .options label {
            display: inline-block;
            line-height: 33px;
        }
    </style>

    <script type="text/x-jquery-tmpl" id="item_check">
        <div class="options fl mr10">
            <input type="checkbox" description="${description}" name="ck_item" id="${id}" Code="${Code}" class="magic-checkbox">
            <label for="${id}">${name}</label>
        </div>
    </script>

</head>


<body style="background: #FFF;">
    <div class="main clearfix" id="list">
      
    </div>
    <div class="btnwrap">
        <input type="button" value="确定" onclick="submit()" class="btn">
        <input type="button" value="取消" onclick="closeWindow()" class="btna ml10">
    </div>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/public.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
    <script src="../../Scripts/WebCenter/TableDesigin.js"></script>
    <script type="text/javascript">
        var index = parent.layer.getFrameIndex(window.name);//弹框的索引
        $(function () {
            UI_Table_Create.PageType = 'SelTabelHead';
            UI_Table_Create.Get_Eva_Table_Header_Custom_List_Compleate = function () {
                var head_vs = parent.get_tablehead();
               
                for (var i in head_vs) {
                    $('#' + head_vs[i].CustomCode).attr('checked', true);
                }
            };
            UI_Table_Create.Get_Eva_Table_Header_Custom_List();
        });
        function submit() {
            var nodes = new Array();
            //父页面的回调函数，很重要
            $(".options").each(function () {
                var check = $(this).children("input").eq(0);
                var label = $(this).children("label").eq(0);
                if (check.is(":checked")) {
                    var obj = new Object();
                    obj.id = check.attr("id");
                    obj.description = check.attr("description");
                    obj.name = label.text();
                    obj.Code = check.attr("Code");
                    nodes.push(obj);
                }
            });
            parent.tablehead(nodes);
            closeWindow();
        }
        function closeWindow() {
            parent.layer.close(index);
        }
    </script>
</body>
</html>
