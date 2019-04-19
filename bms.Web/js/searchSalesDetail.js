$(document).ready(function () {
    $("#ccustomer").text($("#customer").val());
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
                url: 'searchSalesDetail.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });
    //$("#back").click(function () {
    //    window.location.href = "../SalesMGT/salesManagement.aspx";
    //})
    $("#print_table").hide();
    $('#a4t').hide();
    //打印
    //$("#zhen").click(function () {
    //    //$("#content").jqprint();
    //    $.ajax({
    //        type: 'Post',
    //        url: 'searchSalesDetail.aspx',
    //        data: {
    //            op: 'print'
    //        },
    //        dataType: 'text',
    //        beforeSend: function (XMLHttpRequest) { //开始请求
    //            swal({
    //                text: "正在获取数据",
    //                imageUrl: "../imgs/load.gif",
    //                imageHeight: 100,
    //                imageWidth: 100,
    //                width: 180,
    //                showConfirmButton: false,
    //                allowOutsideClick: false
    //            });
    //        },
    //        success: function (data) {
    //            $(".swal2-container").remove();
    //            $("#print_table tr:not(:first)").remove();
    //            $("#print_table").append(data);
    //            try {
    //                MyPreview();
    //            }
    //            catch(err) {
    //                window.location.href = "/CLodop_Setup_for_Win32NT.html";
    //            }
    //        },
    //        error: function (XMLHttpRequest, textStatus) { //请求失败
    //            $(".swal2-container").remove();
    //            if (textStatus == 'timeout') {
    //                var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
    //                xmlhttp.abort();
    //                swal({
    //                    title: "提示",
    //                    text: "请求超时",
    //                    type: "warning",
    //                    confirmButtonColor: '#3085d6',
    //                    confirmButtonText: '确定',
    //                    confirmButtonClass: 'btn btn-success',
    //                    buttonsStyling: false,
    //                    allowOutsideClick: false
    //                });
    //            } else if (textStatus == "error") {
    //                swal({
    //                    title: "提示",
    //                    text: "服务器内部错误",
    //                    type: "warning",
    //                    confirmButtonColor: '#3085d6',
    //                    confirmButtonText: '确定',
    //                    confirmButtonClass: 'btn btn-success',
    //                    buttonsStyling: false,
    //                    allowOutsideClick: false
    //                });
    //            }
    //        }
    //    })
    //})
})

