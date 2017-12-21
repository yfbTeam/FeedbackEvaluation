<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllotTable.aspx.cs" Inherits="FEWeb.SysSettings.AllotTable" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>临时评价任务</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .allot_left{
            width:220px;
        }
        .table table thead tr,.table table tbody tr{border:1px solid #ccc;}
         .table table thead tr th:first-child,.table table tbody tr td:first-child{border-right:1px solid #ccc;}
         .table table tbody tr td{border-bottom:none;}
         .allot_center{
             width:550px;
         }
         .allot_right{
             width:370px;
         }
         .table{
             border:none;
         }
    </style>
</head>
<body >
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
			<div class="sort_nav"  id="threenav">
                   
            </div>
            <div class="sortwrap clearfix">
                <div class="allot_left fl">
                     <div class="search_toobar clearfix">
                        <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl" style="width: 130px;">
                        <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                      </div>
                    <div class="table">
                        <table>
                            <thead>
                                <tr>
                                    <th width="40px;">
                                        <input type="checkbox" name="name" value=" " /></th>
                                    <th>专家姓名</th>
                                </tr>
                            </thead> 
                            <tbody>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>李丽华</td>
                                </tr>
                                 <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>李丽华</td>
                                </tr>
                                 <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>李丽华</td>
                                </tr>
                                 <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>李丽华</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="allot_center ml10 fl">
                    <div class="search_toobar clearfix">
                        <div class="fl">
                             <div class="fl">
                            <label for="">所属学院:</label>

                            <select class="select" style="width:178px;">
                                <option value="">全部</option>
                            </select>
                        </div>
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
                                    <th width="40px;">
                                        <input type="checkbox" name="name" value=" " /></th>
                                    <th>课程编码</th>
                                    <th>所属学院</th>
                                    <th>课程名称</th>
                                    <th>热咳教师</th>
                                </tr>
                            </thead> 
                            <tbody>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td>7000101</td>
                                    <td>计算机学院</td>
                                    <td>城市发展建设</td>
                                    <td>李丽华</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="allot_right fr">
                    <div class="search_toobar clearfix">
                        <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl" style="width: 130px;">
                        <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                      </div>
                    <div class="table">
                        <table>
                            <thead>
                                <tr>
                                    <th width="40px;">
                                        <input type="checkbox" name="name" value=" " /></th>
                                    <th>评价表名称</th>
                                </tr>
                            </thead> 
                            <tbody>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td><span class="textoverflow">北方工业大学评价表</span></td>
                                </tr>
                                 <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td><span class="textoverflow">北方工业大学评价表</span></td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td><span class="textoverflow">北方工业大学评价表</span></td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td><span class="textoverflow">北方工业大学评价表</span></td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td><span class="textoverflow">北方工业大学评价表</span></td>
                                </tr>
                                <tr>
                                    <td><input type="checkbox" name="name" value=" " /></td>
                                    <td><span class="textoverflow">北方工业大学评价表</span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/public.js"></script>
    
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
    </script>
</body>
</html>
