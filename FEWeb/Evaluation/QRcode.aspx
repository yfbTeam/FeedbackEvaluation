<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRcode.aspx.cs" Inherits="FEWeb.Evaluation.QRcode" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    
    <style type="text/css">
        .bot {
            text-align:center;
            margin-top:10px;
            color:#666;
        }
    </style>

</head>
<body>
    <div>
        <div id="qrcode" style="width: 200px; height: 200px; margin: auto; margin-top: 20px"></div>
        <div class="bot">
            该二维码将在<span id="sp_endtime"></span>结束
        </div>
    </div>
     <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/public.js"></script>
   
    <script src="../scripts/public.js"></script>
    <script src="../scripts/layer/layer.js"></script>
    <script src="../Scripts/qrcode.min.js"></script>
    <script type="text/javascript">
        var qrcode = new QRCode(document.getElementById("qrcode"), {
            width: 200,
            height: 200
        });


        function makeCode() {
            var elText = getQueryString("url");
            qrcode.makeCode(elText);
        }
        var StartTime = getQueryString("StartTime");
        var EndTime = getQueryString("EndTime");
        $("#sp_endtime").html(DateTimeConvert(EndTime, 'yyyy-MM-dd', true));
        makeCode();
    </script>
</body>
</html>
