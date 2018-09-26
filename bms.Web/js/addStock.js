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
            url: 'addStock.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

$("#btnAdd").click(function () {
    var ID = $("#ID").val();
    var isbn = $("#isbn").val();
    var allCount = $("#allCount").val();
    var price = $("#price").val();
    var discount = $("#discount").val();
    var realPrice = $("#realPrice").val();
    var allPrice = $("#allPrice").val();
    var goodsShelf = $("#goodsShelf").val();
    var remark = $("#remark").val();
    $.ajax({
        type: 'Post',
        url: 'addStock.aspx',
        data: {
            ID: ID,
            isbn: isbn,
            allCount: allCount,
            price: price,
            discount: discount,
            realPrice: realPrice,
            allPrice: allPrice,
            goodsShelf: goodsShelf,
            remark: remark,
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
})
