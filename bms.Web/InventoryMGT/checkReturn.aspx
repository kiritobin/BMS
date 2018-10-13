<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkReturn.aspx.cs" Inherits="bms.Web.InventoryMGT.checkReturn" %>

<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>图书综合管理系统</title>
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
                <a href="javascript:;" class="simple-text text-center logo-normal">图书综合管理平台
                </a>
            </div>
             <div class="sidebar-wrapper">
                <ul class="nav">
                    <%if (funcUser||funcRole||funcOrg||funcGoods) { %>
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
                                <%if (funcUser) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/userManagement.aspx">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcRole) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/roleManagement.aspx">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcOrg) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/organizationalManagement.aspx">
                                        <span class="sidebar-normal">组织管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcGoods) { %>
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
                    <%if (funcCustom) {%>
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
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcPut||funcOut||funcReturn||funcSupply) {%>
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
                                <%if (funcOut) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/warehouseManagement.aspx">
                                        <span class="sidebar-normal">出库管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcPut) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/stockManagement.aspx">
                                        <span class="sidebar-normal">入库管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcReturn) { %>
                                <li class="nav-item">
                                    <a class="nav-link  activeNext" href="../InventoryMGT/returnManagement.aspx">
                                        <span class="sidebar-normal">退货管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcSupply) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/replenishMent.aspx">
                                        <span class="sidebar-normal">补货管理</span>
                                    </a>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%} %>
                    <%if (funcSale||funcSaleOff) { %>
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
                                <%if (funcSale) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/tradeManagement.aspx">
                                        <span class="sidebar-normal">销售管理</span>
                                    </a>
                                </li>
                                <%} %>
                                <%if (funcSaleOff) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/backQuery.aspx">
                                        <span class="sidebar-normal">销退管理</span>
                                    </a>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </li>
                    <%} %>
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
                                <%if (funcLibrary) { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/collectionManagement.aspx">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                                <%} %>
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
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-header card-header-danger">
                                        <h4 class="card-title">退货查询</h4>
                                    </div>
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                        <div class="btn-group" role="group">
                                            <button class="btn btn-warning btn-sm" id="back" onclick="javascript:history.back(-1);">返回</button>
                                        </div>
                                            <div class="btn-group" role="group">
                                            <button class="btn btn-success btn-sm" id="export">导出</button>
                                        </div>
                                        <div class="btn-group" role="group">
                                            <button class="btn btn-info btn-sm" id="print">打印</button>
                                        </div>
                                        <table class="table text-center table_stock">
                                            <tr>
                                                <td class="td_text"><span class="span-text">单据编号:</span></td>
                                                <td class="td_width">
                                                    <input type="text" value="<%=shId %>" class="input_text" placeholder="单据编号"></td>
                                                <td class="td_text"><span class="span-text">操作员:</span></td>
                                                <td class="td_width">
                                                    <input type="text" value="<%=shOperator %>" class="input_text" placeholder="操作员"></td>
                                                <td class="td_text"><span class="span-text">单据总数:</span></td>
                                                <td class="td_width">
                                                    <input type="text" value="<%=shCount %>" class="input_text" placeholder="单据总数"></td>
                                            </tr>
                                            <tr>
                                                <td class="td_text"><span class="span-text">退货组织:</span></td>
                                                <td class="td_width">
                                                    <input type="text" value="<%=shRegionName %>" class="input_text" placeholder="入库组织"></td>
                                                <td class="td_text"><span class="span-text">总码洋:</span></td>
                                                <td class="td_width">
                                                    <input type="text" value="<%=shTotalPrice %>" class="input_text" placeholder="总码洋"></td>
                                            </tr>
                                            <tr>
                                                <td class="td_text"><span class="span-text">总实洋:</span></td>
                                                <td class="td_width">
                                                    <input type="text" value="<%=shRealPrice %>" class="input_text" placeholder="总实洋"></td>
                                            </tr>
                                            <tr>
                                                <td class="td_text"><span class="span-text">制单日期:</span></td>
                                                <td class="td_width">
                                                    <div class="jeinpbox">
                                                        <input type="text" value="<%=shTime %>" class="jeinput input_text" id="test2" placeholder="YYYY年MM月DD日">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--<div class="input-group no-border">
                                            <input type="text" value="" class="form-control col-sm-2 input-search" placeholder="请输入查询条件">
                                            <button class="btn btn-info btn-sm" id="btn-search"><i class="fa fa-search fa-lg"></i>&nbsp;查询</button>
                                              &nbsp;
                                            <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp;添加</button>
                                        </div>--%>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr style="border: 2px solid #DDD">
                                                    <td colspan="9">商品</td>
                                                </tr>
                                                <tr>
                                                    <td>序号</td>
                                                    <td>单据编号</td>
                                                    <td>ISBN号</td>
                                                    <td>商品数量</td>
                                                    <td>单价</td>
                                                    <td>折扣</td>
                                                    <td>码洋</td>
                                                    <td>实洋</td>
                                                    <td>货架名</td>
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

                <!-- 主界面页脚部分 -->
                <footer class="footer">
                    <div class="container-fluid">
                        <!-- 版权内容 -->
                        <div class="copyright text-center">
                            &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                            &nbsp;版权所有
                        </div>
                    </div>
                </footer>
            </div>
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
    <!-- alert.js -->
    <script src="../js/sweetalert2.js"></script>
    <!-- paging.js -->
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/jedate.min.js"></script>
    <script src="../js/checkReturn.js"></script>
</body>

</html>
