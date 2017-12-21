<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcheveRewadInfoEdit.aspx.cs" Inherits="FEWeb.TeaAchManage.AcheveRewadInfoEdit" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>修改教师业绩</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <link href="../Scripts/choosen/chosen.css" rel="stylesheet" />
    <link href="../Scripts/choosen/prism.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/x-jquery-tmpl" id="tr_Info">
        <tr>
            <td>${Name}</td>
            <td>{{if ULevel==0}}独著 {{else ULevel==1}}主编{{else ULevel==2}}参编{{else}}其他人员{{/if}}</td>           
            <td>${Sort}</td>
            <td>${Major_Name}</td>
            <td>${WordNum}</td>
        </tr>
    </script>
    <%--成员信息(已添加的)--%>
    <script type="text/x-jquery-tmpl" id="tr_MemEdit">
        {{each(i, mem) AcheiveMember_data.retData}}    
        <tr id="tr_mem_${mem.Id}" class="memedit {{if i==0}}meditor{{/if}}" un="${mem.UserNo}">
            <td>{{if i!=0}}<input type='checkbox' value="${mem.UserNo}" name="ck_trsub" onclick="CheckSub(this);"/>{{/if}}</td>
            <td>${mem.Name}</td>
            <td>${mem.Major_Name}</td>
            <td><input type="number" value="${mem.Score}" min="0" step="0.01" onblur="ChangeRankScore(this);"></td>
        </tr>
        {{/each}}      
    </script>
     <%--成员信息(新添加的)--%>
    <script type="text/x-jquery-tmpl" id="itemData">
        <tr class="memadd" un="${UniqueNo}">
            <td><input type='checkbox' value="${UniqueNo}" name="ck_trsub" onclick="CheckSub(this);" /></td>
            <td>${Name}</td>
            <td>${MajorName}</td>
            <td><input type="number" value="" min="0" step="0.01" onblur="ChangeRankScore(this);"></td>
        </tr>
    </script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        });
    </script>
