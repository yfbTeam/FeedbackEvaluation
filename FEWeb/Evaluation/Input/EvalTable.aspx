<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalTable.aspx.cs" Inherits="FEWeb.Evaluation.Input.EvalTable" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>评价表</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
   
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            <div style="width: 1160px; position: fixed; z-index: 99; background: #fff; margin-top: -10px; padding: 10px 0px;">
                <div class="crumbs">
                    <a href="javascript:window.location.go(-1)" >全部评价</a>
                    <span>&gt;</span>
                    <a href="javascript:;"  id="couse_name">北方工业大学实验教学课堂检查表</a>
                </div>
            </div>
            <div class="detail_left fl" style="margin-top:40px;">
                 <div class="tableheader">
                    <h1 class="tablename">
                        北方工业大学实验教学课堂检查表
                    </h1>
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
            </div>
           <div class="detail_right">
               <div class="totalscore clearfix">
                    <div class="totalscore_left fl">
                        <div style="border-right: 1px dashed #fff;">
                            <p>总分：</p>
                        </div>
                    </div>
                    <div class="totalscore_left fl">
                        <h3 id="sp_total1">92.60分</h3>
                    </div>
                </div>
               <div class="answer_detail">
                   <p class="answer_type">教学态度<span>（ 4题 ）</span></p>
                   <ul class="answer_number clearfix">
                       <li class="on">
                           <a href="javsacript:;">1</a>
                       </li>
                   </ul>
               </div>
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
