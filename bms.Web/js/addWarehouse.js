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
            url: 'addWarehouse.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

$(document).ready(function () {
    //sessionStorage.setItem("flag", "false");
    $("#btnAdd").attr("disabled", true);
    $(".paging").pagination({
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var search = $("#btn-search").val();
            $.ajax({
                type: 'Post',
                url: 'addWarehouse.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    search: search,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    })
})

//回车时间
$("#isbn").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var isbn = $("#isbn").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'addWarehouse.aspx',
                data: {
                    isbn: isbn,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "ISBN不存在") {
                        swal({
                            title: "错误提示:)",
                            text: "ISBN不存在",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else {
                        $("#table2 tr:not(:first)").empty(); //清空table处首行
                        $("#table2").append(data); //加载table
                    }
                }
            })
        }
    }
})

//$("#table2").delegate("input[type='checkbox']", "click",function () {
//    if (sessionStorage.getItem("flag") == "true") {
//        sessionStorage.setItem("flag", "false");
//    }
//    else {
//        sessionStorage.setItem("flag", "true");
//    }
//    if (sessionStorage.getItem("flag") == "true") {
//      $("input[type='checkbox']").not(this).attr("checked", false);
//    } else {
//        $("input[type='checkbox']").attr("checked", false);
//        alert("false");
//    }
//    if (sessionStorage.getItem("flag") == "false") {
//        swal({
//            title: "温馨提示:)",
//            text: "请选择一条图书信息",
//            buttonsStyling: false,
//            confirmButtonClass: "btn btn-warning",
//            type: "warning"
//        }).catch(swal.noop);
//    }
//})

//添加出库单头
$("#btnAdd").click(function () {
    var bookNum = $("input[name='radio']:checked").val();
    var billCount = $("#billCount").val();
    var disCount = $("#disCount").val();
    if (bookNum == "" || bookNum == null) {
        swal({
            title: "温馨提示:)",
            text: "请选择一条图书信息",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    }
    else if (billCount == "") {
        swal({
            title: "温馨提示:)",
            text: "商品总数不能为空，请您重新输入",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else {
        $.ajax({
            type: 'Post',
            url: 'addWarehouse.aspx',
            data: {
                bookNum: bookNum,
                billCount: billCount,
                disCount: disCount,
                op: "add"
            },
            datatype: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
                    swal({
                        title: succ,
                        text: succ,
                        type: "success",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: succ,
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        })
    }
})

//删除
$("#table").delegate(".btn-delete", "click", function () {
    swal({
        title: "温馨提示:)",
        text: "删除后将无法恢复,您确定要删除吗？？？",
        type: "question",
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
        var ID = $(".btn-delete").prev().val();
        $.ajax({
            type: 'Post',
            url: 'addWarehouse.aspx',
            data: {
                ID: ID,
                op: 'del'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    swal({
                        title: succ,
                        text: succ,
                        type: "success",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: succ,
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        })
    })
});
