<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEval.aspx.cs" Inherits="FEWeb.SysSettings.Regu.AddEval" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="../../css/layout.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.11.2.min.js"></script>
</head>
<body >
    <div class="main">
        <div class="input-wrap">
            <label>评价名称：</label>
            <input type="text" class="text"  id="name" value="" placeholder="请填写评价名称" style="width:333px;"/>
        </div>      
        <div class="input-wrap">
            <label>起止时间：</label>
            <input type="text" id="StartTime" name="StartTime" class="text ml10 Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 150px; margin-left: 10px;" />
            <span style="padding-left: 10px;">~</span>
            <input type="text" id="EndTime" name="EndTime" class="text Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 150px;" />
        </div>
               
    </div>
    <div class="btnwrap">
        <input type="button" value="保存" class="btn" onclick="submit()" />
        <input type="button" value="取消" class="btna" onclick="parent.CloseIFrameWindow();" />
    </div>
    <script src="../../Scripts/Common.js"></script>
    <script src="../../scripts/public.js"></script>
    <script src="../../scripts/jquery.linq.js"></script>
    <script src="../../Scripts/linq.min.js"></script>
    <script src="../../scripts/layer/layer.js"></script>
    <script src="../../scripts/jquery.tmpl.js"></script>
    <script src="../../Scripts/WebCenter/RegularEval.js"></script>
    <script type="text/javascript" src="../../scripts/My97DatePicker/WdatePicker.js"></script>
</body>
</html>


<script>
    var select_sectionid = parent.select_sectionid;
    var reguType = 1;
    function submit()
    {
        Add_Eva_RegularCompleate = function () {           
            parent.Reflesh();
        };
        Add_Eva_Regular(reguType);
    }
</script>