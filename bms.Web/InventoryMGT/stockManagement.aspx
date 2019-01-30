<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stockManagement.aspx.cs" Inherits="bms.Web.InventoryMGT.lnventoryList" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->
<!--入库管理-->
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
    <link rel="stylesheet" href="../css/qc.css">
    <link rel="stylesheet" href="../css/jedate.css" />
    <script src="../js/jedate.min.js"></script>
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
                    <%if (funcBook || funcBookStock)
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
                    <li class="active">入库管理</li>
                </ul>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title">入库管理</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <input type="text" id="ID" class="" placeholder="请输入单据编号">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" class="" placeholder="请输入时间段" readonly="readonly" id="time" data-toggle="modal" data-target="#timeModal" />
                                            </div>
                                            <div class="btn-group" role="group">
                                                <select class="selectpicker" data-live-search="true" title="请选择入库来源" id="region">
                                                <option value="">全部入库来源</option>
                                                <%for (int i = 0; i < dsRegion.Tables[0].Rows.Count; i++)
                                                    {%>
                                                <option><%=dsRegion.Tables[0].Rows[i]["regionName"] %></option>
                                                <%} %>
                                            </select>
                                                <%--<input type="text" id="region" class="searchOne" placeholder="请输入入库来源">--%>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <select class="selectpicker" data-live-search="true" title="请选择操作员" id="user">
                                                    <option value="">全部操作员</option>
                                                    <%for (int i = 0; i < dsUser.Tables[0].Rows.Count; i++)
                                                        {%>
                                                    <option><%=dsUser.Tables[0].Rows[i]["userName"] %></option>
                                                    <%} %>
                                                </select>
                                                <%--<input type="text" id="user" class="searchOne" placeholder="请输入操作员名称">--%>
                                                <button class="btn btn-info btn-sm" id="btn-search">查询</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal" style="height: 41px" id="btn-add">添加</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <%--<a href="/InventoryMGT/inventoryStatistics.aspx?type=RK">
                                                    <button class="btn btn-info btn-sm" style="height: 41px" id="tjbb">统计报表</button></a>--%>
                                                 <button class="btn btn-info btn-sm" style="height: 41px" id="tjbb"  data-toggle="modal" data-target="#bbModal">统计报表</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table text-center mostTable table-bordered" id="table">
                                            <thead>
                                                <tr>
                                                    <th>单据编号</th>
                                                    <th>入库来源</th>
                                                    <th>操作员</th>
                                                    <th>单据总数</th>
                                                    <th>实洋</th>
                                                    <th>码洋</th>
                                                    <th>制单时间</th>
                                                    <th>操作</th>
                                                </tr>
                                            </thead>
                                            <%=getData() %>
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

                <!--添加模态框-->
                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                    <div class="modal-dialog" style="max-width: 400px;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title float-left" id="myModalLabel">入库添加</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="close">
                                    <i class="fa fa-close"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <table class="table model-table">
                                    <tr>
                                        <td class="text-right"><span>入库来源:</span></td>
                                        <td>
                                            <select class="selectpicker" data-live-search="true" title="请选择入库来源" id="source">
                                                <option value="">请选择入库来源</option>
                                                <%for (int i = 0; i < dsRegion.Tables[0].Rows.Count; i++)
                                                    {%>
                                                <option value="<%=dsRegion.Tables[0].Rows[i]["regionId"] %>"><%=dsRegion.Tables[0].Rows[i]["regionName"] %></option>
                                                <%} %>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <button type="submit" class="btn btn-info btn-sm" id="btnAdd">添加</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--时间选择模态框-->
            <div class="modal fade" id="timeModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="timeModalLabel">请选择查询时间段</h4>
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
                                            <input type="text" class="jeinput text-center" readonly="readonly" id="startTime" placeholder="年--月--日" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right" style="width: 40%">结束时间:
                                    </td>
                                    <td class="text-left">
                                        <div class="jeinpbox">
                                            <input type="text" class="jeinput text-center" readonly="readonly" id="endTime" placeholder="年--月--日" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">例：开始时间2018-10-26,结束时间2018-10-29;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">只统计26、27、28;&nbsp;&nbsp;&nbsp;不统计29</td>
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

            <%-- 报表查询条件 --%>
            <div class="modal fade" id="bbModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                    <div class="modal-dialog" style="max-width: 450px;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title float-left" id="bbModalLabel">请选择查询条件</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    <i class="fa fa-close"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                <table class="table model-table">
                                    <tr>
                                        <td class="text-right"><span>请选择组织:</span></td>
                                        <td>
                                            <select class="selectpicker" data-live-search="true" title="请选择组织" id="bbsource">
                                                <option value="">请选择组织</option>
                                                <%for (int i = 0; i < dsRegion.Tables[0].Rows.Count; i++)
                                                    {%>
                                                <option value="<%=dsRegion.Tables[0].Rows[i]["regionId"] %>"><%=dsRegion.Tables[0].Rows[i]["regionName"] %></option>
                                                <%} %>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-right"><span>请输入单据编号:</span></td>
                                        <td>
                                            <input type="text" id="singleHeadId" class="" placeholder="请输入单据编号">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <button type="submit" class="btn btn-info btn-sm" id="check">查看</button>
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
    <script src="../js/stockManagement.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/checkLogined.js"></script>
</body>

</html>
