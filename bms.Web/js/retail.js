$(document).ready(function () {
    $("#table tr:not(:first)").empty(); //清空table处首行
    $("#table").append(data); //加载table
})

$("#btnSearch").click(function () {
    var isbn = $("#search").val();
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
            url: 'retail.aspx',
            data: {
                isbn: isbn,
                op: 'isbn'
            },
            dataType: 'text',
            success: function (data) {
                if (data == "添加成功") {
                    window.location.reload();
                } else if (data == "添加失败") {
                    swal({
                        title: data,
                        text: data,
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else {
                    $("#myModal").modal("show");
                    $("#table2 tr:not(:first)").empty(); //清空table处首行
                    $("#table2").append(data); //加载table
                }
            }
        })
    }
})
$("#search").keypress(function (e) {
    if (e.keyCode == 13) {
        var isbn = $("#search").val();
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
                url: 'retail.aspx',
                data: {
                    isbn: isbn,
                    op: 'isbn'
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "添加成功") {
                        window.location.reload();
                    } else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else {
                        $("#myModal").modal("show");
                        $("#table2 tr:not(:first)").empty(); //清空table处首行
                        $("#table2").append(data); //加载table
                    }
                }
            })
        }
    }
})
$("#close").click(function () {

})
$("#insert").click(function () {
    $.ajax({
        type: 'Post',
        url: 'retail.aspx',
        data: {
            isbn: isbn,
            op: 'insert'
        },
        dataType: 'text',
        success: function (data) {
        }
    })
})