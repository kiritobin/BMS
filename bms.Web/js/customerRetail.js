$(document).ready(function () {
    $("#search").focus();
    sessionStorage.setItem("kind", 0);
    sessionStorage.setItem("number", 0);
    sessionStorage.setItem("totalPrice", 0);
    sessionStorage.setItem("realPrice", 0);
    $("#time").text(CurentTime());
})
//获取当前时间
function CurentTime() {
    var now = new Date();

    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();

    var hh = now.getHours();
    var mm = now.getMinutes();

    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day + " ";

    if (hh < 10)
        clock += "0";

    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm;
    return (clock);
}
//输入isbn后回车
$("#search").keypress(function (e) {
    if (sessionStorage.getItem("retailId") == null || sessionStorage.getItem("retailId") == undefined) {
        swal({
            title: "请先扫描小票",
            text: "请先扫描小票",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else {
        sessionStorage.setItem("ISBN", $("#search").val())
        //回车事件触发
        if (e.keyCode == 13) {
            var kind = $("#kind").text().trim();
            var isbn = $("#search").val();
            if (isbn == "" || isbn == null) {
                swal({
                    title: "ISBN不能为空",
                    text: "ISBN不能为空，请您重新输入",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                $.ajax({
                    type: 'Post',
                    url: 'customerRetail.aspx',
                    data: {
                        isbn: isbn,
                        headId: sessionStorage.getItem("retailId"),
                        op: 'isbn'
                    },
                    dataType: 'text',
                    success: function (succ) {
                        var datas = succ.split("|:");
                        var data = datas[0];
                        $("#search").val("");
                        if (data == "添加失败") {
                            swal({
                                title: data,
                                text: data,
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-warning",
                                type: "warning"
                            }).catch(swal.noop);
                        } else if (data == "ISBN不存在") {
                            swal({
                                title: "ISBN:" + isbn,
                                text: "不存在",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-warning",
                                type: "warning"
                            }).catch(swal.noop);
                        } else if (data == "已添加过此图书") {
                            swal({
                                title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                                text: "需要继续添加可前往修改数量",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-warning",
                                type: "warning"
                            }).catch(swal.noop);
                        } else if (data == "一号多书") {
                            $.ajax({
                                type: 'Post',
                                url: 'customerRetail.aspx',
                                data: {
                                    isbn: isbn,
                                    op: 'choose'
                                },
                                dataType: 'text',
                                success: function (succ) {
                                    $("#myModal").modal("show");
                                    $("#table2 tr:not(:first)").empty(); //清空table处首行
                                    $("#table2").append(succ); //加载table
                                }
                            })
                        } else {
                            var math = datas[1].split(",");
                            if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                                $(".first").remove();
                                sessionStorage.setItem("kind", 1)
                            }
                            $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                            $("#table").append(data);
                            $("#kind").text(math[0]);
                            $("#number").text(math[1]);
                            $("#total").text(math[2]);
                            $("#real").text(math[3]);
                            //获取焦点
                            $("#search").focus();
                        }
                    }
                })
            }
        }
    }
})
//选择一号多书中需要的图书
$("#btnAdd").click(function () {
    var bookNum = $("input[type='radio']:checked").val();
    $.ajax({
        type: 'Post',
        url: 'customerRetail.aspx',
        data: {
            bookNum: bookNum,
            headId: sessionStorage.getItem("retailId"),
            op: 'add'
        },
        dataType: 'text',
        success: function (succ) {
            var datas = succ.split("|:");
            var data = datas[0];
            $("#search").val("");
            if (data == "已添加过此图书") {
                swal({
                    title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                    text: "需要继续添加可前往修改数量",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                var math = datas[1].split(",");
                if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                    $(".first").remove();
                    sessionStorage.setItem("kind", 1)
                }
                $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                $("#table").append(data);
                $("#kind").text(math[0]);
                $("#number").text(math[1]);
                $("#total").text(math[2]);
                $("#real").text(math[3]);
                //获取焦点
                $("#search").focus();
                $("#myModal").modal("hide")
            }
        }
    })
})

//扫描单头id，显示单据明细
$("#scannSea").keypress(function (e) {
    if (e.keyCode == 13) {
        var retailId = $("#scannSea").val();
        sessionStorage.setItem("retailId", retailId);
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                retailId: retailId,
                op: 'scann'
            },
            dataType: 'text',
            success: function (data) {
                if (data == "记录不存在") {
                    swal({
                        title: "记录不存在",
                        text: "单据编号：" + retailId + "记录不存在",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
                var datas = data.split("|:");
                var math = datas[0].split(",");
                var total = math[0].split(":");
                $("#total").text(total[1]);
                $("#real").text(math[1]);
                $("#number").text(math[2]);
                $("#kind").text(math[3]);
                $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                $("#table").append(datas[1]);
                $("#scannSea").val("");
                $("#myModal1").modal("hide");
            }
        })
    }
})
//收银修改数量
$("#table").delegate(".numberEnd", "keypress", function (e) {
    if (e.keyCode == 13) {
        var id = $(this).parent().prev().prev().prev().prev().prev().text().trim();
        var number = parseInt($(this).val());
        var total = $(this).parent().next().next();
        var real = $(this).parent().next().next().next();
        if (number == 0) {
            swal({
                title: "请输入商品数量",
                text: "数量不能为0",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'customerRetail.aspx',
                data: {
                    retailId: id,
                    number: number,
                    op: 'change'
                },
                dataType: 'text',
                success: function (data) {
                    var datas = data.split("|:");
                    var succ = datas[0];
                    if (succ == "更新成功") {
                        var math = datas[1].split(",");
                        $("#number").text(math[0]);
                        $("#total").text(math[1]);
                        $("#real").text(math[2]);
                        total.text(datas[2]);
                        real.text(datas[3]);
                    } else if(succ == "更新失败"){
                        swal({
                            title: "修改数量失败",
                            text: "修改数量失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    }
                }
            })
        }
    }
})
//收银结算
$("#Settlement").click(function () {
    $("#totalEnd").text($("#total").text().trim());
    $("#realEnd").text($("#real").text().trim());
})
//收银折扣
$("#discountEnd").keypress(function (e) {
    if (e.keyCode == 13) {
        var discount = parseFloat($("#discountEnd").val());
        var retailId = sessionStorage.getItem("retailId");
        if (discount == 100) {
            $("#copeEnd").focus();
        }
        else if (discount == 0) {
            swal({
                title: "请输入折扣",
                text: "折扣不能为0",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'customerRetail.aspx',
                data: {
                    discount: discount,
                    retailId: retailId,
                    op: 'discount'
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "更新成功") {
                        var total = $("#totalEnd").text().trim();
                        var real = total * discount * 0.01;
                        $("#realEnd").text((real).toFixed(2))
                        $("#copeEnd").focus();
                    } else if (data == "更新失败") {
                        swal({
                            title: "错误提示",
                            text: "修改失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#discountEnd").val("100");
                    }
                }
            })
        }
    }
})
//收银删除
$("table").delegate(".delete", "click", function () {
    var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
    var tr = $(this).parent().parent();
    swal({
        title: "删除记录",
        text: "您确定要删除吗？",
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
            url: 'customerRetail.aspx',
            data: {
                retailId: id,
                headId: sessionStorage.getItem("retailId"),
                op: 'del'
            },
            dataType: 'text',
            success: function (data) {
                var succ = data.split("|:");
                if (succ[0] == "删除成功") {
                    tr.remove();
                    var kind = parseInt($("#kind").text());
                    $("#kind").text(kind - 1);
                    $("#number").text(succ[1]);
                    $("#total").text(succ[2]);
                    $("#real").text(succ[3]);
                }
                else if (succ[0] == "删除失败") {
                    swal({
                        title: "删除失败",
                        text: "删除失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
            }
        })
    })
})
//收银计算找零
$("#copeEnd").keypress(function (e) {
    if (e.keyCode == 13) {
        var give = parseFloat($("#copeEnd").val());
        var cope = parseFloat($("#realEnd").text().trim());
        var real = give - cope;
        $("#change").text((real).toFixed(2));
    }
})
$("#settleClose").click(function () {
    $("#totalEnd").text("");
    $("#discountEnd").val("100");
    $("#realEnd").text("");
    $("#copeEnd").val("");
    $("#change").text("");
})
//收银完成
$("#btnSettle").click(function () {
    if ($("#discountEnd").val().trim() == "" || $("#discountEnd").val().trim() == 0 || $("#discountEnd").val().trim() == "0") {
        swal({
            title: "请输入折扣",
            text: "折扣不能为空或0",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else if ($("#copeEnd").val().trim() == "" || $("#copeEnd").val().trim() == 0 || $("#copeEnd").val().trim() == "0") {
        swal({
            title: "请输入实付金额",
            text: "实付金额不能为空或0",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    }
    $.ajax({
        type: 'Post',
        url: 'customerRetail.aspx',
        data: {
            headId: sessionStorage.getItem("retailId"),
            op: 'end'
        },
        dataType: 'text',
        success: function (data) {
            if (data == "更新成功") {
                sessionStorage.removeItem("retailId");
                swal({
                    title: "结算完成",
                    text: "结算完成",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "success"
                }).catch(swal.noop);
                setTimeout(function () {
                    window.location.reload();
                }, 2000);
            } else {
                swal({
                    title: "结算失败",
                    text: "结算失败",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
        }
    })
})
//弹出模态框获取焦点事件
$('#myModal1').on('shown.bs.modal', function (e) {
    $('#scannSea').focus();
});
$('#myModal2').on('shown.bs.modal', function (e) {
    $('#discountEnd').focus();
});
