<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerPurchase.aspx.cs" Inherits="bms.Web.CustomerMGT.customerPurchase" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
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
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/pagination.css" />
    <style>
    </style>
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="wrapper ">
        <div class="main-panel" style="margin: 0px auto; float: none;">
            <!-- 主界面头部面板 -->
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container">
                    <div class="navbar-wrapper">
                    </div>
                    <a class="btn btn-white btn-sm" href="javascript:logout();">退出系统</a>
                </div>
            </nav>
            <!-- 主界面内容 -->
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title">客户采购查询</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="searchOne" id="bookSearch" placeholder="书名查询">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="searchOne" id="goodsSearch" placeholder="供应商查询">
                                                <button class="btn btn-info btn-sm" id="btn-search">查询</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <nobr>单据编号</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>ISBN</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>书名</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>单价</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>数量</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>码洋（册）</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>实洋</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>折扣</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>供应商</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>采购日期</nobr>
                                                    </th>
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

            <!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <!-- 版权内容 -->
                    <div class="copyright text-center">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear());
                        </script>
                        &nbsp;版权所有
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/customerPurchase.js"></script>
    <script src="../js/jquery.pagination.js"></script>
</body>
</html>
