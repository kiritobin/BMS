//存储当前页数
var page = $("#page").val();
sessionStorage.setItem("page", page);
//存储总页数
var countPage = $("#countPage").val();
sessionStorage.setItem("countPage", countPage);
//点击翻页按钮
$(".jump").click(function () {
    switch ($(this).text().trim()) {
        //点击上一页按钮时
        case ('上一页'):
            if (parseInt(sessionStorage.getItem("page")) > 1) {
                jump(parseInt(sessionStorage.getItem("page")) - 1);
                sessionStorage.setItem("page", parseInt(sessionStorage.getItem("page")) - 1);
                break;
            } else {
                jump(1);
                break;
            }
            //点击下一页按钮时
        case ('下一页'):
            if (parseInt(sessionStorage.getItem("page")) < parseInt(sessionStorage.getItem("countPage"))) {
                jump(parseInt(sessionStorage.getItem("page")) + 1);
                sessionStorage.setItem("page", parseInt(sessionStorage.getItem("page")) + 1);
                break;
            } else {
                jump(parseInt(sessionStorage.getItem("countPage")));
                break;
            }
            //点击首页按钮时
        case ('首页'):
            jump(1);
            break;
            //点击尾页按钮时
        case ('尾页'):
            jump(parseInt(sessionStorage.getItem("countPage")));
            break;
    }
});
//翻页时获取当前页数
function jump(cur) {
    var strWhere = sessionStorage.getItem("strWhere");
    var type = sessionStorage.getItem("type");
    if (strWhere != null && strWhere != "") {
        window.location.href = "functionManagement.aspx?currentPage=" + cur + "&search=" + strWhere + "&type=" + type;
    } else {
        window.location.href = "functionManagement.aspx?currentPage=" + cur
    }
}
//点击查询按钮时
$("#btn-search").click(function () {
    var strWhere = $(".input-search").val();
    sessionStorage.setItem("strWhere", strWhere);
    sessionStorage.setItem("type", "search");
    jump(1);
});
//点击添加按钮时
$("#btnAdd").click(function () {
    var name = $("#functionName").val();
    $.ajax({
        type: 'Post',
        url: 'functionManagement.aspx',
        data: {
            functionName: name,
            op: "add"
        },
        dataType: 'text',
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
                    window, location.reload();
                })
            } else {
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
                    window, location.reload();
                })
            }
        }
    })
})
//删除用户
$(".btn-delete").click(function () {
    var functionId = $(this).prev().val();
    //弹窗
    swal({
        title: "是否删除？",
        text: "删除了将无法恢复！！！",
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
            url: 'functionManagement.aspx',
            data: {
                functionId: functionId,
                op: "del"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
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
                        window, location.reload();
                    })
                } else {
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
                        window, location.reload();
                    })
                }
            }
        })
    })
})