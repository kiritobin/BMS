$(document).ready(function () {
    checkLogin();
    //单点登录校验
    $("body").click(function () {
        checkLogin();
    })
});

function checkLogin() {
    var url = window.location.pathname;
    var check = "";
    if (url == "/welcomePage.aspx") {
        check = "checkLogined.aspx";
    }
    else {
        check = "../checkLogined.aspx";
    }
    $.ajax({
        type: 'post',
        url: check,
        datatype: 'text',
        data: {

        },
        success: function (data) {
            if (data == "已登录") {
                if (url == "/welcomePage.aspx") {
                    window.location.href = "login.aspx?logout=force";
                }
                else {
                    window.location.href = "../login.aspx?logout=force";
                }
            }
        },
        error: function (XMLHttpRequest, textStatus) { //请求失败
            if (url == "/welcomePage.aspx") {
                window.location.href = "login.aspx?logout=error";
            }
            else {
                window.location.href = "../login.aspx?logout=error";
            }
            //swal({
            //    title: "注意",
            //    text: "会话失败，请重新登录！",
            //    type: "warning",
            //    confirmButtonColor: '#3085d6',
            //    confirmButtonText: '确定',
            //    confirmButtonClass: 'btn btn-success',
            //    buttonsStyling: false,
            //    allowOutsideClick: false
            //}).then(function () {
            //    if (url == "/welcomePage.aspx") {
            //        window.location.href = "login.aspx";
            //    }
            //    else {
            //        window.location.href = "../login.aspx";
            //    }
            //});
        }
    });
}
