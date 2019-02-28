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

//地址栏获取
//console.log(getUrlParam(location.href,"参数名"));
function getUrlParam(url, name) {
    var pattern = new RegExp("[?&]" + name + "\=([^&]+)", "g");
    var matcher = pattern.exec(url);
    var items = null;
    if (null != matcher) {
        try {
            items = decodeURIComponent(decodeURIComponent(matcher[1]));
        } catch (e) {
            try {
                items = decodeURIComponent(matcher[1]);
            } catch (e) {
                items = matcher[1];
            }
        }
    }
    return items;
}  

$(document).ready(function () {
    var region = getUrlParam(location.href, "region");
    var singleHeadId = getUrlParam(location.href, "singleHeadId");
    
    //出入退
    var type = getUrlParam(location.href, "type");
    if (type == "RK") {
        $("#tjType").html("入&nbsp;库&nbsp;统&nbsp;计");
        $("#diff").text("来源组织");
        $('#resource').attr('title', "请输入来源组织");
        $("#change").text("请选择来源组织");
        $("button[data-id='resource']>.filter-option-inner-inner").text("请选择来源组织");
        $("#rkgl").text("入库管理");
        $("#rktj").text("入库统计");
        $('#rkgl').attr('href', 'stockManagement.aspx'); 
    }
    else if (type == "CK") {
        $("#tjType").html("出&nbsp;库&nbsp;统&nbsp;计");
        $("#diff").text("收货组织");
        $('#resource').attr('title', "请输入收货组织");
        $("#change").text("请选择收货组织");
        $("button[data-id='resource']>.filter-option-inner-inner").html("请选择收货组织");
        $("#rkgl").text("出库管理");
        $("#rktj").text("出库统计");
        $('#rkgl').attr('href', 'warehouseManagement.aspx'); 
    }
    else if (type == "TH") {
        $("#tjType").html("退&nbsp;货&nbsp;统&nbsp;计");
        $("#diff").text("收货组织");
        $("#change").text("请选择收货组织");
        $('#resource').attr('title', "请输入收货组织");
        $("button[data-id='resource']>.filter-option-inner-inner").text("请选择收货组织");
        $("#rkgl").text("退货管理");
        $("#rktj").text("退货统计");
        $('#rkgl').attr('href', 'returnManagement.aspx'); 
    }

    var bookIsbn = $("#bookIsbn").val();
    var bookName = $("#bookName").val();
    var supplier = $("#supplier").val();
    var time = $("#time").val();
    var userName = $("#userName").val();
    //var region = $("#region").val();
    var resource = $("#resource").val();

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
            var bookIsbn = $("#bookIsbn").val();
            var bookName = $("#bookName").val();
            var supplier = $("#supplier").val();
            var time = $("#time").val();
            var userName = $("#userName").val();
            //var region = $("#region").val();
            var resource = $("#resource").val();
            if (supplier == "请选择供应商") {
                supplier = "";
            }
            if (region == "请选择组织") {
                region = "";
            }
            if (userName == "请选择制单员") {
                userName = "";
            }
            if (resource == "请选择来源组织" || "请选择收货组织") {
                resource = "";
            }
            $.ajax({
                type: 'Post',
                url: 'inventoryStatistics.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    type: type,
                    bookIsbn: bookIsbn,
                    bookName: bookName,
                    supplier: supplier,
                    time: time,
                    userName: userName,
                    region: region,
                    resource: resource,
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
        $("#timeModal").modal('hide');
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
            $("#timeModal").modal('hide');
        }
    })

    $("#btn_search").click(function () {
        var bookIsbn = $("#bookIsbn").val();
        var bookName = $("#bookName").val();
        var supplier = $("#supplier").find("option:selected").text();
        var time = $("#time").val();
        var userName = $("#userName").find("option:selected").text();
        //var region = $("#region").find("option:selected").text();
        var resource = $("#resource").find("option:selected").text();
        if (supplier == "请选择供应商") {
            supplier = "";
        }
        if (region == "请选择组织") {
            region = "";
        }
        if (userName == "请选择制单员") {
            userName = "";
        }
        if (resource == "请选择来源组织" || "请选择收货组织") {
            resource = "";
        }
        $.ajax({
            type: 'Post',
            url: 'inventoryStatistics.aspx',
            data: {
                type: type,
                bookIsbn: bookIsbn,
                bookName: bookName,
                supplier: supplier,
                time: time,
                userName: userName,
                region: region,
                resource: resource,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                var datas = data.split("|:");
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(datas[0]); //加载table
                $("#sjNum").val(datas[1]);
                $("#sbNum").val(datas[2]);
                $("#total").val(datas[3]);
                $("#real").val(datas[4]);
                $("#intPageCount").remove();
                $(".paging").empty();
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
                        var bookIsbn = $("#bookIsbn").val();
                        var bookName = $("#bookName").val();
                        var supplier = $("#supplier").val();
                        var time = $("#time").val();
                        var userName = $("#userName").val();
                        //var region = $("#region").val();
                        var resource = $("#resource").val();
                        $.ajax({
                            type: 'Post',
                            url: 'inventoryStatistics.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                type: type,
                                bookIsbn: bookIsbn,
                                bookName: bookName,
                                supplier: supplier,
                                time: time,
                                userName: userName,
                                region: region,
                                resource: resource,
                                op: "paging"
                            },
                            dataType: 'text',
                            success: function (data) {
                                var datas = data.split("|:");
                                $("#table tr:not(:first)").remove(); //清空table处首行
                                $("#table").append(datas[0]); //加载table
                                $("#sjNum").val(datas[1]);
                                $("#sbNum").val(datas[2]);
                                $("#total").val(datas[3]);
                                $("#real").val(datas[4]);
                                $("#intPageCount").remove();
                            }
                        });
                    }
                });
            }
        });
    });
});