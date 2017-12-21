<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Group_User_Add.aspx.cs" Inherits="FEWeb.SysSettings.Group_User_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="input-wrap">
                <label>用户组名称：</label><input type="text" class="text" id="btn_Group_User_Add">
            </div>
            <div class="input-wrap clearfix">
                <label>角色类型：</label>
                    <span class="ml10">
                    <input type="radio" name="role" id="teacher" class="magic-radio" checked value="1">
                    <label for="teacher">教师</label>
                </span>
                <span class="ml10">
                    <input type="radio" name="role" id="student" class="magic-radio" value="0">
                    <label for="student">学生</label>
                </span>
            </div>
            <div class="btnwrap">
                <input type="button" value="保存" class="btn" />
                <input type="button" value="取消" class="btna" onclick="cancel();" />
            </div>
        </div>
    </form>

    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/public.js"></script>

    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/WebCenter/Power.js"></script>
</body>
</html>

<script>
    var type = Number(getQueryString('type'));
    var index = parent.layer.getFrameIndex(window.name);
    $(function () {
    
        switch (type) {
            case 1: //添加
                $('.btn').on('click', function () {
                    UI_Power.Add_Compleate = function () {                       
                        parent.SelectGourp_Init();
                        layer.msg('操作成功!');
                        parent.layer.close(index);
                    };
                    UI_Power.Add($('#btn_Group_User_Add').val());                   
                });
                break;
            case 2:  //编辑
                UI_Power.Get();

                $('#btn_Group_User_Add').val(UI_Power.CurrentRoleName);
                $('.btn').on('click', function () {
                    UI_Power.Edit_Compleate = function () {
                        parent.SelectGourp_Init();
                        layer.msg('操作成功!');
                        parent.layer.close(index);
                    };
                    UI_Power.Edit(UI_Power.CurrentRoleid, $('#btn_Group_User_Add').val());                                     
                });
                break;
            default:
        }
    });

    function cancel() {     
        parent.layer.close(index);
    }

</script>
