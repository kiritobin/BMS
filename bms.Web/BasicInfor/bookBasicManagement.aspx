<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookBasicManagement.aspx.cs" Inherits="bms.Web.BasicInfor.bookBasicManagement" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>图书综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 材料图标样式 -->
    <link rel="stylesheet" href="../css/materialdesignicons.css">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/demo.css">
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="wrapper ">
        <!-- 左侧垂直导航 -->
        <div class="sidebar" data-color="danger" data-background-color="white" data-image="../imgs/sidebar-2.jpg">
            <!--
                Tip 1: 需要改变导航条的颜色可以修改: data-color="purple | azure | green | orange | danger"
        
                Tip 2: 需要改变导航条的背景图片可以修改 data-image tag
            -->
            <!-- 平台字体logo -->
            <div class="logo">
                <a href="javascript:;" class="simple-text text-center logo-normal">图书综合管理平台</a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li class="nav-item">
                        <a class="nav-link" href="#securityManage" data-toggle="collapse">
                            <i class="material-icons">security</i>
                            <p>
                                权限管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="securityManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">功能管理</span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link" href="#userManage" data-toggle="collapse">
                            <i class="material-icons">person</i>
                            <p>
                                客户管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="userManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">客户信息管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="material-icons">book</i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="inventoryManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">出库</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">入库</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">退货</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#saleManage" data-toggle="collapse">
                            <i class="material-icons">library_books</i>
                            <p>
                                销售管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="saleManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">销售</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">销退</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item active">
                        <a class="nav-link" href="#baseManage" data-toggle="collapse">
                            <i class="material-icons">bubble_chart</i>
                            <p>
                                基础信息
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="baseManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">架位管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor foucsColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">书籍基础数据管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">组织管理</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="main-panel">
            <!-- 主界面头部面板 -->
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container-fluid">
                    <button class="navbar-toggler" type="button" data-toggle="collapse" aria-controls="navigation-index"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                    </button>
                    <!-- 导航栏 -->
                    <div class="collapse navbar-collapse justify-content-end">
                        <form class="navbar-form">
                        </form>
                        <ul class="navbar-nav">
                            <li class="nav-item dropdown">
                                <a class="nav-link" href="javascript:;" id="navbarDropdownMenuLink" data-toggle="dropdown"
                                    aria-haspopup="true" aria-expanded="false">
                                    <i class="material-icons">settings</i>
                                    <p class="d-lg-none d-md-block">
                                        更多设置
                                    </p>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" href="#">个人中心</a>
                                    <a class="dropdown-item" href="#">修改密码</a>
                                    <a class="dropdown-item" href="#">退出系统</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <!-- 主界面内容 -->
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title ">书籍基础数据管理</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <%-- 表格头部按钮功能组 --%>
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" placeholder="书名查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" placeholder="书号查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" placeholder="标识查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" placeholder="备注查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" placeholder="ISBN查询">
                                                <button class="btn btn-info btn-sm" id="btnISBNS"><i class="fa fa-search fa-lg"></i>查询</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp 添加</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center">
                                            <thead>
                                                <tr class="book-tab-tr">
                                                    <th>序号</th>
                                                    <th>书号</th>
                                                    <th>书名</th>
                                                    <th>作者</th>
                                                    <th>定价</th>
                                                    <th>出版日期</th>
                                                    <th>出版社</th>
                                                    <th>ISBN</th>
                                                    <th>编目</th>
                                                    <th>备注</th>
                                                    <th>标识</th>
                                                    <th>操作</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>7115438546</td>
                                                    <td>bootstrap 入门经典</td>
                                                    <td>Jennife Kyrnin</td>
                                                    <td>￥59.00</td>
                                                    <td>2016年12月第一版</td>
                                                    <td>人民邮电出版社</td>
                                                    <td>7115438546</td>
                                                    <td>B0000001</td>
                                                    <td>最后一本图书</td>
                                                    <td>Teach</td>
                                                    <td>
                                                        <button class="btn btn-danger">
                                                            <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </button>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <ul class="pagination">
                                                <li class="paginate_button page-item first" id="datatables_first"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="0" tabindex="0" class="page-link">首页</a></li>
                                                <li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="1" tabindex="0" class="page-link">上一页</a></li>
                                                <li class="paginate_button page-item active"><a href="#" aria-controls="datatables" data-dt-idx="3"
                                                    tabindex="0" class="page-link">2</a></li>
                                                <!--类名active表示当前页 -->
                                                <li class="paginate_button page-item"><a href="#" aria-controls="datatables" data-dt-idx="4"
                                                    tabindex="0" class="page-link">3</a></li>
                                                <li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="6" tabindex="0" class="page-link">下一页</a></li>
                                                <li class="paginate_button page-item last" id="datatables_last"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="7" tabindex="0" class="page-link">尾页</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--添加书籍模态框-->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">添加书籍</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="material-icons">clear</i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <table class="table model-table">
                                <tr>
                                    <td class="table-tr-td-bookName"><span class="model-tab-td-span">书名:</span></td>
                                    <td>
                                        <input type="text" value="" name="书名" class="col-sm-12 txtTitle txtVerify"></td>
                                    <td class="table-tr-td-bookAuoth"><span class="model-tab-td-span">作者:</span></td>
                                    <td>
                                        <input type="text" value="" name="作者" class="col-sm-12 txtAuthor txtVerify"></td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">定价:</span></td>
                                    <td>
                                        <input type="text" value="" name="定价" class="col-sm-12 txtPrice txtVerify"></td>
                                    <td><span class="model-tab-td-span">出版日期:</span></td>
                                    <td>
                                        <input type="text" value="" name="出版日期" class="col-sm-12 txtTime txtVerify"></td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">出版社:</span></td>
                                    <td>
                                        <input type="text" value="" name="出版社" class="col-sm-12 txtPress txtVerify"></td>
                                    <td><span class="model-tab-td-span">ISBN:</span></td>
                                    <td>
                                        <input type="text" value="" name="ISBN" class="col-sm-12 txtISBN txtVerify"></td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">编目:</span></td>
                                    <td>
                                        <input type="text" value="" name="编目" class="col-sm-12 txtCatalogue txtVerify"></td>
                                    <td><span class="model-tab-td-span">标识:</span></td>
                                    <td>
                                        <input type="text" value="" name="标识" class="col-sm-12 txtId txtVerify"></td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">备注:</span></td>
                                    <td>
                                        <input type="text" value="" class="col-sm-12 remarks"></td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="model-btnclose1">关闭</button>
                            <button type="submit" class="btn btn-success btn-sm" id="btnAdd">添加</button>
                        </div>
                    </div>
                </div>
            </div>
            <!--查看书籍模态框-->
            <div class="modal fade" id="myModa2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="width: 700px; max-width: 800px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabe2">查看书籍信信息</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="material-icons">clear</i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered model-table">
                                <tr>
                                    <td><span class="model-tab-td-span">书号:</span></td>
                                    <td>711543854600000001</td>
                                    <td class="table-tr-td-bookName"><span class="model-tab-td-span">书名:</span></td>
                                    <td>bootstrap 入门经典</td>
                                </tr>
                                <tr>
                                    <td class="table-tr-td-bookAuoth"><span class="model-tab-td-span">作者:</span></td>
                                    <td>Jennife Kyrnin</td>
                                    <td><span class="model-tab-td-span">定价:</span></td>
                                    <td>59.00￥</td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">出版日期:</span></td>
                                    <td>2016年12月第一版</td>
                                    <td><span class="model-tab-td-span">出版社:</span></td>
                                    <td>人民邮电出版社</td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">ISBN:</span></td>
                                    <td>7115438546</td>
                                    <td><span class="model-tab-td-span">编目:</span></td>
                                    <td>编目</td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">标识:</span></td>
                                    <td>标识9</td>
                                    <td><span class="model-tab-td-span">备注:</span></td>
                                    <td>web前端框架</td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>
            <!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <!-- 版权内容 -->
                    <div class="copyright text-center">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                        , made with <i class="material-icons">favorite</i> by
                        <a href="javascript:;" target="_blank"></a>for a better web.
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="../js/jquery-3.3.1.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/demo.js"></script>
    <script>
        $(function () {
            $(".txtVerify").focus(function () {
                if ($(this).val().length == 0) {
                    $(this).css("border-color", "#ddd");
                }
            });
            $(".txtVerify").blur(function () {
                if ($(this).val().length == 0) {
                    $(this).css("border-color", "red");
                }
            });
            //提交按钮单机非空验证
            $("#btnAdd").click(function () {
                //$(".txtVerify").each(function () {
                //    var val = $(this).val().trim();
                //    if (val == "") {
                //        var name = $(this).attr("name");
                //        alert("您的" + name + "信息为空，请确认后再次提交");
                //        $(this).focus();
                //        return false;
                //    }
                //    else {
                //        alert("提交成功");
                //    }
                //});
                if ($(".txtTitle").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "书名不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtAuthor").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "作者不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtPrice").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "价格不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtTime").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "出版时间不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtPress").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "出版社不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtISBN").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "ISBN不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtCatalogue").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "编目不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else if ($(".txtId").val() == "") {
                    swal({
                        title: "温馨提示:)",
                        text: "标识不能为空，请确认后再次提交!",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                    $(this).focus();
                }
                else {
                    swal({
                        title: "温馨提示:)",
                        text: "数据添加成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success"
                    }).catch(swal.noop)
                }
            });

        });
    </script>
</body>

</html>
