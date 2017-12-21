<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevelMan.aspx.cs" Inherits="FEWeb.SysSettings.LevelMan" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>业绩等级管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script id="tr_Level" type="text/x-jquery-tmpl">
        <li>
            <dt class="version_header clearfix">
                <span>${Name}</span>
                <i class="iconfont fr icond" id="${Id}">&#xe643;</i>
                <input type="button" name="name" value="新增奖项" class="btn fr ml10" onclick="AddReward('${Id}')" />
                <div class="oprated fr">
                    <div class="operate" onclick="javascript:OpenIFrameWindow('编辑等级', '../TeaAchManage/LeveAdd.aspx?Id=${Id}', '500px', '300px');">
                        <i class="iconfont color_purple">&#xe628;</i>
                        <span class="operate_none bg_purple">编辑</span>
                    </div>
                    <div class="operate" onclick="LeverSort('up',${Id})">
                        <i class="iconfont color_purple">&#xe629;</i>
                        <span class="operate_none bg_purple">上移</span>
                    </div>
                    <div class="operate" onclick="LeverSort('down',${Id})">
                        <i class="iconfont color_purple">&#xe62d;</i>
                        <span class="operate_none bg_purple">下移</span>
                    </div>
                </div>
            </dt>
            <dd class="none">
                <div class="table">
                    <table>
                        <thead>
                            <tr>
                                <th>奖项</th>
                                <th>分数（分）</th>
                                <th>奖金（万元）</th>
                                <th>追加（万元）</th>
                                <th id="ops" width="230px;">操作</th>
                            </tr>
                        </thead>
                        <tbody id="Leve${Id}"></tbody>
                    </table>
                </div>
            </dd>
        </li>
    </script>
    <script id="tr_Reward" type="text/x-jquery-tmpl">
        <tr>
            <td>${Name}</td>
            <td>${Score}</td>
            <td>${FirstMoney}</td>
            <td>${AddMoney}</td>
            <td class="operate_wrap">
                <div class="operate" onclick="javascript:OpenIFrameWindow('奖金管理', '../TeaAchManage/AddAward.aspx?Id=${Id}', '600px', '400px');">
                    <i class="iconfont color_purple">&#xe623;</i>
                    <span class="operate_none bg_purple">奖金</span>
                </div>
                <div class="operate" onclick="javascript:OpenIFrameWindow('编辑奖项', '../TeaAchManage/RewardAdd.aspx?Id=${Id}&batid=${FirstId}', '500px', '480px');">
                    <i class="iconfont color_purple">&#xe628;</i>
                    <span class="operate_none bg_purple">编辑</span>
                </div>
                <div class="operate" onclick="RewardSort('up',${Id})">
                    <i class="iconfont color_purple">&#xe629;</i>
                    <span class="operate_none bg_purple">上移</span>
                </div>
                <div class="operate" onclick="RewardSort('down',${Id})">
                    <i class="iconfont color_purple">&#xe62d;</i>
                    <span class="operate_none bg_purple">下移</span>
                </div>
                {{if ScoreType==3}}
                 <div class="operate" onclick="javascript:OpenIFrameWindow('等级设置', '../TeaAchManage/RankSet.aspx?RId=${Id}&Score=${Score}', '500px', '550px');">
                     <i class="iconfont color_purple">&#xe630;</i>
                     <span class="operate_none bg_purple">排名</span>
                 </div>
                {{else}}
                {{/if}}
            </td>
        </tr>
    </script>
