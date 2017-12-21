<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllotTask.aspx.cs" Inherits="FEWeb.SysSettings.AllotTask" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>用户管理</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/layout.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .email_right .scroll-pane {
            width: 253px;
            height: 405px;
            overflow: auto;
            outline: none;
        }

        .select_expertdiv {
            height: 400px;
            overflow: auto;
        }

        .linkman_lists li {
            padding: 10px 15px;
            font-size: 14px;
            text-align: center;
            color: #333;
            cursor: pointer;
            /*border-top: 1px solid #dcdcdc;*/
            border-bottom: 1px solid #dcdcdc;
        }

        .search_toobar .text {
            height: 31px;
        }

        .linkman_lists li:hover, .linkman_lists li.selected {
            background: #fff;
            border-left: 2px solid #ffac32;
        }

        .scroll-pane {
            border-top: 1px solid #dcdcdc;
        }
    </style>

</head>
<body>

    <div class="main clearfix">
        <div class="email_right fl">
            <h1>专家列表</h1>
            <div class="searchwrap">
                <input type="text" name="name" id="key1" value="" placeholder="查找专家" />
                <a class="search fl" href="javascript:search1();"><i class="iconfont">&#xe600;</i></a>
            </div>
            <div class="scroll-pane">
                <ul class="linkman_lists" id="experts" style="height: 400px; overflow-y: auto">
                    <%--  <li class="selected">（兼）李华东</li>--%>
                </ul>
            </div>
        </div>
        <div class="InitiateEval fr" style="width: 885px;">
            <h2 class="navEval">选择评教老师
                <div class="search_toobar clearfix fr">
                    <div class="fl ml10">
                        <label for="">所属部门:</label>
                        <select class="select" id="college">
                            <option value="0">全部</option>
                        </select>
                    </div>
                    <div class="fl ml20">
                        <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl">
                        <a class="search fl" href="javascript:search();" style="background: #fff;"><i class="iconfont">&#xe600;</i></a>
                    </div>
                </div>
            </h2>
            <div class="select_expertdiv">
                <ul class="select_expert clearfix" id="teachers">
                </ul>
            </div>
            <h2 class="navEval mt20">已选择</h2>
            <div class="select_expertdiv" style="overflow-y: auto; height: 80px; min-height: 80px;">
                <ul id="selected_course" class="slectd">
                    <li>于秀玲 【有课程】<i class="iconfont">&#xe672;</i></li>
                    <li id="20040005">王明哲 【有课程】<i class="iconfont">&#xe672;</i></li>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
<script src="../../Scripts/Common.js"></script>
<script src="../../scripts/public.js"></script>
<script src="../../Scripts/linq.min.js"></script>
<script src="../../Scripts/layer/layer.js"></script>
<script src="../../Scripts/jquery.tmpl.js"></script>
<script src="../../Scripts/WebCenter/AllotTask.js"></script>
<script src="../../Scripts/WebCenter/AllotPeople.js"></script>
<script type="text/x-jquery-tmpl" id="item_Expert">
    <li id="${UniqueNo}">${Name}
    </li>
</script>
<%--开课单位--%>
<script type="text/x-jquery-tmpl" id="item_College">
    <option value="${Id}">${College_Name}</option>
</script>

<script type="text/x-jquery-tmpl" id="item_Teachers">
    <li>
        <span>${Teacher_Name}</span>
        <ul>
            <li>
                <span>计算机基础</span>

            </li>
            <li>
                <span>计算机基础</span>
            </li>
        </ul>
    </li>
</script>
<script>
    $(function () {       
        UI_Allot.PageType = 'AllotTask';
        UI_Allot.GetProfessInfo();//获取院系

        PageType = 'AllotTask';
        PrepareInit();//初始化
        GetUserByType('16,17');//获取专家     
        GetTeacherInfo_Course_Cls();//获取教师
    })
</script>
