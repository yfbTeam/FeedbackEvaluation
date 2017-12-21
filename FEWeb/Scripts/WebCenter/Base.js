var Base = {
    bindStudySection: function () {
        $.ajax({
            url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
            type: "post",
            dataType: "json",
            data: { Func: "Get_StudySection" },
            success: function (json) {
                json.result.retData.forEach(function (item) {
                    var str = "<option value='" + item.Id + "'>" + item.DisPlayName + "</option>";
                    $("#section").append(str);
                })
            },
            error: function () { }
        });
    },
    BindDepart:function () {
        $.ajax({
            url: HanderServiceUrl + "/UserMan/UserManHandler.ashx",
            type: "post",
            dataType: "json",
            data: {
                func: "GetMajors"
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $(json.result.retData).each(function () {
                        $("#DepartMent").append('<option value="' + this.Id + '">' + this.Major_Name + '</option>');
                    });
                }
            },
            error: function (errMsg) {}
        });
    }       
}
