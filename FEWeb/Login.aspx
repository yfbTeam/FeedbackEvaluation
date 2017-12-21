<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FEWeb.Login" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <link href="images/favicon.ico" rel="shortcut icon">
    <title>登录</title>
    <link href="css/reset.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet"/>
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    
    <style type="text/css">
        /*iconfont*/
.iconfont {
    display: inline-block;
    font: normal normal normal 14px/1 iconfont;
    font-size: inherit;
    text-rendering: auto;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}
@font-face {
  font-family: 'iconfont';  /* project id 162596 */
  src: url('//at.alicdn.com/t/font_162596_gyupmbm6o2mx6r.eot');
  src: url('//at.alicdn.com/t/font_162596_gyupmbm6o2mx6r.eot?#iefix') format('embedded-opentype'),
  url('//at.alicdn.com/t/font_162596_gyupmbm6o2mx6r.woff') format('woff'),
  url('//at.alicdn.com/t/font_162596_gyupmbm6o2mx6r.ttf') format('truetype'),
  url('//at.alicdn.com/t/font_162596_gyupmbm6o2mx6r.svg#iconfont') format('svg');
}
        .Validform_checktip {
            display: block;
            line-height: 25px;
            font-size: 15px;
            color: #fff;
            text-indent: 45px;
        }
        .Validform_wrong {
            color: red;
        }

        .Validform_right {
            color: #91c954;
        }
    </style>
