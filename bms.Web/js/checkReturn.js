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
            url: 'checkReturn.aspx?op=logout',
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
                url: 'checkReturn.aspx',
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
});

//打印
$("#print").click(function () {
    //$("#content").jqprint();
    $.ajax({
        type: 'Post',
        url: 'checkReturn.aspx',
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
                link = "<link rel='stylesheet' type='text/css' href='../css/zgz.css'><link rel='stylesheet' href='../css/material-dashboard.min.css'>";
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
