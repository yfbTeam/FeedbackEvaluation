<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertReward.aspx.cs" Inherits="FEWeb.TeaAchManage.InsertReward" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>录入个人竞赛奖</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <link href="../Scripts/choosen/chosen.css" rel="stylesheet" />
    <link href="../Scripts/choosen/prism.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <%--成员信息--%>
    <script type="text/x-jquery-tmpl" id="itemData">
    <tr class="memadd" un="${UniqueNo}">        
        <td><input type='checkbox' value="${UniqueNo}" name="ck_trsub" onclick="CheckSub(this);"/></td>       
        <td>${Name}</td>       
        <td>${MajorName}</td>
        <td><input type="number" value="" min="0" step="0.01" onblur="ChangeRankScore(this);"></td>
    </tr>
</script>
</head>
<body style="background: #fff;">
    <input type="hidden" name="Func" id="Func" value="AddAcheiveRewardInfoData" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="Status" id="Status" value="0" />
    <input type="hidden" name="Id" id="Id" value="0" />
    <input type="hidden" id="Group" name="Group" value="2"/>
    <input type="hidden" id="hid_UploadFunc" value="Upload_AcheiveReward"/>
    <div class="main">
        <div class="cont">
            <h2 class="cont_title"><span>获奖文件信息</span></h2>
            <div class="area_form clearfix">
                <div class="input_lable fl">
                    <label for="">发文号：</label>
                    <input type="text" isrequired="true" fl="发文号" name="FileEdionNo" id="FileEdionNo" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">文件名称：</label>
                    <input type="text" isrequired="true" fl="文件名称" name="FileNames" id="FileNames" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">认定机构：</label>
                    <input type="text" isrequired="true" fl="认定机构" name="DefindDepart" id="DefindDepart" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">认定日期：</label>
                    <input type="text" isrequired="true" fl="认定日期" name="DefindDate" id="DefindDate" value="" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy年MM月dd日'});" onkeydown="ChangeLid();"/>
                </div>
                <div class="clear"></div>
                    <div class="input_lable input_lable2">
                        <label for="">获奖扫描件：</label>                        
                        <div class="fl uploader_container">
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
            </div>
            <h2 class="cont_title"><span>基本信息</span></h2>
            <div class="area_form clearfix">
                <div class="input_lable">
                    <label for="">奖项名称：</label>
                    <input type="text" isrequired="true" fl="奖项名称" class="text" id="Name" name="Name" style="width: 692px;" />
                </div>
                <div class="input_lable fl">
                    <label for="">奖励项目：</label>
                    <select class="select" isrequired="true" fl="奖励项目" name="Gid" id="Gid" onchange="BindLinfo()"></select>
                </div>
                <div class="input_lable fl">
                    <label for="">获奖级别：</label>
                    <select class="select" isrequired="true" fl="获奖级别" name="Lid" id="Lid" onchange="BindRewardInfo()"></select>
                </div>
                <div class="input_lable fl">
                    <label for="">奖励等级：</label>
                    <select class="select" isrequired="true" fl="奖励等级" name="Rid" id="Rid" onchange="SetScore()"></select>
                </div>
                <div class="input_lable fl">
                    <label for="">获奖年度：</label>
                    <input type="text" isrequired="true" fl="获奖年度" class="text Wdate" name="Year" id="Year" onfocus="WdatePicker({dateFmt:'yyyy年'})" />
                </div>
                <div class="input_lable fl">
                    <label for="">负责人：</label>
                    <input type="text" name="ResponsMan_Name" id="ResponsMan_Name" class="text" readonly="readonly"/>
                    <input type="hidden" name="ResponsMan" id="ResponsMan" value=""/>                            
                </div>
                <div class="input_lable fl">
                    <label for="">负责单位：</label>
                    <select class="chosen-select" isrequired="true" fl="负责单位" data-placeholder="负责单位" id="DepartMent" name="DepartMent" multiple="multiple"></select>
                </div>
            </div>  
            <h2 class="cont_title members none"><span>成员信息</span></h2>
                <div class="area_form members none">
                    <div class="clearfix">
                        <span class="fr status mr10">已分配：<span id="span_CurScore">0</span></span>
                        <span class="fr status mr10">总分：<span id="span_AllScore">0</span></span>
                    </div>
                    <table class="allot_table mt10">
                        <thead>
                            <tr>
                                <th width="40px;"><input type="checkbox" name="ck_tball" onclick="CheckAll(this)"/></th>
                                <th>姓名</th>
                                <th>单位/部门</th>
                                <th>得分</th>
                            </tr>
                        </thead>
                        <tbody id="tb_Member"></tbody>
                    </table>
                </div>          
        </div>
    </div>
    <div class="score" style="border-top: 1px solid #ECDEE6;"></div>
    <div class="btnwrap2">
        <input type="button" value="保存" onclick="Save()" class="btn" />
        <input type="button" value="提交" onclick="submit()" class="btna" />
    </div>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../Scripts/choosen/prism.js"></script>
    <script src="../Scripts/Webuploader/dist/webuploader.js"></script>
    <link href="../Scripts/Webuploader/css/webuploader.css" rel="stylesheet"/>
    <script src="upload_batchfile.js"></script>
    <script src="BaseUse.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            $("#CreateUID").val(GetLoginUser().UniqueNo);
            $("#ResponsMan_Name").val(loginUser.Name);
            $("#ResponsMan").val(loginUser.UniqueNo);
            $("#itemData").tmpl([{ UniqueNo: loginUser.UniqueNo, Name: loginUser.Name, MajorName: '' }]).appendTo("#tb_Member");
            BindFile_Plugin();
            BindDepart("DepartMent");
            BindGInfo();
            var Id = UrlDate.Id;
            if (Id != undefined) {
                $("#Gid").attr("disabled", "disabled");
                $("#Lid").attr("disabled", "disabled");
                $("#Rid").attr("disabled", "disabled");
                $("#Id").val(Id);
                GetDataById(Id);
            }
        })
        function GetDataById() {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetAcheiveRewardInfoData", "IsPage": "false", Id: UrlDate.Id },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $(json.result.retData).each(function () {
                            $("#Name").val(this.Name);
                            $("#Gid").val(this.Gid);
                            BindLinfo();
                            $("#Lid").val(this.Lid);
                            BindRewardInfo();
                            $("#Rid").val(this.Rid);
                            SetScore();
                            $("#Year").val(this.Year);
                            $("#ResponsMan").val(this.ResponsMan);                          
                            $("#DepartMent").val(this.DepartMent);
                            $("#DepartMent").trigger("chosen:updated");
                            $("#DepartMent").chosen();
                            $("#FileEdionNo").val(this.FileEdionNo);
                            $("#FileNames").val(this.FileNames);
                            $("#DefindDate").val(this.DefindDate);
                            $("#DefindDepart").val(this.DefindDepart);
                            if (this.Status == "0") {
                                $(".btn").show();
                            }
                            else {
                                $(".btn").hide();
                            }
                        });
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function submit() {
            $("#Status").val("1");
            Save();
        }
        //提交按钮
        function Save() {
            //验证为空项或其他
            var valid_flag = validateForm($('select,input[type="text"]'));
            if (valid_flag !=0)////验证失败的情况  需要表单的input控件 有 isrequired 值为true或false 和fl 值为不为空的名称两个属性
            {
                return false;
            }
            if ($("#uploader .filelist li").length <= 0) {
                layer.msg("请上传获奖扫描件!");
                return;
            }
            var object = getFromValue();//组合input标签   
            object["DepartMent"] = $("#DepartMent").val().join(',');
            var addArray = Rtn_AddMemArray(0);
            object.MemberStr = addArray.length > 0 ? JSON.stringify(addArray) : '';
            var add_path = Get_AddFile();
            object.Add_Path = add_path.length > 0 ? JSON.stringify(add_path) : "";
            object.AchieveType = 1;
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: object,
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        parent.layer.msg('操作成功!');
                        parent.BindData(1,10);
                        parent.CloseIFrameWindow();
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }

        //奖励项目
        function BindGInfo() {
            $("#Gid").html('<option value="">请选择</option>');
            $("#Lid").html('<option value="">请选择</option>');
            $("#Rid").html('<option value="" ss="0">请选择</option>');           
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { "Func": "GetAcheiveLevelData", "IsPage": "false", "Pid": 2 },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $(json.result.retData).each(function () {
                            $("#Gid").append('<option value="' + this.Id + '">' + this.Name + '</option>');
                        })
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //获奖级别
        function BindLinfo() {
            $("#Lid").html('<option value="">请选择</option>');
            $("#Rid").html('<option value="" ss="0">请选择</option>');
            if (!$("#DefindDate").val().trim().length) {
                layer.msg("请先指定认定日期");
                return;
            }
            if ($("#Gid").val() != 0) {
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                    type: "post",
                    dataType: "json",
                    async: false,
                    data: { "Func": "GetRewardLevelData", "LID": $("#Gid").val(), DefindDate: $("#DefindDate").val().trim(), "IsPage": "false" },
                    success: function (json) {
                        if (json.result.errMsg == "success") {
                            $(json.result.retData).each(function () {
                                $("#Lid").append('<option value="' + this.Id + '">' + this.Name + '</option>');
                            })
                        }
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            }
        }
        //奖励等级
        function BindRewardInfo() {
            $("#Rid").html('<option value="" ss="0">请选择</option>');
            if ($("#Lid").val() != 0) {
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                    type: "post",
                    dataType: "json",
                    async: false,
                    data: { "Func": "GetRewardInfoData", "LID": $("#Lid").val(), "IsPage": "false" },
                    success: function (json) {
                        if (json.result.errMsg == "success") {
                            $(json.result.retData).each(function () {
                                $("#Rid").append('<option value="' + this.Id + '" ss="' + this.Score + '">' + this.Name + '</option>');
                            })
                        }
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            }
        }       
    </script>
</body>

</html>
