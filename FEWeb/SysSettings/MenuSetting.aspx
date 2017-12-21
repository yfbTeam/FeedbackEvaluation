<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuSetting.aspx.cs" Inherits="FEWeb.SysSettings.MenuSetting" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>导航设置</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
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
        }

        .operate_none {
            height: 35px;
            line-height: 35px;
            min-width: 40px;
        }
    </style>
</head>
<body style="background: #fff;">
    <input type="hidden" id="Pid" />
    <input type="hidden" id="CreateUID" value="011" />

    <div class="main clearfix" style="min-height: 517px;">
        <div class="menu fl">
            <ul class="menu_list">
                <li><span class="selected">教学成果奖</span></li>
                <li><span>教学成果奖</span></li>
                <li><span>教学成果奖</span></li>
                <li><span>教学成果奖</span></li>
            </ul>
        </div>
        <div class="menu_right">
            <div class="btnwrap" style="width: 0">
                <input type="button" value="新增" class="btn" onclick="addNew()" />
            </div>

            <ul id="Child">
            </ul>
        </div>
    </div>
    <%--<div class="btnwrap">
        <input type="button" value="确定" onclick="submit()" class="btn" />
        <input type="button" value="取消" class="btna" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>--%>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script>
        $(function () {
            //$('.menu_list>li').click(function () {
            //    $('.menu_list>li>span').removeClass('selected');
            //    $(this).children('span').addClass('selected');
            //})
            //tableSlide();
            $("#CreateUID").val(GetLoginUser().UniqueNo);

            GetNavData();
            BindNav();
        })
        //绑定左侧菜单
        function GetNavData() {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { "Func": "GetAcheiveLevelData", "IsPage": "false" },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        AcheiveLevel = json.result.retData;
                        parent.menu_list(AcheiveLevel);
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function BindNav() {
            $(".menu_list").empty();
            var i = 0;
            $(AcheiveLevel).each(function () {
                if (this.Pid == "0") {
                    if (i == 0) {
                        $(".menu_list").append("<li id=" + this.Id + " onclick='ChangeMenu(this," + this.Id + ")'><span class='selected'>" + this.Name + "</span></li>");
                        ChangeMenu("", this.Id);
                        $("#Pid").val(this.Id);
                    }
                    else {
                        $(".menu_list").append("<li id=" + this.Id + " onclick='ChangeMenu(this," + this.Id + ")'><span>" + this.Name + "</span></li>")
                    }
                    i++;
                }
            })
        }
        //左侧菜单点击事件
        function ChangeMenu(em, Id) {
            $("#Pid").val(Id);
            $("#Child").empty();
            if (em != "") {
                $(em).find("span").addClass("selected").parent().siblings().find("span").removeClass("selected");
            }
            $(AcheiveLevel).each(function () {
                if (this.Pid == Id) {
                    $("#Child").append('<li class="clearfix">' +
                '<input type="text" name="" value="' + this.Name + '" class="text fl"/>' +
               '<div class="operates fl">' +
                   '<div class="operate" onclick="Save(' + this.Id + ',this)">' +
                       '<i class="iconfont color_purple">&#xe66f;</i><span class="operate_none bg_purple">保存</span>' +
                   '</div>' +
                   '<div class="operate" onclick="MenuSort(\'up\',' + this.Id + ')">' +
                       '<i class="iconfont color_purple">&#xe629;</i><span class="operate_none bg_purple">上移</span>' +
                   '</div>' +
                   '<div class="operate" onclick="MenuSort(\'down\',' + this.Id + ')">' +
                       '<i class="iconfont color_purple">&#xe62d;</i><span class="operate_none bg_purple">下移</span>' +
                   '</div>' +
               '</div>' +
            '</li>')
                }
            })
            tableSlide();
            IsShowSort();
        }
        //第一条限制上移 最后一条限制下移
        function IsShowSort() {
            var Count = $('#Child').find("li").length;
            var $First = $('#Child').find("li:eq(0)").find('div').children().eq(1);
            $First.removeAttr("onclick");
            $First.find('i').removeClass('color_purple').addClass('color_gray');
            $First.find('span').removeClass('bg_purple').addClass('bg_gray');

            var $Last = $('#Child').find("li:eq(" + parseInt(Count - 1) + ")").find('div').children().eq(2);
            $Last.removeAttr("onclick");

            $Last.find('i').removeClass('color_purple').addClass('color_gray');
            $Last.find('span').removeClass('bg_purple').addClass('bg_gray');

        }

        function addNew() {

            $("#Child").append('<li class="clearfix">' +
                '<input type="text" name="" value="" class="text fl"/>' +
               '<div class="operates fl">' +
                   '<div class="operate" onclick="Save(0,this)">' +
            '<i class="iconfont color_purple">&#xe66f;</i><span class="operate_none bg_purple">保存</span>' +
                   '</div>' +
               '</div>' +
            '</li>');
        }

        /*
        function addNew(em) {
            var Index = $(em).parents("li").index();
            $("#Child li").eq(Index).after('<li class="clearfix">' +
                '<input type="text" name="" value="" class="text"/>' +
               '<div class="operates fl">' +
                   '<div class="operate" onclick="addNew(this)">' +
                       '<i class="iconfont color_purple">&#xe649;</i><span class="operate_none bg_purple">添加</span>' +
                   '</div>' +
                   '<div class="operate" onclick="MenuSort(\'up\',${Id})">' +
                       '<i class="iconfont color_purple">&#xe629;</i><span class="operate_none bg_purple">上移</span>' +
                   '</div>' +
                   '<div class="operate" onclick="MenuSort(\'down\',${Id})">' +
                       '<i class="iconfont color_purple">&#xe62d;</i><span class="operate_none bg_purple">下移</span>' +
                   '</div>' +
               '</div>' +
            '</li>');
        }*/
        function Save(Id, em) {
            var Name = $(em).parents("li").find("input").val();
            if (!Name) {
                layer.msg("项目名称不能为空");
            }
            else {
                var Pid = $("#Pid").val();
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                    type: "post",
                    dataType: "json",
                    data: { "Func": "AddAcheiveLevelData", "Name": Name, Pid: Pid, Id: Id },
                    success: function (json) {
                        if (json.result.errMsg == "success") {
                            layer.msg('操作成功!');
                            GetNavData();
                            ChangeMenu("", Pid)
                        }
                        else {
                            layer.msg(json.result.errMsg);
                            ChangeMenu();
                        }
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            }
        }

        function MenuSort(SortType, Id) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "TPM_AcheiveLevelSort", SortType: SortType, Id: Id },
                success: function (json) {
                    if (json.result.errNum ==0) {
                        // parent.layer.msg('操作成功!');
                        GetNavData();
                        ChangeMenu("", $("#Pid").val());
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
    </script>
</body>
</html>
