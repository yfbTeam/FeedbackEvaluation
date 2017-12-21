<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcheveRewadSearch.aspx.cs" Inherits="FEWeb.TeaAchManage.AcheveRewadSearch" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>业绩查询</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/x-jquery-tmpl" id="tr_Info">
        <tr>
            <td>${GidName}</td>
            <td>${AchiveName}</td>
            <td>${Major_Name}</td>
            <td>${ResponsName}</td>            
            <td>${Year}</td>
            <td>${CreateName}</td>
            <td>${DateTimeConvert(CreateTime,"yyyy-MM-dd")}</td>
            <td class="operate_wrap">
                <div class="operate" onclick="OpenIFrameWindow('业绩查看', 'CheckAchieve.aspx?Id=${Id}&Type=View', '1000px', '700px')">
                    <i class="iconfont color_purple">&#xe60b;</i>
                    <span class="operate_none bg_purple">查看
                    </span>
                </div>
                {{if list_achsearch_operall==true}}
                <div class="operate" onclick="OpenIFrameWindow('业绩修改', 'AdminAchieveEdit.aspx?AcheiveId=${Id}', '1000px', '700px')">
                    <i class="iconfont color_purple">&#xe602;</i>
                    <span class="operate_none bg_purple">修改
                    </span>
                </div>
                {{else list_achsearch_operdepart==true}}
                   {{if MajorCount>0}}
                    <div class="operate" onclick="OpenIFrameWindow('业绩修改', 'AdminAchieveEdit.aspx?AcheiveId=${Id}', '1000px', '700px')">
                        <i class="iconfont color_purple">&#xe602;</i>
                        <span class="operate_none bg_purple">修改
                        </span>
                    </div>
                {{else}}
                    <div class="operate">
                        <i class="iconfont color_gray">&#xe602;</i>
                        <span class="operate_none bg_gray">修改
                        </span>
                    </div>
                {{/if}}
                {{/if}}  
            </td>
        </tr>
    </script>
</head>
<body>
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap">
            <div class="search_toobar clearfix">
                <div class="fl ml20">
                    <label for="">业绩类别:</label>
                    <select class="select" style="width: 198px;" id="AcheiveType" onchange="Bind_SelGInfo();"></select>
                </div>
                <div class="fl ml20">
                    <label for="">奖励项目：</label>
                    <select class="select" name="Gid" id="Gid" onchange="BindData(1,10);"></select>
                </div>
                <div class="fl ml20">
                    <input type="text" name="key" id="key" placeholder="请输入负责人关键字" value="" class="text fl" style="width: 150px;">
                    <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                </div>
            </div>
            <div class="table mt10">
                <table>
                    <thead>
                        <tr>
                            <th>奖励项目</th>
                            <th>获奖项目名称</th>
                            <th>负责单位</th>
                            <th>负责人</th>                         
                            <th>获奖年度</th>
                            <th>录入人</th>
                            <th>录入时间</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tb_info">
                    </tbody>
                </table>
                <div class="page" id="class_table_wrap"></div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/laypage/laypage.js"></script>
    <script src="BaseUse.js"></script>
    <script>
        $(function () {
            $("#CreateUID").val(GetLoginUser().UniqueNo);
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        });
    </script>
    <script>
        var UrlDate = new GetUrlDate();
        var list_achsearch_showall = false, list_achsearch_operall = false, list_achsearch_operdepart = false;//查看-全校范围,查看、编辑-全校范围,查看、编辑-院系范围
        var loginUser = GetLoginUser();
        $(function () {
            Get_PageBtn("/TeaAchManage/AcheveRewadSearch.aspx");
            list_achsearch_showall = JudgeBtn_IsExist("list_achsearch_showall")
             , list_achsearch_operall = JudgeBtn_IsExist("list_achsearch_operall")
             , list_achsearch_operdepart = JudgeBtn_IsExist("list_achsearch_operdepart");
            Bind_SelAchieve();
            navTab('.sort_nav', '.table');
        });        
        var resonname = "";
        function search() {
            resonname = $("#key").val().trim();
            BindData(1, 10);
        }
        function BindData(startIndex, pageSize) {
            $("#tb_info").empty();
            var parmsData = { "Func": "GetAcheiveRewardInfoData", PageIndex: startIndex, pageSize: pageSize, AchieveLevel: $("#AcheiveType").val(), Gid: $("#Gid").val(), "ResponName": resonname, Status_Com: '>2' };
            if (list_achsearch_operdepart && !list_achsearch_operall) { parmsData["LoginMajor_ID"] = loginUser.Major_ID; }
            if (!list_achsearch_showall && !list_achsearch_operall) { parmsData["Major_ID"] = loginUser.Major_ID; }
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: parmsData,
                success: function (json) {
                    if (json.result.errMsg == "success") {                       
                        $("#tr_Info").tmpl(json.result.retData.PagedData).appendTo("#tb_info");
                        laypage({
                            cont: 'class_table_wrap', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
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
                       
                        tableSlide();
                    } else {
                        nomessage('#tb_info');
                    }
                },
                error: function () {}
            });
        }
    </script>
</body>
</html>

