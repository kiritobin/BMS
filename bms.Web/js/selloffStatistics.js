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
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部") {
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
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部") {
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
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部") {
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
        var name = $(this).parent().prev().prev().prev().prev().prev().text();
        window.location.href = "sellOffDetail.aspx?type=" + groupbyType + "&&name=" + name;
    })

    //点击查询按钮时
    $("#btn_search").click(function () {
        var groupby = $("#groupby").find("option:selected").text();
        var supplier = $("#supplier").find("option:selected").text();
        var regionName = $("#region").find("option:selected").text();
        var customerName = $("#customer").find("option:selected").text();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
            if (supplier == "全部") {
                supplier = "";
            }
            regionName = "";
            customerName = "";
        }
        else if (groupby == "组织") {
            groupbyType = "regionName";
            if (regionName == "全部") {
                regionName = "";
            }
            supplier = "";
            customerName = "";
        } else if (groupby == "客户") {
            if (customerName == "全部") {
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

            $("#groupcustom").hide();
        }
        else if (groupby == "组织") {
            $("#groupsupplier").hide();
            $("#groupregion").show();
            $("#groupcustom").hide();
        } else if (groupby == "客户") {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
            $("#groupcustom").show();
        } else {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
            $("#groupcustom").hide();
        }
    })
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
var group = $("#table").find('tr').eq(0).find('th').eq(1).text().trim();
var kinds = $("#table").find('tr').eq(0).find('th').eq(2).text().trim();
var num = $("#table").find('tr').eq(0).find('th').eq(3).text().trim();
var totalPrice = $("#table").find('tr').eq(0).find('th').eq(4).text().trim();
var realPrice = $("#table").find('tr').eq(0).find('th').eq(5).text().trim();
var LODOP; //声明为全局变量
function MyPreview() {
    AddTitle();
    var iCurLine = 75;//标题行之后的数据从位置80px开始打印
    var j = $("#table").find("tr").length;
    var row = $("#table").find('tr');
    for (i = 1; i < j; i++) {
        LODOP.ADD_PRINT_TEXT(iCurLine, 15, 50, 20, i);
        LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(1).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 270, 80, 20, row.eq(i).find('td').eq(2).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 330, 50, 20, row.eq(i).find('td').eq(3).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 380, 50, 20, row.eq(i).find('td').eq(4).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 430, 50, 20, row.eq(i).find('td').eq(5).text().trim());
        iCurLine = iCurLine + 25;//每行占25px
        LODOP.ADD_PRINT_LINE(iCurLine - 5, 14, iCurLine - 5, 480, 0, 1);//横线
        //竖线
        LODOP.ADD_PRINT_LINE(70, 14, 70 + 25 * i, 14, 0, 1);
        LODOP.ADD_PRINT_LINE(70, 65, 70 + 25 * i, 65, 0, 1);
        LODOP.ADD_PRINT_LINE(70, 265, 70 + 25 * i, 265, 0, 1);
        LODOP.ADD_PRINT_LINE(70, 325, 70 + 25 * i, 325, 0, 1);
        LODOP.ADD_PRINT_LINE(70, 375, 70 + 25 * i, 375, 0, 1);
        LODOP.ADD_PRINT_LINE(70, 425, 70 + 25 * i, 425, 0, 1);
        LODOP.ADD_PRINT_LINE(70, 480, 70 + 25 * i, 480, 0, 1);
    }
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 480, 0, 1);
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 14, 0, 1);
    //LODOP.ADD_PRINT_TEXT(iCurLine + 5, 20, 300, 20, "打印时间：" + (new Date()).toLocaleDateString() + " " + (new Date()).toLocaleTimeString());
    //LODOP.ADD_PRINT_TEXT(iCurLine + 5, 346, 150, 20, "合计金额：" + document.getElementById("HJ").value);
    LODOP.SET_PRINT_PAGESIZE(3, 2200, 100, "");//这里3表示纵向打印且纸高“按内容的高度”；1385表示纸宽138.5mm；45表示页底空白4.5mm
    LODOP.PREVIEW();
};
function AddTitle() {
    LODOP = getLodop();
    LODOP.PRINT_INIT("打印控件功能演示_Lodop功能_不同高度幅面");
    LODOP.ADD_PRINT_TEXT(15, 102, 355, 30, "北京市东城区沃乐福商城收款票据");
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 50, 20, "序号");
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 70, 200, 20, group);
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 270, 50, 20, kinds);
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 330, 50, 20, num);
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 380, 50, 20, totalPrice);
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 430, 50, 20, realPrice);
    LODOP.SET_PRINT_STYLEA(5, "FontSize", 8);
    LODOP.SET_PRINT_STYLEA(5, "Bold", 1);
    //横线
    LODOP.ADD_PRINT_LINE(45, 14, 45, 480, 0, 1);
    LODOP.ADD_PRINT_LINE(70, 14, 70, 480, 0, 1);
    //竖线
    LODOP.ADD_PRINT_LINE(45, 14, 70, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 65, 70, 65, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 265, 70, 265, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 325, 70, 325, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 375, 70, 375, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 425, 70, 425, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 480, 70, 480, 0, 1);
};