<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadyfyDemo.aspx.cs" Inherits="UploadyfyDemo" %>

<!DOCTYPE html>

<html>
<head>
    <title>uploadify-实例</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="uploadify/uploadify.css" rel="stylesheet" />
    <script src="../jquery-1.8.2.min.js"></script>
    <script src="uploadify/jquery.uploadify-3.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //var FirstUrl = window.location.href;
            //FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

            //var formData = { 'HFoldUrl': GetRequest().HFoldUrl, 'UrlName': GetRequest().UrlName, 'hSubject': GetRequest().hSubject, 'hContent': GetRequest().hContent, 'HStatus': GetRequest().HStatus, 'capacity': GetRequest().capacity };
            $("#uploadify").uploadify({
                'swf': 'uploadify/uploadify.swf',
                'uploader': 'upload.ashx',
                //'buttonCursor': 'hand',
                'buttonText': '浏览',
                //'cancelImg': 'upload/uploadify-cancel.png',
                //'buttonImage': FirstUrl + '/_layouts/15/SVDigitalCampus/Image/bg.png',
                // 'folder': '/jxdBlog/photos',
                'auto': false,
                'multi': true,
                //'wmode': 'transparent',
                'queueID': 'fileQueue',
                'width': '100%',
                'height': 70,
                'fileSizeLimit': '100MB',
                'fileTypeDesc': '文件',       //图片选择描述  
                'queueSizeLimit': 10,                   //一个队列上传文件数限制  
                'removeTimeout': 1,                  //完成时清除队列显示秒数,默认3秒  
                'successTimeout': 20,                  //上传超时  
                //'fileTypeExts': '*.docx;*.doc;*.ppt;*.pptx;*.pdf;*.caj;*.txt;*.rar;*.zip;*.jpg;*.gif;',
                //'fileTypeDesc:': '请选择docx doc ppt pptx pdf caj txt rar zip jpg gif文件',
                'progressData': 'all',

                //'formData': formData, //参数
                'removeCompleted': true,
                'overrideEvents': ['onUploadProgress', 'onDialogClose', 'onUploadSuccess', 'onUploadError', 'onSelectError'],
                'onUploadSuccess': function (file, data, response) {

                    if (data.indexOf('错误提示') > -1) {
                        alert(data);
                    }

                    else {
                        $("#" + file.id).find(".data").html("上传成功");

                    }
                },
                'onSelectError': function (file, errorCode, errorMsg) {
                    var msgText = "上传失败!!\n";
                    switch (errorCode) {
                        case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                            msgText += "每次最多选择 " + this.settings.queueSizeLimit + "个文件";
                            break;
                        case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                            msgText += "文件大小超过限制( " + this.settings.fileSizeLimit + " )";
                            break;
                        case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                            msgText += "文件大小为0";
                            break;
                        case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                            msgText += "文件格式不正确，仅限 " + this.settings.fileTypeExts;
                            break;
                        default:
                            msgText += "错误代码：" + errorCode + "\n" + errorMsg;
                    }
                    alert(msgText);

                },
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    // 手工取消不弹出提示  
                    if (errorCode == SWFUpload.UPLOAD_ERROR.FILE_CANCELLED
                            || errorCode == SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED) {
                        return;
                    }
                    var msgText = "上传失败\n";
                    switch (errorCode) {
                        case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
                            msgText += "HTTP 错误\n" + errorMsg;
                            break;
                        case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
                            msgText += "上传文件丢失，请重新上传";
                            break;
                        case SWFUpload.UPLOAD_ERROR.IO_ERROR:
                            msgText += "IO错误";
                            break;
                        case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
                            msgText += "安全性错误\n" + errorMsg;
                            break;
                        case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                            msgText += "每次最多上传 " + this.settings.uploadLimit + "个";
                            break;
                        case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
                            msgText += errorMsg;
                            break;
                        case SWFUpload.UPLOAD_ERROR.SPECIFIED_FILE_ID_NOT_FOUND:
                            msgText += "找不到指定文件，请重新操作";
                            break;
                        case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:
                            msgText += "参数错误";
                            break;
                        default:
                            msgText += "文件:" + file.name + "\n错误码:" + errorCode + "\n"
                                     + errorMsg + "\n" + errorString;
                    }
                    alert(msgText);
                },
                //上传汇总  
                'onUploadProgress': function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                    var persent = parseInt(100 * bytesUploaded / bytesTotal);

                    $("#" + file.id).find(".data").html(persent + "%");
                    $("#" + file.id).find(".uploadify-progress-bar").css("width", persent + "%");

                },
                //返回一个错误，选择文件的时候触发             
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    alert('文件 ' + file.name + '上传失败: ' + errorString);
                },
                //检测FLASH失败调用              
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                'onAllComplete': function (event, data) {
                    alert(data.filesUploaded + '个图片上传成功');
                }

            });
        });
        //变换按钮  
        function changeBtnText() {
            $('#uploadify').uploadify('settings', 'buttonText', '继续上传');
        }


        //返回按钮  
        function returnBtnText() {
            alert('The button says ' + $('#uploadify').uploadify('settings', 'buttonText'));
        }
    </script>

</head>
<body>
    <table style="width: 90%;">
        <tr>
            <td style="width: 100px;">
                <input type="file" name="uploadify" id="uploadify" />
            </td>
            <td align="left">
                <a href="javascript:$('#uploadify').uploadify('upload', '*')">开始上传</a>|  
            <a href="javascript:jQuery('#uploadify').uploadify('cancel')">取消上传</a>|
                <a href="javascript:$('#uploadify').uploadify('cancel', '*');">清除队列</a>  |   
                <a href="javascript:$('#uploadify').uploadify('destroy');">销毁上传</a>  |   
                <a href="javascript:$('#uploadify').uploadify('disable', true);">禁用上传</a>  |   
                <a href="javascript:$('#uploadify').uploadify('disable', false);">激活上传</a>  |   
                <a href="javascript:$('#uploadify').uploadify('stop');">停止上传</a>  |   
                <a href="javascript:changeBtnText();">变换按钮</a>  |   
                <span id="result" style="font-size: 13px; color: red"></span>
            </td>
        </tr>
    </table>
    <div id="fileQueue" style="width: 500px; height: auto; border: 2px solid green;"></div>
</body>
</html>
