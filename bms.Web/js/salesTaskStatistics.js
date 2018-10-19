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
})

//打印
$("#print").click(function () {
    //$("#content").jqprint();

    $.ajax({
        type: 'Post',
        url: 'salesTaskStatistics.aspx',
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
                link = "<link rel='stylesheet' type='text/css' href='../css/zgz.css'><link rel='stylesheet' href='../css/material-dashboard.min.css'><link rel='stylesheet' href='../css/lgd.css'>";
                style = "<style>body{background-color:white !important;}#table tr td{border: 1px solid black !important;padding:5px 5px;font-size:13px;}</style>";
                LODOP.ADD_PRINT_HTM(0, 0, "100%", "100%", link + style + "<body>" + document.getElementById("content").innerHTML + "</body>");
                //LODOP.SET_PRINTER_INDEX("Send To OneNote 2016");
                LODOP.SET_PRINT_PAGESIZE(3, "100%", "", "");
                LODOP.PREVIEW();
                window.location.reload();
            }
        }
    })
})