<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectTable.aspx.cs" Inherits="FEWeb.Evaluation.Input.selectTable" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>选择评价表</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
   
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            <h1 class="title">
                <a href="javascript:window.history.go(-1)">全部评价</a><span>&gt;</span><a href="#" class="crumbs" id="GropName">北方工业大学实验教学课堂检查表（专家用表）</a>
            </h1>
            <div class="tableheader">
                <h1 class="tablename">
                    <select v-model="" class="tableheader_select">
                        <option value="0">北方工业大学实验教学课堂检查表</option>
                    </select>
                </h1>
                <div class="evalmes">
                    <span>学院：计算机学院</span>
                    <span>课程名称：实验教学</span>
                    <span>实验名称：<input type="text" value=""/></span>
                    <span>实验指导教师：张力和</span>
                    <span>实验时间：<input type="text" value=""/></span>
                    <span>学生班级：<input type="text" value=""/></span>
                    <span>检查人：张力和</span>
                    <span>检查时间：<input type="text" value=""/></span>
                </div>
            </div>
            <div>
                <ul class="details_lists">
                    <li>
                        <dl>
                            <dt>实验条件</dt>
                            <dd>
                                <div class="ti">
                                    <h2 class="title1">1、不迟到，不早退，课时饱满。<span>(<b>20</b>分)</span></h2>
                                    <div class="test_desc clearfix">
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="radio" name="" id="a">
                                            <label for="a">A、优</label>
                                        </span>
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="radio" name="" id="b">
                                            <label for="b">A、优</label>
                                        </span>
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="radio" name="" id="c">
                                            <label for="c">A、优</label>
                                        </span>
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="radio" name="" id="d">
                                            <label for="d">A、优</label>
                                        </span>
                                    </div>
                                </div>
                                <div class="ti">
                                    <h2 class="title1">1、不迟到，不早退，课时饱满。<span>(<b>20</b>分)</span></h2>
                                    <div class="test_desc clearfix">
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="checkbox" name="" id="a">
                                            <label for="a">A、优</label>
                                        </span>
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="checkbox" name="" id="b">
                                            <label for="b">A、优</label>
                                        </span>
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="checkbox" name="" id="c">
                                            <label for="c">A、优</label>
                                        </span>
                                        <span class="fl" style="margin-right: 30px;">
                                            <input type="checkbox" name="" id="d">
                                            <label for="d">A、优</label>
                                        </span>
                                    </div>
                                </div>
                                <div class="ti">
                                    <h2 class="title1">1、不迟到，不早退，课时饱满。<span>(<b>20</b>分)</span></h2>
                                    <div class="test_desc clearfix">
                                       <textarea></textarea>
                                    </div>
                                </div>
                            </dd>
                        </dl>
                    </li>
                </ul>
            </div>
            <div class="btnwrap" style="">
                <input type="button" value="保存" onclick="Save()" class="btn">
                <input type="button" value="取消" class="btna ml10 n_uploadbtn" onclick="window.history.go(-1)">
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/public.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
    </script>
</body>
</html>