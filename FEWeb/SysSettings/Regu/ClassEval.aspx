<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassEval.aspx.cs" Inherits="FEWeb.SysSettings.ClassEval" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
             <div class="sort_nav" id="threenav">
            </div>
             <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>
                    <select class="select" id="section">
                         <option value="">全部</option>
                    </select>
                </div>
                <div class="fl ml10">
                    <input type="text" name="" id="class_key" placeholder="请输入关键字" value="" class="text fl">
                    <a class="search fl" href="javascript:;" ><i class="iconfont">&#xe600;</i></a>
                </div>
                <div class="fr">
                    <input type="button" name="" id=""  value="新增评价"  class="btn" onclick="OpenIFrameWindow('新增评价','CreateModel.aspx','545px','400px')">
                </div>
            </div>
            <div class="table mt10">
                <table>
                    <thead>
                        <tr>
                            <th>评价名称</th>
                            <th>学年学期</th>
                            <th>创建人</th>
                            <th>开始时间</th>
                            <th>截止时间</th>
                            <th>状态</th>
                            <th>评价表</th>
                            <th>创建时间</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>评价名称</td>
                            <td>学年学期</td>
                            <td>创建人</td>
                            <td>开始时间</td>
                            <td>截止时间</td>
                            <td>状态</td>
                            <td>评价表</td>
                            <td>创建时间</td>
                            <td class="operate_wrap">
                                <div class="operate" onclick="OpenIFrameWindow('编辑评价','CreateModel.aspx','545px','400px')">
                                    <i class="iconfont color_purple">&#xe602;</i>
                                    <span class="operate_none bg_purple">编辑</span>
                                </div>
                                 <div class="operate">
                                    <i class="iconfont color_purple">&#xe61b;</i>
                                    <span class="operate_none bg_purple">删除</span>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/public.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
     <script src="../../Scripts/WebCenter/Base.js"></script>
    <script src="../../Scripts/linq.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            Base.bindStudySection();
        })
    </script>
</body>
</html>
