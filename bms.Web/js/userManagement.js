//存储当前页数
var page = $("#page").val();
sessionStorage.setItem("page", page);
//存储总页数
var countPage = $("#countPage").val();
sessionStorage.setItem("countPage", countPage);

//点击翻页按钮
$(".jump").click(function () {
    switch ($.trim($(this).html())) {
        //点击上一页按钮时
        case ('previous'):
            if (parseInt(sessionStorage.getItem("page")) > 1) {
                jump(parseInt(sessionStorage.getItem("page")) - 1);
                sessionStorage.setItem("page", parseInt(sessionStorage.getItem("page")) - 1);
                break;
            } else {
                jump(1);
                break;
            }
        //点击下一页按钮时
        case ('next'):
            if (parseInt(sessionStorage.getItem("page")) < parseInt(sessionStorage.getItem("countPage"))) {
                jump(parseInt(sessionStorage.getItem("page")) + 1);
                sessionStorage.setItem("page", parseInt(sessionStorage.getItem("page")) + 1);
                break;
            } else {
                jump(parseInt(sessionStorage.getItem("countPage")));
                break;
            }
        //点击首页按钮时
        case ("first"):
            jump(1);
            break;
        //点击尾页按钮时
        case ("last"):
            jump(parseInt(sessionStorage.getItem("countPage")));
            break;
    }
});
//点击查询按钮时
$("#btn-search").click(function () {
    var strWhere = $("#inputsearch").val();
    sessionStorage.setItem("strWhere", strWhere);
    jump(1);
});
//翻页时获取当前页数
function jump(cur) {
    if (sessionStorage.getItem("strWhere") === null) {
        window.location.href = "adminList.aspx?currentPage=" + cur
    } else {
        window.location.href = "adminList.aspx?currentPage=" + cur + "&search=" + sessionStorage.getItem("strWhere");
    }
}