var curPage = $("#curPage").val();
var countPage = $("#pageCount").val();
sessionStorage.setItem("curPage", curPage);
sessionStorage.setItem("totalPage", countPage);
$(document).ready(function () {
    //分页
    $(".jump").click(function () {
        switch ($.trim($(this).text())) {
            case ('上一页'):
                if (parseInt(sessionStorage.getItem("curPage")) > 1) {
                    jump(parseInt(sessionStorage.getItem("curPage")) - 1);
                    break;
                } else {
                    jump(1);
                    break;
                }
            case ('下一页'):
                if (parseInt(sessionStorage.getItem("curPage")) < parseInt(sessionStorage.getItem("totalPage"))) {
                    jump(parseInt(sessionStorage.getItem("curPage")) + 1);
                    break;
                } else {
                    jump(parseInt(sessionStorage.getItem("totalPage")));
                    break;
                }
            case ("首页"):
                jump(1);
                break;
            case ("尾页"):
                jump(parseInt(sessionStorage.getItem("totalPage")));
                break;
        }
    });
    //地区下拉查询
    $("#select-region").change(function () {
        var regionId = $("#select-region").find("option:selected").val();
        alert(regionId);
    })

    //地址栏传值
    function jump(curr) {
        window.location.href = "customerManagement.aspx?currentPage=" + curr;
    }
})