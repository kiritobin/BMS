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
            var search = $("#btn-search").val().trim();
            $.ajax({
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    search: search,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });
    //编辑按钮
    $("#table").delegate(".edited", "click", function () {
        var saleId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        sessionStorage.setItem("saleid", saleId);
        var defaultDiscount = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var number = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
        var price = $(this).parent().prev().prev().prev().prev().prev().text().trim();
        var allprice = $(this).parent().prev().prev().prev().prev().text().trim();
        $("#defaultDiscount").val(defaultDiscount);
        $("#allpricemlimited").val(allprice);
        $("#numberlimited").val(number);
        $("#pricelimited").val(price);
        $.ajax({
            type: 'Post',
            url: 'tradeManagement.aspx',
            data: {
                saleId:saleId,
                op: "isEdit"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "可以编辑") {
                    $("#myModa2").modal("show");
                }
                else {
                    swal({
                        title: "温馨提示",
                        text: "销售任务已完成，不可编辑",
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window, location.reload();
                    })
                }
            }
        });
    })
    //编辑保存
    $("#btn_change").click(function () {
        var saleId = sessionStorage.getItem("saleid");
        var allpricemlimited = $("#allpricemlimited").val();
        var numberlimited = $("#numberlimited").val();
        var pricelimited = $("#pricelimited").val();
        var defaultDiscount = $("#defaultDiscount").val();
        if (allpricemlimited == "" || numberlimited == "" || pricelimited == "" || defaultDiscount == "") {
            swal({
                title: "温馨提示:)",
                text: "不能含有未填项",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    saleId: saleId,
                    allpricemlimited: allpricemlimited,
                    numberlimited: numberlimited,
                    pricelimited: pricelimited,
                    defaultDiscount, defaultDiscount,
                        op: "edit"
                    },
                        dataType: 'text',
                    success: function (data) {
                        if (data == "保存成功") {
                            swal({
                                title: "温馨提示",
                                text: "保存成功",
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
                        else {
                            swal({
                                title: "温馨提示",
                                text: "保存失败",
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window, location.reload();
                            })
                        }
                    }
            });
        }
    })

    //点击查询按钮时
    $("#btn-search").click(function () {
        var search = $("#search_All").val().trim();
        $.ajax({
            type: 'Post',
            url: 'tradeManagement.aspx',
            data: {
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
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
                        $.ajax({
                            type: 'Post',
                            url: 'tradeManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                search: search,
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
    //添加销售任务
    $("#btnAdd").click(function () {
        var saleCustmer = $("#saleCustmer").val().trim();//客户
        var billCount = $("#billCount").val().trim();//最大采购数量
        var totalPrice = $("#totalPrice").val().trim();//单价上限
        var realPrice = $("#realPrice").val().trim();//码洋上限
        var Price = $("#Price").val().trim();//默认折扣
        if (saleCustmer == "") {
            swal({
                title: "温馨提示:)",
                text: "客户未选择，请您选择客户。",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (billCount == "") {
            swal({
                title: "温馨提示:)",
                text: "最大采购数不能为空!请您确认后再次提交。",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (totalPrice == "") {
            swal({
                title: "温馨提示:)",
                text: "单价上限不能为空!请您确认后再次提交。",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (realPrice == "") {
            swal({
                title: "温馨提示:)",
                text: "码洋上限不能为空!请您确认后再次提交。",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (Price == "") {
            swal({
                title: "温馨提示:)",
                text: "默认折扣不能为空!请您确认后再次提交。",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    Custmer: saleCustmer,
                    numberLimit: billCount,
                    priceLimit: totalPrice,
                    totalPriceLimit: realPrice,
                    defaultDiscount: Price,
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
                    } else {
                        swal({
                            title: "温馨提示",
                            text: "添加失败",
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
    $("#table").delegate(".btn_del", "click", function () {
        // var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        swal({
            title: "温馨提示:)",
            text: "删除后将无法恢复，您确定要删除吗？？？",
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
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    ID: ID,
                    op: 'del'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "删除成功") {
                        swal({
                            title: "提示",
                            text: "删除成功",
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
                            title: "提示",
                            text: "删除失败，其它地方有引用记录!",
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

    //查看
    $("#table").delegate(".btn_search", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'tradeManagement.aspx',
            data: {
                ID: ID,
                op: 'look'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "成功") {
                    window.location.href = "../SalesMGT/salesManagement.aspx";
                } else {
                    swal({
                        title: "提示",
                        text: "单头添加失败，请重试",
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

    //售
    $("#table").delegate(".btn_sale", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var time = $(this).parent().prev().text();
        if (time == "销售任务采集中") {
            $.ajax({
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    ID: ID,
                    op: 'sale'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "可以") {
                        window.location.href = "../SalesMGT/salesManagement.aspx";
                    } else {
                        swal({
                            title: "提示",
                            text: "该销售任务已完成，如要查看请点击查看按钮!",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        })
                    }
                }
            })
        }
        else {
            swal({
                title: "提示",
                text: "该销售任务已完成，如要查看请点击查看按钮!",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        }
    })
    //退
    $("#table").delegate(".btn_back", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var time = $(this).parent().prev().text();
        if (time == "销售任务采集中") {
            swal({
                title: "提示",
                text: "该销售任务未完成，不能执行销退!",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        } else {
            $.ajax({
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    ID: ID,
                    op: 'saleback'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "可以") {
                        window.location.href = "../SalesMGT/backManagement.aspx";
                    } else {
                        swal({
                            title: "提示",
                            text: "该销售任务未完成，不能执行销退!",
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
})
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
            url: 'tradeManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
