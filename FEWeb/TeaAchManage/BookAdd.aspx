<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookAdd.aspx.cs" Inherits="FEWeb.TeaAchManage.BookAdd" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>新增教材</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <link href="../Scripts/choosen/chosen.css" rel="stylesheet" />
    <link href="../Scripts/choosen/prism.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
     <%--作者信息(已添加的)--%>
    <script type="text/x-jquery-tmpl" id="tr_Info">
        {{each(i, auth) RewardUserInfo_data.retData}}        
        <tr id="tr_auth_${auth.Id}" un="${auth.UserNo}" class="memedit {{if i==0}}meditor{{/if}}">
            <td>{{if i!=0}}<input type="checkbox" name="ck_trsub" onclick="CheckSub(this);" value="${auth.Id}"/>{{/if}}</td>
            <td>${auth.Name}</td>
            <td>
                {{if auth.ULevel==0}}独著
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
            <td><input type="number" value="${auth.WordNum}" step="0.01"/></td>
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
            <td><input type="number" value="" step="0.01"/></td>          
        </tr>
    </script>
</head>
<body style="background: #fff;">
    <input type="hidden" name="Func" value="AddTPM_BookStory" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="Status" id="Status" value="0" />
    <input type="hidden" id="Id" name="Id" value="" />
    <input type="hidden" id="Group" name="Group" />
    <input type="hidden" id="hid_UploadFunc" value="Upload_BookStory"/>
    <div class="main">
        <div class="cont">
            <h2 class="cont_title"><span>教材信息</span></h2>
            <div class="area_form clearfix">
                <div class="input_lable fl">
                    <label for="">教材类型：</label>
                    <select class="select" id="BookType" name="BookType" onchange="ChangeType()">
                        <option value="1" selected="selected">立项教材</option>
                        <option value="2">已出版教材</option>
                    </select>
                </div>
                <div class="input_lable fl">
                    <label for="">分册情况：</label>
                    <select class="select" id="IsOneVolum" name="IsOneVolum" onchange="IsOneVolum()" <%--disabled="disabled"--%>>
                        <option value="1">单册</option>
                        <option value="2">多册</option>
                    </select>
                </div>
                <div class="input_lable fl">
                    <label for="">主编姓名：</label>
                    <select class="chosen-select" data-placeholder="主编姓名" isrequired="true" fl="主编姓名" id="MEditor" name="MEditor" onchange="BindDepartInfo('MEditor','MEditorDepart','MEditorDepart_Name');"></select>
                </div>
                <div class="input_lable fl">
                    <label for="">主编单位：</label>
                   <input type="text" isrequired="true" fl="主编单位" name="MEditorDepart_Name" id="MEditorDepart_Name" readonly="readonly" value="无" class="text"/>
                    <input type="hidden" id="MEditorDepart" name="MEditorDepart" value=""/>  
                </div>
                <div class="input_lable fl">
                    <label for="">书名：</label>
                    <input type="text" name="Name" id="Name" isrequired="true" fl="书名" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">使用对象：</label>
                    <input type="text" name="UseObj" id="UseObj" isrequired="true" fl="使用对象" value="" class="text" />
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
                <div class="input_lable fl edition">
                    <label for="">立项类型：</label>
                    <select class="select" id="ProjectType" name="ProjectType">
                        <option value="1" selected="selected">北京市精品教材立项</option>
                        <option value="2">国家级精品教材立项</option>
                    </select>
                </div>

                <div class="input_lable fl none publish">
                    <label for="">出版时间：</label>
                    <input type="text" isrequired="true" fl="出版时间" name="PublisthTime" id="PublisthTime" value="" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM'})" />
                </div>
                <div class="input_lable fl none publish">
                    <label for="">出版社：</label>
                    <input type="text" isrequired="true" fl="出版社" name="Publisher" id="Publisher" value="" class="text" />
                </div>
                <div class="input_lable fl none publish">
                    <label for="">书号：</label>
                    <input type="text" isrequired="true" fl="书号" name="ISBN" id="ISBN" value="" class="text" />
                </div>
                <div class="input_lable fl none publish">
                    <label for="">版次：</label>
                    <div class="radio_wrap">
                        第<input type="text" isrequired="true" fl="版次" name="EditionNo" id="EditionNo" value="" />版
                    </div>
                </div>
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
            <h2 class="cont_title none" id="congshut"><span>丛书信息</span></h2>
            <div class="area_form none clearfix" id="congshu">
                <div class="input_lable fl">
                    <label for="">丛书名称：</label>
                    <input type="text" isrequired="true" fl="丛书名称" name="SeriesBookName" id="SeriesBookName" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">代表ISBN号：</label>
                    <input type="text" isrequired="true" fl="代表ISBN号" name="MainISBN" id="MainISBN" value="" class="text" />
                </div>
                <div class="input_lable fl">
                    <label for="">本丛书本数：</label>
                    <div class="radio_wrap">
                        <input type="text" isrequired="true" fl="本丛书本数" name="SeriesBookNum" id="SeriesBookNum" value="" />本
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
                    <input type="button" name="name" id="AddAuthor" value="添加" onclick="javascript: OpenIFrameWindow('添加作者','AddAchMember.aspx?tb=AuthorInfo', '900px', '650px');" class="btn fr mr10" />
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
                    <tbody id="AuthorInfo"></tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="btnwrap" style="background: #fafafa; padding: 15px 0px;">
        <input type="button" value="保存" onclick="Save()" class="btn" style="display:none;" id="Save" />
        <input type="button" value="提交" class="btn ml10" onclick="submit()" id="Submit" />
    </div>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../Scripts/choosen/prism.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/Webuploader/dist/webuploader.js"></script>
    <link href="../Scripts/Webuploader/css/webuploader.css" rel="stylesheet" />
    <script src="upload_batchfile.js"></script>
    <script src="BaseUse.js"></script>
    <script>
        var UrlDate = new GetUrlDate();
        var btn_book_addall = false, btn_book_adddepart = false, btn_book_noaudit = false;//新增教材-全校范围,新增教材-院系范围,无需审核
        $(function () {            
            Get_PageBtn("/TeaAchManage/MyBookList.aspx");
            btn_book_addall = JudgeBtn_IsExist("btn_book_addall")
               , btn_book_adddepart = JudgeBtn_IsExist("btn_book_adddepart")
               , btn_book_noaudit = JudgeBtn_IsExist("btn_book_noaudit");
            $("#CreateUID").val(GetLoginUser().UniqueNo);                         
            var Id = UrlDate.Id;
            $("#Id").val(Id);
            BindFile_Plugin();
            if (Id != undefined) {
                $("#BookType").attr("disabled", "disabled");
                Get_Sys_Document(2, $("#Id").val());
                GetBookDetailById();
            } else {
                $("#Save").show();
                Get_UserAndMajor("MEditor", GetLoginUser().UniqueNo, "add", btn_book_adddepart, btn_book_addall);
            }
        });       
        function GetBookDetailById() {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetTPM_BookStory", "IsPage": "false", Id: UrlDate.Id },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $(json.result.retData).each(function () {
                            $("#Name").val(this.Name);
                            $("#BookType").val(this.BookType);
                            //只有未提交状态的教材，修改教材才能修改教材类型
                            if (this.Status != "0") {//未提交
                                $("#BookType").attr("disabled", "disabled");
                                $("#Save").hide();
                            }
                            else {
                                $("#Save").show();
                            }
                            $("#IsOneVolum").val(this.IsOneVolum);
                            $("#MEditorDepart").val(this.MEditorDepart);
                            $("#MEditorDepart_Name").val(this.MEditorDepart_Name);
                            $("#UseObj").val(this.UseObj);
                            $("#IsPlanBook").val(this.IsPlanBook);
                            $("#ProjectType").val(this.ProjectType);
                            $("#SeriesBookName").val(this.SeriesBookName);
                            $("#MainISBN").val(this.MainISBN);
                            $("#SeriesBookNum").val(this.SeriesBookNum);
                            if (this.IsPlanBook == "1") {
                                $(':radio[name="IsPlanBook"]').eq(0).attr("checked", true);
                            }
                            else {
                                $(':radio[name="IsPlanBook"]').eq(1).attr("checked", true);
                            }
                            if (this.IsOneAuthor == "1") {
                                $(':radio[name="IsOneAuthor"]').eq(0).attr("checked", true);
                            }
                            else {
                                $(':radio[name="IsOneAuthor"]').eq(1).attr("checked", true);
                            }

                            $("#EditionNo").val(this.EditionNo);
                            $("#ISBN").val(this.ISBN);
                            $("#Publisher").val(this.Publisher);
                            $("#PublisthTime").val(DateTimeConvert(this.PublisthTime, 'yyyy-MM'));
                            IsOneVolum();
                            ChangeType();
                            IsOneAuthor();
                            Get_UserAndMajor("MEditor", this.MEditor, 'edit', btn_book_adddepart, btn_book_addall);
                            GetTPM_RewardUserInfo();                            
                        });
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //立项、出版
        function ChangeType() {           
            if ($("#BookType").val() == "1") { //立项教材
                $(".publish").hide();
                $(".edition").show();               
            }
            else {//出版教材                
                $(".publish").show();
                $(".edition").hide();                
            }            
        }
        //是否单册
        function IsOneVolum() {
            if ($("#IsOneVolum").val() == "1") {
                $("#congshut").hide();
                $("#congshu").hide();
            }
            else {               
                $("#congshut").show();
                $("#congshu").show();
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
            $("#Status").val(btn_book_noaudit? "3" : "1");
            Save(1);
        }
        //提交按钮
        function Save(s_type) {
            type = arguments[0] || 0;
            var valid_flag=0;
            if ($("#BookType").val() != "1") {                
                //验证为空项或其他          
                valid_flag = validateForm($('select,input[type="text"]:visible'));             
                if(valid_flag== 0&&$("input[name='IsOneAuthor']:checked").val() == "0"&& $("#AuthorInfo tr").length==1){
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
            }
            else{
                var name = $("#Name").val().trim(),meditor=$("#MEditor").val();
                if (!name.length) { layer.msg("请填写书名！"); return; }
                if (!meditor.length) { layer.msg("请选择主编姓名！"); return; }
            }           
            if (valid_flag== 0)
            {
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
        }
        function LastSave(o) {
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: o,//组合input标签
                async: false,
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
                    alert(errMsg);//接口错误时需要执行的
                }
            });
        }
    </script>
</body>
</html>
