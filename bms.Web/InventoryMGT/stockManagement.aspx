﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stockManagement.aspx.cs" Inherits="bms.Web.InventoryMGT.lnventoryList" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->
<!--入库管理-->
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
                    <li class="nav-item active">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="material-icons">book</i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="inventoryManage">
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
                                    <a class="nav-link" href="../SalesMGT/tradeManagement.aspx">
                                        <span class="sidebar-normal">营销管理</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#baseManage" data-toggle="collapse">
                            <i class="material-icons">bubble_chart</i>
                            <p>
                                基础信息
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="baseManage">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookshelfManagement.aspx">
                                        <span class="sidebar-normal">架位管理</span>
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
                                    <h4 class="card-title">入库管理</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">                                            
                                            <div class="btn-group" role="group">
                                                <input type="text" class="searchOne" id="btn-search1" placeholder="请输入查询内容">
                                                <button class="btn btn-info btn-sm" id="btn-search2">查询</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp;添加</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table text-center mostTable table-bordered">
                                            <thead>
                                                <tr>
                                                    <td>单编ID</td>
                                                    <td>组织名称</td>
                                                    <td>操作员名称</td>
                                                    <td>单据总数</td>
                                                    <td>总实洋</td>
                                                    <td>总码洋</td>
                                                    <td>到货时间</td>
                                                    <td>付款时间</td>
                                                    <td>制单时间</td>
                                                    <td>操作</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>10000001</td>
                                                    <td>加基森</td>
                                                    <td>保罗</td>
                                                    <td>3929</td>
                                                    <td>56</td>
                                                    <td>456</td>
                                                    <td>2018-12-23</td>
                                                    <td>2019-8-9</td>
                                                    <td>2020-9-8</td>
                                                    <td>
                                                        <button class="btn btn-success btn-sm" onclick="window.location.href='addStock.aspx'"><i class="fa fa-plus fa-lg"></i></button>
                                                        <button class="btn btn-info btn-sm" onclick="window.location.href='checkStock.aspx'"><i class="fa fa-search"></i></button>
                                                        <button class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></button>
                                                    </td>
                                                </tr>
                                            </tbody>
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
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title float-left" id="myModalLabel">入库添加</h4>
                    </div>
                    <div class="modal-body">
                        <table class="table model-table">
                            <tr>
                                <td class="model-td-left"><span class="model-tab-td-span">单据ID:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="headID" placeholder="">
                                </td>
                                <td class="model-td-left"><span class="model-tab-td-span">组织名称:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="regionID" placeholder="">
                                </td>
                            </tr>
                             <tr>
                                <td class="model-td-left"><span class="model-tab-td-span">操作员名称:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="userName" placeholder="">
                                </td>
                                <td class="model-td-left"><span class="model-tab-td-span">单据总数:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="billCount" placeholder="">
                                </td>
                            </tr>
                             <tr>
                                <td class="model-td-left"><span class="model-tab-td-span">总码洋:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="totalPrice" placeholder="">
                                </td>
                                <td class="model-td-left"><span class="model-tab-td-span">总实洋:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="realPrice" placeholder="">
                                </td>
                            </tr>
                             <tr>
                                <td class="model-td-left"><span class="model-tab-td-span">到货时间:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="time1" placeholder="">
                                </td>
                                <td class="model-td-left"><span class="model-tab-td-span">付款时间:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="time2" placeholder="">
                                </td>
                            </tr>
                             <tr>
                                <td class="model-td-left"><span class="model-tab-td-span">制单时间:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="time3" placeholder="">
                                </td>
                                <td class="model-td-left"><span class="model-tab-td-span">备注:</span></td>
                                <td>
                                    <input type="text" value="" class="form-control col-sm-15 input-search" id="remarks" placeholder="">
                                </td>
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
    <script src="../js/bookshelfManagement.js"></script>
</body>

</html>
