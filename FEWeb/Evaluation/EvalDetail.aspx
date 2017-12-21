<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalDetail.aspx.cs" Inherits="FEWeb.Evaluation.EvalDetail" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>评价统计</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
             <h1 class="title">
                <a href="javascript:window.history.go(-1)">全部评价</a><span>&gt;</span><a href="#" class="crumbs" id="GropName">北方工业大学实验教学课堂检查表（专家用表）</a>
            </h1>
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
                    <div class="fr clearfix">
                        
                        <div class="tableheader_input fl ml20">
                            <label for="">分数：60分</label>
                        </div>
                    </div>
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
                                    <h2 class="title1">1、不迟到，不早退，课时饱满。（3分）<span class="fr">20分</span></h2>
                                    <div class="test_desc clearfix">
                                        
                                    </div>
                                </div>
                            </dd>
                        </dl>
                    </li>
                </ul>
            </div>
            
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
    </script>
</body>
</html>
