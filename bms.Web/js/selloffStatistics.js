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

window.onload = function () {
    $("#groupsupplier").hide();
    $("#groupregion").hide();
    $("#groupcustom").hide();
}

$(document).ready(function () {
    $("#printContent").hide();//隐藏打印内容
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
            var groupby = $("#groupby").find("option:selected").text();
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var customerName = $("#customer").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部客户") {
                    customerName = "";
                }
                groupbyType = "customerName";
                supplier = "";
                regionName = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
                customerName = "";
            }
            var time = $("#time").val();
            var page = api.getCurrent();
            $.ajax({
                type: 'Post',
                url: 'selloffStatistics.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time: time,
                    customerName: customerName,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                    }
                    else if (groupby == "组织") {
                        $("#showType").text("组织");
                    } else if (groupby == "客户") {
                        $("#showType").text("客户");
                    } else {
                        $("#showType").text("客户");
                    }
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
                }
            });
        }
    });
    $(".paging").hide();
    //导出报表
    $("#exportAll").click(function () {
        if (!$("#table td:visible").length) {
            swal({
                title: "无查询条件或无数据",
                text: "若以选择条件请先点击查询再导出",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            var groupby = $("#groupby").find("option:selected").text();
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var customerName = $("#customer").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部客户") {
                    customerName = "";
                }
                groupbyType = "customerName";
                supplier = "";
                regionName = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
                customerName = "";
            }
            if (groupbyType == "state") {
                swal({
                    title: "提示",
                    text: "请选择分组方式",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else {
                var time = $("#time").val();
                window.location.href = "selloffStatistics.aspx?op=exportAll&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&time=" + time + "&&customerName=" + customerName;
            }
        }
    })
    //导出报表明细
    $("#exportDe").click(function () {
        if (!$("#table td:visible").length) {
            swal({
                title: "无查询条件或无数据",
                text: "若以选择条件请先点击查询再导出",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            var groupby = $("#groupby").find("option:selected").text();
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var customerName = $("#customer").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部客户") {
                    customerName = "";
                }
                groupbyType = "customerName";
                supplier = "";
                regionName = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
                customerName = "";
            }
            if (groupbyType == "state") {
                swal({
                    title: "提示",
                    text: "请选择分组方式",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else {
                var time = $("#time").val();
                window.location.href = "selloffStatistics.aspx?op=exportDe&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&time=" + time + "&&customerName=" + customerName;
            }
        }
    })
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

    //查看详情
    $("#table").delegate(".look", "click", function (e) {
        var groupby = $("#groupby").find("option:selected").text();
        var looktime = $("#time").val();
        var regionId = $(this).prev().val();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
        }
        else if (groupby == "组织") {
            groupbyType = "regionName";
        } else if (groupby == "客户") {
            groupbyType = "customerName";
        } else {
            groupbyType = "supplier";
        }
        var name = $(this).parent().prev().prev().prev().prev().prev().text().trim();
        window.location.href = "sellOffDetail.aspx?type=" + groupbyType + "&&name=" + name + "&&looktime=" + looktime + "&&regionId=" + regionId;
        //if (name == "" || name == null) {
        //    swal({
        //        title: "提示",
        //        text: groupbyType +"为空，请联系管理员查找原因",
        //        type: "warning",
        //        confirmButtonColor: '#3085d6',
        //        confirmButtonText: '确定',
        //        confirmButtonClass: 'btn btn-success',
        //        buttonsStyling: false,
        //        allowOutsideClick: false
        //    });
        //} else {
        //    window.location.href = "sellOffDetail.aspx?type=" + groupbyType + "&&name=" + name;
        //}
    })

    //点击查询按钮时
    $("#btn_search").click(function () {
        var groupby = $("#groupby").find("option:selected").text();
        var supplier = $("#supplier").find("option:selected").text();
        var regionName = $("#region").find("option:selected").text();
        var customerName = $("#customer").find("option:selected").text();
        var regionid = $("#regionid").find("option:selected").val();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
            if (supplier == "全部供应商") {
                supplier = "";
            }
            regionName = "";
            customerName = "";
        }
        else if (groupby == "组织") {
            regionid = "";
            groupbyType = "regionName";
            if (regionName == "全部组织") {
                regionName = "";
            }
            supplier = "";
            customerName = "";
        } else if (groupby == "客户") {
            if (customerName == "全部客户") {
                customerName = "";
            }
            groupbyType = "customerName";
            supplier = "";
            regionName = "";
        } else {
            groupbyType = "state";
            supplier = "";
            regionName = "";
            customerName = "";
        }
        if (groupbyType == "state") {
            swal({
                title: "提示",
                text: "请选择分组方式",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            var time = $("#time").val();
            $.ajax({
                type: 'Post',
                url: 'selloffStatistics.aspx',
                data: {
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time: time,
                    customerName: customerName,
                    regionid: regionid,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                        $("#showType").show();
                    }
                    else if (groupby == "组织") {
                        $("#showType").hide();
                    } else if (groupby == "客户") {
                        $("#showType").text("客户");
                        $("#showType").show();
                    } else {
                        $("#showType").text("客户");
                        $("#showType").show();
                    }
                    $("#intPageCount").remove();
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
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
                            $.ajax({
                                type: 'Post',
                                url: 'selloffStatistics.aspx',
                                data: {
                                    page: api.getCurrent(), //页码
                                    groupbyType: groupbyType,
                                    supplier: supplier,
                                    regionName: regionName,
                                    time: time,
                                    customerName: customerName,
                                    regionid: regionid,
                                    op: "paging"
                                },
                                dataType: 'text',
                                success: function (data) {
                                    if (groupby == "供应商") {
                                        $("#showType").text("供应商");
                                        $("#showType").show();
                                    }
                                    else if (groupby == "组织") {
                                        $("#showType").hide();
                                    } else if (groupby == "客户") {
                                        $("#showType").text("客户");
                                        $("#showType").show();
                                    } else {
                                        $("#showType").text("客户");
                                        $("#showType").show();
                                    }
                                    $("#table tr:not(:first)").remove(); //清空table处首行
                                    $("#table").append(data); //加载table
                                    $("#intPageCount").remove();
                                }
                            });
                        }
                    });
                }
            });
        }

    });
    //分组方式改变
    $("#groupby").change(function () {
        var groupby = $("#groupby").find("option:selected").text();
        if (groupby == "供应商") {
            $("#groupsupplier").show();
            $("#groupregion").hide();
            $('#groupregion').selectpicker('refresh');
            $("#div_regionid").show();
            $("#groupcustom").hide();
        }
        else if (groupby == "组织") {
            $("#div_regionid").hide();
            $("#groupsupplier").hide();
            $("#groupregion").show();
            $("#groupcustom").hide();
        } else if (groupby == "客户") {
            $("#div_regionid").show();
            $("#groupsupplier").hide();
            $("#groupregion").hide();
            $("#groupcustom").show();
        } else {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
            $("#groupcustom").hide();
        }
    })

    //打印
    $("#print").click(function () {
        var t = $("#table").find('tr').length;
        //alert(t);
        if (t <= 1) {
            swal({
                title: "提示",
                text: "请先查询你要打印的内容",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-warning',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        }
        else {
            $("#printmodel").modal("show");
        }
    })
})

$("#a4").click(function () {
    $("#changeprint").attr("href", "../css/a4print.css");
    var groupby = $("#groupby").find("option:selected").text();
    var t = $("#table").find('tr').length;
    if (t <= 1) {
        swal({
            title: "提示",
            text: "请先查询你要打印的内容",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-warning',
            buttonsStyling: false,
            allowOutsideClick: false
        });
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'selloffStatistics.aspx',
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
                $("#pname").html("<h3>销退统计</h3>");
                $(".swal2-container").remove();
                $("#printTabled tr:not(:first)").remove(); //清空table处首行
                $("#printTabled").append(data); //加载tab
                $('#printContent').show();
                if (groupby == "供应商") {
                    $("#printShowType").text("供应商");
                }
                else if (groupby == "组织") {
                    $("#printShowType").text("组织");
                } else if (groupby == "客户") {
                    $("#printShowType").text("客户");
                }
                $("#printContent").jqprint();
                $('#printContent').hide();

            },
            error: function (XMLHttpRequest, textStatus) { //请求失败
                $(".swal2-container").remove();
                $('#printTabled').hide();
                $('#printContent').hide();
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
    }
})
$("#zhen").click(function () {
    $("#changeprint").attr("href", "../css/duolianprint.css");
    var groupby = $("#groupby").find("option:selected").text();
    var t = $("#table").find('tr').length;
    if (t <= 1) {
        swal({
            title: "提示",
            text: "请先查询你要打印的内容",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-warning',
            buttonsStyling: false,
            allowOutsideClick: false
        });
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'selloffStatistics.aspx',
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
                $("#pname").html("<h3>销退统计</h3>");
                $(".swal2-container").remove();
                $("#printTabled tr:not(:first)").remove(); //清空table处首行
                $("#printTabled").append(data); //加载tab
                $('#printContent').show();
                if (groupby == "供应商") {
                    $("#printShowType").text("供应商");
                }
                else if (groupby == "组织") {
                    $("#printShowType").text("组织");
                } else if (groupby == "客户") {
                    $("#printShowType").text("客户");
                }
                $("#printContent").jqprint({ importCSS: true });
                $('#printContent').hide();

            },
            error: function (XMLHttpRequest, textStatus) { //请求失败
                $(".swal2-container").remove();
                $('#printTabled').hide();
                $('#printContent').hide();
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
    }
})
//$("#zhen").click(function () {
//    $.ajax({
//        type: 'Post',
//        url: 'selloffStatistics.aspx',
//        data: {
//            op: "print"
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
//            $("#print_table tr:not(:first)").remove(); //清空table处首行
//            $("#print_table").append(data); //加载table
//            try {
//                MyPreview();
//            }
//            catch{
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
            url: 'selloffStatistics.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//打印
//var LODOP; //声明为全局变量
//var group = $("#table").find('tr').eq(0).find('th').eq(1).text().trim();
//var kinds = $("#table").find('tr').eq(0).find('th').eq(2).text().trim();
//var num = $("#table").find('tr').eq(0).find('th').eq(3).text().trim();
//var totalPrice = $("#table").find('tr').eq(0).find('th').eq(4).text().trim();
//var realPrice = $("#table").find('tr').eq(0).find('th').eq(5).text().trim();
//function createdate() {
//    //------循环画线例子begin-----			
//    LODOP = getLodop();
//    LODOP.PRINT_INITA(0, 0, 577, 10000, "打印控件功能演示_Lodop功能_不同高度幅面");
//    LODOP.SET_PRINT_PAGESIZE(3, 1505, 45, "");
//    LODOP.ADD_PRINT_TEXT(8, 136, 275, 30, "销退统计报表打印");
//    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
//    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
//    LODOP.ADD_PRINT_TEXT(50, 15, 50, 20, "序号");
//    LODOP.ADD_PRINT_TEXT(50, 80, 200, 20, group);
//    LODOP.ADD_PRINT_TEXT(50, 289, 100, 20, kinds);
//    LODOP.ADD_PRINT_TEXT(50, 409, 100, 20, num);
//    LODOP.ADD_PRINT_TEXT(50, 530, 100, 20, totalPrice);
//    LODOP.ADD_PRINT_TEXT(50, 650, 100, 20, realPrice);
//    LODOP.ADD_PRINT_LINE(44, 14, 44, 720, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 14, 44, 14, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 75, 44, 75, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 270, 44, 270, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 400, 44, 400, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 512, 44, 512, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 620, 44, 620, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 720, 44, 720, 0, 1);
//    LODOP.ADD_PRINT_LINE(76, 14, 76, 720, 0, 1);
//    //--行内容
//    var j = $("#table").find("tr").length;
//    var row = $("#table").find('tr');
//    for (i = 0; i < j-1; i++) {
//        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 16, 81, 20, i+1);
//        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 90, 200, 20, row.eq(i+1).find('td').eq(1).text().trim());
//        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 289, 81, 20, row.eq(i + 1).find('td').eq(2).text().trim());
//        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 409, 81, 20, row.eq(i + 1).find('td').eq(3).text().trim());
//        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 520, 81, 20, row.eq(i + 1).find('td').eq(4).text().trim());
//        LODOP.ADD_PRINT_TEXT(81 + 25 * i, 650, 81, 20, row.eq(i + 1).find('td').eq(5).text().trim());
//        //--格子画线		
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 14, 76 + 25 * i, 15, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 75, 76 + 25 * i, 75, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 270, 76 + 25 * i, 271, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 400, 76 + 25 * i, 400, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 512, 76 + 25 * i, 512, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 620, 76 + 25 * i, 620, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 720, 76 + 25 * i, 720, 0, 1);
//        LODOP.ADD_PRINT_LINE(101 + 25 * i, 14, 101 + 25 * i, 720, 0, 1);
//    }
//    LODOP.ADD_PRINT_LINE(101 + 25 * j, 14, 102 + 25 * j, 510, 0, 1);
//    LODOP.ADD_PRINT_TEXT(105 + 25 * j, 20, 300, 20, "打印时间：‎2015‎-‎12‎-‎15 ‎12‎:‎19‎");
//    LODOP.ADD_PRINT_TEXT(105 + 25 * j, 346, 150, 20, "合计金额：" + 10 * j);
//    //------------end-------------
//};
//function mypreview() {
//    LODOP = getLodop();
//    createdate();
//    LODOP.PREVIEW();//打印预览	



var LODOP; //声明为全局变量
function MyPreview() {
    AddTitle();
    var iCurLine = 75;//标题行之后的数据从位置80px开始打印
    var j = $("#printTabled").find("tr").length;
    var row = $("#printTabled").find('tr');
    for (i = 1; i < j; i++) {
        LODOP.ADD_PRINT_TEXT(iCurLine, 15, 50, 20, i);
        if (row.eq(i).find('td').eq(1).text().trim().length > 12) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(1).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(1).text().trim());
        }
        LODOP.ADD_PRINT_TEXT(iCurLine, 270, 100, 20, row.eq(i).find('td').eq(2).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 370, 100, 20, row.eq(i).find('td').eq(3).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 470, 100, 20, row.eq(i).find('td').eq(4).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 570, 100, 20, row.eq(i).find('td').eq(5).text().trim());
        iCurLine = iCurLine + 25;//每行占25px
        LODOP.ADD_PRINT_LINE(iCurLine - 5, 14, iCurLine - 5, 670, 0, 1);//横线
        //竖线
        LODOP.ADD_PRINT_LINE(70, 14, 70 + 25 * i, 14, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 65, iCurLine - 30 + 25, 65, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 265, iCurLine - 30 + 25, 265, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 365, iCurLine - 30 + 25, 365, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 465, iCurLine - 30 + 25, 465, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 565, iCurLine - 30 + 25, 565, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 670, iCurLine - 30 + 25, 670, 0, 1);
    }
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 670, 0, 1);
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 14, 0, 1);
    LODOP.SET_PRINT_PAGESIZE(3, 2200, 100, "");//这里3表示纵向打印且纸高“按内容的高度”；1385表示纸宽138.5mm；45表示页底空白4.5mm
    LODOP.PREVIEW();
};
function AddTitle() {
    var group = $("#table").find('tr').eq(0).find('th').eq(1).text().trim();
    var kinds = $("#table").find('tr').eq(0).find('th').eq(2).text().trim();
    var num = $("#table").find('tr').eq(0).find('th').eq(3).text().trim();
    var totalPrice = $("#table").find('tr').eq(0).find('th').eq(4).text().trim();
    var realPrice = $("#table").find('tr').eq(0).find('th').eq(5).text().trim();
    LODOP = getLodop();

    LODOP.PRINT_INIT("销退统计");
    LODOP.SET_PRINT_PAGESIZE(3, 2200, 100, "");
    LODOP.ADD_PRINT_TEXT(5, 262, 126, 30, "销退统计");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 50, 20, "序号");
    LODOP.ADD_PRINT_TEXT(50, 70, 200, 20, group);
    LODOP.ADD_PRINT_TEXT(50, 270, 100, 20, kinds);
    LODOP.ADD_PRINT_TEXT(50, 370, 100, 20, num);
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
    LODOP.ADD_PRINT_TEXT(50, 470, 100, 20, totalPrice);
    LODOP.ADD_PRINT_TEXT(50, 570, 100, 20, realPrice);
    //横线
    LODOP.ADD_PRINT_LINE(45, 14, 45, 670, 0, 1);
    LODOP.ADD_PRINT_LINE(70, 14, 70, 670, 0, 1);
    //竖线
    LODOP.ADD_PRINT_LINE(45, 14, 70, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 65, 70, 65, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 265, 70, 265, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 365, 70, 365, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 465, 70, 465, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 565, 70, 565, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 670, 70, 670, 0, 1);
};