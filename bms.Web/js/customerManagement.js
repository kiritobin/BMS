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
        sessionStorage.setItem("region", regionId);
        if (sessionStorage.getItem("strWhere") != null) {
            sessionStorage.removeItem("strWhere");
        }
        jump(1);
    })
    //按钮查询
    $("#btn-search").click(function () {
        var str = $("#search_All").val();
        sessionStorage.setItem("strWhere", str);
        jump(1);
    })

    //地址栏传值
    function jump(curr) {
        if (sessionStorage.getItem("region") != null && sessionStorage.getItem("region") != "" && sessionStorage.getItem("strWhere") == null && sessionStorage.getItem("region") != "0") {
            window.location.href = "customerManagement.aspx?currentPage=" + curr + "&regionID=" + sessionStorage.getItem("region");
        }
        else if (sessionStorage.getItem("region") == null && sessionStorage.getItem("strWhere") != null) {
            window.location.href = "customerManagement.aspx?currentPage=" + curr + "&strWhere=" + sessionStorage.getItem("strWhere");
        }
        else {
            window.location.href = "customerManagement.aspx?currentPage=" + curr;
        }
    }

    //添加客户
    $("#btnAdd").click(function () {
        var id = $("#customerId").val();
        var name = $("#customerName").val();
        var regionID = $("#model-select-region").find("option:selected").val();
        if (id == "" || name == "" || regionID == "") {
            alert("账号、姓名和地区名称都不能为空！");
        }
        else {
            aler("ajax");
            $.ajax({
                type: 'Post',
                url: 'customerManagerment.aspx',
                data: {
                    customerId: id,
                    cutomerName: name,
                    zoneId: regionID,
                    op:"add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == 添加成功) {
                        alert("添加成功");
                    }
                    else {
                        alert("添加失败");
                    }
                }
            })
        }
    })
})