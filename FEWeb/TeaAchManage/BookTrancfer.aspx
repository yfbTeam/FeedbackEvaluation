<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookTrancfer.aspx.cs" Inherits="FEWeb.TeaAchManage.BookTrancfer" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>立项转出版</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
   <%--作者信息(已添加的)--%>
    <script type="text/x-jquery-tmpl" id="tr_Info">
        {{each(i, auth) RewardUserInfo_data.retData}}        
        <tr id="tr_auth_${auth.Id}" un="${auth.UserNo}" class="memedit {{if i==0}}meditor{{/if}}">
            <td>{{if i!=0}}<input type="checkbox" name="ck_trsub" onclick="CheckSub(this);" value="${auth.Id}"/>{{/if}}</td>
            <td>${auth.Name}</td>
            <td>
                {{if ULevel==0}}独著
                {{else}}
                    <select>
                         <option value="1" {{if auth.ULevel==1}}selected="selected"{{/if}}>主编</option>
                        {{if i!=0}}<option value="2" {{if auth.ULevel==2}}selected="selected"{{/if}}>参编</option>
                        <option value="3" {{if auth.ULevel==3}}selected="selected"{{/if}}>其他人员</option>{{/if}}  
                    </select>
                {{/if}}
            </td>
            <td><input type="number" value="${auth.Sort}" min="0" {{if i==0}}disabled="disabled"{{/if}}/></td>
            <td>${auth.Major_Name}</td>
            <td><input type="number" value="${auth.WordNum}" /></td>
        </tr>
        {{/each}}        
    </script>
    <%--作者信息(新添加的)--%>
    <script type="text/x-jquery-tmpl" id="itemData">
        <tr un="${UniqueNo}" class="memadd">
            <td><input type="checkbox" name="ck_trsub" onclick="CheckSub(this);"/></td>
            <td>${Name}</td>
            <td>
                <select>
                   <option value="1">主编</option>
                   <option value="2" selected="selected">参编</option>
                   <option value="3">其他人员</option>
                </select></td>
            <td><input type="number" value="" min="0" step="1"/></td>
            <td mid="${Major_ID}">${MajorName}</td>
            <td><input type="number" value=""/></td>          
        </tr>
    </script>
