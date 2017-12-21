<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermanAllot.aspx.cs" Inherits="FEWeb.TeaAchManage.PermanAllot" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>业绩分配</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link href="../css/layout.css" rel="stylesheet" />
    <link href="../Scripts/choosen/chosen.css" rel="stylesheet" />
    <link href="../Scripts/choosen/prism.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .area_form {
            padding: 0px 0px 20px 0px;
        }
    </style>
    <script type="text/x-jquery-tmpl" id="tr_MemEdit">
        {{each(i, mem) AcheiveMember_data.retData}}  
        <tr id="tr_mem_${mem.Id}" class="memedit" un="${mem.UserNo}">              
            <td>{{if $("#AchieveType").val()=="2"}}
                  {{if i>4}}<input type="checkbox" name="ck_trsub" value="${mem.UserNo}" onclick="CheckSub(this);"/>{{/if}}
                {{else}}
                  {{if i!=0}}<input type="checkbox" name="ck_trsub" value="${mem.UserNo}" onclick="CheckSub(this);"/>{{/if}}
                {{/if}}
            </td>
            <td>${mem.Name}</td>
            <td>{{if $("#AchieveType").val()=="3"}}${mem.UnitScore* mem.WordNum}
                {{else}}<input type="number" name="score" value="${mem.Score}" min="0" step="0.01"/>
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
            <td>${Name}</td>
            <td><input type="number" name="score" value="" min="0" step="0.01"></td>          
            <td>${loginUser.Name}</td>
            <td>${DateTimeConvert(new Date(),"yyyy-MM-dd",false)}</td>          
        </tr>
    </script>
</head>
<body style="background: #fff;">
    <input type="hidden" name="AwardSwich" id="AchieveType" value="" />
    <input type="hidden" name="AwardSwich" id="AwardSwich" value="" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" id="hid_UploadFunc" value="Upload_AcheiveReward"/>
    <div class="main" >
        <div class="cont">
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
            <h2 class="cont_title"><span>分数分配</span></h2>
            <div class="area_form clearfix">
                <div class="clearfix">                   
                    <input type="button" name="memberbtn" value="添加" class="btn ml" style="display:none;" id="AddBtn" onclick="javascript: OpenIFrameWindow('添加成员','AddAchMember.aspx', '900px', '650px');"/>
                    <input type="button" name="memberbtn" value="删除" class="btn ml10" style="display:none;" onclick="Del_HtmlMember();"/>
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
                <div class="input_lable fl input_lable2">
                        <label for="" style="min-width:46px;">附件：</label>
                        <div class="fl uploader_container" style="padding-left:55px;">
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
        </div>
    </div>
    <div class="btnwrap2">
        <input type="button" value="保存" onclick="submit(4)" class="btn" />
        <input type="button" value="提交" class="btn ml10" onclick="submit(5)"/>
    </div>
    <script src="../Scripts/Common.js"></script>
    <script src="../scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
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
            BindFile_Plugin();
            GetDateById();
            Get_TPM_AcheiveMember(cur_AchieveId);
            Get_Sys_Document(3, cur_AchieveId);
        });
        function GetDateById() {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                async:false,
                data: { "Func": "GetAcheiveRewardInfoData", "IsPage": "false", Id: cur_AchieveId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $.each(json.result.retData, function () {
                            InitControl(this);
                            $("#Name").html(this.AchiveName);
                            $("#Gid").html(this.GidName);
                            $("#Lid").html(this.LevelName);
                            $("#Rid").html(this.RewadName);
                            $("#Year").html(this.Year);
                            $("#TeaUNo").html(this.ResponsName);
                            $("#ResponsMan").html(this.ResponsName);
                            $("#DepartMent").html(this.Major_Name);
                            $("#Sort").html(this.rankName);                           
                            $('#span_AllScore').html(this.TotalScore);                            
                        });
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function InitControl(model) {
            switch (model.AchieveType) {
                case "2":
                    $("#Sort").parent().show();                  
                    $('.members').show();
                    $("#Name").parent().show();
                    break;
                case "3":
                    $(".book").show();
                    $('#BookName').html(model.BookName);
                    $('#BookId').val(model.BookId);
                    $('#ISBN').html(model.ISBN);                  
                    break;
                case "5":
                    $("#TeaUNo").parent().show();
                    break;
                case "1":                  
                    $("#Name").parent().show();
                    break;
            }
        }       
        function CheckScoreAndAward()
        {          
            var AllScore = $("#span_AllScore").html();
            var ShareScore=0;          
            $("#tb_Member").find("tr").each(function () {
                var Score=  $(this).find("td").eq(2).find("input").val();             
                ShareScore= numAdd(ShareScore,Score);
            });
            if (isNaN(ShareScore)==true&& isNaN(ShareAward)==true) {
                layer.msg("没有数据修改");
                return false;
            }
            //else if (AllScore>=ShareScore&&AllAward>ShareAward) {
            //    return true;
            //}
            //else if (AllScore>=ShareScore&&AllAward==NaN) {
            //    return true;
            //}
            else if (AllScore==NaN&&AllAward>ShareAward) {
                return true;
            }
            else if (AllScore<ShareScore) {
                layer.msg("分配的分数大于总分数");
                return false;
            }
            //else if (AllAward<ShareAward)
            //{
            //    layer.msg("分配的金额大于总金额")
            //    return false
            //}
            else{  return true;}
        }
        function submit(status) {
            if (CheckScoreAndAward()) {                
                if (status == 5) {
                    layer.confirm('确认提交吗？提交后将不能进行修改', {
                        btn: ['确定', '取消'], //按钮
                        title: '操作'
                    }, function (index) {
                        LastSave(status);
                    }, function () { });
                } else {
                    LastSave(status);
                }                
            }
        }
        function LastSave(status) {
            var object = { Func: "Add_AcheiveAllot", Id: cur_AchieveId };
            object.Status = status;
            var addArray = Get_AddMember();
            object.MemberStr = addArray.length > 0 ? JSON.stringify(addArray) : '';
            var editArray = Get_EditMember();
            object.MemberEdit = editArray.length > 0 ? JSON.stringify(editArray) : '';
            var add_path = Get_AddFile(3);
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
            var addArray = [];
            var add_tr = $("#tb_Member tr");
            $(add_tr).each(function (i, n) {
                if ($(this).hasClass('memadd')) {
                    var sub_m = new Object();
                    sub_m.UserNo = $(this).attr('un');
                    sub_m.Score = $(this).find('input[type=number][name=score]').val();                   
                    sub_m.Sort = i + 1;
                    sub_m.CreateUID = loginUser.UniqueNo;
                    addArray.push(sub_m);
                }
            });
            return addArray;
        }
        function Get_EditMember() {
            var editArray = [];
            $("#tb_Member tr").each(function (i, n) {
                if ($(this).hasClass('memedit')) {
                    var sub_e = new Object();
                    sub_e.Id = n.id.replace('tr_mem_', '');
                    sub_e.Score = $(this).find('input[type=number][name=score]').val();                   
                    sub_e.Sort = i + 1;
                    sub_e.EditUID = loginUser.UniqueNo;
                    editArray.push(sub_e);
                }
            });
            return editArray;
        }
        function Check(Status) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "CheckTPM_BookStory", "Status": Status, Id: cur_AchieveId },
                success: function (json) {
                    if (json.result.errMsg == "success") {
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
