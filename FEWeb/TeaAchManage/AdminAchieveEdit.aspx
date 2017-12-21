<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAchieveEdit.aspx.cs" Inherits="FEWeb.TeaAchManage.AdminAchieveEdit" %>
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
    <script type="text/x-jquery-tmpl" id="tr_MemEdit">
        {{each(i, mem) Member_Data.retData}}  
        <tr id="tr_mem_${mem.Id}" class="memedit" un="${mem.UserNo}">              
            <td>{{if $("#AchieveType").val()=="2"}}
                  {{if i>4}}<input type="checkbox" name="ck_trsub" value="${mem.UserNo}" onclick="CheckSub(this);"/>{{/if}}
                {{else}}
                  {{if i!=0}}<input type="checkbox" name="ck_trsub" value="${mem.UserNo}" onclick="CheckSub(this);"/>{{/if}}
                {{/if}}
            </td>
            <td class="td_memname">${mem.Name}</td>
            <td>{{if $("#AchieveType").val()=="3"}}${mem.UnitScore* mem.WordNum}
                {{else}}<input type="number" name="score" value="${mem.Score}" oldsc="${mem.Score}" min="0" step="0.01"/>
            {{/if}}
            </td>
            <td>${mem.CreateName}</td>
            <td>${DateTimeConvert(mem.CreateTime,"yyyy-MM-dd")}</td>
        </tr>
        {{/each}}  
    </script>
     <%--成员信息(新添加的)--%>
    <script type="text/x-jquery-tmpl" id="itemData">
        <tr class="memadd" un="${UniqueNo}">
            <td><input type='checkbox' value="${UniqueNo}" name="ck_trsub" onclick="CheckSub(this);" /></td>
            <td class="td_memname">${Name}</td>
            <td><input type="number" name="score" value="" min="0" step="0.01"></td>          
            <td>${loginUser.Name}</td>
            <td>${DateTimeConvert(new Date(),"yyyy-MM-dd",false)}</td>          
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
                        {{if UrlDate.AchieveType==3}}
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
                                {{if UrlDate.AchieveType==3}}
                                <td>{{if mem.ULevel==0}}独著 {{else mem.ULevel==1}}主编{{else mem.ULevel==2}}参编{{else}}其他人员{{/if}}</td>
                                <td>${mem.Sort}</td>
                                <td>${mem.Major_Name}</td>
                                <td>${mem.WordNum}</td>
                                <td class="td_money">{{if AuditStatus==0||AuditStatus==2}}<input type="number" isrequired="true" oldre="0" fl="奖金" min="0" step="0.01">{{/if}}</td>
                                {{else}}
                                <td class="td_money">{{if AuditStatus==0||AuditStatus==2}}<input type="number" isrequired="true" oldre="0" fl="奖金" min="0" step="0.01">{{/if}}</td>
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
            </div>
        </div>
    </script>   