</head>
<body>
    
    <%--<input type="hidden" id="hidPreUrl" runat="server" />--%>
    <div class="header">
        <div class="container clearfix">
            <div class="logo pull-left">
                <img src="images/logo.png" />
            </div>
        </div>
    </div>
    <div class="login_bg">
    <div class="container container1">
            <div class="img">
                <img src="images/zi1.png" class="zi1 animated fadeInUp"/>
                <img src="images/zi2.png" class="zi2 animated fadeInUp"/>
                <img src="images/zi3.png" class="zi3 animated fadeInUp"/>
                
            </div>
            <div class="btns clearfix">
                    <button type="button" class="btnss btn1 pull-left animated fadeInUp" style="margin-right:30px;" >教学业绩管理</button>
                    <button type="button" class="btnss btn2  pull-right animated fadeInUp" style="margin-left:30px;" >教学质量评价</button>
            </div>
            <div id="LoginArea" class="row">
                <div class="col-lg-12">
                    <div class="well bs-component">
                        <form id="LoginForm" class="form-horizontal" name="loginform" >
                            <fieldset>
                                <legend style="text-align:center;border-bottom:0px;">登录</legend>
                                <div class="form-group ">
                                    <div class="col-lg-12">
                                        <i class="fa fa-user fa-lg"></i>
                                        <input type="text" class="form-control" id="txt_loginName" placeholder="登录名" required datatype="*" nullmsg="请输入登录名！">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <i class="fa fa-lock fa-lg"></i>
                                        <input type="password" class="form-control" id="txt_passWord" placeholder="密码" required datatype="*" nullmsg="请输入密码">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-6 col-md-6">
                                        <i class="iconfont">&#xe633;</i>
                                        <input id="inpCode" name="inpCode" type="text" class="form-control" style="ime-mode: disabled" placeholder="请输入验证码" required datatype="iCode" nullmsg="请输入验证码！" errormsg="验证码输入错误！" />
                                    </div>
                                    <div class="col-lg-6 col-md-6">
                                        <input type="hidden" id="hidCode" name="hidCode" />
                                        <span class="form-control" id="checkCode" onclick="createCode()" style="color:blue;padding-left:12px;text-align:center;line-height:34px;font-size:18px;letter-spacing:10px;font-weight:bold;"></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <button id="LoginButton" type="button" class="btn btn-success btn-block">登录</button>
                                    </div>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer id="footer" class="footer">
        
    </footer>
    <script src="Scripts/jquery-1.11.2.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
     <script src="Scripts/Common.js"></script>
    <script src="Scripts/Validform_v5.3.1.js"></script>
    <script src="Scripts/md5.js"></script>
   
    <script type="text/javascript">
       
       
        $(function ()
        {
            $('#footer').load('/footer.html');
            Checkcookie();
          
            createCode();
            //加载验证码

            //回车提交事件
            $("body").keydown(function () {
                if (event.keyCode == "13") {//keyCode=13是回车键
                    $("#LoginButton").click();
                }
            });

            var valiNewForm = $("#LoginForm").Validform({
                datatype: {
                    "iCode": function (gets, obj, curform, regxp) {
                        /*参数gets是获取到的表单元素值，
                          obj为当前表单元素，
                          curform为当前验证的表单，
                          regxp为内置的一些正则表达式的引用。*/
                        var reg1 = regxp["*"];

                        var hidcode = curform.find("#hidCode");
                        if (reg1.test(gets)) { if (hidcode.val().toUpperCase() == gets.toUpperCase()) { return true; } }
                        return false;
                    }
                },
                ajaxPost: true,
                btnSubmit: "#LoginButton",
                tiptype: 3,
                showAllError: false,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    ;
                     Login();
                }
            })
        });
        var code; //在全局 定义验证码
        function createCode() {
            code = "";
            var codeLength = 4;//验证码的长度
            var checkCode = document.getElementById("checkCode");
            checkCode.innerHTML = "";
            var selectChar = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');

            for (var i = 0; i < codeLength; i++) {
                var charIndex = Math.floor(Math.random() * 60);
                code += selectChar[charIndex];
            }
            if (code.length != codeLength) {
                createCode();
            }
            checkCode.innerHTML = code;
            $("#hidCode").val(code);
            //$("#inpCode").val(code);
        }

        function Login()
        {
            var loginName = $("#txt_loginName").val()
            var passWord = $("#txt_passWord").val()
            var isOK = false;
            var postData = { "Func": "Login", "loginName": loginName, "passWord": passWord };
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/Login/LoginHandler.ashx",
                data: postData,
                dataType: "json",
                async: false,
                success: function (returnVal) {

                    if (returnVal.result.errMsg == "success")
                    {
                        isOK = true;//登陆验证通过
                        var data = returnVal.result.retData;
                        Set_AllBtn(data[0].Sys_Role_Id);

                        //把用户信息存在cookie中
                        localStorage.setItem('Userinfos', JSON.stringify(data));
                        localStorage.setItem('LoginTime', Date.parse(new Date()));
                    }
                    else
                    {
                        alert("用户名密码错误");
                        createCode();
                    }
                },
                error: function (errMsg) {
                    parent.layer.msg("登录发生错误");
                    createCode();
                }
            });
            if (isOK) { //如果验证成功
                var user = JSON.parse(localStorage.getItem('Userinfos'));
                var user0 = user[0];
                Set_AllBtn(user0.Sys_Role_Id);
                var teacher = role(user0.UniqueNo, "IsTeacher");
                
                var student = role(user0.UniqueNo, "IsStudent");
               
                if (!teacher && !student) {
                    localStorage.setItem('Userinfo_LG', JSON.stringify(user0));
                }                
                window.location.href = "/Index.aspx";    
            }
        }
        function role(uniqueNo, roleName) {
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/Login/LoginHandler.ashx",
                data: { "Func": roleName, "UniqueNo": uniqueNo },
                dataType: "json",
                async: false,
                success: function (returnVal) {
                    if (returnVal.result.errMsg == "success") {
                        var data = returnVal.result.retData;

                        localStorage.setItem('Userinfo_LG', JSON.stringify(data));
                        
                        //将教师角色加入到集合
                        var user = JSON.parse(localStorage.getItem('Userinfos'));
                        user.push(data);
                        localStorage.setItem('Userinfos', JSON.stringify(user));
                        return true;
                    }
                    else {
                        return false;
                    }
                },
                error: function (errMsg) {
                    return false;
                }
            });
        }
      
        function Checkcookie()
        {
            var cookie_Userinfo = localStorage.getItem('Userinfo_LG');
            var LoginTime = localStorage.getItem('LoginTime');
            var curData = Date.parse(new Date());
            if (cookie_Userinfo != null && cookie_Userinfo != "null" && LoginTime != null && (curData - LoginTime)<=1000*60*60)
            {
                window.location.href = "/Index.aspx";
            }
        }
        function Set_AllBtn(roleid) {          
            $.ajax({
                type: "Post",
                url: HanderServiceUrl + "/SetMenu/SetMenuHandler.ashx",
                data: { "Func": "Get_MenuBtnInfo", Rid: roleid },
                dataType: "json",
                async: false,
                success: function (json) {
                    localStorage.removeItem('Menu_Btns');
                    if (json.result.errNum == 0) {                        
                        var data = json.result.retData;                        
                        localStorage.setItem('Menu_Btns', JSON.stringify(data));                      
                    } else {                       
                        localStorage.setItem('Menu_Btns', "");
                    }
                },
                error: function (errMsg) {}
            });
        }
    </script>
</body>
</html>
