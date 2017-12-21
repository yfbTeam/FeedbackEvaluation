<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcheveExport.aspx.cs" Inherits="FEWeb.TeaAchManage.AcheveExport" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>新增教师业绩</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />

    <link href="../Scripts/choosen/chosen.css" rel="stylesheet" />
    <link href="../Scripts/choosen/prism.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>


    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
    </script>
</head>
<body>
    <input type="hidden" name="Func" value="AddAcheiveRewardInfoData" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="Status" id="Status" value="0" />

    <input type="hidden" id="Group" name="Group" />
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" style="padding-bottom: 0px;">
            <h1 class="title">
                <a onclick="javascript:window.history.go(-1)" style="cursor: pointer;">业绩录入</a><span>&gt;</span><a href="#" class="crumbs" id="GropName">教师指导各类竞赛及学生科技活动获奖类</a>
            </h1>
            <div class="cont">

                <h2 class="cont_title book"><span>获奖信息</span></h2>
                <div class="table">
                    <div class="clearfix mt10">
                        <input type="button" value="导入数据" onclick="Export()" class="btn fr" />
                    </div>
                    

                    <table class="mt10" style="border:1px solid #ccc;">
                        <thead>
                            <tr>
                                <th>项目</th>
                                <th>获奖级别</th>
                                <th>获奖等级</th>
                                <th>获奖教师</th>
                                <th>单位/部门</th>
                                <th>获奖日期</th>
                                <th>得分</th>
                            </tr>
                        </thead>
                        <tbody id="tb_info">
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>

    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../Scripts/choosen/prism.js"></script>
    <script src="../Scripts/Uploadify/uploadify/jquery.uploadify-3.1.min.js"></script>
    <script src="BaseUse.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script>
        var UrlDate = new GetUrlDate();

        $(function () {
            $("#Group").val(UrlDate.Group);
        })
        function Export()
        {
            layer.msg("暂无数据源");
        }
    </script>
</body>
</html>
