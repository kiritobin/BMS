$(document).ready(function () {
    //只允许输入数字
    $("#table").delegate("#inputISBN", "keyup", function (e) {
        $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).bind("paste", function () {  //CTR+V事件处理    
        $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).css("ime-mode", "disabled");
    $("#table").delegate("#inputCount", "keyup", function (e) {
        $(this).val($(this).val().replace(/[^\r\n\-0-9]/g, ''));//允许输入“-”运算符号
    }).bind("paste", function () {  //CTR+V事件处理    
        $(this).val($(this).val().replace(/[^\r\n\-0-9]/g, ''));
    }).css("ime-mode", "disabled");
    //ISBN回车事件
    $("#table").delegate("#inputISBN", "keypress", function (e) {
        if (e.keyCode == 13) {
            var isbn = $(this).val();
            if (isbn == "") {
                swal({
                    title: "温馨提示:)",
                    text: "ISBN不能为空!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else {
                $.ajax({
                    type: 'Post',
                    url: 'addRs.aspx',
                    data: {
                        ISBN: isbn,
                        op: "searchISBN"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "暂无此数据") {
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
                                $("#table").find('tr').eq(1).find('td').eq(2).find('input').focus();
                            })
                        }
                        else if (data.indexOf("much") > 0) {
                            $("#tablebook").empty();
                            $("#myModa2").modal("show");
                            $("#tablebook").append(data); //加载table
                        }
                        else {
                            $("#table").empty();
                            $("#table").append(data); //加载table
                            $("#table tr:first").find("td").eq(5).children().focus();
                            $("#table").find('tr').eq(1).find('td').eq(6).find('input').focus();
                        }
                    }
                });
            }
        }
    })
    //一号多模态框书确定书籍
    $("#sureBook").click(function () {
        var isbn = $("input[type='radio']:checked").parents('tr').find('td').eq(1).text();
        var bookNum = $("input[type='radio']:checked").val();
        var bookName = $("input[type='radio']:checked").parents('tr').find('td').eq(3).text();
        var price = $("input[type='radio']:checked").parents('tr').find('td').eq(4).text();
        var discount = $("input[type='radio']:checked").parents('tr').find('td').eq(4).text();
        $.ajax({
            type: 'Post',
            url: 'addRs.aspx',
            data: {
                ISBN: isbn,
                bookNO: bookNum,
                bookName: bookName,
                price: price,
                op: "searchISBN"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "销售单据中无此数据") {
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
                        $("#myModa2").modal("show");
                    })
                }
                else {
                    $("#myModa2").modal("hide");
                    $("#table").empty();
                    $("#table").append(data); //加载table
                    $("#table tr:first").find("td").eq(5).children().focus();
                    $("#table").find('tr').eq(1).find('td').eq(6).find('input').focus();
                }
            }
        })
    })

    $("#table").delegate("#inputCount", "keypress", function (e) {
        if (e.keyCode == 13) {
            var num = $("#table").find('tr').eq(1).find('td').eq(6).find('input').val();
            if (num == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else if (num == "0") {
                swal({
                    title: "温馨提示:)",
                    text: "数量不能未0!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        count: num,
                        op: "add"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功") {
                            swal({
                                title: "温馨提示",
                                text: data,
                                type: "success",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window.location.reload();
                            })
                        }
                        else {
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
                                $("#table").find('tr').eq(1).find('td').eq(4).find('input').focus();
                            })
                        }
                    }
                });
            }
        }
    })


})