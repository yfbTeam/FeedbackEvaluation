<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyStatiscal.aspx.cs" Inherits="FEWeb.Evaluation.MyStatiscal" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价统计</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <style>
        .objective_lists li {
            position: relative;
        }

        .tableheader .evalmes {
            margin: 0 auto;
        }

        .Pagination {
            position: absolute;
            top: 0px;
            right: 50px;
        }

        .objective_lists li dd .lists_row span {
            padding: 0;
        }
        .no_data {
            color:#333;
            margin-left:26px;
        }
    </style>
    <script type="text/x-jquery-tmpl" id="item_detail">
        {{each TList}}
        {{if $value.index==1}}
        <tr>
            <td rowspan="{{= $value.len}}" style="width: 20px; line-height: 20px; padding: 10px  20px;">{{= $value.IndicatorType_Name}}</td>
            <td style="text-align: left; padding-left: 20px; padding-right: 20px;">{{= $value.order_number}}、{{= $value.Name}}</td>
            <td>{{= $value.person_score}}</td>
            <td>{{= $value.college_score}}</td>
            <td>{{= $value.school_score}}</td>
        </tr>
        {{else $value.index!=1}}
        <tr>
            <td style="text-align: left; padding-left: 20px;">{{= $value.order_number}}、{{= $value.Name}}</td>
            <td>{{= $value.person_score}}</td>
            <td>{{= $value.college_score}}</td>
            <td>{{= $value.school_score}}</td>
        </tr>
        {{else}}
        {{/if}}
        {{/each}}
    </script>
    <script type="text/x-jquery-tmpl" id="item_question">
        {{each TList}}
        <li>
            <dt class="clearfix">
                <div class="objective_name fl">1.{{= $value.Name}}</div>
                <div class="toggle fr"><i class="iconfont">&#xe643;</i></div>
            </dt>

            <dd>
                <div class="request_body">
                    <div class="lists_row">
                       <span>
                            <b class="fr">提交时间：2017-02-20 08:00</b></span>
                    </div>
                    <div class="lists_row">

                        <span>讲话的生动：惟妙惟肖 津津有味 绘声绘色 娓娓动听 妙语连珠 滔滔不绝 余音袅袅 活神活现出口成章 行云流水。内容的精彩：引人入胜 如痴如醉 思绪万千 身临其境 字字珠玑 一字千金 精彩绝伦 扣人心弦 韵味无穷 精彩纷呈 跌宕起伏 纷繁复杂 一波三折 文不加点 回肠荡气 文采飞扬 妙趣横生 辞采华美。讲话的生动：惟妙惟肖 津津有味 绘声绘色 娓娓动听 妙语连珠 滔滔不绝 余音袅袅 活神活现出口成章 行云流水。内容的精彩：引人入胜 如痴如醉 思绪万千 身临其境 字字珠玑 一字千金 精彩绝伦 扣人心弦 韵味无穷 精彩纷呈 跌宕起伏 纷繁复杂 一波三折 文不加点 回肠荡气 文采飞扬 妙趣横生 辞采华美。
										        <b class="fr">提交时间：2017-02-20 08:00</b>
                        </span>
                    </div>
                </div>
            </dd>

        </li>
        {{/each}}
    </script>
    <script type="text/x-jquery-tmpl" id="item_StudySection">
        <option value="${Id}">${DisPlayName}
        </option>
    </script>
    <script type="text/x-jquery-tmpl" id="item_answer">
        <dd>

            <div class="request_body">
                <div class="lists_row">
                    <em class="img answered" onclick="OpenIFrameWindow('匿名互动','/InteractFeed/Answer.aspx','800px','650px;')">
                        <i class="iconfont">&#xe808;</i>
                        <p class="type">已回</p>
                    </em>
                    <span>
                        <b class="fr">提交时间：2017-02-20 08:00</b>
                    </span>
                </div>
                <div class="lists_row">
                    <em class="img noanswer">
                        <i class="iconfont">&#xe808;</i>
                        <p class="type">未回</p>
                    </em>
                    <span><b class="fr">提交时间：2017-02-20 08:00</b></span>
                </div>
            </div>
        </dd>
    </script>

    <script type="text/x-jquery-tmpl" id="item_Course">
        <option value="${CourseRel_Id}">${Course_Name}
        </option>
    </script>

    <script type="text/x-jquery-tmpl" id="item_Regular_Name">
        <option>${Name}
        </option>
    </script>
    <%-- <script type="text/x-jquery-tmpl" id="item_Question">
        <tr>
            <td rowspan="6" style="width: 20px; padding: 0px 20px; line-height: 20px;">教学态度
            </td>
            <td style="text-align: left; padding-left: 20px; padding-right: 20px;">1．老师对教学工作有热情，讲课认真、投入。
            </td>
            <td>120</td>
            <td>120</td>
            <td>120</td>
        </tr>
        {{each child_list}}
                <tr>
                    <td style="text-align: left; padding-left: 20px;">2．老师上课不迟到不早退，课上不接打手机。
                    </td>
                    <td>120</td>
                    <td>120</td>
                    <td>120</td>
                </tr>
        {{/each}}
                           
    </script>--%>
