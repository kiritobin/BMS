﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="collectionManagement.aspx.cs" Inherits="bms.Web.CustomerMGT.collectionManagement" %>

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
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/bootstrap-select.css" />
    <link rel="stylesheet" href="../css/pagination.css" />
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
                    <%if (funcCustom)
                        {%>
                    <li class="nav-item active">
                        <a class="nav-link" href="#userManage" data-toggle="collapse">
                            <i class="fa fa-user fa-lg"></i>
                            <p>
                                客户管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="userManage">
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
                                    <a class="nav-link activeNext" href="../BasicInfor/collectionManagement.aspx">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcPut || funcOut || funcReturn)
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
                                    <a class="nav-link activeNext" href="../InventoryMGT/inventoryManagement.aspx">
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
                    <li><a href="javascript:;">客户管理</a></li>
                    <li class="active">客户馆藏数据</li>
                </ul>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title ">客户馆藏数据管理</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" id="bookSearch" placeholder="请输入书名">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" id="isbnSearch" placeholder="请输入ISBN">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <%--<input type="text" value="" class="searchOne" id="cusSearch" placeholder="客户查询">--%>
                                                <select class="modal_select selectpicker" data-live-search="true" id="cusSearch">
                                                    <option value="">请选择客户</option>
                                                    <%for (int i = 0; i < dsCustom.Tables[0].Rows.Count; i++)
                                                        {%>
                                                    <option value="<%=dsCustom.Tables[0].Rows[i]["customerID"] %>"><%=dsCustom.Tables[0].Rows[i]["customerName"] %></option>
                                                    <%} %>
                                                </select>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info" id="btn-search">查询</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info btn-sm" id="" data-toggle="modal" data-target="#myModal">导入</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-danger btn-sm" id="" data-toggle="modal" data-target="#myModal2">删除</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr class="book-tab-tr">
                                                    <th>序号</th>
                                                    <th>ISBN号</th>
                                                    <th>书名</th>
                                                    <th>客户名称</th>
                                                    <th>价格</th>
                                                    <th>数量(册)</th>
                                                </tr>
                                            </thead>
                                            <%--<%= getData() %>--%>
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
            <!--导入模态框 -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">数据操作
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <table class="table text-center model-table">
                                <tr>
                                    <td colspan="2">
                                        <a class="btn btn-info" id="downEx" href="/uploads/muban/客户馆藏数据表.zip">下载模板</a>
                                        <span class="btn btn-info fileinput-button">
                                            <span>选择文件</span>
                                            <input type="file" class="" name="file" id="file" value="">
                                        </span>
                                        <button class="btn btn-info" id="upload">上传</button>
                                    </td>
                                </tr>
                            </table>
                            <div class="text-center">
                                <span>请选择客户:</span>
                                <select class="selectpicker" data-live-search="true" title="请选择客户" data-style="btn-sm" id="model-select-custom" style="float: left;">
                                    <option value="">请选择客户</option>
                                    <%for (int j = 0; j < dsCustom.Tables[0].Rows.Count; j++)
                                        { %>
                                    <option value="<%=dsCustom.Tables[0].Rows[j]["customerId"] %>"><%=dsCustom.Tables[0].Rows[j]["customerName"] %></option>
                                    <%}%>
                                </select>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" id="btnImport" data-toggle="modal">导入</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel2">删除客户数据
                            </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="text-center">
                                <span>请选择客户:</span>
                                <select class="selectpicker" title="请选择客户" data-live-search="true" data-style="btn-sm" id="sel-del" style="float: left;">
                                    <option value="">请选择客户</option>
                                    <%for (int j = 0; j < dsCustom.Tables[0].Rows.Count; j++)
                                        { %>
                                    <option value="<%=dsCustom.Tables[0].Rows[j]["customerId"] %>"><%=dsCustom.Tables[0].Rows[j]["customerName"] %></option>
                                    <%}%>
                                </select>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-danger" id="btndel" data-toggle="modal">删除</button>
                        </div>
                    </div>
                </div>
            </div>

            <%-- marc数据弹窗 --%>
            <div class="modal fade" id="marcModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabe1" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="width: 1050px; max-height: 920px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h3 class="modal-title float-left" id="marcModalLabe">Marc数据导入</h3>
                            <button type="button" class="close" id="marcclose" data-dismiss="modal" aria-hidden="true" style="z-index: 200;">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <table class="bing">
                                <tr>
                                    <td style="text-align: center;">字段名称</td>
                                    <td style="text-align: center;">字段号</td>
                                    <td style="text-align: center;">子字段号</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">ISBN</td>
                                    <td>
                                        <input type="text" id="fisbn" value="" maxlength="3" class="inputNoBorder" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" /></td>
                                    <td>
                                        <input type="text" id="sisbn" value="" maxlength="1" class="inputNoBorder" onkeyup="this.value=this.value.replace(/[^a-z]/ig,'')" /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">书名</td>
                                    <td>
                                        <input type="text" id="fbookName" maxlength="3" value="" class="inputNoBorder" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" />
                                    <td>
                                        <input type="text" id="sbookName" maxlength="1" value="" class="inputNoBorder" onkeyup="this.value=this.value.replace(/[^a-z]/ig,'')" /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">单价</td>
                                    <td>
                                        <input type="text" id="fprice" value="" maxlength="3" class="inputNoBorder" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" />
                                    <td>
                                        <input type="text" id="sprice" value="" maxlength="1" class="inputNoBorder" onkeyup="this.value=this.value.replace(/[^a-z]/ig,'')" /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <nobr>馆藏数量</nobr>
                                    </td>
                                    <td>
                                        <input type="text" id="fnum" maxlength="3" value="" class="inputNoBorder" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" />
                                    <td>
                                        <input type="text" id="snum" maxlength="1" value="" class="inputNoBorder" onkeyup="this.value=this.value.replace(/[^a-z]/ig,'')" /></td>
                                </tr>
                            </table>
                        </div>
                        <p style="text-align: center">
                            例：ISBN的字段号为010，子字段号为a
                        </p>
                        <div>
                            <button class="btn btn-info btn-sm" style="float: right; height: 40px;" id="confirmImport">确认导入</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabe1" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="width: 500px; height: 500px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h3 class="modal-title float-left" id="myModalLabe1">正在导入，请保持网络畅通，导入过程中请勿关闭页面</h3>
                            <button type="button" class="close" id="close" data-dismiss="modal" aria-hidden="true" style="z-index: 300;">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <img style="width: 450px; height: 300px;" src="../imgs/loading.gif" id="img" />
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
    <script src="../js/collectionManagement.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/ajaxfileupload.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/checkLogined.js"></script>
</body>
</html>
