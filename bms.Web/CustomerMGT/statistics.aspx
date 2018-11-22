<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="statistics.aspx.cs" Inherits="bms.Web.CustomerMGT.statistics" %>

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
    <link rel="stylesheet" href="../css/jedate.css" />
</head>

<body>
    <div class="wrapper ">
        <!-- 左侧垂直导航 -->
        <div class="sidebar" data-color="danger" data-background-color="white" data-image="../imgs/sidebar-2.jpg">
            <!-- 平台字体logo -->
            <div class="logo text-center">
                <a href="javascript:;" class="simple-text text-center logo-normal">云南新华书店项目综合管理系统</a>
                <span class="text-danger"></span><br />
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../InventoryMGT/replenishMent.aspx">
                                        <span class="sidebar-normal">补货管理</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item active">
                        <a class="nav-link" href="#saleManage" data-toggle="collapse">
                            <i class="fa fa-area-chart"></i>
                            <p>
                                销售管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="saleManage">
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
                                <li class="nav-item">
                                    <a class="nav-link activeNext" href="statistics.aspx">
                                        <span class="sidebar-normal">大屏统计</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#baseManage" data-toggle="collapse">
                            <i class="fa fa-file-archive-o"></i>
                            <p>基础信息<b class="caret"></b>
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
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title ">大屏统计设置</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <select class="modal_select selectpicker" data-live-search="true" id="startTime">
                                                    <option value="">开始时间</option>                                                  
                                                </select>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <%--<select class="modal_select selectpicker" data-live-search="true" id="endTime">
                                                    <option value="">结束时间</option>                                                  
                                                </select>--%>
                                                <input id="datepicker" placeholder="请选择时间" type="text" readonly/>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <select class="modal_select selectpicker" data-live-search="true" id="cusSearch">
                                                    <option value="">选择组织</option>                                                  
                                                </select>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-success btn-sm" id="" data-toggle="modal" data-target="#myModal2">确定</button>
                                            </div>
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
    <%--<script>

        $('.selectpicker').selectpicker({
            'selectedText': 'cat'
        });
    </script>--%>

    <script src="../js/jquery-3.3.1.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/ajaxfileupload.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/jedate.min.js"></script>
</body>
</html>
