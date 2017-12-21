<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskAllot.aspx.cs" Inherits="FEWeb.Evaluation.TaskAllot" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
   
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" id="TaskAllot">
             <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>
                    <select class="select" id="section"  style="width:198px;" v-model="options.section">
                         <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml10">
                    <label for="">部门:</label>
                    <select class="select" style="width:148px;" v-model="options.department" id="DepartMent">
                         <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml10">
                    <label for="">课程:</label>
                    <select class="select" style="width:148px;" v-model="options.course">
                         
                    </select>
                </div>
                 <div class="fl ml10">
                    <label for="">评价表:</label>
                    <select class="select" style="width:188px;" v-model="options.evaltable">
                         
                    </select>
                </div>
                 <div class="fl ml10" >
                     <input type="text" name="key" id="key" placeholder="请输入教师姓名" value="" class="text fl" style="width: 130px;" v-model="options.key">
                     <a href="javascript:;" class="search fl"><i class="iconfont">&#xe600;</i></a>
                 </div>
            </div>
             <div class="table">
                 <table>
                    <thead>
                        <tr>
                            <th width="40px">序号</th>
                            <th>学年学期</th>
                            <th>部门</th>
                            <th>教师</th>
                            <th>课程</th>
                            <th>班级</th>
                            <th>评价表名称</th>
                            <th>评价人</th>
                            <th>得分</th>
                             <th>状态</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tb_RegEval">
                        <tr v-for="(item,index) in list" v-cloak>
                            <td >{{index+1}}</td>
                            <td>{{item.section}}</td>
                            <td>{{item.department}}</td>
                            <td>{{item.teacer}}</td>
                            <td>{{item.courseName}}</td>
                            <td>{{item.className}}</td>
                            <td>{{item.evelTable}}</td>
                            <td>{{item.evelPeople}}</td>
                            <td>{{item.score}}</td>
                            <td>
                                <span class="checking1">待审核</span>
                            </td>
                            <td class="operate_wrap">
                                <div class="operate" onclick="OpenIFrameWindow('评价审核','./check/CheckModal.aspx','800px','600px')">
                                    <i class="iconfont color_purple">&#xe624;</i>
                                    <span class="operate_none bg_purple">审核</span>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="page" id="page"></div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../js/vue.min.js"></script>
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
            Base.BindDepart();
        })
        
    
        var TaskAllot = new Vue({
            el: '#TaskAllot',
            data: {
                list: [
                    {
                        section: '2017年第一学期',
                        department: '计算机基础',
                        teacer: '张过分',
                        courseName: '计算机基础',
                        className: '计12-11',
                        evelTable: "课堂教学评价表",
                        evelPeople:"刘安",
                        score: '60'
                    }
                ],
                options: {
                    section: "",
                    department: "",
                    course: "",
                    evaltable: "",
                    key: "",
                },
            },
            methods: {
                initList() {
                    var that = this;
                }
            },
            mounted: function () {
                var that = this;
                tableSlide();
                that.initList();
            }
        })
    </script>
</body>
</html>