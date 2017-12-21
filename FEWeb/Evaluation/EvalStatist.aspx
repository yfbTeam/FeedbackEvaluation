<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalStatist.aspx.cs" Inherits="FEWeb.Evaluation.EvalStatist" %>

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
            <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>

                    <select class="select" style="width: 198px;" id="section">
                        <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml20">
                    <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl" style="width: 130px;">
                    <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                </div>
            </div>
            <div class="table">
                <table>
                    <thead>
                        <tr>
                            <th>学院名称</th>
                            <th>听课门次</th>
                            <th>好≥90</th>
                            <th>90>较好≥80</th>
                            <th>80>一般≥60</th>
                            <th>差<60</th>
                        </tr>
                    </thead>
                    <tbody id="tb_RegEval">
                        <tr>
                            <td>电气与控制工程学院</td>
                            <td>90</td>
                            <td>60</td>
                            <td>20</td>
                            <td>8</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>电气与控制工程学院</td>
                            <td>90</td>
                            <td>60</td>
                            <td>20</td>
                            <td>8</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>电气与控制工程学院</td>
                            <td>90</td>
                            <td>60</td>
                            <td>20</td>
                            <td>8</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>电气与控制工程学院</td>
                            <td>90</td>
                            <td>60</td>
                            <td>20</td>
                            <td>8</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>电气与控制工程学院</td>
                            <td>90</td>
                            <td>60</td>
                            <td>20</td>
                            <td>8</td>
                            <td>2</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
     <script src="../Scripts/WebCenter/Base.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            Base.bindStudySection();
        })
    </script>
</body>
</html>