$("#a4").click(function () {
    //var name = $("#region").val() + "   新华书店有限公司    ";
    $("#changeprint").attr("href", "../css/a4print.css");
    var name = $("#region").val();
    $.ajax({
        type: 'Post',
        url: 'searchSalesDetail.aspx',
        data: {
            op: 'print'
        },
        dataType: 'text',
        beforeSend: function (XMLHttpRequest) { //开始请求
            swal({
                text: "正在获取数据",
                imageUrl: "../imgs/load.gif",
                imageHeight: 100,
                imageWidth: 100,
                width: 280,
                showConfirmButton: true,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            $("#pname").text(name);
            $(".swal2-container").remove();
            $("#print_table tr:not(:first)").remove();
            $("#print_table").append(data);
            $('#print_table').show();
            $('#a4t').show();
            $("#a4t").jqprint();
            $('#a4t').hide();
        },
        error: function (XMLHttpRequest, textStatus) { //请求失败
            $(".swal2-container").remove();
            $('#a4t').hide();
            if (textStatus == 'timeout') {
                var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
                xmlhttp.abort();
                swal({
                    title: "提示",
                    text: "请求超时",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else if (textStatus == "error") {
                swal({
                    title: "提示",
                    text: "服务器内部错误",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            }
        }
    })
})
$("#zhen").click(function () {
    //var name = $("#region").val() + "   新华书店有限公司    ";
    $("#changeprint").attr("href", "../css/duolianprint.css");
    var name = $("#region").val();
    $.ajax({
        type: 'Post',
        url: 'searchSalesDetail.aspx',
        data: {
            op: 'print'
        },
        dataType: 'text',
        beforeSend: function (XMLHttpRequest) { //开始请求
            swal({
                text: "正在获取数据",
                imageUrl: "../imgs/load.gif",
                imageHeight: 100,
                imageWidth: 100,
                width: 280,
                showConfirmButton: true,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            $("#pname").text(name);
            $(".swal2-container").remove();
            $("#print_table tr:not(:first)").remove();
            $("#print_table").append(data);
            $('#print_table').show();
            //dayin();
            $('#a4t').show();
            $("#a4t").jqprint({importCSS:true});
            $('#a4t').hide();
        },
        error: function (XMLHttpRequest, textStatus) { //请求失败
            $(".swal2-container").remove();
            $('#a4t').hide();
            if (textStatus == 'timeout') {
                var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
                xmlhttp.abort();
                swal({
                    title: "提示",
                    text: "请求超时",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else if (textStatus == "error") {
                swal({
                    title: "提示",
                    text: "服务器内部错误",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            }
        }
    })
})

//导出
$("#export").click(function () {
    window.location.href = "searchSalesDetail.aspx?op=export";
})

function MyPreview() {
    var status = "";
    var LODOP = getLodop();
    //LODOP.SET_LICENSES("", "3C5743518A25D4EEFBB1CCB8C6FF9A49", "C94CEE276DB2187AE6B65D56B3FC2848", "");
    var link = "";
    var style = "";
    LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
    LODOP.On_Return = function (TaskID, Value) {
        status = Value;
    };
    if (status != "" || status != null) {
        //link = "<link rel='stylesheet' type='text/css' href='../css/zgz.css'><link rel='stylesheet' href='../css/material-dashboard.min.css'>";
        //style = "<style>body{background-color:white !important;}#table tr td{border: 1px solid black !important;padding:5px 5px;font-size:13px;}</style>";
        //LODOP.ADD_PRINT_HTM(0, 0, "100%", "100%", link + style + "<body>" + document.getElementById("content").innerHTML + "</body>");
        //LODOP.SET_PRINTER_INDEX("Send To OneNote 2016");
        //LODOP.SET_PRINT_PAGESIZE(3, "100%", "", "");
        //LODOP.PREVIEW();
        //window.location.reload();
        LODOP = getLodop();
        //LODOP.PRINT_INITA(0, 0, 577, 10000, "打印控件功能演示_Lodop功能_不同高度幅面");
        LODOP.ADD_PRINT_TEXT(30, 200, 600, 30, $("#region").val() + "销售单");
        LODOP.SET_PRINT_PAGESIZE(3, 2200, 0, "");
        LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        //------统计-----
        LODOP.ADD_PRINT_TEXT(80, 20, 200, 20, "单据编号：" + $("#XSnum").val());
        LODOP.ADD_PRINT_TEXT(80, 300, 200, 20, "客户名称：" + $("#customer").val());
        LODOP.ADD_PRINT_TEXT(80, 580, 200, 20, "操作员：" + $("#operator").val());
        LODOP.ADD_PRINT_TEXT(120, 20, 200, 20, "销售日期：" + $("#test2").val());
        LODOP.ADD_PRINT_TEXT(120, 300, 150, 20, "书籍种类：" + $("#bookKinds").val());
        LODOP.ADD_PRINT_TEXT(120, 580, 150, 20, "书本总数：" + $("#allBookCount").val());
        LODOP.ADD_PRINT_TEXT(160, 20, 150, 20, "总码洋：" + $("#alltotalprice").val());
        LODOP.ADD_PRINT_TEXT(160, 300, 150, 20, "总实洋：" + $("#allreadprice").val());
        //LODOP.ADD_PRINT_TEXT(160, 580, 150, 20, "：" + $("#allRealPrice").val());
        //划线
        //LODOP.ADD_PRINT_LINE(20, 14, 20, 730, 0, 1);
        //LODOP.ADD_PRINT_LINE(180, 14, 180, 730, 0, 1);

        //---------表格明细--------
        LODOP.ADD_PRINT_TEXT(210, 20, 50, 20, "序号");
        LODOP.ADD_PRINT_TEXT(210, 70, 100, 20, "商品编号");
        LODOP.ADD_PRINT_TEXT(210, 170, 120, 20, "书号");
        LODOP.ADD_PRINT_TEXT(210, 300, 300, 20, "商品名称");
        LODOP.ADD_PRINT_TEXT(210, 600, 50, 20, "单价");
        LODOP.ADD_PRINT_TEXT(210, 650, 50, 20, "数量");
        LODOP.ADD_PRINT_TEXT(210, 700, 60, 20, "折扣");
        LODOP.ADD_PRINT_TEXT(210, 760, 70, 20, "实洋");
        //表头表格
        LODOP.ADD_PRINT_LINE(200, 14, 200, 800, 0, 1);//一线(行)
        LODOP.ADD_PRINT_LINE(225, 14, 200, 14, 0, 1);//1
        LODOP.ADD_PRINT_LINE(225, 65, 200, 65, 0, 1);//2
        LODOP.ADD_PRINT_LINE(225, 165, 200, 165, 0, 1);//3
        LODOP.ADD_PRINT_LINE(225, 295, 200, 295, 0, 1);//4
        LODOP.ADD_PRINT_LINE(225, 595, 200, 595, 0, 1);//5
        LODOP.ADD_PRINT_LINE(225, 645, 200, 645, 0, 1);
        LODOP.ADD_PRINT_LINE(225, 695, 200, 695, 0, 1);//7
        LODOP.ADD_PRINT_LINE(225, 755, 200, 755, 0, 1);//8
        LODOP.ADD_PRINT_LINE(225, 800, 200, 800, 0, 1);//9
        LODOP.ADD_PRINT_LINE(225, 14, 225, 800, 0, 1);//二线(行)

        //--行内容
        var j = $("#print_table").find("tr").length;
        for (i = 0; i < j; i++) {
            var row = $("#print_table").find('tr').eq(i + 1).find('td');
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
            //LODOP.ADD_PRINT_TEXT(235 + 25 * i, 300, 300, 20, row.eq(3).text().trim());
            LODOP.ADD_PRINT_TEXT(235 + 25 * i, 600, 50, 20, row.eq(4).text().trim());
            LODOP.ADD_PRINT_TEXT(235 + 25 * i, 650, 50, 20, row.eq(5).text().trim());
            LODOP.ADD_PRINT_TEXT(235 + 25 * i, 700, 60, 20, row.eq(6).text().trim());
            LODOP.ADD_PRINT_TEXT(235 + 25 * i, 760, 60, 20, row.eq(7).text().trim());
            //--格子画线
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 14, 225 + 25 * i, 15, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 65, 225 + 25 * i, 65, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 165, 225 + 25 * i, 165, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 295, 225 + 25 * i, 295, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 595, 225 + 25 * i, 595, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 645, 225 + 25 * i, 645, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 695, 225 + 25 * i, 695, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 755, 225 + 25 * i, 755, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 800, 225 + 25 * i, 800, 0, 1);
            LODOP.ADD_PRINT_LINE(250 + 25 * i, 14, 250 + 25 * i, 800, 0, 1);

        }
        //------------end-------------
        LODOP.SET_SHOW_MODE("HIDE_PAPER_BOARD", true);
        LODOP.SET_PRINT_MODE("PRINT_PAGE_PERCENT", "Full-Width");
        LODOP.PREVIEW();//打印预览	
        //window.location.reload();
    }
}

function dayin() {
    var LODOP; //声明为全局变量 
    LODOP = getLodop();
    var name = $("#region").val();
    LODOP.PRINT_INIT(name);
    //var printStyleCss = "<link rel='stylesheet' type='text/css' href='../css/materialdesignicons.min.css'>";
    var strBodyStyle = "<style>#print_table input{ background:white}#pname{margin-left:-800px}#print_table{width:45%;}#print_table td{border: 1px;border-style: solid;border-color: #666666;} #print_table tr td:nth-child(1){font-size:15px;width:1%;}#print_table tr td:nth-child(2){font-size:15px;width:1%;}#print_table tr td:nth-child(3){font-size:15px;width:5%;}#print_table tr td:nth-child(4){font-size:15px;width:1%;}#print_table tr td:nth-child(5){font-size:15px;width:1%;}#print_table tr td:nth-child(7){font-size:15px;width:1%;}#print_table tr td:nth-child(6){font-size:15px;width:1%;}</style>";
    var strFormHtml = strBodyStyle + "<body>" + document.getElementById("a4t").innerHTML + "</body>";    LODOP.SET_PRINT_PAGESIZE(1, 240, 280, "多联");
    LODOP.ADD_PRINT_HTM(25, 20, 1500, 700, strFormHtml);


    LODOP.PREVIEW();
}
