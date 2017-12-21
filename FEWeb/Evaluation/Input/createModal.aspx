<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createModal.aspx.cs" Inherits="FEWeb.Evaluation.Input.createModal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/images/favicon.ico" rel="shortcut icon" />
    <title>评价录入</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" id="createInput">
            <div class="search_toobar clearfix">
                <div class="clearfix fl clearfix">
                    <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl" style="width: 130px;" v-model="key"/>
                    <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                </div>
                <div class="fr">
                    <button class="btn ml10" onclick="OpenIFrameWindow('发起评教','StartEval.aspx','900px','650px')">发起评教</button>
                    <button class="btn" onclick="window.history.go(-1);">返回上一步</button>
                </div>
            </div>
            <div class="table mt10">
                 <table>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>评价课程</th>
                            <th>被评价教师</th>
                            <th>部门</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,index) in list" v-cloak>
                            <td>{{index+1}}</td>
                            <td>{{item.course}}</td>
                            <td>{{item.teracher}}</td>
                            <td>{{item.depart}}</td>
                            <td class="operate_wrap">
                                <div class="operate" onclick="window.location.href='./selectTable.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')">
                                    <i class="iconfont color_purple">&#xe617;</i>
                                    <span class="operate_none bg_purple">录入</span>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div id="page" class="page"></div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../../js/vue.min.js"></script>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/public.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        })
        var createInput = new Vue({
            el: '#createInput',
            data: {
                list: [
                    {
                        course: '评价课程',
                        teracher: '被评价教师',
                        depart:'计算机学院'
                    }
                ],
                key:''
            },
            methods: {
                initList: function () {
                    var that = this;
                }
            },
            mounted: function () {
                var that = this;
                tableSlide();
            }
        })
       
    </script>
</body>
</html>
