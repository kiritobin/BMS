﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkStock.aspx.cs" Inherits="bms.Web.InventoryMGT.checkStock" %>

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
    <link rel="stylesheet" href="../css/jedate.css" />
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/qc.css">
    <link rel="stylesheet" id="changeprint" href="../css/a4print.css" media="print">
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
                    <li class="nav-item active">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="fa fa-book"></i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="inventoryManage">
                            <ul class="nav">
                                <%if (funcPut)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link activeNext" href="../InventoryMGT/stockManagement.aspx">
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
                    <%if (funcBook || funcBook)
                        { %>
                    <li class="nav-item">
                        <a class="nav-link" href="#baseManage" data-toggle="collapse">
                            <i class="fa fa-file-archive-o"></i>
                            <p>
                                基础信息
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="baseManage">
                            <ul class="nav">
                                <%if (funcBook)
                                    { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookBasicManagement.aspx">
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
                    <div class="navbar-wrapper">
                    </div>
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
                    <li><a href="javascript:;">库存管理</a></li>
                    <li><a href="stockManagement.aspx">入库管理</a></li>
                    <li class="active">入库查询</li>
                </ul>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title">入库查询</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-warning btn-sm" id="back" onclick="javascript:history.back(-1);">返回</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info btn-sm" id="export">导出</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <%--<button class="btn btn-info btn-sm" id="print">打印</button>--%>
                                                 <button class="btn btn-info btn-sm" id="print" data-toggle="modal" data-target="#printmodel">打印</button>
                                            </div>
                                        </div>
                                        <%--<div class="input-group no-border">
                                            <input type="text" value="" class="form-control col-sm-2 input-search" placeholder="请输入查询条件">
                                            <button class="btn btn-info btn-sm" id="btn-search"><i class="fa fa-search fa-lg"></i>&nbsp;查询</button>
                                              &nbsp;
                                            <button class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp;添加</button>
                                        </div>--%>
                                    </div>
                                    <div id="content">
                                        <table class="table table_stock text-right">
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>单据编号:</span>
                                                </td>
                                                <td>
                                                    <input value="<%=putId %>" class="form-control" disabled id="RKId">
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <span>操作员:</span>
                                                </td>
                                                <td>
                                                    <input value="<%=putOperator %>" class="form-control" disabled id="operator">
                                                </td>
                                            </tr>
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>入库来源:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=putRegionName %>" class="form-control" disabled id="source"></td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <span>制单日期:</span>
                                                </td>
                                                <td>
                                                    <div class="jeinpbox">
                                                        <input type="text" value="<%=putTime %>" class="form-control" disabled id="test2">
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                        <div class="table-responsive">
                                            <table class="table mostTable table-bordered text-center" id="table">
                                                <thead>
                                                    <tr>
                                                        <td>
                                                            <nobr>序号</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>ISBN号</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>书名</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>数量</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>单价</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>折扣</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>码洋</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>实洋</nobr>
                                                        </td>
                                                        <td>
                                                            <nobr>货架</nobr>
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <%=getData() %>
                                            </table>
                                        </div>
                                        <div>
                                            <table class="table table_stock text-right">
                                                <tr class="text-nowrap">
                                                    <td>
                                                        <span>总合计:</span>
                                                    </td>
                                                    <td>
                                                        <span>单据总数:</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" value="<%=putCount %>" class="form-control" disabled id="allCount">
                                                    </td>
                                                    <td>
                                                        <span>总码洋:</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" value="<%=putTotalPrice %>" class="form-control" disabled id="allToatlPrice">
                                                    </td>
                                                    <td>
                                                        <span>总实洋:</span>
                                                    </td>
                                                    <td>
                                                        <input type="text" value="<%=putRealPrice %>" class="form-control" disabled id="allRealPrice">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <div class="m-style paging"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- a4打印 --%>
            <div class="table-responsive" id="a4t">
                <p>
                    <h3 class="table-responsive" style="text-align: center"><span id="pname"></span>  入库单</h3>
                </p>
                <table class="table table_stock text-right" id="tablehead">
                    <tr class="text-nowrap">
                        <td>
                            <span>单据编号:</span>
                        </td>
                        <td>
                            <%--<input value="<%=putId %>" class="form-control" disabled>--%>
                            <div><%=putId %></div>
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                            <span>操作员:</span>
                        </td>
                        <td>
                            <%--<input value="<%=putOperator %>" class="form-control" disabled>--%>
                            <div><%=putOperator %></div>
                        </td>
                    </tr>
                    <tr class="text-nowrap">
                        <td>
                            <span>入库来源:</span>
                        </td>
                        <td>
                            <%--<input type="text" value="<%=putRegionName %>" class="form-control" disabled>--%>
                            <div><%=putRegionName %></div>
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                            <span>制单日期:</span>
                        </td>
                        <td>
                            <div class="jeinpbox">
                                <%--<input type="text" value="<%=putTime %>" class="form-control" disabled>--%>
                                <div><%=putTime %></div>
                            </div>
                        </td>
                    </tr>
                    <tr class="text-nowrap">
                        <td>
                            <span>单据总数:</span>
                        </td>
                        <td>
                            <%--<input type="text" value="<%=putCount %>" class="form-control" disabled>--%>
                            <div><%=putCount %></div>
                        </td>
                        <td>
                            <span>总码洋:</span>
                        </td>
                        <td>
                            <%--<input type="text" value="<%=putTotalPrice %>" class="form-control" disabled>--%>
                            <div><%=putTotalPrice %></div>
                        </td>
                        <td>
                            <span>总实洋:</span>
                        </td>
                        <td>
                            <%--<input type="text" value="<%=putRealPrice %>" class="form-control" disabled>--%>
                            <div><%=putRealPrice %></div>
                        </td>
                    </tr>
                </table>
                <table border="1" cellspacing="0" class="table mostTable table-bordered text-center" id="print_table">
                    <thead>
                        <tr>
                            <td>
                                <nobr>序号</nobr>
                            </td>
                            <td>
                                <nobr>商品编号</nobr>
                            </td>
                            <td>
                                <nobr>商品名称</nobr>
                            </td>
                            <td>
                                <nobr>商品数量</nobr>
                            </td>
                            <td>
                                <nobr>单价</nobr>
                            </td>
                            <td>
                                <nobr>折扣</nobr>
                            </td>
                            <td>
                                <nobr>码洋</nobr>
                            </td>
                            <td>
                                <nobr>实洋</nobr>
                            </td>
                            <td>
                                <nobr>货架</nobr>
                            </td>
                        </tr>
                    </thead>
                </table>
            </div>

            <!--打印弹窗-->
            <div class="modal fade" id="printmodel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="max-width: 300px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="showTittle">请选择打印方式</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body text-center" style="max-height: 400px;">
                            <div>
                                <button type="button" class="btn btn-info" id="a4">A4纸打印</button>
                                <button type="button" class="btn btn-info" id="zhen">多联纸打印</button>
                            </div>
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
    <!-- selectpicker.js -->
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/defaults-zh_CN.js"></script>
    <!-- alert.js -->
    <script src="../js/sweetalert2.js"></script>
    <!-- paging.js -->
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/jedate.min.js"></script>
    <script src="../js/checkStock.js"></script>
    <script src="../js/jquery-migrate-1.2.1.min.js"></script>
    <script src="../js/jquery.jqprint.js"></script>
    <script src="../js/public.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/checkLogined.js"></script>
</body>
</html>
