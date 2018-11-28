<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesStatistics.aspx.cs" Inherits="bms.Web.reportStatistics.salesStatistics" %>


<!DOCTYPE html>
<html lang="zh">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>云南新华书店项目综合管理系统</title>

    <!-- CSS -->
    <link href="../css/font-awesome.min.css" rel="stylesheet">
    <link href="../css/material-dashboard.min.css" rel="stylesheet">
    <link href="../css/zgz.css" rel="stylesheet">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/bootstrap-select.css" />
    <link rel="stylesheet" href="../css/pagination.css">
    <link rel="stylesheet" href="../css/jedate.css" />
    <link rel="stylesheet" href="../css/sweetalert2.min.css" />
    <script src="../js/jedate.min.js"></script>
</head>
<body>
    <div class="wrapper ">
        <!-- 左侧垂直导航 -->
        <div class="sidebar" data-color="danger" data-background-color="white" data-image="../imgs/sidebar-2.jpg">
            <!-- 平台字体logo -->
            <div class="logo text-center">
                <a href="javascript:;" class="simple-text text-center logo-normal">云南新华书店项目综合管理系统</a>
                <span class="text-danger"></span>
                <br />
                <span class="text-danger"></span>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/roleManagement.aspx">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/userManagement.aspx">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/organizationalManagement.aspx">
                                        <span class="sidebar-normal">组织管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/bookshelfManagement.aspx">
                                        <span class="sidebar-normal">货架管理</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/customerManagement.aspx">
                                        <span class="sidebar-normal">客户信息管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/collectionManagement.aspx">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/stockManagement.aspx">
                                        <span class="sidebar-normal">入库管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/warehouseManagement.aspx">
                                        <span class="sidebar-normal">出库管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/returnManagement.aspx">
                                        <span class="sidebar-normal">退货管理</span>
                                    </a>
                                </li>
