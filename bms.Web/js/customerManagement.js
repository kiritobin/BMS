var curPage = $("#curPage").val();
var countPage = $("#pageCount").val();
sessionStorage.setItem("curPage", curPage);
sessionStorage.setItem("totalPage", countPage);
$(document).ready(function () {
    $('.paging').pagination({
        pageCount: $("#countPage").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var region = $("#select-region").find("option:selected").val();
            var search = $("#btn-search").val().trim();
            $.ajax({
                type: 'Post',
                url: 'customerManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    region: region,
                    search: search,
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

    //点击查询按钮时
    $("#btn-search").click(function () {
        var region = $("#select-region").find("option:selected").val();
        var search = $("#search_All").val().trim();
        $.ajax({
            type: 'Post',
            url: 'customerManagement.aspx',
            data: {
                region: region,
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
            }
        });
    });

    $("#select-region").change(function () {
        var region = $("#select-region").val();
        var search = $("#search_All").val().trim();
        alert(search);
        $.ajax({
            type: 'Post',
            url: 'customerManagement.aspx',
            data: {
                region: region,
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
            }
        });
    })

    //$("#select-region").change(function () {
    //    var regionId = $("#select-region").find("option:selected").val();
    //    sessionStorage.setItem("region", regionId);
    //    if (sessionStorage.getItem("strWhere") != null) {
    //        sessionStorage.removeItem("strWhere");
    //    }
    //    jump(1);
    //})
    //按钮查询
    //$("#btn-search").click(function () {
    //    var str = $("#search_All").val();
    //    sessionStorage.setItem("strWhere", str);
    //    jump(1);
    //})

    ////地址栏传值
    //function jump(curr) {
    //    window.location.href = "customerManagement.aspx?currentPage=" + curr;
    //}

    //添加客户
    $("#btnAdd").click(function () {
        var id = $("#customerId").val();
        var name = $("#customerName").val();
        var regionID = $("#model-select-region").find("option:selected").val();
        if (id == "" || name == "" || regionID == "") {
            alert("账号、姓名和地区名称都不能为空！");
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'customerManagement.aspx',
                data: {
                    customerId: id,
                    cutomerName: name,
                    zoneId: regionID,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: succ,
                            text: succ,
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
                            title: succ,
                            text: succ,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    }
                }
            })
        }
    })
    //编辑客户
    $(".btn_Editor").click(function () {
        var custId = $(this).parent().prev().prev().prev().prev().text().trim();
        var custName = $(this).parent().prev().prev().prev().text().trim();
        $(".editor_name").val(custName);
        var custRegion = $(this).parent().prev().prev().text().trim();
        $(".editor_id").text(custId);
    })
    //提交编辑
    $(".sava_Editor").click(function () {
        var custId = $(".editor_id").text();
        var custName = $(".editor_name").val();
        var regId = $(".editor_region").find("option:selected").val();
        $.ajax({
            type: 'Post',
            url: 'customerManagement.aspx',
            data: {
                customerid: custId,
                customername: custName,
                regionid: regId,
                op: "editData"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "更新成功") {
                    swal({
                        title: succ,
                        text: succ,
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
                        title: succ,
                        text: succ,
                        type: "success",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window, location.reload();
                    })
                }
            }
        });
    })
    //删除
    $(".btn_delete").click(function () {
        var custId = $(this).parent().prev().prev().prev().prev().text().trim();
        //弹窗
        swal({
            title: "是否删除？",
            text: "删除了将无法恢复！！！",
            type: "question",
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
                url: 'customerManagement.aspx',
                data: {
                    cutomerId:custId,
                    op: "del"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "删除成功") {
                        swal({
                            title: succ,
                            text: succ,
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
                            title: succ,
                            text: succ,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    }
                }
            })
        })
    })
    //判断当删除最后一页最后一条信息时，当前也自动跳到上一页
    //if (parseInt(sessionStorage.getItem("curPage")) > parseInt(sessionStorage.getItem("totalPage"))) {
    //    {
    //        jump(parseInt(sessionStorage.getItem("curPage")) - 1);
    //    }
    //}
    //重置密码
    $(".reset_pwd").click(function () {
        var custId = $(this).parent().prev().prev().prev().prev().text().trim();
        swal({
            title: "是否重置？",
            text: "重置后将无法恢复！！！",
            type: "question",
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
                url: 'customerManagement.aspx',
                data: {
                    customerid: custId,
                    op: "reset"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "重置成功") {
                        swal({
                            title: succ,
                            text: succ,
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
                            title: succ,
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    }
                }
            })
        })
    })
})