var HanderServiceUrl = "http://192.168.1.156:8012/Service/";
var login_User = GetLoginUser();
/**日期转换成时间字符串**/
/** date日期  **/
/** format要转换的字符串格式，默认为 "yyyy-MM-dd"格式，若没有需要的格式可自己添加 **/
function DateTimeConvert(date, format, iseval) {
    iseval = iseval == false ? false : true;
    if (iseval) {
        date = date.replace(/(^\s*)|(\s*$)/g, "");
        if (date == "") {
            return "";
        }
        if (date.indexOf("Date") != -1) {
            if (date.indexOf("-") != -1) {
                date = date.replace("\/Date\(", "").replace("\)\/", "");
                date = new Date(Number(date));
            } else {
                date = eval(date.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
            }
        } else {
            date = new Date(Date.parse(date));
        }
    }
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString();
    var twoMonth = month.length == 1 ? "0" + month : month; //月份为1位数时，前面加0
    var day = (date.getDate()).toString();
    var twoDay = day.length == 1 ? "0" + day : day; //天数为1位数时，前面加0
    var hour = (date.getHours()).toString();
    var twoHour = hour.length == 1 ? "0" + hour : hour; //小时数为1位数时，前面加0
    var minute = (date.getMinutes()).toString();
    var twoMinute = minute.length == 1 ? "0" + minute : minute; //分钟数为1位数时，前面加0
    var second = (date.getSeconds()).toString();
    var twoSecond = second.length == 1 ? "0" + second : second; //秒数为1位数时，前面加0
    var dateTime;
    if (format == "yyyy-MM-dd HH:mm:ss") {
        dateTime = year + "-" + twoMonth + "-" + twoDay + " " + twoHour + ":" + twoMinute + ":" + twoSecond;
    } else if (format == "yyyy-MM-dd HH:mm") {
        dateTime = year + "-" + twoMonth + "-" + twoDay + " " + twoHour + ":" + twoMinute;
    } else if (format == "年月日") {
        dateTime = year + "年" + month + "月" + day + "日";
    } else if (format == "yyyy-MM") {
        dateTime = year + "-" + twoMonth
    } else if (format == "MM-dd") {
        dateTime = twoMonth + "-" + twoDay
    }
    else if (format == "yy-MM") {
        dateTime = JSON.stringify(year).substring(2, 4) + "-" + twoMonth
    }
    else if (format == "MM") {
        dateTime = twoMonth
    }
    else if (format == "dd") {
        dateTime = twoDay;
    }
    else {
        dateTime = year + "-" + twoMonth + "-" + twoDay
    }
    return dateTime;
}

//获取URL参数
function GetUrlDate(str) {
    str = arguments[0] || location.href;
    var name, value;
    var num = str.indexOf("?")
    str = str.substr(num + 1); //取得所有参数   stringvar.substr(start [, length ]

    var arr = str.split("&"); //各个参数放到数组里
    for (var i = 0; i < arr.length; i++) {
        num = arr[i].indexOf("=");
        if (num > 0) {
            name = arr[i].substring(0, num);
            value = arr[i].substr(num + 1);
            this[name] = value;
        }
    }
}
//js 获取地址栏参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]);
    return null;
}
function Arabia_To_SimplifiedChinese(Num) {
    for (i = Num.length - 1; i >= 0; i--) {
        Num = Num.replace(",", "")//替换Num中的“,”
        Num = Num.replace(" ", "")//替换Num中的空格
    }
    if (isNaN(Num)) { //验证输入的字符是否为数字
        //alert("请检查小写金额是否正确");
        return;
    }
    //字符处理完毕后开始转换，采用前后两部分分别转换
    part = String(Num).split(".");
    newchar = "";
    //小数点前进行转化
    for (i = part[0].length - 1; i >= 0; i--) {
        if (part[0].length > 10) {
            //alert("位数过大，无法计算");
            return "";
        }//若数量超过拾亿单位，提示
        tmpnewchar = ""
        perchar = part[0].charAt(i);
        //
        switch (perchar) {
            case "0": tmpnewchar = "零" + tmpnewchar; break;
            case "1": tmpnewchar = "一" + tmpnewchar; break;
            case "2": tmpnewchar = "二" + tmpnewchar; break;
            case "3": tmpnewchar = "三" + tmpnewchar; break;
            case "4": tmpnewchar = "四" + tmpnewchar; break;
            case "5": tmpnewchar = "五" + tmpnewchar; break;
            case "6": tmpnewchar = "六" + tmpnewchar; break;
            case "7": tmpnewchar = "七" + tmpnewchar; break;
            case "8": tmpnewchar = "八" + tmpnewchar; break;
            case "9": tmpnewchar = "九" + tmpnewchar; break;
        }
        switch (part[0].length - i - 1) {
            case 0: tmpnewchar = tmpnewchar; break;
            case 1: if (perchar != 0) tmpnewchar = tmpnewchar + "十"; break;
            case 2: if (perchar != 0) tmpnewchar = tmpnewchar + "百"; break;
            case 3: if (perchar != 0) tmpnewchar = tmpnewchar + "千"; break;
            case 4: tmpnewchar = tmpnewchar + "万"; break;
            case 5: if (perchar != 0) tmpnewchar = tmpnewchar + "十"; break;
            case 6: if (perchar != 0) tmpnewchar = tmpnewchar + "百"; break;
            case 7: if (perchar != 0) tmpnewchar = tmpnewchar + "千"; break;
            case 8: tmpnewchar = tmpnewchar + "亿"; break;
            case 9: tmpnewchar = tmpnewchar + "十"; break;
        }
        newchar = tmpnewchar + newchar;
    }
    //替换所有无用汉字，直到没有此类无用的数字为止
    while (newchar.search("零零") != -1 || newchar.search("零亿") != -1 || newchar.search("亿万") != -1 || newchar.search("零万") != -1) {
        newchar = newchar.replace("零亿", "亿");
        newchar = newchar.replace("亿万", "亿");
        newchar = newchar.replace("零万", "万");
        newchar = newchar.replace("零零", "零");
    }
    //替换以“一十”开头的，为“十”
    if (newchar.indexOf("一十") == 0) {
        newchar = newchar.substr(1);
    }
    //替换以“零”结尾的，为“”
    if (newchar.lastIndexOf("零") == newchar.length - 1) {
        newchar = newchar.substr(0, newchar.length - 1);
    }
    return newchar;
}
$(function () {
    var href = window.location.href;
    if (href.indexOf("Login.html") != -1 || href.indexOf("Register.html") != -1) {
        return;
    }
    var cookie_Userinfo = localStorage.getItem('Userinfo_LG');
    if (cookie_Userinfo == null || cookie_Userinfo == "null") {
        window.location.href = "Login.html?id=" + UrlDate.id;
    }
    GetHead_ReadCount(); //设置底部互动反馈红点
});
function GetHead_ReadCount() {
    var user = JSON.parse(localStorage.getItem('Userinfo_LG'));
    var $feed_a = $('.footer a:nth-child(2)');
    if ($feed_a) {
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/InteractFeed/Feed_DiscussHandler.ashx",
            data: {
                Func: "GetHead_ReadCount",
                LoginUID: user.UniqueNo,
            },
            dataType: "json",
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    var rtndata = json.result.retData;
                    if (parseInt(rtndata) > 0) {
                        $feed_a.find('b').show();
                    } else {
                        $feed_a.find('b').hide();
                    }
                    if ($("#span_IxReadCount").length) {
                        $("#span_IxReadCount").html(rtndata);
                    }
                }
            },
            error: function (errMsg) { }
        });
    }
}
//设置localStorage的值
function setItem(key, value) {
    localStorage.setItem(key, value);
}

