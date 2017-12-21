<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FEWeb.Evaluation.ExpertEvalSee.index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>专家评价查看</title>
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/layout.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" id="expertEvalSee">
             <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>
                    <select class="select" id="section"  style="width:198px;" v-model="options.section">
                         <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml10" v-if="role==1" v-cloak>
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
                 <div class="fl ml10" v-if="role==1" v-cloak>
                     <input type="text" name="key" id="key" placeholder="请输入教师姓名" value="" class="text fl" style="width: 130px;" v-model="options.key">
                     <a href="javascript:;" class="search fl"><i class="iconfont">&#xe600;</i></a>
                 </div>
            </div>
            <div class="table mt10">
                <div v-if="role==1" v-cloak>
                   <table>
                        <thead>
                            <tr>
                                <th>序号</th>
                                <th>学年学期</th>
                                <th>评价人</th>
                                <th>课程名称</th>
                                <th>班级</th>
                                <th>教师</th>
                                <th>评价表</th>
                                <th>评价时间</th>
                                <th>得分</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item,index) in list" >
                                <td>{{index+1}}</td>
                                <td>{{item.section}}</td>
                                <td>{{item.evelPerson}}</td>
                                <td>{{item.courseName}}</td>
                                <td>{{item.className}}</td>
                                <td>{{item.teacher}}</td>
                                <td>{{item.evelTable}}</td>
                                <td>{{item.evalTime}}</td>
                                <td>{{item.score}}</td>
                                <td class="operate_wrap">
                                    <div class="operate" onclick="location.href='detailModal.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')+''">
                                        <i class="iconfont color_purple">&#xe60b;</i>
                                        <span class="operate_none bg_purple">查看</span>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div id="pageBar1" class="page"></div>
                </div>
                <div v-if="role==3" v-cloak>
                    <table>
                        <thead>
                            <tr>
                                <th>序号</th>
                                <th>学年学期</th>
                                <th>课程名称</th>
                                <th>班级</th>
                                <th>评价表</th>
                                <th>评价时间</th>
                                <th>得分</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item,index) in list">
                                <td>{{index+1}}</td>
                                <td>{{item.section}}</td>
                                <td>{{item.courseName}}</td>
                                <td>{{item.className}}</td>
                                <td>{{item.evelTable}}</td>
                                <td>{{item.evalTime}}</td>
                                <td>{{item.score}}</td>
                                <td class="operate_wrap">
                                    <div class="operate" onclick="location.href='detailModal.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')+''">
                                        <i class="iconfont color_purple">&#xe60b;</i>
                                        <span class="operate_none bg_purple">查看</span>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div id="pageBar2" class="page"></div>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="/js/vue.min.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/public.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
     <script src="/Scripts/WebCenter/Base.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/linq.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            Base.bindStudySection();
            Base.BindDepart();
            tableSlide();
        })
        var expertEvalSee = new Vue({
            el: '#expertEvalSee',
            data:{
                role: "",
                options:{
                    section: "",
                    department: "",
                    course: "",
                    evaltable: "",
                    key:"",
                },
                list: [
                    {
                        section: '2017年第一学期',
                        evelPerson: '张过分',
                        courseName: '计算机基础',
                        className:'计12-11',
                        teacher: "11",
                        evelTable: "课堂教学评价表",
                        evalTime:'2017-09-10',
                        score:'60'
                    }
                ],
               
            },
            methods:{
                
                getList: function () {
                    var that = this;
                    if (this.role == 1) {
                        data = {};
                    } else if (this.role == 3) {
                        data = {};
                    }
                }
            },
            mounted: function () {
                this.role = GetLoginUser().Sys_Role_Id;
                this.getList();
            }
        })
       
    </script>
</body>
</html>