</head>
<body>
     <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
        <div class="crumbs" style="margin: 5px 0px 20px 0px;">
            <a id="leve1" onclick="history.back()">调查与测验</a>
            <span>&gt;</span>
            <a onclick="location.replace(location.href);" id="leve2" href="javascript:;"></a>
        </div>

        <div class="content clearix">
            <div class="tableheader">
                <h1 class="tablename" id="tablename"></h1>
                <div class="evalmes">
                    <span id="eval_name"></span>
                    <%--<span id="section_name"></span>--%>
                    <%--<span id="course_name"></span>--%>
                    <span id="regu_name"></span>
                    <span id="all_eva"></span>
                    <span id="sj_eva"></span>
                    <span id="pj_eva"></span>
                </div>
            </div>
            <div class="sort_nav">
                <a href="javascript:;" class="selected">学生评价</a>
            </div>
            <div class="sort_list">
                <div class="sort_item">
                    <%-- <h1 class="title_ti">一、主观题（6题）</h1>--%>
                    <table class="statable" style="display: none">
                        <thead>
                            <tr>
                                <th>测评内容
                                </th>
                                <th width="4%">A
                                </th>
                                <th width="4%">B
                                </th>
                                <th width="4%">C
                                </th>
                                <th width="4%">D
                                </th>
                                <th width="4%">E
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tb_detail">
                        </tbody>
                    </table>
                    <%--<h1 class="title_ti">二、客观题（4题）</h1>--%>
                    <ul class="objective_lists" id="tb_question">
                    </ul>
                </div>
                <div class="sort_item none">
                    <ul class="feedback_lists">
                        <%--<li>
                            <h2 class="ti">讲话的生动：惟妙惟肖 津津有味 绘声绘色 娓娓动听 妙语连珠 滔滔不绝 余音袅袅 活神活现出口成章 行云流水。</h2>
                            <textarea></textarea>
                        </li>
                        <li>
                            <h2 class="ti">讲话的生动：惟妙惟肖 津津有味 绘声绘色 娓娓动听 妙语连珠 滔滔不绝 余音袅袅 活神活现出口成章 行云流水。</h2>
                            <textarea></textarea>
                        </li>--%>
                    </ul>
                    <div class="btnwrap" style="position: static; background: none; display: none">
                        <input type="button" value="提交" onclick="submit_TeacherAnswer()" class="btn" />
                        <input type="button" value="取消" class="btna" id="cancel" />
                    </div>
                </div>
            </div>

        </div>
   
             </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
     <script src="../Scripts/public.js"></script>
    
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/jquery.linq.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
    <script type="text/javascript">
        var page = getQueryString("page");
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            if (page == 0) {
                $("#nav li").eq(1).addClass('active');
            }
            else {
                $("#nav li").eq(2).addClass('active');
            }
           
            Get_Eva_Common_A_By_Id();
          
            navTab('.sort_nav', '.sort_list');

        })

        var user = GetLoginUser();

        if (user.Sys_Role == '超级管理员') {
            $('#tongji').css("display", 'block');
            $('#system_setting').css("display", 'block');

        }

       
        GetCourseInfo();
        function GetCourseInfo() {

            var postData = { func: "GetCourseInfo" };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
                data: postData,
                dataType: "json",
                async: false,
                success: function (json) {
                    if (json.result.errMsg == "success") {

                        var retData = json.result.retData;
                        retData = Enumerable.From(retData).OrderBy('$.Id').ToArray();//按Id进行升序排列        


                        $("#item_Course").tmpl(retData).appendTo("#course");
                    }
                },
                error: function (errMsg) {
                    layer.msg("失败2");
                }
            });
        }

        Get_Eva_Regular_Name();
        //获取定期评价名称
        function Get_Eva_Regular_Name() {
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Get_Eva_Regular_Name" },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        //console.log(JSON.stringify(json));
                        var retData = json.result.retData;

                        $("#item_Regular_Name").tmpl(retData).appendTo("#regular_name");
                    }

                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }

        var edu_details = [];


        var Task_Id = getQueryString("Task_Id");
        function Get_Eva_Common_A_By_Id() {
            var TeacherUID = GetLoginUser().UniqueNo;
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { Func: "Get_Eva_Common_A_By_Id", Task_Id: Task_Id, TeacherUID: TeacherUID },
                success: function (json) {
                    if (json.result.errMsg == "success") {

                        var retData = json.result.retData;
                        var cache_data = null;
                        console.log(JSON.stringify(json));
                        //评价名称
                        //$("#eval_name").html('评价名称：' + retData.task.Name);
                        if (retData.task != null) {
                            $("#leve2").html(retData.task.Name);
                           
                        }

                        ////任务名称
                        $("#tablename").html(retData.task.Name);
                     
                        $("#regu_name").html('评价时间：' + DateTimeConvert(retData.task.StartTime, 'yyyy-MM-dd', true) + '~' + DateTimeConvert(retData.task.EndTime, 'yyyy-MM-dd', true))

                        ////平均分赋值
                        //var score = retData[0].score_list;
                        ////全部评价人数

                        if (page == 0) {
                            $("#all_eva").html("评价人数：" + retData.score_list[1] + "人");
                        }
                        ////实际评价人数
                        if (page == 0) {
                            $("#sj_eva").html("实际评价人数：" + retData.score_list[2] + "人");
                        }
                        else {
                            $("#sj_eva").html("评价人数：" + retData.score_list[2] + "人");
                        }
                        ////评价分
                        //$("#pj_eva").html("平均分:100分");

                        var order_number = 0;
                        var single_html = '';
                        var t_list = retData.TList[0];
                        if (retData.task.IsScore == "0") {
                            $("#pj_eva").html("平均分：" + retData.score_list[0] + "分");
                        }
                        else {
                            $("#pj_eva").html("平均分：不计分");
                        }

                        for (var i = 0; i < t_list.length; i++) {

                            if (t_list[i].t.QuesType_Id != 3) {
                                order_number++;
                                $(".statable").show();
                                single_html += '<tr>';
                                single_html += '<td style="text-align: left; padding-left: 20px; padding-right: 20px;">' + order_number + '、' + t_list[i].t.Name + '</td>';

                                single_html += '<td>' + t_list[i].t.OptionA_S + '</td>';
                                single_html += '<td>' + t_list[i].t.OptionB_S + '</td><td>' + t_list[i].t.OptionC_S + '</td><td>' + t_list[i].t.OptionD_S + '</td><td>' + t_list[i].t.OptionE_S + '</td></tr>';
                            }
                        }
                        $("#tb_detail").append(single_html);


                        ////教学反馈结束
                        var answer = '';
                        var index_q = 0;
                        var TList_1 = retData.TList[0];
                        for (var i = 0; i < TList_1.length; i++) {
                            index_q++;
                            //alert(TList_1[i].t.QuesType_Id);
                            if (TList_1[i].t.QuesType_Id == 3) {
                                answer += '<li><dt class="clearfix"><div class="objective_name fl">' + index_q + '、' + TList_1[i].t.Name + '</div><div class="toggle fr"><i class="iconfont">&#xe643;</i></div></dt><dd >';
                                var feed_anony_model_list = retData.feed_anony_model_list;
                                if (feed_anony_model_list.length > 0) {
                                    for (var j = 0; j < feed_anony_model_list.length; j++) {
                                        if (feed_anony_model_list[j].Eva_TableDetailId == TList_1[i].t.Id && TList_1[i].t.QuesType_Id == 3) {
                                            var Feed_Anony_Model_Answer_List = feed_anony_model_list[j].Feed_Anony_Model_Answer_List;
                                            if (Feed_Anony_Model_Answer_List != null) {

                                                for (var m = 0; m < Feed_Anony_Model_Answer_List.length; m++) {
                                                    var isExit = Feed_Anony_Model_Answer_List[m].isExit == "0" ? "未回" : "已回";
                                                    if (m == Feed_Anony_Model_Answer_List.length-1) {
                                                        answer += '<div class="request_body"><div class="lists_row" style="min-height:20px">';
                                                    }
                                                    else {
                                                        answer += '<div class="request_body"><div class="lists_row" style="min-height:20px;border-bottom:1px solid #ccc">';
                                                    }
                                                    //if (Feed_Anony_Model_Answer_List[m].isExit == 0) {
                                                    //    answer += '<em onclick="answer(' + Feed_Anony_Model_Answer_List[m].AnswerId + ',' + Feed_Anony_Model_Answer_List[m].StudentUID + ')" id="em_' + Feed_Anony_Model_Answer_List[m].AnswerId + '" class="img answered">';
                                                    //}
                                                    //else {
                                                    //    answer += '<em class="img noanswer" onclick="answer(' + Feed_Anony_Model_Answer_List[m].AnswerId + ',' + Feed_Anony_Model_Answer_List[m].StudentUID + ')">';
                                                    //}
                                                    answer += '<span>';
                                                    answer += '<b class="no_data" style="color:#333">' + Feed_Anony_Model_Answer_List[m].Answer + '</b><b class="fr">提交时间：' + DateTimeConvert(Feed_Anony_Model_Answer_List[m].CreateTime, 'yyyy-MM-dd HH:mm:ss', true) + '</b></span></div>';
                                                    answer += '</div>';
                                                }
                                            }
                                            else {
                                                answer += '<div class="request_body"><div class="lists_row" style="text-align:left">暂无数据';
                                                answer += '</div></div>';
                                            }
                                        }
                                    }
                                }
                                else {
                                    answer += '<div class="request_body"><div class="lists_row" style="text-align:center">暂无数据';
                                    answer += '</div></div>';
                                }
                                answer += '</dd></li>';
                            }
                        }

                        $("#tb_question").append(answer);
                        $('.objective_lists li').each(function () {
                            $(this).find('dd').kkPages({
                                PagesClass: '.request_body', //需要分页的元素
                                PagesMth: 3, //每页显示个数
                                PagesNavMth: 4 //显示导航个数
                            });
                        })
                        animate();
                    }
                    //animate();
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }

     
        function submit_TeacherAnswer() {
            $('feedback_lists').each(function () {
                $(this).find('dd').kkPages({
                    PagesClass: '.request_body', //需要分页的元素
                    PagesMth: 3, //每页显示个数
                    PagesNavMth: 4 //显示导航个数
                });
            })
            var TeacherUID = GetLoginUser().UniqueNo;
            var obj_data = new Object();
            obj_data.CourseId = Course_UniqueNo;
            obj_data.TeacherUID = TeacherUID;
            obj_data.Func = 'Add_Eva_TeacherAnswer';

            var obj_arr = [];

            $('.ti').each(function () {
                var sub_array = new Object();
                //alert($(this).find('input[type="hidden"]').val());
                sub_array.Eva_Distribution_Id = Id;
                sub_array.Indicator_Id = $(this).find('input[type="hidden"]').val();
                sub_array.Answer = $(this).find('textarea').val();
                obj_arr.push(sub_array);
            })
            obj_data.List = JSON.stringify(obj_arr);
            $.ajax({
                url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: obj_data,
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        //console.log(JSON.stringify(json));
                        if (json.result.errMsg == "success") {
                            layer.msg('提交成功!');
                            window.history.go(-1);
                        }
                    }

                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }

        //回复                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
        function answer(answerId, studentUID) {
            OpenIFrameWindow('回复', '/InteractFeed/Answer.aspx?stuid=' + studentUID + '&answerid=' + answerId, '900px', '650px')
        }

        //样式的回调方法
        function changeStatusStyle(answerId) {
            $("#em_" + answerId + "").removeClass('answered').addClass('noanswer');
            $("#p_" + answerId + "").html('已回');
        }

        function animate() {
            $('.objective_lists').find('li:has(dt)').find('dt').click(function () {
                var $next = $(this).next('dd');
                if ($next.is(':hidden')) {
                    $(this).parent().siblings('li').removeClass('active');
                    $(this).parent('li').addClass('active');
                    $next.stop().slideDown();

                    if ($(this).parent('li').siblings('li').children('dd').is(':visible')) {
                        $(this).parent("li").siblings("li").removeClass('active');
                        $(this).parent("li").siblings("li").find("dd").slideUp();
                    }

                    $('.table').kkPages({
                        PagesClass: 'tbody tr', //需要分页的元素
                        PagesMth: 5, //每页显示个数
                        PagesNavMth: 2, //显示导航个数
                        IsShow: false
                    });

                } else {
                    $(this).parent('li').removeClass('active');
                    $next.stop().slideUp();
                }

            })
        }
    </script>
</body>
</html>

