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
//退出系统
function logout() {
    swal({
        title: "温馨提示:)",
        text: "您确定要退出系统吗？",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false,
        allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
    }).then(function () {
        $.ajax({
            type: 'get',
            url: 'statistics.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
$(document).ready(function () {
    $("#sure").click(function () {
        var st = $("#startTime").val();
        var et = $("#endTime").val();
        var rName = $("#cusSearch").find('option:selected').text();
        var type = $("#searchKinds").find('option:selected').text();
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
        else if (type == "选择类型") {
            swal({
                title: "温馨提示:)",
                text: "展示类型不能为空",
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
                    type:type,
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
    $("#goScreen").click(function () {
        var rName = $("#cusSearch").find('option:selected').text();
        $.ajax({
            type: "Post",
            url: "statistics.aspx",
            data: {
                regName: rName,
                op:"goScreen"
            },
            dateType: "text",
            success: function (data) {
                if (data == "记录不存在") {
                    swal({
                        title: "温馨提示:)",
                        text: "你还未做过任何配置",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
                else {
                    var hh = "../SalesMGT/salesRanking.aspx?userId=" + $("#userId").val();
                    window.location.href = hh;
                }
            }
        })
    })
})