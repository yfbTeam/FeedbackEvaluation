(function (window) {
    var theUA = window.navigator.userAgent.toLowerCase();
    if ((theUA.match(/msie\s\d+/) && theUA.match(/msie\s\d+/)[0]) || (theUA.match(/trident\s?\d+/) && theUA.match(/trident\s?\d+/)[0])) {
        var ieVersion = theUA.match(/msie\s\d+/)[0].match(/\d+/)[0] || theUA.match(/trident\s?\d+/)[0];
        if (ieVersion < 9) {
            var str = "你的浏览器版本太low了\n已经和时代脱轨了";
            var str2 = "推荐使用:<a href='https://www.baidu.com/s?ie=UTF-8&wd=%E8%B0%B7%E6%AD%8C%E6%B5%8F%E8%A7%88%E5%99%A8' target='_blank' style='color:#cc0'>谷歌</a>,"
            + "<a href='https://www.baidu.com/s?ie=UTF-8&wd=%E7%81%AB%E7%8B%90%E6%B5%8F%E8%A7%88%E5%99%A8' target='_blank' style='color:#cc0'>火狐</a>,"
            + "<a href='https://www.baidu.com/s?ie=UTF-8&wd=%E7%8C%8E%E8%B1%B9%E6%B5%8F%E8%A7%88%E5%99%A8' target='_blank' style='color:#cc0'>猎豹</a>,其他双核急速模式";
            document.writeln("<pre style='text-align:center;color:#333;line-height:40px;font-size:18px; height:100%;border:0;position:fixed;top:0;left:0;width:100%;z-index:1234'>" +
            "<h2 style='padding-top:200px;margin:0'><strong>" + str + "<br/></strong></h2><p>" +
            str2 + "</p><h2 style='margin:0'><strong>如果你的使用的是双核浏览器,请切换到极速模式访问<br/></strong></h2></pre>");
            document.execCommand("Stop");
        };
    }
})(window);

~(function () {
    //->初始化内容区域高度
    function initContaniner() {
        var winHeight = $(window).height();
        var footerHeight = $('#footer').height();
        var headerHeight = $('#header').outerHeight();
        if (footerHeight || headerHeight) {
            if ($('#index_body')) {
                $('#index_body').css('minHeight', winHeight - footerHeight - headerHeight-140 + 'px');
            }
            if ($('#centerwrap')) {
                $('#centerwrap').css('minHeight', winHeight - footerHeight - 262 + 'px');
                $('#centerwrap>.wrap').css('minHeight', winHeight - footerHeight - 303 + 'px');
            }
        }
    }
    $(function () {
        initContaniner();
        $(window).resize(function () {
            initContaniner();
        })
    })
})();
//检测是否登录
var cookie_Userinfo = JSON.parse(localStorage.getItem('Userinfo_LG'));
var LoginTime = localStorage.getItem('LoginTime');
var curData = Date.parse(new Date());
if (cookie_Userinfo == null || (curData - LoginTime) > 1000 * 60 * 60) {
    localStorage.removeItem('Userinfo_LG');
    localStorage.removeItem('Userinfos');
    localStorage.removeItem('Menu_Btns');
    localStorage.removeItem('navAry');
    localStorage.removeItem('LoginTime');
    parent.parent.parent.location.href = "/Login.aspx";
}
var cururl = getUrl(cururl);
var login_User = GetLoginUser();
localStorage.setItem('navAry', JSON.stringify(getMenuByRoleid(login_User.Sys_Role_Id)));
var items = JSON.parse(localStorage.getItem('navAry'));
$(function () {
    BindThreeNav();
})
//根据条件获取导航
function getNav(Id) {
    return items.filter(function (item) {
        return item.Pid == Id;
    })
}
//根据1级导航获取3级导航
function get3rdNavBy1st(fristnavId) {
    var twoAry = getNav(fristnavId);
    if (twoAry.length > 0) {
        return getNav(twoAry[0].ID);
    }
}
function BindThreeNav() {
    $('#threenav').html('');
    var threeNav = [];
    var Id = getQueryString('Id'), Iid = getQueryString('Iid');
    if (Iid == null) {
        threeNav = get3rdNavBy1st(Id)
        $(threeNav).each(function (index1, elem) {
            $('#threenav').append('<a href="' + elem.Url + '?Id=' + Id + '&Iid=' + elem.Pid + '" class="' + (elem.Url == cururl ? "selected" : "") + '">' + elem.Name + '</a>')
        })
    } else {
        threeNav = getNav(Iid);
        $(threeNav).each(function (i, item) {
            $('#threenav').append('<a href="' + item.Url + '?Id=' + Id + '&Iid=' + Iid + '" class="' + (item.Url == cururl ? "selected" : "") + '">' + item.Name + '</a>')
        })
    }
}
//表格操作
function tableSlide() {
    $('.operate').hover(function () {
        $(this).find('.operate_none').slideDown('400');
    }, function () {
        $(this).find('.operate_none').stop(true, true).slideUp('400');
    })
}
//分类导航分配
function navTab(tabobj, tarobj) {
    $(tabobj).children().click(function () {
        var n = $(this).index();
        $(this).addClass('selected').siblings().removeClass('selected');

        $(tarobj).children().eq(n).show().siblings().hide();
    })
}
//序号
var pageNum = 1;
function pageIndex() {
    return pageNum++;
}
/****************************************结束********************************************************/


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
function nomessage(id,subele, size, height) {
    size = size || 19;
    height = height || 480;
    subele = subele || 'tr';
    if (subele == 'tr') {
        $(id).append('<tr><td colspan="100" style="border-bottom:none;"><div style="width:100%;background:url(/images/nomessage.png) no-repeat center center;height:' + height + 'px;background-size:' + size + '% auto;"></div></td></tr>');
        $(id).children('tr').hover(function () {
            $(this).css('background', '#fff');
        })
    } else if (subele == 'li') {
        $(id).append('<li style="width:100%;background:url(/images/nomessage.png) no-repeat center center;height:' + height + 'px;background-size:' + size + '% auto;"></li>');
    } else {
        $(id).append('<div style="width:100%;background:url(/images/nomessage.png) no-repeat center center;height:' + height + 'px;background-size:' + size + '% auto;"></div>');
    }
}