</head>
<body>
    <input type="hidden" name="Func" value="AddAcheiveRewardInfoData" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="Status" id="Status" value="0" />
    <input type="hidden" name="Id" id="Id" value="0" />
    <input type="hidden" id="Group" name="Group" />
    <input type="hidden" id="hid_UploadFunc" value="Upload_AcheiveReward"/>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" style="padding-bottom: 0px;">
            <h1 class="title">
                <a onclick="javascript:window.history.go(-1)" style="cursor: pointer;">业绩修改</a><span>&gt;</span><a href="#" class="crumbs" id="GropName"></a>
            </h1>
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
                        <input type="text" isrequired="true" fl="认定日期" name="DefindDate" id="DefindDate" class="text Wdate" onclick="WdatePicker({ dateFmt: 'yyyy年MM月dd日' });" onkeydown="ChangeLid();"/>
                    </div>
                    <div class="input_lable fl input_lable2">
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
                    <div class="input_lable fl none">
                        <label for="">获奖教师：</label>
                        <select class="chosen-select" data-placeholder="获奖教师" id="TeaUNo" name="TeaUNo"></select>
                    </div>
                    <div class="input_lable fl none">
                        <label for="">奖项名称：</label>
                        <input type="text" isrequired="true" fl="奖项名称" class="text" name="Name" id="Name" style="width: 694px" />
                    </div>
                    <div class="input_lable book fl none">
                        <label for="">书名：</label>
                        <select class="chosen-select" data-placeholder="书名" id="BookId" name="BookId" onchange="Get_OperReward_UserInfo();"></select>
                    </div>
                    <div class="input_lable book fl none">
                        <label for="">书号：</label>
                        <input type="text" name="ISBN" id="ISBN" value="" class="text" readonly="readonly"/>
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
                        <select class="select" isrequired="true" fl="奖励等级" name="Rid" id="Rid" onchange="SetScore();BindRank();"></select>
                    </div>
                    <div class="input_lable fl none">
                        <label for="">排名：</label>
                        <select class="select" name="Sort" id="Sort" onchange="SetScore('#Sort');"></select>
                    </div>
                    <div class="input_lable fl">
                        <label for="">获奖年度：</label>
                        <input type="text" isrequired="true" fl="获奖年度" class="text Wdate" name="Year" id="Year" onclick="WdatePicker({dateFmt:'yyyy年'})" />
                    </div>
                    <div class="input_lable fl">
                        <label for="">负责人：</label>
                        <select class="chosen-select" isrequired="true" fl="负责人" data-placeholder="负责人" id="ResponsMan" name="ResponsMan" onchange="Bind_ResponsMan('ResponsMan');"></select>
                    </div>
                    <div class="input_lable fl">
                        <label for="">负责单位：</label>
                        <select class="chosen-select" isrequired="true" fl="负责单位" data-placeholder="负责单位" id="DepartMent" name="DepartMent" multiple="multiple"></select>
                    </div>
                </div>
                <h2 class="cont_title members none"><span>成员信息</span></h2>
                <div class="area_form members none">
                    <div class="clearfix">
                        <input type="button" name="name" id="" value="添加" class="btn fl" onclick="javascript: OpenIFrameWindow('添加成员','AddAchMember.aspx', '1000px', '700px');">
                        <input type="button" name="name" id="" value="删除" class="btn fl ml20" onclick="Del_HtmlMember();">
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
                <h2 class="cont_title book none"><span>作者信息</span></h2>
                <div class="area_form book none">
                    <div class="clearfix">                       
                       <span class="fr status mr10">总分：<span id="span_BookScore">0</span></span>                      
                       <span class="fr status mr10">总贡献字数：<span id="span_Words">0</span></span>                   
                    </div>
                    <table class="allot_table mt10  ">
                        <thead>
                            <tr>

                                <th>姓名</th>
                                <th>作者类型</th>
                                <th>排名</th>
                                <th>单位／部门</th>
                                <th>贡献字数（万字）</th>
                            </tr>
                        </thead>
                        <tbody id="tb_info"></tbody>
                    </table>
                </div>              
            </div>
        </div>
        <div class="score">
            
        </div>
        <div class="btnwrap" style="background: #fafafa; padding: 15px 0px;">
            <input type="button" value="保存" onclick="Save()" class="btn" />
            <input type="button" value="提交" class="btn ml10" onclick="submit()"/>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../Scripts/choosen/prism.js"></script>
    <script src="../Scripts/Webuploader/dist/webuploader.js"></script>
    <link href="../Scripts/Webuploader/css/webuploader.css" rel="stylesheet" />
    <script src="upload_batchfile.js"></script>
    <script src="BaseUse.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script>
        var UrlDate = new GetUrlDate();        
        $(function () {
            $("#CreateUID").val(GetLoginUser().UniqueNo);
            var Type = UrlDate.Type;
            switch (Type) {
                case "2":
                    $("#Sort").parent().show();                    
                    $('.members').show();
                    $("#Name").parent().show();
                    break;
                case "3":
                    $(".book").show();
                    break;
                case "5":
                    $("#TeaUNo").parent().show();
                    break;
                case "1":                   
                    $("#Name").parent().show();
                    break;
            }
            BindFile_Plugin();
            $("#Group").val(UrlDate.Group);
            BindUser("ResponsMan");
            BindUser("TeaUNo");
            //$("#Group").val(UrlDate.Group);
            //$("#GropName").html(decodeURI(UrlDate.Name));
            if (UrlDate.Group != undefined) {
                BindGInfo();//奖励项目
            }
            BindBook();
            var itemId = UrlDate.itemId;
            if (itemId != undefined) {
                $("#Id").val(itemId);
                Get_Sys_Document(0, $("#Id").val());
                if (UrlDate.Type != "3") {
                    Get_TPM_AcheiveMember(itemId);
                }
                GetDataById(itemId);
            } else {
                BindDepart("DepartMent");
            }
        });
        function GetDataById() {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetAcheiveRewardInfoData", "IsPage": "false", Id: UrlDate.itemId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $(json.result.retData[0]).each(function () {
                            $("#GropName").html(this.GName);
                            $("#Name").val(this.Name);
                            $("#Gid").val(this.Gid);
                            $("#BookId").val(this.BookId);                            
                            $("#BookId").trigger("chosen:updated");
                            $("#BookId").chosen();
                            $('#ISBN').val(this.ISBN);
                            $("#Year").val(this.Year);
                            $("#ResponsMan").val(this.ResponsMan);
                            $("#ResponsMan").trigger("chosen:updated");
                            $("#ResponsMan").chosen();

                            BindDepart("DepartMent", this.DepartMent);
                            //$("#DepartMent").val(this.DepartMent);
                            //$("#DepartMent").trigger("chosen:updated");
                            //$("#DepartMent").chosen();

                            $("#FileEdionNo").val(this.FileEdionNo);
                            $("#FileNames").val(this.FileNames);
                            $("#DefindDate").val(DateTimeConvert(this.DefindDate, '年月日'));
                            $("#DefindDepart").val(this.DefindDepart);
                            BindLinfo();
                            $("#Lid").val(this.Lid);
                            BindRewardInfo();
                            $("#Rid").val(this.Rid);
                            BindRank();
                            SetScore();
                            Get_OperReward_UserInfo();
                            if (this.Status == "0" || this.Status == "2") {
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
        function BindBook() {
            $("#BookId").empty();
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                async:false,
                data: { "Func": "GetTPM_BookStory", "IsPage": "false", "Status": "3" },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $("#BookId").append('<option value="">请选择教材</option>');
                        $.each(json.result.retData, function () {
                            $("#BookId").append('<option value="' + this.Id + '" isbn="' + this.ISBN + '" bt="' + this.BookType + '">' + this.Name + (this.BookType == 1 ? '-立项' : '-出版') + '</option>');
                        });
                    }
                    $("#BookId").chosen({
                        allow_single_deselect: true,
                        disable_search_threshold: 1,
                        no_results_text: '未找到',
                        search_contains: true,
                        width: '270px'
                    });
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }        
        function BindRank() {
            $("#Sort").html('<option value="" ss="0">请选择</option>');
            if ($("#Rid").val() != 0) {
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                    type: "post",
                    dataType: "json",
                    data: { "Func": "GetRank", "IsPage": "false", "RId": $("#Rid").val() },
                    success: function (json) {
                        if (json.result.errMsg == "success") {
                            $.each(json.result.retData, function () {
                                $("#Sort").append('<option value="' + this.Id + '" ss="' + this.Score + '">' + this.Name + '</option>');
                            });
                        }
                    },
                    error: function (errMsg) {
                        alert(errMsg);
                        //接口错误时需要执行的
                    }
                });
            }
        }
      
        function submit() {
            $("#Status").val("3");
            Save();
        }
        //提交按钮
        function Save() {
            //验证为空项或其他
            var valid_flag = validateForm($('select,input[type="text"]:visible'));
            if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有 isrequired 值为true或false 和fl 值为不为空的名称两个属性
            {
                return false;
            }
            if ($("#uploader .filelist li").length <= 0) {
                layer.msg("请上传获奖扫描件!");
                return;
            }
            if (UrlDate.Type == "2" && !$("#Sort").val().length) {
                layer.msg("请选择排名!");
                return;
            }
            if (UrlDate.Type == "3" && !$("#BookId").val().trim().length) {
                layer.msg("请输入书名!");
                return;
            }
            if (UrlDate.Type == "5" && !$("#TeaUNo").val().trim().length) {
                layer.msg("请输入获奖教师!");
                return;
            }
            var object = getFromValue();//组合input标签
            object["DepartMent"] = $("#DepartMent").val().join(',');
            object.MemberStr = '';
            object.MemberEdit = '';
            object.AchieveType = UrlDate.Type;
            if (UrlDate.Type == "2") {
                var add_tr = $("#tb_Member tr");
                if (add_tr.length <= 0) {
                    layer.msg("请添加成员信息!");
                    return;
                }
                if (add_tr.length <= 4) {
                    layer.msg("请至少添加五个成员信息!");
                    return;
                }
                if (Num_Fixed($('#span_AllScore').html()) < Num_Fixed($('#span_CurScore').html())) {
                    layer.msg("已分配分数不能大于总分！");
                    return;
                }                
            }
            var addArray = Rtn_AddMemArray(1);
            object.MemberStr = addArray.length > 0 ? JSON.stringify(addArray) : '';
            var editArray = [];
            $("#tb_Member tr").each(function (i, n) {
                if ($(this).hasClass('memedit')) {
                    var sub_e = new Object();
                    sub_e.Id = n.id.replace('tr_mem_', '');
                    sub_e.Score = $(this).find('input[type=number]').val();
                    sub_e.Sort = i + 1;
                    sub_e.EditUID = loginUser.UniqueNo;
                    editArray.push(sub_e);
                }
            });
            object.MemberEdit = editArray.length > 0 ? JSON.stringify(editArray) : '';
            var add_path = Get_AddFile();
            object.Add_Path = add_path.length > 0 ? JSON.stringify(add_path) : "";
            object.Edit_PathId = Get_EditFileId();
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: object,
                success: function (json) {
                    if (json.result.errNum ==0) {
                        layer.msg('操作成功!');                        
                        Del_Document();
                        //window.location.go(-1);
                        window.history.go(-1);
                       // window.location.href = "AcheveRewadSearch.aspx";
                    } else if (json.result.errNum == -1) {

                    }
                },
                error: function (errMsg) {
                    //接口错误时需要执行的
                    alert(errMsg);
                }
            });
        }

        //奖励项目
        function BindGInfo() {
            $("#Gid").html('<option value="">请选择</option>');
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                async:false,
                data: { "Func": "GetAcheiveLevelData", "IsPage": "false", "Pid": UrlDate.Group },
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
            $("#Sort").html('<option value="" ss="0">请选择</option>');
            if (!$("#DefindDate").val().trim().length) {
                layer.msg("请先指定认定日期");
                return;
            }
            if ($("#Gid").val() != 0) {
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
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
            $("#Sort").html('<option value="" ss="0">请选择</option>');
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
                                $("#Rid").append('<option value="' + this.Id + '" ss="' + this.Score + '" st="' + this.ScoreType + '">' + this.Name + '</option>');
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
