<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesDetail.aspx.cs" Inherits="bms.Web.SalesMGT.salesDetail" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title></title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 材料图标样式 -->
    <link rel="stylesheet" href="../css/materialdesignicons.css">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/demo.css">
    <link rel="stylesheet" href="../css/pagination.css" />
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <h3 class="text-center">销&nbsp;售</h3>
                        <hr />
                        <div class="card-body">
                            <div class="card-header card_btn">
                                <div class="input-group">
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success" data-toggle="modal" data-target="#myModa2">添加销售</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_bookName" placeholder="请输入书名">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_ISBN" placeholder="请输入ISBN">
                                        <button class="btn btn-info" id="btn_search">查询</button>
                                    </div>
                                    <%if (type == "addsale")
                                        {%>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success" data-toggle="modal" data-target="#myModa2"><i class="fa fa-print"></i></button>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                            <div class="content_tab col-md-12">
                                <div class="table-responsive col-md-10">
                                    <table class="table mostTable table-bordered text-center" id="table">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <nobr>序号</nobr>
                                                </th>
                                                <th>
                                                    <nobr>书名</nobr>
                                                </th>
                                                <th>
                                                    <nobr>ISBN号</nobr>
                                                </th>
                                                <th>
                                                    <nobr>单价</nobr>
                                                </th>
                                                <th>
                                                    <nobr>数量</nobr>
                                                </th>
                                                <th>
                                                    <nobr>实际折扣</nobr>
                                                </th>
                                                <th>
                                                    <nobr>实洋</nobr>
                                                </th>
                                                <th>
                                                    <nobr>时间</nobr>
                                                </th>
                                                <th>
                                                    <nobr>操作</nobr>
                                                </th>
                                            </tr>
                                        </thead>
                                        <%=getData() %>
                                    </table>
                                </div>
                                <div class="statistics col-md-2">
                                    统计
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
    </div>
    <!--模态框-->
    <div class="modal fade" id="myModa2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 900px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">添加销售</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="material-icons">clear</i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table model-table">
                        <tr>
                            <td>ISBN号</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                            <td>实际折扣</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                            <td>单价</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                        </tr>
                        <tr>
                            <td>数量</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                            <td>实洋</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success btn-sm" id="btnAdd">添加</button>
                </div>
            </div>
        </div>
    </div>
    <!-- js file -->
    <script src="../js/jquery-3.3.1.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 事物处理 -->
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/demo.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <!-- selectpicker.js -->
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/salesDetail.js"></script>
</body>

</html>
