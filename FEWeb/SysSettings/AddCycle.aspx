<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCycle.aspx.cs" Inherits="FEWeb.SysSettings.AddCycle" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <link href="/images/favicon.ico" rel="shortcut icon">
    <title>新增指标</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    
</head>
<body>
    <div class="main" >
        <div style="width:80%;margin-left:14%;">
            <div class="input-wrap">
                <label>年度：</label><input type="text" class="text Wdate" name="year" onfocus="WdatePicker({dateFmt:'yyyy'})"><span class="info year_put">请选择年度。<p></p></span>
                <span class="info_error year_error">请选择年度！<p></p></span>
                <span class="ok year_ok"></span>
            </div>
            <div class="input-wrap">
                <label>季别：</label>
                <select class="text" name="season">
                    <option value="春">
                        春
                    </option>
                    <option value="秋">
                        秋
                    </option>
                </select>
            </div>
            <div class="input-wrap">
                <label>状态：</label>
                <select name="status" class="text">
                    <option value="0">
                        不启用
                    </option>
                    <option value="1">
                        启用
                    </option>
                </select>
            </div>
            <div class="input-wrap">
                <label>备注：</label><textarea name="remark" style="height:80px;" class="text"></textarea>
            </div>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="提交" onclick="submit()" class="btn" />
        <input type="button" value="取消" class="btna" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
     <script src="../Scripts/Common.js"></script>
       <script src="../scripts/public.js"></script>
   
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
