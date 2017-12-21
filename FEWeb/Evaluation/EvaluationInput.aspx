<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluationInput.aspx.cs" Inherits="FEWeb.Evaluation.EvaluationInput" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>评价录入</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .regularEval{
            color:#179720;
        }
        .selfEval{
            color:#B25F83;
        }
    </style>
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" id="EvaluationInput">
             <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>
                    <select class="select" id="section"  style="width:198px;" v-model="options.section">
                         <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml10" >
                    <label for="">部门:</label>
                    <select class="select" id="DepartMent" style="width:148px;" v-model="options.department">
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
                 <div class="fr pr">
                     <button class="btn" onclick="window.location.href='./Input/createModal.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')">评价任务</button>
                     <b class="dian"></b>
                 </div>
            </div>
             <div class="table">
                <table>
                    <thead>
                        <tr>
                            <th width="40px">序号</th>
                            <th>学年学期</th>
                            <th>教师</th>
                            <th>课程</th>
                            <th>部门</th>
                            <th>评价表名称</th>
                            <th>状态</th>
                            <th>得分</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tb_RegEval">
                        <tr v-for="(item,index) in list" v-cloak>
                            <td >{{index+1}}</td>
                            <td>{{item.section}}</td>
                            <td>{{item.evelPerson}}</td>
                            <td>{{item.courseName}}</td>
                            <td>{{item.className}}</td>
                            <td>{{item.evelTable}}</td>
                            <td>
                                <span v-if="item.status==1" class="nosubmit">未提交</span>
                                <span v-else-if="item.status==2" class="checking1">待审核</span>
                                <span v-else-if="item.status==3" class="pass">入库</span>
                            </td>
                            <td>{{item.score}}</td>
                            <td class="operate_wrap">
                                <div class="operate" onclick="window.location.href='EvalDetail.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')">
                                    <i class="iconfont color_purple">&#xe60b;</i>
                                    <span class="operate_none bg_purple">查看</span>
                                </div>
                                <div class="operate" onclick="window.location.href='./Input/EvalTable.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')">
                                    <i class="iconfont color_purple">&#xe617;</i>
                                    <span class="operate_none bg_purple">评价</span>
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
   
        var EvaluationInput = new Vue({
            el: '#EvaluationInput',
            data: {
                list: [
                    {
                        section: '2017年第一学期',
                        evelPerson: '张过分',
                        courseName: '计算机基础',
                        className: '计12-11',
                        evelTable: "课堂教学评价表",
                        status:1,
                        score: '60'
                    },
                    {
                        section: '2017年第一学期',
                        evelPerson: '张过分',
                        courseName: '计算机基础',
                        className: '计12-11',
                        evelTable: "课堂教学评价表",
                        status: 2,
                        score: '60'
                    },
                    {
                        section: '2017年第一学期',
                        evelPerson: '张过分',
                        courseName: '计算机基础',
                        className: '计12-11',
                        evelTable: "课堂教学评价表",
                        status: 3,
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
            mounted:function(){
                var that = this;
                tableSlide();
                that.initList();
            }
        })
       
    </script>
</body>
</html>
