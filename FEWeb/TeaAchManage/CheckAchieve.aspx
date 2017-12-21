<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAchieve.aspx.cs" Inherits="FEWeb.TeaAchManage.CheckAchieve" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>业绩查看审核</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .area_form {
            padding: 0px 0px 20px 0px;
        } 
        .file-ary .title1{
            float:left;line-height:30px;
        } 
        .file-ary .file-panel{
            float:left;margin-left:10px;cursor:pointer;
        }      
    </style>
    <%--成员信息--%>
    <script type="text/x-jquery-tmpl" id="tr_MemEdit">
        <tr class="memedit" un="${UserNo}">
            <td>${Name}</td>
            <td>${Score}</td>
            <td>${Major_Name}</td>
        </tr>
    </script>
    <%--成员信息--%>
    <script type="text/x-jquery-tmpl" id="tr_MemEdit1">
        <tr id="tr_mem_${Id}" class="memedit" un="${UserNo}">
            <td>${Name}</td>
            <td>${Score}</td>
            <td>${Major_Name}</td>
            <td>${DateTimeConvert(CreateTime,"yyyy-MM-dd")}</td>
        </tr>
    </script>
    <script type="text/x-jquery-tmpl" id="tr_Info">
        <tr>
            <td>${Name}</td>
            <td>{{if ULevel==0}}独著 {{else ULevel==1}}主编{{else ULevel==2}}参编{{else}}其他人员{{/if}}</td>            
            <td>${Sort}</td>
            <td>${Major_Name}</td>
            <td>${WordNum}</td>           
        </tr>
    </script>
    <script type="text/x-jquery-tmpl" id="tr_Info1">
        <tr>
            <td>${Name}</td>
            <td>{{if ULevel==0}}独著 {{else ULevel==1}}主编{{else ULevel==2}}参编{{else}}其他人员{{/if}}</td>            
            <td>${Sort}</td>
            <td>${Major_Name}</td>
            <td>${WordNum}</td>
            <td>${UnitScore*WordNum}</td>
        </tr>
    </script>
    <script type="text/x-jquery-tmpl" id="div_item">
       <div class="clearfix allot_item">
            <div class="clearfix">
                <div class="fl status-left">
                     <label for="">状态：</label>
                    {{if AuditStatus==0}}<span class="nosubmit">待提交</span>
                    {{else AuditStatus==1}}<span class="checking1">待审核</span>
                    {{else AuditStatus==2}}<span class="nocheck">审核不通过</span>
                    {{else}} <span class="assigning">审核通过</span>{{/if}}
                </div>
                <div class="fr status">奖金${Money}，已分配<span>${HasAllot}</span></div>
            </div>
            <table class="allot_table mt10  ">
                <thead>
                    <tr>
                        {{if cur_AchieveType==3}}
                        <th>姓名</th>
                        <th>作者类型</th>
                        <th>排名</th>
                        <th>单位／部门</th>
                        <th>贡献字数（万字）</th>
                        <th>奖金</th>
                        {{else}}
                        <th>成员</th>
                        <th>奖金</th>
                        <th>部门</th>
                        <th>录入日期</th>
                        {{/if}}
                    </tr>
                </thead>
                <tbody id="tb_Member_${rowNum}" autid="${AuditId}" rewid="${Id}">
                    {{each(i, mem) Member_Data.retData}}                        
                            <tr un="${mem.UserNo}" uid="${mem.Id}">
                                <td>${mem.Name}</td>
                                {{if cur_AchieveType==3}}
                                <td>{{if mem.ULevel==0}}独著 {{else mem.ULevel==1}}主编{{else mem.ULevel==2}}参编{{else}}其他人员{{/if}}</td>
                                <td>${mem.Sort}</td>
                                <td>${mem.Major_Name}</td>
                                <td>${mem.WordNum}</td>
                                <td class="td_money">0.00</td>
                                {{else}}
                                <td class="td_money">0.00</td>
                                <td>${mem.Major_Name}</td>
                                <td>${DateTimeConvert(mem.CreateTime,"yyyy-MM-dd")}</td>
                                {{/if}}     
                            </tr>
                    {{/each}}    
                </tbody>
            </table>
            <div class="clearfix mt10 Enclosure">
                <div class="status-left">
                    <label for="" class="fl">附件：</label>
                    <div class="fl">
                        <ul id="ul_ScoreFile_${rowNum}" auid="${AuditId}" class="clearfix file-ary allot_file"></ul>
                    </div>
                </div>
                {{if UrlDate.Type =='Check'&&AuditStatus==1}}
                <div class="reward_btn">
                    <input type="button" value="通过" onclick="AllotAudit(3,${AuditId});" class="btn" />
                    <input type="button" value="不通过" onclick="AllotAudit(2,${AuditId});" class="btnb" />
                </div>
                {{/if}} 
            </div>
        </div>
    </script>
