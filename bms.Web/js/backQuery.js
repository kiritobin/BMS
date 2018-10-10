$(document).ready(function () {
    //回车事件
    $(".addsell").keypress(function (e) {
        if (e.keyCode == 13) {
            var ISBN = $("#isbn").val();
            var realDiscount = $("#realDiscount").val();
            var count = $("#count").val();
            if (ISBN == ""||realDiscount==""||count=="") {
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
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: ISBN,
                        discount: realDiscount,
                        count:count,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功"||data=="更新成功") {
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
                            //$("#tablebook th:not(:first)").empty(); //清空table处首行
                            $(".first").remove();
                            //$("#tablebook").empty();
                            $("#table").append(data); //加载table
                        }
                    }
                });
            }
        }
    })
    //销退单体添加
    $("#add_back").click(function () {
        var discount = $("#sellId").val();
        $("#realDiscount").val(discount);
    })
    //提交添加信息
    $("#btnAdd").click(function () {
        bookNum = $("input[type='radio']:checked").val();
        var ISBN = $("#isbn").val();
        var realDiscount = $("#realDiscount").val();
        var count = $("#count").val();
        if (ISBN == "" || realDiscount == "" || count == "" || bookNum == "") {
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
                url: 'backQuery.aspx',
                data: {
                    bookNum:bookNum,
                    ISBN: ISBN,
                    discount: realDiscount,
                    count: count,
                    op: "search"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "添加成功" || data == "更新成功") {
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
                       // $("#tablebook th:not(:first)").empty(); //清空table处首行
                        //$("#tablebook").append(data); //加载table
                        swal({
                            title: "温馨提示",
                            text: data,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-warning',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            $("#isbn").focus();
                        })
                    }
                }
            });
        }
    })
    //返回事件
    $("#toBack").click(function () {
        window.location.href = "backManagement.aspx";
    });
    //保存单据
    $("#sure").click(function () {
        swal({
            title: "温馨提示:)",
            text: "保存单据后将无法再进行修改",
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
                url: 'backQuery.aspx',
                data: {
                    op: "sure"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "更新成功") {
                        swal({
                            title: "提示",
                            text: "保存成功",
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
    })
    $("#table").delegate("#inputISBN", "keypress", function (e) {
        if (e.keyCode == 13) {
            var isbn = $('table input:eq(0)').val();
            if (isbn == "") {
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
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: isbn,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        $("#table").append(data); //加载table
                    }
                });
            }
        }
    })
})