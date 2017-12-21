<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detailModal.aspx.cs" Inherits="FEWeb.Evaluation.CourseEvalSee.detailModal" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>评价统计</title>
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/layout.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
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
                 <ul class="details_lists">
                    <li>
                        <dl>
                            <dt>实验条件</dt>
                            <dd>
                                <table class="allot_table mt10">
                                    <thead>
                                        <tr>
                                            <th>调查项目</th>
                                            <th width="5%">A</th>
                                            <th  width="5%">B</th>
                                            <th  width="5%">C</th>
                                            <th  width="5%">D</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="tl">1.按时上下课</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                        </tr>
                                        <tr>
                                            <td class="tl">1.按时上下课</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                        </tr>
                                        <tr>
                                            <td class="tl">1.按时上下课</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                            <td>12人</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <ul class="objective_lists">
                                    <li>
                                        <dt style="border:none;" class="clearfix">
                                            <div class="objective_name fl">7.希望与要求</div>
                                            <div class="fl pagebar" id="page_top"></div>
                                            <i class="toggle iconfont">&#xe643;</i>
                                        </dt>
                                        <dd>
                                            <div class="lists_row">
                                                <span>11111111111111111111111111111<b class="fr">2017-08-12</b></span>
                                            </div>
                                            <div class="lists_row">
                                                <span>11111111111111111111111111111<b class="fr">2017-08-12</b></span>
                                            </div>
                                            <div class="pagebar" id="page_bottom"></div>
                                        </dd>
                                    </li>
                                </ul>
                            </dd>
                        </dl>
                    </li>
                </ul>
            </div>
            
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/public.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            animate();
        })
        function animate() {
            $('.objective_lists').find('li:has(dt)').find('dt').click(function () {
                var $next = $(this).next('dd');
                if ($next.is(':hidden')) {
                    $(this).parent().siblings('li').removeClass('active');
                    $(this).parent('li').addClass('active');
                    $next.stop().slideDown();

                    if ($(this).parent('li').siblings('li').children('dd').is(':visible')) {
                        $(this).parent("li").siblings("li").removeClass('active');
                        $(this).parent("li").siblings("li").find("dd").slideUp();
                    }

                } else {
                    $(this).parent('li').removeClass('active');
                    $next.stop().slideUp();
                }

            })
        }
    </script>
</body>
</html>
