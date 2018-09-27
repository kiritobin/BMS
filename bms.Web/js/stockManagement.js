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
            $.ajax({
                type: 'Post',
                url: 'stockManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    role: roleId,
                    region: regionId,
                    search: strWhere,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });


})

//点击查询按钮时
$("#btn-search2").click(function () {
    var singHeadId = $("#btn-id").val().trim();
    var regionName = $("#btn-region").val().trim();
    var userName = $("#btn-user").val().trim();
    $.ajax({
        type: 'Post',
        url: 'stockManagement.aspx',
        data: {
            singHeadId: singHeadId,
            regionName: regionName,
            userName: userName,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#intPageCount").remove();
            $("#table tr:not(:first)").empty(); //清空table处首行
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
                            op: "paging"
                        },
                        dataType: 'text',
                        success: function (data) {
                            $("#table tr:not(:first)").empty(); //清空table处首行
                            $("#table").append(data); //加载table
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
                        window.location.reload();
                    })
                }
            }
        })
    });
})

$("#btnAdd").click(function () {
    var count = $("#billCount").val().trim();
    var totalPrice = $("#totalPrice").val().trim();
    var realPrice = $("#realPrice").val().trim();
    var source = $("#source").val().trim();
    if (count == "" || totalPrice == "" || realPrice == "" || source == "") {
        alert("含有未填项");
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'stockManagement.aspx',
            data: {
                count: count,
                totalPrice: totalPrice,
                realPrice: realPrice,
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
                        window.location.reload();
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
