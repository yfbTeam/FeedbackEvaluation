<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FEWeb.Evaluation.CourseEvalSee.index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价查看</title>
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/layout.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix" id="courseEvalSee">
             <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>
                    <select class="select" id="section" style="width: 148px;" v-model="option.section">
                         <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml10">
                    <label for="">评价名称:</label>
                    <select class="select" id="" style="width: 148px;" v-model="option.evalName">
                         <option value="">全部</option>
                    </select>
                </div>
                <div class="fl ml10">
                    <label for="">专业部门:</label>
                    <select class="select"  style="width: 148px;" id="DepartMent" v-model="option.department">
                         <option value="0">全部</option>
                    </select>
                </div>
                <div class="fl ml10">
                    <label for="">年级:</label>
                    <select class="select" style="width:108px;" v-model="option.grade">
                         
                    </select>
                </div>
                 <div class="fl ml10" v-if="role==1" v-cloak>
                    <label for="">教师:</label>
                    <select class="select" style="width:108px;" v-model="option.teacher">
                         
                    </select>
                </div>
                  <div class="fl ml10" >
                     <input type="text" name="key" id="key" placeholder="请输入课程名称" value="" class="text fl" style="width: 130px;" v-model="option.key">
                     <a href="javascript:;" class="search fl"><i class="iconfont">&#xe600;</i></a>
                 </div>
            </div>
            <div class="table mt10">
                <div v-if="role==1" v-cloak>
                    <table>
                    <thead>
                        <tr>
                            <th>学年学期</th>
                            <th>评价名称</th>
                            <th>课程名称</th>
                            <th>教师姓名</th>
                            <th>专业部门</th>
                            <th>年级</th>
                            <th>合班</th>
                            <th>班级人数</th>
                            <th>参评人数</th>
                            <th>参评率</th>
                            <th>平均分</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in list">
                            <td>{{item.section}}</td>
                            <td>{{item.evelName}}</td>
                            <td>{{item.courseName}}</td>
                            <td>{{item.teacherName}}</td>
                            <td>{{item.department}}</td>
                            <td>{{item.grade}}</td>
                            <td>{{item.className}}</td>
                            <td>{{item.classNumer}}</td>
                            <td>{{item.evalNumber}}</td>
                            <td>{{item.evalLv}}</td>
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
                </div>
                <div v-if="role==3" v-cloak>
                    <table>
                    <thead>
                        <tr>
                            <th>学年学期</th>
                            <th>评价名称</th>
                            <th>课程名称</th>
                            <th>专业部门</th>
                            <th>年级</th>
                            <th>合班</th>
                            <th>开始时间</th>
                            <th>结束时间</th>
                            <th>班级人数</th>
                            <th>参评人数</th>
                            <th>参评率</th>
                            <th>平均分</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in list">
                            <td>{{item.section}}</td>
                            <td>{{item.evelName}}</td>
                            <td>{{item.courseName}}</td>
                            <td>{{item.department}}</td>
                            <td>{{item.grade}}</td>
                            <td>{{item.className}}</td>
                            <td>{{item.startTime}}</td>
                            <td>{{item.endTime}}</td>
                            <td>{{item.classNumer}}</td>
                            <td>{{item.evalNumber}}</td>
                            <td>{{item.evalLv}}</td>
                            <td>{{item.score}}</td>
                            <td class="operate_wrap">
                                <div class="operate" onclick="location.href='detailModal.aspx?Id='+getQueryString('Id')+'&Iid='+getQueryString('Iid')+''">
                                    <i class="iconfont color_purple">&#xe60b;</i>
                                    <span class="operate_none bg_purple">查看</span>
                                </div>
                                <div class="operate" onclick="">
                                    <i class="iconfont color_purple">&#xe609;</i>
                                    <span class="operate_none bg_purple">扫码</span>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../../js/vue.min.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/public.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/WebCenter/Base.js"></script>
    <script src="/Scripts/linq.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            Base.bindStudySection();
            Base.BindDepart();
            tableSlide();
        })
        var courseEvalSee = new Vue({
            el: '#courseEvalSee',
            data: {
                role: "",
                option: {
                    section: "",
                    department: "",
                    evalName: "",
                    grade: "",
                    teacher:'',
                    key: "",
                },
                list: [
                    {
                        section: '2017年第一学期',
                        evelName: '第一次学生评价',
                        courseName: '计算机基础',
                        teacherName: "刘安",
                        department: '计算机学院',
                        grade: "2014",
                        className: '交通12-11',
                        startTime: "2017-08-15",
                        endTime: "2017-08-15",
                        classNumer: "20",
                        evalNumber: "18",
                        evalLv: "90%",
                        score: "90",
                    }
                ]
            },
            methods: {
                
                getList: function () {
                    var that = this;
                    if (that.role == 1) {
                        data = {}
                    } else if (that.role == 3) {
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
