﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookBasicManagement.aspx.cs" Inherits="bms.Web.BasicInfor.bookBasicManagement" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>云南新华书店项目综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
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
    <div class="wrapper ">
        <!-- 左侧垂直导航 -->
        <div class="sidebar" data-color="danger" data-background-color="white" data-image="../imgs/sidebar-2.jpg">
            <!-- 平台字体logo -->
            <div class="logo text-center">
                <a href="javascript:;" class="simple-text text-center logo-normal">云南新华书店项目综合管理系统</a>
                <span class="text-danger"><%=userName %></span><br />
                <span class="text-danger"><%=regionName %></span>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <%if (funcUser || funcRole || funcOrg || funcGoods)
                        { %>
                    <li class="nav-item">
                        <a class="nav-link" href="#securityManage" data-toggle="collapse">
                            <i class="fa fa-cogs"></i>
                            <p>
                                权限管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="securityManage">
                            <ul class="nav">
                                <%if (funcRole)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/roleManagement.aspx">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcUser)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/userManagement.aspx">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcOrg)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/organizationalManagement.aspx">
                                        <span class="sidebar-normal">组织管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcGoods)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/bookshelfManagement.aspx">
                                        <span class="sidebar-normal">货架管理</span>
                                    </a>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcCustom || funcLibrary)
                        {%>
                    <li class="nav-item">
                        <a class="nav-link" href="#userManage" data-toggle="collapse">
                            <i class="fa fa-user fa-lg"></i>
                            <p>
                                客户管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="userManage">
                            <ul class="nav">
                                <%if (funcCustom)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/customerManagement.aspx">
                                        <span class="sidebar-normal">客户信息管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcLibrary)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/collectionManagement.aspx">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcPut || funcOut || funcReturn || funcSupply)
                        {%>
                    <li class="nav-item">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="fa fa-book"></i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="inventoryManage">
                            <ul class="nav">
                                <%if (funcPut)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/stockManagement.aspx">
                                        <span class="sidebar-normal">入库管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcOut)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/warehouseManagement.aspx">
                                        <span class="sidebar-normal">出库管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcReturn)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/returnManagement.aspx">
                                        <span class="sidebar-normal">退货管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcSupply)
                                    { %>
                                <%--<li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/replenishMent.aspx">
                                        <span class="sidebar-normal">补货管理</span>
                                    </a>
                                </li>--%>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcSale || funcSaleOff || funcRetail)
                        { %>
                    <li class="nav-item ">
                        <a class="nav-link" href="#saleManage" data-toggle="collapse">
                            <i class="fa fa-area-chart"></i>
                            <p>
                                团采管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="saleManage">
                            <ul class="nav">
                                <%if (funcSale)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/tradeManagement.aspx">
                                        <span class="sidebar-normal">销售管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcRetail)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/retailManagement.aspx" id="retail">
                                        <span class="sidebar-normal">零售管理</span>
                                    </a>
                                </li>
                                <li class="nav-item" id="customerRetail">
                                    <a class="nav-link" href="../SalesMGT/customerRetail.aspx">
                                        <span class="sidebar-normal">POS收款</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/retailBackManagement.aspx" id="retailBack">
                                        <span class="sidebar-normal">零退管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/statistics.aspx">
                                        <span class="sidebar-normal">大屏配置</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcBook||funcBookStock)
                        { %>
                    <li class="nav-item active">
                        <a class="nav-link" href="#baseManage" data-toggle="collapse">
                            <i class="fa fa-file-archive-o"></i>
                            <p>
                                基础信息
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="baseManage">
                            <ul class="nav">
                                <%if (funcBook)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link activeNext" href="../BasicInfor/bookBasicManagement.aspx">
                                        <span class="sidebar-normal">书籍基础数据管理</span>
                                    </a>
                                </li>
                                <%}%>
                                <%if (funcBookStock)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/inventoryManagement.aspx">
                                        <span class="sidebar-normal">书籍库存查看</span>
                                    </a>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%if (isAdmin)
                    { %>
                    <li class="nav-item">
                        <a class="nav-link" href="#ReportStatistics" data-toggle="collapse">
                            <i class="fa fa-table"></i>
                            <p>
                                报表统计
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="ReportStatistics">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/stockStatistics.aspx">
                                        <span class="sidebar-normal">入库统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/warehouseStatistics.aspx">
                                        <span class="sidebar-normal">出库统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/returnStatistics.aspx">
                                        <span class="sidebar-normal">退货统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/salesStatistics.aspx">
                                        <span class="sidebar-normal">销售统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/selloffStatistics.aspx">
                                        <span class="sidebar-normal">销退统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/retailStatistics.aspx">
                                        <span class="sidebar-normal">零售统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../ReportStatistics/bookStock.aspx">
                                        <span class="sidebar-normal">库存统计</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <span class="nav-link">
                                        <span class="sidebar-normal"></span>
                                    </span>
                                </li>

                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%} %>
                </ul>
            </div>

        </div>
        <div class="main-panel">
            <!-- 主界面头部面板 -->
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container-fluid">
                    <div class="navbar-wrapper"></div>
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
                                    <i class="fa fa-gear"></i>
                                    <p class="d-lg-none d-md-block">
                                        更多设置
                                    </p>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" href="../changePwd.aspx">修改密码</a>
                                    <a class="dropdown-item" href="javascript:logout();">退出系统</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <!-- 主界面内容 -->
            <div class="content">
                <ul class="breadcrumb">
                    <li><a href="javascript:;">基础信息</a></li>
                    <li class="active">书籍基础数据管理</li>
                </ul>
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
                                                <input type="text" value="" class="" id="bookName" placeholder="请输入书名">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="" id="bookNum" placeholder="请输入书号">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="" id="bookISBN" placeholder="请输入ISBN">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="" id="bookDiscount2" placeholder="请输入进货折扣" readonly="readonly" data-toggle="modal" data-target="#numberModal2">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="" id="bookDiscount" placeholder="请输入销售折扣" readonly="readonly" data-toggle="modal" data-target="#numberModal">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <select class="modal_select selectpicker collectionStatus" title="请选择供应商" data-live-search="true" id="bookGys">
                                                    <option>全部供应商</option>
                                                    <%for (int i = 0; i < dsSupplier.Rows.Count; i++)
                                                        {%>
                                                    <option value="<%=dsSupplier.Rows[i]["supplier"] %>"><%=dsSupplier.Rows[i]["supplier"] %></option>
                                                    <%} %>
                                                </select>
                                                <%--<input type="text" value="" class="searchOne" id="bookGys" placeholder="请输入供应商">--%>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info btn-sm" id="btn-search">查询</button>
                                            </div>
                                            <%if (funcBook)
                                                { %>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add">导入</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info btn-sm" id="btn-out">导出</button>
                                            </div>
                                            <%} %>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr class="book-tab-tr">
                                                    <th>
                                                        <nobr>序号</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>书号</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>书名</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>定价</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>出版日期</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>供应商</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>ISBN</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>预收数量</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>进货折扣</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>销售折扣</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>备注</nobr>
                                                    </th>
                                                    <%if (funcBook)
                                                        { %>
                                                    <th>
                                                        <nobr>操作</nobr>
                                                    </th>
                                                    <%} %>
                                                </tr>
                                            </thead>
                                           <%--<%=getData() %>--%>
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
            <!-- 添加书籍模态框 -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">基础数据导入</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body" style="max-height: 500px; overflow: auto;">
                            <table class="table text-center model-table">
                                <tr>
                                    <td>
                                        <a class="btn btn-info" id="downEx" style="color: white" href="/uploads/muban/基础数据表.zip">下载模板</a>
                                        <a class="btn btn-info text-white fileinput-button">
                                            <span>选择文件</span>
                                            <input type="file" name="file" id="file">
                                        </a>
                                        <a class="btn btn-info text-white" id="upload">上传文件</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-info" id="btnImport">导入</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 导入模态框 -->
            <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabe1" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="width: 500px; max-height: 500px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h3 class="modal-title float-left" id="myModalLabe1">正在导入，请保持网络畅通，导入过程中请勿关闭页面</h3>
                            <button type="button" class="close" id="close" data-dismiss="modal" aria-hidden="true" style="z-index: 100;">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <img style="width: 450px; height: 300px;" src="../imgs/loading.gif" id="img" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- 输入销售折扣模态框 -->
            <div class="modal fade modal-mini modal-primary" id="numberModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;" aria-hidden="true">
                <div class="modal-dialog" style="max-width: 580px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">按销售折扣查询</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body text-center">
                            <table class="table text-center" id="table_numberModal">
                                <tr>
                                    <td class="text-right">方式：</td>
                                    <td colspan="2" class="text-left">
                                        <div style="margin-top: 8px">
                                            <label class="radio-inline">
                                                <input type="radio" name="optionsRadios" value="小于" checked>
                                                小于
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="optionsRadios" checked="checked" style="margin-left: 20px" value="等于">
                                                等于
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="optionsRadios" style="margin-left: 20px" value="大于">
                                                大于
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">请输入折扣值：</td>
                                    <td class="text-left" colspan="2">
                                        <input type="number" value="" id="number"></td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btn_clear" class="btn btn-success btn-sm" style="margin-right: 10px">
                                清除
                            </button>
                            <button type="button" id="btn_number" class="btn btn-success btn-sm">
                                确定
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 输入进货折扣模态框 -->
            <div class="modal fade modal-mini modal-primary" id="numberModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;" aria-hidden="true">
                <div class="modal-dialog" style="max-width: 580px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">按进货折扣查询</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body text-center">
                            <table class="table text-center" id="table_numberModal">
                                <tr>
                                    <td class="text-right">方式：</td>
                                    <td colspan="2" class="text-left">
                                        <div style="margin-top: 8px">
                                            <label class="radio-inline">
                                                <input type="radio" name="optionsRadios2" value="小于" checked>
                                                小于
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="optionsRadios2" checked="checked" style="margin-left: 20px" value="等于">
                                                等于
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="optionsRadios2" style="margin-left: 20px" value="大于">
                                                大于
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right">请输入折扣值：</td>
                                    <td class="text-left" colspan="2">
                                        <input type="number" value="" id="number2"></td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btn_clear2" class="btn btn-success btn-sm" style="margin-right: 10px">
                                清除
                            </button>
                            <button type="button" id="btn_number2" class="btn btn-success btn-sm">
                                确定
                            </button>
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
                        &nbsp;版权归云南新华书店图书有限公司所有 滇ICP备09009025号-4
                        <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
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
    <script src="../js/defaults-zh_CN.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/demo.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/bookBasicManagement.js"></script>
    <script src="../js/ajaxfileupload.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/checkLogined.js"></script>
</body>

</html>
