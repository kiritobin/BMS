$(document).ready(function () {
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
            var bookName = $("#sales_bookName").val().trim();
            var ISBN = $("#sales_ISBN").val().trim();
            $.ajax({
                type: 'Post',
                url: 'salesDetail.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    bookName: bookName,
                    ISBN: ISBN,
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
    //查询
    $("#btn_search").click(function () {
        var bookName = $("#sales_bookName").val().trim();
        var ISBN = $("#sales_ISBN").val().trim();
        $.ajax({
            type: 'Post',
            url: 'salesDetail.aspx',
            data: {
                bookName: bookName,
                ISBN: ISBN,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
                $('.paging').pagination({
                    //totalData: $("#countPage").val(), //数据总数
                    //showData: $("#totalCount").val(), //每页显示的条数
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
                            url: 'salesDetail.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                bookName: bookName,
                                ISBN: ISBN,
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

    //输入完敲回车键
    $(".addsale").keypress(function (e) {
        if (e.keyCode == 13) {
            var ISBN = $("#ISBN").val().trim();
            var disCount = $("#disCount").val().trim();
            var number = $("#number").val().trim();
            if (disCount == "" || number == "" || ISBN == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                $.ajax({
                    type: 'Post',
                    url: 'salesDetail.aspx',
                    data: {
                        ISBN: ISBN,
                        disCount: disCount,
                        number: number,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功") {
                            swal({
                                title: "温馨提示",
                                text: "添加成功",
                                type: "success",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window, location.reload();
                            })
                        }
                        else if (data == "库存数量不足") {
                            swal({
                                title: "温馨提示",
                                text: "库存数量不足",
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                            })
                        }
                        else if (data == "无库存") {
                            swal({
                                title: "温馨提示",
                                text: "无库存",
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                            })
                        }
                        else if (data == "无数据") {
                            swal({
                                title: "温馨提示",
                                text: "无数据",
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                            })
                        }
                        else {
                            $("#tablebook th:not(:first)").empty(); //清空table处首行
                            $("#tablebook").append(data); //加载table
                        }
                    }
                });
            }
        }
    })
    //添加
    $("#btnAdd").click(function () {
        var bookNum = $("input[type='radio']:checked").val();
        var ISBN = $("#ISBN").val().trim();
        var disCount = $("#disCount").val().trim();
        var number = $("#number").val().trim();
        if (disCount == "" || number == "" || ISBN == "") {
            swal({
                title: "温馨提示:)",
                text: "不能含有未填项!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'salesDetail.aspx',
                data: {
                    bookNum: bookNum,
                    ISBN: ISBN,
                    disCount: disCount,
                    number: number,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示",
                            text: "添加成功",
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    } else if (succ == "库存数量不足") {
                        swal({
                            title: "温馨提示",
                            text: "库存数量不足",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                    else if (succ == "无库存") {
                        swal({
                            title: "温馨提示",
                            text: "无库存",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                    else if (succ == "无数据") {
                        swal({
                            title: "温馨提示",
                            text: "无数据",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                    else {
                        swal({
                            title: "温馨提示",
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

    //返回按钮
    $("#back").click(function () {
        $.ajax({
            type: 'Post',
            url: 'salesDetail.aspx',
            data: {
                op: 'back'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    window.location.href = "../SalesMGT/tradeManagement.aspx";
                } else if (succ == "删除失败") {
                    window.location.href = "../SalesMGT/tradeManagement.aspx";
                } else {
                    window.location.href = "../SalesMGT/tradeManagement.aspx";
                }
            }
        })
    })
    //删除
    $("#table").delegate(".btn_del", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
        swal({
            title: "是否删除？",
            text: "删除后将无法恢复！！！",
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
            $.ajax({
                type: 'Post',
                url: 'salesDetail.aspx',
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
})