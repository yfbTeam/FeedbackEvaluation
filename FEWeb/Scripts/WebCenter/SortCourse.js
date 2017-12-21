
var UI_SortCourse =
    {
        PageType: 'SortCourse', //SortCourse 分配课程子页面
        PrepareInit: function () {
            //UI_SortCourse.GetCourseInfo();
            select_uniques = [];
            $('#college').change(function () {            
                var majorId = $('#college').val();
                var key = $("#key").val();
                GetNoDis_CourseInfo(0, select_sectionid, majorId, key);

            });
        },       
        //------------------------------------全选/单选------------------------------------
        Course_CheckAll: function (oInput) {
            var isCheckAll = function () {
                for (var i = 1, n = 0; i < oInput.length; i++) {
                    oInput[i].checked && n++
                }
                oInput[0].checked = n == oInput.length - 1;
            };
            //全选
            oInput[0].onchange = function () {
                for (var i = 1; i < oInput.length; i++) {
                    oInput[i].checked = this.checked;
                    var Course_No = oInput[i].value;
                    UI_SortCourse.AddORDelCourse(Course_No, $(this).is(':checked'));
                }
                isCheckAll()
            };
            //根据复选个数更新全选框状态
            for (var i = 1; i < oInput.length; i++) {
                oInput[i].onchange = function () {   //单选              
                    UI_SortCourse.AddORDelCourse(this.value, $(this).is(':checked'));
                    isCheckAll()
                }
            }
        },
        //数组添加或移除课程编码
        AddORDelCourse: function (Course_No, ischeck) {
            
            var cindex = $.inArray(Course_No, select_uniques);
            if (!ischeck) { //取消选中          
                if (cindex > -1) {
                    select_uniques.splice(cindex, 1);
                }
            }
            else { //选中  
                if (cindex == -1) {
                    select_uniques.push(Course_No);
                }
            }
        },

        //------------------------------------分配课程------------------------------------
        SubmitUpdateCourseSort: function () {
            if (select_uniques.length <= 0) {
                layer.msg("请勾选要分配的课程！");
                return;
            }
            layer.confirm('确定要将这些课程分配给【' + $("#_currsele").find("option:selected").text() + '】吗？', {
                btn: ['确定', '取消'], //按钮
                title: '操作'
            }, function () {
                var UniqueNos = select_uniques.join(",");
                var postData = { func: "SetCourseSort", "Course_No": UniqueNos, "CourseTypeId": $("#_currsele").val(), "StudySection_Id": select_sectionid };
                $.ajax({
                    type: "Post",
                    url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
                    data: postData,
                    dataType: "json",
                    async: false,
                    success: function (returnVal) {
                        if (returnVal.result.errMsg == "success") {
                            UI_SortCourse.SubmitUpdateCourseSort_Compleate(true);
                        }
                        else {
                            UI_SortCourse.SubmitUpdateCourseSort_Compleate(false);
                        }
                    },
                    error: function (errMsg) {
                        UI_SortCourse.SubmitUpdateCourseSort_Compleate(false);
                    }
                });
            }, function () { });
        },
        SubmitUpdateCourseSort_Compleate: function (result) { },
    };




function GetNoDis_CourseInfo(PageIndex) {
    

    var majorId = $('#college').val();
    var key = $("#key").val();

    var postData = {
        func: "GetNoDis_CourseInfo",
        "SectionId": select_sectionid,
        "Major_Id": majorId,
        "Key": key,
        "PageIndex": PageIndex,
        "PageSize": pageSize,

    };
    layer_index = layer.load(1, {
        shade: [0.1, '#fff'] //0.1透明度的白色背景
    });
    $.ajax({
        type: "Post",
        url: HanderServiceUrl + "/SysClass/CourseInfoHandler.ashx",
        data: postData,
        dataType: "json",
        success: function (returnVal) {
            if (returnVal.result.errMsg == "success") {
                reUserinfoByselect = returnVal.result.retData;

                $('#ShowCourseInfo').empty();

                layer.close(layer_index);
                if (reUserinfoByselect.length <= 0) {
                    nomessage('#ShowCourseInfo');
                    $('#pageBar').hide();
                    return;
                }
                else {
                    $('#pageBar').show();
                }
                $("#item_course").tmpl(reUserinfoByselect).appendTo("#ShowCourseInfo");
                tableSlide();

                laypage({
                    cont: 'pageBar', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                    pages: returnVal.result.PageCount, //通过后台拿到的总页数
                    curr: returnVal.result.PageIndex || 1, //当前页
                    skip: true, //是否开启跳页
                    skin: '#CA90B0',
                    groups: 10,
                    jump: function (obj, first) { //触发分页后的回调
                        if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr     

                            GetNoDis_CourseInfo(obj.curr, select_sectionid, majorId, key)
                            pageIndex = obj.curr;
                        }
                    }
                });
                $("#itemCount").tmpl(returnVal.result).appendTo(".laypage_total");


                UI_SortCourse.Course_CheckAll($('input:checkbox'));//全选        
                    $("#ck_head").removeAttr('checked');
                    if (select_uniques.length > 0) {
                        var $check_Sub = $('input:checkbox[name=ss]');
                        var seltimes = 0;
                        $check_Sub.each(function () {
                            var Course_No = this.value;
                            if ($.inArray(Course_No, select_uniques) != -1) {
                                $(this).attr("checked", true);
                                seltimes++;
                            }
                        });
                        $('input:checkbox')[0].checked = seltimes == $check_Sub.length;
                    }
            }
        },
        error: function (errMsg) {
            alert("失败2");
        }
    });

}


