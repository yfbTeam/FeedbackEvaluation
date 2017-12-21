<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportUser.aspx.cs" Inherits="FEWeb.TeaAchManage.ImportUser" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>业绩导入</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#uploadify").uploadify({
                'auto': true,                      //是否自动上传
                'swf': '../Scripts/Uploadyfy/uploadify/uploadify.swf',
                'uploader': 'Uploade.ashx',
                'formData': { Func: "UplodExcel" }, //参数
                'fileTypeExts': '*.xls;*.xlsx',
                'buttonText': '选择Excel',//按钮文字
                // 'cancelimg': 'uploadify/uploadify-cancel.png',
                'width': 90,
                'height': 24,
                //最大文件数量'uploadLimit':
                'multi': false,//单选            
                'fileSizeLimit': '10MB',//最大文档限制
                'queueSizeLimit': 1,  //队列限制
                'removeCompleted': true, //上传完成自动清空
                'removeTimeout': 0, //清空时间间隔
                //'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError', 'onSelectError'],
                'onUploadSuccess': function (file, data, response) {
                    var json = $.parseJSON(data);
                    $("#weike").attr("src", json.url);
                },

            });
        });
    </script>
</head>
<body style="background: #F8FCFF;">
    <a id="weike"></a>
    <div class="p20">
        <div class="insert—content">
            <div class="row_l clearfix">
                <label for="">
                    资源文件：
                </label>
                <div class="row—content">
                    <div class="upload_imga">
                        <input type="file" name="" id="uploadify" multiple="multiple" />

                    </div>
                </div>
            </div>
            <div class="row_l clearfix">
                <label for="">
                    说明：
                </label>
                <div class="row—content">
                    <p>导入的用户数据文件必须是Excel文件（.xls和.xlsx）！</p>
                    <p>导入的用户数据的字段及排列顺序必须和模板文件中的一致！</p>
                    <p>[<a href="/UserManage/ExcelModel/UserInfo.xlsx">点此下载模板文件</a>]</p>
                    <p>模板文件中的红色字体为必填项，必须填写才能导入成功！</p>
                    <p>导入时间跟用户数据多少成正比,请耐心等待</p>
                </div>
                <div class="row—content" id="Prompt" style="color: #91c954; overflow: hidden; margin-left: 72px; word-break: break-all;">
                </div>

                <%--<div class="row—content" id="Prompt" style="color: #91c954; overflow: hidden; margin-left: 72px;">
                </div>--%>
            </div>
        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="导入" class="btns insert" onclick="Import()" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="Close()" />
        </div>
    </div>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/Uploadify/uploadify/jquery.uploadify-3.1.min.js"></script>

    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        function Close() {
            parent.CloseIFrameWindow();
        }

        function Import() {
            var OrgNo = UrlDate.OrgNo;
            var FilePath = $("#weike").attr("src");
            if (FilePath == undefined || FilePath == "") {
                layer.msg("请上传要导入的文件");
            }
            else {
                $.ajax({
                    url: "Uploade.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        //PageName: "/UserManage/UserInfo.ashx",
                        func: "ImportUser",
                        FilePath: $("#weike").attr("src"),
                        SysAccountNo: SysAccountNo,
                        OrgNo: OrgNo
                    },
                    success: function (json) {
                        var result = json.result;
                        if (result.errNum == 0) {
                            $("#Prompt").html(result.errMsg);
                            parent.getData(1, 10);
                        } else {
                            $("#Prompt").html("导入失败");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $("#Prompt").html("导入失败");
                    }
                });
            }
        }
    </script>
</body>
</html>
