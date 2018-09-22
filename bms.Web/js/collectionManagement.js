$(document).ready(function () {
    //点击查询按钮时
    $("#btn-search").click(function () {
        var book = $("#bookSearch").val().trim();
        var isbn = $("#isbnSearch").val().trim();
        $.ajax({
            type: 'Post',
            url: 'collectionManagement.aspx',
            data: {
                book: book,
                isbn: isbn,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
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
                        $.ajax({
                            type: 'Post',
                            url: 'collectionManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                book: book,
                                isbn: isbn,
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
    });

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
            var book = $("#bookSearch").val().trim();
            var isbn = $("#isbnSearch").val().trim();
            $.ajax({
                type: 'Post',
                url: 'collectionManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    book: book,
                    isbn: isbn,
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

    $("#btnImport").click(function () {
        var custom = $("#model-select-custom").val();
        var file = $("#file").val();
        if (custom == "" || custom == null) {
            alert("请选择客户");
        }
        else if (file == "" || file == null) {
            alert("请上传文件");
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'collectionManagement.aspx',
                data: {
                    custom:custom,
                    action: "import"
                },
                dataType: 'text',
                success: function (data) {
                    alert(data);
                    //if (data.indexOf("导入成功") >= 0) {
                    //    alert("导入成功");
                    //    location.reload();
                    //} else if (data.indexOf("导入失败") >= 0) {
                    //    alert("导入失败");
                    //}
                    //else {
                    //    alert(data);
                    //}
                }
            });
        }
    });

    $("#upload").click(function () {
        var location = $("input[name='file']").val();
        var point = location.lastIndexOf(".");
        var type = location.substr(point).toLowerCase();
        var uploadFiles = document.getElementById("file").files;
        if (uploadFiles.length == 0) {
            alert("请选择要上传的文件");
        }
        else if (type == ".xls") {
            ajaxFileUpload();
        }
        else {
            alert("只允许上传.xls格式的文件");
        }
    });

    function ajaxFileUpload() {
        $.ajaxFileUpload(
            {
                url: '/CustomerMGT/uploadCollection.aspx', //用于文件上传的服务器端请求地址
                secureuri: false, //是否需要安全协议，一般设置为false
                fileElementId: 'file', //文件上传域的ID
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status)  //服务器成功响应处理函数
                {
                    console.log(data.msg);
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                            alert(data.error);
                        } else {
                            alert(data.msg);
                        }
                    }
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    alert(e);
                }
            }
        );
        return false;
    }
});