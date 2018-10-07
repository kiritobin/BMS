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
    <!-- css file -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css" />
    <link rel="stylesheet" href="../css/pagination.css" />
</head>

<body>
    <div class="retail-content">
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="input-group col-md-12">
                            <div class="btn-group" role="group">
                                <input type="text" placeholder="请输入ISBN" id="search" class="searchOne">
                                <button class="btn btn-info btn-sm" id="btnSearch">添加</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive col-md-10">
                            <table class="table mostTable table-bordered text-center" id="table">
                                <thead>
                                    <tr>
                                        <th>ISBN号</th>
                                        <th>书名</th>
                                        <th>单价</th>
                                        <th>数量</th>
                                        <th>码洋</th>
                                    </tr>
                                </thead>
                                <%=getData() %>
                            </table>
                        </div>
                        <div class="retailList col-md-2">
                            <h3 class="text-center">零售单</h3>
                            <hr />
                            <ul class="list-unstyled">
                                <li>时间：</li>
                                <li>单号：</li>
                                <li>种类：</li>
                                <li>数量：</li>
                                <li>总码洋：</li>
                                <li>总实洋：</li>
                            </ul>
                            <hr />
                            <div class="input-group text-white">
                                <a class="btn btn-warning btn-sm"><i class="fa fa-qrcode"></i></a>
                                <a class="btn btn-success btn-sm"><i class="fa fa-jpy"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="width: 380px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="close">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table model-table" id="table2">
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
                                <th>单价价</th>
                                <th>出版社</th>
                            </tr>
                        </thead>
                        <%=getIsbn() %>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success btn-sm" id="btnAdd">提交</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/demo.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/customerRetail.js"></script>
</body>

</html>
