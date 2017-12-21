<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalDetail_judge.aspx.cs" Inherits="FEWeb.Evaluation.EvalDetail_judge" %>

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
                <a href="EvluationInput.aspx">全部评价</a><span>&gt;</span><a href="#" class="crumbs" id="GropName">北方工业大学实验教学课堂检查表（专家用表）</a>
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
                        <div class="tableheader_input fl">
                            <label for="">当前评价：</label>
                            <select>
                                <option value="0">2017-01-11 12:10</option>
                            </select>
                        </div>
                        <div class="tableheader_input fl ml20">
                            <label for="">分数：60分</label>
                        </div>
                    </div>
                </div>
            </div>
            <ul class="detail_lists">
                <li>
                    <dt>实验条件</dt>
                    <dd>
                        <div class="detail_row">
                            <p>
                                <span class="ti_name fl">
                                    1. 实验室布局合理，设施配备达标，环境安全。（3分）
                                </span>
                                <span class="fr ti_score">
                                    5分
                                </span>
                            </p>
                            <span class="detail_con">
                                话的生动：惟妙惟肖 津津有味 绘声绘色 娓娓动听 妙语连珠 滔滔不绝 余音袅袅 活神活现出口成章 行云流水。内容的精彩：引人入胜 如痴如醉 思绪万千 身临其境 字字珠玑 一字千金 精彩绝伦 扣人心弦 韵味无穷 精彩纷呈 跌宕起伏 纷繁复杂 一波三折 文不加点 回肠荡气 文采飞扬 妙趣横生 辞采华美。<span class="fr">提交时间：2017-02-20</span>
                            </span>
                        </div>
                    </dd>
                </li>
                <li>
                    <dt>实验条件</dt>
                    <dd>
                        <div class="detail_row">
                            <p>
                                <span class="ti_name fl">
                                    1. 实验室布局合理，设施配备达标，环境安全。（3分）
                                </span>
                                <span class="fr ti_score">
                                    5分
                                </span>
                            </p>
                        </div>
                        <div class="detail_row">
                            <p>
                                <span class="ti_name fl">
                                    1. 实验室布局合理，设施配备达标，环境安全。（3分）
                                </span>
                                <span class="fr ti_score">
                                    5分
                                </span>
                            </p>
                        </div>
                    </dd>
                </li>
            </ul>    
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
