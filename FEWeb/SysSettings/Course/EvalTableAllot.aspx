<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalTableAllot.aspx.cs" Inherits="FEWeb.SysSettings.EvalTableAllot" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课程分类</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
</head>
<body>
    <div>
        <div class="wrap clearfix">
            <div class="sortwrap clearfix">
                <div style="min-height: 500px;">
                    <div class="search_toobar clearfix">
                        <div class="fl mr20 ">
                            <label for="">当前选择课程分类:</label>
                            <label id="lblCourseType"></label>
                        </div>

                        <div class="fr ml20" style="display: block">
                            <input type="button" value="分配评价表" class="btn " id="distable" />
                        </div>
                        <div class="fr" style="margin-top: 1px">
                            <input type="text" name="" id="key" placeholder="请输入关键字" value="" class="text fl">
                            <a class="search fl" href="javascript:;" onclick="SelectByWhereList()"><i class="iconfont">&#xe600;</i></a>
                        </div>
                    </div>
                    <div class="table">
                        <table>
                            <thead>
                                <tr>
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
                                    <th width="20%">操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tb_eva">
                            </tbody>
                        </table>
                    </div>
                    <div id="test1" class="pagination"></div>
                </div>
            </div>
        </div>
        <div class="btnwrap">
            <input type="button" value="关闭 " class="btna" onclick="CloseW()" />
        </div>
    </div>
    <script src="../../Scripts/Common.js"></script>
    <script src="/Scripts/public.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/kkPage/jquery.kkPages.js"></script>
    <link href="../../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.tmpl.js"></script>
    <script src="../../Scripts/pagination/jquery.pagination.js"></script>
    <link href="../../Scripts/pagination/pagination.css" rel="stylesheet" />
    <script src="../../Scripts/WebCenter/AllotTable.js"></script>
    <script type="text/x-jquery-tmpl" id="item_eva">
        <tr>
            <td style="text-align: left; padding-left: 20px; width: 35%">${Name}</td>
            {{if IsScore ==0}}
            <td style="width: 5%">是</td>
            {{else}}
            <td style="width: 5%">否</td>
            {{/if}}            
            <td>${UseTimes}</td>

            {{if IsEnable ==0}}
            <td style="width: 5%"><span>启用</span></td>
            {{else}}
              <td style="width: 5%"><span>禁用</span></td>
            {{/if}}   
            <td style="width: 15%">${UserName}</td>
            <td style="width: 15%">${DateTimeConvert(CreateTime,'yyyy-MM-dd',true)}</td>

            <td>
                <div class="operate" onclick="Delete_CourseType_Table('${Id}')">
                    <i class="iconfont color_purple"></i>
                    <span class="operate_none bg_purple" style="display: none;">删除      
                    </span>
                </div>
            </td>
        </tr>
    </script>
    <script>
        //当前选择的课程类型
        var select_CourseTypeId = getQueryString('select_CourseTypeId');
        //当前选择的课程类型
        var select_CourseTypeName = getQueryString('select_CourseTypeName');
        var CourseType_Table_Dat = [];
        var select_sectionid = parent.select_sectionid
        $(function () {
            $('#lblCourseType').text(select_CourseTypeName);
            $('#distable').on('click', function () {
                OpenIFrameWindow('分配表格', 'Allot_Add_Table.aspx?course_TypeId=' + select_CourseTypeId + '&select_CourseTypeName=' + select_CourseTypeName, '800px', '500px');
            });
            PageType = 'EvalTableAllot';         
            GetCourseType_Table(select_CourseTypeId, select_sectionid);
        })
        var returnVal_Courseinfo;
        var reUserinfoByselect;
        function reflesh() {
            GetCourseType_Table(select_CourseTypeId, select_sectionid);
        }
    </script>
</body>
</html>
