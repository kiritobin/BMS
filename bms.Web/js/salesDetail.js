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
    //isbn敲回车键
    $("#table").delegate(".isbn", "keypress", function (e) {
        if (e.keyCode == 13) {
            $("#tablebook").html("");
            var ISBN = $(this).val().trim();
            if (ISBN == "") {
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
                        //disCount: disCount,
                        //number: number,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data.indexOf("much") >= 0) {
                            $("#myModa2").modal("show");
                            $("#tablebook th:not(:first)").empty(); //清空table处首行
                            $("#tablebook").append(data); //加载table
                        }
                        else if (data == "无数据") {
                            swal({
                                title: "温馨提示",
                                text: "无该书基础数据",
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
                            $(".first").remove();
                            $("#table").append(data);
                            $("#table tr:last").find("td").eq(5).children().focus();
                        }
                    }
                });
            }
        }
    })
    var limited = $("#limtalltotalprice").val();
    var numberLimit = $("#numberLimit").val();
    var priceLimit = $("#priceLimit").val();

    //数量回车
    $("#table").delegate(".count", "keypress", function (e) {
        if (e.keyCode == 13) {
            var num = $(this).parent().prev().prev().prev().prev().prev().text();
            var bookNum = $(this).parent().prev().prev().prev().text();
            var bookISBN = $(this).parent().prev().prev().prev().prev().children().val();
            var price = $(this).parent().prev().text().trim();
            var number = $(this).val().trim();
            var discount = $(this).parent().next().children().val().trim();
            var allprice = number * price;
            if (number == "") {
                swal({
                    title: "温馨提示:)",
                    text: "数量不能为空",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                })
            } else if (parseFloat(price) > parseFloat(priceLimit) || number > numberLimit || allprice > limited) {
                swal({
                    title: "是否继续录入",
                    text: "本次录入已达上限,是否继续录入?" + "<br/>" + "最大数量为:" + numberLimit + " 单价限制为:" + priceLimit + " 总码洋限制为:" + limited,
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    confirmButtonClass: 'btn btn-success',
                    cancelButtonClass: 'btn btn-danger',
                    buttonsStyling: false,
                    showLoaderOnConfirm: true,//是否显示load
                    allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
                }).then(function () {
                    $.ajax({
                        type: 'Post',
                        url: 'salesDetail.aspx',
                        data: {
                            bookISBN: bookISBN,
                            bookNum: bookNum,
                            number: number,
                            discount: discount,
                            op: "addsale",
                            tips: "addsale"
                        },
                        dataType: 'json',
                        success: function (succ) {
                            if (succ.Messege == "添加成功") {
                                $("#table tbody").html("");
                                $("#table").append(succ.DataTable);
                                $("#table").append(succ.DataTable1);
                                //$("#table").append(table);
                                $("#ISBN").focus();
                                $("#kinds").text(succ.AllKinds);
                                $("#allnumber").text(succ.Number);
                                $("#alltotalprice").text(succ.AlltotalPrice);
                                $("#allreadprice").text(succ.AllrealPrice);
                            } else if (succ.Messege == "库存不足") {
                                var count = succ.Count;
                                swal({
                                    title: "库存不足",
                                    text: "当前最大库存为:" + succ.Count1 + ",是否生成补货单？",
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
                                            bookISBN: bookISBN,
                                            bookNum: bookNum,
                                            number: number,
                                            discount: discount,
                                            count: count,
                                            op: "addRsMon",
                                            tips: "addMon"
                                        },
                                        dataType: 'json',
                                        success: function (succ) {
                                            if (succ.Messege == "添加成功") {
                                                $("#table tbody").html("");
                                                $("#table").append(succ.DataTable);
                                                $("#table").append(succ.DataTable1);
                                                //$("#table").append(table);
                                                $("#ISBN").focus();
                                                $("#kinds").text(succ.AllKinds);
                                                $("#allnumber").text(succ.Number);
                                                $("#alltotalprice").text(succ.AlltotalPrice);
                                                $("#allreadprice").text(succ.AllrealPrice);
                                                swal({
                                                    title: "温馨提示:)",
                                                    text: "添加成功并已完成补货单的添加！",
                                                    type: "success",
                                                    confirmButtonColor: '#3085d6',
                                                    confirmButtonText: '确定',
                                                    confirmButtonClass: 'btn btn-success',
                                                    buttonsStyling: false,
                                                    allowOutsideClick: false
                                                }).then(function () {
                                                })
                                            }
                                            else if (succ.Messege == "添加失败") {
                                                swal({
                                                    title: "温馨提示:)",
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
                                            else if (succ.Messege == "无数据") {
                                                swal({
                                                    title: "温馨提示:)",
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
                                            else if (succ.Messege == "客户馆藏已存在") {
                                                swal({
                                                    title: "温馨提示:)",
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
                                                    title: "温馨提示:)",
                                                    text: succ.Messege,
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
                            }
                            else if (succ.Messege == "添加失败") {
                                swal({
                                    title: "温馨提示:)",
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
                            else if (succ.Messege == "无数据") {
                                swal({
                                    title: "温馨提示:)",
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
                            else if (succ.Messege == "客户馆藏已存在") {
                                swal({
                                    title: "温馨提示:)",
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
                                    title: "温馨提示:)",
                                    text: succ.Messege,
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
            }
            else {
                $.ajax({
                    type: 'Post',
                    url: 'salesDetail.aspx',
                    data: {
                        bookISBN: bookISBN,
                        bookNum: bookNum,
                        number: number,
                        discount: discount,
                        op: "addsale",
                        tips: "addsale"
                    },
                    dataType: 'json',
                    success: function (succ) {
                        if (succ.Messege == "添加成功") {
                            $("#table tbody").html("");
                            $("#table").append(succ.DataTable);
                            $("#table").append(succ.DataTable1);
                            //$("#table").append(table);
                            $("#ISBN").focus();
                            $("#kinds").text(succ.AllKinds);
                            $("#allnumber").text(succ.Number);
                            $("#alltotalprice").text(succ.AlltotalPrice);
                            $("#allreadprice").text(succ.AllrealPrice);
                        } else if (succ.Messege == "库存不足") {
                            var count = succ.Count;
                            swal({
                                title: "库存不足",
                                text: "当前最大库存为:" + succ.Count1 + "是否生成补货单？",
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
                                        bookISBN: bookISBN,
                                        bookNum: bookNum,
                                        number: number,
                                        discount: discount,
                                        count: count,
                                        op: "addRsMon",
                                        tips: "addMon"
                                    },
                                    dataType: 'json',
                                    success: function (succ) {
                                        if (succ.Messege == "添加成功") {
                                            $("#table tbody").html("");
                                            $("#table").append(succ.DataTable);
                                            $("#table").append(succ.DataTable1);
                                            //$("#table").append(table);
                                            $("#ISBN").focus();
                                            $("#kinds").text(succ.AllKinds);
                                            $("#allnumber").text(succ.Number);
                                            $("#alltotalprice").text(succ.AlltotalPrice);
                                            var alltotalprice = parseFloat(succ.AlltotalPrice);
                                            var limtalltotalprice = parseFloat(limited);
                                            $("#allreadprice").text(succ.AllrealPrice);
                                            swal({
                                                title: "温馨提示:)",
                                                text: "添加成功并已完成补货单的添加！",
                                                type: "success",
                                                confirmButtonColor: '#3085d6',
                                                confirmButtonText: '确定',
                                                confirmButtonClass: 'btn btn-success',
                                                buttonsStyling: false,
                                                allowOutsideClick: false
                                            }).then(function () {
                                            })
                                        }
                                        else if (succ.Messege == "添加失败") {
                                            swal({
                                                title: "温馨提示:)",
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
                                        else if (succ.Messege == "无数据") {
                                            swal({
                                                title: "温馨提示:)",
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
                                        else if (succ.Messege == "客户馆藏已存在") {
                                            swal({
                                                title: "温馨提示:)",
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
                                                title: "温馨提示:)",
                                                text: succ.Messege,
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
                        }
                        else if (succ.Messege == "添加失败") {
                            swal({
                                title: "温馨提示:)",
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
                        else if (succ.Messege == "无数据") {
                            swal({
                                title: "温馨提示:)",
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
                        else if (succ.Messege == "客户馆藏已存在") {
                            swal({
                                title: "温馨提示:)",
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
                                title: "温馨提示:)",
                                text: succ.Messege,
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
        }
    })
    //添加
    $("#btnAdd").click(function () {
        var bookNum = $("input[type='radio']:checked").val();
        var tb = $("input[type='radio']:checked");
        var isbn = $(tb).parent().parent().next().text();
        var bookname = $(tb).parent().parent().next().next().next().text();
        var price = $(tb).parent().parent().next().next().next().next().text();
        if (bookNum == "") {
            swal({
                title: "温馨提示:)",
                text: "请勾选书籍！",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $("#myModa2").modal("hide");
            $.ajax({
                type: 'Post',
                url: 'salesDetail.aspx',
                data: {
                    bookNum: bookNum,
                    isbn: isbn,
                    bookname: bookname,
                    price: price,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    $("#tablebook").html("");
                    $(".first").remove();
                    $("#table").append(succ);
                    $("#table tr:last").find("td").eq(5).children().focus();
                }
            })
        }
    })
    //打印
    $("#print").click(function () {
        $("#table").jqprint();
    })
    //$("#test").click(function () {
    //    var status = "";
    //    var LODOP = getLodop();
    //    var strStyleCSS = "";
    //    LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
    //    LODOP.On_Return = function (TaskID, Value) {
    //        status = Value;
    //    };
    //    if (status != "" || status != null) {
    //        strStyleCSS += "<link rel='stylesheet' type='text/css' href='../css/zgz.css'><link rel='stylesheet' href='../css/material-dashboard.min.css'>";

    //        LODOP.ADD_PRINT_HTM(0, 0, "100%", "100%", strStyleCSS + "<body>" + document.getElementById("content").innerHTML + "</body>");
    //        LODOP.SET_PRINTER_INDEX("Send To OneNote 2016");
    //        LODOP.SET_PRINT_PAGESIZE(3, "100%", 0, "A4");
    //        LODOP.PREVIEW();
    //    }
    //})

    //单据完成
    $("#success").click(function () {
        $(".first").remove();
        swal({
            title: "温馨提示:)",
            text: "是否新建单据？",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger',
            buttonsStyling: false,
            showLoaderOnConfirm: true,//是否显示load
            allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
        }).then(function () {
            $.ajax({
                type: 'Post',
                url: 'salesDetail.aspx',
                data: {
                    op: 'success'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "状态修改成功") {
                        swal({
                            title: "提示",
                            text: "状态修改成功",
                            type: "success",
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: '确定',
                            cancelButtonText: '取消',
                            confirmButtonClass: 'btn btn-success',
                            cancelButtonClass: 'btn btn-danger',
                            buttonsStyling: false,
                            allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
                        })
                    } else if (succ == "没有数据") {
                        swal({
                            title: "温馨提示:)",
                            text: "空单不能完成！",
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
                        })
                    }

                    else {
                        swal({
                            title: "温馨提示:)",
                            text: "单据状态修改失败，请联系技术人员！",
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
                        })
                    }
                }
            })
        })
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
                if (succ == "更新成功") {
                    window.location.href = "../SalesMGT/salesManagement.aspx";
                } else if (succ == "无数据") {
                    window.location.href = "../SalesMGT/salesManagement.aspx";
                } else {
                    swal({
                        title: "温馨提示:)",
                        text: "单据状态修改失败",
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
                    })
                }
            }
        })
    })
    ////删除
    //$("#table").delegate(".btn_del", "click", function () {
    //    var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
    //    swal({
    //        title: "是否删除？",
    //        text: "删除后将无法恢复！！！",
    //        type: "question",
    //        showCancelButton: true,
    //        confirmButtonColor: '#3085d6',
    //        cancelButtonColor: '#d33',
    //        confirmButtonText: '确定',
    //        cancelButtonText: '取消',
    //        confirmButtonClass: 'btn btn-success',
    //        cancelButtonClass: 'btn btn-danger',
    //        buttonsStyling: false,
    //        allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
    //    }).then(function () {
    //        $.ajax({
    //            type: 'Post',
    //            url: 'salesDetail.aspx',
    //            data: {
    //                ID: ID,
    //                op: 'del'
    //            },
    //            dataType: 'text',
    //            success: function (succ) {
    //                if (succ == "删除成功") {
    //                    swal({
    //                        title: succ,
    //                        text: succ,
    //                        type: "success",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    }).then(function () {
    //                        window.location.reload();
    //                    })
    //                } else {
    //                    swal({
    //                        title: succ,
    //                        text: succ,
    //                        type: "warning",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    }).then(function () {
    //                    })
    //                }
    //            }
    //        })
    //    })
    //});
})