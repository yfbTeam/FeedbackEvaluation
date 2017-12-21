<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartEval.aspx.cs" Inherits="FEWeb.Evaluation.Input.StartEval" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>评价统计</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
</head>
<body>
    <div class="main">
        <div class="InitiateEval">
            <h2 class="navEval">选择评教老师
                <div class="search_toobar clearfix fr">
                    <div class="fl ml10">
                        <label for="">所属部门:</label>
                        <select class="select" id="DepartMent">
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
    <div class="btnwrap">
        <input type="button" value="确定" onclick="submit()" class="btn">
        <input type="button" value="取消" onclick="parent.CloseIFrameWindow()" class="btna ml10">
    </div>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/public.js"></script>
     <script src="../../Scripts/WebCenter/Base.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <script>
        $(function () {
            Base.BindDepart();
        })
    </script>
</body>
</html>
