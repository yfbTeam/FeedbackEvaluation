<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Allot_Add_Table.aspx.cs" Inherits="FEWeb.SysSettings.Allot_Add_Table" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>表格添加</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/layout.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>

    <style>
        .table {
            margin-top: 10px;
        }
    </style>
</head>


<body>
    <div class="main" >
        <div class="search_toobar clearfix">
            <div class="fl ">
                <label for="">当前选择课程分类:</label>
                <label id="lblCourseType"></label>
                <%--<select id="_currsele" class="select">
                </select>--%>
            </div>
            <div class="fr ml10">
                <input type="text" name="" id="key" placeholder="请输入表名称" value="" class="text fl">
                <a class="search fl" href="javascript:;" onclick="SelectByWhere()"><i class="iconfont">&#xe600;</i></a>
            </div>
        </div>

        <div class="table" style="min-height: 240px;">
            <table >
                <thead>
                    <tr>
                        <th>
                            <input type="checkbox" id="cb_all" onclick="Check_All()" />
                        </th>
                        <th style="text-align: left; padding-left: 20px;">评价表名称	    	 		 				 					 					   	
                        </th>
                        <th width="8%">是否记分
                        </th>
                        <th width="8%">引用次数
                        </th>
                        <th width="8%">状态
                        </th>
                        <th width="8%">创建人
                        </th>
                        <th width="8%">创建时间
                        </th>
                    </tr>
                </thead>

                <tbody id="tb_eva">
                </tbody>
            </table>
        </div>
        <div id="test1" class="pagination"></div>
        <div class="btnwrap">
            <input type="button" value="保存" class="btn" onclick="Submit()" />
            <input type="button" value="取消" class="btna" onclick="CloseW()" />
        </div>

    </div>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/public.js"></script>
    <script src="../../Scripts/jquery.linq.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
    <script src="../../Scripts/pagination/jquery.pagination.js"></script>
    <link href="../../Scripts/pagination/pagination.css" rel="stylesheet" />
    <script src="../../Scripts/WebCenter/SortCourse.js"></script>
    <script src="../../Scripts/WebCenter/CourSortMan.js"></script>
    <script src="../../Scripts/WebCenter/TableDesigin.js"></script>
    <script src="../../Scripts/WebCenter/AllotTable.js"></script>
    <script type="text/x-jquery-tmpl" id="item_courseType">
        <option value="${Key}">${Value}</option>
    </script>
    <script type="text/x-jquery-tmpl" id="item_eva">
        <tr>
            <td style="width: 5%">
                <input no="${t.Id}" tableId ="${t.Table}" name="se" type="checkbox" /></td>

            <td style="text-align: left; padding-left: 20px; width: 35%">${t.Name}</td>
            {{if t.IsScore ==0}}
            <td style="width: 5%">是</td>
            {{else}}
            <td style="width: 5%">否</td>
            {{/if}}            
            <td>${t.UseTimes}</td>

            {{if t.IsEnable ==0}}
            <td style="width: 5%"><span>启用</span></td>
            {{else}}
              <td style="width: 5%"><span>禁用</span></td>
            {{/if}}   

            <td style="width: 15%">${u.Name}</td>
            <td style="width: 15%">${DateTimeConvert(t.CreateTime,'yyyy-MM-dd',true)}</td>
        </tr>
    </script>

    <script>
        var Eva_Role = get_Eva_Role_by_rid();
        var select_sectionid = parent.select_sectionid
        //var TableList = [];
        UI_Course.PageType = 'Allot_Add_Table';
        var course_TypeId = getQueryString('course_TypeId');
        //当前选择的课程类型
        var select_CourseTypeName = getQueryString('select_CourseTypeName');
        var IsAll_Select = false;
        var reUserinfoByselect = [];
        var select_uniques = [];
        var pageSize = 5;
        var pageIndex = 0;
        $(function () {
            initdata();
            $('#lblCourseType').text(select_CourseTypeName);
        })
        //初始化表格列表
        function initdata() {
            //===========获取课程分类，选择课程分类
            UI_Course.GetCourse_Type_Compleate = function () {
                if (course_TypeId != null && course_TypeId > -1) {
                    $('#_currsele').val(course_TypeId);
                }
            };
            UI_Course.GetCourse_Type();
            UI_Table.initdataCompleate = function (data) {
               
            };           
            UI_Table.PageType = 'Allot_Add_Table';
            UI_Table.initdata(true);
        }
        function Submit() {
            PageType = 'Allot_Add_Table';
            Add_CourseType_Table(course_TypeId, select_uniques, select_sectionid);
        }
    </script>
</body>
</html>