</head>
<body style="background: #fff;">
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <div class="checkmes none"></div>
    <div class="main" >
        <div class="cont">
            <h2 class="cont_title re_view none"><span>获奖文件信息</span></h2>
            <div class="area_form clearfix re_view none">
                <div class="input_lable fl">
                    <label for="">发文号：</label>
                    <span id="FileEdionNo"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">文件名称：</label>
                    <span id="FileNames"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">认定机构：</label>
                    <span id="DefindDepart"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">认定日期：</label>
                    <span id="DefindDate"></span>
                </div>
                <div class="input_lable fl input_lable2">
                    <label for="">获奖扫描件：</label>
                    <div class="fl uploader_container">
                        <div id="uploader">
                            <div class="queueList">
                                <div id="dndArea" class="placeholder photo_lists">                                   
                                    <ul class="filelist clearfix"></ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h2 class="cont_title"><span>基本信息</span></h2>
            <div class="area_form clearfix">
                <div class="input_lable fl none">
                    <label for="">获奖教师：</label>
                    <span id="TeaUNo"></span>
                </div>
                <div class="input_lable fl none">
                    <label for="">奖项名称：</label>
                    <span id="Name"></span>
                </div>
                <div class="input_lable book fl none">
                    <label for="">书名：</label>
                    <span id="BookName"></span> 
                    <input type="hidden" id="BookId" name="BookId" value=""/>                 
                </div>
                <div class="input_lable book fl none">
                    <label for="">书号：</label>
                    <span id="ISBN"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">奖励项目：</label>
                    <span id="Gid"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">获奖级别：</label>
                    <span id="Lid"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">奖励等级：</label>
                    <span id="Rid"></span>
                </div>
                <div class="input_lable fl none">
                    <label for="">排名：</label>
                    <span id="Sort"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">获奖年度：</label>
                    <span id="Year"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">负责人：</label>
                    <span id="ResponsMan"></span>
                </div>
                <div class="input_lable fl">
                    <label for="">负责单位：</label>
                    <span id="DepartMent"></span>
                </div>
            </div> 
            <h2 class="cont_title members none"><span>成员信息</span></h2>
            <div class="area_form members none">
                <div class="clearfix" id="div_user_mem">                        
                    <span class="fr status mr10">已分配：<span id="span_CurScore">0</span></span>
                    <span class="fr status mr10">总分：<span id="span_AllScore">0</span></span>
                </div>
                <table class="allot_table mt10">
                    <thead>
                        <tr>                               
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
                <div class="clearfix" id="div_user_book"> 
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
            <h2 class="cont_title re_score none"><span>分数分配</span></h2> 
            <div class="area_form re_score none">
                <div class="clearfix">
                    <div class="fl status-left">
                         <label for="">状态：</label>
                         <span class="pass">审核通过</span>
                    </div>
                    <div id="div_score_statis" class="fr status"></div>
                </div>
                <table class="allot_table mt10">
                    <thead>
                        <tr class="user_mem none">
                            <th>成员</th>
                            <th>分数</th>
                            <th>部门</th>
                            <th>录入日期</th>                         
                        </tr>
                        <tr class="user_book none">
                            <th>姓名</th>
                            <th>作者类型</th>
                            <th>排名</th>
                            <th>单位／部门</th>
                            <th>贡献字数（万字）</th>
                            <th>分数</th>
                        </tr>
                    </thead>
                    <tbody id="tb_Member1"></tbody>
                </table>               
                <div class="clearfix mt10 Enclosure">
                    <div class="status-left">
                        <label for="" class="fl">附件：</label>
                        <div class="fl">
                            <ul id="ul_ScoreFile" class="clearfix file-ary"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <h2 class="cont_title re_reward none"><span>奖金分配</span></h2> 
            <div class="area_form re_reward none" id="div_MoneyInfo"></div>        
            <h2 class="cont_title re_reward none"><span>分配历史</span></h2>
            <div class="area_form re_reward none">
                <ul class="history">
                    <li class="clearfix">
                        <span class="fl">陈成一给李树一分配了110分<a href="javascript:;" onclick="OpenDetail('查看详情22','查看详情.jpg')">查看详情</a></span>
                        <span class="fr">2016-08-09</span>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="score none"></div>
    <div class="btnwrap2 none">
        <input type="button" id="btn_Pass" value="通过" class="btn" onclick="Check(3)" />
        <input type="button" id="btn_Nopass" value="不通过" class="btnb ml10" onclick="Check(2)" />
    </div>
    <script src="../Scripts/Common.js"></script>
    <script src="../scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>    
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <link href="../Scripts/Webuploader/css/webuploader.css" rel="stylesheet" />
    <script src="BaseUse.js"></script>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var cur_AchieveId = UrlDate.Id,cur_AchieveType=1;
        $(function () {
            $("#CreateUID").val(GetLoginUser().UniqueNo);            
            var Id = UrlDate.Id;
            if (Id != undefined) {
                GetBookDetailById();                
                Get_LookPage_Document(0, Id);
            }
        });
        function GetBookDetailById() {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetAcheiveRewardInfoData", "IsPage": "false", Id: cur_AchieveId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $(json.result.retData).each(function () {
                            InitControl(this);
                            $("#TeaUNo").html(this.ResponsName);
                            $("#Name").html(this.AchiveName);
                            $("#Gid").html(this.GidName);
                            $("#Lid").html(this.LevelName);
                            $("#Rid").html(this.RewadName);
                            $("#Year").html(this.Year);
                            $("#ResponsMan").html(this.ResponsName);
                            $("#DepartMent").html(this.Major_Name);
                            $("#Sort").html(this.rankName);
                            $("#FileEdionNo").html(this.FileEdionNo);
                            $("#FileNames").html(this.FileNames);
                            $("#DefindDate").html(DateTimeConvert(this.DefindDate, '年月日'));
                            $("#DefindDepart").html(this.DefindDepart);
                            $('#span_AllScore').html(this.TotalScore);
                            Get_AchieveStatus(this.Status,".checkmes");                            
                        });
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function View_CheckInit(model) { //查看、审核初始化
            var yesstatus = 3, nostatus = 2;
            if (model.Status >= 0 && model.Status <= 3) {//信息
                yesstatus = 3, nostatus = 2;
                if (model.AchieveType == 3) { yesstatus =7}//教材建设类直接跳过分数审核
            } else if (model.Status > 3 && model.Status <= 6) {//分数
                yesstatus = 7, nostatus = 6;
                $(".re_score").show();
                Get_LookPage_Document(3, model.Id, $("#ul_ScoreFile"));
            } else {  //奖金
                yesstatus = 12, nostatus = 11;
                $(".re_score").show();
                $(".re_reward").show();
                Get_LookPage_Document(3, model.Id, $("#ul_ScoreFile"));
            }
            if (UrlDate.Type == "Check") {
                $("#btn_Pass").click(function () { Check(yesstatus); });
                $("#btn_Nopass").click(function () { Check(nostatus); });
                $(".checkmes").hide();
                if (model.Status <= 6) {
                    $(".btnwrap2").show();
                }               
                $(".re_view").hide();
            }
            else {
                $(".checkmes").show();
                $(".btnwrap2").hide();
                $(".re_view").show();
            }
        }
        function InitControl(model) {           
            View_CheckInit(model);
            SetScore_LooK(model);
            cur_AchieveType=model.AchieveType;
            switch (model.AchieveType) {
                case "2":
                    $("#Sort").parent().show();
                    $("#Name").parent().show();
                    if (model.Status <= 3) {
                        $('.members').show();                        
                    }                             
                    break;
                case "3":                    
                    $('#BookName').html(model.BookName);
                    $('#BookId').val(model.BookId);
                    $('#ISBN').html(model.ISBN);
                    if (model.Status <= 3) {
                        $(".book").show();                      
                    }
                    break;
                case "5":
                    $("#TeaUNo").parent().show();                   
                    break;
                case "1":                   
                    $("#Name").parent().show();                  
                    break;
            }
            Get_RewardUserInfo(model);
        }
        //绑定成员信息
        var Member_Data = [];
        function Get_RewardUserInfo(model) {
            $("#tb_Member,#tb_Info,#tb_Member1").html("");
            if (model.AchieveType == 3) {
                $(".user_book").show();
            } else {
                $(".user_mem").show();
            }
            var postData = { func: "GetTPM_UserInfo", RIId: model.Id, IsPage: false };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                data: postData,
                dataType: "json",
                success: function (json) {
                    if (json.result.errNum.toString() == "0") { 
                        Member_Data = json.result;
                        Get_RewardBatchData();
                        if (model.AchieveType == 3) { //教材建设类                          
                            $("#tr_Info").tmpl(json.result.retData).appendTo("#tb_info");                          
                            $("#tr_Info1").tmpl(json.result.retData).appendTo("#tb_Member1");
                            var $span_Words = $('#span_Words');
                            var curwords = 0;
                            $(json.result.retData).each(function (i, n) {
                                if (n.WordNum) {
                                    curwords= numAdd(curwords,n.WordNum);
                                }
                            });
                            $span_Words.html(curwords);
                            Set_BookScore();
                            $("#div_score_statis").html($("#div_user_book").html());
                        } else {  //其他类型
                            $("#tr_MemEdit").tmpl(json.result.retData).appendTo("#tb_Member");
                            $("#tr_MemEdit1").tmpl(json.result.retData).appendTo("#tb_Member1");
                            var $span_CurScore = $('#span_CurScore');
                            var curscore = 0;
                            $(json.result.retData).each(function (i, n) {
                                if (n.Score) {
                                    curscore = numAdd(curscore,n.Score);
                                }                                
                            });
                            $span_CurScore.html(curscore);
                            $("#div_score_statis").html($("#div_user_mem").html());
                        }                        
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //为业绩赋总分
        function SetScore_LooK(model) {
            $(".score").html("分数：" + model.TotalScore + "分" + (model.AchieveType == "2" ? "/万字" : ""));
            if (model.AchieveType == "3" && model.ScoreType == "2") {
                Set_BookScore();
            }
            $span_AllScore = $('#span_AllScore');
            if ($span_AllScore) {
                $span_AllScore.html(model.TotalScore);
            }
        }
        function Check(Status) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "CheckAcheiveRewardInfoData", "Status": Status, Id: cur_AchieveId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        parent.layer.msg('操作成功!');
                        parent.BindAchieve(1,10);
                        parent.CloseIFrameWindow();
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //奖金分配审核
        function AllotAudit(status,id){
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "Check_AuditReward", Status: status,Id: id,AchieveId: cur_AchieveId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        layer.msg('操作成功!');
                        Get_RewardBatchData();                       
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function OpenDetail(editResult, file) {
            layer.open({
                type: 1,
                title: '修改原因',
                area: ['420px', '240px'], //宽高
                content: '<div style="padding:10px 20px;"><div class="editResult">' + editResult + '</div><div class="file"><label for="">附件：</label><span>' + file + '</span><a href="' + file + '" target="_blank">(查看)</a></div></div>'
            });
        }
    </script>
</body>
</html>
