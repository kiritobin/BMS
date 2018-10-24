$(document).ready(function () {
    //sessionStorage.removeItem("save");
    //打印
    $("#print").click(function () {
        //$("#print_content").jqprint();
        var status = "";
        var LODOP = getLodop();
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
            ////LODOP.SET_PRINTER_INDEX("Send To OneNote 2016");
            //LODOP.SET_PRINT_PAGESIZE(3, 2000, "", "");
            //LODOP.PREVIEW();
            //window.location.reload();
            LODOP = getLodop();
            //LODOP.PRINT_INITA(0, 0, 577, 10000, "打印控件功能演示_Lodop功能_不同高度幅面");
            LODOP.ADD_PRINT_TEXT(30, 200, 600, 30, $("#XSRW").val() + "销退单");
            LODOP.SET_PRINT_PAGESIZE(3, 2200, 0, "");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            //------统计-----
            LODOP.ADD_PRINT_TEXT(80, 20, 200, 20, "单据编号：" + $("#XT").val());
            LODOP.ADD_PRINT_TEXT(80, 300, 200, 20, "制单时间：" + $("#makeTime").val());
            LODOP.ADD_PRINT_TEXT(80, 580, 200, 20, "品种数：" + $("#allKinds").val());
            LODOP.ADD_PRINT_TEXT(120, 20, 200, 20, "总数量：" + $("#allCount").val());
            LODOP.ADD_PRINT_TEXT(120, 300, 150, 20, "总码洋：" + $("#allTotalPrice").val());
            LODOP.ADD_PRINT_TEXT(120, 580, 150, 20, "总实洋：" + $("#allRealPrice").val());
            //LODOP.ADD_PRINT_TEXT(160, 580, 150, 20, "：" + $("#allRealPrice").val());
            //划线
            //LODOP.ADD_PRINT_LINE(20, 14, 20, 730, 0, 1);
            //LODOP.ADD_PRINT_LINE(180, 14, 180, 730, 0, 1);

            //---------表格明细--------
            LODOP.ADD_PRINT_TEXT(210, 20, 50, 20, "序号");
            LODOP.ADD_PRINT_TEXT(210, 70, 100, 20, "ISBN号");
            LODOP.ADD_PRINT_TEXT(210, 170, 120, 20, "书号");
            LODOP.ADD_PRINT_TEXT(210, 300, 300, 20, "书名");
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
            //LODOP.ADD_PRINT_LINE(225, 730, 200, 730, 0, 1);//10
            LODOP.ADD_PRINT_LINE(225, 14, 225, 800, 0, 1);//二线(行)

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
            LODOP.PREVIEW();//打印预览	
            //window.location.reload();

        }
    })
    //只允许输入数字
    $("#table").delegate("#inputISBN", "keyup", function (e) {
        $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).bind("paste", function () {  //CTR+V事件处理    
        $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).css("ime-mode", "disabled");
    $("#table").delegate("#inputCount", "keyup", function (e) {
        $(this).val($(this).val().replace(/[^\r\n\-0-9]/g, ''));//允许输入“-”运算符号
    }).bind("paste", function () {  //CTR+V事件处理    
        $(this).val($(this).val().replace(/[^\r\n\-0-9]/g, ''));
    }).css("ime-mode", "disabled");
    //回车事件
    $(".addsell").keypress(function (e) {
        if (e.keyCode == 13) {
            var ISBN = $("#isbn").val();
            var realDiscount = $("#realDiscount").val();
            var count = $("#count").val();
            if (ISBN == "" || realDiscount == "" || count == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else if (count == "0") {
                swal({
                    title: "温馨提示:)",
                    text: "数量不能未0!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: ISBN,
                        discount: realDiscount,
                        count: count,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功" || data == "更新成功") {
                            swal({
                                title: "温馨提示",
                                text: "添加成功",
                                type: "success",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window, location.reload();
                            })
                        } else {
                            //$("#tablebook th:not(:first)").empty(); //清空table处首行
                            $(".first").remove();
                            //$("#tablebook").empty();
                            $("#table").append(data); //加载table
                        }
                    }
                });
            }
        }
    })
    //销退单体添加
    $("#add_back").click(function () {
        //var discount = $("#sellId").val();
        //$("#realDiscount").val(discount);
        $("#myModa2").modal("show");
    })
    //返回事件
    $("#toBack").click(function () {
        window.location.href = "backManagement.aspx";
    });
    //保存单据
    $("#sure").click(function () {
        swal({
            title: "温馨提示:)",
            text: "保存单据后将无法再进行修改",
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
                type: 'Post',
                url: 'backQuery.aspx',
                data: {
                    op: "sure"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "更新成功") {
                        swal({
                            title: "提示",
                            text: "保存成功",
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            //sessionStorage.setItem("save","succ");
                            window.location.reload();
                        })
                    } else {
                        swal({
                            title: "提示",
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-danger',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                }
            })
        })
    })
    //ISBN输入框回车事件，查询到信息后返回页面进行显示
    $("#table").delegate("#inputISBN", "keypress", function (e) {
        if (e.keyCode == 13) {
            var isbn = $('table input:eq(0)').val();
            if (isbn == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: isbn,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "暂无此数据") {
                            swal({
                                title: "温馨提示",
                                text: data,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-warning',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                $("#table").find('tr').eq(1).find('td').eq(1).find('input').focus();
                            })
                        }
                        else if (data.indexOf("much") > 0) {
                            $("#tablebook").empty();
                            $("#myModa2").modal("show");
                            $("#tablebook").append(data); //加载table
                        }
                        else {
                            $("#table").empty();
                            $("#table").append(data); //加载table
                            $("#table tr:first").find("td").eq(5).children().focus();
                            $("#table").find('tr').eq(1).find('td').eq(4).find('input').focus();
                        }
                    }
                });
            }
        }
    })
    //一号多书时弹出模态框，确定书籍
    $("#sureBook").click(function () {
        var isbn = $("input[type='radio']:checked").parents('tr').find('td').eq(1).text();
        var bookNum = $("input[type='radio']:checked").val();
        var price = $("input[type='radio']:checked").parents('tr').find('td').eq(4).text();
        var bookName = $("input[type='radio']:checked").parents('tr').find('td').eq(3).text();
        $.ajax({
            type: 'Post',
            url: 'backQuery.aspx',
            data: {
                ISBN: isbn,
                bookNO: bookNum,
                price: price,
                bookName:bookName,
                op: "search"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "销售单据中无此数据") {
                    swal({
                        title: "温馨提示",
                        text: data,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-warning',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        $("#myModa2").modal("show");
                    })
                }
                else {
                    $("#myModa2").modal("hide");
                    $("#table").empty();
                    $("#table").append(data); //加载table
                    //$("#table tr:first").find("td").eq(5).children().focus();
                    $("#table").find('tr').eq(1).find('td').eq(5).find('input').focus();
                }
            }
        })
    })
    //添加数量输入框回车事件
    $("#table").delegate("#inputCount", "keypress", function (e) {
        if (e.keyCode == 13) {
            var isbn = $("#table").find('tr').eq(1).find('td').eq(1).find('input').val();
            var num = $("#table").find('tr').eq(1).find('td').eq(5).find('input').val();
            var bookNO = $("#table").find('tr').eq(1).find('td').eq(2).text();
            if (num == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else if (num == "0") {
                swal({
                    title: "温馨提示:)",
                    text: "数量不能未0!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: isbn,
                        count: num,
                        bookNum: bookNO,
                        op: "add"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功") {
                            swal({
                                title: "温馨提示",
                                text: data,
                                type: "success",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window.location.reload();
                            })
                        }
                        else {
                            swal({
                                title: "温馨提示",
                                text: data,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-warning',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                $("#table").find('tr').eq(1).find('td').eq(4).find('input').focus();
                            })
                        }
                    }
                });
            }
        }
    })

})

//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外
function banBackSpace(e) {
    var ev = e || window.event;//获取event对象
    var obj = ev.target || ev.srcElement;//获取事件源
    var t = obj.type || obj.getAttribute('type');//获取事件源类型
    //获取作为判断条件的事件类型
    var vReadOnly = obj.getAttribute('readonly');
    //处理null值情况
    vReadOnly = (vReadOnly == "") ? false : vReadOnly;
    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
    //并且readonly属性为true或enabled属性为false的，则退格键失效
    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea")
                && vReadOnly == "readonly") ? true : false;
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea")
                ? true : false;

    //判断
    if (flag2) {
        return false;
    }
    if (flag1) {
        return false;
    }
}

window.onload = function () {
    //禁止后退键 作用于Firefox、Opera
    document.onkeypress = banBackSpace;
    //禁止后退键  作用于IE、Chrome
    document.onkeydown = banBackSpace;
}