<%--                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/replenishMent.aspx">
                                        <span class="sidebar-normal">补货管理</span>
                                    </a>
                                </li>--%>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#saleManage" data-toggle="collapse">
                            <i class="fa fa-area-chart"></i>
                            <p>
                                销售管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="saleManage">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/tradeManagement.aspx">
                                        <span class="sidebar-normal">销售管理</span>
                                    </a>
                                </li>
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
                            </ul>
                        </div>
                    </li>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookBasicManagement.aspx">
                                        <span class="sidebar-normal">书籍基础数据管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/inventoryManagement.aspx">
                                        <span class="sidebar-normal">书籍库存查看</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item active">
                        <a class="nav-link" href="#ReportStatistics" data-toggle="collapse">
                            <i class="fa fa-table"></i>
                            <p>
                                报表统计
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="ReportStatistics">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link activeNext" href="salesStatistics.aspx">
                                        <span class="sidebar-normal">销售统计</span>
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
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title ">销售统计</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header" style="padding-right: 0px;">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <div class="btn-group" role="group">
                                                    <select class="modal_select selectpicker collectionStatus" id="groupby">
                                                        <option value="">分组方式</option>
                                                        <option value="supplier">供应商</option>
                                                        <option value="region">组织</option>
                                                        <option value="customer">客户</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="btn-group" id="groupsupplier">
                                                <%--<input type="text" class="" placeholder="请输入供应商" id="supplier">--%>
                                                <select class="modal_select selectpicker collectionStatus" title="请选择供应商" data-live-search="true" id="supplier">
                                                    <option>全部</option>
                                                    <%for (int i = 0; i < dsSupplier.Rows.Count; i++)
                                                        {%>
                                                    <option value="<%=dsSupplier.Rows[i]["supplier"] %>"><%=dsSupplier.Rows[i]["supplier"] %></option>
                                                    <%} %>
                                                </select>
                                            </div>
                                            <div class="btn-group" id="groupregion">
                                                <%--<input type="text" class="" placeholder="请输入组织" id="region">--%>
                                                <select class="modal_select selectpicker collectionStatus" title="请选择组织" data-live-search="true" id="region">
                                                    <option>全部</option>
                                                    <%for (int i = 0; i < dsRegion.Tables[0].Rows.Count; i++)
                                                        {%>
                                                    <option value="<%=dsRegion.Tables[0].Rows[i]["regionId"] %>"><%=dsRegion.Tables[0].Rows[i]["regionName"] %></option>
                                                    <%} %>
                                                </select>
                                            </div>
                                            <div class="btn-group" id="groupcustom">
                                                <%--<input type="text" class="" placeholder="请输入客户" id="customer">--%>
                                                <select class="modal_select selectpicker collectionStatus" title="请选择客户" data-live-search="true" id="customer">
                                                    <option>全部</option>
                                                    <%for (int i = 0; i < dsCustom.Tables[0].Rows.Count; i++)
                                                        {%>
                                                    <option value="<%=dsCustom.Tables[0].Rows[i]["customerID"] %>"><%=dsCustom.Tables[0].Rows[i]["customerName"] %></option>
                                                    <%} %>
                                                </select>
                                            </div>
                                            <div class="btn-group">
                                                <input type="text" class="" placeholder="请输入时间段" readonly="readonly" id="time" data-toggle="modal" data-target="#myModal">
                                            </div>
                                            <%-- <div class="btn-group">
                                                <input type="text" class="search" placeholder="请输入定价">
                                            </div>
                                            <div class="btn-group">
                                                <input type="text" class="search" placeholder="请输入销折">
                                            </div>
                                            <div class="btn-group">
                                                <input type="text" class="search" placeholder="请输入进折">
                                            </div>
                                            <div class="btn-group">
                                                <input type="text" class="search" placeholder="请输入采集人用户名">
                                            </div>
                                            <div class="btn-group">
                                                <input type="text" class="search" placeholder="请输入ISBN">
                                            </div>--%>
                                            <div class="btn-group" role="group">
                                                <div class="btn-group" role="group">
                                                    <select class="modal_select selectpicker collectionStatus" id="state">
                                                        <option value="">采集状态</option>
                                                        <option value="">空</option>
                                                        <option value="">销售</option>
                                                        <option value="">预采</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="btn-group">
                                                <button class="btn btn-sm btn-info" id="btn_search">查询</button>
                                            </div>
                                            <div class="btn-group">
                                                <button class="btn btn-sm btn-info" id="exportAll">导出报表</button>
                                            </div>
                                            <div class="btn-group">
                                                <button class="btn btn-sm btn-info" id="exportDe">导出明细</button>
                                            </div>
                                            <div class="btn-group">
                                                <button class="btn btn-sm btn-info" id="print">打印报表</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr class="book-tab-tr text-nowrap">
                                                    <th>序号</th>
                                                    <th id="showType">供应商</th>
                                                    <th>品种</th>
                                                    <th>数量</th>
                                                    <th>码洋</th>
                                                    <th>实洋</th>
                                                    <th>操作</th>
                                                </tr>
                                            </thead>
                                            <%--<%=getData() %>--%>
                                        </table>
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
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">请选择查询时间段</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body" style="max-height: 400px; overflow: auto;">
                            <table class="table text-center model-table">
                                <tr>
                                    <td class="text-right" style="width: 40%">开始时间:
                                    </td>
                                    <td class="text-left">
                                        <div class="jeinpbox">
                                            <input type="text" class="jeinput text-center" readonly="readonly" id="startTime" placeholder="年--月--日">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right" style="width: 40%">结束时间:
                                    </td>
                                    <td class="text-left">
                                        <div class="jeinpbox">
                                            <input type="text" class="jeinput text-center" readonly="readonly" id="endTime" placeholder="年--月--日">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">例：开始时间2018-10-26,结束时间2018-10-29;只统计26,27,28;29不统计</td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-info" id="modalClose">清空</button>&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            <button type="button" class="btn btn-info" id="btnOK">确认</button>
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
                        &nbsp;版权归云南新华书店图书有限公司所有
                                <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                    </div>
                </div>
            </footer>
        </div>
    </div>

    <!-- jQuery -->
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/salesStatistics.js"></script>
    <!-- Bootstrap JavaScript -->
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/sweetalert2.min.js"></script>
    <script src="../js/jquery.pagination.js"></script>
</body>
</html>

