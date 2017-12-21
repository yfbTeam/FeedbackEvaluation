<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookStory_List.aspx.cs" Inherits="FEWeb.TeaAchManage.BookStory_List" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>教材库</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>  
    <script type="text/x-jquery-tmpl" id="trBook">
        <tr>
            <td>${ISBN}</td>
            <td>${Name}{{if PrizeCount>0}}<i class="iconfont reward">&#xe778;</i>{{/if}}</td>
            <td>{{if IsOneVolum==1}}单册{{else}} 多册{{/if}}</td>
            <td>${EditName}</td>
            <td>{{if BookType==1}}立项教材{{else}} 出版教材{{/if}}</td> 
            <td>{{if IsPlanBook==0}}否{{else}}是{{/if}}</td>           
            <td>${EditionNo}</td>
           <td>{{if BookType==2}}${DateTimeConvert(PublisthTime,"yyyy-MM")}{{/if}}</td>
            <td class="operate_wrap">
                <div class="operate" onclick="OpenIFrameWindow('教材查看','CheckFail.aspx?Id=${Id}&Type=Look','700px', '800px');">
                    <i class="iconfont color_purple">&#xe60b;</i>
                    <span class="operate_none bg_purple">查看</span>
                </div>
                {{if list_book_operall==true}}
                <div class="operate" onclick="OpenIFrameWindow('教材修改','BookAdd.aspx?Id=${Id}','1100px','700px');">
                    <i class="iconfont color_purple">&#xe617;</i>
                    <span class="operate_none bg_purple">修改</span>
                </div>
                <div class="operate" onclick="Del_Book(${Id});">
                    <i class="iconfont color_purple">&#xe61b;</i>
                    <span class="operate_none bg_purple">删除</span>
                </div>
                {{else list_book_operdepart==true}}
                   {{if MajorCount>0}}
                    <div class="operate" onclick="OpenIFrameWindow('教材修改','BookAdd.aspx?Id=${Id}','1100px','700px');">
                        <i class="iconfont color_purple">&#xe617;</i>
                        <span class="operate_none bg_purple">修改</span>
                    </div>
                <div class="operate" onclick="Del_Book(${Id});">
                    <i class="iconfont color_purple">&#xe61b;</i>
                    <span class="operate_none bg_purple">删除</span>
                </div>
                {{else}}
                    <div class="operate">
                        <i class="iconfont color_gray">&#xe617;</i>
                        <span class="operate_none bg_gray">修改
                        </span>
                    </div>
                <div class="operate">
                    <i class="iconfont color_gray">&#xe61b;</i>
                    <span class="operate_none bg_gray">删除
                    </span>
                </div>
                {{/if}}
                {{/if}}                   
            </td>
        </tr>
    </script>
    
</head>
<body>
    <input type="hidden" id="CreateUID" value="011" />
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap">
            <div id="threenav" class="sort_nav"></div>
            <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">教材类型:</label>
                    <select class="select" id="BookType" onchange="Book(1,10);" style="width: 198px;">
                        <option value="">全部</option>
                        <option value="1">立项教材</option>
                        <option value="2">已出版教材</option>
                    </select>
                </div> 
                <div class="fl ml20">
                    <label for="">国家级“十一五”规划教材:</label>
                    <select class="select" id="IsPlanBook" onchange="Book(1,10);" style="width: 198px;">
                        <option value="">全部</option>
                        <option value="1">是</option>
                        <option value="0">否</option>
                    </select>
                </div>                         
                <div class="fl ml20">
                    <input type="text" id="Key" placeholder="关键字搜索" value="" class="text fl" style="width: 130px;height:31px;">
                    <a class="search fl" onclick="Book(1,10);" style="cursor: pointer;"><i class="iconfont">&#xe600;</i></a>
                </div>                        
            </div>
            <div class="table mt10">
                <table>
                    <thead>
                        <tr>
                            <th>书号</th>
                            <th>书名</th>
                            <th>分册情况</th>
                            <th>主编</th>
                            <th>教材类型</th>
                            <th>国家级“十一五”规划教材</th>
                            <th>版次</th>
                            <th>出版时间</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tb_Book"></tbody>
                </table>
                <div id="pageBar_Book" class="page"></div>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/public.js"></script>
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/laypage/laypage.js"></script>
    <script>
        $(function () {
            $("#CreateUID").val(GetLoginUser().UniqueNo);
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
        });
    </script>
    <script>
        var UrlDate = new GetUrlDate();    
        var list_book_showall = false, list_book_operall = false, list_book_operdepart = false;//查看-全校范围,编辑、删除-全校范围,编辑、删除-院系范围
        $(function () {
            Get_PageBtn("/TeaAchManage/BookStory_List.aspx");
            list_book_showall = JudgeBtn_IsExist("list_book_showall")
             , list_book_operall = JudgeBtn_IsExist("list_book_operall")
             , list_book_operdepart = JudgeBtn_IsExist("list_book_operdepart");
            Book(1, 10);
        });
        function Book(startIndex, pageSize) {           
            $("#tb_Book").empty();
            var parmsData = { "Func": "GetTPM_BookStory", BookType: $("#BookType").val(), IsPlanBook: $("#IsPlanBook").val(), "Name": $("#Key").val(), PageIndex: startIndex, pageSize: pageSize, Status: 3 };
            if (list_book_operdepart && !list_book_operall) { parmsData["LoginMajor_ID"] = GetLoginUser().Major_ID; }
            if (!list_book_showall && !list_book_operall) { parmsData["Major_ID"] = GetLoginUser().Major_ID; }
            $.ajax({
                url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                type: "post",
                dataType: "json",
                data: parmsData,
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $("#pageBar_Book").show();
                        $("#trBook").tmpl(json.result.retData.PagedData).appendTo("#tb_Book");                       
                        laypage({
                            cont: 'pageBar_Book', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: true, //是否开启跳页
                            skin: '#6a264b',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    Book(obj.curr, pageSize)
                                }
                            }
                        });
                        tableSlide();
                    } else {
                        $("#pageBar_Book").hide();
                        nomessage('#tb_Book');
                    }
                },
                error: function () {
                    //接口错误时需要执行的
                }
            });
        }        
        /* 删除教材*/
        function Del_Book(id) { 
            layer.confirm('确定删除该教材吗？', {
                btn: ['确定', '取消'],
                title: '操作'
            }, function (index) {
                $.ajax({
                    url: HanderServiceUrl + "/TeaAchManage/AchRewardInfo.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: { Func: "Del_BookStory", ItemId: id },
                    success: function (json) {
                        if (json.result.errNum == 0) {                          
                            layer.msg('操作成功!');
                            Book(1, 10);
                        } 
                    },
                    error: function () { }
                });
            }, function () { });
        }
    </script>
</body>
</html>