</head>
<body style="background: #fff;">
    <input type="hidden" name="Func" value="AddTPM_BookStory" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="Status" id="Status" value="0" />
    <input type="hidden" id="Id" name="Id" value="" />
    <input type="hidden" id="Group" />
    <input type="hidden" id="hid_UploadFunc" value="Upload_BookStory"/>
    <div class="main" >
        <div class="cont">
            <h2 class="cont_title"><span>教材信息</span></h2>
            <div class="area_form clearfix">
                <div class="input_lable none fl">
                    <label for="">教材类型：</label>
                    <select class="select" id="BookType" name="BookType" isrequired="true" fl="教材类型">
                        <option value="1">立项教材</option>
                        <option value="2" selected="selected">已出版教材</option>
                    </select>
                </div>
                <div class="input_lable fl">
                    <label for="">立项教材：</label>
                    <select class="select" data-placeholder="立项教材" id="SelBook" name="SelBook" isrequired="true" fl="立项教材" onchange="GetBookById()"></select>
                </div>
                <div class="input_lable fl">
                    <label for="">分册情况：</label>
                    <select class="select" id="IsOneVolum" name="IsOneVolum" onchange="IsOneVolum()">
                        <option value="1">单册</option>
                        <option value="2">多册</option>
                    </select>
                </div>
                <div class="input_lable fl">
                    <label for="">主编姓名：</label>
                    <select class="chosen-select" isrequired="true" fl="主编姓名" data-placeholder="负责人" id="MEditor" name="MEditor" onchange="BindDepartInfo('MEditor','MEditorDepart','MEditorDepart_Name');"></select>
                </div>
                <div class="input_lable fl">
                    <label for="">主编单位：</label>
                    <input type="text" isrequired="true" fl="主编单位" name="MEditorDepart_Name" id="MEditorDepart_Name" readonly="readonly" value="无" class="text"/>
                    <input type="hidden" id="MEditorDepart" name="MEditorDepart" value=""/>                  
                </div>
                <div class="input_lable fl">
                    <label for="">书名：</label>
                    <input type="text" isrequired="true" fl="书名" name="Name" id="Name" value="" class="text" />

                </div>
                <div class="input_lable fl">
                    <label for="">使用对象：</label>
                    <input type="text" isrequired="true" fl="使用对象" name="UseObj" id="UseObj" value="" class="text" />

                </div>
                <div class="input_lable fl">
                    <label for="">国家级“十一五”规划教材：</label>
                    <div class="radio_wrap">
                        <input type="radio" value="1" name="IsPlanBook" id="shi" checked="checked" />
                        <label for="shi">是</label>
                        <input type="radio" value="0" name="IsPlanBook" id="fou" />
                        <label for="fou">否</label>
                    </div>
                </div>
                <div class="input_lable none fl edition">
                    <label for="">立项类型：</label>
                    <select class="select" id="" name=""></select>
                </div>

                <div class="input_lable fl  publish">
                    <label for="">出版时间：</label>
                    <input type="text" isrequired="true" fl="出版时间" name="PublisthTime" id="PublisthTime" value="" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM'})" />
                </div>
                <div class="input_lable fl publish">
                    <label for="">出版社：</label>
                    <input type="text" isrequired="true" fl="出版社" name="Publisher" id="Publisher" value="" class="text" />
                </div>
                <div class="input_lable fl publish">
                    <label for="">书号：</label>
                    <input type="text" isrequired="true" fl="书号" name="ISBN" id="ISBN" value="" class="text" />
                </div>
                <div class="input_lable fl publish">
                    <label for="">版次：</label>
                    <div class="radio_wrap">
                        第<input type="text" isrequired="true" fl="版次" name="EditionNo" id="EditionNo" value="" />版
                    </div>
                </div>
                <div class="clear"></div>
                <div class="input_lable fl input_lable2">
                    <label for="">扫描文件：</label>
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
            <h2 class="cont_title none publish" id="congshut"><span>丛书信息</span></h2>
            <div class="area_form none clearfix publish" id="congshu">
                <div class="input_lable fl">
                    <label for="">丛书名称：</label>
                    <input type="text" name="SeriesBookName" id="SeriesBookName" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">代表ISBN号：</label>
                    <input type="text" name="MainISBN" id="MainISBN" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">本丛书本数：</label>
                    <div class="radio_wrap">
                        <input type="text" name="SeriesBookNum" id="SeriesBookNum" value=""/>本
                    </div>
                </div>

            </div>
            <h2 class="cont_title"><span>作者信息</span></h2>
            <div class="area_form clearfix">
                <div class="clearfix">
                    <div class="radio_wrap" onchange="IsOneAuthor()">
                        <span class="fl status mr10">是否独著:</span>
                        <input type="radio" value="1" name="IsOneAuthor" id="shi" />
                        <label for="shi">是</label>
                        <input type="radio" value="0" name="IsOneAuthor" id="fou" checked="checked" />
                        <label for="fou">否</label>
                    </div>
                    <input type="button" name="name" id="DelAuthor" value="删除" class="btn fr" onclick="Del_HtmlAuthor();"/>
                    <input type="button" name="name" id="AddAuthor" value="添加" class="btn fr mr10" onclick="javascript: OpenIFrameWindow('添加作者','AddAchMember.aspx?tb=AuthorInfo', '900px', '650px');" />
                    <%--<select class="chosen-select fr" data-placeholder="作者" id="UserNo"></select>--%>
                </div>
                <table class="allot_table mt10  ">
                    <thead>
                        <tr>
                            <th width="16px"><input type="checkbox" name="ck_tball" onclick="CheckAll(this)"/></th>
                            <th>姓名</th>
                            <th>作者类型</th>
                            <th>排名</th>
                            <th>单位／部门</th>
                            <th>贡献字数（万字）</th>                            
                        </tr>
                    </thead>
                    <tbody id="AuthorInfo">
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="btnwrap" style="background: #fafafa; padding: 15px 0px;">
        <input type="button" value="保存" onclick="submit()" class="btn" />
        <input type="button" value="提交" class="btn ml10" />
    </div>

    <link href="../Scripts/choosen/chosen.css" rel="stylesheet" />
    <link href="../Scripts/choosen/prism.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
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
        var btn_book_allproject = false, btn_book_projectdepart = false, btn_book_noaudit = false;//立项转出版-全校范围,立项转出版-院系范围,无需审核
        $(function () {
            Get_PageBtn("/TeaAchManage/MyBookList.aspx");
            btn_book_allproject = JudgeBtn_IsExist("btn_book_allproject")
              , btn_book_projectdepart = JudgeBtn_IsExist("btn_book_projectdepart")
              , btn_book_noaudit = JudgeBtn_IsExist("btn_book_noaudit");
            $("#CreateUID").val(GetLoginUser().UniqueNo);
            BindFile_Plugin();
            Get_UserAndMajor("MEditor", GetLoginUser().UniqueNo, "add", btn_book_projectdepart, btn_book_allproject);
            BindBook(1, "");           
        })
        function GetBookById() {
            //$("#Id").val($("#SelBook").val());
            Get_Sys_Document(2, $("#SelBook").val());
            BindBook("", $("#SelBook").val());
            //GetTPM_UserInfo("",  $("#SelBook").val());
        }
        function BindBook(type, id) {
            var parmsData = { "Func": "GetTPM_BookStory", "IsPage": "false", Id: id, BookType: type, "Status": "3", IdentifyCol: 0 };
            if (type == 1) {
                $("#SelBook").html('<option value="">请选择教材</option>');
                if (!btn_book_allproject && !btn_book_projectdepart) { parmsData["AuthorNo"] = loginUser.UniqueNo; }
                else if (btn_book_projectdepart && !btn_book_allproject) { parmsData["Major_ID"] = loginUser.Major_ID; }
            }            
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: parmsData,
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        if (id == "") {                            
                            $(json.result.retData).each(function () {
                                $("#SelBook").append('<option value="' + this.Id + '">' + this.Name + '</option>');
                            });
                            $("#SelBook").chosen({
                                allow_single_deselect: true,
                                disable_search_threshold: 1,
                                no_results_text: '未找到',
                                search_contains: true,
                                width: '270px'
                            });
                        }
                        else {                            
                            $(json.result.retData).each(function () {                               
                                $("#Name").val(this.Name);
                                $("#IsOneVolum").val(this.IsOneVolum);                               
                                var btn_book_addall = JudgeBtn_IsExist("btn_book_addall");
                                if (btn_book_addall) {
                                    $("#MEditor").val(this.MEditor);
                                    $("#MEditor").trigger("chosen:updated");
                                    $("#MEditor").chosen();
                                    BindDepartInfo('MEditor', 'MEditorDepart', 'MEditorDepart_Name');                                    
                                }                               
                                $("#UseObj").val(this.UseObj);
                                $("#IsPlanBook").val(this.IsPlanBook);
                                IsOneVolum();
                            });
                        }
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //是否单册
        function IsOneVolum() {
            if ($("#IsOneVolum").val() == "1") {
                $("#congshut").hide();
                $("#congshu").hide();
                $("#congshu input").children().hide();
            }
            else {
                if ($("#BookType").val() == "2") {
                    $("#congshut").show();
                    $("#congshu").show();
                    $("#congshu input").children().show();
                }
            }
        }
        function IsOneAuthor() {                   
            if ($("input[name='IsOneAuthor']:checked").val() == "1") {
                $("#DelAuthor").hide();
                $("#AddAuthor").hide();
                $("#AuthorInfo tr").not('.meditor').remove();
            }
            else {
                $("#DelAuthor").show();
                $("#AddAuthor").show();
            }
            Set_FirstAuthor();
        }
        function submit() {
            $("#Status").val(btn_book_noaudit ? "3" : "1");         
            Save(1);
        }
        //提交按钮
        function Save(s_type) {
            //验证为空项或其他          
            var valid_flag = validateForm($('select,input[type="text"]:visible'));
            if (valid_flag != 0)////验证失败的情况  需要表单的input控件 有 isrequired 值为true或false 和fl 值为不为空的名称两个属性
            {
                return false;
            }
            if($("#IsOneVolum").val() == "2"){                
                if (!$("#MainISBN").val().trim().length) {layer.msg("请输入丛书名称！");return;}
                if (!$("#SeriesBookName").val().trim().length) {layer.msg("请输入代表ISBN号！");return;}
                if (!$("#SeriesBookNum").val().trim().length) { layer.msg("请输入本丛书本数！"); return; }
            }
            if ($("input[name='IsOneAuthor']:checked").val() == "0" && $("#AuthorInfo tr").length == 1) {
                layer.msg("该教材非独著，请添加作者后提交！");
                return;
            }
            var au_count = 0;
            $("#AuthorInfo tr input[type='number']").each(function () {
                if ($(this).val().trim() == "") {
                    au_count++;
                    return false;
                }
            });
            if (au_count > 0) { layer.msg("请填写作者信息处的排名及贡献字数！"); return; }
            var o = getFromValue();
            if (o["OneAuthor"]) {
                if (!o["OneAuthor"].push) {
                    o["OneAuthor"] = [o["OneAuthor"]];
                }
                o["OneAuthor"].push($("input[name='IsOneAuthor']:checked").val());
            } else {
                o["OneAuthor"] = $("input[name='IsOneAuthor']:checked").val();
            }

            if (o["PlanBook"]) {
                if (!o["PlanBook"].push) {
                    o["PlanBook"] = [o["PlanBook"]];
                }
                o["PlanBook"].push($("input[name='IsPlanBook']:checked").val());
            } else {
                o["PlanBook"] = $("input[name='IsPlanBook']:checked").val();
            }
            var addArray = Rtn_AddAuthorArray();
            o.MemberStr = addArray.length > 0 ? JSON.stringify(addArray) : '';
            var editArray = Rtn_EditAuthorArray();
            o.MemberEdit = editArray.length > 0 ? JSON.stringify(editArray) : '';
            var add_path = Get_AddFile(2);
            o.Add_Path = add_path.length > 0 ? JSON.stringify(add_path) : "";
            o.Edit_PathId = Get_EditFileId();
            if (s_type == 1) {
                layer.confirm('确认提交吗？提交后将不能进行修改', {
                    btn: ['确定', '取消'], //按钮
                    title: '操作'
                }, function (index) {
                    LastSave(o);
                }, function () { });
            } else {
                LastSave(o);
            }
        }
        function LastSave(o) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: o,//组合input标签
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        parent.layer.msg('操作成功!');
                        Del_Document();
                        parent.Book(1, 10);
                        parent.CloseIFrameWindow();
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function (errMsg) {
                    alert(errMsg);
                    //接口错误时需要执行的
                }
            });
        }
        function GetTPM_UserInfo(RIId, BookId) {            
            $("#AuthorInfo").empty();
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetTPM_UserInfo", "IsPage": "false", RIId: RIId, BookId: BookId },
                success: function (json) {                    
                    if (json.result.errMsg == "success") {
                        $("#tr_Info").tmpl(json.result.retData).appendTo("#AuthorInfo");
                    }                    
                    IsOneAuthor();
                    IsDisabled_IsOneAuthor();
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }        
    </script>
</body>
</html>

