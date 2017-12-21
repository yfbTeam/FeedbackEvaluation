<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllotPeople.aspx.cs" Inherits="FEWeb.SysSettings.AllotPeople" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>用户管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>

</head>

<%--开课单位--%>
<script type="text/x-jquery-tmpl" id="item_College">
    <option value="${Id}">${College_Name}</option>
</script>

<script type="text/x-jquery-tmpl" id="itemData">
    <tr>
        {{if Roleid ==  CurrentRoleid}}
           <td>
               <input type='checkbox' uniqueno="${UniqueNo}" name='se' checked="checked" /></td>
        {{else}}
         <td>
             <input type='checkbox' uniqueno="${UniqueNo}" name='se' /></td>
        {{/if}}
        <td>${UniqueNo}</td>
        <td>${Name} </td>
        <td>${Sex} </td>
        <td>${MajorName}  </td>
    </tr>
</script>


<body>
    <div class="main">
        <div class="search_toobar clearfix">
            <div class="fl">
                <label for="" class="fl">当前选择角色:</label>
                <span style="line-height: 35px; margin-left: 10px; display: inline-block; color: #6a264b" class="fl" id="rolenametext">学生</span>
            </div>
            <div class="fr">

                <div class="fl ml10">
                    <label for="">成员分类:</label>
                    <select class="select" id="teacher_student" style="width: 178px">
                        <option selected="selected" value="3">教师</option>
                    </select>
                </div>

                <div class="fl ml10">
                    <label for="">系(院):</label>
                    <select class="select" id="college" style="width: 178px">
                        <option value="">全部</option>
                    </select>
                </div>


                <%--href="javascript:search();"--%>
                <div class="fl ml10">
                    <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl">
                    <a class="search fl" href="javascript:search();" onclick="SelectByWhere()"><i class="iconfont">&#xe600;</i></a>
                </div>
            </div>
        </div>
        <div class="table">
            <table>
                <thead>
                    <tr>
                        <th style="text-align: center; width: 40px;">
                            <input type="checkbox" id="cb_all" onclick="Check_All()" />
                        </th>
                        <th style="width: 200px">教职工号</th>
                        <th style="width: 150px">用户名</th>
                        <th style="width: 150px">性别</th>
                        <th>系(院)</th>
                    </tr>
                </thead>
                <tbody id="tb_indicator">
                </tbody>
            </table>
            <div id="test1" class="pagination"></div>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="确定" class="btn" onclick="SubmitUserinfo()" />
        <input type="button" value="取消" class="btna" onclick="quxiao()" />
    </div>

</body>
</html>
<script src="../Scripts/Common.js"></script>
<script src="../scripts/public.js"></script>
<script src="../Scripts/linq.min.js"></script>
<script src="../Scripts/layer/layer.js"></script>
<script src="../Scripts/jquery.tmpl.js"></script>
<link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
<script src="../Scripts/kkPage/jquery.kkPages.js"></script>
<link href="../Scripts/pagination/pagination.css" rel="stylesheet" />
<script src="../Scripts/pagination/jquery.pagination.js"></script>
<script src="../Scripts/WebCenter/AllotPeople.js"></script>
<script>

    var CurrentRoleid = parent.GetCurrentRoleid();
    var CurrentRoleName = parent.GetCurrentRoleName();
    //从子窗体筛选之后获取的数据【未处理】
    var reUserinfoAll = parent.get_teachers();
    
   
    var index = parent.layer.getFrameIndex(window.name);
    //从子窗体获取相关角色已有的用户
    var reUserinfoByselect_uniques = parent.get_reUserinfoByselect_Ids();
    //选中的用户
    var select_uniques = [];
    //绑定的数据【UI】
    var reUserinfoByselect;
    //从子窗体筛选之后获取的数据【已处理】
    var selectreUserinfoAll;
    var roleid = 3;

    var pageIndex = 0;
    var pageSize = 10;
    var pageCount;

    var IsAll_Select = false;


    //-----------初始化--------------------------------------------------------------------------------------
    $(function () {
       
        UI_Allot.prepare_init();
        UI_Allot.data_init(3);
        UI_Allot.PageType = 'AllotPeople';
        //UI_Course.PageType = 'AllotPeople';
        UI_Allot.GetProfessInfo();

        UI_Allot.SubmitUserinfo_Compleate = function (result) {
            if (result) {
                parent.layer.msg('分配成功');
                parent.SelectGourp_Init();
                parent.layer.close(index);
            }
            else {
                alert("分配失败");
                parent.layer.close(index);
            }
        };
    })

    //-----------提交信息---------------------------------------------------------------------------------------  
    function SubmitUserinfo() {
        UI_Allot.SubmitUserinfo();
    }
    //-----------取消---------------------------------------------------------------------------------------
    function quxiao() {
        parent.layer.close(index);
    }
    //-----------搜索---------------------------------------------------------------------------------------
    function SelectByWhere() {
        UI_Allot.all_change();
    }

    function Check_All() {
        UI_Allot.Check_All();
    }
</script>

