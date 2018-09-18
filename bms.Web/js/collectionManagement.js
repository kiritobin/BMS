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
    var region = sessionStorage.getItem("region");
    var type = sessionStorage.getItem("type");
    if (strWhere != null && strWhere != "") {
        window.location.href = "collectionManagement.aspx?currentPage=" + cur + "&search=" + strWhere + "&type=" + type;
    } else if (region != null || region != "") {
        window.location.href = "collectionManagement.aspx?currentPage=" + cur + "&region=" + region + "&type=" + type;
    } else {
        window.location.href = "collectionManagement.aspx?currentPage=" + cur
    }
}
//点击查询按钮时
$("#btn-search").click(function () {
    var strWhere = $("#search").val();
    sessionStorage.setItem("strWhere", strWhere);
    sessionStorage.setItem("type", "search");
    jump(1);
});
//地区下拉框改变事件
$("#select-region").change(function () {
    var regionId = $("#select-region").val();
    if (regionId == 0) {
        sessionStorage.removeItem("region");
        sessionStorage.removeItem("type");
    } else {
        sessionStorage.setItem("region", regionId);
        sessionStorage.setItem("type","region");
    }
})