//获取localStorage的值
function getItem(key) {
    localStorage.getItem(key);
}
function GetLoginUser() {
    return JSON.parse(localStorage.getItem('Userinfo_LG'));
}
function MesTips(MesContent) {
    $('body').append('<div class="screen_success"><div class="wenzi"></div></div>');
    $('.screen_success .wenzi').html(MesContent);

    setTimeout(function () {
        $('.screen_success').remove();
    }, 2000);
}
/****************************************互动反馈开始*****************************************/
//定位最后一条讨论内容
function Position_LastDiscuss() {
    var $objDis = $("#div_Discuss div.useposition:last"); //找到要定位的讨论内容            
    var objDis = $objDis[0]; //转化为dom对象 
    objDis.scrollIntoView(true);
    // $("#div_Discuss").parent().animate({ scrollTop: objDis.offsetTop }, "slow"); //定位

}
//添加新的讨论信息
function AddToDiscuss(type) {
    type = arguments[0] || 0; //0实名、教师匿名；1学生匿名 
    $("#div_Discuss").append('<div class="myspeak_wrap useposition">'
        + '<div class="ren"><i></i><img src="' + HanderServiceUrl + login_User.HeadPic + '" onerror="javascript:this.src=\'images/ren.jpg\'"/></div>'
        + '<div class="myspeak">' + (type == 1 ? '<span style="color:#FC0301;">(待审核)</span>' : "") + $("#emojiInput").val().trim() + '<p>' + DateTimeConvert(new Date(), 'yyyy-MM-dd HH:mm:ss', false) + '</p></div></div>');
    Position_LastDiscuss();
    $("#emojiInput").val('');
}
//匿名讨论设置为已读
function SetAnonymity_DiscussRead(anoid) {
    $.ajax({
        url: HanderServiceUrl + "/InteractFeed/Feed_DiscussHandler.ashx",
        type: "post",
        dataType: "json",
        data: {
            Func: "SetAnonymity_DiscussRead",
            AnonymityId: anoid
        },
        success: function (json) {
            GetHead_ReadCount();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { }
    });
}
/****************************************互动反馈结束*****************************************/

/****************************************评价相关开始*****************************************/
//排序
function TaskInfoOrder(retData) {
    var eva_detail_list = retData.eva_detail_list;
    if (eva_detail_list != null) {
        for (var i = 0; i < eva_detail_list.length; i++) {
            var indicator_list = eva_detail_list[i].indicator_list;
            for (var j = 0; j < indicator_list.length; j++) {
                eva_detail_list[i]["QuesType_Id"] = indicator_list[j].QuesType_Id;
            }
        }
    }
    eva_detail_list = Enumerable.From(eva_detail_list).OrderBy("item=>item.QuesType_Id").ToArray();//按题的类型排序，若是问答题则 排列在最下边
    return eva_detail_list;
}
var total = 0;//试卷总分数
function GetScore() { //答题-设置分数
    $(".indicatype").each(function () {
        var indiScore = 0;//指标库分类分数
        var indiId = $(this).attr("id").replace("div_indi_", "");
        //求最大分
        $(this).find(".maxques").each(function () {
            var numbers1 = [];
            $(this).find(".numbers").each(function () {
                numbers1.push($(this).html());
            })
            var max = Math.max.apply(null, numbers1);
            $("#sp_" + $(this).attr("id").replace("ul_ques_", "")).html(max);
            indiScore = indiScore + max;
        });
        $("#h_" + indiId).html(indiScore);
        total = total + indiScore
    });
    //总分
    //$("#sp_total").html('总分：' + total + '分')
}
function GetScore_Answer() { //查看试卷-设置分数
    GetScore();
    var realTotal = 0;//实时总分
    $(".radios li").each(function () {
        if ($(this).hasClass('on')) {
            realTotal = numAdd(realTotal, $(this).find('b.numbers').html());
        }
    });
    $("#sp_realtotal").html(realTotal);
}
function InitControl(isscore) { //答题-控件初始化
    isscore = arguments[0] || 0;
    //多选题 修改
    $('.checkboxs li').on('tap', function () {
        if ($(this).hasClass('on')) {
            $(this).removeClass('on');
            $(this).find('input').prop('checked', false);
        } else {
            $(this).addClass('on');
            $(this).find('input').prop('checked', true);
        }
    });
    //单选题
    $('.radios li').on('tap', function () {
        $(this).addClass('on').siblings().removeClass('on');
        $(this).find('input').prop('checked', true);
        if (isscore == 0) {
            var realTotal = 0;//实时总分
            $(".radios li").each(function () {
                if ($(this).hasClass('on')) {
                    realTotal = numAdd(realTotal, $(this).find('b.numbers').html());
                }
            });
            $("#sp_realtotal").html(realTotal);
        }
    });
    $('textarea').on("focus", function () {
        $('.header,.test_time').css('position', 'absolute');
    }).on('blur', function () {
        $('.header,.test_time').css('position', 'fixed');
    })
}
function InitControl_Answer() { //查看试卷-控件初始化
    $('textarea').each(function () {
        $(this).attr("disabled", "disabled");
    });
}
/****************************************评价相关结束*****************************************/

function nomessage(id, top, bottom) {
    top = top || '0';
    bottom = bottom || '0';
    $('#' + id).html('<div style="width:100%;position:absolute;top:' + top + 'px;bottom:' + bottom + 'px;background:#fff url(images/nomessage.png) no-repeat center;background-size:60% auto;"></div>');
}
function numAdd(num1, num2) {
    var baseNum, baseNum1, baseNum2;
    try {
        baseNum1 = num1.toString().split(".")[1].length;
    } catch (e) {
        baseNum1 = 0;
    }
    try {
        baseNum2 = num2.toString().split(".")[1].length;
    } catch (e) {
        baseNum2 = 0;
    }
    baseNum = Math.pow(10, Math.max(baseNum1, baseNum2));
    return (num1 * baseNum + num2 * baseNum) / baseNum;
};

/*var STATE = 'x-back';
var element;

var onPopState = function (event) {
    event.state === STATE && fire();
    record(STATE);  //初始化事件时，push一下
}

var record = function (state) {
    history.pushState(state, null, location.href);
}

var fire = function () {
    var event = document.createEvent('Events');
    event.initEvent(STATE, false, false);
    element.dispatchEvent(event);
}

var listen = function (listener) {
    element.addEventListener(STATE, listener, false);
}

!function () {
    element = document.createElement('span');
    window.addEventListener('popstate', onPopState);
    this.listen = listen;
    record(STATE);
}.call(window[pkg] = window[pkg] || {});

XBack.listen(function(){

})*/