</head>
<body>
    <input id="LID" type="hidden" />
    <input id="ELID" type="hidden" />
    <input id="EID" type="hidden" />
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            
            <div class="sortwrap clearfix">
                <div class="menu fl">
                    <h1 class="titlea">业绩等级管理</h1>
                    <ul class="menu_list">
                       
                    </ul>
                    <input type="button" name="name" value="设置" class="new" onclick="OpenIFrameWindow('菜单设置','MenuSetting.aspx','900px','680px')" />
                </div>
                <div class="sort_right fr">
                    <div class="nav_version_wrap clearfix">
                        <div class="nav_version_left fl">
                            <%--<a href="javascript:;" class="selected">[2017] 10号 (12-02~17-12)</a>
                            <a href="javascript:;">[2017] 10号 (12-02~17-12)</a>--%>
                        </div>
                        <div class="fr">
                            <input type="button" name="name" value="编辑版本" class="btn2" id="EditEdition" />
                            <input type="button" name="name" value="新增版本" class="btn2" id="NewEdition" />
                        </div>
                    </div>
                    <div class="nav_title clearfix">
                        <span id="BigGroupName"></span>
                        <div class="fr clearfix">
                            <div class="input-wrap fl mr10" style="margin-bottom: 0;" onclick="ChangeAwardSwichStatus()">
                                <label for="" style="min-width: auto;">金额分配：</label>
                                <span class="switch-on" themecolor="#6a264b" id="AwardSwich"></span>
                            </div>
                            <input type="button" name="name" value="新增等级" class="btn" id="NewLevel" />
                        </div>
                    </div>
                    <div class="">
                        <ul class="version_lists">
                            <%--<li>
                                <dt class="version_header clearfix">
                                    <span>国家级</span>
                                    <i class="iconfont fr icond">&#xe643;</i>
                                    <input type="button" name="name" value="新增奖项" class="btn fr ml10" />
                                    <div class="oprated fr">
                                        <div class="operate">
                                            <i class="iconfont color_purple">&#xe628;</i>
                                            <span class="operate_none bg_purple">编辑</span>
                                        </div>
                                        <div class="operate">
                                            <i class="iconfont color_gray">&#xe629;</i>
                                            <span class="operate_none bg_gray">上移</span>
                                        </div>
                                        <div class="operate">
                                            <i class="iconfont color_purple">&#xe62d;</i>
                                            <span class="operate_none bg_purple">下移</span>
                                        </div>
                                    </div>
                                </dt>
                                <dd class="none">
                                    <div class="table">
                                        <table>
                                            <thead>
                                                <tr>
                                                    <th>奖项</th>
                                                    <th>分数</th>
                                                    <th>排名递减</th>
                                                    <th>奖金</th>
                                                    <th>追加</th>
                                                    <th id="ops">操作</th>
                                                </tr>
                                            </thead>
                                            <tbody id="myPirze_table">
                                            </tbody>
                                        </table>
                                    </div>
                                </dd>
                            </li>--%>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/pagination/jquery.pagination.js"></script>
    <link href="../Scripts/pagination/pagination.css" rel="stylesheet" />
    <link href="../Scripts/HoneySwitch/honeySwitch.css" rel="stylesheet" />
    <script src="../Scripts/HoneySwitch/honeySwitch.js"></script>
    <script>
        $(function () {
            $("#CreateUID").val(GetLoginUser().UniqueNo);
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
    </script>
    <script>
        var UrlDate=new GetUrlDate();
        function AddReward(LID)
        {
            OpenIFrameWindow('新增奖项', '../TeaAchManage/RewardAdd.aspx?LID=' + LID, '500px', '410px');
        }
        //开启（关闭）金额分配
        function ChangeAwardSwichStatus()
        { 
            var AwardSwich=0;
            if($("#AwardSwich").hasClass("switch-on"))
            {
                AwardSwich=1;
            }
            var Id= $("#ELID").val();
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "ChangeAwardSwichStatus", "IsPage": "false",Id:Id,AwardSwich:AwardSwich },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        if (AwardSwich=="1") {
                            honeySwitch.showOn("#AwardSwich");
                            $("#"+Id).attr("switch","1");
                        }
                        else
                        {
                            honeySwitch.showOff("#AwardSwich");
                            $("#"+Id).attr("switch","0");
                        }
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        $(function () {
            menu_list("");
            $("#NewLevel").click(function () {
                var EID=$("#EID").val();

                if (!EID) {
                    layer.msg("请选择一个版本");
                }
                else
                {
                    OpenIFrameWindow('新增等级', '../TeaAchManage/LeveAdd.aspx?EID=' +EID , '500px', '270px');
                }
            })
            Edition('#NewEdition', 'EditionAdd','新增');
            Edition('#EditEdition', 'EditionEdit','编辑','800','500');
        })
        function Edition(obj, src,name, width, height) {
            width = width || 600;
            height = height || 270;
            $(obj).click(function () {
                var LID = $("#ELID").val();

                if (!LID) {
                    layer.msg("请选择一个分类");
                }
                else {
                    OpenIFrameWindow('' + name + '版本', '../TeaAchManage/' + src + '.aspx?LID=' + LID + '&id=' + $(".nav_version_left .selected").attr('id'), '' + width + 'px', '' + height + 'px');
                }
            })
        }
        //业绩分类
        function menu_list(node) {
            if (node=="") {
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                    type: "post",
                    dataType: "json",
                    data: { "Func": "GetAcheiveLevelData", "IsPage": "false" },
                    async:false,
                    success: function (json) {
                        if (json.result.errMsg == "success") {
                            AcheiveLevel = json.result.retData;
                        }
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            }
            else{
                AcheiveLevel=node;
            }
            BindP();
        }
        function BindP()
        {
            $('.menu_list').html('');
            $(AcheiveLevel).each(function () {
                if (this.Pid == "0") {
                    var ulID = 'ul' + this.Id;
                    //$("#ELID").val(this.Id)
                    $(".menu_list").append("<li><span>" + this.Name + "<i class=\"iconfont\">&#xe643;</i></span><ul id=" + ulID + "></ul></li>")
                    BindChild(this.Id);
                }
            })
            InitClass();
        }
        function BindChild(Id) {
            var ulID = 'ul' + Id;
            $("#" + ulID).html("");
            $(AcheiveLevel).each(function () {
                if (this.Pid == Id) {
                    $("#" + ulID).append("<li id=" + this.Id + " switch="+this.AwardSwich+">" + this.Name + "</li>")
                }
            })
        }        
        function InitClass() {
            $('.menu_list li:eq(0)').children('span').addClass('selected');
            $('.menu_list li:eq(0)').children('ul').slideDown();
            $('.menu_list li:eq(0)').children('ul').find("li:eq(0)").addClass('selected');
            var id=$('.menu_list li:eq(0)').children('ul').find("li:eq(0)").attr("id");
            var Name=$('.menu_list li:eq(0)').children('ul').find("li:eq(0)").html();
            var AwardSwich=$('.menu_list li:eq(0)').children('ul').find("li:eq(0)").attr("switch");
            if (AwardSwich=="0") {
                honeySwitch.showOff("#AwardSwich");
            }
            else{
                honeySwitch.showOn("#AwardSwich");
            }
            $("#BigGroupName").html(Name);
            $("#ELID").val(id)
            BindEdition(id);
            $('.menu_list>li>span').hover(function(){
                $(this).siblings('em').show();
            },function(){
                $(this).siblings('em').hide();
            })
            $('.menu_list').find('li:has(ul)').children('span').click(function () {
                var $next = $(this).next('ul');
                if ($next.is(':hidden')) {
                    $(this).addClass('selected');
                    $next.stop().slideDown();
                    $('.menu_list').find('li').removeClass('selected');
                    $(this).next('ul').find("li:eq(0)").addClass("selected");
                    
                    var id= $(this).next('ul').find("li:eq(0)").addClass("selected").attr("id");
                    var Name=$(this).next('ul').find("li:eq(0)").html();
                    var AwardSwich= $(this).next('ul').find("li:eq(0)").attr("switch");
                    //$("#BigGroupName").html(Name);
                    //$("#ELID").val(id)
                    //BindEdition(id);
                    GroupSelected(id,Name,AwardSwich);

                    if($(this).parent('li').siblings().children('ul').is(':visible')) {
                        $(this).parent('li').siblings().children('span').removeClass('selected');
                        $(this).parent('li').siblings().children('ul').stop().slideUp();
                    }
                } else {
                    $(this).removeClass('selected');
                    $next.stop().slideUp();
                }
            });
            $('.menu_list').find('li:has(ul)').find('li').click(function () {
                $('.menu_list').find('li').removeClass('selected');
                $(this).addClass('selected').siblings().removeClass('selected');
                //$("#ELID").val($(this).attr("id"));
                //$("#BigGroupName").html($(this).html());
                ////绑定版本
                //BindEdition($(this).attr("id"));
                GroupSelected($(this).attr("id"),$(this).html(),$(this).attr("switch"));
            });
        }
        function GroupSelected(id,name,AwardSwich)
        {
            $("#ELID").val(id);
            $("#BigGroupName").html(name);
            if (AwardSwich=="0") {
                honeySwitch.showOff("#AwardSwich");
            }
            else{
                honeySwitch.showOn("#AwardSwich");
            }
            //绑定版本
            BindEdition(id);
        }

        //奖项版本
        function BindEdition(LID) {
            $(".nav_version_left").html("");
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetRewardEditionData", "LID": LID, "IsPage": "false" },
                success: function (json) {
                    var i = 0;
                    if (json.result.errMsg == "success") {
                        $(json.result.retData).each(function () {
                            if (i == 0) {
                                $(".nav_version_left").append('<a href="javascript:;" id=' + this.Id + ' class="selected" onclick="BindRewardLevelData(' + this.Id +',this)">' + this.Name + '</a>')
                                $("#EID").val(this.Id);
                                BindRewardLevelData(this.Id);
                            }
                            else {
                                $(".nav_version_left").append('<a href="javascript:;" id=' + this.Id + ' onclick="BindRewardLevelData(' + this.Id + ',this)">' + this.Name + '</a>')
                            }
                            i++;
                        });
                    }
                    else{
                        $(".version_lists").html(""); 
                        $("#EID").val("");
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //奖项等级
        function BindRewardLevelData(EID,em) {
            $(em).addClass("selected").siblings().removeClass("selected");
            if (EID==undefined) {
                EID=$("#EID").val();
            }
            else{
                $("#EID").val(EID);
            }
            //$(em).addClass("selected").siblings().removeClass("selected");
            $(".version_lists").html("");
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetRewardLevelData", "EID": EID, "IsPage": "false" },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $("#tr_Level").tmpl(json.result.retData).appendTo(".version_lists");
                        $('.oprated').find('.operate').hover(function () {
                            $(this).find('.operate_none').slideDown();
                        }, function () {
                            $(this).find('.operate_none').stop().slideUp();
                        });
                    }
                    get_animate();
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }        
        function get_animate() {
            $('.version_lists').find('li:has(dt)').find('.icond').click(function () {
                BindReward($(this).attr("id"));
                $("#LID").val($(this).attr("id"))

                var $next = $(this).parent().next('dd');
                if ($next.is(':hidden')) {
                    $(this).parent().siblings('li').removeClass('active');
                    $(this).parents('li').addClass('active');
                    $next.show();
                    if ($(this).parents('li').siblings('li').children('dd').is(':visible')) {
                        $(this).parents("li").siblings("li").removeClass('active');
                        $(this).parents("li").siblings("li").find("dd").hide();
                    }
                } else {
                    $(this).parents('li').removeClass('active');
                    $next.hide();
                }
            })
            var Count= $('.version_lists').find("li").length;
            var $First= $('.version_lists').find("li:eq(0)").find('dt').find('div').children().eq(1);
            $First.removeAttr("onclick");
            $First.find('i').removeClass('color_purple').addClass('color_gray');
            $First.find('span').removeClass('bg_purple').addClass('bg_gray');

            var $Last= $('.version_lists').find("li:eq("+parseInt(Count-1)+")").find('dt').find('div').children().eq(2);
            $Last.removeAttr("onclick");
            $Last.find('i').removeClass('color_purple').addClass('color_gray');
            $Last.find('span').removeClass('bg_purple').addClass('bg_gray');

        }
        function LeverSort(type,Id){
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "SortRewardLevelData", "Id": Id, "SortType": type },
                success: function (json) {
                    if (json.result.errMsg == "") {
                        BindRewardLevelData($("#EID").val());
                    }       
                    else{
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        //奖项
        function BindReward(LID) {
            if (LID==undefined) {
                LID= $("#LID").val();
            }
            var Leveid = "Leve" + LID;
            //$("#" + Leveid).parent().parent().parent().show();
            $("#" + Leveid).html("");
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetRewardInfoData", "LID": LID, "IsPage": "false" },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $("#tr_Reward").tmpl(json.result.retData).appendTo("#" + Leveid);
                    }
                    tableSlide();
                    TableInit();                     
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function TableInit()
        {
            $(".version_lists").find("li").each(function(){
                var Count= $(this).find("table").find("tbody").find("tr").length;
                var $First=$(this).find("table").find("tbody").find("tr:eq(0)").find('td').find('div').eq(2);
                $First.removeAttr("onclick");
                $First.find('i').removeClass('color_purple').addClass('color_gray');
                $First.find('span').removeClass('bg_purple').addClass('bg_gray');

                var $Last=$(this).find("table").find("tbody").find("tr:eq("+parseInt(Count-1)+")").find('td').find('div').eq(3);
                $Last.removeAttr("onclick");
                $Last.find('i').removeClass('color_purple').addClass('color_gray');
                $Last.find('span').removeClass('bg_purple').addClass('bg_gray');
            });
        }
        function RewardSort(type,Id){
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchManage.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "SortRewardInfoData", "Id": Id, "SortType": type },
                success: function (json) {
                    if (json.result.errMsg == "") {
                        BindReward($("#LID").val());
                    }       
                    else{
                        layer.msg(json.result.errMsg);
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
