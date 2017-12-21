<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcheveStatis.aspx.cs" Inherits="FEWeb.TeaAchManage.AcheveStatis" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>业绩统计</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script> 
        <script type="text/x-jquery-tmpl" id="li_User">
            <li>
                <dt class="clearfix">
                    <span>${Name}</span>
                    <span>${Major_Name}</span>
                    <i class="iconfont">&#xe643;</i>
                    <span style="margin-right: 100px; float: right;">${SumScore}分</span>
                </dt>
                <dd>
                    <div class="table">
                        <table>
                            <thead>
                                <tr>
                                    <th>奖励项目</th>
                                    <th>获奖项目名称</th>
                                    <th>负责单位</th>
                                    <th>负责人</th>
                                    <th>获奖年度</th>
                                    <th>个人分数</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody id="u-${UserNo}"></tbody>
                        </table>
                    </div>
                </dd>
            </li>
    </script>
    <script type="text/x-jquery-tmpl" id="tr_CurInfo">
        <tr>
            <td>${GidName}</td>
            <td>${AchiveName}</td>
            <td>${Major_Name}</td>
            <td>${ResponsName}</td>
            <td>${Year}</td>        
            <td>${Score}</td>
            <td class="operate_wrap">
                <div class="operate" onclick="OpenIFrameWindow('业绩查看', 'CheckAchieve.aspx?Id=${RIId}&Type=View', '1000px', '700px')">
                    <i class="iconfont color_purple">&#xe60b;</i>
                    <span class="operate_none bg_purple">查看</span>
                </div>
            </td>
        </tr>
    </script>
</head>
<body>
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" style="padding-bottom: 0px;">
            <div class="search_toobar clearfix">
                <div class="fl" id="reg_eval">
                    <label for="">获奖年度:</label>
                    <input type="text" id="BeginTime" value="" class="text Wdate" style="border: 1px solid #cccccc" onclick="WdatePicker({ dateFmt: 'yyyy' })" />
                    <span>~</span>
                    <input type="text" id="EndTime" value="" class="text Wdate" style="border: 1px solid #cccccc" onclick="WdatePicker({ dateFmt: 'yyyy' })" />
                </div>
                <div class="fl ml20 none" id="div_Depart">
                    <label for="">部门:</label>
                    <select class="select" style="width: 198px;" id="DepartMent" onchange="BindData(1,10)">
                        <option value="">全部</option>
                    </select>
                </div>
                <div class="fl ml20">
                    <input type="text" name="key" id="key" placeholder="请输入教师姓名关键字" value="" class="text fl" style="width: 150px;">
                    <a class="search fl" onclick="BindData(1,10);"><i class="iconfont">&#xe600;</i></a>
                </div>
                <div class="fr ml20">
                    <label id="lbl_AllScore" for="" style="color:#731F4F;"></label>
                </div>
            </div>
            <ul class="statis_lists" id="tb_info"></ul>
            <div id="tb_info_page" class="page"></div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/laypage/laypage.js"></script>
    <script>
        var list_achstatis_showall = false;
        var loginUser = GetLoginUser();
        $(function () {
            $("#CreateUID").val(loginUser.UniqueNo);
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            Get_PageBtn("/TeaAchManage/AcheveStatis.aspx");
            list_achstatis_showall = JudgeBtn_IsExist("list_achstatis_showall");
            if (list_achstatis_showall) {
                $("#div_Depart").show();
                BindDepart();
            } 
            BindData(1, 10);
        })
        function BindDepart() {
            $.ajax({
                url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
                type: "post",
                dataType: "json",
                data: {
                    func: "GetMajors"
                },
                async: false,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#DepartMent").append('<option value="' + this.Id + '">' + this.Major_Name + '</option>');
                        });
                    }
                },
                error: function (errMsg) {}
            });
        }        
        function BindData(startIndex, pageSize) {
            $("#tb_info").empty();
            var parmsData = {
                "Func": "GetTPM_UserInfo", IsStatistic: "1", PageIndex: startIndex, pageSize: pageSize, Status_Com: '>6',
                BeginTime: $("#BeginTime").val(), EndTime: $("#EndTime").val(),
                "Name": $("#key").val(), "DepartMent": $("#DepartMent").val()
            };
            if (!list_achstatis_showall) {
                parmsData["DepartMent"] = loginUser.Major_ID;
            }
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: parmsData,
                success: function (json) {
                    if (json.result.errNum == 0) {
                        $("#lbl_AllScore").html("总分：" + json.result.errMsg + "分");
                        $("#li_User").tmpl(json.result.retData.PagedData).appendTo("#tb_info");                       
                        laypage({
                            cont: 'tb_info_page', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: true, //是否开启跳页
                            skin: '#6a264b',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    BindData(obj.curr, pageSize)
                                }
                            }
                        });
                        BindSubData();
                        animation();                                            
                    } else {
                        $("#lbl_AllScore").html("总分：0分");
                        nomessage('#tb_info','li');
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function BindSubData() {
            var userNoArray=[];
            $("#tb_info li tbody").each(function(i,n){
                userNoArray.push(n.id.replace('u-',''));
            });
            if (userNoArray.length > 0) {
                var parmsData = {
                    "Func": "GetTPM_UserInfo", IsStatistic: "2", Static_RIId: "1", IsPage: false, Status_Com: '>6',
                    BeginTime: $("#BeginTime").val(), EndTime: $("#EndTime").val(),
                    "Name": $("#key").val(), "DepartMent": $("#DepartMent").val(), UserNos: userNoArray.join(',')
                };
                if (!list_achstatis_showall) {
                    parmsData["DepartMent"] = loginUser.Major_ID;
                }
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                    type: "post",
                    dataType: "json",
                    data: parmsData,
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            var alldata = json.result.retData;
                            $.each(userNoArray, function (index, value) {
                                $("#u-" + value).empty();
                                var bind_List = alldata;
                                bind_List = Enumerable.From(bind_List).Where("x=>x.UserNo=='" + value + "'").ToArray();                               
                                $("#tr_CurInfo").tmpl(bind_List).appendTo("#u-" + value);
                            });
                            tableSlide();
                        } 
                    },
                    error: function () {
                        //接口错误时需要执行的
                    }
                });
            }            
        }
        function animation() {
            $('.statis_lists').find('li:has(dd)').find('dt').click(function () {
                var $next = $(this).next('dd');
                if ($next.is(':hidden')) {
                    $(this).parent().siblings('li').removeClass('selected');
                    $(this).parent('li').addClass('active');
                    $next.show();
                    if ($(this).parent('li').siblings('li').children('dd').is(':visible')) {
                        $(this).parent("li").siblings("li").removeClass('selected');
                        $(this).parent("li").siblings("li").find("dd").hide();
                    }
                } else {
                    $(this).parent('li').removeClass('selected');
                    $next.hide();
                }
            })
        }
    </script>
</body>
</html>
