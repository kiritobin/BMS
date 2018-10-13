$(document).ready(function () {
    //判断是否含有特殊字符
    function checkName(val) {
        var reg = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？]");
        var rs = "";
        for (var i = 0, l = val.length; i < val.length; i++) {
            rs = rs + val.substr(i, 1).replace(reg, '');
        }
        return rs;
    }
    $(".paging").pagination({
        pageCount: $("#intPageCount").val(),
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
                url: 'backManagement.aspx',
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
    })
    //查询
    $("#btn-search").click(function () {
        //var stockId = $("#bill").val();
        var sellId = checkName($("#region").val());
        var customer = checkName($("#customer").val());
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                //stockId: stockId,
                sellId: sellId,
                customer: customer,
                op: "paging"
            },
            datatype: 'text',
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
                        //var search = $("#btn-search").val().trim();
                        $.ajax({
                            type: 'Post',
                            url: 'backManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                //stockId: stockId,
                                sellId: sellId,
                                customer: customer,
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
            }
        });
    })
    //页面链接
    //查询按钮
    $("#table").delegate(".search_back", "click", function () {
        // window.location.href = 'backQuery.aspx'
        var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                sohId: id,
                op: "searchMonomer"
            },
            dataType: 'text',
            success: function (succ) {
                window.location.href = 'backQuery.aspx';
            }
        })
    })
    //添加单体
    $("#table").delegate(".btn_add", "click", function () {
        var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var state = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                sohId: id,
                state: state,
                op: "addMonomer"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "处理中") {
                    window.location.href = 'backQuery.aspx';
                } else {
                    swal({
                        title: "温馨提示",
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        //window, location.reload();
                    })
                }
            }
        })
    })
    //添加单头
    $("#btn-add").click(function () {
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                op: "add"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
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
                    swal({
                        title: "温馨提示",
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        //window, location.reload();
                    })
                }
            }
        })
    })
    //删除
    $("#table").delegate(".btndelete", "click", function () {
        var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                sohId: id,
                op: "delete"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    swal({
                        title: "温馨提示",
                        text: "删除成功",
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
                    swal({
                        title: "温馨提示",
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        //window, location.reload();
                    })
                }
            }
        })

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
            url: 'warehouseManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
