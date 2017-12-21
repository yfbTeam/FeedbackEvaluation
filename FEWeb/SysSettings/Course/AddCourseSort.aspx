<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourseSort.aspx.cs" Inherits="FEWeb.SysSettings.AddCourseSort" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="renderer" content="webkit" />
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>新增指标</title>
    <link rel="stylesheet" href="../../css/reset.css" />
    <link rel="stylesheet" href="../../css/style.css" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
  

    <style>
        .main {          
          min-height: initial;
        }
    </style>
</head>
<body>
    <div class="main">
        <div class="input-wrap clearfix">
            <label>分类名称：</label>
            <input type="text" class="text" id="CourseTypeName" placeholder="请输入分类名称" />
        </div>
        <div class="input-wrap clearfix" style="margin-bottom: 0;">
            <label>是否启用：</label>
            <div class="ml10 fl">
                <span class="switch-on" themecolor="#6a264b" id="AwardSwich"></span>
            </div>
        </div>
    </div>
    <div style="text-align: center">
        <input type="button" value="保存" onclick="submit()" class="btn" />
        <input type="button" value="取消" onclick="cancel()" class="btna" />
    </div>
    <script src="../../scripts/public.js"></script>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../Scripts/layer/layer.js"></script>
    <script src="../../Scripts/jquery.tmpl.js"></script>
   
    <link href="../../Scripts/HoneySwitch/honeySwitch.css" rel="stylesheet" />
    <script src="../../Scripts/HoneySwitch/honeySwitch.js"></script>
    <script src="../../Scripts/WebCenter/CourSortMan.js"></script>
</body>
</html>

<script>
    var Id = getQueryString('Id');
    var Name = getQueryString('Name');
    var SectionId = getQueryString('SectionId');
    var IsEnable = Number(getQueryString('IsEnable'));
    var index = parent.layer.getFrameIndex(window.name);//索引

    $(function () {
        if (Name != null && Name != '') {
            $("#CourseTypeName").val(Name);
        }
        
        if (IsEnable == 1) {
            honeySwitch.showOff($('#AwardSwich'));
            UI_Course.IsEnable = 1;

        }
        else {
            honeySwitch.showOn($('#AwardSwich'));
            UI_Course.IsEnable = 0;
        }

        $('#AwardSwich').on('click', function () {

            switch (UI_Course.IsEnable) {
                case 0:
                    UI_Course.IsEnable = 1;
                    break;
                case 1:
                    UI_Course.IsEnable = 0;
                    break;
                default:

            }
        });
    });

    //提交按钮
    function submit() {

        if (Id != null && Id != '') {

            var CourseTypeName = $("#CourseTypeName").val();
            if (CourseTypeName == "") {
                layer.msg("分类名称不能为空");
                return false;
            }
            UI_Course.Name = CourseTypeName;
            UI_Course.Id = Id;
            UI_Course.Edit_Data_Compeate = function (flg, msg) {
                if (flg) {
                    parent.layer.msg('操作成功!');
                    parent.GetCourse_Type();
                    parent.layer.close(index);
                }
                else {
                    parent.layer.msg(msg);
                }
            };
            UI_Course.Edit_Data();


        }
        else {

            var CourseTypeName = $("#CourseTypeName").val();
            if (CourseTypeName == "") {
                layer.msg("课程类型名称不能为空");
                return false;
            }
            UI_Course.CourseTypeName = CourseTypeName;
            UI_Course.SectionId = SectionId;
            UI_Course.Add_Data_Compeate = function (flg, msg) {
                parent.layer.msg('操作成功!');
                parent.GetCourse_Type();
                parent.layer.close(index);
            };
            UI_Course.Add_Data();
        }
    }


    //取消按钮
    function cancel() {
        parent.layer.close(index);
    }
</script>
