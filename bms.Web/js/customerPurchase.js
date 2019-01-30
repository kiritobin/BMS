jeDate("#startTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});
jeDate("#endTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});
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
            url: 'customerPurchase.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../customerLogin.aspx";
            }
        });
    })
}
var kNum = $("#kinds").val();
$("#bookKinds").attr("value", kNum);
var aNum = $("#alln").val();
$("#allBookCount").attr("value", aNum);
var atp = $("#allt").val();
$("#alltotalprice").attr("value", atp);
var arp = $("#allr").val();
$("#allreadprice").attr("value", arp);

$(document).ready(function () {
    $('.paging').pagination({
        //totalData: $("#countPage").val(), //数据总数
        //showData: $("#totalCount").val(), //每页显示的条数
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            //var bookName = $("#bookSearch").val().trim();
            //var source = $("#goodsSearch").val().trim();
            //var isbn = $("#isbnSearch").val().trim();
            //var time = $("#time").val();
            $.ajax({
                type: 'Post',
                url: 'customerPurchase.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });

    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#myModal").modal('hide');
    })
    //选择时间后确定
    $("#btnOK").click(function () {
        var startTime = $("#startTime").val();
        var endTime = $("#endTime").val();
        if (startTime == "" || startTime == null) {
            swal({
                title: "提示",
                text: "请选择开始时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else if (endTime == "" || endTime == null) {
            swal({
                title: "提示",
                text: "请选择结束时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            $("#time").val(startTime + "至" + endTime);
            $("#myModal").modal('hide');
        }
    })
    $("#btn-search").click(function () {
        var bookName = $("#bookSearch").val().trim();
        var source = $("#goodsSearch").val().trim();
        var isbn = $("#isbnSearch").val().trim();
        var time = $("#time").val();
        $.ajax({
            type: 'Post',
            url: 'customerPurchase.aspx',
            data: {
                bookName: bookName,
                source: source,
                isbn: isbn,
                time: time,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#kinds").remove();
                $("#alln").remove();
                $("#allt").remove();
                $("#allr").remove();
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
                var kNum = $("#kinds").val();
                $("#bookKinds").attr("value", kNum);
                var aNum = $("#alln").val();
                $("#allBookCount").attr("value", aNum);
                var atp = $("#allt").val();
                $("#alltotalprice").attr("value", atp);
                var arp = $("#allr").val();
                $("#allreadprice").attr("value", arp);
                $('.paging').pagination({
                    //totalData: $("#totalCount").val(),
                    //showData: $("#pageSize").val(),
                    pageCount: $("#intPageCount").val(), //总页数
                    jump: true,
                    mode: 'fixed',//固定页码数量
                    coping: true,
                    homePage: '首页',
                    endPage: '尾页',
                    prevContent: '上页',
                    nextContent: '下页',
                    callback: function (api) {
                        $.ajax({
                            type: 'Post',
                            url: 'customerPurchase.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                bookName: bookName,
                                source: source,
                                isbn: isbn,
                                time: time,
                                op: "paging"
                            },
                            dataType: 'text',
                            success: function (data) {
                                $("#table tr:not(:first)").remove(); //清空table处首行
                                $("#table").append(data); //加载table
                                $("#intPageCount").remove();
                                //var k = $("#kinds").val();
                                //$("#bookKinds").attr("value", k);
                            }
                        });
                    }
                });
            }
        });
    });
});