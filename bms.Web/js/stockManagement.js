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
$('.paging').pagination({
        //totalData: $("#totalCount").val(),
        //showData: $("#pageSize").val(),
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var strWhere = $("#input-search").val();
            var regionId = $("#select-region").val();
            var roleId = $("#select-role").val();
            var time = $("#time").val();
            $.ajax({
                type: 'Post',
                url: 'stockManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    role: roleId,
                    region: regionId,
                    search: strWhere,
                    time: time,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
                }
            });
        }
    });

    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#timeModal").modal('hide');
    })
    //选择时间后确定
    $("#btnOK").click(function () {
        var startTime = $("#startTime").val();
        var endTime = $("#endTime").val();
        if (startTime == "" || startTime == null) {
            swal({
                title: "提示",
                text: "请选择开始时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else if (endTime == "" || endTime == null) {
            swal({
                title: "提示",
                text: "请选择结束时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            $("#time").val(startTime + "至" + endTime);
            $("#timeModal").modal('hide');
        }
    })
})

//点击查询按钮时
$("#btn-search").click(function () {
    var singHeadId = $("#ID").val();
    var regionName = $("#region").val();
    var userName = $("#user").val();
    var time = $("#time").val();
    $.ajax({
        type: 'Post',
        url: 'stockManagement.aspx',
        data: {
            singHeadId: singHeadId,
            regionName: regionName,
            userName: userName,
            time: time,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#intPageCount").remove();
            $("#table tr:not(:first)").remove(); //清空table处首行
            $("#table").append(data); //加载table
            $(".paging").empty();
            $('.paging').pagination({
                //totalData: $("#totalCount").val(),
                //showData: $("#pageSize").val(),
                pageCount: $("#intPageCount").val(), //总页数
                jump: true,
                mode: 'fixed',//固定页码数量
                coping: true,
                homePage: '首页',
                endPage: '尾页',
                prevContent: '上页',
                nextContent: '下页',
                callback: function (api) {
                    $.ajax({
                        type: 'Post',
                        url: 'stockManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            singHeadId: singHeadId,
                            regionName: regionName,
                            userName: userName,
                            time: time,
                            op: "paging"
                        },
                        dataType: 'text',
                        success: function (data) {
                            $("#table tr:not(:first)").remove(); //清空table处首行
                            $("#table").append(data); //加载table
                            $("#intPageCount").remove();
                        }
                    });
                }
            });
        }
    });
});

//删除用户
$("#table").delegate(".btn-danger", "click", function () {
    var account = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text().trim();
    swal({
        title: '温馨提示:)',
        text: '确定要删除账号为：' + account + '的用户吗？',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: '是的，删掉它!',
        cancelButtonText: '不，让我思考一下',
        confirmButtonClass: "btn btn-success",
        cancelButtonClass: "btn btn-danger",
        buttonsStyling: false,
        allowOutsideClick: false
    }).then(function () {
        $.ajax({
            type: 'Post',
            url: 'stockManagement.aspx',
            data: {
                account: account,
                op: "del"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "用户删除成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: "温馨提示:)",
                        text: "用户删除失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "warning",
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        })
    });
})

$("#btnAdd").click(function () {
    var source = $("#source").val();
    if (source == "") {
        swal({
            title: "温馨提示:)",
            text: "请选择入库来源",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'stockManagement.aspx',
            data: {
                source: source,
                op:"add"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "添加成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "添加成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                }
                else {
                    swal({
                        title: "温馨提示:)",
                        text: "添加失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "warning",
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        });
    }
});
$("#close").click(function () {
    window.location.reload();
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
            url: 'stockManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
