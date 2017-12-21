<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourSortMan.aspx.cs" Inherits="FEWeb.SysSettings.CourSortMan" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课程分类</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    <style>
       .menu_list li .operates {
            position: absolute;
            right: 10px;
            top: 8px;
            line-height: 22px;
            z-index: 11;
        }
        .menu .menu_list li .operate {
            text-align: center;
            min-width: 32px;
        }
        .menu_list li em {
            display: block;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            line-height: 36px;
        }
        .menu_list li ul li:before {
                width: 5px;
                height: 5px;
                content: '';
                border-radius: 50%;
                display: block;
                background: #C1E68E;
                position: absolute;
                left: 8px;
                top: 15px;
            }
        .menu_list li ul li {
            padding: 0px 10px 0px 20px;
        }
    </style>
    
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
            <div class="sort_nav" id="threenav">
            </div>
            <div class="sortwrap clearfix">
                <div class="menu fl">
                    <h1 class="titlea">课程分类
                    </h1>
                    <ul class="menu_list" id="menu_listscours">
                    </ul>
                    <%--  <input type="button" value="新增分类" style="display: block" class="new" onclick="OpenIFrameWindow('新增分类', 'AddCourseSort.aspx', '500px', '200px')" />--%>
                </div>
                <div class="sort_right fr mr" style="margin-left: -20px">
                    <div class="search_toobar clearfix">
                        <div class="fl">
                            <input type="text" name="" id="select_where" placeholder="请输入关键字" value="" class="text fl">
                            <a class="search fl" href="javascript:;" onclick="all_change();"><i class="iconfont">&#xe600;</i></a>
                        </div>
                        <div class="fr" style="display: block">
                            <input type="button" value="分配评价表" class="btn mr10" onclick="OpenTableAllot()" />
                            <input type="button" name="" id="course_set" value="分配课程" class="btn" onclick="dis_course();">
                        </div>
                    </div>
                    <div class="table">
                        <table>
                            <thead>
                                <tr>
                                    <th style="width: 10%;">课程编码</th>
                                    <th style="width: 25%;">课程名称</th>
                                    <th style="width: 10%;">排课分类</th>
                                    <th style="width: 10%;">课程性质</th>
                                    <th style="width: 15%;">部门</th>
                                    <th style="width: 15%;">子部门</th>
                                    <th style="width: 10%;">操作</th>
                                </tr>
                            </thead>
                            <tbody id="ShowCourseInfo">
                            </tbody>
                        </table>
                    </div>
                    <div id="pageBar" class="page"></div>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../../Scripts/Common.js"></script>
    <script src="/Scripts/public.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <script src="../../Scripts/pagination/jquery.pagination.js"></script>
    <link href="../../Scripts/pagination/pagination.css" rel="stylesheet" />
    <script src="../../Scripts/laypage/laypage.js"></script>
    <script src="../../Scripts/WebCenter/CourSortMan.js"></script>
    <script id="Courseinfo_tmpl" type="text/x-jquery-tmpl">
        <tr>
            <td>${Course_No}</td>
            <td>${Course_Name}</td>
            <td>${CourseRel_Name}</td>
            <td>${CourseProperty}</td>
            <td>${DepartmentName}</td>
            <td>${SubDepartmentName}</td>
            <td>
                <div class="operate" onclick="removeCourseDis('${CourseRelID}','${Course_Name}')">
                    <i class="iconfont color_purple">&#xe798;</i>
                    <span class="operate_none bg_purple" style="display: none;">移除      
                    </span>
                </div>
            </td>
        </tr>
    </script>
    <script id="course_item" type="text/x-jquery-tmpl">
        <li sectionid='${course_parent.SectionId}'>
            <span>${course_parent.DisPlayName}<i class="iconfont">&#xe643;</i></span>
            <ul>
                {{each objectlist}}
                <li>
                    <em onclick="GetCourseinfoBySortMan('{{= $value.Key}}','{{= $value.Value}}','{{= $value.SectionId}}')">{{= $value.Value}}</em>
                    <div class="operates">
                        <div class="operate" onclick="OpenIFrameWindow('设置分类', 'AddCourseSort.aspx?id={{= $value.Id}}&Name={{= $value.Value}}&IsEnable={{= $value.IsEnable}}', '500px', '280px')">
                            <i class="iconfont color_purple">&#xe632;</i>
                            <a class='operate_none bg_purple'>设置</a>
                        </div>
                        <div class="operate ml5" onclick="remove('{{= $value.Id}}','{{= $value.Value}}');">
                            <i class="iconfont color_purple">&#xe61b;</i>
                            <a class='operate_none bg_purple'>删除</a>
                        </div>
                    </div>
                </li>
                {{/each}}                                     
                {{if course_parent.Study_IsEnable == 1}}
             <input type="button" value="新增分类" style="display: block" class="new" onclick="OpenIFrameWindow('新增分类', 'AddCourseSort.aspx?itemid=0&SectionId={{= course_parent.SectionId}}&IsEnable=0', '500px', '280px')" />
                {{else course_parent.Study_IsEnable== 0}}
                {{/if}}
              
            </ul>
        </li>
    </script>

    <script type="text/x-jquery-tmpl" id="itemCount">
        <span style="margin-left: 5px; font-size: 14px;">共${RowCount}条，共${PageCount}页</span>
    </script>
