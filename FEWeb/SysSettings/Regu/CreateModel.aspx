<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateModel.aspx.cs" Inherits="FEWeb.SysSettings.Regu.CreateModel" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
    <style>
       .label{display: inline-block;
    min-width: 110px;
    height: 35px;
    text-align: left;
    font-size: 15px;
    color: #555;
    float: left;
    line-height: 35px;}
    </style>
</head>
<body >
    <div class="main" id="newEval">
        <div class="input-wrap">
            <label>评价名称：</label>
            <input type="text" class="text" readonly="readonly" id="name" value="" placeholder="请填写评价名称" style="width:333px;"/>
        </div>
        <div class="input-wrap">
            <label>学年学期：</label>
            <select class="select ml10" style="width:335px;" id="section">
                <option value="0">全部</option>
            </select>
        </div>
        <div class="input-wrap">
            <label>起止时间：</label>
            <input type="text" id="StartTime" name="StartTime" class="text ml10 Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 150px; margin-left: 10px;" />
            <span style="padding-left: 10px;">~</span>
            <input type="text" id="EndTime" name="EndTime" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 150px;" />
        </div>
        <div class="input-wrap pr">
            <label>评价表分配：</label>
            <select class="select ml10" style="width:335px;">
                         
            </select>
        </div>
        <div class="input-wrap clearfix" v-if="role==1" v-cloak>
            <label>评价范围：</label>
            <span class="ml10">
                <input type="radio" name="rank" id="all" class="magic-radio"  v-model="picked" value="0" @change="DepartToggle">
                <label for="all">全校</label>
            </span>
            <span class="ml10">
                <input type="radio" name="rank" id="appoint" class="magic-radio" v-model="picked" value="1" @change="DepartToggle">
                <label for="appoint">指定部门</label>
            </span>
        </div>
        <div class="input-wrap1 pb20" v-cloak v-show="appoint">
            <label class="label"></label>
            <select id="sel_section" data-placeholder="选择部门" class="chosen-select select" multiple="multiple">
                <option value="0">q</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
            </select>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="保存" class="btn" onclick="submit()" />
        <input type="button" value="取消" class="btna" onclick="parent.CloseIFrameWindow();" />
    </div>
    <link rel="stylesheet" href="../../Scripts/choosen/prism.css" />
    <link rel="stylesheet" href="../../Scripts/choosen/chosen.css" />
    <script src="../../js/vue.min.js"></script>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../scripts/public.js"></script>
    <script src="../../scripts/jquery.linq.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../scripts/layer/layer.js"></script>
    <script src="../../scripts/jquery.tmpl.js"></script>
    <script src="../../Scripts/WebCenter/Base.js"></script>
    <script src="../../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../../Scripts/choosen/prism.js"></script>
    <script type="text/javascript" src="../../scripts/My97DatePicker/WdatePicker.js"></script>
    <script>
        var newEval = new Vue({
            el: '#newEval',
            data: {
                role: "",
                appoint: false,
                picked:'0'
            },
            methods: {
                DepartToggle: function () {
                    this.picked == 1 ? this.appoint = true : this.appoint = false
                }
            },
            mounted: function () {
                this.role = GetLoginUser().Sys_Role_Id;
                Base.bindStudySection();
                $("#sel_section").chosen({
                    allow_single_deselect: true,
                    disable_search_threshold: 6,
                    no_results_text: '未找到',
                    width: '335px'
                })
               
            }
        })
        
    </script>
</body>
</html>
