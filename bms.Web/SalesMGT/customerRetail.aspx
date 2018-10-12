<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerRetail.aspx.cs" Inherits="bms.Web.SalesMGT.customerRetail" %>

<%="" %>
<!DOCTYPE html>
<html class="no-js">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>图书综合管理平台</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!--分页样式-->
    <link rel="stylesheet" href="../css/pagination.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/jedate.css" />
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
</head>

<body>
    <div class="retail-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-4 col-lg-3">
                    <img src="../imgs/YNXH-LOGO.png" id="topImg" class="img-fluid" alt="Cinque Terre">
                </div>
                <div class="col-sm-2">
                    <input type="text" id="search" class="topSearch">
                </div>
            </div>
            <div class="card">
                <div class="card-header card-header-danger">
                    <h3 class="card-title text-center">自助扫码开单</h3>
                </div>
                <div class="card-body">
                    <%--<div class="row">
                        <div class="input-group col-md-12">
                            <div class="btn-group" role="group">
                                <input type="text" placeholder="请输入ISBN" id="search" class="searchOne">
                                <button class="btn btn-info btn-sm" id="btnSearch" data-toggle="modal">扫描</button>
                                <button class="btn btn-info btn-sm" id="save" data-toggle="modal">save</button>
                                <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal">添加</button>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-9 col-lg-8 text-right">
                            <div class="text-right">时间：<span id="time"></span></div>
                        </div>
                        <div class="table-responsive col-md-9">
                            <div style="height: 500px; display: block; overflow-y: auto;">
                                <table class="table mostTable table-bordered text-center test" id="table">
                                    <thead>
                                        <tr>
                                            <td>
                                                <nobr>序号</nobr>
                                            </td>
                                            <td>
                                                <nobr>ISBN</nobr>
                                            </td>
                                            <td>
                                                <nobr>书名</nobr>
                                            </td>
                                            <td>
                                                <nobr>单价</nobr>
                                            </td>
                                            <td style="display: none">
                                                <nobr>数量</nobr>
                                            </td>
                                            <td>
                                                <nobr>商品数量</nobr>
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
                                            <td style="display: none">
                                                <nobr>书号</nobr>
                                            </td>
                                            <td>
                                                <nobr>操作</nobr>
                                            </td>
                                        </tr>
                                    </thead>
                                    <tr class="first">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <%--<h3 class="text-center">零售单</h3>
                            <hr />
                            <ul class="list-unstyled">
                                <li>种类：<span id="kind"></span></li>
                                <li>数量：<span id="number"></span></li>
                                <li>总码洋：<span id="total"></span></li>
                                <li>总实洋：<span id="real"></span></li>
                                <li>时间：<span id="time"></span></li>
                            </ul>
                            <hr />
                            <div class="input-group text-white" id="scanning" data-toggle="modal" data-target="#myModal1">
                                <a class="btn btn-warning btn-sm"><i class="fa fa-qrcode">扫描</i></a>
                            </div>
                            <div class="input-group text-white" id="Settlement" data-toggle="modal" data-target="#myModal2">
                                <a class="btn btn-success btn-sm"><i class="fa fa-jpy">收银</i></a>
                            </div>--%>
                            <div class="retailList">
                                <img src="../imgs/login.jpg" alt="img" class="img-fluid" />
                            </div>
                            <div class="container">
                                <div class="row">
                                    <div class="text-white col-sm-6 text-right" id="scanning" data-toggle="modal" data-target="#myModal1">
                                        <button class="btn btn-success btn-sm btnText">扫  描</button>
                                    </div>
                                    <div class="text-white col-sm-6 text-left" id="Settlement" data-toggle="modal" data-target="#myModal2">
                                        <button class="btn btn-danger btn-sm btnText">收  银</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- 合计模块 -->
                        <div class="col-md-9">
                            <fieldset>
                                <legend>
                                    <b>合计</b>
                                </legend>
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>品种：<span id="kind"></span></label></div>
                                        <div class="col-md-6">
                                            <label>数量：<span id="number"></span></label></div>
                                        <div class="col-md-6">
                                            <label>码洋：<span id="total"></span></label></div>
                                        <div class="col-md-6">
                                            <label>应收：<span id="real"></span></label></div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--选择一号多书模态框-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">请选择需要的图书</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table table-bordered mostTable text-center" id="table2">
                        <thead>
                            <tr>
                                <th>
                                    <div class="pretty inline">
                                        <input type="radio" name="radio" disabled="disabled">
                                        <label aria-disabled="true"><i class="mdi mdi-check"></i></label>
                                    </div>
                                </th>
                                <th>ISBN</th>
                                <th>书名</th>
                                <th>单价</th>
                                <th>出版社</th>
                            </tr>
                        </thead>
                        <%=getIsbn() %>
                    </table>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success btn-sm" id="btnAdd">提交</button>
                </div>
            </div>
        </div>
    </div>
    <!--扫描模态框-->
    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel1">请扫描</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="text" placeholder="请输入单头ID" id="scannSea" class="searchOne">
                </div>
            </div>
        </div>
    </div>
    <!--结算模态框-->
    <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" id="settleClose" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body" style="width: 360px; margin: 0 auto; padding-top: 8px;">
                    <h4 class="text-center">结算小票</h4>
                    <hr />
                    <ul class="list-unstyled">
                        <li>码洋合计：<span id="totalEnd"></span></li>
                        <li>码洋折扣：<input id="discountEnd" value="100" /></li>
                        <li>实洋合计：<span id="realEnd"></span></li>
                        <li>实付金额：<input id="copeEnd" /></li>
                        <li>找补金额：<span id="change"></span></li>
                    </ul>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success btn-sm" id="btnSettle">确定</button>
                </div>
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
    <script src="../js/customerRetail.js"></script>
    <script src="../js/jquery.tabletojson.js"></script>
</body>
</html>
