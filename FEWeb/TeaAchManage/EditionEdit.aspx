<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditionEdit.aspx.cs" Inherits="FEWeb.TeaAchManage.EditionEdit" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>编辑版本</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .menu{
            min-height:413px;
        }
        .menu_right{
            width:520px;
        }
        .del{
            color:#6a264b;text-decoration:underline;margin-left:10px;
        }
        .input_lable2{text-align:left;}
    </style>
</head>
<body>
    <input type="hidden" name="Func" value="AddRewardEditionData" />
    <!--这两个必须放在上边-->
    <input type="hidden" name="id" id="id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="EditUID" id="EditUID" value="011" />
    <input type="hidden" name="LID" id="LID"/>
    <input type="hidden" id="hid_UploadFunc" value="Upload_RewardEdition"/>
    <div class="main">
        <div class="clearfix">
            <div class="menu fl">
                <ul class="menu_lists" id="ul_Edition"></ul>
            </div>
            <div class="menu_right fr">
                <div class="input-wrap">
                    <label>版本名称：</label><input type="text" isrequired="true" fl="版本名称" id="Name" name="Name" class="text" placeholder="请输入版本名称" />
                </div>
                <div class="input-wrap">
                    <label>生效时间：</label>
                    <input type="text" class="text Wdate" id="BeginTime" name="BeginTime" isrequired="true" fl="开始时间" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate: '#F{$dp.$D(\'EndTime\')}',position:{left:0,top:-231}})" style="width: 150px;" />
                    <span style="padding-left: 10px;">~</span>
                    <input type="text" class="text Wdate" id="EndTime" name="EndTime" isrequired="true" fl="结束时间" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate: '#F{$dp.$D(\'BeginTime\')}',position:{left:0,top:-231}})" style="width: 150px;" />
                </div>
                <div class="input-wrap input_lable2">
                    <label for="" style="min-width:100px;">相关：</label>
                    <div class="fl uploader_container" style="padding-left:110px;">
                            <div id="uploader">
                                <div class="queueList">
                                    <div id="dndArea" class="placeholder photo_lists">
                                        <div id="filePicker"></div>
                                        <ul class="filelist clearfix"></ul>
                                    </div>
                                </div>
                                <div class="statusBar" style="display: none;">
                                    <div class="progress">
                                        <span class="text">0%</span>
                                        <span class="percentage"></span>
                                    </div>
                                    <div class="info"></div>                                
                                </div>
                            </div>
                        </div>
                </div>
                <div style="text-align:center;margin-top:60px;">
                    <input type="button" value="保存" onclick="submit()" class="btn" />
                    <input type="button" value="取消" class="btna" onclick="javascript: parent.CloseIFrameWindow();" />
                    <a id="a_Delbtn" href="javascript:;" class="del">删除</a>
                </div>
            </div>
        </div>
            </div>
        <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>   
    <script src="../Scripts/jquery.tmpl.js"></script>
  
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/Webuploader/dist/webuploader.js"></script>
    <link href="../Scripts/Webuploader/css/webuploader.css" rel="stylesheet" />
    <script src="upload_batchfile.js"></script>
<script type="text/javascript">
    var UrlDate = new GetUrlDate();
    var index = parent.layer.getFrameIndex(window.name);
    var Edtion_Data = [];
    $(function () {
        $("#CreateUID").val(GetLoginUser().UniqueNo);
        $("#LID").val(UrlDate.LID);
        BindFile_Plugin();
        BindEdition(UrlDate.id || 0);
    });
    //奖项版本
    function BindEdition(id) {
        $("#ul_Edition").html("");
        $.ajax({
            url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
            type: "post",
            dataType: "json",           
            data: { "Func": "GetRewardEditionData", "IsPage": "false", "LID": $("#LID").val()},
            success: function (json) {              
                if (json.result.errMsg == "success") {
                    Edtion_Data = json.result.retData;
                    $(Edtion_Data).each(function (i, n) {                       
                        if (i == 0 && id==0) {
                            id = n.Id;
                        }
                        $("#ul_Edition").append('<li eid="' + n.Id + '"><em title="' + n.Name + '">' + n.Name + '</em></li>');
                    });
                    SetControlValue(id);
                    $("#ul_Edition li").click(function (i, n) {                       
                        SetControlValue($(this).attr("eid"));
                    });
                }
                else {
                    $("#ul_Edition").html("<li>暂无版本</li>");
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
    function SetControlValue(id) {       
        $("#id").val(id);
        $("#a_Delbtn").off('click');
        $("#a_Delbtn").on('click', function () { Del_RewardEdition(); });
        $("#ul_Edition li[eid='" + id + "']").addClass("selected").siblings().removeClass("selected");
        var leftdata = Edtion_Data;
        var cur_e = Enumerable.From(leftdata).Where("x=>x.Id=='" + id + "'").FirstOrDefault();
        $("#Name").val(cur_e.Name);
        $("#BeginTime").val(DateTimeConvert(cur_e.BeginTime, 'yyyy-MM-dd'));
        $("#EndTime").val(DateTimeConvert(cur_e.EndTime, 'yyyy-MM-dd'));
        Get_Sys_Document(1,$("#id").val());
    }
    //提交按钮
    function submit() {
        //验证为空项或其他
        var valid_flag = validateForm($('select,input[type="text"]'));
        if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有 isrequired 值为true或false 和fl 值为不为空的名称两个属性
        {
            return false;
        }
        var object = getFromValue();//组合input标签
        var add_path = Get_AddFile(1);
        object.Add_Path = add_path.length > 0 ? JSON.stringify(add_path) : "";
        object.Edit_PathId = Get_EditFileId();
        $.ajax({
            url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
            type: "post",
            dataType: "json",
            data: object,
            success: function (json) {
                if (json.result.errMsg == "success") {
                    layer.msg('操作成功!');
                    BindEdition($("#id").val());
                    Del_Document();
                    parent.BindEdition($("#LID").val());
                } else if (json.result.errNum == -1) {
                    var warn = "";
                    $(json.result.retData).each(function (i, n) {
                        warn += "与版本-" + n.Name + "时间交叉（" + DateTimeConvert(n.BeginTime, 'yyyy-MM-dd') + "~" + DateTimeConvert(n.EndTime, 'yyyy-MM-dd') + "）；";
                    });
                    layer.msg(warn);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
    //删除版本
    function Del_RewardEdition() {
        var del_path = [];
        $("#uploader .filelist li").each(function (i, n) {
            del_path.push($(this).attr('pt'));
        });
        layer.confirm('确定删除该版本吗？', {
            btn: ['确定', '取消'], //按钮
            title: '操作'
        }, function (index) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Del_RewardEdition",LID:$("#LID").val(), ItemId: $("#id").val(), BeginTime: $("#BeginTime").val(), EndTime: $("#EndTime").val() },
                success: function (json) {
                    if (json.result.errNum == 0) {                        
                        layer.close(index);
                        layer.msg('操作成功!');
                        Del_EditionDoc(del_path);
                        BindEdition(0);
                        parent.BindEdition($("#LID").val());
                    } else if (json.result.errNum == -1) {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function () { }
            });
        }, function () { });
    }
    function Del_EditionDoc(del_path) {
        if (del_path.length > 0) {
            $.ajax({
                url: "/CommonHandler/UploadHtml5Handler.ashx",
                type: "post",
                dataType: "json",
                data: { Func: "Del_Document", FilePath: JSON.stringify(del_path) },
                success: function (json) { }
            });
        }        
    }
</script>
</body>
</html>


