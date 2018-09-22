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
    <link rel="stylesheet" href="../css/pagination.css" />
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/userManagement.aspx">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/roleManagement.aspx">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <%--<li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/jurisdictionManagement.aspx">
                                        <span class="sidebar-normal">功能管理</span>
                                    </a>
                                </li>--%>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/organizationalManagement.aspx">
                                        <span class="sidebar-normal">组织管理</span>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/customerManagement.aspx">
                                        <span class="sidebar-normal">客户信息管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/collectionManagement.aspx">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="material-icons">book</i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="inventoryManage">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="warehouseManagement.aspx">
                                        <span class="sidebar-normal">出库管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="stockManagement.aspx">
                                        <span class="sidebar-normal">入库管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="returnManagement.aspx">
                                        <span class="sidebar-normal">退货管理</span>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="tradeManagement.aspx">
                                        <span class="sidebar-normal">营销管理</span>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookshelfManagement.aspx">
                                        <span class="sidebar-normal">货架管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookBasicManagement.aspx">
                                        <span class="sidebar-normal">书籍基础数据管理</span>
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
                                                <input type="text" value="" class="search" id="bookName" placeholder="书名查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" id="bookNum" placeholder="书号查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="search" id="bookISBN" placeholder="ISBN查询">
                                                <button class="btn btn-info btn-sm" id="btn-search"><i class="fa fa-search fa-lg"></i>查询</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp 添加</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr class="book-tab-tr">
                                                    <th>序号</th>
                                                    <th>书号</th>
                                                    <th>书名</th>
                                                    <th>作者</th>
                                                    <th>定价</th>
                                                    <th>出版日期</th>
                                                    <th>供应商</th>
                                                    <th>ISBN</th>
                                                    <th>编目</th>
                                                    <th>备注</th>
                                                    <th>标识</th>
                                                    <th>操作</th>
                                                </tr>
                                            </thead>
                                            <%=getData() %>
                                        </table>
                                    </div>
                                    <div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <div class="m-style paging"></div>
                                            <%--分页栏--%>
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
                <div class="modal-dialog" style="min-width: 1000px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">基础数据导入导入</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="material-icons">clear</i>
                            </button>
                        </div>
                        <div class="modal-body" style="max-height: 500px; overflow: auto;">
                            <table class="table text-center model-table">
                                <tr>
                                    <td>
                                        <button class="btn btn-success" id="downEx">下载模板</button>
                                        <span class="btn btn-success fileinput-button">
                                            <span>选择文件</span>
                                            <input type="file" >
                                        </span>
                                        <button class="btn btn-success" id="upload">上传</button>
                                        <button type="submit" class="btn btn-success" id="btn_import">导入</button>
                                    </td>
                                </tr>
                            </table>
                            <div style="">
                                <table class="table mostTable table-bordered" id="bookBasicModal_table">
                                    <thead>
                                        <tr>
                                            <td colspan="11" class="text-center">
                                                <h4>重复数据</h4>
                                            </td>
                                        </tr>
                                        <tr class="book-tab-tr">
                                            <th>
                                                <div class="form-check">
                                                    <label class="form-check-label">
                                                        <input class="form-check-input" type="checkbox" value="" />
                                                        <span class="form-check-sign">
                                                            <span class="check functionCheck"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </th>
                                            <th>书号</th>
                                            <th>书名</th>
                                            <th>作者</th>
                                            <th>定价</th>
                                            <th>出版日期</th>
                                            <th>供应商</th>
                                            <th>ISBN</th>
                                            <th>编目</th>
                                            <th>备注</th>
                                            <th>标识</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="book-tab-tr">
                                            <td>
                                                <div class="form-check">
                                                    <label class="form-check-label">
                                                        <input class="form-check-input" type="checkbox" value="" />
                                                        <span class="form-check-sign">
                                                            <span class="check functionCheck"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>10000000000006</td>
                                            <td>java</td>
                                            <td>aouther</td>
                                            <td>65￥</td>
                                            <td>2015</td>
                                            <td>新华书店</td>
                                            <td>152255555553</td>
                                            <td>湫隘说就</td>
                                            <td>备注</td>
                                            <td>标识</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-success" id="btnAdd">导入选中</button>
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
                        </script>&nbsp;版权所有
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
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/bookBasicManagement.js"></script>
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