function accAdd(arg1, arg2) {
    var r1, r2, m;
    try {
        r1 = arg1.toString().split(".")[1].length;
    }
    catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    }
    catch (e) {
        r2 = 0;
    }
    m = Math.pow(10, Math.max(r1, r2));
    return (arg1 * m + arg2 * m) / m;
}

//获取指标分类【判定当前身份应该获取的指标类型】
function get_IndicatorType_by_rid() {
    var P_Type = 0;

    var cookie_Userinfo = localStorage.getItem('Userinfo_LG');
    var jsoncookie_Userinfo = JSON.parse(cookie_Userinfo);
    var rid = jsoncookie_Userinfo.Sys_Role_Id;
    if (rid == 10) {
        //指标库的 P_Type 1:公用指标  2:专用指标
        P_Type = 2;
    }
    if (rid == 16) {
        P_Type = 1;
    }
    return P_Type;
}
//rid: 10:督导管理员  1:超级管理员 16:学生信息管理员
//获取表格分类【判定当前身份应该获取的表格类型】
function get_Eva_Role_by_rid() {
    var Eva_Role = 0;
    var cookie_Userinfo = localStorage.getItem('Userinfo_LG');
    var jsoncookie_Userinfo = JSON.parse(cookie_Userinfo);
    var rid = jsoncookie_Userinfo.Sys_Role_Id;
    if (rid == 10) {
        //表格的Type 1:学生用表  2:专用用表
        Eva_Role = 2;
    }
    if (rid == 16) {
        Eva_Role = 1;
    }
    if (rid == 19 || rid == 9) {
        Eva_Role = 2;
    }
    if (rid == 1 || rid == 6) {
        Eva_Role = 0;
    }

    return Eva_Role;
}
//获取地址栏页面链接
function getUrl(cururl) {
    var host = 'http://' + window.location.host || 'https://'+window.location.host;
    var href = window.location.href;
    cururl = href.split(host)[1];
    if (cururl.indexOf("?") != -1) {
        var queindex = cururl.lastIndexOf("?");
        cururl = cururl.substring(0, queindex);
    }
    return cururl;
}
//辅助方法---------------------------------------------------------------------------------------------------------------------------------------------------
function isHasElement(arr, value) {
    var str = arr.toString();
    var index = str.indexOf(value);
    if (index >= 0) {
        //存在返回索引 
        //"(^"+value+",)|(,"+value+",)|(,"+value+"$)" 
        value = value.toString().replace(/(\[|\])/g, "\\$1");
        var reg1 = new RegExp("((^|,)" + value + "(,|$))", "gi");
        return str.replace(reg1, "$2@$3").replace(/[^,@]/g, "").indexOf("@");
    } else {
        return -1;//不存在此项 
    }
}
//按钮权限
var Cur_PageBtn = [];
function Get_PageBtn(url, elem) {
    elem = arguments[1] || "a";
    var menu_btns = localStorage.getItem('Menu_Btns');
    if (menu_btns) {
        var btns = JSON.parse(localStorage.getItem('Menu_Btns'));
        var p_page = Enumerable.From(btns).Where("x=>x.Url=='" + url + "'").FirstOrDefault();
        if (p_page) {
            Cur_PageBtn = p_page.Btn;
            $("[mcode^= '" + elem + "']").each(function () {
                if (Enumerable.From(Cur_PageBtn).Where("x=>x.MenuCode=='" + $(this).attr('mcode') + "'").ToArray().length > 0) {
                    $(this).show();
                } else {
                    $(this).remove();
                }
            });            
        }
    }    
}
function JudgeBtn_IsExist(menucode) {
    var btnresult = false;
    if (Cur_PageBtn.length > 0) {
        var btndata = Cur_PageBtn;
        btndata = Enumerable.From(btndata).Where("x=>x.MenuCode=='" + menucode + "'").ToArray();
        btnresult = btndata.length > 0;
    }   
    return btnresult;
}
//上传文件
function UploadFile(func, objid, fileurlid, showobj, exten) {
    func = arguments[0] || "Upload_LetterAtta"; //方法名称
    objid = arguments[1] || "#filePicker"; //上传按钮id
    fileurlid = arguments[2] || "#hid_FileUrl"; //接收文件路径id
    showobj = arguments[3] || "#txt_File"; //显示文件名称控件
    exten = arguments[4] || 'docx,doc,ppt,pptx,pdf,caj,txt,rar,zip,jpg,gif,png,jpeg,xls,xlsx,mp4,mp3,flv,mpeg,mav,mpg';
    WebUploader.create({
        pick: objid,
        formData: { Func: func },
        accept: {
            title: 'Images',
            extensions: exten,
            mimeTypes: 'image/!*'
        },
        duplicate: true,//可重复上传
        auto: true,
        chunked: false,
        chunkSize: 512 * 1024,
        server: '/CommonHandler/UploadHtml5Handler.ashx',
        // 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
        disableGlobalDnd: true,
        //fileNumLimit: 1,
        fileSizeLimit: 200 * 1024 * 1024,    // 200 M
        fileSingleSizeLimit: 200 * 1024 * 1024    // 200 M
    })
   .on('uploadSuccess', function (file, response) {
       var json = $.parseJSON(response._raw);
       var path = json.result.retData;
       if ($(fileurlid) != null) {
           $(fileurlid).val(path);
       }
       if ($(showobj) != null) {
           $(showobj).html(CutFileName(path, 50));
       }
   }).onError = function (code) {
       switch (code) {
           case 'exceed_size':
           case 'Q_EXCEED_SIZE_LIMIT':
               layer.msg('文件大小超出');
               break;
           case 'interrupt':
               layer.msg('上传暂停');
               break;
           default:
               layer.msg('错误: ' + code);
               break;
       }
   };
}
function CutFileName(path, len) {
    len = arguments[1] || 30;
    var filename = path.substr(path.lastIndexOf('/') + 1);
    var exten_str = filename.substr(filename.lastIndexOf("."));//后缀
    var charindex = filename.lastIndexOf('_');
    var showname = charindex > -1 ? filename.substring(0, charindex) + exten_str : filename;
    if (len > 0) {
        showname = showname.length > len ? showname.substr(0, len - 2) + "..." : showname;
    }
    return showname;
}
function get_FileName(path) {
    var filename = path.substr(path.lastIndexOf('/') + 1);
    var exten_str = filename.substr(filename.lastIndexOf("."));//后缀
    var charindex = filename.lastIndexOf('_');
    var showname = charindex > -1 ? filename.substring(0, charindex) + exten_str : filename;
    return showname;
}
//文件下载
function DownLoad(filepath) {
    $.ajax({
        url: "/InteractFeed/DownLoadHandler.ashx",
        type: "post",
        async: false,
        dataType: "text",
        data: {
            filepath: filepath
        },
        success: function (result) {
            if (result == "-1") {
                layer.msg('文件不存在!');
                return;
            }
            window.open("/InteractFeed/DownLoadHandler.ashx?filepath=" + filepath + "&time=" + new Date().getTime(), "_self");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg('文件不存在!');
        }
    });
}
//获取查看页面文件信息
function Get_LookPage_Document(type, relationid, ul_file) {
    ul_file =arguments[2]||$('.queueList .filelist');
    ul_file.html('');
    $.ajax({
        url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
        type: "post",
        dataType: "json",
        data: { Func: "Get_Sys_Document", Type: type, RelationId: relationid, IsPage: false },
        success: function (json) {
            if (json.result.errNum == 0) {
                $(json.result.retData).each(function (i, n) {
                    ul_file.append('<li id="file_' + n.Id + '" pt="' + n.Url + '">' +
                                   '<p class="title1">' + n.Name + '</p>' +
                                 '<div class="file-panel">' +                                        
                                        '<span class="preview" onclick="File_Viewer(\'' + n.Url + '\');">预览</span>' +
                                 '</div></li>');
                });
            } else {
                ul_file.append('<li><p>无</p></li>');
            }
        }
    });
}
function File_Viewer(filepath) {
    var exten_str = filepath.substr(filepath.lastIndexOf(".")).toLowerCase();//后缀
    if (exten_str == 'pdf') {
        window.open("Pdf_View.html?src=" + filepath);
    } else {
        window.open(filepath);
    }
}