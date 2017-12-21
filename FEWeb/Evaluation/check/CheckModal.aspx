<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckModal.aspx.cs" Inherits="FEWeb.Evaluation.check.CheckModal" %>
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
<body style="background:#fff">
    <div class="main" >
         <div class="tableheader">
            <h1 class="tablename" id="tablename">北方工业大学实验教学课堂检查表（专家用表）</h1>
            <div class="evalmes clearfix">
                <span id="">学院：计算机学院</span>
                <span id="">课程名称：实验教学</span>
                <span id="">实验名称：XXXXXXXXXXXX</span>
                <span id="">实验地点：XXXXXX</span>
                <span id="">实验指导教师：张力和</span>
                <span id="">实验时间:2016-11-12</span>
                <span id="">学生班级:111班</span>
                <span id="">检查人：张恨水</span>
                <span id="">检查时间：2016-12-11</span>
            </div>
        </div>
        <div class="mt10">
                <ul class="details_lists details_lists1">
                <li>
                    <dl>
                        <dt>实验条件</dt>
                        <dd>
                            <div class="ti" style="padding:0;">
                                <h2 class="title1">1、不迟到，不早退，课时饱满。<span class="fr"></span></h2>
                                <div class="test_desc clearfix">
                                    <div class="detail_con">
                                        不迟到，不早退，课时饱满 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。 不迟到，不早退，课时饱满。
                                        <span>2017-12-02</span>
                                    </div>
                                </div>
                            </div>
                                <div class="ti" style="padding:0;">
                                <h2 class="title1">1、不迟到，不早退，课时饱满。<span class="fr">20分</span></h2>
                                <div class="test_desc clearfix">
                                        
                                </div>
                            </div>
                        </dd>
                    </dl>
                </li>
            </ul>
        </div>
    </div>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/public.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <div class="btnwrap">
        <input type="button" value="入库" onclick="submit()" class="btn">
        <input type="button" value="不入库" onclick="parent.CloseIFrameWindow()" class="btn ml10">
    </div>
</body>
</html>
