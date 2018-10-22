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
            url: 'checkStock.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

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
            $.ajax({
                type: 'Post',
                url: 'checkStock.aspx',
                data: {
                    page: api.getCurrent(), //页码
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
})

$("#export").click(function () {
    $.ajax({
        type: 'Post',
        url: 'checkStock.aspx',
        data: {
            op:'export'
        },
        dataType: 'text',
        success: function (data) {
            if (data == "没有数据，不能执行导出操作!") {
                swal({
                    title: "温馨提示:)",
                    text: "ISBN不能为空，请您重新输入",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
        }
    })
})

$("#print").click(function () {
    //$("#content").jqprint();
    $.ajax({
        type: 'Post',
        url: 'checkStock.aspx',
        data: {
            op: 'print'
        },
        dataType: 'text',
        success: function (data) {
            $("#table tr:not(:first)").remove();
            $("#table").append(data);
            var status = "";
            var LODOP = getLodop();
            var link = "";
            var style = "";
            LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
            LODOP.On_Return = function (TaskID, Value) {
                status = Value;
            };
            if (status != "" || status != null) {
                //LODOP.NewPage();
                //LODOP.SET_PRINT_PAGESIZE(3, "100%", "100%", "");
                //link = "<link rel='stylesheet' type='text/css' href='../css/zgz.css'><link rel='stylesheet' href='../css/material-dashboard.min.css'>";
                //style = "<style>body{background-color:white !important;}#table tr td{border: 1px solid black !important;padding:5px 5px;font-size:13px;}</style>";
                //LODOP.ADD_PRINT_HTM(0, 0, "100%", "100%", link + style + "<body>" + document.getElementById("content").innerHTML + "</body>");
                ////LODOP.SET_PRINTER_INDEX("Send To OneNote 2016");
                //LODOP.SET_SHOW_MODE("NP_NO_RESULT", true);
                //LODOP.PREVIEW();
                //window.location.reload();
                LODOP = getLodop();
                LODOP.PRINT_INITA(0, 0, 577, 10000, "打印控件功能演示_Lodop功能_不同高度幅面");
                //LODOP.ADD_PRINT_TEXT(5, 136, 275, 30, $("#RKId").val() + "入库单据");
                LODOP.SET_PRINT_PAGESIZE(3, 2000, 50, "");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(20, 20, 200, 20, "单据号：" + $("#RKId").val());
                LODOP.ADD_PRINT_TEXT(20, 220, 150, 20, "单据总数：" + $("#allCount").val());
                LODOP.ADD_PRINT_TEXT(20, 380, 150, 20, "总码洋：" + $("#allToatlPrice").val());
                LODOP.ADD_PRINT_TEXT(20, 540, 150, 20, "总实洋：" + $("#allRealPrice").val());
                //---------表格明细--------
                LODOP.ADD_PRINT_TEXT(50, 20, 50, 20, "序号");
                LODOP.ADD_PRINT_TEXT(50, 70, 100, 20, "ISBN号");
                LODOP.ADD_PRINT_TEXT(50, 170, 300, 20, "书名");
                LODOP.ADD_PRINT_TEXT(50, 440, 40, 20, "数量");
                LODOP.ADD_PRINT_TEXT(50, 480, 50, 20, "单价");
                LODOP.ADD_PRINT_TEXT(50, 530, 50, 20, "折扣");
                LODOP.ADD_PRINT_TEXT(50, 580, 60, 20, "码洋");
                LODOP.ADD_PRINT_TEXT(50, 640, 60, 20, "实洋");
                LODOP.ADD_PRINT_TEXT(50, 700, 80, 20, "货架");
                //表头表格
                LODOP.ADD_PRINT_LINE(44, 14, 44, 730, 0, 1);//一线(行)
                LODOP.ADD_PRINT_LINE(76, 14, 44, 14, 0, 1);//1
                LODOP.ADD_PRINT_LINE(76, 65, 44, 65, 0, 1);//2
                LODOP.ADD_PRINT_LINE(76, 165, 44, 165, 0, 1);//3
                LODOP.ADD_PRINT_LINE(76, 435, 44, 435, 0, 1);//4
                LODOP.ADD_PRINT_LINE(76, 475, 44, 475, 0, 1);//5
                LODOP.ADD_PRINT_LINE(76, 515, 44, 515, 0, 1);//6
                LODOP.ADD_PRINT_LINE(76, 565, 44, 565, 0, 1);//7
                LODOP.ADD_PRINT_LINE(76, 625, 44, 625, 0, 1);//8
                LODOP.ADD_PRINT_LINE(76, 685, 44, 685, 0, 1);//9
                LODOP.ADD_PRINT_LINE(44, 730, 76, 730, 0, 1);//10
                LODOP.ADD_PRINT_LINE(76, 14, 76, 730, 0, 1);//二线(行)

                //--行内容
                var j = $("#table").find("tr").length;
                for (i = 0; i < j; i++) {
                    var row = $("#table").find('tr').eq(i+1).find('td');
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 20, 50, 20, (i+1));
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 70, 100, 20, row.eq(1).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 170, 300, 20, row.eq(2).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 440, 50, 20, row.eq(3).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 480, 50, 20, row.eq(4).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 530, 50, 20, row.eq(5).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 580, 60, 20, row.eq(6).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 640, 60, 20, row.eq(7).text().trim());
                    LODOP.ADD_PRINT_TEXT(81 + 25 * i, 700, 80, 20, row.eq(8).text().trim());
                    //--格子画线		
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 14, 76 + 25 * i, 15, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 65, 76 + 25 * i, 65, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 165, 76 + 25 * i, 165, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 435, 76 + 25 * i, 435, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 475, 76 + 25 * i, 475, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 515, 76 + 25 * i, 515, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 565, 76 + 25 * i, 565, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 625, 76 + 25 * i, 625, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 685, 76 + 25 * i, 685, 0, 1);
                    LODOP.ADD_PRINT_LINE(101 + 25 * i, 730, 76 + 25 * i, 730, 0, 1);
                    LODOP.ADD_PRINT_LINE(102 + 25 * i, 14, 101 + 25 * i, 730, 0, 1);
                }
                //------------end-------------
                LODOP.PREVIEW();//打印预览	
            }
        }
    })
})
