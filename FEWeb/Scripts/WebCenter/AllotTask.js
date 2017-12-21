var PageType = 'AllotTask';//AllotTask 定期评价分配
var ExpertList = [];
var Teachers = null;


function PrepareInit() {
    $('#teachers li').hover(function () {
        $(this).addClass('selected');
        $(this).children('ul').show();
    }, function () {
        $(this).removeClass('selected');
        $(this).children('ul').hide();
    })
}

function GetTeacherInfo_Course_Cls() {
  
    if (Teachers == null) {
        var postData = { func: "GetTeacherInfo_Course_Cls" };
        $.ajax({
            type: "Post",
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            data: postData,
            dataType: "json",
            async: false,
            success: function (json) {
                if (json.result.errMsg == "success") {
                    Teachers = json.result.retData;
                    switch (PageType) {
                        case 'AllotTask':
                            Teachers_Reflesh();
                            break;
                        default:
                    }
                }
            },
            error: function (errMsg) {
                layer.msg("失败2");
            }
        });
    }
}
function Teachers_Reflesh() {
    var that = this;
    var college = $('#college').val().trim();
    var key = $('#key').val().trim();
    $("#item_Teachers").tmpl(Teachers).appendTo("#teachers");
    PrepareInit();
}

//获取督导专家
function GetUserByType(userType) {
    var that = this;
    var postData = { func: "GetUserByType", type: userType };
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
        data: postData,
        dataType: "json",
        async: false,
        success: function (json) {
            if (json.result.errMsg == "success") {
                debugger
                switch (PageType) {
                    case 'AllotTask':

                        ExpertList = json.result.retData;
                        $("#experts").empty();
                        $("#item_Expert").tmpl(ExpertList).appendTo("#experts");

                        //默认第一个选中，并且添加点击事件，选中样式
                        $('.linkman_lists li:eq(0)').addClass('selected');
                        $('.linkman_lists li').click(function () {
                            $(this).addClass('selected').siblings().removeClass('selected');
                        })

                        break;
                    default:

                }
            }
        },
        error: function (errMsg) {
            layer.msg("失败2");
        }
    });
}