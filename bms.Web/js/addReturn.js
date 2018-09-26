$(document).ready(function () {
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
                url: 'addReturn.aspx?op=logout',
                datatype: 'text',
                data: {},
                success: function (data) {
                    window.location.href = "../login.aspx";
                }
            });
        })
    }
    $("#btn-add").click(function () {
        var addISBN = $("#addISBN").val().trim();
        var addNum = $("#addNum").val().trim();
        var addPrice = $("#addPrice").val().trim();
        var addDiscount = $("#addDiscount").val().trim();
        var addOcean = $("#addOcean").val().trim();
        var addTotalPrice = $("#addTotalPrice").val().trim();
        // $("#shelfId").find("option:selected").text();
        var shelfId = $("#shelfId").val().trim();
        if (addISBN == "" || addNum == "" || addPrice == "" || addDiscount == "" || addOcean == "" || shelfId == "") {
            swal({
                title: "温馨提示:)",
                text: "不能含有未填项",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        } else {
            $.ajax({
                type: 'Post',
                url: 'addReturn.aspx',
                data: {
                    addISBN: addISBN,
                    addNum: addNum,
                    addPrice: addPrice,
                    addDiscount: addDiscount,
                    addOcean: addOcean,
                    shelfId: shelfId,
                    addTotalPrice:addTotalPrice,
                    op:"add"
                }, datatype: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "添加成功",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "success",
                            allowOutsideClick: false
                        }).then(function () { })
                    }
                    else if (succ == "添加失败") {
                        swal({
                            title: "温馨提示:)",
                            text: "添加失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        })
                    }
                }
            })
        }
    })

})
