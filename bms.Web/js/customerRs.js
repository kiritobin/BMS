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
            $.ajax({
                type: 'Post',
                url: 'customerRs.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    op: "paging"

                },
                dataType: 'text',
                success: function (data) {
                    $("#table tbody").empty(); //清空table处首行
                    $("#table tbody").append(data); //加载table
                }
            });
        }
    });
})

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
            url: 'customerRs.aspx',
            datatype: 'text',
            data: {
                op: "logout"
            },
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//地区下拉框查询
$("#btn_search").click(function () {
    var cusId = $("#customer").val();
    $.ajax({
        type: 'post',
        url: 'customerRs.aspx',
        datatype: 'text',
        data: {
            cusId: cusId,
            op: "paging"
        },
        success: function (succ) {
            var data = succ.split(":|");
            $("#intPageCount").remove();
            $("#table tr:not(:first)").remove(); //清空table处首行
            $("#table").append(data[0]); //加载table
            $("#kinds").val(data[1]);
            $("#count").val(data[2]);
            $("#region").val(data[3]);
            $(".paging").empty();
            $('.paging').pagination({
                pageCount: $("#intPageCount").val(), //总页数
                jump: true,
                mode: 'fixed',//固定页码数量
                coping: true,
                homePage: '首页',
                endPage: '尾页',
                prevContent: '上页',
                nextContent: '下页',
                callback: function (api) {
                    var book = $("#bookSearch").val();
                    var isbn = $("#isbnSearch").val();
                    $.ajax({
                        type: 'Post',
                        url: 'customerRs.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            cusId: cusId,
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
})

//打印
$("#print").click(function () {
    //$("#content").jqprint();
    var cusId = $("#customer").val();
    if (cusId == null || cusId == "") {
        swal({
            title: "提示",
            text: "请选择需要查询的客户",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-success',
            buttonsStyling: false,
            allowOutsideClick: false
        })
    }
    else {
        var LODOP = getLodop();
        var link = "";
        var style = "";
        LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
        LODOP.On_Return = function (TaskID, Value) {
            status = Value;
        };
        if (status != "" || status != null) {
            LODOP = getLodop();
            //LODOP.PRINT_INITA(0, 0, 577, 10000, "打印控件功能演示_Lodop功能_不同高度幅面");
            LODOP.ADD_PRINT_TEXT(30, 200, 600, 30, $("#region").val() + "新华书店有限公司   补货单");
            LODOP.SET_PRINT_PAGESIZE(3, 2250, 0, "");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", 1);
            //------统计-----
            LODOP.ADD_PRINT_TEXT(80, 100, 200, 20, "书籍种类：" + $("#kinds").val());
            LODOP.ADD_PRINT_TEXT(120, 100, 200, 20, "总数量：" + $("#count").val());
            LODOP.ADD_PRINT_TEXT(160, 100, 150, 20, "地区：" + $("#region").val());
            //---------表格明细--------
            LODOP.ADD_PRINT_TEXT(210, 20, 50, 20, "序号");
            LODOP.ADD_PRINT_TEXT(210, 70, 100, 20, "ISBN号");
            LODOP.ADD_PRINT_TEXT(210, 170, 120, 20, "书号");
            LODOP.ADD_PRINT_TEXT(210, 300, 300, 20, "书名");
            LODOP.ADD_PRINT_TEXT(210, 600, 50, 20, "数量");
            LODOP.ADD_PRINT_TEXT(210, 650, 70, 20, "客户");
            LODOP.ADD_PRINT_TEXT(210, 710, 60, 20, "地区");
            LODOP.ADD_PRINT_TEXT(210, 790, 70, 20, "日期");
            //表头表格
            LODOP.ADD_PRINT_LINE(200, 14, 200, 835, 0, 1);//一线(行)
            LODOP.ADD_PRINT_LINE(225, 14, 200, 14, 0, 1);//1
            LODOP.ADD_PRINT_LINE(225, 65, 200, 65, 0, 1);//2
            LODOP.ADD_PRINT_LINE(225, 165, 200, 165, 0, 1);//3
            LODOP.ADD_PRINT_LINE(225, 295, 200, 295, 0, 1);//4
            LODOP.ADD_PRINT_LINE(225, 595, 200, 595, 0, 1);//5
            LODOP.ADD_PRINT_LINE(225, 645, 200, 645, 0, 1);
            LODOP.ADD_PRINT_LINE(225, 705, 200, 705, 0, 1);//7
            LODOP.ADD_PRINT_LINE(225, 765, 200, 765, 0, 1);//8
            LODOP.ADD_PRINT_LINE(225, 835, 200, 835, 0, 1);//9
            //LODOP.ADD_PRINT_LINE(225, 840, 200, 730, 0, 1);//10
            LODOP.ADD_PRINT_LINE(225, 14, 225, 835, 0, 1);//二线(行)

            //--行内容
            var j = $("#table").find("tr").length;
            for (i = 0; i < j; i++) {
                var row = $("#table").find('tr').eq(i + 1).find('td');
                LODOP.ADD_PRINT_TEXT(235 + 25 * i, 20, 50, 20, (i + 1));
                LODOP.ADD_PRINT_TEXT(235 + 25 * i, 70, 100, 20, row.eq(1).text().trim());
                LODOP.ADD_PRINT_TEXT(235 + 25 * i, 170, 120, 20, row.eq(2).text().trim());
                if (row.eq(3).text().trim().length > 20) {
                    LODOP.ADD_PRINT_TEXT(235 + 25 * i, 300, 300, 20, row.eq(3).text().trim());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
                }
                else {
                    LODOP.ADD_PRINT_TEXT(235 + 25 * i, 300, 300, 20, row.eq(3).text().trim());
                }
                LODOP.ADD_PRINT_TEXT(235 + 25 * i, 600, 50, 20, row.eq(4).text().trim());
                if (row.eq(5).text().trim().length > 4 || row.eq(6).text().trim().length > 4) {
                    LODOP.ADD_PRINT_TEXT(235 + 25 * i, 650, 70, 20, row.eq(5).text().trim());
                    LODOP.ADD_PRINT_TEXT(235 + 25 * i, 710, 60, 20, row.eq(6).text().trim());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
                }
                else {
                    LODOP.ADD_PRINT_TEXT(235 + 25 * i, 650, 70, 20, row.eq(5).text().trim());
                    LODOP.ADD_PRINT_TEXT(235 + 25 * i, 710, 60, 20, row.eq(6).text().trim());
                }
                LODOP.ADD_PRINT_TEXT(235 + 25 * i, 770, 70, 20, row.eq(7).text().trim());
                //--格子画线		
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 14, 225 + 25 * i, 15, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 65, 225 + 25 * i, 65, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 165, 225 + 25 * i, 165, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 295, 225 + 25 * i, 295, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 595, 225 + 25 * i, 595, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 645, 225 + 25 * i, 645, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 705, 225 + 25 * i, 705, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 765, 225 + 25 * i, 765, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 835, 225 + 25 * i, 835, 0, 1);
                LODOP.ADD_PRINT_LINE(250 + 25 * i, 14, 250 + 25 * i, 835, 0, 1);
            }
            //------------end-------------
            LODOP.SET_PRINT_MODE("POS_BASEON_PAPER", true);
            LODOP.PREVIEW();//打印预览
        }
    }
})