</body>
</html>

<script>
    var select_sectionid = '';
    var returnVal_Courseinfo;
    var reUserinfoByselect
    var UserinfoByNoSort;
    //当前选择的课程类型
    var select_CourseTypeId = '';
    //当前选择的课程类型
    var select_CourseTypeName = '';
    var pageIndex = 0;
    var pageSize = 10;
    var pageCount;
    $(function () {
        $('#top').load('/header.html');
        $('#footer').load('/footer.html');
        UI_Course.PageType = 'CourSortMan';
        
        UI_Course.GetCourse_Type();

    });
    function menu_list() {
        UI_Course.menu_list();
    }
    function GetCourse_Type() {
        //获取课程分类
        UI_Course.GetCourse_Type();
    }
    function GetCourseInfo() {
        var key = $("#select_where").val();
        UI_Course.GetCourseInfo(pageIndex, select_sectionid, key, select_CourseTypeId);
    }
    //设置课程分类为未分类状态
    function SetIsEnable(Course_Id, isenable) {
        UI_Course.SetIsEnable(Course_Id, isenable);
    }
    //点击课程分类
    function GetCourseinfoBySortMan(key, value, SectionId) {
        select_CourseTypeId = key;
        select_CourseTypeName = value;
        select_sectionid = SectionId;
        pageIndex = 0;
        UI_Course.select_CourseTypeId = key;
        UI_Course.select_CourseTypeName = value;
        var key = $("#select_where").val();
        UI_Course.GetCourseInfo(pageIndex, select_sectionid, key, select_CourseTypeId);
    }
    //搜索
    function all_change() {
        var key = $("#select_where").val();
        UI_Course.GetCourseInfo(pageIndex, select_sectionid, key, select_CourseTypeId);
    }
    //分配课程
    function dis_course() {
        OpenIFrameWindow('分配课程', 'SortCourse.aspx?course_TypeId=' + select_CourseTypeId, '1000px', '600px')
    }
    //删除课程分类
    function remove(id, value) {
        layer.confirm('确定删除' + value + '吗？', {
            btn: ['确定', '取消'], //按钮
            title: '操作'
        }, function () { UI_Course.remove(id) });
    }

    function OpenTableAllot() {
        OpenIFrameWindow('分配表格', 'EvalTableAllot.aspx?Id=' + getQueryString('Id') + '&Iid=' + getQueryString('Iid') + '&select_CourseTypeId=' + select_CourseTypeId + '&select_CourseTypeName=' + select_CourseTypeName, '1000px', '650px');
    }
</script>

