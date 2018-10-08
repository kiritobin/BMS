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
                            $("#tablebook th:not(:first)").empty(); //清空table处首行
                            $("#tablebook").append(data); //加载table
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
        $.ajax({
            type: 'Post',
            url: 'backQuery.aspx',
            data: {
                op: "back"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "返回") {
                    window.location.href = "tradeManagement.aspx";
                } else {
                    window.location.href = "tradeManagement.aspx";
                }
            }
        });
    })
})