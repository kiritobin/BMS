jeDate("#startTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});
jeDate("#endTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});

$(document).ready(function () {
    $("#sure").click(function () {
        var st = $("#startTime").val();
        var et = $("#endTime").val();
        var rName = $("#cusSearch").find('option:selected').text();
        if (st == "" || st == null) {
            swal({
                title: "温馨提示:)",
                text: "开始时间不能为空",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (et == "" || et == null) {
            swal({
                title: "温馨提示:)",
                text: "结束时间不能为空",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (rName=="选择组织") {
            swal({
                title: "温馨提示:)",
                text: "组织不能为空",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: "Post",
                url: "statistics.aspx",
                data: {
                    startDt: st,
                    endDt: et,
                    regName: rName,
                    op: "sure"
                },
                dateType: "text",
                success: function (data) {
                    if (data == "更新成功" || data == "添加成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "设置成功",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "success"
                        }).catch(swal.noop);
                    }
                    else {
                        swal({
                            title: "温馨提示:)",
                            text: "设置失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    }
                }
            })
        }
    })
})