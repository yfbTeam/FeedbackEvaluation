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
    <input type="hidden" name="Func" value="Add_StudySection" />
    <!--这两个必须放在上边-->

    <input type="hidden" name="id" id="id" value="0" />
    <input type="hidden" name="CreateUID" id="CreateUID" value="011" />
    <input type="hidden" name="EditUID" id="EditUID" value="011" />
    <div class="main" >
        <div class="search_toobar clearfix fl">
           <%-- <div style="display: none" class="input-wrap">
                <label for="">年份:</label>
                <input type="text" class="text Wdate" name="Academic" isrequired="true" fl="年份" onfocus="WdatePicker({dateFmt:'yyyy'})" style="width: 150px;" />
            </div>
            <div style="display: none" class="input-wrap">
                <label for="">学期:</label>
                <select class="select" style="width: 163px" name="Semester" isrequired="true" fl="学期">
                    <option value="1">上学期</option>
                    <option value="2">下学期</option>
                </select>
            </div>--%>
            <div class="input-wrap">
                <label>学年学期：</label><input type="text" isrequired="true" fl="学年学期" name="StudyDisPlayName" class="text" placeholder="请输入学年学期" />
            </div>
            <div class="input-wrap">
                <label>起止时间：</label>
                <input type="text" class="text Wdate" name="StartTime" isrequired="true" fl="开始时间" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',position:{left:0,top:-231}})" style="width: 150px;" />
                <span style="padding-left: 10px;">~</span>
                <input type="text" class="text Wdate" name="EndTime" isrequired="true" fl="结束时间" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',position:{left:0,top:-231}})" style="width: 150px;" />
            </div>

        </div>
    </div>
    <div class="btnwrap">
        <input type="button" value="保存" onclick="submit()" class="btn" />
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
   
<script type="text/javascript">
    var index = parent.layer.getFrameIndex(window.name);

    //提交按钮
    function submit() {
        //验证为空项或其他
        var valid_flag = validateForm($('select,input[type="text"]'));
        if (valid_flag != "0")////验证失败的情况  需要表单的input控件 有 isrequired 值为true或false 和fl 值为不为空的名称两个属性
        {
            return false;
        }
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: getFromValue(),//组合input标签
            success: function (json) {
                console.log(JSON.stringify(json));
                if (json.result.errMsg == "success") {
                    parent.layer.msg('操作成功!');
                    parent.init_StudySection_data();
                    parent.layer.close(index);
                }
            },
            error: function () {
                //接口错误时需要执行的
            }
        });
    }
</script>
