<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPerformance.aspx.cs" Inherits="FEWeb.TeaAchManage.MyPerformance" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>我的业绩</title>
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
            <td>${Score}</td>
            <td>{{if Status==0}}<span class="nosubmit">待提交</span>
                {{else Status==1}}<span class="checking1">信息待审核</span>
                {{else Status==2}}<span class="nocheck">信息不通过</span>
                {{else Status==3}}<span class="assigning">分数待分配</span>
                {{else Status==4}}<span class="nosubmit">分数待提交</span>
                {{else Status==5}}<span class="checking1">分数待审核</span>
                {{else Status==6}}<span class="nocheck">分数不通过</span>
                {{else Status==7}}<span class="assigning">审核通过</span>
                {{else Status==8}}<span class="assigning">奖金待分配</span>
                {{else Status==9}}<span class="nosubmit">奖金待提交</span>
                {{else Status==10}}<span class="checking1">奖金待审核</span>
                {{else Status==11}}<span class="nocheck">奖金不通过</span>
                {{else}} <span class="assigning">审核通过</span>
                {{/if}}
            </td>
            <td class="operate_wrap">
                {{if Status==0||Status==2}}
                    <%-- <div class="operate" onclick="OpenIFrameWindow('修改业绩信息', 'InsertReward.aspx?Id=${Id}&Type=${AchieveType}', '1110px', '700px')">--%>
                    <div class="operate" onclick="EditAchive(${Id},'${AchieveType}','${GPid}')">
                        <i class="iconfont color_purple">&#xe617;</i>
                        <span class="operate_none bg_purple">修改
                        </span>
                    </div>
                {{else}}
                     <div class="operate">
                         <i class="iconfont color_gray">&#xe617;</i>
                         <span class="operate_none bg_gray">修改
                         </span>
                     </div>
                {{/if}}
                    <div class="operate" onclick="OpenIFrameWindow('业绩查看', 'CheckAchieve.aspx?Id=${Id}&Type=View', '1000px', '700px')">
                        <i class="iconfont color_purple">&#xe60b;</i>
                        <span class="operate_none bg_purple">查看</span>
                    </div>
                {{if IsShow(Status,ResponsMan,CreateUID)}}
                   {{if Status==3||Status==4||Status==6}}
                    <div class="operate" onclick="OpenIFrameWindow('分数分配','PermanAllot.aspx?AcheiveId=${Id}&AchieveType=${AchieveType}','1000px','700px')">
                        <i class="iconfont color_purple">&#xe63d;</i>
                        <span class="operate_none bg_purple">分配</span>
                    </div>
                {{else}}
                    <div class="operate">
                        <i class="iconfont color_gray">&#xe63d;</i>
                        <span class="operate_none bg_gray">分配</span>
                    </div>
                {{/if}} 
                {{if Status==7||Status==8||Status==9||Status==11}} 
                <div class="operate" onclick="OpenIFrameWindow('奖金分配','RewardAllot.aspx?AcheiveId=${Id}&AchieveType=${AchieveType}','1000px','700px')">
                    <i class="iconfont color_purple">&#xe6c2;</i>
                    <span class="operate_none bg_purple">分配</span>
                </div>
                {{else}}
                 <div class="operate">
                     <i class="iconfont color_gray">&#xe6c2;</i>
                     <span class="operate_none bg_gray">分配</span>
                 </div>
                {{/if}}            
                {{else}}
                    <div class="operate">
                        <i class="iconfont color_gray">&#xe63d;</i>
                        <span class="operate_none bg_gray">分配</span>
                    </div>
                    <div class="operate" >
                        <i class="iconfont color_gray">&#xe6c2;</i>
                        <span class="operate_none bg_gray">分配</span>
                    </div>
                {{/if}}                
            </td>
        </tr>
    </script>
   
</head>
<body>
    <input type="hidden" id="CreateUID" value="011" />
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap">
            <div class="sort_wrap">
                <div class="sort_item">
                    <div class="search_toobar clearfix" id="div_ache_bar">
                        <div class="fl ml20">
                            <label for="">业绩类别:</label>
                            <select class="select" style="width: 198px;" id="AcheiveType" onchange="Bind_SelGInfo();"></select>
                        </div>
                        <div class="fl ml20">
                            <label for="">奖励项目：</label>
                            <select class="select" name="Gid" id="Gid" onchange="BindData(1,10);"></select>
                        </div>
                        <div class="fr" id="div_AddAward">
                            <input type="button" value="录入个人奖项" class="btn" onclick="OpenIFrameWindow('录入个人竞赛奖', 'InsertReward.aspx', '1110px', '700px')" />
                        </div>
                    </div>
                    <div class="table mt10" >
                        <table>
                            <thead>
                                <tr>
                                    <th>奖励项目</th>
                                    <th width="320px">获奖项目名称</th>
                                    <th>负责单位</th>
                                    <th>负责人</th>
                                    <th>获奖年度</th>                                    
                                    <th>个人分数</th>
                                    <th>状态</th>                                    
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody id="tb_info"></tbody>
                        </table>
                        <div id="pageBar" class="page"></div>
                    </div>
                </div>
               
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
        $(function () {
            Bind_SelAchieve();
        });
        //我的业绩，我录入的业绩
        function BindData(startIndex, pageSize) {
            $("#tb_info").empty();
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: { "Func": "GetAcheiveRewardInfoData", "MyUno": $("#CreateUID").val(), "CreateUID": '', PageIndex: startIndex, pageSize: pageSize, AchieveLevel: $("#AcheiveType").val(), Gid: $("#Gid").val()},
                success: function (json) {                   
                    if (json.result.errMsg == "success") {
                        $("#pageBar").show();
                        $("#tr_Info").tmpl(json.result.retData.PagedData).appendTo("#tb_info");                       
                        laypage({
                            cont: 'pageBar', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
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
                        $("#pageBar").hide();
                        nomessage('#tb_info');
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }
        function IsShow(Status, ResponsMan, CreateUID) {
            var flag = false;
            if (ResponsMan == $("#CreateUID").val() || CreateUID == $("#CreateUID").val()) {
                flag = true;
            }
            return flag;
        }
        function EditAchive(Id, Type, Group) {
            if (Type == "4") {
                layer.msg("此类教师业绩数据由外部导入，不允许修改");
            }
            else {
                window.location.href = 'AcheveRewadInfoEdit.aspx?itemId=' + Id + '&Id=' + UrlDate.Id + '&Iid=' + UrlDate.Iid + "&Type=" + Type + "&Group=" + Group;
            }
        }
    </script>
</body>
</html>
