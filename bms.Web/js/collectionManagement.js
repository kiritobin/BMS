$(document).ready(function () {
    //存储当前页
    sessionStorage.setItem("page", $("#page").val());
    //存储总页数
    sessionStorage.setItem("countPage", $("#countPage").val());
    //当总页数为1时，首页与尾页按钮隐藏
    if (sessionStorage.getItem("countPage") === "1") {
        $("#first").hide();
        $("#last").hide();
    }

    $("#btn-search").click(function () {
        var search = $("#search").val();
       
    });
});