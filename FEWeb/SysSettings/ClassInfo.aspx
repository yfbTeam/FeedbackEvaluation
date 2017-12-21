<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassInfo.aspx.cs" Inherits="FEWeb.SysSettings.ClassInfo" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂信息维护</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
       
	</head>
    
	<body>
		<div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
				<div class="sort_nav" id="threenav">
                   
                </div>
                <div class="search_toobar clearfix">
                    <div class="fl">
                        <label for="">学年学期:</label>
                        <select class="select" id="nd" onchange="SelectDataTest()">
                         
                        </select>
                    </div>
                    <%--<div class="fl ml10">
                        <label for="">学期:</label>
                        <select class="select" id="jb" onchange="SelectDataTest()">
                        
                        </select>
                    </div>--%>
                    <div class="fl ml10">
                        <input type="text" name="" id="class_key" placeholder="请输入课堂名称" value="" class="text fl">
                        <a class="search fl" href="javascript:;" onclick="SelectByWhere()"><i class="iconfont">&#xe600;</i></a>
                    </div>
                    <div class="fr">
                        <input type="button" name="" id="" style="display:none" value="添加" onclick="OpenIFrameWindow('添加课堂信息', 'AddClassInfo.aspx', '800px', '540px')" class="btn">
                    </div>
                </div>
                <div class="table">
                	<table class="W_form" id="tb_CourseList">
                        <thead>
                            <tr class="trth">
                                <th class="number" width="5%">序号</th>
                                <th width="20%">学年学期</th>
                                <th width="25%">课程名称</th>
                                <th width="10%">班</th>
                               <%-- <th>年级</th>--%>
                               <%-- <th>专业部门名称</th>--%>
                                <th width="20%">教师姓名</th>
                                <th width="20%">教师性质</th>
                               <%-- <th>操作</th>--%>
                            </tr>
                        </thead>
                        <tbody id="tb_course">
                        	
                        </tbody>
                    </table>
                </div>
                 <div id="test1" class="pagination"></div>
			</div>
		</div>
		<footer id="footer"></footer>
        <script src="../Scripts/Common.js"></script>
         <script src="/Scripts/public.js"></script>
        <script src="/Scripts/layer/layer.js"></script>
        <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
        <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
           <script src="../Scripts/linq.min.js"></script>
        <script src="../Scripts/pagination/jquery.pagination.js"></script>
        <link href="../Scripts/pagination/pagination.css" rel="stylesheet" />
		<script>
		    $(function () {
		        $('#top').load('/header.html');
		        $('#footer').load('/footer.html');
		        GetClassInfo();
		        Get_StudySection_List();
		    })
		    //绑定年度信息
		    function Get_StudySection_List() {
		        var postData = { func: "Get_StudySection_List" };
		        $.ajax({
		            type: "Post",
		            url: HanderServiceUrl + "/SysClass/ClassInfoHandler.ashx",
		            data: postData,
		            dataType: "json",
		            async: false,
		            success: function (returnVal) {
		                console.log(returnVal);
		                if (returnVal.result.errMsg == "success")
		                {
		                   
		                    BindDataTo_StudySection_DisPlayName(returnVal.result.retData.DisPlayName);
		                }
		            },
		            error: function (errMsg) {
		                alert("失败2");
		            }
		        });
		    }

		    //绑定学年学期
		    function BindDataTo_StudySection_DisPlayName(bindData) {
		        $("#nd").empty();
		        //var str = "<option value='index'>未筛选</option>"
		        //$("#nd").append(str);
		        $(bindData).each(function () {
		            var str = "<option value='" + this + "'>" + this + "</option>";
		            $("#nd").append(str);
		        });
		    }

		    //绑定年度信息
		    function BindDataTo_StudySection_Academic(bindData) {
		        $("#nd").empty();
		        //var str= "<option value='index'>未筛选</option>"
		        //$("#nd").append(str);
		        $(bindData).each(function ()
		        {
		            var str = "<option value='" + this + "'>" + this + "</option>";
		            $("#nd").append(str);
		        });		    
		    }
		    //绑定年度信息
		    function BindDataTo_StudySection_Semester(bindData) {
		        $("#jb").empty();		    
		        //var str = "<option value='index'>未筛选</option>"
		        //$("#jb").append(str);
		        $(bindData).each(function () {
		            var str = "<option value='" + this + "'>"+this+"</option>";
		            $("#jb").append(str);
		        });
		    }
		    var redata;
		    var reUserinfoByselect;
            //绑定课程信息
		    function GetClassInfo()
		    {
		        var postData = { func: "GetClassInfo" };
		        $.ajax({
		            type: "Post",
		            url: HanderServiceUrl + "/SysClass/ClassInfoHandler.ashx",
		            data: postData,
		            dataType: "json",
		            async: false,
		            success: function (returnVal)
		            {
		                if (returnVal.result.errMsg == "success")
		                {
		                    redata = returnVal.result.retData;
		                    reUserinfoByselect = redata;
		                    var count = Enumerable.From(reUserinfoByselect).ToArray().length;
		                    fenye(count);
		                    //BindDataTo_GetClassInfo(returnVal.result.retData);
		                }
		            },
		            error: function (errMsg) {
		                alert("失败2");
		            }
		        });
		    }
		    //绑定课程信息
		    function BindDataTo_GetClassInfo(index,bindData)
		    {
		        $("#tb_course").empty();
		      
		        var strall = "";
		    
		        $(bindData).each(function () 
		        {
		           
		            var str =  "<tr>"+
                        		"<td>"+index+"</td>" +
                        		"<td>" + this.DisPlayName + "</td>" +
                        		"<td>"+this.Course_Name+"</td>"+
                        		"<td>"+this.ClassInfo_Name+"</td>"+
                        		//"<td>"+this.GradeInfo_Name+"</td>"+
                        		//"<td>"+this.Department_Name+"</td>"+
                        		"<td>"+this.Teacher_Name+"</td>"+
                        		"<td>" + this.Teacher_JobTitle + "</td>" +
                        		//"<td class='operate_wrap'>"+
                        		//	"<div class='operate'>"+
                        		//		"<i class='iconfont color_green'>&#xe602;</i>"+
                        		//		"<span class='operate_none bg_green'>编辑</span>"+
                        		//	"</div>"+
                        		//	"<div class='operate'>"+
                        		//		"<i class='iconfont color_green'>&#xe604;</i>"+
                        		//		"<span class='operate_none bg_green'>删除</span>"+
                        		//	"</div>"+              
                        		//"</td>"+         
                        	"</tr>"
		           
		            strall = strall + str
		            index++;
		        });
		        if (Enumerable.From(bindData).ToArray().length == 0) {
		            nomessage('#tb_course');
		            return;
		        }
		        $("#tb_course").append(strall);
		        //fenye();
		    }

		   
		    //function fenye1() {
		    //    $('.Pagination').remove();
		    //    $('.table').kkPages({
		    //        PagesClass: 'tbody tr', //需要分页的元素
		    //        PagesMth: 10, //每页显示个数
		    //        PagesNavMth: 4 //显示导航个数
		    //    });
		    //}
		    var pageIndex = 0;
		    var pageSize = 10;
		    var pageCount;
		    function fenye(pageCount) {

		        $("#test1").pagination(pageCount, {
		            callback: PageCallback,
		            prev_text: '上一页',
		            next_text: '下一页',
		            items_per_page: pageSize,
		            num_display_entries: 6,//连续分页主体部分分页条目数
		            current_page: pageIndex,//当前页索引
		            num_edge_entries: 1//两侧首尾分页条目数
		        });
		    }


		    //翻页调用
		    function PageCallback(index, jq) {

		        var arrRes = Enumerable.From(reUserinfoByselect).Skip(index * pageSize).Take(pageSize).ToArray();

		        BindDataTo_GetClassInfo(index * pageSize + 1, arrRes);
		    }

		    function SelectDataTest()
		    {
		       
		        reUserinfoByselect = Enumerable.From(redata).Where(function (i) {
		            //各个条件
		          
		            var flg = true;
		            var nd = $("#nd").val();
		          
		            if (nd != "index" && nd != i.DisPlayName) {
		                flg = false;
		            }
		            //var jb = $("#jb").val();
		            //if (jb != "index" && jb != i.Semester) {
		            //    flg = false;
		            //}		          
		            if (flg) { return i }
		        }).ToArray();
		        fenye(reUserinfoByselect.length)
		        //关键字筛选项设为空
		        $("#class_key").val("");
		    }
		    function SelectByWhere()
		    {
		        var sw = $("#class_key").val();
		        reUserinfoByselect = Enumerable.From(redata).Where("x=>x.Course_Name.indexOf('" + sw + "')!=-1").ToArray();
		        fenye(reUserinfoByselect.length)
		    }
		</script>
	</body>
</html>

