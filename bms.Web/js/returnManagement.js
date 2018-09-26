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
            url: 'returnManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//添加退货单头
$("#btnAdd").click(function () {
    var billCount = $("#billCount").val();
    var totalPrice = $("#totalPrice").val();
    var realPrice = $("#realPrice").val();
    var arrivalTime = $("#arrivalTime").val();
    var payTime = $("#payTime").val();
    if (billCount == "") {
        alert("单据总数不能为空");
    } else if (totalPrice == "") {
        alert("总码洋不能为空");
    } else if (realPrice == "") {
        alert("总实洋不能为空");
    } else if (arrivalTime == "") {
        alert("到货时间不能为空");
    } else if (payTime == "") {
        alert("付款时间不能为空");
    } else {
        $.ajax({
            type: 'Post',
            url: 'returnManagement.aspx',
            data: {
                billCount: billCount,
                totalPrice: totalPrice,
                realPrice: realPrice,
                op:"add"
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
                        window.location.reload();
                    })
                }
            }
        })
    }
})
