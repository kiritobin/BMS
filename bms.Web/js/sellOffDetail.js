//时间选择器
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

$(document).ready(function () {
    $(".paging").pagination({
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var isbn = $("#isbn").val();
            var price = $("#price").val();
            var discount = $("#discount").val();
            var user = $("#user").val();
            var time = $("#time").val();
            var state = $("#state").val();
            $.ajax({
                type: 'Post',
                url: 'sellOffDetail.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    isbn: isbn,
                    price: price,
                    discount: discount,
                    user: user,
                    time: time,
                    state: state,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
                }
            });
        }
    });
    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#myModal").modal('hide');
    });
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
    });
    //点击查询按钮
    $("#search").click(function () {
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var user = $("#user").val();
        var time = $("#time").val();
        var state = $("#state").val();
        $.ajax({
            type: 'Post',
            url: 'sellOffDetail.aspx',
            data: {
                isbn: isbn,
                price: price,
                discount: discount,
                user: user,
                time: time,
                state: state,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
                $(".paging").pagination({
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
                            url: 'sellOffDetail.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                isbn: isbn,
                                price: price,
                                discount: discount,
                                user: user,
                                time: time,
                                state: state,
                                op: "paging"
                            },
                            dataType: 'text',
                            success: function (data) {
                                $("#table tr:not(:first)").remove(); //清空table处首行
                                $("#table").append(data); //加载table
                                $("#intPageCount").remove();
                            }
                        });
                    }
                });
            }
        });
    });
    //导出
    $("#export").click(function () {
        window.location.href = "sellOffDetail.aspx?op=export";
    })
    //返回上一页
    $("#back").click(function () {
        window.location.href = "selloffStatistics.aspx";
    })
});
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
            url: 'sellOffDetail.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//打印
var LODOP; //声明为全局变量
function createdate() {
    //------循环画线例子begin-----			
    LODOP = getLodop();
    LODOP.PRINT_INITA(0, 0, 577, 10000, "打印控件功能演示_Lodop功能_不同高度幅面");
    LODOP.SET_PRINT_PAGESIZE(3, 1505, 45, "");
    LODOP.ADD_PRINT_TEXT(8, 136, 275, 30, "销退统计报表明细打印");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 110, 20, "ISBN");
    LODOP.ADD_PRINT_TEXT(50, 120, 130, 20, "书号");
    LODOP.ADD_PRINT_TEXT(50, 260, 200, 20, "书名");
    LODOP.ADD_PRINT_TEXT(50, 470, 100, 20, "定价");
    LODOP.ADD_PRINT_TEXT(50, 520, 50, 20, "数量");
    LODOP.ADD_PRINT_TEXT(50, 570, 80, 20, "码洋");
    LODOP.ADD_PRINT_TEXT(50, 570, 80, 20, "实洋");
    LODOP.ADD_PRINT_LINE(44, 14, 44, 720, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 14, 44, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 110, 44, 110, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 250, 44, 250, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 465, 44, 465, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 512, 44, 512, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 560, 44, 560, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 620, 44, 620, 0, 1);
    LODOP.ADD_PRINT_LINE(76, 14, 76, 720, 0, 1);

    //--行内容
    var j = $("#table").find("tr").length;
    var row = $("#table").find('tr');
    for (i = 0; i < j - 1; i++) {
        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 16, 110, 20, row.eq(i + 1).find('td').eq(1).text().trim());
        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 120, 130, 20, row.eq(i + 1).find('td').eq(2).text().trim());
        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 260, 200, 20, row.eq(i + 1).find('td').eq(3).text().trim());
        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 470, 81, 20, row.eq(i + 1).find('td').eq(4).text().trim());
        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 520, 81, 20, row.eq(i + 1).find('td').eq(5).text().trim());
        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 570, 81, 20, row.eq(i + 1).find('td').eq(6).text().trim());
        //--格子画线		
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 14, 76 + 25 * i, 15, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 110, 76 + 25 * i, 110, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 250, 76 + 25 * i, 250, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 465, 76 + 25 * i, 465, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 512, 76 + 25 * i, 512, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 560, 76 + 25 * i, 560, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 720, 76 + 25 * i, 720, 0, 1);
        LODOP.ADD_PRINT_LINE(101 + 25 * i, 14, 101 + 25 * i, 720, 0, 1);
    }
    LODOP.ADD_PRINT_LINE(101 + 25 * j, 14, 102 + 25 * j, 510, 0, 1);
    LODOP.ADD_PRINT_TEXT(105 + 25 * j, 20, 300, 20, "打印时间：‎2015‎-‎12‎-‎15 ‎12‎:‎19‎");
    LODOP.ADD_PRINT_TEXT(105 + 25 * j, 346, 150, 20, "合计金额：" + 10 * j);
    //------------end-------------
};
function mypreview() {
    LODOP = getLodop();
    createdate();
    LODOP.PREVIEW();//打印预览	
}