(function () {
    $(function () {
        showTime();
        var cookie_Userinfo = JSON.parse(localStorage.getItem('Userinfo_LG'));
        if (cookie_Userinfo != null) {
            $("#userName").html(cookie_Userinfo.Sys_Role + "：" + cookie_Userinfo.Name + '<i class="iconfont">&#xe659;</i>');
            var cookie_UserList = JSON.parse(localStorage.getItem('Userinfos'));
            if (cookie_UserList != null) {
                for (var i = 0; i < cookie_UserList.length; i++) {
                    var userinfo = cookie_UserList[i];
                    if (cookie_Userinfo.Sys_Role_Id != userinfo.Sys_Role_Id) {
                        $("#roleList").prepend("<a href=\"javascript:void(0);\" class=\"changerole\" id='" + userinfo.Sys_Role_Id + "'>" + userinfo.Sys_Role + "：" + userinfo.Name + "</a>");
                    }
                }
            }
        }
        changeRole();
        loginOut();
        BindOneNav();
    })
    function changeRole() {
        $(".changerole").click(function(){
            var roleId = $(this).attr("id");
            var cookie_UserList = JSON.parse(localStorage.getItem('Userinfos'));
            if (cookie_UserList != null) {
                for (var i = 0; i < cookie_UserList.length; i++) {
                    var userinfo = cookie_UserList[i];
                    if (userinfo.Sys_Role_Id == roleId) {
                        localStorage.setItem('Userinfo_LG', JSON.stringify(userinfo));
                        window.location.href = window.location.href;
                    }
                }
            }
        });
    }
    //绑定一级导航
    function BindOneNav() {
        $('#onenav,#ul_twonav').html('');
        var oneNav = getNav(0);
        $(oneNav).each(function (i,n) {
            n['child'] = getNav(n.ID);
            if (n['child'].length > 0) {
                $('#onenav').append('<li id="li_top_' + n.ID + '" class="' + (n.child[0].Url == cururl ? "selected" : "") + '"><a href="' + n.child[0].Url + '?Id=' + n.ID + '&Iid=' + n.child[0].ID+ '"><i class="iconfont">' + n.IconClass + '</i><span>' + n.Name + '</span></a></li>');
            } else {
                $('#onenav').append('<li id="li_top_' + n.ID + '" class="' + (n.Url == cururl ? "selected" : "") + '"><a href="' + n.Url + '?Id=' + n.ID + '"><i class="iconfont">' + n.IconClass + '</i><span>' + n.Name + '</span></a></li>');
            }
        })
        if ($('#ul_twonav')) {
            BindTwoNav();
            navWidth();
        }
    }
    //绑定一级导航
    function BindTwoNav() {
        var Id = getQueryString('Id'), Iid = getQueryString('Iid');
        var twoNav = getNav(Id);
        $(twoNav).each(function (i, item) {
            $("#ul_twonav").append('<li id="li_twonav_' + item.ID + '" class="' + (item.Url == cururl ? "selected" : "") + '"><a href="' + item.Url + '?Id=' + item.Pid + '&Iid=' + item.ID + '"><div><i class="iconfont">' + item.IconClass + '</i><span>' + item.Name + '</span></div></a></li>')
        })
        $("#li_top_" + Id).addClass('selected').siblings().removeClass('selected');
        if (Iid != 'null') {
            $("#li_twonav_" + Iid).addClass('selected').siblings().removeClass('selected');
        }
    }
   
    function loginOut() {
        $('.login_area').hover(function () {
            $(this).find('.iconfont').html('&#xe6ac;');
            $(this).find('.loginout').slideDown();
            $('#loginout').click(function () {
                localStorage.removeItem('Userinfo_LG');
                localStorage.removeItem('Userinfos');
                localStorage.removeItem('Menu_Btns');
                localStorage.removeItem('navAry');
                localStorage.removeItem('LoginTime');
                
                window.location.href = '/Login.aspx';
            })
        }, function () {
            $(this).find('.iconfont').html('&#xe659;');
            $(this).find('.loginout').stop().slideUp();
        });

    }
    function showTime() {
        var date = new Date();
        var year = date.getFullYear();
        var month = (date.getMonth() + 1).toString();
        var twoMonth = month.length == 1 ? "0" + month : month; //月份为1位数时，前面加0
        var day = (date.getDate()).toString();
        var twoDay = day.length == 1 ? "0" + day : day; //天数为1位数时，前面加0
        $('#timer').html(year + '年' + twoMonth + '月' + twoDay + '日');
    }
    //计算导航的宽度
    function navWidth() {
        var len = $('.nav_detail ul li').length;
        $('.nav_detail ul li').width(100 / len + '%');
    }

})();