</head>
<body style="background: #fff;">
    <input type="hidden" name="AwardSwich" id="AchieveType" value="" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" id="hid_UploadFunc" value="Upload_AcheiveReward"/>
    <div class="main"  id="RewardAllot">
        <h2 class="cont_title"><span>基本信息</span></h2>
        <div class="area_form clearfix" style="min-height:165px;">
            <div class="input_lable fl" v-if="Info.AchieveType==5" v-cloak >
                <label for="">获奖教师：</label>
                <span id="TeaUNo">{{Info.ResponsName}}</span>
            </div>
            <div class="input_lable fl" v-if="Info.AchieveType==1||Info.AchieveType==2" v-cloak >
                <label for="">奖项名称：</label>
                <span id="Name" >{{Info.AchiveName}}</span>
            </div>
            <div class="input_lable book fl" v-if="Info.AchieveType==3"v-cloak  >
                <label for="">书名：</label>
                <span id="BookName" >{{Info.BookName}}</span> 
                <input type="hidden" id="BookId" name="BookId" v-model="Info.BookId"/>                 
            </div>
            <div class="input_lable book fl" v-if="Info.AchieveType==3"  v-cloak >
                <label for="">书号：</label>
                <span id="ISBN">{{Info.ISBN}}</span>
            </div>
            <div class="input_lable fl"  v-cloak >
                <label for="">奖励项目：</label>
                <span id="Gid">{{Info.GidName}}</span>
            </div>
            <div class="input_lable fl" v-cloak >
                <label for="">获奖级别：</label>
                <span id="Lid" >{{Info.LevelName}}</span>
            </div>
            <div class="input_lable fl" v-cloak >
                <label for="">奖励等级：</label>
                <span id="Rid" >{{Info.RewadName}}</span>
            </div>
            <div class="input_lable fl" v-if="Info.AchieveType==2" v-cloak >
                <label for="">排名：</label>
                <span id="Sort">{{Info.rankName}}</span>
            </div>
            <div class="input_lable fl" v-cloak >
                <label for="">获奖年度：</label>
                <span id="Year" >{{Info.Year}}</span>
            </div>
            <div class="input_lable fl" v-cloak >
                <label for="">负责人：</label>
                <span id="ResponsMan" >{{Info.ResponsMan}}</span>
            </div>
            <div class="input_lable fl" v-cloak >
                <label for="">负责单位：</label>
                <span id="DepartMent" >{{Info.Major_Name}}</span>
            </div>
        </div>
        <h2 class="cont_title"><span>分数分配</span></h2>
        <div class="area_form clearfix">
            <div class="clearfix">                   
                <input type="button" name="memberbtn" value="添加" class="btn ml" style="display:none;" id="AddBtn" onclick="javascript: OpenIFrameWindow('添加成员','AddAchMember.aspx', '900px', '650px');"/>
                <input type="button" name="memberbtn" value="删除" class="btn ml10" style="display:none;" onclick="Del_HtmlMember(1);"/>
                <span class="fr status">总分：<span id="span_AllScore"></span><span id="Unit"></span>，<span id="span_CurScore" style="margin-right:15px;"></span></span>
            </div>
            <table class="allot_table mt10  ">
                <thead>
                    <tr>
                        <th width="16px"><input type="checkbox" name="ck_tball" onclick="CheckAll(this)"/></th>
                        <th>成员</th>
                        <th>分数</th>                          
                        <th>录入人</th>
                        <th>录入日期</th>                                                  
                    </tr>
                </thead>
                <tbody id="tb_Member"></tbody>
            </table>
            <div class="clearfix mt10 Enclosure">
                <div class="status-left">
                    <label for="" class="fl">附件：</label>
                    <div class="fl">
                        <ul id="ul_ScoreFile" class="clearfix file-ary"></ul>
                    </div>
                </div>
            </div>
            <div class="EditResult">
                <textarea id="txt_Reasonscore" class="textarea" placeholder="请输入修改原因" isrequired="true" fl="请输入修改原因"></textarea>
                 <div class="input_lable input_lable2">
                    <label for="" style="min-width:46px;">附件：</label>
                    <div class="fl uploader_container" style="padding-left:55px;">
                        <div id="uploader_score">
                            <div class="queueList">
                                <div id="dndArea_score" class="placeholder photo_lists">
                                    <div id="filePicker_score"></div>
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
                 <div class="reward_btn">
                    <input type="button" value="提交" class="btn" onclick="Save_Score(7);"/>
                </div>
           </div>
        </div>
        <h2 class="cont_title none RewardReason"><span>奖金分配</span></h2>
        <div id="div_MoneyInfo" class="area_form clearfix"></div>
        <div class="EditResult none RewardReason">
            <textarea id="txt_Reasonreward" class="textarea" placeholder="请输入修改原因" isrequired="true" fl="请输入修改原因"></textarea>
            <div class="input_lable input_lable2">
                <label for="" style="min-width:46px;">附件：</label>
                <div class="fl uploader_container" style="padding-left:55px;">
                    <div id="uploader_reward">
                        <div class="queueList">
                            <div id="dndArea_reward" class="placeholder photo_lists">
                                <div id="filePicker_reward"></div>
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
            <div class="reward_btn">
                <input type="button" value="提交" class="btn" onclick="Save_Reward(3);"/>
            </div>
        </div>
         <h2 class="cont_title re_reward none RewardReason"><span>分配历史</span></h2>
        <div class="area_form re_reward none RewardReason">
            <ul class="history">
                <li class="clearfix">
                    <span class="fl">陈成一给李树一分配了110分<a href="javascript:;" onclick="OpenDetail('查看详情22','查看详情.jpg')">查看详情</a></span>
                    <span class="fr">2016-08-09</span>
                </li>
            </ul>
        </div>
    </div>
    <script src="../js/vue.min.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script src="../scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>    
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../Scripts/choosen/prism.js"></script>
    <script src="../Scripts/Webuploader/dist/webuploader.js"></script>
    <link href="../Scripts/Webuploader/css/webuploader.css" rel="stylesheet" />
    <script src="upload_batchfile.js"></script>
    <script src="BaseUse.js"></script>
     <script type="text/javascript">
         var UrlDate = new GetUrlDate();
         var cur_AchieveId = UrlDate.AcheiveId;
         $(function () {
             $("#CreateUID").val(GetLoginUser().UniqueNo);
             $("#AchieveType").val(UrlDate.AchieveType);
             if (UrlDate.AchieveType == "3")//教材建设类
             {
                 $("[name='memberbtn']").hide();
                 $("#Unit").html("分/万字");
             }
             else {
                 $("[name='memberbtn']").show();
                 $("#Unit").html("分");
             }
             BindFile_Plugin("#uploader_score", "#filePicker_score", "#dndArea_score");
             BindFile_Plugin("#uploader_reward", "#filePicker_reward", "#dndArea_reward");             
             Get_LookPage_Document(3, cur_AchieveId, $("#ul_ScoreFile"));            
         });
         //基本信息
         var RewardAllot = new Vue({
             el: '#RewardAllot',
             data: {
                 Info: {},
             },
             methods: {
                 GetDateById: function () {
                     var that = this;
                     $.ajax({
                         url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                         type: "post",
                         dataType: "json",
                         data: { "Func": "GetAcheiveRewardInfoData", "IsPage": "false", Id: cur_AchieveId },
                         success: function (json) {
                             if (json.result.errMsg == "success") {
                                 that.Info = json.result.retData[0];
                                 $('#span_AllScore').html(that.Info.TotalScore);
                                 Get_RewardUserInfo();
                             }
                         },
                         error: function () {
                             //接口错误时需要执行的
                         }
                     })
                 },
             },
             mounted: function () {
                 var that = this;
                 that.GetDateById();
             }
         });
         //绑定成员信息
         var Member_Data = [];
         function Get_RewardUserInfo() {
             $.ajax({
                 type: "Post",
                 url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                 data: { func: "GetTPM_UserInfo", RIId: cur_AchieveId, IsPage: false },
                 dataType: "json",
                 success: function (json) {
                     if (json.result.errNum.toString() == "0") {
                         Member_Data = json.result;
                         $("#tr_MemEdit").tmpl(json.result).appendTo("#tb_Member");
                         var $span_CurScore = $('#span_CurScore');
                         var curscore = 0;
                         $(json.result.retData).each(function (i, n) {
                             if (n.Score) {
                                 curscore = numAdd(curscore,n.Score);
                             }
                         });
                         $span_CurScore.html(curscore);
                     }
                     Get_RewardBatchData(">0", $(".RewardReason"));
                 },
                 error: function (errMsg) {
                     layer.msg(errMsg);
                 }
             });
         }
         //分数分配
         function CheckScoreAndAward() {
             var AllScore = $("#span_AllScore").html();
             var ShareScore = 0;
             $("#tb_Member").find("tr").each(function () {
                 var Score = $(this).find("td").eq(2).find("input").val();
                 ShareScore= numAdd(ShareScore,Score);
             });
             if (isNaN(ShareScore) == true && isNaN(ShareAward) == true) {
                 layer.msg("没有数据修改");
                 return false;
             }
                 //else if (AllScore>=ShareScore&&AllAward>ShareAward) {
                 //    return true;
                 //}
                 //else if (AllScore>=ShareScore&&AllAward==NaN) {
                 //    return true;
                 //}
             else if (AllScore == NaN && AllAward > ShareAward) {
                 return true;
             }
             else if (AllScore < ShareScore) {
                 layer.msg("分配的分数大于总分数");
                 return false;
             }
                 //else if (AllAward<ShareAward)
                 //{
                 //    layer.msg("分配的金额大于总金额")
                 //    return false
                 //}
             else { return true; }
         }
         function Save_Score(status) {
             if (CheckScoreAndAward()) {
                 var object = { Func: "Add_AcheiveAllot", Id: cur_AchieveId };
                 object.Status = status;
                 var addArray = Get_AddMember().addarray;
                 object.MemberStr = addArray.length > 0 ? JSON.stringify(addArray) : '';
                 var recordArray = Get_AddMember().edithis;
                 var editArray = Get_EditMember();
                 object.MemberEdit = editArray.length > 0 ? JSON.stringify(editArray) : '';
                 return;
                 layer.confirm('确认提交吗？', {
                     btn: ['确定', '取消'], //按钮
                     title: '操作'
                 }, function (object) {
                     LastSave(status);
                 }, function () { });
             }
         }
         function Save_Reward(ststus) {

         }
         function LastSave(object) {             
             var add_path = Get_AddFile(5);
             object.Add_Path = add_path.length > 0 ? JSON.stringify(add_path) : "";
             object.Edit_PathId = Get_EditFileId();
             $.ajax({
                 url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                 type: "post",
                 dataType: "json",
                 data: object,
                 success: function (json) {
                     if (json.result.errNum == 0) {
                         parent.layer.msg('操作成功!');
                         Del_Document();
                         parent.BindData(1, 10);
                         parent.CloseIFrameWindow();
                     } else if (json.result.errNum == -1) {

                     }
                 },
                 error: function (errMsg) {
                     //接口错误时需要执行的
                     alert(errMsg);
                 }
             });
         }
         function Get_AddMember() {
             var addArray = [],edithis=[];
             var add_tr = $("#tb_Member tr");
             $(add_tr).each(function (i, n) {
                 if ($(this).hasClass('memadd')) {
                     var userno = $(this).attr('un'), score = $(this).find('input[type=number][name=score]').val();
                     addArray.push({ UserNo: userno, Score: score, Sort: i + 1, CreateUID: loginUser.UniqueNo });
                     edithis.push({
                         Type: 0, Acheive_Id: cur_AchieveId, Content: loginUser.Name + '添加新成员' + $(this).find('td.td_memname').html() + ",并分配" + score+"分"
                         , EditReason: $("#txt_Reasonscore").val(), ModifyUID: userno, CreateUID: loginUser.UniqueNo
                     });
                 }
             });
             return { addarray: addArray, edithis: edithis };
         }
         function Get_EditMember() {
             var editArray = [],edithis=[];
             $("#tb_Member tr").each(function (i, n) {
                 if ($(this).hasClass('memedit')) {
                     var id = n.id.replace('tr_mem_', ''), userno = $(this).attr('un')
                       ,score = $(this).find('input[type=number][name=score]').val()
                       ,oldscore=$(this).find('input[type=number][name=score]').attr('oldsc');
                     if ($(this).is(":visible")) {
                         editArray.push({ Id: id, Score: score, Sort: i + 1, EditUID: loginUser.UniqueNo });
                         if (parseFloat(score) != parseFloat(oldscore)) {
                             edithis.push({
                                 Type: 0, Acheive_Id: cur_AchieveId, Content: loginUser.Name + '将' + $(this).find('td.td_memname').html() + oldscore+"分"+"改为"+score
                                                                              , EditReason: $("#txt_Reasonscore").val(), ModifyUID: userno, CreateUID: loginUser.UniqueNo
                             });
                         }
                     } else {
                         edithis.push({
                             Type: 0, Acheive_Id: cur_AchieveId, Content: loginUser.Name + '删除成员' + $(this).find('td.td_memname').html()
                                                 , EditReason: $("#txt_Reasonscore").val(), ModifyUID: userno, CreateUID: loginUser.UniqueNo
                         });
                     }                    
                 }
             });
             return editArray;
         }         
         //分配历史
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
