<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClassInfo.aspx.cs" Inherits="FEWeb.SysSettings.AddClassInfo" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"><link href="/images/favicon.ico" rel="shortcut icon">
    <title>用户管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/layout.css" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
  
</head>
<body>
    <div class="main">
        <div style="width: 80%; margin-left: 18%;">
            <div class="input-wrap">
                <label>年度：</label><select id="nd" class="select" name="nd">
                    <option value="2015">2015</option>
                    <option value="2016">2016</option>
                    <option value="2017">2017</option>
                </select>
            </div>
            <div class="input-wrap">
                <label>季别：</label><select id="jb" class="select" name="jb">
                    <option value="春">春</option>
                    <option value="秋">秋</option>
                </select>
            </div>
            <div class="input-wrap">
                <label>课程名称：</label><input type="text" class="text" id="kcmc" name="kcmc" />
                <span class='info kcmc_put'>请输入课程名称。<p></p>
                </span>
                <span class='info_error kcmc_error'>请输入课程名称！<p></p>
                </span>
                <span class='ok kcmc_ok'></span>
            </div>
            <div class="input-wrap">
                <label>年级：</label><select id="nj" class="select" name="nj">
                    <option value="2013">2013</option>
                    <option value="2014">2014</option>
                    <option value="2015">2015</option>
                    <option value="2016">2016</option>
                    <option value="2017">2017</option>
                </select>
            </div>
            <div class="input-wrap">
                <label>部门名称：</label><input type="text" class="text" id="zybmmc" name="zybmmc" />
            </div>
            <div class="input-wrap">
                <label>教师姓名：</label><input type="text" class="text" id="jsxm" name="jsxm" />
            </div>
            <div class="input-wrap">
                <label>教师性质：</label><input type="text" class="text" id="xzmc" name="xzmc" />
            </div>
        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="保存" class="btn" />
        <input type="button" value="取消" class="btna" />
    </div>
     <script src="../Scripts/Common.js"></script>
      <script src="../scripts/public.js"></script>
   
    <script src="../Scripts/jquery.linq.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
</body>
</html>
